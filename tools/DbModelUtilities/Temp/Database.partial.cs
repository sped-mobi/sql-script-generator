// -----------------------------------------------------------------------
// <copyright file="Database.partial.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DatabaseUtilities.Temp
{
    public partial class Schema
    {
        [XmlIgnore]
        public Database Parent { get; private set; }

        public void Initialize(Database parent)
        {
            Parent = parent;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Name} ({(DbDefault ? "Built-In" : "User-Defined")})";
        }
    }

    public partial class Table
    {
        public Table()
        {
            Columns = new List<Column>();
            Indexes = new List<Index>();
            ForeignKeys = new List<ForeignKey>();
        }

        [XmlIgnore]
        public Database Parent { get; private set; }

        public void Initialize(Database parent)
        {
            Parent = parent;
            foreach (Column column in Columns)
            {
                column.Initialize(this);
            }

            foreach (Index index in Indexes)
            {
                index.Initialize(this);
            }

            foreach (ForeignKey foreignKey in ForeignKeys)
            {
                foreignKey.Initialize(this);
            }
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Name} ({Columns.Count} columns)";
        }
    }

    public partial class Index
    {
        [XmlIgnore]
        public Table Parent { get; private set; }

        public void Initialize(Table parent)
        {
            Parent = parent;
            foreach (Member member in Members)
            {
                member.Initialize(this);
            }
        }
    }

    public partial class Member
    {
        [XmlIgnore]
        public Index Parent { get; private set; }

        public void Initialize(Index parent)
        {
            Parent = parent;
        }
    }

    public partial class Column
    {
        [XmlIgnore]
        public Table Parent { get; private set; }

        public void Initialize(Table parent)
        {
            Parent = parent;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Name} ({DataType})";
        }
    }

    public partial class ForeignKey
    {
        public ForeignKey()
        {
            ForeignKeyColumns = new List<ForeignKeyColumn>();
        }

        [XmlIgnore]
        public Table Parent { get; private set; }

        public void Initialize(Table parent)
        {
            Parent = parent;
            foreach (ForeignKeyColumn foreignKeyColumn in ForeignKeyColumns)
            {
                foreignKeyColumn.Initialize(this);
            }
        }
    }

    public partial class ForeignKeyColumn
    {
        [XmlIgnore]
        public ForeignKey Parent { get; private set; }

        public void Initialize(ForeignKey parent)
        {
            Parent = parent;
        }
    }

    public partial class Database
    {
        public Database()
        {
            Schemas = new List<Schema>();
            Tables = new List<Table>();
        }

        public void Initialize()
        {
            foreach (Schema schema in Schemas)
            {
                schema.Initialize(this);
            }

            foreach (Table table in Tables)
            {
                table.Initialize(this);
            }
        }

        public static Database DeserializeFromFile(string filePath)
        {
            XmlReader reader = XmlReader.Create(filePath);
            XmlSerializer serializer = new XmlSerializer(typeof(Database));
            var database = (Database) serializer.Deserialize(reader);
            database.Initialize();
            return database;
        }

        public static string SerializeObject(Database o)
        {
            MemoryStream stream = new MemoryStream();
            XmlWriter writer = XmlWriter.Create(stream);
            XmlSerializer serializer = new XmlSerializer(typeof(Database));
            serializer.Serialize(writer, o);
            return Encoding.ASCII.GetString(stream.ToArray());
        }
    }
}
