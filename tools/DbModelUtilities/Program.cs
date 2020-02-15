// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using DatabaseUtilities.Sql;
using DatabaseUtilities.Temp;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DatabaseUtilities
{
    public static class Program
    {
        private const string FilePath = @"C:\Users\bmarshall\Source\Repos\sql-script-generator\tools\DbModelUtilities\Temp\Database.xml";

        [STAThread]
        public static void Main()
        {
            //var parser = new TSql150Parser(true, SqlEngineType.Standalone);

            //var fragment = parser.Parse(new StringReader(query), out IList<ParseError> errors);

            //foreach (var token in fragment.ScriptTokenStream)
            //{
            //    Console.WriteLine($"{token.TokenType} `{token.Text}`");
            //}


            //var items = TSqlSelectVisitor.VisitSelectStatements(fragment);



            Database db = Database.DeserializeFromFile(FilePath);

            TSqlScriptGenerator generator = new TSqlScriptGenerator();

            TSqlScript script = generator.GenerateScript(db, QuoteType.SquareBracket);

            StringBuilder sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            using (TSqlScriptWriter writer = new TSqlScriptWriter(sw, Generator))
            {
                writer.WriteScript(script, KeywordCasing.Uppercase);
            }

            Clipboard.SetText(sb.ToString());

            Console.WriteLine(sb);

            Console.ReadKey();
        }

        private static Sql150ScriptGenerator Generator
        {
            get
            {
                Sql150ScriptGenerator generator = new Sql150ScriptGenerator();
                generator.Options.AlignClauseBodies = true;
                generator.Options.AsKeywordOnOwnLine = true;
                generator.Options.KeywordCasing = KeywordCasing.Lowercase;
                generator.Options.IncludeSemicolons = true;
                generator.Options.IndentationSize = 4;
                generator.Options.SqlVersion = SqlVersion.Sql150;
                generator.Options.AlignColumnDefinitionFields = true;
                generator.Options.NewLineBeforeOpenParenthesisInMultilineList = true;
                generator.Options.NewLineBeforeCloseParenthesisInMultilineList = true;
                return generator;
            }
        }

        public class TSqlSelectVisitor : TSqlConcreteFragmentVisitor
        {
            public static IList<AlterTableAddTableElementStatement> VisitAlterTableAddTableElementStatements(TSqlFragment fragment)
            {
                TSqlSelectVisitor visitor = new TSqlSelectVisitor();
                fragment.Accept(visitor);
                return visitor.AlterTableAddTableElementStatements;
            }

            public static IList<CreateTableStatement> VisitCreateTableStatements(TSqlFragment fragment)
            {
                TSqlSelectVisitor visitor = new TSqlSelectVisitor();
                fragment.Accept(visitor);
                return visitor.CreateTableStatements;
            }

            public static IList<SelectStatement> VisitSelectStatements(TSqlFragment fragment)
            {
                TSqlSelectVisitor visitor = new TSqlSelectVisitor();
                fragment.Accept(visitor);
                return visitor.SelectStatements;
            }

            public static IList<CreateProcedureStatement> VisitCreateProcedureStatements(TSqlFragment fragment)
            {
                TSqlSelectVisitor visitor = new TSqlSelectVisitor();
                fragment.Accept(visitor);
                return visitor.CreateProcedureStatements;
            }

            public static IList<ProcedureParameter> VisitProcedureParameters(TSqlFragment fragment)
            {
                TSqlSelectVisitor visitor = new TSqlSelectVisitor();
                fragment.Accept(visitor);
                return visitor.ProcedureParameters;
            }

            public TSqlSelectVisitor()
            {
                SelectStatements = new List<SelectStatement>();
                AlterTableAddTableElementStatements = new List<AlterTableAddTableElementStatement>();
                CreateTableStatements = new List<CreateTableStatement>();
                CreateProcedureStatements = new List<CreateProcedureStatement>();
                ProcedureParameters = new List<ProcedureParameter>();
            }

            public IList<SelectStatement> SelectStatements { get; }

            public IList<CreateTableStatement> CreateTableStatements { get; }

            public IList<AlterTableAddTableElementStatement> AlterTableAddTableElementStatements { get; }

            public IList<CreateProcedureStatement> CreateProcedureStatements { get; }

            public IList<ProcedureParameter> ProcedureParameters { get; }

            public override void Visit(SelectStatement node)
            {
                SelectStatements.Add(node);
            }

            public override void Visit(AlterTableAddTableElementStatement node)
            {
                AlterTableAddTableElementStatements.Add(node);
            }

            public override void Visit(CreateTableStatement node)
            {
                CreateTableStatements.Add(node);
            }

            public override void Visit(CreateProcedureStatement node)
            {
                CreateProcedureStatements.Add(node);
            }

            public override void Visit(ProcedureParameter node)
            {
                ProcedureParameters.Add(node);
            }
        }
    }
}
