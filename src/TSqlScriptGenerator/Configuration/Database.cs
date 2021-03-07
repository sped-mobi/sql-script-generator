// -----------------------------------------------------------------------
// <copyright file="Database.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
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

        [XmlElement(Type = typeof(SqlEngineType))]
        public SqlEngineType SqlEngineType { get; set; }

        [XmlElement(Type=typeof(SqlVersion))]
        public SqlVersion SqlVersion { get; set; }
    }

    [Serializable]
    [XmlRoot]
    public partial class Database
    {
        [XmlElement]
        public GenerationOptions Options { get; set; }

        [XmlAttribute]
        public string DatabaseName { get; set; }

        [XmlArrayItem(IsNullable = false)]
        public List<Schema> Schemas { get; set; }

        [XmlArrayItem(IsNullable = false)]
        public List<Table> Tables { get; set; }

        [XmlAttribute]
        public string Version { get; set; }
    }

    [Serializable]
    [XmlRoot]
    public partial class Schema
    {
        [XmlAttribute]
        public bool DbDefault { get; set; }

        [XmlAttribute]
        public string Name { get; set; }
    }



    [Serializable]
    [XmlRoot]
    public partial class Table
    {
        [XmlArrayItem(IsNullable = false)]
        public List<Column> Columns { get; set; }

        [XmlArrayItem(IsNullable = false)]
        public List<ForeignKey> ForeignKeys { get; set; }

        [XmlArrayItem(IsNullable = false)]
        public List<Index> Indexes { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Schema { get; set; }
    }

    [Serializable]
    [XmlRoot]
    public partial class Column
    {
        [XmlAttribute]
        public bool AllowNulls { get; set; }

        [XmlAttribute]
        public string DataType { get; set; }

        [XmlAttribute]
        public string DefaultName { get; set; }

        [XmlAttribute]
        public string DefaultValue { get; set; }

        [XmlAttribute]
        public int IdentityIncrement { get; set; }

        [XmlAttribute]
        public int IdentitySeed { get; set; }

        [XmlAttribute]
        public bool IsIdentity { get; set; }

        [XmlAttribute]
        public bool RowGuid { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int Position { get; set; }
    }

    [Serializable]
    [XmlRoot]
    public partial class Index
    {
        [XmlAttribute]
        public bool IsClustered { get; set; }

        [XmlAttribute]
        public bool IsPrimary { get; set; }

        [XmlAttribute]
        public bool IsUnique { get; set; }

        [XmlArrayItem(IsNullable = false)]
        public List<Member> Members { get; set; }

        [XmlAttribute]
        public string Name { get; set; }
    }

    [Serializable]
    [XmlRoot]
    public partial class Member
    {
        [XmlAttribute]
        public string Column { get; set; }

        [XmlAttribute]
        public bool IsIncluded { get; set; }

        [XmlAttribute]
        public int Position { get; set; }
    }

    [Serializable]
    [XmlRoot]
    public partial class ForeignKey
    {
        [XmlArrayItem(IsNullable = false)]
        public List<ForeignKeyColumn> ForeignKeyColumns { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string PkTable { get; set; }

        [XmlAttribute]
        public string PkTableSchema { get; set; }
    }

    [Serializable]
    [XmlRoot]
    public partial class ForeignKeyColumn
    {
        [XmlAttribute]
        public string FkColumn { get; set; }

        [XmlAttribute]
        public string PkColumn { get; set; }

        [XmlAttribute]
        public int Position { get; set; }
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
