// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.SqlServer.TransactSql.ScriptDom;

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.Csv.Utilities;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace System.Data.Csv
{
    /// <summary>
    /// Defines the <see cref="Program" />
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The Dispose
        /// </summary>
        public static void Dispose()
        {
            _writer.Dispose();
        }

        /// <summary>
        /// The IsNullable
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsNullable(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// The Main
        /// </summary>
        public static void Main()
        {
            ModeConCols();
            WriteFactory();
            Console.Write(_sb);
            File.WriteAllText(@"C:\stage\src\sandbox\TAC\tools\TSqlScriptGenerator\ScriptDom\ScriptFactory.cs", _sb.ToString());
            PressAnyKey();
        }

        /// <summary>
        /// Defines the _sb
        /// </summary>
        private static readonly StringBuilder _sb = new StringBuilder();

        /// <summary>
        /// Defines the _sw
        /// </summary>
        private static readonly StringWriter _sw = new StringWriter(_sb);

        /// <summary>
        /// Defines the _writer
        /// </summary>
        private static readonly IndentedTextWriter _writer = new IndentedTextWriter(_sw);

        /// <summary>
        /// The GetTypes
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Type}"/></returns>
        private static IEnumerable<Type> GetTypes()
        {
            return new[]
            {
                typeof(FunctionCall),
                typeof(UseStatement),
                typeof(ExecuteStatement),
                typeof(DropDatabaseStatement),
                typeof(DropTableStatement),
                typeof(DropProcedureStatement),
                typeof(DropFunctionStatement),
                typeof(DropIndexStatement),
                typeof(DropIndexClause),
                typeof(AlterDatabaseSetStatement),
                typeof(AlterTableAddTableElementStatement),
                typeof(CreateDatabaseStatement),
                typeof(CreateTableStatement),
                typeof(CreateProcedureStatement),
                typeof(CreateFunctionStatement),
                typeof(CreateTriggerStatement),
                typeof(CreateViewStatement),
                typeof(CreateSchemaStatement),
                typeof(CreateIndexStatement),
                typeof(ColumnDefinition),
                typeof(TableDefinition),
                typeof(IndexDefinition),
                typeof(NullableConstraintDefinition),
                typeof(CheckConstraintDefinition),
                typeof(DefaultConstraintDefinition),
                typeof(ForeignKeyConstraintDefinition),
                typeof(UniqueConstraintDefinition),
                // typeof(SqlDataTypeReference),
                typeof(UserDataTypeReference),
                typeof(InsertStatement),
                typeof(UpdateStatement),
                typeof(DeleteStatement),
                typeof(SelectStatement),
                typeof(InsertSpecification),
                typeof(UpdateSpecification),
                typeof(DeleteSpecification),
                typeof(IntegerLiteral),
                typeof(StringLiteral),
                typeof(NullLiteral),
                typeof(MaxLiteral),
                typeof(DefaultLiteral),
                typeof(IdentifierLiteral),
                typeof(PrintStatement),
                typeof(ParenthesisExpression),
                typeof(IdentityOptions),

                typeof(ColumnWithSortOrder),
                typeof(ColumnReferenceExpression),
                //typeof(MultiPartIdentifier)

                typeof(ProcedureReference),
                typeof(ProcedureReferenceName),
                typeof(ProcedureParameter),
                typeof(StatementList),
                typeof(MethodSpecifier),
                typeof(ProcedureOption),
                //typeof(ProcedureStatementBody),

                typeof(BeginEndBlockStatement),
                typeof(PredicateSetStatement),
                typeof(ReturnStatement),
                typeof(TopRowFilter),
                typeof(OutputIntoClause),
                typeof(OutputClause),

                typeof(NamedTableReference),
                typeof(ExecuteInsertSource),
                typeof(SelectInsertSource),
                typeof(ValuesInsertSource),
                typeof(RowValue),

                

                //typeof(SchemaObjectName),
                typeof(Identifier)
            };
        }

        /// <summary>
        /// The GetMutableProperties
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <returns>The <see cref="IList{PropertyInfo}"/></returns>
        private static PropertyInfo[] GetMutableProperties(Type type)
        {
            List<PropertyInfo> list = new List<PropertyInfo>();
            var checkList = typeof(TSqlFragment)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(pp => pp.Name).ToList();
            foreach (var p in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!checkList.Contains(p.Name))
                {
                    if (p.SetMethod != null && p.SetMethod.IsPublic)
                    {
                        list.Add(p);
                    }
                    else
                    {
                        if (p.PropertyType.Name.StartsWith("IList", StringComparison.CurrentCulture))
                        {
                            list.Add(p);
                        }
                    }
                }
            }

            list.Sort(Comparers.PropertyInfo);
            return list.ToArray();
        }

        /// <summary>
        /// The WriteFactory
        /// </summary>
        private static void WriteFactory()
        {
            WriteLine("using System;");
            WriteLine("using System.Collections;");
            WriteLine("using System.Collections.Generic;");
            WriteLine();
            WriteLine("namespace Microsoft.SqlServer.TransactSql.ScriptDom");
            OpenBlock();
            WriteLine("internal static partial class ScriptFactory");
            OpenBlock();
            foreach (var type in GetTypes())
            {
                var properties = GetMutableProperties(type);
                WriteFullFactoryMethod(type, properties.ToArray());

                if (properties.PropertyTypeNamesContains(nameof(Identifier)))
                {
                    WriteMinimalFactoryMethod(type, properties.ToArray());
                }

                if (properties.LastPropertyIsListType())
                {
                    WriteParamsFactoryMethod(type, properties.ToArray());
                }


                if (!typeof(Literal).IsAssignableFrom(type) 
                    && !typeof(ScalarExpression).IsAssignableFrom(type))
                {
                    if (properties.PropertyNamesContains("Collation", "DatabaseSnapshot"))
                    {
                        WriteWithoutCollationAndGeneratedAlwaysFactoryMethod(type, properties, RemoveCollationAndGeneratedAlways, WriteFullFactoryMethod);

                        if (properties.LastPropertyIsListType())
                        {
                            WriteWithoutCollationAndGeneratedAlwaysFactoryMethod(type, properties, RemoveCollationAndGeneratedAlways, WriteParamsFactoryMethod);
                        }

                        if (properties.PropertyTypeNamesContains(nameof(Identifier)))
                        {
                            WriteWithoutCollationAndGeneratedAlwaysFactoryMethod(type, properties, RemoveCollationAndGeneratedAlways, WriteMinimalFactoryMethod);
                        }

                    }
                }
                else
                {
                    if (properties.PropertyNamesContains("Collation", "DatabaseSnapshot"))
                    {
                        WriteWithoutCollationAndGeneratedAlwaysFactoryMethod(type, properties, RemoveCollationAndGeneratedAlways, WriteFullFactoryMethod);

                        if (properties.LastPropertyIsListType())
                        {
                            WriteWithoutCollationAndGeneratedAlwaysFactoryMethod(type, properties, RemoveCollationAndGeneratedAlways, WriteParamsFactoryMethod);
                        }
                    }
                }

            }

            CloseBlock();
            CloseBlock();
        }

        /// <summary>
        /// The WriteWithoutCollationAndGeneratedAlwaysFactoryMethod
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <param name="properties">The properties<see cref="PropertyInfo[]"/></param>
        /// <param name="removeProperties">The removeProperties<see cref="Func{PropertyInfo[], PropertyInfo[]}"/></param>
        /// <param name="writeFactoryMethodAction">The writeFactoryMethodAction<see cref="Action{Type, PropertyInfo[]}"/></param>
        private static void WriteWithoutCollationAndGeneratedAlwaysFactoryMethod(
            Type type,
            PropertyInfo[] properties,
            Func<PropertyInfo[], PropertyInfo[]> removeProperties,
            Action<Type, PropertyInfo[]> writeFactoryMethodAction)
        {
            writeFactoryMethodAction(type, removeProperties(properties));
        }

        /// <summary>
        /// The RemoveCollationAndGeneratedAlways
        /// </summary>
        /// <param name="properties">The properties<see cref="PropertyInfo[]"/></param>
        /// <returns>The <see cref="PropertyInfo[]"/></returns>
        private static PropertyInfo[] RemoveCollationAndGeneratedAlways(PropertyInfo[] properties)
        {
            var properties2 = properties.RemovePropertyWithName("Collation");
            properties2 = properties2.RemovePropertyWithName("GeneratedAlways");
            properties2 = properties2.RemovePropertyWithName("DatabaseSnapshot");
            return properties2.ToArray();
        }

        /// <summary>
        /// The RemovePropertyWithName
        /// </summary>
        /// <param name="source">The source<see cref="IEnumerable{PropertyInfo}"/></param>
        /// <param name="propertyName">The propertyName<see cref="string"/></param>
        /// <returns>The <see cref="IEnumerable{PropertyInfo}"/></returns>
        private static IEnumerable<PropertyInfo> RemovePropertyWithName(this IEnumerable<PropertyInfo> source, string propertyName)
        {
            List<PropertyInfo> list = new List<PropertyInfo>(source);

            var propertyInfo = list.FirstOrDefault(p => p.Name == propertyName);

            if (propertyInfo != null)
            {
                if (!list.Remove(propertyInfo))
                {
                    throw new InvalidOperationException();
                }
            }

            return list;
        }

        /// <summary>
        /// The LastPropertyIsListType
        /// </summary>
        /// <param name="source">The source<see cref="IEnumerable{PropertyInfo}"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool LastPropertyIsListType(this IEnumerable<PropertyInfo> source)
        {
            try
            {
                return new List<PropertyInfo>(source).Last().PropertyType.TypeNameStartsWith("IList");
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        /// <summary>
        /// The PropertyTypeNamesContains
        /// </summary>
        /// <param name="source">The source<see cref="IEnumerable{PropertyInfo}"/></param>
        /// <param name="typeName">The typeName<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool PropertyTypeNamesContains(this IEnumerable<PropertyInfo> source, string typeName)
        {
            return source.Select(p => p.PropertyType.Name).Contains(typeName);
        }

        /// <summary>
        /// The WriteParamsFactoryMethod
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <param name="properties">The properties<see cref="PropertyInfo[]"/></param>
        private static void WriteParamsFactoryMethod(Type type, PropertyInfo[] properties)
        {
            string typeName = type.Name.Replace("Statement", string.Empty);
            WriteLine();
            Write($"public static {type.Name} {typeName}(");
            WriteParamsFactoryMethodParameterList(properties);
            WriteLine(")");
            OpenBlock();
            WriteParamsFactoryMethodBody(type, properties);
            CloseBlock();
        }

        /// <summary>
        /// The WriteFullFactoryMethod
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <param name="properties">The properties<see cref="PropertyInfo[]"/></param>
        private static void WriteFullFactoryMethod(Type type, PropertyInfo[] properties)
        {
            string typeName = type.Name.Replace("Statement", string.Empty);
            WriteLine();
            Write($"public static {type.Name} {typeName}(");
            WriteFullFactoryMethodParameterList(properties);
            WriteLine(")");
            OpenBlock();
            WriteFullFactoryMethodBody(type, properties);
            CloseBlock();
        }

        /// <summary>
        /// The WriteFullFactoryMethodBody
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <param name="properties">The properties<see cref="PropertyInfo[]"/></param>
        private static void WriteFullFactoryMethodBody(Type type, PropertyInfo[] properties)
        {
            WriteLine($"var retVal = new {type.Name}();");
            foreach (var property in properties)
            {
                string parameterName = CreateParameterName(property);
                if (property.IsList())
                {
                    WriteLine($"if ({parameterName} != null)");
                    PushIndent();
                    WriteLine($"retVal.{property.Name}.AddRange({parameterName});");
                    PopIndent();
                }
                else if (property.IsNotPrimativeOrEnum())
                {
                    WriteLine($"if ({parameterName} != null)");
                    PushIndent();
                    WriteLine($"retVal.{property.Name} = {parameterName};");
                    PopIndent();
                }
                else
                {
                    WriteLine($"retVal.{property.Name} = {parameterName};");
                }
            }

            WriteLine("return retVal;");
        }

        /// <summary>
        /// The IsNotPrimativeOrEnum
        /// </summary>
        /// <param name="property">The property<see cref="PropertyInfo"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool IsNotPrimativeOrEnum(this PropertyInfo property)
        {
            return !property.PropertyType.IsPrimitive && !property.PropertyType.IsEnum;
        }

        /// <summary>
        /// The IsList
        /// </summary>
        /// <param name="property">The property<see cref="PropertyInfo"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool IsList(this PropertyInfo property)
        {
            return property.PropertyType.TypeNameStartsWith("IList");
        }

        /// <summary>
        /// The WriteParamsFactoryMethodBody
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <param name="properties">The properties<see cref="PropertyInfo[]"/></param>
        private static void WriteParamsFactoryMethodBody(Type type, PropertyInfo[] properties)
        {
            int last = properties.Length - 1;
            WriteLine($"var retVal = new {type.Name}();");
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                string parameterName = CreateParameterName(property);
                if (i == last)
                {
                    if (parameterName.ToCharArray().Last() != 's')
                    {
                        parameterName += 's';
                    }
                }

                if (property.PropertyType.TypeNameStartsWith("IList"))
                {
                    WriteLine($"if ({parameterName} != null)");
                    PushIndent();
                    WriteLine($"retVal.{property.Name}.AddRange({parameterName});");
                    PopIndent();
                }
                else if (!property.PropertyType.IsPrimitive && !property.PropertyType.IsEnum)
                {
                    WriteLine($"if ({parameterName} != null)");
                    PushIndent();
                    WriteLine($"retVal.{property.Name} = {parameterName};");
                    PopIndent();
                }
                else
                {
                    WriteLine($"retVal.{property.Name} = {parameterName};");
                }
            }

            WriteLine("return retVal;");
        }

        /// <summary>
        /// The WriteFullFactoryMethodParameterList
        /// </summary>
        /// <param name="properties">The properties<see cref="PropertyInfo[]"/></param>
        private static void WriteFullFactoryMethodParameterList(PropertyInfo[] properties)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                string parameterName = CreateParameterName(property);
                Write(Format(property.PropertyType));
                Write(" ");
                Write(parameterName);
                if (property.PropertyType.AllowDefault())
                {
                    Write($" = {GetDefaultValueString(property.PropertyType)}");
                }

                if (i != properties.Length - 1)
                {
                    Write(", ");
                }
            }
        }

        /// <summary>
        /// The AllowDefault
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool AllowDefault(this Type type)
        {
            return typeof(TSqlFragment).IsAssignableFrom(type) && type != typeof(Identifier)
                   || type.IsEnum || type.IsNullable() || type.TypeNameStartsWith("IList");
        }

        /// <summary>
        /// The WriteParamsFactoryMethodParameterList
        /// </summary>
        /// <param name="properties">The properties<see cref="PropertyInfo[]"/></param>
        private static void WriteParamsFactoryMethodParameterList(PropertyInfo[] properties)
        {
            int last = properties.Length - 1;
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                string parameterName = CreateParameterName(property);
                if (i == last)
                {
                    if (parameterName.ToCharArray().Last() != 's')
                    {
                        parameterName += 's';
                    }

                    string argument = property.PropertyType.GetGenericArguments()[0].Name;
                    Write($"params {argument}[] {parameterName}");
                }
                else
                {
                    Write(Format(property.PropertyType));
                    Write(" ");
                    Write(parameterName);
                    if (property.PropertyType.AllowDefault())
                    {
                        Write($" = {GetDefaultValueString(property.PropertyType)}");
                    }

                    if (i != properties.Length - 1)
                    {
                        Write(", ");
                    }
                }
            }
        }

        /// <summary>
        /// The WriteExtensionMethods
        /// </summary>
        private static void WriteExtensionMethods()
        {
            WriteLine();
            WriteLine("public static MultiPartIdentifier MultiPartIdentifier(IEnumerable<Identifier> identifiers = null)");
            OpenBlock();
            WriteLine("var retVal = new MultiPartIdentifier();");
            WriteLine("if (identifiers != null)");
            WriteLine("retVal.Identifiers.AddRange(identifiers);");
            WriteLine("return retVal;");
            CloseBlock();
            WriteLine(@"");
            WriteLine("");
            WriteLine("public static MultiPartIdentifier MultiPartIdentifier(params Identifier[] identifiers)");
            OpenBlock();
            WriteLine("var retVal = new MultiPartIdentifier();");
            WriteLine("if (identifiers != null)");
            WriteLine("retVal.Identifiers.AddRange(identifiers);");
            WriteLine("return retVal;");
            CloseBlock();
            WriteLine();
            WriteLine("public static SqlDataTypeReference SqlDataType(SqlDataTypeOption option, IEnumerable<Literal> parameters)");
            OpenBlock();
            WriteLine("var fragment = new SqlDataTypeReference();");
            WriteLine("fragment.SqlDataTypeOption = option;");
            WriteLine("fragment.Parameters.AddRange(parameters);");
            WriteLine("return fragment;");
            CloseBlock();
            WriteLine();
            WriteLine("public static SqlDataTypeReference SqlDataType(SqlDataTypeOption option, bool max)");
            OpenBlock();
            WriteLine(@"return max ? SqlDataType(option, MaxLiteral(""MAX"")) : SqlDataType(option);");
            CloseBlock();
            WriteLine();
            WriteLine("public static SqlDataTypeReference SqlDataType(SqlDataTypeOption option, params Literal[] parameters)");
            OpenBlock();
            WriteLine("var fragment = new SqlDataTypeReference();");
            WriteLine("fragment.SqlDataTypeOption = option;");
            WriteLine("fragment.Parameters.AddRange(parameters);");
            WriteLine("return fragment;");
            CloseBlock();
            WriteLine("");
            WriteLine("public static SqlDataTypeReference SqlDataType(SqlDataTypeOption option, int size)");
            OpenBlock();
            WriteLine("return SqlDataType(option, IntegerLiteral(size.ToString()));");
            CloseBlock();
            WriteLine("");
            WriteLine("public static SqlDataTypeReference SqlDataType(SqlDataTypeOption option, int size, int scale)");
            OpenBlock();
            WriteLine("return SqlDataType(option, IntegerLiteral(size.ToString()), IntegerLiteral(scale.ToString()));");
            CloseBlock();
            WriteLine();
            WriteLine("public static void AddRange<T>(this IList<T> source, params T[] items)");
            OpenBlock();
            WriteLine("if (items is null) return;");
            WriteLine("foreach (T item in items)");
            OpenBlock();
            WriteLine("source.Add(item);");
            CloseBlock();
            CloseBlock();
            WriteLine("");
            WriteLine("public static void AddRange<T>(this IList<T> source, IEnumerable<T> items)");
            OpenBlock();
            WriteLine("if (items is null) return;");
            WriteLine("foreach (T item in items)");
            OpenBlock();
            WriteLine("source.Add(item);");
            CloseBlock();
            CloseBlock();
            WriteLine($"public static SchemaObjectName FullTableName(string databaseName,");
            PushIndent();
            WriteLine($"string schemaName,");
            WriteLine($"string tableName,");
            WriteLine($"QuoteType quoteType = QuoteType.NotQuoted)");
            PopIndent();
            OpenBlock();
            WriteLine($"return FullTableName(Identifier(databaseName, quoteType), Identifier(schemaName, quoteType), Identifier(tableName, quoteType));");
            CloseBlock();
            WriteLine();
            WriteLine($"public static SchemaObjectName FullTableName(Identifier databaseName, Identifier schemaName, Identifier tableName)");
            OpenBlock();
            WriteLine($"var fragment = new SchemaObjectName();");
            WriteLine($"fragment.Identifiers.AddRange(databaseName, schemaName, tableName);");
            WriteLine($"return fragment;");
            CloseBlock();

        }

        /// <summary>
        /// The WriteMinimalFactoryMethod
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <param name="properties">The properties<see cref="PropertyInfo[]"/></param>
        private static void WriteMinimalFactoryMethod(Type type, PropertyInfo[] properties)
        {
            string typeName = type.Name.Replace("Statement", string.Empty);
            WriteLine();
            Write($"public static {type.Name} {typeName}(");
            WriteMinimalFactoryMethodParameterList(properties);
            WriteLine(")");
            OpenBlock();
            WriteMinimalFactoryMethodBody(type, properties);
            CloseBlock();
        }

        /// <summary>
        /// The WriteMinimalFactoryMethodBody
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <param name="properties">The properties<see cref="PropertyInfo"/></param>
        private static void WriteMinimalFactoryMethodBody(Type type, PropertyInfo[] properties)
        {
            WriteLine($"var retVal = new {type.Name}();");
            foreach (var property in properties)
            {
                string parameterName = CreateParameterName(property);
                if (property.IsIdentifier())
                {
                    WriteLine($"retVal.{property.Name} = Identifier({parameterName}, quoteType);");
                }
                else if (property.PropertyType.TypeNameStartsWith("IList"))
                {
                    WriteLine($"retVal.{property.Name}.AddRange({parameterName});");
                }
                else
                {
                    WriteLine($"retVal.{property.Name} = {parameterName};");
                }
            }


            WriteLine("return retVal;");
        }

        /// <summary>
        /// The TypeNameStartsWith
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <param name="startsWith">The startsWith<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool TypeNameStartsWith(this Type type, string startsWith)
        {
            return type.Name.StartsWith(startsWith, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// The WriteMinimalFactoryMethodParameterList
        /// </summary>
        /// <param name="properties">The properties<see cref="PropertyInfo[]"/></param>
        private static void WriteMinimalFactoryMethodParameterList(PropertyInfo[] properties)
        {
            bool shouldAddParameter = false;
            for (int i = 0; i < properties.Length; i++)
            {
                PropertyInfo property = properties[i];
                string parameterName = CreateParameterName(property);
                if (property.IsIdentifier())
                {
                    shouldAddParameter = !properties.PropertyTypeNamesContains("QuoteType") && !typeof(Literal).IsAssignableFrom(property.PropertyType);


                    Write($"string {parameterName}");
                }
                else
                {
                    Write(Format(property.PropertyType));
                    Write(" ");
                    Write(parameterName);
                    if (property.PropertyType.AllowDefault())
                    {
                        Write($" = {GetDefaultValueString(property.PropertyType)}");
                    }
                }

                if (shouldAddParameter)
                {
                    Write(", ");
                }
                else
                {
                    if (i != properties.Length - 1)
                    {
                        Write(", ");
                    }
                }
            }

            if (shouldAddParameter)
            {
                Write("QuoteType quoteType = QuoteType.NotQuoted");
            }
        }

        /// <summary>
        /// The PropertiesContains
        /// </summary>
        /// <param name="properties">The properties<see cref="IEnumerable{PropertyInfo}"/></param>
        /// <param name="propertyNames"></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool PropertyNamesContains(this IEnumerable<PropertyInfo> properties, params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                if (properties.Select(p => p.Name).Contains(propertyName))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The IsBool
        /// </summary>
        /// <param name="property">The property<see cref="PropertyInfo"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool IsBool(this PropertyInfo property)
        {
            return property.PropertyType == typeof(bool);
        }

        /// <summary>
        /// The IsIdentifier
        /// </summary>
        /// <param name="property">The property<see cref="PropertyInfo"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool IsIdentifier(this PropertyInfo property)
        {
            return property.PropertyType == typeof(Identifier);
        }

        /// <summary>
        /// The CreateParameterName
        /// </summary>
        /// <param name="property">The property<see cref="PropertyInfo"/></param>
        /// <returns>The <see cref="string"/></returns>
        private static string CreateParameterName(PropertyInfo property)
        {
            var name = CodeIdentifier.MakeCamel(property.Name);
            if (IsKeyword(name))
            {
                return '@' + name;
            }

            return name;
        }

        /// <summary>
        /// The IsKeyword
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool IsKeyword(string name)
        {
            switch (name)
            {
                case "bool":
                case "byte":
                case "sbyte":
                case "short":
                case "ushort":
                case "int":
                case "uint":
                case "long":
                case "ulong":
                case "double":
                case "float":
                case "decimal":
                case "string":
                case "char":
                case "object":
                case "typeof":
                case "sizeof":
                case "null":
                case "true":
                case "false":
                case "if":
                case "else":
                case "while":
                case "for":
                case "foreach":
                case "do":
                case "switch":
                case "case":
                case "default":
                case "lock":
                case "try":
                case "throw":
                case "catch":
                case "finally":
                case "goto":
                case "break":
                case "continue":
                case "return":
                case "public":
                case "private":
                case "internal":
                case "protected":
                case "static":
                case "readonly":
                case "sealed":
                case "const":
                case "new":
                case "override":
                case "abstract":
                case "virtual":
                case "partial":
                case "ref":
                case "out":
                case "in":
                case "where":
                case "params":
                case "this":
                case "base":
                case "namespace":
                case "using":
                case "class":
                case "struct":
                case "interface":
                case "delegate":
                case "checked":
                case "get":
                case "set":
                case "add":
                case "remove":
                case "operator":
                case "implicit":
                case "explicit":
                case "fixed":
                case "extern":
                case "event":
                case "enum":
                case "unsafe":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// The GetDefaultValueString
        /// </summary>
        /// <param name="primativeType">The primativeType<see cref="Type"/></param>
        /// <returns>The <see cref="string"/></returns>
        private static string GetDefaultValueString(Type primativeType)
        {
            if (primativeType.IsEnum)
            {
                var names = Enum.GetNames(primativeType);
                var values = new List<object>(Enum.GetValues(primativeType).ToList());
                values.Sort();
                return $"{primativeType.Name}.{values[0]}";
            }

            if (primativeType == typeof(bool))
            {
                return "false";
            }

            if (primativeType == typeof(byte) ||
                primativeType == typeof(int) ||
                primativeType == typeof(sbyte) ||
                primativeType == typeof(short) ||
                primativeType == typeof(uint) ||
                primativeType == typeof(ulong) ||
                primativeType == typeof(ushort)
            )
            {
                return "0";
            }

            if (primativeType == typeof(long))
            {
                return "0L";
            }

            if (primativeType == typeof(float))
            {
                return "0.0F";
            }

            if (primativeType == typeof(double))
            {
                return "0.0D";
            }

            if (primativeType == typeof(decimal))
            {
                return "0M";
            }

            if (primativeType == typeof(char))
            {
                return "\\0";
            }

            return "null";
        }

        /// <summary>
        /// The Format
        /// </summary>
        /// <param name="type">The type<see cref="Type"/></param>
        /// <returns>The <see cref="string"/></returns>
        private static string Format(Type type)
        {
            if (type.Name.StartsWith("IList", StringComparison.CurrentCulture))
            {
                string argument = type.GetGenericArguments()[0].Name;
                return $"IEnumerable<{argument}>";
            }

            if (type.IsNullable())
            {
                var t = Nullable.GetUnderlyingType(type);
                return $"{Format(t)}?";
            }

            string typeName = type.Namespace + '.' + type.Name;
            switch (typeName)
            {
                // Built-In Types
                case "System.Boolean": return "bool";
                case "System.Byte": return "byte";
                case "System.SByte": return "sbyte";
                case "System.Char": return "char";
                case "System.Decimal": return "decimal";
                case "System.Double": return "double";
                case "System.Single": return "float";
                case "System.Int32": return "int";
                case "System.UInt32": return "uint";
                case "System.Int64": return "long";
                case "System.UInt64": return "ulong";
                case "System.Object": return "object";
                case "System.Int16": return "short";
                case "System.UInt16": return "ushort";
                case "System.String": return "string";
                default: return type.Name;
            }
        }

        /// <summary>
        /// The ToList
        /// </summary>
        /// <param name="array">The array<see cref="Array"/></param>
        /// <returns>The <see cref="IList{object}"/></returns>
        private static IList<object> ToList(this Array array)
        {
            List<object> list = new List<object>();
            for (int i = 0; i < array.Length; i++)
            {
                object item = array.GetValue(i);
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// The PushIndent
        /// </summary>
        private static void PushIndent()
        {
            _writer.Indent++;
        }

        /// <summary>
        /// The PopIndent
        /// </summary>
        private static void PopIndent()
        {
            _writer.Indent--;
        }

        /// <summary>
        /// The OpenBlock
        /// </summary>
        private static void OpenBlock()
        {
            WriteLine("{");
            PushIndent();
        }

        /// <summary>
        /// The CloseBlock
        /// </summary>
        private static void CloseBlock()
        {
            PopIndent();
            WriteLine("}");
        }

        /// <summary>
        /// The Write
        /// </summary>
        /// <param name="value">The value<see cref="string"/></param>
        private static void Write(string value)
        {
            _writer.Write(value);
        }

        /// <summary>
        /// The WriteLine
        /// </summary>
        /// <param name="value">The value<see cref="string"/></param>
        private static void WriteLine(string value)
        {
            _writer.WriteLine(value);
        }

        /// <summary>
        /// The WriteLine
        /// </summary>
        private static void WriteLine()
        {
            _writer.WriteLine();
        }

        /// <summary>
        /// The PressAnyKey
        /// </summary>
        private static void PressAnyKey()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// The ModeConCols
        /// </summary>
        private static void ModeConCols()
        {
            Console.SetWindowSize(160, 50);
        }
    }
}
