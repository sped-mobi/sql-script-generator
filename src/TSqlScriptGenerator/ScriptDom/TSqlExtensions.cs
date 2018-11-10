﻿// -----------------------------------------------------------------------
// <copyright file="TSqlExtensions.cs" company="sped.mobi">
//     Copyright © 2018 Brad R. Marshall. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

namespace Microsoft.SqlServer.TransactSql.ScriptDom
{
    internal static class TSqlExtensions
    {
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
    }
}
