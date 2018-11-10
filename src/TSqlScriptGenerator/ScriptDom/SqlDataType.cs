// -----------------------------------------------------------------------
// <copyright file="SqlDataType.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

namespace Microsoft.SqlServer.TransactSql.ScriptDom
{
    public struct SqlDataType : IEquatable<SqlDataType>
    {
        public static SqlDataType Parse(string value)
        {
            var match = Regex.Match(value, "(?<Name>[a-zA-Z_]*)(\\((?<Parameters>.*)\\))?");
            var match2 = Regex.Match(value, "\\((?<Parameters>.*)\\)");
            string name = match.Groups["Name"].Value.ToLower().Trim();
            string parameters = match.Groups["Parameters"].Value;

            SqlDataTypeOption option = name.ToSqlDataTypeOption();
            if (!string.IsNullOrEmpty(parameters))
            {
                string cleanParamters = parameters.Trim().ToLower().Replace(" ", string.Empty);
                if (cleanParamters == "max")
                {
                    return new SqlDataType(option, true);
                }

                if (cleanParamters.Contains(","))
                {
                    string[] split = cleanParamters.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                    int size = int.Parse(split[0]);
                    int scale = int.Parse(split[1]);
                    return new SqlDataType(option, size, scale);
                }
                else
                {
                    int size = int.Parse(cleanParamters);
                    return new SqlDataType(option, size);
                }
            }

            return new SqlDataType(option);
        }

        public SqlDataType(SqlDataTypeOption type, bool isMax = false) : this()
        {
            Type = type;
            IsMax = isMax;
            Size = null;
            Scale = null;
        }

        public SqlDataType(SqlDataTypeOption type, int size)
        {
            Type = type;
            IsMax = false;
            Size = size;
            Scale = null;
        }

        public SqlDataType(SqlDataTypeOption type, int size, int scale)
        {
            Type = type;
            IsMax = false;
            Size = size;
            Scale = scale;
        }

        public bool IsMax { get; }

        public int? Scale { get; }

        public int? Size { get; }

        public SqlDataTypeOption Type { get; }

        public bool Equals(SqlDataType other)
        {
            return Type == other.Type && IsMax == other.IsMax && Size == other.Size && Scale == other.Scale;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SqlDataType other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (int) Type;
                hashCode = (hashCode * 397) ^ IsMax.GetHashCode();
                if (Size.HasValue)
                {
                    hashCode = (hashCode * 397) ^ Size.Value;
                }

                if (Scale.HasValue)
                {
                    hashCode = (hashCode * 397) ^ Scale.Value;
                }

                return hashCode;
            }
        }

        public static bool operator ==(SqlDataType left, SqlDataType right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SqlDataType left, SqlDataType right)
        {
            return !left.Equals(right);
        }
    }
}