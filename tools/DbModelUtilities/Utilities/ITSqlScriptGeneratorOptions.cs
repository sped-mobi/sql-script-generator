// -----------------------------------------------------------------------
// <copyright file="ITSqlScriptGeneratorOptions.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DatabaseUtilities.Utilities
{
    public interface ITSqlScriptGeneratorOptions
    {
        KeywordCasing KeywordCasing { get; set; }

        SqlVersion SqlVersion { get; set; }

        SqlEngineType SqlEngineType { get; set; }

        int IndentationSize { get; set; }

        bool IncludeSemicolons { get; set; }

        bool AlignColumnDefinitionFields { get; set; }

        bool NewLineBeforeFromClause { get; set; }

        bool NewLineBeforeWhereClause { get; set; }

        bool NewLineBeforeGroupByClause { get; set; }

        bool NewLineBeforeOrderByClause { get; set; }

        bool NewLineBeforeHavingClause { get; set; }

        bool NewLineBeforeJoinClause { get; set; }

        bool NewLineBeforeOffsetClause { get; set; }

        bool NewLineBeforeOutputClause { get; set; }

        bool AlignClauseBodies { get; set; }

        bool MultilineSelectElementsList { get; set; }

        bool MultilineWherePredicatesList { get; set; }

        bool IndentViewBody { get; set; }

        bool MultilineViewColumnsList { get; set; }

        bool AsKeywordOnOwnLine { get; set; }

        bool IndentSetClause { get; set; }

        bool AlignSetClauseItem { get; set; }

        bool MultilineSetClauseItems { get; set; }

        bool MultilineInsertTargetsList { get; set; }

        bool MultilineInsertSourcesList { get; set; }

        bool NewLineBeforeOpenParenthesisInMultilineList { get; set; }

        bool NewLineBeforeCloseParenthesisInMultilineList { get; set; }
    }
}