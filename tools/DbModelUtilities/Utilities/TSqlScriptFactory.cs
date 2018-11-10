// -----------------------------------------------------------------------
// <copyright file="TSqlScriptFactory.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DatabaseUtilities.Sql;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DatabaseUtilities.Utilities
{
    internal static class TSqlScriptFactory
    {
        public static CreateTableStatement CreateTable(string databaseName,
            string schemaName,
            string tableName,
            TableDefinition definition,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            return CreateTable(FullTableName(databaseName, schemaName, tableName, quoteType), definition);
        }

        public static CreateTableStatement CreateTable(Identifier databaseName,
            Identifier schemaName,
            Identifier tableName,
            TableDefinition definition)
        {
            return CreateTable(FullTableName(databaseName, schemaName, tableName), definition);
        }

        public static CreateTableStatement CreateTable(SchemaObjectName tableName, TableDefinition definition)
        {
            var fragment = new CreateTableStatement();
            fragment.SchemaObjectName = tableName;
            fragment.Definition = definition;
            return fragment;
        }

        public static SqlDataTypeReference DataType(SqlDataTypeOption option, IEnumerable<Literal> parameters)
        {
            var fragment = new SqlDataTypeReference();
            fragment.SqlDataTypeOption = option;
            fragment.Parameters.AddRange(parameters);
            return fragment;
        }

        public static SqlDataTypeReference DataType(SqlDataTypeOption option, bool max)
        {
            return max ? DataType(option, Max()) : DataType(option);
        }

        public static SqlDataTypeReference DataType(SqlDataTypeOption option, params Literal[] parameters)
        {
            return DataType(option, (IEnumerable<Literal>) parameters);
        }

        public static SqlDataTypeReference DataType(SqlDataTypeOption option, int size)
        {
            return DataType(option, Integer(size));
        }

        public static SqlDataTypeReference DataType(SqlDataTypeOption option, int size, int scale)
        {
            return DataType(option, Integer(size), Integer(scale));
        }

        public static IntegerLiteral Integer(int value)
        {
            return new IntegerLiteral
            {
                Value = value.ToString()
            };
        }

        public static MaxLiteral Max()
        {
            return new MaxLiteral
            {
                Value = "max"
            };
        }


        public static IdentityOptions IdentityOptions(ScalarExpression increment, ScalarExpression seed, bool forReplication = false)
        {
            var fragemnt = new IdentityOptions();
            fragemnt.IdentityIncrement = increment;
            fragemnt.IdentitySeed = seed;
            fragemnt.IsIdentityNotForReplication = forReplication;
            return fragemnt;
        }

        public static IdentityOptions IdentityOptions(int increment, int seed, bool forReplication = false)
        {
            return IdentityOptions(Scalar(increment), Scalar(seed), forReplication);
        }

        public static ScalarExpression Scalar(int expression)
        {
            return Scalar(expression.ToString());
        }

        public static ScalarExpression Scalar(string expression)
        {
            var fragment = new ScalarExpressionSnippet();
            fragment.Script = expression;
            return fragment;
        }

        
        public static ColumnDefinition IdentityColumn(string columnName,
            DataTypeReference dataType,
            IdentityOptions options,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            ColumnDefinition definition = Column(columnName, dataType, false, quoteType);
            definition.IdentityOptions = options;
            return definition;
        }

        public static ColumnDefinition Column(string columnName,
            string defaultValue,
            DataTypeReference dataType,
            bool nullable = false,
            bool isRowGuid = false,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            return Column(Identifier(columnName, quoteType), dataType, nullable, isRowGuid);
        }

        public static ColumnDefinition Column(string columnName,
            DataTypeReference dataType,
            bool nullable = false,
            bool isRowGuid = false,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            return Column(Identifier(columnName, quoteType), dataType, nullable, isRowGuid);
        }

        public static ColumnDefinition Column(string columnName,
            DataTypeReference dataType,
            bool nullable = false,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            return Column(Identifier(columnName, quoteType), dataType, nullable);
        }



        public static ColumnDefinition Column(string defaultValue, Identifier columnName, DataTypeReference dataType, bool nullable = false, bool isRowGuid = false)
        {
            var fragment = new ColumnDefinition();
            fragment.ColumnIdentifier = columnName;
            fragment.DataType = dataType;
            fragment.IsRowGuidCol = isRowGuid;
            fragment.Constraints.Add(NullableConstraint(nullable));
            return fragment;
        }

        public static ColumnDefinition Column(Identifier columnName, DataTypeReference dataType, bool nullable = false, bool isRowGuid = false)
        {
            var fragment = new ColumnDefinition();
            fragment.ColumnIdentifier = columnName;
            fragment.DataType = dataType;
            fragment.IsRowGuidCol = isRowGuid;
            fragment.Constraints.Add(NullableConstraint(nullable));
            return fragment;
        }

        public static DefaultLiteral Default(string value)
        {
            DefaultLiteral literal = new DefaultLiteral();
            literal.Value = value;
            return literal;
        }

        public static DefaultConstraintDefinition DefaultConstraint(Identifier column, DefaultLiteral expression)
        {
            return DefaultConstraint(column, (ScalarExpression) expression);
        }
        public static DefaultConstraintDefinition DefaultConstraint(Identifier column, ScalarExpression expression)
        {
            var fragment = new DefaultConstraintDefinition();
            fragment.Column = column;
            fragment.Expression = expression;
            return fragment;
        }
        public static NullableConstraintDefinition NullableConstraint(bool nullable = false)
        {
            var fragment = new NullableConstraintDefinition();
            fragment.Nullable = nullable;
            return fragment;
        }

        public static TableDefinition TableDefinition(IEnumerable<ColumnDefinition> columns,
            IEnumerable<IndexDefinition> indexes,
            IEnumerable<ConstraintDefinition> constraints)
        {
            var fragment = new TableDefinition();
            fragment.ColumnDefinitions.AddRange(columns);
            fragment.Indexes.AddRange(indexes);
            fragment.TableConstraints.AddRange(constraints);
            return fragment;
        }

        public static TableDefinition TableDefinition(IEnumerable<ColumnDefinition> columns, IEnumerable<IndexDefinition> indexes)
        {
            var fragment = new TableDefinition();
            fragment.ColumnDefinitions.AddRange(columns);
            fragment.Indexes.AddRange(indexes);
            return fragment;
        }

        public static TableDefinition TableDefinition(IEnumerable<ColumnDefinition> columns)
        {
            var fragment = new TableDefinition();
            fragment.ColumnDefinitions.AddRange(columns);
            return fragment;
        }

        public static TableDefinition TableDefinition(params ColumnDefinition[] columns) => TableDefinition(ColumnDefinitions(columns));

        public static IList<ColumnDefinition> ColumnDefinitions(params ColumnDefinition[] definitions) =>
            new List<ColumnDefinition>(definitions);

        public static IList<IndexDefinition> IndexDefinitions(params IndexDefinition[] definitions) =>
            new List<IndexDefinition>(definitions);

        public static IList<ConstraintDefinition> ConstraintDefinitions(params ConstraintDefinition[] definitions) =>
            new List<ConstraintDefinition>(definitions);

        public static SchemaObjectName PartTableName(
            string schemaName,
            string tableName)
        {
            return new SchemaObjectNameSnippet
            {
                Script = $"{schemaName}.{tableName}"
            };
        }

        public static SchemaObjectName FullTableName(string databaseName,
            string schemaName,
            string tableName,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            return FullTableName(Identifier(databaseName, quoteType), Identifier(schemaName, quoteType), Identifier(tableName, quoteType));
        }

        public static SchemaObjectName FullTableName(Identifier databaseName, Identifier schemaName, Identifier tableName)
        {
            var fragment = new SchemaObjectName();
            fragment.Identifiers.AddRange(databaseName, schemaName, tableName);
            return fragment;
        }

        public static DropDatabaseStatement DropDatabase(bool isIfExistss, IEnumerable<Identifier> databases)
        {
            var fragment = new DropDatabaseStatement
            {
                IsIfExists = isIfExistss
            };
            fragment.Databases.AddRange(databases);
            return fragment;
        }

        public static DropDatabaseStatement DropDatabase(bool isIfExists, params Identifier[] databases)
        {
            return DropDatabase(isIfExists, new List<Identifier>(databases));
        }

        public static DropDatabaseStatement DropDatabase(bool isIfExists, QuoteType quoteType, params string[] databases)
        {
            return DropDatabase(isIfExists, IdentifierList(quoteType, databases));
        }

        public static CreateDatabaseStatement CreateDatabase(Identifier databaseName)
        {
            CreateDatabaseStatement fragment = new CreateDatabaseStatement();
            fragment.DatabaseName = databaseName;
            return fragment;
        }

        public static CreateDatabaseStatement CreateDatabase(string databaseName, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return CreateDatabase(Identifier(databaseName, quoteType));
        }

        public static UseStatement Use(Identifier databaseName)
        {
            UseStatement fragment = new UseStatement();
            fragment.DatabaseName = databaseName;
            return fragment;
        }

        public static UseStatement Use(string databaseName, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return Use(Identifier(databaseName, quoteType));
        }

        public static IList<Identifier> IdentifierList(QuoteType quoteType, params string[] values)
        {
            if (values is null) throw new ArgumentNullException(nameof(values));
            List<Identifier> list = new List<Identifier>();
            foreach (string t in values)
            {
                list.Add(Identifier(t, quoteType));
            }

            return list;
        }

        public static Identifier BracketedIdentifier(string value)
        {
            return Identifier(value, QuoteType.SquareBracket);
        }

        public static Identifier QuotedIdentifier(string value)
        {
            return Identifier(value, QuoteType.DoubleQuote);
        }

        public static Identifier Identifier(string value, QuoteType quoteType = QuoteType.NotQuoted)
        {
            return new Identifier
            {
                Value = value,
                QuoteType = quoteType
            };
        }
    }

    internal static class TSqlExtensions
    {
        public static SqlDataTypeOption ToSqlDataTypeOption(this string source, out bool isMax)
        {
            isMax = Regex.IsMatch(source, "(MAX|max|Max|mAx|MaX)");
            return source.ToSqlDataTypeOption();
        }

        public static SqlDataTypeOption ToSqlDataTypeOption(this string source)
        {
            if (string.IsNullOrEmpty(source)) throw new ArgumentNullException(nameof(source));
            var match = Regex.Match(source, @"(?<identifier>[a-zA-Z_]*)(\((?<parameters>[0-9]+(, ?[0-9]+)?)\))?");
            var identifier = match.Groups["identifier"].Value;
            switch (identifier.ToLower())
            {
                case "bigint": return SqlDataTypeOption.BigInt;
                case "int": return SqlDataTypeOption.Int;
                case "smallint": return SqlDataTypeOption.SmallInt;
                case "tinyint": return SqlDataTypeOption.TinyInt;
                case "bit": return SqlDataTypeOption.Bit;
                case "decimal": return SqlDataTypeOption.Decimal;
                case "numeric": return SqlDataTypeOption.Numeric;
                case "money": return SqlDataTypeOption.Money;
                case "smallmoney": return SqlDataTypeOption.SmallMoney;
                case "float": return SqlDataTypeOption.Float;
                case "real": return SqlDataTypeOption.Real;
                case "datetime": return SqlDataTypeOption.DateTime;
                case "smalldatetime": return SqlDataTypeOption.SmallDateTime;
                case "char": return SqlDataTypeOption.Char;
                case "varchar": return SqlDataTypeOption.VarChar;
                case "text": return SqlDataTypeOption.Text;
                case "nchar": return SqlDataTypeOption.NChar;
                case "nvarchar": return SqlDataTypeOption.NVarChar;
                case "ntext": return SqlDataTypeOption.NText;
                case "binary": return SqlDataTypeOption.Binary;
                case "varbinary": return SqlDataTypeOption.VarBinary;
                case "image": return SqlDataTypeOption.Image;
                case "cursor": return SqlDataTypeOption.Cursor;
                case "sql_variant": return SqlDataTypeOption.Sql_Variant;
                case "table": return SqlDataTypeOption.Table;
                case "timestamp": return SqlDataTypeOption.Timestamp;
                case "uniqueidentifier": return SqlDataTypeOption.UniqueIdentifier;
                case "date": return SqlDataTypeOption.Date;
                case "time": return SqlDataTypeOption.Time;
                case "datetime2": return SqlDataTypeOption.DateTime2;
                case "datetimeoffset": return SqlDataTypeOption.DateTimeOffset;
                case "rowversion": return SqlDataTypeOption.Rowversion;
                default: return SqlDataTypeOption.None;
            }
        }

        //public static void AddRange<T>(this IList<T> source, params T[] items)
        //{
        //    if (items is null) throw new ArgumentNullException(nameof(items));
        //    foreach (T item in items)
        //    {
        //        source.Add(item);
        //    }
        //}

        //public static void AddRange<T>(this IList<T> source, IEnumerable<T> items)
        //{
        //    if (items is null) throw new ArgumentNullException(nameof(items));
        //    foreach (T item in items)
        //    {
        //        source.Add(item);
        //    }
        //}
    }
}
