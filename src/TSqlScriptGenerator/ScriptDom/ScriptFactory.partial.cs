using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.SqlServer.TransactSql.ScriptDom
{
    internal static partial class ScriptFactory
    {
        public static void AddRange<T>(this IList<T> source, params T[] items)
        {
            if (items is null) return;
            foreach (T item in items)
            {
                source.Add(item);
            }
        }
        
        public static void AddRange<T>(this IList<T> source, IEnumerable<T> items)
        {
            if (items is null) return;
            foreach (T item in items)
            {
                source.Add(item);
            }
        }

                
        public static SqlDataTypeReference SqlDataType(SqlDataTypeOption option, IEnumerable<Literal> parameters)
        {
            var fragment = new SqlDataTypeReference();
            fragment.SqlDataTypeOption = option;
            fragment.Parameters.AddRange(parameters);
            return fragment;
        }
        
        public static SqlDataTypeReference SqlDataType(SqlDataTypeOption option, bool max)
        {
            return max ? SqlDataType(option, MaxLiteral("MAX")) : SqlDataType(option);
        }
        
        public static SqlDataTypeReference SqlDataType(SqlDataTypeOption option, params Literal[] parameters)
        {
            var fragment = new SqlDataTypeReference();
            fragment.SqlDataTypeOption = option;
            fragment.Parameters.AddRange(parameters);
            return fragment;
        }
        
        public static SqlDataTypeReference SqlDataType(SqlDataTypeOption option, int size)
        {
            return SqlDataType(option, IntegerLiteral(size.ToString()));
        }
        
        public static SqlDataTypeReference SqlDataType(SqlDataTypeOption option, int size, int scale)
        {
            return SqlDataType(option, IntegerLiteral(size.ToString()), IntegerLiteral(scale.ToString()));
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

        public static ColumnDefinition IdentityColumn(string columnName,
            DataTypeReference dataType,
            IdentityOptions options,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            ColumnDefinition definition = ColumnDefinition(columnName, dataType, false, quoteType);
            definition.IdentityOptions = options;
            return definition;
        }

        public static ColumnDefinition ColumnDefinition(string columnName,
            string defaultValue,
            string defaultName,
            DataTypeReference dataType,
            bool nullable = false,
            bool isRowGuid = false,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            var fragment = new ColumnDefinition();
            fragment.Constraints.Add(NullableConstraintDefinition(null, nullable));
            fragment.Constraints.Add(DefaultConstraintDefinition(
                null,
                Identifier(defaultName, quoteType),
                false,
                IdentifierLiteral(defaultValue)));
            fragment.ColumnIdentifier = Identifier(columnName, quoteType);
            fragment.IsRowGuidCol = isRowGuid;
            fragment.DataType = dataType;

            return fragment;
        }

        public static ColumnDefinition ColumnDefinition(string columnName,
            DataTypeReference dataType,
            bool nullable = false,
            bool isRowGuid = false,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            return ColumnDefinition(Identifier(columnName, quoteType), dataType, nullable, isRowGuid);
        }

        public static ColumnDefinition ColumnDefinition(string columnName,
            DataTypeReference dataType,
            bool nullable = false,
            QuoteType quoteType = QuoteType.NotQuoted)
        {
            return ColumnDefinition(Identifier(columnName, quoteType), dataType, nullable);
        }

        public static ColumnDefinition ColumnDefinition(Identifier columnName, DataTypeReference dataType, bool nullable = false, bool isRowGuid = false)
        {
            var fragment = new ColumnDefinition();
            fragment.ColumnIdentifier = columnName;
            fragment.IsRowGuidCol = isRowGuid;
            fragment.DataType = dataType;
            fragment.Constraints.Add(NullableConstraint(nullable));
            return fragment;
        }

        public static NamedTableReference NamedTableReference(SchemaObjectName namedTable)
        {
            var fragment = new NamedTableReference();
            fragment.SchemaObject = namedTable;
            return fragment;
        }

        public static NullableConstraintDefinition NullableConstraint(bool nullable)
        {
            return nullable ? NullableConstraint() : NotNullableConstraint();
        }

        public static NullableConstraintDefinition NullableConstraint()
        {
            var fragment = new NullableConstraintDefinition();
            fragment.Nullable = true;
            return fragment;
        }

        public static NullableConstraintDefinition NotNullableConstraint()
        {
            var fragment = new NullableConstraintDefinition();
            fragment.Nullable = false;
            return fragment;
        }

        public static IList<Identifier> List(params Identifier[] identifiers)
        {
            return new IdentifierList(identifiers);
        }

        public static IList<Identifier> List(QuoteType quoteType, params string[] identifiers)
        {
            return new IdentifierList(identifiers.Select(i=>Identifier(i, quoteType)));
        }

        public static MultiPartIdentifier MultiPartIdentifier(IEnumerable<Identifier> identifiers = null)
        {
            var retVal = new MultiPartIdentifier();
            if (identifiers != null)
                retVal.Identifiers.AddRange(identifiers);
            return retVal;
        }
        
        
        public static MultiPartIdentifier MultiPartIdentifier(params Identifier[] identifiers)
        {
            var retVal = new MultiPartIdentifier();
            if (identifiers != null)
                retVal.Identifiers.AddRange(identifiers);
            return retVal;
        }

        public static MultiPartIdentifier MultiPartIdentifier(QuoteType quoteType, params string[] identifiers)
        {
            var retVal = new MultiPartIdentifier();
            if (identifiers != null)
                retVal.Identifiers.AddRange(List(quoteType,identifiers));
            return retVal;
        }
    }


    public class IdentifierList : List<Identifier>
    {
        public IdentifierList(IEnumerable<Identifier> identifiers)
        {
            AddRange(identifiers);
        }
    }
}