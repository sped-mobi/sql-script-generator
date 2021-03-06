﻿// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DatabaseUtilities
{
    public interface ISqlScriptGeneratorOptions
    {
        /// <summary>
        /// Gets or sets a boolean indicating if there should be a newline before the OFFSET clause
        /// </summary>
        bool NewLineBeforeOffsetClause { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if the INSERT column sources list should be spread across multiple lines
        /// </summary>
        bool MultilineInsertSourcesList { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if the INSERT column targets list should be spread across multiple lines
        /// </summary>
        bool MultilineInsertTargetsList { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if SET clause items should be listed on multiple lines
        /// </summary>
        bool MultilineSetClauseItems { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if SET clause items in an UPDATE statement should be aligned
        /// </summary>
        bool AlignSetClauseItem { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if the SET clause for an UPDATE statement should be indented
        /// </summary>
        bool IndentSetClause { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if the AS keyword should be written on its own line
        /// </summary>
        bool AsKeywordOnOwnLine { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if view statement columns should be listed as a multi-line list
        /// </summary>
        bool MultilineViewColumnsList { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if the view statement body should be indented.
        /// </summary>
        bool IndentViewBody { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if WHERE predicates (expressions separated by AND, and OR) should be written on multiple lines
        /// </summary>
        bool MultilineWherePredicatesList { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if select elements should be listed as a multi-line list
        /// </summary>
        bool MultilineSelectElementsList { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if the bodies of FROM, WHERE, GROUP BY, etc. clauses should be aligned
        /// </summary>
        bool AlignClauseBodies { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if there should be a newline before the OUTPUT clause
        /// </summary>
        bool NewLineBeforeOutputClause { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if a newline should be placed before an close parenthesis when writing a multi-line list in parenthesis
        /// </summary>
        bool NewLineBeforeCloseParenthesisInMultilineList { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if there should be a newline before the JOIN clause in a SELECT statement
        /// </summary>
        bool NewLineBeforeJoinClause { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if there should be a newline before the HAVING clause in a SELECT statement
        /// </summary>
        bool NewLineBeforeHavingClause { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if there should be a newline before the ORDER BY clause in a SELECT statement
        /// </summary>
        bool NewLineBeforeOrderByClause { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if there should be a newline before the GROUP BY clause in a SELECT statement
        /// </summary>
        bool NewLineBeforeGroupByClause { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if there should be a newline before the WHERE clause in a SELECT statement
        /// </summary>
        bool NewLineBeforeWhereClause { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if there should be a newline before the FROM clause in a SELECT statement
        /// </summary>
        bool NewLineBeforeFromClause { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if column definition fields (column name, data type, constraints) should be aligned into alignment columns.
        /// </summary>
        bool AlignColumnDefinitionFields { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if a semi colon should be included after each statement
        /// </summary>
        bool IncludeSemicolons { get; set; }

        /// <summary>
        /// Gets or sets the number of spaces to use when indenting text
        /// </summary>
        int IndentationSize { get; set; }

                
        /// <summary>
        /// Gets or sets the Sql version to generate script for
        /// </summary>
        SqlVersion SqlVersion { get; set; }

        /// <summary>
        /// Gets or sets the keyword casing option to use during script generation
        /// </summary>
        KeywordCasing KeywordCasing { get; set; }

        /// <summary>
        /// Gets or sets a boolean indicating if a newline should be placed before an open parenthesis when writing a multi-line list in parenthesis
        /// </summary>
        bool NewLineBeforeOpenParenthesisInMultilineList { get; set; }
    }
}
