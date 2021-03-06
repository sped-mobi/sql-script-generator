﻿// -----------------------------------------------------------------------
// <copyright file="TSqlScriptGenerator.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.IO;
using DatabaseUtilities.Temp;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DatabaseUtilities.Utilities
{
    public class TSqlScriptGenerator : IDisposable
    {
        private readonly TextWriter _writer;
        private readonly Sql150ScriptGenerator _generator;
        private readonly TSqlScriptBuilder _builder;

        public TSqlScriptGenerator(TextWriter writer, SqlScriptGeneratorOptions options)
        {
            _writer = writer;
            _generator = new Sql150ScriptGenerator(options);
            _builder = new TSqlScriptBuilder();
        }

        private void WriteHeader()
        {
            WriteLine("/*******************************************************************************");
            WriteLine("--     This code was generated by a tool.");
            WriteLine("--     Runtime Version:4.0.30319.42000");
            WriteLine("--");
            WriteLine("--     Changes to this file may cause incorrect behavior and will be lost if");
            WriteLine("--     the code is regenerated.");
            WriteLine("/*******************************************************************************");
            WriteLine();
        }

        public CreateDatabaseStatement GenerateCreateDatabaseStatement(Database database, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return TSqlScriptFactory.CreateDatabase(TSqlScriptFactory.Identifier(database.DatabaseName, quoteType));
        }

        private void Comment(string text)
        {
            Write("-- ");
            WriteLine(text);
        }

        private void CommentLine(string text)
        {
            Write("-- ");
            Write(text);
        }

        private void Write(string value)
        {
            _writer.Write(value);
        }

        private void WriteLine(string value)
        {
            _writer.WriteLine(value);
        }

        private void WriteLine()
        {
            _writer.WriteLine();
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
