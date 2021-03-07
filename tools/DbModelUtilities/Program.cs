// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using DatabaseUtilities.Sql;
using DatabaseUtilities.Temp;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DatabaseUtilities
{
    public static partial class Program
    {
        // private const string FilePath = @"C:\Users\bmarshall\Source\Repos\sql-script-generator\tools\DbModelUtilities\Temp\Database.xml";

        [STAThread]
        public static void Main()
        {
            var parser = new TSql150Parser(true, SqlEngineType.Standalone);

            var fragment = parser.Parse(new StringReader("select top 1 [Column1],[Column2],[Column3] from [table] where [id] = 2;"), out IList<ParseError> errors);

            foreach (var error in errors)
            {
                Console.WriteLine($"Line: {error.Line} Column:{error.Column} Number: {error.Number} Message: {error.Message}");
            }

            foreach (var token in fragment.ScriptTokenStream)
            {
                Console.WriteLine($"{token.TokenType,20}: `{token.Text}`");
            }


            var items = TSqlSelectVisitor.VisitSelectStatements(fragment);

            var definedTypes = typeof(Sql100ScriptGenerator).Assembly.DefinedTypes;

            foreach(var definedType in definedTypes)
            {
                var baseType = definedType.BaseType;
                if (typeof(TableReference).IsAssignableFrom(baseType))
                {

                }
            }

            var types = typeof(Sql100ScriptGenerator).Assembly.DefinedTypes.Where(
                x => x.BaseType.Name == "TableReference");




            var props = typeof(ISqlScriptGeneratorOptions).GetProperties().OrderBy(x => x.Name) ;

            var sb = new StringBuilder();
            foreach(var prop in props)
            {
                string type = "";
                switch (prop.PropertyType.Name.ToLower())
                {
                    case "string":
                        {
                            type = "xs:string";
                            break;
                        }
                    case "boolean":
                        {
                            type = "xs:boolean";
                            break;
                        }
                    case "int32":
                        {
                            type = "xs:int";
                            break;
                        }
                    default:
                        {
                            type = prop.Name;
                            break;
                        }

                }

                //sb.AppendLine($"<xs:element name=\"{prop.Name}\" type=\"{type}\" />");
                sb.AppendLine($"<xs:element ref=\"{prop.Name}\" minOccurs=\"0\" />");
            }

            Console.Write(sb);
            Clipboard.SetText(sb.ToString());
            Debug.Print("");
        }

        private static void XYZ()
        {
            //Database db = Database.DeserializeFromFile(FilePath);
            //TSqlScriptGenerator generator = new TSqlScriptGenerator();
            //TSqlScript script = generator.GenerateScript(db, QuoteType.SquareBracket);
            //StringBuilder sb = new StringBuilder();
            //using (var sw = new StringWriter(sb))
            //using (TSqlScriptWriter writer = new TSqlScriptWriter(sw, Generator))
            //{
            //    writer.WriteScript(script, KeywordCasing.Uppercase);
            //}
            //Clipboard.SetText(sb.ToString());
            //Console.WriteLine(sb);
            //Console.ReadKey();e.ReadKey();
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
    }
}
