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
            builder.AddCreateInsertProcedures(database.Tables, quoteType);

            return builder;
        }

        public SchemaObjectName GenerateSchemaObjectName(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return ScriptFactory.FullTableName(table.Parent.DatabaseName, table.Schema, table.Name, quoteType);
        }

        public SchemaObjectName GenerateSchemaAndIdentifierName(string schemaName, string identifierName, QuoteType quoteType = QuoteType.NotQuoted)
        {
            SchemaObjectName objectName = new SchemaObjectName();
            objectName.Identifiers.AddRange(ScriptFactory.Identifier(schemaName, quoteType), ScriptFactory.Identifier(identifierName, quoteType));
            return objectName;
        }

        public SchemaObjectName GenerateProcedureName(ProcedureKind kind, string databaseName, string tableName, QuoteType quoteType = QuoteType.NotQuoted)
        {
            SchemaObjectName objectName = new SchemaObjectName();
            objectName.Identifiers.AddRange(ScriptFactory.Identifier(tableName, quoteType));
            return objectName;
        }

        private static string CreateProcedureNameString(ProcedureKind kind, string databaseName, string tableName)
        {
            switch (kind)
            {
                case ProcedureKind.Insert:
                    return $"usp_{databaseName}_Insert{tableName}";
                case ProcedureKind.Update:
                    return $"usp_{databaseName}_Update{tableName}";
                case ProcedureKind.Delete:
                    return $"usp_{databaseName}_Delete{tableName}";
                case ProcedureKind.Read:
                    return $"usp_{databaseName}_Read{tableName}";
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind));
            }
            
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

        public CreateProcedureStatement GenerateCreateInsertStoredProcedure(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            Database database = table.Parent;
            Options options = database.Options;
            string storedProcedureName = string.Concat(options.ProcedurePrefix,"_", "Insert", table.Name);

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

        public CreateProcedureStatement GenerateCreateReadStoredProcedure(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            Database database = table.Parent;
            Options options = database.Options;
            string storedProcedureName = string.Concat(options.ProcedurePrefix, "_", "Insert", table.Name);

            List<ProcedureParameter> parameters = new List<ProcedureParameter>();

            //foreach (var column in table.Columns)
            //{
            //    parameters.Add(
            //        ScriptFactory.ProcedureParameter(CreateProcedureParameterVariableName(column), false,
            //            GenerateDataTypeReference(SqlDataType.Parse(column.DataType))));
            //}

            QueryExpression e;
            

            SelectStarExpression expression = new SelectStarExpression();
            expression.Qualifier = ScriptFactory.MultiPartIdentifier(
                ScriptFactory.Identifier(table.Schema, quoteType),
                ScriptFactory.Identifier(table.Name, quoteType));

            SelectStatement select = new SelectStatement();
            


            return ScriptFactory.CreateProcedure(false,
                ScriptFactory.ProcedureReference(
                    GenerateStoredProcedureName(storedProcedureName, table, quoteType)),
                        ScriptFactory.List(
                            ScriptFactory.BeginEndBlock(), null, null));
        }


        private static string CreateProcedureParameterVariableName(Column column)
        {
            return  "@" + CodeIdentifier.MakeCamel(column.Name);
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

    public static class NameFactory
    {
        private class NamingStrategy
        {

        }

        public static string CreateTableName(Table table)
        {
            return "";
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
