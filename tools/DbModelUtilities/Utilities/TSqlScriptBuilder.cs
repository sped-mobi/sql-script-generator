// -----------------------------------------------------------------------
// <copyright file="TSqlScriptBuilder.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DatabaseUtilities.Sql;
using DatabaseUtilities.Temp;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using Factory = DatabaseUtilities.Utilities.TSqlScriptFactory;

namespace DatabaseUtilities.Utilities
{
    public class TSqlScriptBuilder : IDisposable
    {


        private TSqlScript Script { get; }

        private IList<TSqlBatch> Batches
        {
            get
            {
                return Script.Batches;
            }
        }

        public TSqlScriptBuilder()
        {
            Script = new TSqlScript();

        }

        public TSqlScript BuildScript(Database database, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddDropDatabaseIfExistsBatch(database, quoteType);
            AddCreateDatabaseBatch(database, quoteType);
            AddUseDatabaseBatch(database, quoteType);
            AddCreateTableBatches(database.Tables, quoteType);
            return Script;
        }

        public void AddCreateTableBatches(IEnumerable<Table> tables, QuoteType quoteType = QuoteType.NotQuoted)
        {
            foreach (Table table in tables)
            {
                AddCreateTableBatch(table, quoteType);
            }
        }

        public void AddCreateTableBatch(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {

            AddBatch(Factory.CreateTable(CreateSchemaObjectName(table, quoteType), Factory.TableDefinition(CreateColumnDefinitions(table, quoteType))));
        }

        public static List<ColumnDefinition> CreateColumnDefinitions(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            List<ColumnDefinition> columns = new List<ColumnDefinition>();
            foreach (var column in table.Columns)
            {
                //Debug.Print($"CURRENT COLUMN IS {column.Name} FROM TABLE {column.Parent.Name}");

                SqlDataType sqlDataType = SqlDataType.Parse(column.DataType);
                DataTypeReference dataType = CreateDataTypeReference(sqlDataType);
                if (column.IsIdentity)
                {
                    ColumnDefinition columnDef = Factory.IdentityColumn(column.Name, dataType, CreateIdentityOptions(column), quoteType);
                    columns.Add(columnDef);
                }
                else
                {
                    columns.Add(Factory.Column(column.Name, dataType, column.AllowNulls, quoteType));
                }
            }

            return columns;
        }

        public void AddUseDatabaseBatch(Database database, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Factory.Use(database.DatabaseName, quoteType));
        }

        public void AddDropDatabaseIfExistsBatch(Database database, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Factory.DropDatabase(true,
                Factory.Identifier(database.DatabaseName, quoteType)));
        }

        public void AddCreateDatabaseBatch(Database database, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Factory.CreateDatabase(
                Factory.Identifier(database.DatabaseName, quoteType)));
        }

        public void AddBatch(params TSqlStatement[] statements)
        {
            Batches.Add(Batch(statements));
        }

        public static TSqlBatch Batch(params TSqlStatement[] statements)
        {
            TSqlBatch batch = new TSqlBatch();
            foreach (var statement in statements)
            {
                batch.Statements.Add(statement);
            }

            return batch;
        }

        public static SchemaObjectName CreateSchemaObjectName(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return Factory.FullTableName(table.Parent.DatabaseName, table.Schema, table.Name, quoteType);
        }

        public static IdentityOptions CreateIdentityOptions(Column column)
        {
            return Factory.IdentityOptions(column.IdentityIncrement, column.IdentitySeed);
        }

        public static DataTypeReference CreateDataTypeReference(SqlDataType dataType)
        {
            if (dataType.IsMax)
            {
                return Factory.DataType(dataType.Type, Factory.Max());
            }

            if (dataType.Scale.HasValue)
            {
                return Factory.DataType(dataType.Type, dataType.Size.Value, dataType.Scale.Value);
            }

            if (dataType.Size.HasValue)
            {
                return Factory.DataType(dataType.Type, dataType.Size.Value);
            }

            return Factory.DataType(dataType.Type);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
        }
    }
}
