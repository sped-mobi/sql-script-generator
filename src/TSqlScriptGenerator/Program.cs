﻿// -----------------------------------------------------------------------
// <copyright file="TSqlScriptGenerator.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Extensions.CommandLineUtils;
using Microsoft.SqlServer.TransactSql.Configuration;
using System;
using System.IO;
using System.Text;
using Microsoft.SqlServer.TransactSql.CodeGeneration;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Windows;
using System.Diagnostics;

namespace Microsoft.SqlServer.TransactSql
{
    public static class Program
    {
        [STAThread]
        public static void Main(params string[] args)
        {
            var app = new CommandLineApplication();
            app.Name = nameof(Program);
            app.Description = Strings.TSqlScriptGenerator_ApplicationDescription;
            app.HelpOption(Strings.TSqlScriptGenerator_HelpString);
            var configOption = app.Option(Strings.TSqlScriptGenerator_Config_File_Template,
                Strings.TSqlScriptGenerator_Config_File_Description, CommandOptionType.SingleValue);
            var clipboardOption = app.Option("-b|--clip", 
                "Outputs generated code to clipboard as well.", CommandOptionType.NoValue);
            var outputFileOption = app.Option("-o|--out-file",
                "Outputs generated code to a sql script file of your choice.",
                CommandOptionType.SingleValue);

            var pauseOption = app.Option(Strings.TSqlScriptGenerator_Pause_Template, Strings.TSqlScriptGenerator_Pause_Description, CommandOptionType.NoValue);

            app.Command("generate",
                command =>
                {
                    command.Options.Add(configOption);
                    command.Options.Add(pauseOption);
                    command.Options.Add(clipboardOption);
                    command.Options.Add(outputFileOption);

                    command.Description = Strings.TSqlScriptGenerator_GenerateCommandDescription;
                    command.HelpOption(Strings.TSqlScriptGenerator_HelpString);
                    command.OnExecute(() =>
                    {
                        if (configOption.HasValue())
                        {
                            Database database = Database.DeserializeFromFile(configOption.Value());

                            TSqlScriptGenerator generator = new TSqlScriptGenerator();
                            

                            TSqlScript script = generator.GenerateScript(database, QuoteType.SquareBracket);

                            StringBuilder sb = new StringBuilder();
                            using (var sw = new StringWriter(sb))
                            using (TSqlScriptWriter writer = new TSqlScriptWriter(sw, GetGenerator(database.GenerationOptions)))
                            {
                                writer.WriteScript(script, KeywordCasing.Uppercase);
                            }

                            if (clipboardOption.HasValue() && clipboardOption.Value() == "on")
                            {
                                Clipboard.SetText(sb.ToString());
                            }

                            if (outputFileOption.HasValue() && !string.IsNullOrEmpty(outputFileOption.Value()))
                            {
                                File.WriteAllText(outputFileOption.Value(), sb.ToString());
                            }
                        }

                        Console.WriteLine("Complete!");
                        if (!pauseOption.HasValue() || pauseOption.Value() != "on") return 0;
                        Console.Write(Strings.TSqlScriptGenerator_Pause_Message);
                        Console.ReadKey();
                        return 0;
                    });
                });

            app.Execute(args);

            Process.Start("notepad.exe", "Database.sql");

        }

        private static SqlScriptGenerator GetGenerator(GenerationOptions options)
        {
            SqlScriptGenerator generator = null;
            switch (options.SqlVersion)
            {

                case SqlVersion.Sql80:
                    generator = new Sql80ScriptGenerator();
                    break;
                case SqlVersion.Sql100:
                    generator = new Sql100ScriptGenerator();
                    break;
                case SqlVersion.Sql110:
                    generator = new Sql110ScriptGenerator();
                    break;
                case SqlVersion.Sql120:
                    generator = new Sql120ScriptGenerator();
                    break;
                case SqlVersion.Sql130:
                    generator = new Sql130ScriptGenerator();
                    break;
                case SqlVersion.Sql140:
                    generator = new Sql140ScriptGenerator();
                    break;
                case SqlVersion.Sql150:
                    generator = new Sql150ScriptGenerator();
                    break;
                default:
                    generator = new Sql90ScriptGenerator();
                    break;
            }

            generator.Options.Apply(options);

            return generator;
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
                generator.Options.MultilineInsertSourcesList = true;
                generator.Options.MultilineInsertTargetsList = true;
                generator.Options.NewLineBeforeOpenParenthesisInMultilineList = true;
                generator.Options.NewLineBeforeCloseParenthesisInMultilineList = true;
                return generator;
            }
        }

    }
}
