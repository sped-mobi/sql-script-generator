// -----------------------------------------------------------------------
// <copyright file="TSqlScriptGenerator.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.SqlServer.TransactSql.Configuration;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace Microsoft.SqlServer.TransactSql.CodeGeneration
{
    public class TSqlScriptGenerator
    {
        public TSqlScript GenerateScript(Database database, QuoteType quoteType = QuoteType.NotQuoted)
        {
            TSqlScriptBuilder builder = new TSqlScriptBuilder(this);

            builder.AddUseMasterDatabaseBatch(quoteType);
            builder.AddDropDatabaseIfExistsBatch(database, quoteType);
            builder.AddCreateDatabaseBatch(database, quoteType);
            builder.AddUseDatabaseBatch(database, quoteType);
            builder.AddCreateTableBatches(database.Tables, quoteType);
            builder.AddPrimaryKeyConstraintBatches(database.Tables, quoteType);
            builder.AddForeignKeyConstraintBatches(database.Tables, quoteType);
            builder.AddCreateInsertProcedures(database.Tables, quoteType);

            return builder;
        }

        public SchemaObjectName GenerateSchemaObjectName(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return ScriptFactory.FullTableName(table.Parent.DatabaseName, table.Schema, table.Name, quoteType);
        }

        public SchemaObjectName GenerateStoredProcedureName(string procedureName, Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            SchemaObjectName objectName = new SchemaObjectName();
            objectName.Identifiers.AddRange(ScriptFactory.Identifier(table.Schema),ScriptFactory.Identifier(procedureName, quoteType));
            return objectName;
        }

        public IEnumerable<ColumnDefinition> GenerateColumnDefinitions(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            List<ColumnDefinition> columns = new List<ColumnDefinition>();
            foreach (var column in table.Columns)
            {
                SqlDataType sqlDataType = SqlDataType.Parse(column.DataType);
                DataTypeReference dataType = GenerateDataTypeReference(sqlDataType);
                if (column.IsIdentity)
                {
                    ColumnDefinition columnDef = ScriptFactory.IdentityColumn(column.Name, dataType, CreateIdentityOptions(column), quoteType);
                    columns.Add(columnDef);
                }
                else if (column.RowGuid)
                {


                    if (!string.IsNullOrEmpty(column.DefaultValue))
                    {
                        ColumnDefinition rowGuidCol = ScriptFactory.ColumnDefinition(column.Name, column.DefaultValue, column.DefaultName, dataType, column.AllowNulls, column.RowGuid, quoteType);
                        columns.Add(rowGuidCol);
                    }


                }
                else
                {
                    columns.Add(ScriptFactory.ColumnDefinition(column.Name, dataType, column.AllowNulls, quoteType));
                }
            }

            return columns;
        }

        public static IdentityOptions CreateIdentityOptions(Column column)
        {
            return ScriptFactory.IdentityOptions(
                false,
                ScriptFactory.IntegerLiteral(column.IdentityIncrement.ToString()),
                ScriptFactory.IntegerLiteral(column.IdentitySeed.ToString()));
        }

        public static DataTypeReference GenerateDataTypeReference(SqlDataType dataType)
        {
            if (dataType.IsMax)
            {
                return ScriptFactory.SqlDataType(dataType.Type, ScriptFactory.MaxLiteral("MAX"));
            }

            if (dataType.Scale.HasValue)
            {
                return ScriptFactory.SqlDataType(dataType.Type, dataType.Size.Value, dataType.Scale.Value);
            }

            return dataType.Size.HasValue ? ScriptFactory.SqlDataType(dataType.Type, dataType.Size.Value) : ScriptFactory.SqlDataType(dataType.Type);
        }

        public PrintStatement GeneratePrintStatement(string message)
        {
            return ScriptFactory.Print(ScriptFactory.ParenthesisExpression(ScriptFactory.StringLiteral(message, false, false)));
        }

        public CreateDatabaseStatement GenerateCreateDatabaseStatement(Database database, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return ScriptFactory.CreateDatabase(ScriptFactory.Identifier(database.DatabaseName, quoteType));
        }

        public DropDatabaseStatement GenerateDropDatabaseStatement(Database database, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return ScriptFactory.DropDatabase(true, ScriptFactory.Identifier(database.DatabaseName, quoteType));
        }

        public CreateIndexStatement GenerateCreateIndexStatement(Index index, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return ScriptFactory.CreateIndex(ScriptFactory.Identifier(index.Name, quoteType), false, index.IsUnique, index.IsClustered);
        }

        public UseStatement GenerateUseDatabaseStatement(Identifier identifier)
        {
            return ScriptFactory.Use(identifier);
        }

        private bool IsPrimaryKey(Column column)
        {
            Table table = column.Parent;
            Database database = table.Parent;

            foreach (var constraint in table.Indexes)
            {
                if (constraint.IsPrimary)
                {
                    foreach (var member in constraint.Members)
                    {
                        if (member.Column == column.Name)
                        {
                            return true;
                        }
                    }
                }

            }

            return false;
        }

        public CreateProcedureStatement GenerateCreateStoreProcedureStatement(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            Database database = table.Parent;
            Options options = database.Options;
            string storedProcedureName = string.Concat(options.ProcedurePrefix, "Insert", table.Name);

            List<ColumnReferenceExpression> columns = new List<ColumnReferenceExpression>();
            List<ScalarExpression> scalarExpressions = new List<ScalarExpression>();
            List<RowValue> rowValues = new List<RowValue>();
            RowValue rowValue = ScriptFactory.RowValue();
            rowValues.Add(rowValue);

            List<ProcedureParameter> parameters = new List<ProcedureParameter>();

            foreach (var column in table.Columns)
            {
                if (IsPrimaryKey(column))
                    continue;

                scalarExpressions.Add(
                    ScriptFactory.IdentifierLiteral(column.Name, quoteType));

                columns.Add(
                    ScriptFactory.ColumnReferenceExpression(
                        ScriptFactory.MultiPartIdentifier(
                            ScriptFactory.Identifier(column.Name, quoteType))));

                parameters.Add(
                    ScriptFactory.ProcedureParameter(CreateProcedureParameterVariableName(column),false,
                    GenerateDataTypeReference(SqlDataType.Parse(column.DataType))));
            }
            rowValue.ColumnValues.AddRange(scalarExpressions);

            var statement = ScriptFactory.CreateProcedure(false, 
                ScriptFactory.ProcedureReference(
                    GenerateStoredProcedureName(storedProcedureName,table, quoteType)),
                        ScriptFactory.List(
                            ScriptFactory.BeginEndBlock(
                                ScriptFactory.List(
                                    ScriptFactory.PredicateSet(true, SetOptions.NoCount),
                                    ScriptFactory.Insert(
                                        ScriptFactory.InsertSpecification(
                                            ScriptFactory.ValuesInsertSource(false,rowValues), 
                                            ScriptFactory.NamedTableReference(
                                                GenerateSchemaObjectName(table, quoteType)),
                                                null,null,null,InsertOption.Into, columns)),
                                    ScriptFactory.Return(ScriptFactory.IntegerLiteral("1"))))),null,null, parameters);

            statement.StatementList
        }

        private static string CreateProcedureParameterVariableName(Column column)
        {
            return  "@" + CodeIdentifier.MakeCamel(column.Name);
        }
    }
}
