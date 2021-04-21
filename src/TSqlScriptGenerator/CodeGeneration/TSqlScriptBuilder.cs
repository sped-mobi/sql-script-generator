// -----------------------------------------------------------------------
// <copyright file="TSqlScriptBuilder.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.TransactSql.Configuration;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace Microsoft.SqlServer.TransactSql.CodeGeneration
{
    public class TSqlScriptBuilder
    {
        private readonly TSqlScript _script;

        private readonly TSqlScriptGenerator Generator;

        public TSqlScriptBuilder(TSqlScriptGenerator generator)
        {
            _script = new TSqlScript();
            Generator = generator;
        }

        public void AddAddFindProcedures(IEnumerable<Table> tables, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"========== Creating Find Procedures =========="));
            foreach (Table table in tables)
            {
                AddFindProcedure(table, quoteType);
            }
        }

        public void AddInsertProcedures(IEnumerable<Table> tables, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"========== Creating Insert Procedures =========="));
            foreach (Table table in tables)
            {
                AddInsertProcedure(table, quoteType);
            }
        }

        public void AddReadProcedures(IEnumerable<Table> tables, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"========== Creating Read Procedures =========="));
            foreach (Table table in tables)
            {
                AddReadProcedure(table, quoteType);
            }
        }

        public void AddUpdateProcedures(IEnumerable<Table> tables, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"========== Creating Update Procedures =========="));
            foreach (Table table in tables)
            {
                AddUpdateProcedure(table, quoteType);
            }
        }

        public void AddFindProcedure(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            Database database = table.Parent;
            string storedProcedureName = string.Concat("Find", table.Name);

            AddBatch(Generator.GeneratePrintStatement($"Creating procedure {storedProcedureName}..."));
            AddBatch(Generator.GenerateFindStoredProcedure(table, quoteType));
        }

        public void AddInsertProcedure(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            Database database = table.Parent;
            string storedProcedureName = string.Concat("Insert", table.Name);

            AddBatch(Generator.GeneratePrintStatement($"Creating procedure {storedProcedureName}..."));
            AddBatch(Generator.GenerateInsertStoredProcedure(table, quoteType));
        }

        public void AddReadProcedure(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            Database database = table.Parent;
            string storedProcedureName = string.Concat("Read", table.Name);
            AddBatch(Generator.GeneratePrintStatement($"Creating procedure {storedProcedureName}..."));
            AddBatch(Generator.GenerateReadStoredProcedure(table, quoteType));
        }

        public void AddUpdateProcedure(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            Database database = table.Parent;
            string storedProcedureName = string.Concat("Update", table.Name);
            AddBatch(Generator.GeneratePrintStatement($"Creating procedure {storedProcedureName}..."));
            AddBatch(Generator.GenerateUpdateStoredProcedure(table, quoteType));
        }

        public void AddCreateTableBatches(IEnumerable<Table> tables, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"========== Creating tables =========="));
            foreach (Table table in tables)
            {
                AddCreateTableBatch(table, quoteType);
            }
        }

        public void AddPrimaryKeyConstraintBatches(IEnumerable<Table> tables, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"========== Creating Primary Keys =========="));
            List<Index> keys = new List<Index>();
            foreach (Table table in tables)
            {
                foreach (var index in table.Indexes)
                {
                    if (index.IsPrimary)
                    {
                        keys.Add(index);
                    }
                }
            }

            for (int i = 0; i < keys.Count; i++)
            {
                Index key = keys[i];
                AddBatch(Generator.GeneratePrintStatement($"[{i+1} - {keys.Count}] Creating primary key constraint [{key.Name}] on table [{key.Parent.Name}]([{key.Members.FirstOrDefault().Column}])"));
                AddCreatePrimaryKeyBatch(key, quoteType);
            }
        }

        public void AddUniqueConstraintBatches(IEnumerable<Table> tables, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"========== Creating Unique Constraints =========="));
            List<Index> indexes = new List<Index>();
            foreach(var table in tables)
            {
                foreach(var index in table.Indexes)
                {
                    if (!index.IsPrimary)
                    {
                        if (index.IsUnique)
                        {
                            indexes.Add(index);
                        }
                    }
                }
            }
            for (int i = 0; i < indexes.Count; i++)
            {
                Index index = indexes[i];
                AddBatch(Generator.GeneratePrintStatement($"[{i + 1,-2} - {indexes.Count}] Creating unique constraint [{index.Name}]"));
                AddCreateUniqueConstraintBatch(index, quoteType);
            }
        }

        private void AddCreateUniqueConstraintBatch(Index index, QuoteType quoteType)
        {
            List<ColumnWithSortOrder> columns = new List<ColumnWithSortOrder>();
            foreach (var member in index.Members)
            {
                Column column = index.Parent.Columns.FirstOrDefault(c => c.Name == member.Column);
                columns.Add(
                    ScriptFactory.ColumnWithSortOrder(
                        ScriptFactory.ColumnReferenceExpression(
                            ScriptFactory.MultiPartIdentifier(
                                ScriptFactory.Identifier(member.Column, quoteType)), ColumnType.Regular), SortOrder.Ascending));
            }
            UniqueConstraintDefinition uniqueConstraint = ScriptFactory.UniqueConstraintDefinition(
                        ScriptFactory.Identifier(index.Name, quoteType),
                        false,
                        index.IsClustered,
                        null,
                        null,
                        null,
                        columns
                        );
            AddBatch(ScriptFactory.AlterTableAddTableElement(
                ScriptFactory.TableDefinition(
                null,
                null,
                new List<ConstraintDefinition> { uniqueConstraint },
                null),
                Generator.GenerateSchemaObjectName(index.Parent, quoteType)));

        }

        public void AddForeignKeyConstraintBatches(IEnumerable<Table> tables, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"========== Creating Froreign Keys =========="));
            List<ForeignKey> keys = new List<ForeignKey>();
            foreach (Table table in tables)
            {
                foreach (var foreignKey in table.ForeignKeys)
                {
                    keys.Add(foreignKey);
                }
            }
            for (int i = 0; i < keys.Count; i++)
            {
                ForeignKey key = keys[i];
                AddBatch(Generator.GeneratePrintStatement($"[{i+1,-2} - {keys.Count}] Creating foriegn key constraint [{key.Name}]"));
                AddCreateForeignKeyBatch(key, quoteType);
            }
        }

        public void AddCreateForeignKeyBatch(ForeignKey key, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var first = key.ForeignKeyColumns.FirstOrDefault();

            var pkTable = key.Parent.Parent.Tables.FirstOrDefault(t => t.Name == key.PkTable);

            ForeignKeyConstraintDefinition uniqueConstraint = ScriptFactory.ForeignKeyConstraintDefinition(
                ScriptFactory.Identifier(key.Name, quoteType),
                false,
                Generator.GenerateSchemaObjectName(pkTable, quoteType),
                DeleteUpdateAction.NotSpecified,
                DeleteUpdateAction.NotSpecified,
                key.ForeignKeyColumns.Select(fkc => ScriptFactory.Identifier(fkc.FkColumn, quoteType)),
                key.ForeignKeyColumns.Select(fkc => ScriptFactory.Identifier(fkc.PkColumn, quoteType))
            );
            AddBatch(ScriptFactory.AlterTableAddTableElement(
                ScriptFactory.TableDefinition(
                    null,
                    null,
                    new List<ConstraintDefinition> { uniqueConstraint },
                    null),
                Generator.GenerateSchemaObjectName(key.Parent, quoteType)));

        }

        public void AddCreatePrimaryKeyBatch(Index index, QuoteType quoteType = QuoteType.NotQuoted)
        {
            
            List<ColumnWithSortOrder> columns = new List<ColumnWithSortOrder>();



            foreach (var member in index.Members)
            {
                Column column = index.Parent.Columns.FirstOrDefault(c => c.Name == member.Column);
                columns.Add(
                    ScriptFactory.ColumnWithSortOrder(
                        ScriptFactory.ColumnReferenceExpression(
                            ScriptFactory.MultiPartIdentifier(
                                ScriptFactory.Identifier(member.Column, quoteType)), ColumnType.Regular), SortOrder.Ascending));
            }

            UniqueConstraintDefinition uniqueConstraint = ScriptFactory.UniqueConstraintDefinition(
                        ScriptFactory.Identifier(index.Name, quoteType),
                        true,
                        index.IsClustered,
                        null,
                        null,
                        null,
                        columns
                        );
            AddBatch(ScriptFactory.AlterTableAddTableElement(
                ScriptFactory.TableDefinition(
                null,
                null,
                new List<ConstraintDefinition> { uniqueConstraint },
                null),
                Generator.GenerateSchemaObjectName(index.Parent, quoteType)));


        }

        public void AddCreateTableBatch(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"Creating table [{table.Schema}].[{table.Name}]"));
            AddBatch(
             ScriptFactory.CreateTable(false, false, false,
                Generator.GenerateSchemaObjectName(table, quoteType),
                    ScriptFactory.TableDefinition(null,
                        Generator.GenerateColumnDefinitions(table, quoteType))));
        }


        public void AddUseMasterDatabaseBatch(QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"========== Preparation =========="));
            AddBatch(Generator.GenerateUseDatabaseStatement(ScriptFactory.Identifier("master", quoteType)));
        }

        public void AddUseDatabaseBatch(Database database, QuoteType quoteType = QuoteType.NotQuoted)
        {

            AddBatch(ScriptFactory.Use(ScriptFactory.Identifier(database.DatabaseName, quoteType)));
        }

        public void AddDropDatabaseIfExistsBatch(Database database, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"Dropping database [{database.DatabaseName}]"));
            AddBatch(Generator.GenerateDropDatabaseStatement(database, quoteType));
        }

        public void AddCreateDatabaseBatch(Database database, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"Creating database [{database.DatabaseName}]"));
            AddBatch(Generator.GenerateCreateDatabaseStatement(database, quoteType));
        }

        public void AddBatch(params TSqlStatement[] statements)
        {
            _script.Batches.Add(Batch(statements));
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

        public TSqlScript ToTSqlScript()
        {

            return _script;
        }

        public static implicit operator TSqlScript(TSqlScriptBuilder builder)
        {
            return builder.ToTSqlScript();
        }
    }
}