// -----------------------------------------------------------------------
// <copyright file="Database.cs" company="sped.mobi">
//  Copyright © 2020. All rights reserved.
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
