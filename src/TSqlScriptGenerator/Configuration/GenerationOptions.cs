// -----------------------------------------------------------------------
// <copyright file="Database.cs" company="sped.mobi">
//  Copyright © 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.TransactSql.Configuration
{


    [Serializable]
    [XmlRoot]
    public partial class GenerationOptions
    {
        public GenerationOptions()
        {
            AlignClauseBodies = false;
            AlignColumnDefinitionFields = false;
            AlignSetClauseItem = false;
            AsKeywordOnOwnLine = false;
            IncludeSemicolons = false;
            IndentationSize = 1;
            IndentSetClause = false;
            IndentViewBody = false;
            KeywordCasing = KeywordCasing.Uppercase;
            MultilineInsertSourcesList = false;
            MultilineInsertTargetsList = false;
            MultilineSelectElementsList = false;
            MultilineSetClauseItems = false;
            MultilineViewColumnsList = false;
            MultilineWherePredicatesList = false;
            NewLineBeforeCloseParenthesisInMultilineList = false;
            NewLineBeforeFromClause = false;
            NewLineBeforeGroupByClause = false;
            NewLineBeforeHavingClause = false;
            NewLineBeforeJoinClause = false;
            NewLineBeforeOffsetClause = false;
            NewLineBeforeOpenParenthesisInMultilineList = false;
            NewLineBeforeOrderByClause = false;
            NewLineBeforeOutputClause = false;
            NewLineBeforeWhereClause = false;
        }

        [XmlElement(Type = typeof(bool))]
        public bool AlignClauseBodies { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool AlignColumnDefinitionFields { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool AlignSetClauseItem { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool AsKeywordOnOwnLine { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool IncludeSemicolons { get; set; }

        [XmlElement(Type = typeof(int))]
        public int IndentationSize { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool IndentSetClause { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool IndentViewBody { get; set; }

        [XmlElement(Type = typeof(KeywordCasing))]
        public KeywordCasing KeywordCasing { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool MultilineInsertSourcesList { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool MultilineInsertTargetsList { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool MultilineSelectElementsList { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool MultilineSetClauseItems { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool MultilineViewColumnsList { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool MultilineWherePredicatesList { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool NewLineBeforeCloseParenthesisInMultilineList { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool NewLineBeforeFromClause { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool NewLineBeforeGroupByClause { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool NewLineBeforeHavingClause { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool NewLineBeforeJoinClause { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool NewLineBeforeOffsetClause { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool NewLineBeforeOpenParenthesisInMultilineList { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool NewLineBeforeOrderByClause { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool NewLineBeforeOutputClause { get; set; }

        [XmlElement(Type = typeof(bool))]
        public bool NewLineBeforeWhereClause { get; set; }

        [XmlElement(Type=typeof(SqlVersion))]
        public SqlVersion SqlVersion { get; set; }
    }


    //[Serializable]
    //[XmlRoot]
    //public partial class Tables
    //{
    //    [XmlElement("Table")]
    //    public List<Table> Table { get; set; }
    //}

    //
    //[Serializable]
    //
    //
    //[XmlRoot]
    //public partial class Schemas
    //{
    //    [XmlElement("Schema")]
    //    public List<Schema> Schema { get; set; }
    //}

    //
    //[Serializable]
    //
    //
    //[XmlRoot]
    //public partial class Indexes
    //{
    //    [XmlElement("Index")]
    //    public List<Index> Index { get; set; }
    //}

    //
    //[Serializable]
    //
    //
    //[XmlRoot]
    //public partial class Members
    //{
    //    [XmlElement("Member")]
    //    public List<Member> Member { get; set; }
    //}

    //
    //[Serializable]
    //
    //
    //[XmlRoot]
    //public partial class Columns
    //{
    //    [XmlElement(Type = typeof(Column))]
    //    public List<Column> Column { get; set; }
    //}

    //
    //[Serializable]
    //
    //
    //[XmlRoot]
    //public partial class ForeignKeys
    //{
    //    [XmlElement("ForeignKey")]
    //    public List<ForeignKey> ForeignKey { get; set; }
    //}

    //
    //[Serializable]
    //
    //
    //[XmlRoot]
    //public partial class ForeignKeyColumns
    //{
    //    [XmlElement("ForeignKeyColumn")]
    //    public List<ForeignKeyColumn> ForeignKeyColumn { get; set; }
    //}
}
