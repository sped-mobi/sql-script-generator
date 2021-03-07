// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DatabaseUtilities
{
    public static partial class Program
    {
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
