// -----------------------------------------------------------------------
// <copyright file="TSqlScriptBuilder.cs" company="sped.mobi">
//  Copyright � 2018 <Unknown>. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using DatabaseUtilities.Temp;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace DatabaseUtilities.Sql
{
    public class TableDefaultConstraint
    {
        public Table Table;
        public DefaultConstraintDefinition Constraint;
    }
}