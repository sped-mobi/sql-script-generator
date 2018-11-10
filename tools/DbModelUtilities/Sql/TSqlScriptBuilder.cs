// -----------------------------------------------------------------------
// <copyright file="TSqlScriptBuilder.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using DatabaseUtilities.Temp;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseUtilities.Sql
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

        public void AddCreateInsertProcedures(IEnumerable<Table> tables, QuoteType quoteType = QuoteType.NotQuoted)
        {
            AddBatch(Generator.GeneratePrintStatement($"========== Creating Insert Procedures =========="));
            foreach (Table table in tables)
            {
                AddCreateInsertProcedure(table, quoteType);
            }
        }

        public void AddCreateInsertProcedure(Table table, QuoteType quoteType = QuoteType.NotQuoted)
        {
            Database database = table.Parent;
            
            AddBatch(Generator.GeneratePrintStatement($"Creating insert procedure [{table.Name}]"));
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
                //var pkTable = key.Parent.Parent.Tables.FirstOrDefault(t => t.Name == key.PkTable);
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