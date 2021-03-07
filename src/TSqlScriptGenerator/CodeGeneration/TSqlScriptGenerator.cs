// -----------------------------------------------------------------------
// <copyright file="TSqlScriptGenerator.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
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
            builder.AddInsertProcedures(database.Tables, quoteType);
            builder.AddReadProcedures(database.Tables, quoteType);
            builder.AddAddFindProcedures(database.Tables, quoteType);

            return builder;
        }

        public SchemaObjectName GenerateSchemaObjectName(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return ScriptFactory.FullTableName(table.Parent.DatabaseName, table.Schema, table.Name, quoteType);
        }

        public SchemaObjectName GenerateSchemaObjectName(Column column, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new SchemaObjectName();
            retVal.Identifiers.Add(ScriptFactory.Identifier(column.Parent.Name, quoteType));
            retVal.Identifiers.Add(ScriptFactory.Identifier(column.Name, quoteType));
            return retVal;
        }

        public SchemaObjectName GenerateSchemaAndIdentifierName(string schemaName, string identifierName, QuoteType quoteType = QuoteType.NotQuoted)
        {
            SchemaObjectName objectName = new SchemaObjectName();
            objectName.Identifiers.AddRange(ScriptFactory.Identifier(schemaName, quoteType), ScriptFactory.Identifier(identifierName, quoteType));
            return objectName;
        }

        public SchemaObjectName GenerateStoredProcedureName(string procedureName, Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            SchemaObjectName objectName = new SchemaObjectName();
            objectName.Identifiers.AddRange(ScriptFactory.Identifier(table.Schema, quoteType),ScriptFactory.Identifier(procedureName, quoteType));
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

        public CreateProcedureStatement GenerateInsertStoredProcedure(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            Database database = table.Parent;
            string storedProcedureName = string.Concat("Insert", table.Name);

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
                    ScriptFactory.IdentifierLiteral(
                        CreateProcedureParameterVariableName(column), QuoteType.NotQuoted));

                columns.Add(
                    ScriptFactory.ColumnReferenceExpression(
                        ScriptFactory.MultiPartIdentifier(
                            ScriptFactory.Identifier(column.Name, quoteType))));

                parameters.Add(
                    ScriptFactory.ProcedureParameter(CreateProcedureParameterVariableName(column),false,
                    GenerateDataTypeReference(SqlDataType.Parse(column.DataType))));
            }
            rowValue.ColumnValues.AddRange(scalarExpressions);

            StatementList statements = ScriptFactory.List(
                                    ScriptFactory.PredicateSet(true, SetOptions.NoCount),
                                    ScriptFactory.Insert(
                                        ScriptFactory.InsertSpecification(
                                            ScriptFactory.ValuesInsertSource(false, rowValues),
                                            ScriptFactory.NamedTableReference(
                                                GenerateSchemaObjectName(table, quoteType)),
                                                null, null, null, InsertOption.Into, columns)),
                                    ScriptFactory.Return(ScriptFactory.IntegerLiteral("1")));
            var statement = ScriptFactory.CreateProcedure(false,
                ScriptFactory.ProcedureReference(
                    GenerateStoredProcedureName(storedProcedureName, table, quoteType)),
                        ScriptFactory.List(
                            ScriptFactory.BeginEndBlock(statements)),null,null, parameters);

            return statement;
        }

        public CreateProcedureStatement GenerateFindStoredProcedure(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            Database database = table.Parent;
            var pkColumn = GetPrimaryKeyColumnName(table);
            var columnNames = GetColumnNamesWithoutPrimaryKey(table, pkColumn);
            var pkDataType = table.Columns.Single(x => x.Name == pkColumn).DataType.ToSqlDataTypeOption();
            string storedProcedureName = string.Concat("Find", table.Name);
            var parameters = new List<ProcedureParameter>();
            parameters.Add(ScriptFactory.ProcedureParameter(
                              ScriptFactory.Identifier(MakeVariableName(pkColumn)),
                              false,
                              ScriptFactory.SqlDataType(pkDataType)));



            StatementList statements = ScriptFactory.List(
                                        ScriptFactory.Select(
                                            default(Identifier),
                                            ScriptFactory.Query(
                                                ScriptFactory.From(ScriptFactory.NamedTableReference(GenerateSchemaObjectName(table, quoteType))),
                                                ScriptFactory.Where(ScriptFactory.BooleanEqualsComparison(ScriptFactory.IdentifierLiteral(pkColumn, quoteType), ScriptFactory.VariableReference(pkColumn))),
                                                ScriptFactory.TopRowFilter(false, false, ScriptFactory.IntegerLiteral("1")),
                                                ScriptFactory.ElementList(columnNames.Select(x => ScriptFactory.SelectScalarExpression(x, quoteType)))
                                            )
                                        )
                                       );
            var statement = ScriptFactory.CreateProcedure(false,
                ScriptFactory.ProcedureReference(
                    GenerateStoredProcedureName(storedProcedureName, table, quoteType)),
                        ScriptFactory.List(
                            ScriptFactory.BeginEndBlock(statements)), null, null, parameters);

            return statement;
        }

        private string GetPrimaryKeyColumnName(Table table)
        {
            return table.Columns.Single(x => IsPrimaryKey(x)).Name;
        }

        public CreateProcedureStatement GenerateReadStoredProcedure(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var pkColumn = table.Indexes.SingleOrDefault(x => x.IsPrimary).Name;
            var columnNames = GetColumnNamesWithoutPrimaryKey(table, pkColumn);

            Database database = table.Parent;
            string storedProcedureName = string.Concat("Read", table.Name);
            StatementList statements = ScriptFactory.List(
                                        ScriptFactory.Select(default(Identifier),
                                            ScriptFactory.Query(
                                                ScriptFactory.From(ScriptFactory.NamedTableReference(GenerateSchemaObjectName(table, quoteType))),
                                                 ScriptFactory.ElementList(columnNames.Select(x => ScriptFactory.SelectScalarExpression(x, quoteType))))));
            var statement = ScriptFactory.CreateProcedure(false,
                ScriptFactory.ProcedureReference(
                    GenerateStoredProcedureName(storedProcedureName, table, quoteType)),
                        ScriptFactory.List(
                            ScriptFactory.BeginEndBlock(statements)), null, null, null);

            return statement;
        }

        private static IEnumerable<string> GetColumnNamesWithoutPrimaryKey(Table table, string pkColumn)
        {
            return table.Columns.Where(x => x.Name != pkColumn).Select(x => x.Name);
        }

        private static string MakeVariableName(string name) => "@" + name;


        private static string CreateProcedureParameterVariableName(Column column)
        {
            return MakeVariableName(column.Name);
        }
    }

    public enum ProcedureKind
    {
        Insert,
        Update,
        Delete,
        Read
    }

    public enum NameKind
    {
        Database,
        Schema,
        Table,
        Column,
        CheckConstraint,
        DefaultConstraint,
        UniqueIndex,
        PrimaryKey,
        ForeignKey
    }

    public class NameStrategy : INamingStrategy
    {
        public string CheckConstraint(Index index)
        {
            throw new NotImplementedException();
        }

        public string Column(Column column)
        {
            throw new NotImplementedException();
        }

        public string Database(Database database)
        {
            throw new NotImplementedException();
        }

        public string ForeignKey(ForeignKey foreignKey)
        {
            throw new NotImplementedException();
        }

        public string Procedure(ProcedureKind kind, Table table)
        {
            throw new NotImplementedException();
        }

        public string Table(Table table)
        {
            throw new NotImplementedException();
        }

        public string UniqueKey(Index index)
        {
            throw new NotImplementedException();
        }
    }

    public interface INamingStrategy
    {
        string Database(Database database);
        string Table(Table table);
        string Procedure(ProcedureKind kind, Table table);
        string Column(Column column);
        string UniqueKey(Index index);
        string ForeignKey(ForeignKey foreignKey);
        string CheckConstraint(Index index);

    }

    //public abstract class AbstractNamingStrategy : INamingStrategy
    //{

    //}

    //public sealed class PrefixBasedNamingStrategy : AbstractNamingStrategy
    //{
    //}
}
