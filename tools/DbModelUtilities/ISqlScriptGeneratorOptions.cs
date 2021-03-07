// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DatabaseUtilities
{
    public static partial class Program
    {
        public interface ISqlScriptGeneratorOptions
        {

            //
            // Summary:
            //     Gets or sets a boolean indicating if there should be a newline before the OFFSET
            //     clause
            bool NewLineBeforeOffsetClause { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if the INSERT column sources list should be
            //     spread across multiple lines
            bool MultilineInsertSourcesList { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if the INSERT column targets list should be
            //     spread across multiple lines
            bool MultilineInsertTargetsList { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if SET clause items should be listed on multiple
            //     lines
            bool MultilineSetClauseItems { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if SET clause items in an UPDATE statement
            //     should be aligned
            bool AlignSetClauseItem { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if the SET clause for an UPDATE statement should
            //     be indented
            bool IndentSetClause { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if the AS keyword should be written on its
            //     own line
            bool AsKeywordOnOwnLine { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if view statement columns should be listed
            //     as a multi-line list
            bool MultilineViewColumnsList { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if the view statement body should be indented.
            bool IndentViewBody { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if WHERE predicates (expressions separated
            //     by AND, and OR) should be written on multiple lines
            bool MultilineWherePredicatesList { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if select elements should be listed as a multi-line
            //     list
            bool MultilineSelectElementsList { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if the bodies of FROM, WHERE, GROUP BY, etc.
            //     clauses should be aligned
            bool AlignClauseBodies { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if there should be a newline before the OUTPUT
            //     clause
            bool NewLineBeforeOutputClause { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if a newline should be placed before an close
            //     parenthesis when writing a multi-line list in parenthesis
            bool NewLineBeforeCloseParenthesisInMultilineList { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if there should be a newline before the JOIN
            //     clause in a SELECT statement
            bool NewLineBeforeJoinClause { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if there should be a newline before the HAVING
            //     clause in a SELECT statement
            bool NewLineBeforeHavingClause { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if there should be a newline before the ORDER
            //     BY clause in a SELECT statement
            bool NewLineBeforeOrderByClause { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if there should be a newline before the GROUP
            //     BY clause in a SELECT statement
            bool NewLineBeforeGroupByClause { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if there should be a newline before the WHERE
            //     clause in a SELECT statement
            bool NewLineBeforeWhereClause { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if there should be a newline before the FROM
            //     clause in a SELECT statement
            bool NewLineBeforeFromClause { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if column definition fields (column name, data
            //     type, constraints) should be aligned into alignment columns.
            bool AlignColumnDefinitionFields { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if a semi colon should be included after each
            //     statement
            bool IncludeSemicolons { get; set; }
            //
            // Summary:
            //     Gets or sets the number of spaces to use when indenting text
            int IndentationSize { get; set; }
            //
            // Summary:
            //     Gets or sets the Sql engine type (All|Engine|Azure) to generate script for
            SqlEngineType SqlEngineType { get; set; }
            //
            // Summary:
            //     Gets or sets the Sql version to generate script for
            SqlVersion SqlVersion { get; set; }
            //
            // Summary:
            //     Gets or sets the keyword casing option to use during script generation
            KeywordCasing KeywordCasing { get; set; }
            //
            // Summary:
            //     Gets or sets a boolean indicating if a newline should be placed before an open
            //     parenthesis when writing a multi-line list in parenthesis
            bool NewLineBeforeOpenParenthesisInMultilineList { get; set; }

            //
            // Summary:
            //     Resets the options to their default value.
            void Reset();
        }
    }
}
