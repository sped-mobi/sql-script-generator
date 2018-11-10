// -----------------------------------------------------------------------
// <copyright file="Comparers.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace System.Data.Csv.Utilities
{
    internal static class Comparers
    {
        public static readonly IComparer<Type> Type = new TypeComparer();
        public static readonly IComparer<PropertyInfo> PropertyInfo = new PropertyInfoComparer();

        private class PropertyInfoComparer : IComparer<PropertyInfo>
        {
            /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero
            /// <paramref name="x" /> is less than <paramref name="y" />.Zero
            /// <paramref name="x" /> equals <paramref name="y" />.Greater than zero
            /// <paramref name="x" /> is greater than <paramref name="y" />.</returns>
            public int Compare(PropertyInfo x, PropertyInfo y)
            {
                if (x.PropertyType == typeof(Identifier) && y.PropertyType != typeof(Identifier))
                    return -6;

                if (x.PropertyType != typeof(Identifier) && y.PropertyType == typeof(Identifier))
                    return 6;

                if (x.PropertyType == typeof(string) && y.PropertyType != typeof(string))
                    return -5;

                if (x.PropertyType != typeof(string) && y.PropertyType == typeof(string))
                    return 5;

                if (x.PropertyType == typeof(bool) && y.PropertyType != typeof(bool))
                    return -4;

                if (x.PropertyType != typeof(bool) && y.PropertyType == typeof(bool))
                    return 4;

                if (x.PropertyType.Name.StartsWith("IList", StringComparison.CurrentCulture) && !y.PropertyType.Name.StartsWith("IList", StringComparison.CurrentCulture))
                    return 3;

                if (!x.PropertyType.Name.StartsWith("IList", StringComparison.CurrentCulture) && y.PropertyType.Name.StartsWith("IList", StringComparison.CurrentCulture))
                    return -3;

                if (x.PropertyType.IsEnum && !y.PropertyType.IsEnum)
                    return 2;

                if (!x.PropertyType.IsEnum && y.PropertyType.IsEnum)
                    return -2;


                return 0;
            }
        }


        private class TypeComparer : IComparer<Type>
        {
            /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
            /// <param name="x">The first object to compare.</param>
            /// <param name="y">The second object to compare.</param>
            /// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero
            /// <paramref name="x" /> is less than <paramref name="y" />.Zero
            /// <paramref name="x" /> equals <paramref name="y" />.Greater than zero
            /// <paramref name="x" /> is greater than <paramref name="y" />.</returns>
            public int Compare(Type x, Type y)
            {
                if (x.IsPrimitive && !y.IsPrimitive)
                    return 3;

                if (x.IsPrimitive && y.IsPrimitive)
                    return 1;

                if (x.Name.StartsWith("IList", StringComparison.CurrentCulture))
                    return 2;

                return 0;
            }
        }
    }
}
