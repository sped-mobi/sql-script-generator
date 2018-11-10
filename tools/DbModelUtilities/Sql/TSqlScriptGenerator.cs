// -----------------------------------------------------------------------
// <copyright file="TSqlScriptGenerator.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using DatabaseUtilities.Temp;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Collections.Generic;

namespace DatabaseUtilities.Sql
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

            return builder;
        }

        public SchemaObjectName GenerateSchemaObjectName(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return ScriptFactory.FullTableName(table.Parent.DatabaseName, table.Schema, table.Name, quoteType);
        }

        public IEnumerable<ColumnDefinition> GenerateColumnDefinitions(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            List<ColumnDefinition> columns = new List<ColumnDefinition>();
            foreach (var column in table.Columns)
            {
                //Debug.Print($"CURRENT COLUMN IS {column.Name} FROM TABLE {column.Parent.Name}");



                SqlDataType sqlDataType = SqlDataType.Parse(column.DataType);
                DataTypeReference dataType = GenerateDataTypeReference(sqlDataType);
                if (column.IsIdentity)
                {
                    ColumnDefinition columnDef = IdentityColumn(column.Name, dataType, CreateIdentityOptions(column), quoteType);
                    columns.Add(columnDef);
                }
                else if (column.RowGuid)
                {


                    if (!string.IsNullOrEmpty(column.DefaultValue))
                    {
                        ColumnDefinition rowGuidCol = Column(column.Name, column.DefaultValue, column.DefaultName, dataType, column.AllowNulls, column.RowGuid, quoteType);
                        //rowGuidCol.DefaultConstraint = ScriptFactory.DefaultConstraintDefinition(
                        //    ScriptFactory.Identifier(column.Name, quoteType),
                        //    ScriptFactory.Identifier(column.DefaultName),
                        //    false,

                        columns.Add(rowGuidCol);
                    }


                }
                else
                {
                    columns.Add(Column(column.Name, dataType, column.AllowNulls, quoteType));
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

        public DropIndexStatement GenerateDropIndexStatement(Index index, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return ScriptFactory.DropIndex(false, ScriptFactory.DropIndexClause(ScriptFactory.Identifier(index.Name, quoteType)));
        }

        public UseStatement GenerateUseDatabaseStatement(Identifier identifier)
        {
            return ScriptFactory.Use(identifier);
        }

        public static ColumnDefinition IdentityColumn(string columnName,
    DataTypeReference dataType,
    IdentityOptions options,
    QuoteType quoteType = QuoteType.NotQuoted)
        {
            ColumnDefinition definition = Column(columnName, dataType, false, quoteType);
            definition.IdentityOptions = options;
            return definition;
        }

        public static ColumnDefinition Column(string columnName,
            string defaultValue,
            string defaultName,
            DataTypeReference dataType,
            bool nullable = false,
            bool isRowGuid = false,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            return Column2(columnName, defaultValue, defaultName, dataType, nullable, isRowGuid, quoteType);
        }

        public static ColumnDefinition Column(string columnName,
            DataTypeReference dataType,
            bool nullable = false,
            bool isRowGuid = false,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            return Column(ScriptFactory.Identifier(columnName, quoteType), dataType, nullable, isRowGuid);
        }

        public static ColumnDefinition Column(string columnName,
            DataTypeReference dataType,
            bool nullable = false,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            return Column(ScriptFactory.Identifier(columnName, quoteType), dataType, nullable);
        }



        public static ColumnDefinition Column2(string columnName,
            string defaultValue,
            string defaultName,
            DataTypeReference dataType,
            bool nullable = false,
            bool isRowGuid = false,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            var fragment = new ColumnDefinition();
            fragment.Constraints.Add(ScriptFactory.NullableConstraintDefinition(null, nullable));
            fragment.Constraints.Add(ScriptFactory.DefaultConstraintDefinition(
                null,
                ScriptFactory.Identifier(defaultName, quoteType),
                false,
                ScriptFactory.IdentifierLiteral(defaultValue)));
            fragment.ColumnIdentifier = ScriptFactory.Identifier(columnName, quoteType);
            fragment.IsRowGuidCol = isRowGuid;
            fragment.DataType = dataType;

            return fragment;
        }

        public static ColumnDefinition Column(Identifier columnName, DataTypeReference dataType, bool nullable = false, bool isRowGuid = false)
        {
            var fragment = new ColumnDefinition();
            fragment.ColumnIdentifier = columnName;
            fragment.IsRowGuidCol = isRowGuid;
            fragment.DataType = dataType;
            fragment.Constraints.Add(ScriptFactory.NullableConstraintDefinition(null, nullable));
            return fragment;
        }
    }
}
