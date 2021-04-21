
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.SqlServer.TransactSql.Configuration
{
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
}
