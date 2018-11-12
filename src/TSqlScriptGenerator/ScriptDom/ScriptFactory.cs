using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.SqlServer.TransactSql.ScriptDom
{
    internal static partial class ScriptFactory
    {
        
        public static FunctionCall FunctionCall(Identifier functionName, Identifier collation, CallTarget callTarget = null, OverClause overClause = null, WithinGroupClause withinGroupClause = null, UniqueRowFilter uniqueRowFilter = UniqueRowFilter.NotSpecified, IEnumerable<ScalarExpression> parameters = null)
        {
            var retVal = new FunctionCall();
            if (functionName != null)
                retVal.FunctionName = functionName;
            if (collation != null)
                retVal.Collation = collation;
            if (callTarget != null)
                retVal.CallTarget = callTarget;
            if (overClause != null)
                retVal.OverClause = overClause;
            if (withinGroupClause != null)
                retVal.WithinGroupClause = withinGroupClause;
            retVal.UniqueRowFilter = uniqueRowFilter;
            if (parameters != null)
                retVal.Parameters.AddRange(parameters);
            return retVal;
        }
        
        public static FunctionCall FunctionCall(string functionName, string collation, CallTarget callTarget = null, OverClause overClause = null, WithinGroupClause withinGroupClause = null, UniqueRowFilter uniqueRowFilter = UniqueRowFilter.NotSpecified, IEnumerable<ScalarExpression> parameters = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new FunctionCall();
            retVal.FunctionName = Identifier(functionName, quoteType);
            retVal.Collation = Identifier(collation, quoteType);
            retVal.CallTarget = callTarget;
            retVal.OverClause = overClause;
            retVal.WithinGroupClause = withinGroupClause;
            retVal.UniqueRowFilter = uniqueRowFilter;
            retVal.Parameters.AddRange(parameters);
            return retVal;
        }
        
        public static FunctionCall FunctionCall(Identifier functionName, Identifier collation, CallTarget callTarget = null, OverClause overClause = null, WithinGroupClause withinGroupClause = null, UniqueRowFilter uniqueRowFilter = UniqueRowFilter.NotSpecified, params ScalarExpression[] parameters)
        {
            var retVal = new FunctionCall();
            if (functionName != null)
                retVal.FunctionName = functionName;
            if (collation != null)
                retVal.Collation = collation;
            if (callTarget != null)
                retVal.CallTarget = callTarget;
            if (overClause != null)
                retVal.OverClause = overClause;
            if (withinGroupClause != null)
                retVal.WithinGroupClause = withinGroupClause;
            retVal.UniqueRowFilter = uniqueRowFilter;
            if (parameters != null)
                retVal.Parameters.AddRange(parameters);
            return retVal;
        }
        
        public static FunctionCall FunctionCall(Identifier functionName, CallTarget callTarget = null, OverClause overClause = null, WithinGroupClause withinGroupClause = null, UniqueRowFilter uniqueRowFilter = UniqueRowFilter.NotSpecified, IEnumerable<ScalarExpression> parameters = null)
        {
            var retVal = new FunctionCall();
            if (functionName != null)
                retVal.FunctionName = functionName;
            if (callTarget != null)
                retVal.CallTarget = callTarget;
            if (overClause != null)
                retVal.OverClause = overClause;
            if (withinGroupClause != null)
                retVal.WithinGroupClause = withinGroupClause;
            retVal.UniqueRowFilter = uniqueRowFilter;
            if (parameters != null)
                retVal.Parameters.AddRange(parameters);
            return retVal;
        }
        
        public static FunctionCall FunctionCall(Identifier functionName, CallTarget callTarget = null, OverClause overClause = null, WithinGroupClause withinGroupClause = null, UniqueRowFilter uniqueRowFilter = UniqueRowFilter.NotSpecified, params ScalarExpression[] parameters)
        {
            var retVal = new FunctionCall();
            if (functionName != null)
                retVal.FunctionName = functionName;
            if (callTarget != null)
                retVal.CallTarget = callTarget;
            if (overClause != null)
                retVal.OverClause = overClause;
            if (withinGroupClause != null)
                retVal.WithinGroupClause = withinGroupClause;
            retVal.UniqueRowFilter = uniqueRowFilter;
            if (parameters != null)
                retVal.Parameters.AddRange(parameters);
            return retVal;
        }
        
        public static UseStatement Use(Identifier databaseName)
        {
            var retVal = new UseStatement();
            if (databaseName != null)
                retVal.DatabaseName = databaseName;
            return retVal;
        }
        
        public static UseStatement Use(string databaseName, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new UseStatement();
            retVal.DatabaseName = Identifier(databaseName, quoteType);
            return retVal;
        }
        
        public static ExecuteStatement Execute(ExecuteSpecification executeSpecification = null, IEnumerable<ExecuteOption> options = null)
        {
            var retVal = new ExecuteStatement();
            if (executeSpecification != null)
                retVal.ExecuteSpecification = executeSpecification;
            if (options != null)
                retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static ExecuteStatement Execute(ExecuteSpecification executeSpecification = null, params ExecuteOption[] options)
        {
            var retVal = new ExecuteStatement();
            if (executeSpecification != null)
                retVal.ExecuteSpecification = executeSpecification;
            if (options != null)
                retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static DropDatabaseStatement DropDatabase(bool isIfExists, IEnumerable<Identifier> databases = null)
        {
            var retVal = new DropDatabaseStatement();
            retVal.IsIfExists = isIfExists;
            if (databases != null)
                retVal.Databases.AddRange(databases);
            return retVal;
        }
        
        public static DropDatabaseStatement DropDatabase(bool isIfExists, params Identifier[] databases)
        {
            var retVal = new DropDatabaseStatement();
            retVal.IsIfExists = isIfExists;
            if (databases != null)
                retVal.Databases.AddRange(databases);
            return retVal;
        }
        
        public static AlterDatabaseSetStatement AlterDatabaseSet(Identifier databaseName, bool useCurrent, AlterDatabaseTermination termination = null, IEnumerable<DatabaseOption> options = null)
        {
            var retVal = new AlterDatabaseSetStatement();
            if (databaseName != null)
                retVal.DatabaseName = databaseName;
            retVal.UseCurrent = useCurrent;
            if (termination != null)
                retVal.Termination = termination;
            if (options != null)
                retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static AlterDatabaseSetStatement AlterDatabaseSet(string databaseName, bool useCurrent, AlterDatabaseTermination termination = null, IEnumerable<DatabaseOption> options = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new AlterDatabaseSetStatement();
            retVal.DatabaseName = Identifier(databaseName, quoteType);
            retVal.UseCurrent = useCurrent;
            retVal.Termination = termination;
            retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static AlterDatabaseSetStatement AlterDatabaseSet(Identifier databaseName, bool useCurrent, AlterDatabaseTermination termination = null, params DatabaseOption[] options)
        {
            var retVal = new AlterDatabaseSetStatement();
            if (databaseName != null)
                retVal.DatabaseName = databaseName;
            retVal.UseCurrent = useCurrent;
            if (termination != null)
                retVal.Termination = termination;
            if (options != null)
                retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static AlterTableAddTableElementStatement AlterTableAddTableElement(TableDefinition definition = null, SchemaObjectName schemaObjectName = null, ConstraintEnforcement existingRowsCheckEnforcement = ConstraintEnforcement.NotSpecified)
        {
            var retVal = new AlterTableAddTableElementStatement();
            if (definition != null)
                retVal.Definition = definition;
            if (schemaObjectName != null)
                retVal.SchemaObjectName = schemaObjectName;
            retVal.ExistingRowsCheckEnforcement = existingRowsCheckEnforcement;
            return retVal;
        }
        
        public static CreateDatabaseStatement CreateDatabase(Identifier databaseName, Identifier databaseSnapshot, Identifier collation, ContainmentDatabaseOption containment = null, MultiPartIdentifier copyOf = null, AttachMode attachMode = AttachMode.None, IEnumerable<FileGroupDefinition> fileGroups = null, IEnumerable<FileDeclaration> logOn = null, IEnumerable<DatabaseOption> options = null)
        {
            var retVal = new CreateDatabaseStatement();
            if (databaseName != null)
                retVal.DatabaseName = databaseName;
            if (databaseSnapshot != null)
                retVal.DatabaseSnapshot = databaseSnapshot;
            if (collation != null)
                retVal.Collation = collation;
            if (containment != null)
                retVal.Containment = containment;
            if (copyOf != null)
                retVal.CopyOf = copyOf;
            retVal.AttachMode = attachMode;
            if (fileGroups != null)
                retVal.FileGroups.AddRange(fileGroups);
            if (logOn != null)
                retVal.LogOn.AddRange(logOn);
            if (options != null)
                retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static CreateDatabaseStatement CreateDatabase(string databaseName, string databaseSnapshot, string collation, ContainmentDatabaseOption containment = null, MultiPartIdentifier copyOf = null, AttachMode attachMode = AttachMode.None, IEnumerable<FileGroupDefinition> fileGroups = null, IEnumerable<FileDeclaration> logOn = null, IEnumerable<DatabaseOption> options = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new CreateDatabaseStatement();
            retVal.DatabaseName = Identifier(databaseName, quoteType);
            retVal.DatabaseSnapshot = Identifier(databaseSnapshot, quoteType);
            retVal.Collation = Identifier(collation, quoteType);
            retVal.Containment = containment;
            retVal.CopyOf = copyOf;
            retVal.AttachMode = attachMode;
            retVal.FileGroups.AddRange(fileGroups);
            retVal.LogOn.AddRange(logOn);
            retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static CreateDatabaseStatement CreateDatabase(Identifier databaseName, Identifier databaseSnapshot, Identifier collation, ContainmentDatabaseOption containment = null, MultiPartIdentifier copyOf = null, AttachMode attachMode = AttachMode.None, IEnumerable<FileGroupDefinition> fileGroups = null, IEnumerable<FileDeclaration> logOn = null, params DatabaseOption[] options)
        {
            var retVal = new CreateDatabaseStatement();
            if (databaseName != null)
                retVal.DatabaseName = databaseName;
            if (databaseSnapshot != null)
                retVal.DatabaseSnapshot = databaseSnapshot;
            if (collation != null)
                retVal.Collation = collation;
            if (containment != null)
                retVal.Containment = containment;
            if (copyOf != null)
                retVal.CopyOf = copyOf;
            retVal.AttachMode = attachMode;
            if (fileGroups != null)
                retVal.FileGroups.AddRange(fileGroups);
            if (logOn != null)
                retVal.LogOn.AddRange(logOn);
            if (options != null)
                retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static CreateDatabaseStatement CreateDatabase(Identifier databaseName, ContainmentDatabaseOption containment = null, MultiPartIdentifier copyOf = null, AttachMode attachMode = AttachMode.None, IEnumerable<FileGroupDefinition> fileGroups = null, IEnumerable<FileDeclaration> logOn = null, IEnumerable<DatabaseOption> options = null)
        {
            var retVal = new CreateDatabaseStatement();
            if (databaseName != null)
                retVal.DatabaseName = databaseName;
            if (containment != null)
                retVal.Containment = containment;
            if (copyOf != null)
                retVal.CopyOf = copyOf;
            retVal.AttachMode = attachMode;
            if (fileGroups != null)
                retVal.FileGroups.AddRange(fileGroups);
            if (logOn != null)
                retVal.LogOn.AddRange(logOn);
            if (options != null)
                retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static CreateDatabaseStatement CreateDatabase(Identifier databaseName, ContainmentDatabaseOption containment = null, MultiPartIdentifier copyOf = null, AttachMode attachMode = AttachMode.None, IEnumerable<FileGroupDefinition> fileGroups = null, IEnumerable<FileDeclaration> logOn = null, params DatabaseOption[] options)
        {
            var retVal = new CreateDatabaseStatement();
            if (databaseName != null)
                retVal.DatabaseName = databaseName;
            if (containment != null)
                retVal.Containment = containment;
            if (copyOf != null)
                retVal.CopyOf = copyOf;
            retVal.AttachMode = attachMode;
            if (fileGroups != null)
                retVal.FileGroups.AddRange(fileGroups);
            if (logOn != null)
                retVal.LogOn.AddRange(logOn);
            if (options != null)
                retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static CreateDatabaseStatement CreateDatabase(string databaseName, ContainmentDatabaseOption containment = null, MultiPartIdentifier copyOf = null, AttachMode attachMode = AttachMode.None, IEnumerable<FileGroupDefinition> fileGroups = null, IEnumerable<FileDeclaration> logOn = null, IEnumerable<DatabaseOption> options = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new CreateDatabaseStatement();
            retVal.DatabaseName = Identifier(databaseName, quoteType);
            retVal.Containment = containment;
            retVal.CopyOf = copyOf;
            retVal.AttachMode = attachMode;
            retVal.FileGroups.AddRange(fileGroups);
            retVal.LogOn.AddRange(logOn);
            retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static CreateTableStatement CreateTable(bool asEdge, bool asFileTable, bool asNode, SchemaObjectName schemaObjectName = null, TableDefinition definition = null, FileGroupOrPartitionScheme onFileGroupOrPartitionScheme = null, FederationScheme federationScheme = null, IdentifierOrValueExpression textImageOn = null, IdentifierOrValueExpression fileStreamOn = null, IEnumerable<TableOption> options = null)
        {
            var retVal = new CreateTableStatement();
            retVal.AsEdge = asEdge;
            retVal.AsFileTable = asFileTable;
            retVal.AsNode = asNode;
            if (schemaObjectName != null)
                retVal.SchemaObjectName = schemaObjectName;
            if (definition != null)
                retVal.Definition = definition;
            if (onFileGroupOrPartitionScheme != null)
                retVal.OnFileGroupOrPartitionScheme = onFileGroupOrPartitionScheme;
            if (federationScheme != null)
                retVal.FederationScheme = federationScheme;
            if (textImageOn != null)
                retVal.TextImageOn = textImageOn;
            if (fileStreamOn != null)
                retVal.FileStreamOn = fileStreamOn;
            if (options != null)
                retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static CreateTableStatement CreateTable(bool asEdge, bool asFileTable, bool asNode, SchemaObjectName schemaObjectName = null, TableDefinition definition = null, FileGroupOrPartitionScheme onFileGroupOrPartitionScheme = null, FederationScheme federationScheme = null, IdentifierOrValueExpression textImageOn = null, IdentifierOrValueExpression fileStreamOn = null, params TableOption[] options)
        {
            var retVal = new CreateTableStatement();
            retVal.AsEdge = asEdge;
            retVal.AsFileTable = asFileTable;
            retVal.AsNode = asNode;
            if (schemaObjectName != null)
                retVal.SchemaObjectName = schemaObjectName;
            if (definition != null)
                retVal.Definition = definition;
            if (onFileGroupOrPartitionScheme != null)
                retVal.OnFileGroupOrPartitionScheme = onFileGroupOrPartitionScheme;
            if (federationScheme != null)
                retVal.FederationScheme = federationScheme;
            if (textImageOn != null)
                retVal.TextImageOn = textImageOn;
            if (fileStreamOn != null)
                retVal.FileStreamOn = fileStreamOn;
            if (options != null)
                retVal.Options.AddRange(options);
            return retVal;
        }
        
        public static CreateProcedureStatement CreateProcedure(bool isForReplication, ProcedureReference procedureReference = null, StatementList statementList = null, MethodSpecifier methodSpecifier = null, IEnumerable<ProcedureOption> options = null, IEnumerable<ProcedureParameter> parameters = null)
        {
            var retVal = new CreateProcedureStatement();
            retVal.IsForReplication = isForReplication;
            
            if (procedureReference != null)
                retVal.ProcedureReference = procedureReference;
            if (statementList != null)
                retVal.StatementList = statementList;
            if (methodSpecifier != null)
                retVal.MethodSpecifier = methodSpecifier;
            if (options != null)
                retVal.Options.AddRange(options);
            if (parameters != null)
                retVal.Parameters.AddRange(parameters);
            return retVal;
        }
        
        public static CreateProcedureStatement CreateProcedure(bool isForReplication, ProcedureReference procedureReference = null, StatementList statementList = null, MethodSpecifier methodSpecifier = null, IEnumerable<ProcedureOption> options = null, params ProcedureParameter[] parameters)
        {
            var retVal = new CreateProcedureStatement();
            retVal.IsForReplication = isForReplication;
            if (procedureReference != null)
                retVal.ProcedureReference = procedureReference;
            if (statementList != null)
                retVal.StatementList = statementList;
            if (methodSpecifier != null)
                retVal.MethodSpecifier = methodSpecifier;
            if (options != null)
                retVal.Options.AddRange(options);
            if (parameters != null)
                retVal.Parameters.AddRange(parameters);
            return retVal;
        }
        
        public static CreateFunctionStatement CreateFunction(SchemaObjectName name = null, FunctionReturnType returnType = null, OrderBulkInsertOption orderHint = null, StatementList statementList = null, MethodSpecifier methodSpecifier = null, IEnumerable<FunctionOption> options = null, IEnumerable<ProcedureParameter> parameters = null)
        {
            var retVal = new CreateFunctionStatement();
            if (name != null)
                retVal.Name = name;
            if (returnType != null)
                retVal.ReturnType = returnType;
            if (orderHint != null)
                retVal.OrderHint = orderHint;
            if (statementList != null)
                retVal.StatementList = statementList;
            if (methodSpecifier != null)
                retVal.MethodSpecifier = methodSpecifier;
            if (options != null)
                retVal.Options.AddRange(options);
            if (parameters != null)
                retVal.Parameters.AddRange(parameters);
            return retVal;
        }
        
        public static CreateFunctionStatement CreateFunction(SchemaObjectName name = null, FunctionReturnType returnType = null, OrderBulkInsertOption orderHint = null, StatementList statementList = null, MethodSpecifier methodSpecifier = null, IEnumerable<FunctionOption> options = null, params ProcedureParameter[] parameters)
        {
            var retVal = new CreateFunctionStatement();
            if (name != null)
                retVal.Name = name;
            if (returnType != null)
                retVal.ReturnType = returnType;
            if (orderHint != null)
                retVal.OrderHint = orderHint;
            if (statementList != null)
                retVal.StatementList = statementList;
            if (methodSpecifier != null)
                retVal.MethodSpecifier = methodSpecifier;
            if (options != null)
                retVal.Options.AddRange(options);
            if (parameters != null)
                retVal.Parameters.AddRange(parameters);
            return retVal;
        }
        
        public static CreateTriggerStatement CreateTrigger(bool withAppend, bool isNotForReplication, SchemaObjectName name = null, TriggerObject triggerObject = null, StatementList statementList = null, MethodSpecifier methodSpecifier = null, TriggerType triggerType = TriggerType.Unknown, IEnumerable<TriggerOption> options = null, IEnumerable<TriggerAction> triggerActions = null)
        {
            var retVal = new CreateTriggerStatement();
            retVal.WithAppend = withAppend;
            retVal.IsNotForReplication = isNotForReplication;
            if (name != null)
                retVal.Name = name;
            if (triggerObject != null)
                retVal.TriggerObject = triggerObject;
            if (statementList != null)
                retVal.StatementList = statementList;
            if (methodSpecifier != null)
                retVal.MethodSpecifier = methodSpecifier;
            retVal.TriggerType = triggerType;
            if (options != null)
                retVal.Options.AddRange(options);
            if (triggerActions != null)
                retVal.TriggerActions.AddRange(triggerActions);
            return retVal;
        }
        
        public static CreateTriggerStatement CreateTrigger(bool withAppend, bool isNotForReplication, SchemaObjectName name = null, TriggerObject triggerObject = null, StatementList statementList = null, MethodSpecifier methodSpecifier = null, TriggerType triggerType = TriggerType.Unknown, IEnumerable<TriggerOption> options = null, params TriggerAction[] triggerActions)
        {
            var retVal = new CreateTriggerStatement();
            retVal.WithAppend = withAppend;
            retVal.IsNotForReplication = isNotForReplication;
            if (name != null)
                retVal.Name = name;
            if (triggerObject != null)
                retVal.TriggerObject = triggerObject;
            if (statementList != null)
                retVal.StatementList = statementList;
            if (methodSpecifier != null)
                retVal.MethodSpecifier = methodSpecifier;
            retVal.TriggerType = triggerType;
            if (options != null)
                retVal.Options.AddRange(options);
            if (triggerActions != null)
                retVal.TriggerActions.AddRange(triggerActions);
            return retVal;
        }
        
        public static CreateViewStatement CreateView(bool withCheckOption, SchemaObjectName schemaObjectName = null, SelectStatement selectStatement = null, IEnumerable<Identifier> columns = null, IEnumerable<ViewOption> viewOptions = null)
        {
            var retVal = new CreateViewStatement();
            retVal.WithCheckOption = withCheckOption;
            if (schemaObjectName != null)
                retVal.SchemaObjectName = schemaObjectName;
            if (selectStatement != null)
                retVal.SelectStatement = selectStatement;
            if (columns != null)
                retVal.Columns.AddRange(columns);
            if (viewOptions != null)
                retVal.ViewOptions.AddRange(viewOptions);
            return retVal;
        }
        
        public static CreateViewStatement CreateView(bool withCheckOption, SchemaObjectName schemaObjectName = null, SelectStatement selectStatement = null, IEnumerable<Identifier> columns = null, params ViewOption[] viewOptions)
        {
            var retVal = new CreateViewStatement();
            retVal.WithCheckOption = withCheckOption;
            if (schemaObjectName != null)
                retVal.SchemaObjectName = schemaObjectName;
            if (selectStatement != null)
                retVal.SelectStatement = selectStatement;
            if (columns != null)
                retVal.Columns.AddRange(columns);
            if (viewOptions != null)
                retVal.ViewOptions.AddRange(viewOptions);
            return retVal;
        }
        
        public static CreateSchemaStatement CreateSchema(Identifier name, Identifier owner, StatementList statementList = null)
        {
            var retVal = new CreateSchemaStatement();
            if (name != null)
                retVal.Name = name;
            if (owner != null)
                retVal.Owner = owner;
            if (statementList != null)
                retVal.StatementList = statementList;
            return retVal;
        }
        
        public static CreateSchemaStatement CreateSchema(string name, string owner, StatementList statementList = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new CreateSchemaStatement();
            retVal.Name = Identifier(name, quoteType);
            retVal.Owner = Identifier(owner, quoteType);
            retVal.StatementList = statementList;
            return retVal;
        }
        
        public static CreateIndexStatement CreateIndex(Identifier name, bool translated80SyntaxTo90, bool unique, bool? clustered = null, FileGroupOrPartitionScheme onFileGroupOrPartitionScheme = null, BooleanExpression filterPredicate = null, IdentifierOrValueExpression fileStreamOn = null, SchemaObjectName onName = null, IEnumerable<ColumnWithSortOrder> columns = null, IEnumerable<ColumnReferenceExpression> includeColumns = null, IEnumerable<IndexOption> indexOptions = null)
        {
            var retVal = new CreateIndexStatement();
            if (name != null)
                retVal.Name = name;
            retVal.Translated80SyntaxTo90 = translated80SyntaxTo90;
            retVal.Unique = unique;
            if (clustered != null)
                retVal.Clustered = clustered;
            if (onFileGroupOrPartitionScheme != null)
                retVal.OnFileGroupOrPartitionScheme = onFileGroupOrPartitionScheme;
            if (filterPredicate != null)
                retVal.FilterPredicate = filterPredicate;
            if (fileStreamOn != null)
                retVal.FileStreamOn = fileStreamOn;
            if (onName != null)
                retVal.OnName = onName;
            if (columns != null)
                retVal.Columns.AddRange(columns);
            if (includeColumns != null)
                retVal.IncludeColumns.AddRange(includeColumns);
            if (indexOptions != null)
                retVal.IndexOptions.AddRange(indexOptions);
            return retVal;
        }
        
        public static CreateIndexStatement CreateIndex(string name, bool translated80SyntaxTo90, bool unique, bool? clustered = null, FileGroupOrPartitionScheme onFileGroupOrPartitionScheme = null, BooleanExpression filterPredicate = null, IdentifierOrValueExpression fileStreamOn = null, SchemaObjectName onName = null, IEnumerable<ColumnWithSortOrder> columns = null, IEnumerable<ColumnReferenceExpression> includeColumns = null, IEnumerable<IndexOption> indexOptions = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new CreateIndexStatement();
            retVal.Name = Identifier(name, quoteType);
            retVal.Translated80SyntaxTo90 = translated80SyntaxTo90;
            retVal.Unique = unique;
            retVal.Clustered = clustered;
            retVal.OnFileGroupOrPartitionScheme = onFileGroupOrPartitionScheme;
            retVal.FilterPredicate = filterPredicate;
            retVal.FileStreamOn = fileStreamOn;
            retVal.OnName = onName;
            retVal.Columns.AddRange(columns);
            retVal.IncludeColumns.AddRange(includeColumns);
            retVal.IndexOptions.AddRange(indexOptions);
            return retVal;
        }
        
        public static CreateIndexStatement CreateIndex(Identifier name, bool translated80SyntaxTo90, bool unique, bool? clustered = null, FileGroupOrPartitionScheme onFileGroupOrPartitionScheme = null, BooleanExpression filterPredicate = null, IdentifierOrValueExpression fileStreamOn = null, SchemaObjectName onName = null, IEnumerable<ColumnWithSortOrder> columns = null, IEnumerable<ColumnReferenceExpression> includeColumns = null, params IndexOption[] indexOptions)
        {
            var retVal = new CreateIndexStatement();
            if (name != null)
                retVal.Name = name;
            retVal.Translated80SyntaxTo90 = translated80SyntaxTo90;
            retVal.Unique = unique;
            if (clustered != null)
                retVal.Clustered = clustered;
            if (onFileGroupOrPartitionScheme != null)
                retVal.OnFileGroupOrPartitionScheme = onFileGroupOrPartitionScheme;
            if (filterPredicate != null)
                retVal.FilterPredicate = filterPredicate;
            if (fileStreamOn != null)
                retVal.FileStreamOn = fileStreamOn;
            if (onName != null)
                retVal.OnName = onName;
            if (columns != null)
                retVal.Columns.AddRange(columns);
            if (includeColumns != null)
                retVal.IncludeColumns.AddRange(includeColumns);
            if (indexOptions != null)
                retVal.IndexOptions.AddRange(indexOptions);
            return retVal;
        }
        
        public static ColumnDefinition ColumnDefinition(Identifier columnIdentifier, Identifier collation, bool isPersisted, bool isRowGuidCol, bool isHidden, bool isMasked, ScalarExpression computedColumnExpression = null, DefaultConstraintDefinition defaultConstraint = null, IdentityOptions identityOptions = null, ColumnStorageOptions storageOptions = null, IndexDefinition index = null, GeneratedAlwaysType? generatedAlways = null, ColumnEncryptionDefinition encryption = null, StringLiteral maskingFunction = null, DataTypeReference dataType = null, IEnumerable<ConstraintDefinition> constraints = null)
        {
            var retVal = new ColumnDefinition();
            if (columnIdentifier != null)
                retVal.ColumnIdentifier = columnIdentifier;
            if (collation != null)
                retVal.Collation = collation;
            retVal.IsPersisted = isPersisted;
            retVal.IsRowGuidCol = isRowGuidCol;
            retVal.IsHidden = isHidden;
            retVal.IsMasked = isMasked;
            if (computedColumnExpression != null)
                retVal.ComputedColumnExpression = computedColumnExpression;
            if (defaultConstraint != null)
                retVal.DefaultConstraint = defaultConstraint;
            if (identityOptions != null)
                retVal.IdentityOptions = identityOptions;
            if (storageOptions != null)
                retVal.StorageOptions = storageOptions;
            if (index != null)
                retVal.Index = index;
            if (generatedAlways != null)
                retVal.GeneratedAlways = generatedAlways;
            if (encryption != null)
                retVal.Encryption = encryption;
            if (maskingFunction != null)
                retVal.MaskingFunction = maskingFunction;
            if (dataType != null)
                retVal.DataType = dataType;
            if (constraints != null)
                retVal.Constraints.AddRange(constraints);
            return retVal;
        }
        
        public static ColumnDefinition ColumnDefinition(string columnIdentifier, string collation, bool isPersisted, bool isRowGuidCol, bool isHidden, bool isMasked, ScalarExpression computedColumnExpression = null, DefaultConstraintDefinition defaultConstraint = null, IdentityOptions identityOptions = null, ColumnStorageOptions storageOptions = null, IndexDefinition index = null, GeneratedAlwaysType? generatedAlways = null, ColumnEncryptionDefinition encryption = null, StringLiteral maskingFunction = null, DataTypeReference dataType = null, IEnumerable<ConstraintDefinition> constraints = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new ColumnDefinition();
            retVal.ColumnIdentifier = Identifier(columnIdentifier, quoteType);
            retVal.Collation = Identifier(collation, quoteType);
            retVal.IsPersisted = isPersisted;
            retVal.IsRowGuidCol = isRowGuidCol;
            retVal.IsHidden = isHidden;
            retVal.IsMasked = isMasked;
            retVal.ComputedColumnExpression = computedColumnExpression;
            retVal.DefaultConstraint = defaultConstraint;
            retVal.IdentityOptions = identityOptions;
            retVal.StorageOptions = storageOptions;
            retVal.Index = index;
            retVal.GeneratedAlways = generatedAlways;
            retVal.Encryption = encryption;
            retVal.MaskingFunction = maskingFunction;
            retVal.DataType = dataType;
            retVal.Constraints.AddRange(constraints);
            return retVal;
        }
        
        public static ColumnDefinition ColumnDefinition(Identifier columnIdentifier, Identifier collation, bool isPersisted, bool isRowGuidCol, bool isHidden, bool isMasked, ScalarExpression computedColumnExpression = null, DefaultConstraintDefinition defaultConstraint = null, IdentityOptions identityOptions = null, ColumnStorageOptions storageOptions = null, IndexDefinition index = null, GeneratedAlwaysType? generatedAlways = null, ColumnEncryptionDefinition encryption = null, StringLiteral maskingFunction = null, DataTypeReference dataType = null, params ConstraintDefinition[] constraints)
        {
            var retVal = new ColumnDefinition();
            if (columnIdentifier != null)
                retVal.ColumnIdentifier = columnIdentifier;
            if (collation != null)
                retVal.Collation = collation;
            retVal.IsPersisted = isPersisted;
            retVal.IsRowGuidCol = isRowGuidCol;
            retVal.IsHidden = isHidden;
            retVal.IsMasked = isMasked;
            if (computedColumnExpression != null)
                retVal.ComputedColumnExpression = computedColumnExpression;
            if (defaultConstraint != null)
                retVal.DefaultConstraint = defaultConstraint;
            if (identityOptions != null)
                retVal.IdentityOptions = identityOptions;
            if (storageOptions != null)
                retVal.StorageOptions = storageOptions;
            if (index != null)
                retVal.Index = index;
            if (generatedAlways != null)
                retVal.GeneratedAlways = generatedAlways;
            if (encryption != null)
                retVal.Encryption = encryption;
            if (maskingFunction != null)
                retVal.MaskingFunction = maskingFunction;
            if (dataType != null)
                retVal.DataType = dataType;
            if (constraints != null)
                retVal.Constraints.AddRange(constraints);
            return retVal;
        }
        
        public static ColumnDefinition ColumnDefinition(Identifier columnIdentifier, bool isPersisted, bool isRowGuidCol, bool isHidden, bool isMasked, ScalarExpression computedColumnExpression = null, DefaultConstraintDefinition defaultConstraint = null, IdentityOptions identityOptions = null, ColumnStorageOptions storageOptions = null, IndexDefinition index = null, ColumnEncryptionDefinition encryption = null, StringLiteral maskingFunction = null, DataTypeReference dataType = null, IEnumerable<ConstraintDefinition> constraints = null)
        {
            var retVal = new ColumnDefinition();
            if (columnIdentifier != null)
                retVal.ColumnIdentifier = columnIdentifier;
            retVal.IsPersisted = isPersisted;
            retVal.IsRowGuidCol = isRowGuidCol;
            retVal.IsHidden = isHidden;
            retVal.IsMasked = isMasked;
            if (computedColumnExpression != null)
                retVal.ComputedColumnExpression = computedColumnExpression;
            if (defaultConstraint != null)
                retVal.DefaultConstraint = defaultConstraint;
            if (identityOptions != null)
                retVal.IdentityOptions = identityOptions;
            if (storageOptions != null)
                retVal.StorageOptions = storageOptions;
            if (index != null)
                retVal.Index = index;
            if (encryption != null)
                retVal.Encryption = encryption;
            if (maskingFunction != null)
                retVal.MaskingFunction = maskingFunction;
            if (dataType != null)
                retVal.DataType = dataType;
            if (constraints != null)
                retVal.Constraints.AddRange(constraints);
            return retVal;
        }
        
        public static ColumnDefinition ColumnDefinition(Identifier columnIdentifier, bool isPersisted, bool isRowGuidCol, bool isHidden, bool isMasked, ScalarExpression computedColumnExpression = null, DefaultConstraintDefinition defaultConstraint = null, IdentityOptions identityOptions = null, ColumnStorageOptions storageOptions = null, IndexDefinition index = null, ColumnEncryptionDefinition encryption = null, StringLiteral maskingFunction = null, DataTypeReference dataType = null, params ConstraintDefinition[] constraints)
        {
            var retVal = new ColumnDefinition();
            if (columnIdentifier != null)
                retVal.ColumnIdentifier = columnIdentifier;
            retVal.IsPersisted = isPersisted;
            retVal.IsRowGuidCol = isRowGuidCol;
            retVal.IsHidden = isHidden;
            retVal.IsMasked = isMasked;
            if (computedColumnExpression != null)
                retVal.ComputedColumnExpression = computedColumnExpression;
            if (defaultConstraint != null)
                retVal.DefaultConstraint = defaultConstraint;
            if (identityOptions != null)
                retVal.IdentityOptions = identityOptions;
            if (storageOptions != null)
                retVal.StorageOptions = storageOptions;
            if (index != null)
                retVal.Index = index;
            if (encryption != null)
                retVal.Encryption = encryption;
            if (maskingFunction != null)
                retVal.MaskingFunction = maskingFunction;
            if (dataType != null)
                retVal.DataType = dataType;
            if (constraints != null)
                retVal.Constraints.AddRange(constraints);
            return retVal;
        }
        
        public static ColumnDefinition ColumnDefinition(string columnIdentifier, bool isPersisted, bool isRowGuidCol, bool isHidden, bool isMasked, ScalarExpression computedColumnExpression = null, DefaultConstraintDefinition defaultConstraint = null, IdentityOptions identityOptions = null, ColumnStorageOptions storageOptions = null, IndexDefinition index = null, ColumnEncryptionDefinition encryption = null, StringLiteral maskingFunction = null, DataTypeReference dataType = null, IEnumerable<ConstraintDefinition> constraints = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new ColumnDefinition();
            retVal.ColumnIdentifier = Identifier(columnIdentifier, quoteType);
            retVal.IsPersisted = isPersisted;
            retVal.IsRowGuidCol = isRowGuidCol;
            retVal.IsHidden = isHidden;
            retVal.IsMasked = isMasked;
            retVal.ComputedColumnExpression = computedColumnExpression;
            retVal.DefaultConstraint = defaultConstraint;
            retVal.IdentityOptions = identityOptions;
            retVal.StorageOptions = storageOptions;
            retVal.Index = index;
            retVal.Encryption = encryption;
            retVal.MaskingFunction = maskingFunction;
            retVal.DataType = dataType;
            retVal.Constraints.AddRange(constraints);
            return retVal;
        }
        
        public static TableDefinition TableDefinition(SystemTimePeriodDefinition systemTimePeriod = null, IEnumerable<ColumnDefinition> columnDefinitions = null, IEnumerable<ConstraintDefinition> tableConstraints = null, IEnumerable<IndexDefinition> indexes = null)
        {
            var retVal = new TableDefinition();
            if (systemTimePeriod != null)
                retVal.SystemTimePeriod = systemTimePeriod;
            if (columnDefinitions != null)
                retVal.ColumnDefinitions.AddRange(columnDefinitions);
            if (tableConstraints != null)
                retVal.TableConstraints.AddRange(tableConstraints);
            if (indexes != null)
                retVal.Indexes.AddRange(indexes);
            return retVal;
        }
        
        public static TableDefinition TableDefinition(SystemTimePeriodDefinition systemTimePeriod = null, IEnumerable<ColumnDefinition> columnDefinitions = null, IEnumerable<ConstraintDefinition> tableConstraints = null, params IndexDefinition[] indexes)
        {
            var retVal = new TableDefinition();
            if (systemTimePeriod != null)
                retVal.SystemTimePeriod = systemTimePeriod;
            if (columnDefinitions != null)
                retVal.ColumnDefinitions.AddRange(columnDefinitions);
            if (tableConstraints != null)
                retVal.TableConstraints.AddRange(tableConstraints);
            if (indexes != null)
                retVal.Indexes.AddRange(indexes);
            return retVal;
        }
        
        public static IndexDefinition IndexDefinition(Identifier name, bool unique, IndexType indexType = null, FileGroupOrPartitionScheme onFileGroupOrPartitionScheme = null, BooleanExpression filterPredicate = null, IdentifierOrValueExpression fileStreamOn = null, IEnumerable<IndexOption> indexOptions = null, IEnumerable<ColumnWithSortOrder> columns = null)
        {
            var retVal = new IndexDefinition();
            if (name != null)
                retVal.Name = name;
            retVal.Unique = unique;
            if (indexType != null)
                retVal.IndexType = indexType;
            if (onFileGroupOrPartitionScheme != null)
                retVal.OnFileGroupOrPartitionScheme = onFileGroupOrPartitionScheme;
            if (filterPredicate != null)
                retVal.FilterPredicate = filterPredicate;
            if (fileStreamOn != null)
                retVal.FileStreamOn = fileStreamOn;
            if (indexOptions != null)
                retVal.IndexOptions.AddRange(indexOptions);
            if (columns != null)
                retVal.Columns.AddRange(columns);
            return retVal;
        }
        
        public static IndexDefinition IndexDefinition(string name, bool unique, IndexType indexType = null, FileGroupOrPartitionScheme onFileGroupOrPartitionScheme = null, BooleanExpression filterPredicate = null, IdentifierOrValueExpression fileStreamOn = null, IEnumerable<IndexOption> indexOptions = null, IEnumerable<ColumnWithSortOrder> columns = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new IndexDefinition();
            retVal.Name = Identifier(name, quoteType);
            retVal.Unique = unique;
            retVal.IndexType = indexType;
            retVal.OnFileGroupOrPartitionScheme = onFileGroupOrPartitionScheme;
            retVal.FilterPredicate = filterPredicate;
            retVal.FileStreamOn = fileStreamOn;
            retVal.IndexOptions.AddRange(indexOptions);
            retVal.Columns.AddRange(columns);
            return retVal;
        }
        
        public static IndexDefinition IndexDefinition(Identifier name, bool unique, IndexType indexType = null, FileGroupOrPartitionScheme onFileGroupOrPartitionScheme = null, BooleanExpression filterPredicate = null, IdentifierOrValueExpression fileStreamOn = null, IEnumerable<IndexOption> indexOptions = null, params ColumnWithSortOrder[] columns)
        {
            var retVal = new IndexDefinition();
            if (name != null)
                retVal.Name = name;
            retVal.Unique = unique;
            if (indexType != null)
                retVal.IndexType = indexType;
            if (onFileGroupOrPartitionScheme != null)
                retVal.OnFileGroupOrPartitionScheme = onFileGroupOrPartitionScheme;
            if (filterPredicate != null)
                retVal.FilterPredicate = filterPredicate;
            if (fileStreamOn != null)
                retVal.FileStreamOn = fileStreamOn;
            if (indexOptions != null)
                retVal.IndexOptions.AddRange(indexOptions);
            if (columns != null)
                retVal.Columns.AddRange(columns);
            return retVal;
        }
        
        public static NullableConstraintDefinition NullableConstraintDefinition(Identifier constraintIdentifier, bool nullable)
        {
            var retVal = new NullableConstraintDefinition();
            if (constraintIdentifier != null)
                retVal.ConstraintIdentifier = constraintIdentifier;
            retVal.Nullable = nullable;
            return retVal;
        }
        
        public static NullableConstraintDefinition NullableConstraintDefinition(string constraintIdentifier, bool nullable, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new NullableConstraintDefinition();
            retVal.ConstraintIdentifier = Identifier(constraintIdentifier, quoteType);
            retVal.Nullable = nullable;
            return retVal;
        }
        
        public static CheckConstraintDefinition CheckConstraintDefinition(Identifier constraintIdentifier, bool notForReplication, BooleanExpression checkCondition = null)
        {
            var retVal = new CheckConstraintDefinition();
            if (constraintIdentifier != null)
                retVal.ConstraintIdentifier = constraintIdentifier;
            retVal.NotForReplication = notForReplication;
            if (checkCondition != null)
                retVal.CheckCondition = checkCondition;
            return retVal;
        }
        
        public static CheckConstraintDefinition CheckConstraintDefinition(string constraintIdentifier, bool notForReplication, BooleanExpression checkCondition = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new CheckConstraintDefinition();
            retVal.ConstraintIdentifier = Identifier(constraintIdentifier, quoteType);
            retVal.NotForReplication = notForReplication;
            retVal.CheckCondition = checkCondition;
            return retVal;
        }
        
        public static DefaultConstraintDefinition DefaultConstraintDefinition(Identifier column, Identifier constraintIdentifier, bool withValues, ScalarExpression expression = null)
        {
            var retVal = new DefaultConstraintDefinition();
            if (column != null)
                retVal.Column = column;
            if (constraintIdentifier != null)
                retVal.ConstraintIdentifier = constraintIdentifier;
            retVal.WithValues = withValues;
            if (expression != null)
                retVal.Expression = expression;
            return retVal;
        }
        
        public static DefaultConstraintDefinition DefaultConstraintDefinition(string column, string constraintIdentifier, bool withValues, ScalarExpression expression = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new DefaultConstraintDefinition();
            retVal.Column = Identifier(column, quoteType);
            retVal.ConstraintIdentifier = Identifier(constraintIdentifier, quoteType);
            retVal.WithValues = withValues;
            retVal.Expression = expression;
            return retVal;
        }
        
        public static ForeignKeyConstraintDefinition ForeignKeyConstraintDefinition(Identifier constraintIdentifier, bool notForReplication, SchemaObjectName referenceTableName = null, DeleteUpdateAction deleteAction = DeleteUpdateAction.NotSpecified, DeleteUpdateAction updateAction = DeleteUpdateAction.NotSpecified, IEnumerable<Identifier> columns = null, IEnumerable<Identifier> referencedTableColumns = null)
        {
            var retVal = new ForeignKeyConstraintDefinition();
            if (constraintIdentifier != null)
                retVal.ConstraintIdentifier = constraintIdentifier;
            retVal.NotForReplication = notForReplication;
            if (referenceTableName != null)
                retVal.ReferenceTableName = referenceTableName;
            retVal.DeleteAction = deleteAction;
            retVal.UpdateAction = updateAction;
            if (columns != null)
                retVal.Columns.AddRange(columns);
            if (referencedTableColumns != null)
                retVal.ReferencedTableColumns.AddRange(referencedTableColumns);
            return retVal;
        }
        
        public static ForeignKeyConstraintDefinition ForeignKeyConstraintDefinition(string constraintIdentifier, bool notForReplication, SchemaObjectName referenceTableName = null, DeleteUpdateAction deleteAction = DeleteUpdateAction.NotSpecified, DeleteUpdateAction updateAction = DeleteUpdateAction.NotSpecified, IEnumerable<Identifier> columns = null, IEnumerable<Identifier> referencedTableColumns = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new ForeignKeyConstraintDefinition();
            retVal.ConstraintIdentifier = Identifier(constraintIdentifier, quoteType);
            retVal.NotForReplication = notForReplication;
            retVal.ReferenceTableName = referenceTableName;
            retVal.DeleteAction = deleteAction;
            retVal.UpdateAction = updateAction;
            retVal.Columns.AddRange(columns);
            retVal.ReferencedTableColumns.AddRange(referencedTableColumns);
            return retVal;
        }
        
        public static ForeignKeyConstraintDefinition ForeignKeyConstraintDefinition(Identifier constraintIdentifier, bool notForReplication, SchemaObjectName referenceTableName = null, DeleteUpdateAction deleteAction = DeleteUpdateAction.NotSpecified, DeleteUpdateAction updateAction = DeleteUpdateAction.NotSpecified, IEnumerable<Identifier> columns = null, params Identifier[] referencedTableColumns)
        {
            var retVal = new ForeignKeyConstraintDefinition();
            if (constraintIdentifier != null)
                retVal.ConstraintIdentifier = constraintIdentifier;
            retVal.NotForReplication = notForReplication;
            if (referenceTableName != null)
                retVal.ReferenceTableName = referenceTableName;
            retVal.DeleteAction = deleteAction;
            retVal.UpdateAction = updateAction;
            if (columns != null)
                retVal.Columns.AddRange(columns);
            if (referencedTableColumns != null)
                retVal.ReferencedTableColumns.AddRange(referencedTableColumns);
            return retVal;
        }
        
        public static UniqueConstraintDefinition UniqueConstraintDefinition(Identifier constraintIdentifier, bool isPrimaryKey, bool? clustered = null, FileGroupOrPartitionScheme onFileGroupOrPartitionScheme = null, IndexType indexType = null, IdentifierOrValueExpression fileStreamOn = null, IEnumerable<ColumnWithSortOrder> columns = null, IEnumerable<IndexOption> indexOptions = null)
        {
            var retVal = new UniqueConstraintDefinition();
            if (constraintIdentifier != null)
                retVal.ConstraintIdentifier = constraintIdentifier;
            retVal.IsPrimaryKey = isPrimaryKey;
            if (clustered != null)
                retVal.Clustered = clustered;
            if (onFileGroupOrPartitionScheme != null)
                retVal.OnFileGroupOrPartitionScheme = onFileGroupOrPartitionScheme;
            if (indexType != null)
                retVal.IndexType = indexType;
            if (fileStreamOn != null)
                retVal.FileStreamOn = fileStreamOn;
            if (columns != null)
                retVal.Columns.AddRange(columns);
            if (indexOptions != null)
                retVal.IndexOptions.AddRange(indexOptions);
            return retVal;
        }
        
        public static UniqueConstraintDefinition UniqueConstraintDefinition(string constraintIdentifier, bool isPrimaryKey, bool? clustered = null, FileGroupOrPartitionScheme onFileGroupOrPartitionScheme = null, IndexType indexType = null, IdentifierOrValueExpression fileStreamOn = null, IEnumerable<ColumnWithSortOrder> columns = null, IEnumerable<IndexOption> indexOptions = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new UniqueConstraintDefinition();
            retVal.ConstraintIdentifier = Identifier(constraintIdentifier, quoteType);
            retVal.IsPrimaryKey = isPrimaryKey;
            retVal.Clustered = clustered;
            retVal.OnFileGroupOrPartitionScheme = onFileGroupOrPartitionScheme;
            retVal.IndexType = indexType;
            retVal.FileStreamOn = fileStreamOn;
            retVal.Columns.AddRange(columns);
            retVal.IndexOptions.AddRange(indexOptions);
            return retVal;
        }
        
        public static UniqueConstraintDefinition UniqueConstraintDefinition(Identifier constraintIdentifier, bool isPrimaryKey, bool? clustered = null, FileGroupOrPartitionScheme onFileGroupOrPartitionScheme = null, IndexType indexType = null, IdentifierOrValueExpression fileStreamOn = null, IEnumerable<ColumnWithSortOrder> columns = null, params IndexOption[] indexOptions)
        {
            var retVal = new UniqueConstraintDefinition();
            if (constraintIdentifier != null)
                retVal.ConstraintIdentifier = constraintIdentifier;
            retVal.IsPrimaryKey = isPrimaryKey;
            if (clustered != null)
                retVal.Clustered = clustered;
            if (onFileGroupOrPartitionScheme != null)
                retVal.OnFileGroupOrPartitionScheme = onFileGroupOrPartitionScheme;
            if (indexType != null)
                retVal.IndexType = indexType;
            if (fileStreamOn != null)
                retVal.FileStreamOn = fileStreamOn;
            if (columns != null)
                retVal.Columns.AddRange(columns);
            if (indexOptions != null)
                retVal.IndexOptions.AddRange(indexOptions);
            return retVal;
        }
        
        public static UserDataTypeReference UserDataTypeReference(SchemaObjectName name = null, IEnumerable<Literal> parameters = null)
        {
            var retVal = new UserDataTypeReference();
            if (name != null)
                retVal.Name = name;
            if (parameters != null)
                retVal.Parameters.AddRange(parameters);
            return retVal;
        }
        
        public static UserDataTypeReference UserDataTypeReference(SchemaObjectName name = null, params Literal[] parameters)
        {
            var retVal = new UserDataTypeReference();
            if (name != null)
                retVal.Name = name;
            if (parameters != null)
                retVal.Parameters.AddRange(parameters);
            return retVal;
        }
        
        public static InsertStatement Insert(InsertSpecification insertSpecification = null, WithCtesAndXmlNamespaces withCtesAndXmlNamespaces = null, IEnumerable<OptimizerHint> optimizerHints = null)
        {
            var retVal = new InsertStatement();
            if (insertSpecification != null)
                retVal.InsertSpecification = insertSpecification;
            if (withCtesAndXmlNamespaces != null)
                retVal.WithCtesAndXmlNamespaces = withCtesAndXmlNamespaces;
            if (optimizerHints != null)
                retVal.OptimizerHints.AddRange(optimizerHints);
            return retVal;
        }
        
        public static InsertStatement Insert(InsertSpecification insertSpecification = null, WithCtesAndXmlNamespaces withCtesAndXmlNamespaces = null, params OptimizerHint[] optimizerHints)
        {
            var retVal = new InsertStatement();
            if (insertSpecification != null)
                retVal.InsertSpecification = insertSpecification;
            if (withCtesAndXmlNamespaces != null)
                retVal.WithCtesAndXmlNamespaces = withCtesAndXmlNamespaces;
            if (optimizerHints != null)
                retVal.OptimizerHints.AddRange(optimizerHints);
            return retVal;
        }
        
        public static UpdateStatement Update(UpdateSpecification updateSpecification = null, WithCtesAndXmlNamespaces withCtesAndXmlNamespaces = null, IEnumerable<OptimizerHint> optimizerHints = null)
        {
            var retVal = new UpdateStatement();
            if (updateSpecification != null)
                retVal.UpdateSpecification = updateSpecification;
            if (withCtesAndXmlNamespaces != null)
                retVal.WithCtesAndXmlNamespaces = withCtesAndXmlNamespaces;
            if (optimizerHints != null)
                retVal.OptimizerHints.AddRange(optimizerHints);
            return retVal;
        }
        
        public static UpdateStatement Update(UpdateSpecification updateSpecification = null, WithCtesAndXmlNamespaces withCtesAndXmlNamespaces = null, params OptimizerHint[] optimizerHints)
        {
            var retVal = new UpdateStatement();
            if (updateSpecification != null)
                retVal.UpdateSpecification = updateSpecification;
            if (withCtesAndXmlNamespaces != null)
                retVal.WithCtesAndXmlNamespaces = withCtesAndXmlNamespaces;
            if (optimizerHints != null)
                retVal.OptimizerHints.AddRange(optimizerHints);
            return retVal;
        }
        
        public static DeleteStatement Delete(DeleteSpecification deleteSpecification = null, WithCtesAndXmlNamespaces withCtesAndXmlNamespaces = null, IEnumerable<OptimizerHint> optimizerHints = null)
        {
            var retVal = new DeleteStatement();
            if (deleteSpecification != null)
                retVal.DeleteSpecification = deleteSpecification;
            if (withCtesAndXmlNamespaces != null)
                retVal.WithCtesAndXmlNamespaces = withCtesAndXmlNamespaces;
            if (optimizerHints != null)
                retVal.OptimizerHints.AddRange(optimizerHints);
            return retVal;
        }
        
        public static DeleteStatement Delete(DeleteSpecification deleteSpecification = null, WithCtesAndXmlNamespaces withCtesAndXmlNamespaces = null, params OptimizerHint[] optimizerHints)
        {
            var retVal = new DeleteStatement();
            if (deleteSpecification != null)
                retVal.DeleteSpecification = deleteSpecification;
            if (withCtesAndXmlNamespaces != null)
                retVal.WithCtesAndXmlNamespaces = withCtesAndXmlNamespaces;
            if (optimizerHints != null)
                retVal.OptimizerHints.AddRange(optimizerHints);
            return retVal;
        }
        
        public static SelectStatement Select(Identifier on, QueryExpression queryExpression = null, SchemaObjectName into = null, WithCtesAndXmlNamespaces withCtesAndXmlNamespaces = null, IEnumerable<ComputeClause> computeClauses = null, IEnumerable<OptimizerHint> optimizerHints = null)
        {
            var retVal = new SelectStatement();
            if (on != null)
                retVal.On = on;
            if (queryExpression != null)
                retVal.QueryExpression = queryExpression;
            if (into != null)
                retVal.Into = into;
            if (withCtesAndXmlNamespaces != null)
                retVal.WithCtesAndXmlNamespaces = withCtesAndXmlNamespaces;
            if (computeClauses != null)
                retVal.ComputeClauses.AddRange(computeClauses);
            if (optimizerHints != null)
                retVal.OptimizerHints.AddRange(optimizerHints);
            return retVal;
        }
        
        public static SelectStatement Select(string on, QueryExpression queryExpression = null, SchemaObjectName into = null, WithCtesAndXmlNamespaces withCtesAndXmlNamespaces = null, IEnumerable<ComputeClause> computeClauses = null, IEnumerable<OptimizerHint> optimizerHints = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new SelectStatement();
            retVal.On = Identifier(on, quoteType);
            retVal.QueryExpression = queryExpression;
            retVal.Into = into;
            retVal.WithCtesAndXmlNamespaces = withCtesAndXmlNamespaces;
            retVal.ComputeClauses.AddRange(computeClauses);
            retVal.OptimizerHints.AddRange(optimizerHints);
            return retVal;
        }
        
        public static SelectStatement Select(Identifier on, QueryExpression queryExpression = null, SchemaObjectName into = null, WithCtesAndXmlNamespaces withCtesAndXmlNamespaces = null, IEnumerable<ComputeClause> computeClauses = null, params OptimizerHint[] optimizerHints)
        {
            var retVal = new SelectStatement();
            if (on != null)
                retVal.On = on;
            if (queryExpression != null)
                retVal.QueryExpression = queryExpression;
            if (into != null)
                retVal.Into = into;
            if (withCtesAndXmlNamespaces != null)
                retVal.WithCtesAndXmlNamespaces = withCtesAndXmlNamespaces;
            if (computeClauses != null)
                retVal.ComputeClauses.AddRange(computeClauses);
            if (optimizerHints != null)
                retVal.OptimizerHints.AddRange(optimizerHints);
            return retVal;
        }
        
        public static InsertSpecification InsertSpecification(InsertSource insertSource = null, TableReference target = null, TopRowFilter topRowFilter = null, OutputIntoClause outputIntoClause = null, OutputClause outputClause = null, InsertOption insertOption = InsertOption.None, IEnumerable<ColumnReferenceExpression> columns = null)
        {
            var retVal = new InsertSpecification();
            if (insertSource != null)
                retVal.InsertSource = insertSource;
            if (target != null)
                retVal.Target = target;
            if (topRowFilter != null)
                retVal.TopRowFilter = topRowFilter;
            if (outputIntoClause != null)
                retVal.OutputIntoClause = outputIntoClause;
            if (outputClause != null)
                retVal.OutputClause = outputClause;
            retVal.InsertOption = insertOption;
            if (columns != null)
                retVal.Columns.AddRange(columns);
            return retVal;
        }
        
        public static InsertSpecification InsertSpecification(InsertSource insertSource = null, TableReference target = null, TopRowFilter topRowFilter = null, OutputIntoClause outputIntoClause = null, OutputClause outputClause = null, InsertOption insertOption = InsertOption.None, params ColumnReferenceExpression[] columns)
        {
            var retVal = new InsertSpecification();
            if (insertSource != null)
                retVal.InsertSource = insertSource;
            if (target != null)
                retVal.Target = target;
            if (topRowFilter != null)
                retVal.TopRowFilter = topRowFilter;
            if (outputIntoClause != null)
                retVal.OutputIntoClause = outputIntoClause;
            if (outputClause != null)
                retVal.OutputClause = outputClause;
            retVal.InsertOption = insertOption;
            if (columns != null)
                retVal.Columns.AddRange(columns);
            return retVal;
        }
        
        public static UpdateSpecification UpdateSpecification(FromClause fromClause = null, WhereClause whereClause = null, TableReference target = null, TopRowFilter topRowFilter = null, OutputIntoClause outputIntoClause = null, OutputClause outputClause = null, IEnumerable<SetClause> setClauses = null)
        {
            var retVal = new UpdateSpecification();
            if (fromClause != null)
                retVal.FromClause = fromClause;
            if (whereClause != null)
                retVal.WhereClause = whereClause;
            if (target != null)
                retVal.Target = target;
            if (topRowFilter != null)
                retVal.TopRowFilter = topRowFilter;
            if (outputIntoClause != null)
                retVal.OutputIntoClause = outputIntoClause;
            if (outputClause != null)
                retVal.OutputClause = outputClause;
            if (setClauses != null)
                retVal.SetClauses.AddRange(setClauses);
            return retVal;
        }
        
        public static UpdateSpecification UpdateSpecification(FromClause fromClause = null, WhereClause whereClause = null, TableReference target = null, TopRowFilter topRowFilter = null, OutputIntoClause outputIntoClause = null, OutputClause outputClause = null, params SetClause[] setClauses)
        {
            var retVal = new UpdateSpecification();
            if (fromClause != null)
                retVal.FromClause = fromClause;
            if (whereClause != null)
                retVal.WhereClause = whereClause;
            if (target != null)
                retVal.Target = target;
            if (topRowFilter != null)
                retVal.TopRowFilter = topRowFilter;
            if (outputIntoClause != null)
                retVal.OutputIntoClause = outputIntoClause;
            if (outputClause != null)
                retVal.OutputClause = outputClause;
            if (setClauses != null)
                retVal.SetClauses.AddRange(setClauses);
            return retVal;
        }
        
        public static DeleteSpecification DeleteSpecification(FromClause fromClause = null, WhereClause whereClause = null, TableReference target = null, TopRowFilter topRowFilter = null, OutputIntoClause outputIntoClause = null, OutputClause outputClause = null)
        {
            var retVal = new DeleteSpecification();
            if (fromClause != null)
                retVal.FromClause = fromClause;
            if (whereClause != null)
                retVal.WhereClause = whereClause;
            if (target != null)
                retVal.Target = target;
            if (topRowFilter != null)
                retVal.TopRowFilter = topRowFilter;
            if (outputIntoClause != null)
                retVal.OutputIntoClause = outputIntoClause;
            if (outputClause != null)
                retVal.OutputClause = outputClause;
            return retVal;
        }
        
        public static IntegerLiteral IntegerLiteral(Identifier collation, string value)
        {
            var retVal = new IntegerLiteral();
            if (collation != null)
                retVal.Collation = collation;
            if (value != null)
                retVal.Value = value;
            return retVal;
        }
        
        public static IntegerLiteral IntegerLiteral(string collation, string value, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new IntegerLiteral();
            retVal.Collation = Identifier(collation, quoteType);
            retVal.Value = value;
            return retVal;
        }
        
        public static IntegerLiteral IntegerLiteral(string value)
        {
            var retVal = new IntegerLiteral();
            if (value != null)
                retVal.Value = value;
            return retVal;
        }
        
        public static StringLiteral StringLiteral(Identifier collation, string value, bool isNational, bool isLargeObject)
        {
            var retVal = new StringLiteral();
            if (collation != null)
                retVal.Collation = collation;
            if (value != null)
                retVal.Value = value;
            retVal.IsNational = isNational;
            retVal.IsLargeObject = isLargeObject;
            return retVal;
        }
        
        public static StringLiteral StringLiteral(string collation, string value, bool isNational, bool isLargeObject, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new StringLiteral();
            retVal.Collation = Identifier(collation, quoteType);
            retVal.Value = value;
            retVal.IsNational = isNational;
            retVal.IsLargeObject = isLargeObject;
            return retVal;
        }
        
        public static StringLiteral StringLiteral(string value, bool isNational, bool isLargeObject)
        {
            var retVal = new StringLiteral();
            if (value != null)
                retVal.Value = value;
            retVal.IsNational = isNational;
            retVal.IsLargeObject = isLargeObject;
            return retVal;
        }
        
        public static NullLiteral NullLiteral(Identifier collation, string value)
        {
            var retVal = new NullLiteral();
            if (collation != null)
                retVal.Collation = collation;
            if (value != null)
                retVal.Value = value;
            return retVal;
        }
        
        public static NullLiteral NullLiteral(string collation, string value, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new NullLiteral();
            retVal.Collation = Identifier(collation, quoteType);
            retVal.Value = value;
            return retVal;
        }
        
        public static NullLiteral NullLiteral(string value)
        {
            var retVal = new NullLiteral();
            if (value != null)
                retVal.Value = value;
            return retVal;
        }
        
        public static MaxLiteral MaxLiteral(Identifier collation, string value)
        {
            var retVal = new MaxLiteral();
            if (collation != null)
                retVal.Collation = collation;
            if (value != null)
                retVal.Value = value;
            return retVal;
        }
        
        public static MaxLiteral MaxLiteral(string collation, string value, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new MaxLiteral();
            retVal.Collation = Identifier(collation, quoteType);
            retVal.Value = value;
            return retVal;
        }
        
        public static MaxLiteral MaxLiteral(string value)
        {
            var retVal = new MaxLiteral();
            if (value != null)
                retVal.Value = value;
            return retVal;
        }
        
        public static DefaultLiteral DefaultLiteral(Identifier collation, string value)
        {
            var retVal = new DefaultLiteral();
            if (collation != null)
                retVal.Collation = collation;
            if (value != null)
                retVal.Value = value;
            return retVal;
        }
        
        public static DefaultLiteral DefaultLiteral(string collation, string value, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new DefaultLiteral();
            retVal.Collation = Identifier(collation, quoteType);
            retVal.Value = value;
            return retVal;
        }
        
        public static DefaultLiteral DefaultLiteral(string value)
        {
            var retVal = new DefaultLiteral();
            if (value != null)
                retVal.Value = value;
            return retVal;
        }
        
        public static IdentifierLiteral IdentifierLiteral(Identifier collation, string value, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new IdentifierLiteral();
            if (collation != null)
                retVal.Collation = collation;
            if (value != null)
                retVal.Value = value;
            retVal.QuoteType = quoteType;
            return retVal;
        }
        
        public static IdentifierLiteral IdentifierLiteral(string collation, string value, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new IdentifierLiteral();
            retVal.Collation = Identifier(collation, quoteType);
            retVal.Value = value;
            retVal.QuoteType = quoteType;
            return retVal;
        }
        
        public static IdentifierLiteral IdentifierLiteral(string value, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new IdentifierLiteral();
            if (value != null)
                retVal.Value = value;
            retVal.QuoteType = quoteType;
            return retVal;
        }
        
        public static PrintStatement Print(ScalarExpression expression = null)
        {
            var retVal = new PrintStatement();
            if (expression != null)
                retVal.Expression = expression;
            return retVal;
        }
        
        public static ParenthesisExpression ParenthesisExpression(Identifier collation, ScalarExpression expression = null)
        {
            var retVal = new ParenthesisExpression();
            if (collation != null)
                retVal.Collation = collation;
            if (expression != null)
                retVal.Expression = expression;
            return retVal;
        }
        
        public static ParenthesisExpression ParenthesisExpression(string collation, ScalarExpression expression = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new ParenthesisExpression();
            retVal.Collation = Identifier(collation, quoteType);
            retVal.Expression = expression;
            return retVal;
        }
        
        public static ParenthesisExpression ParenthesisExpression(ScalarExpression expression = null)
        {
            var retVal = new ParenthesisExpression();
            if (expression != null)
                retVal.Expression = expression;
            return retVal;
        }
        
        public static IdentityOptions IdentityOptions(bool isIdentityNotForReplication, ScalarExpression identityIncrement = null, ScalarExpression identitySeed = null)
        {
            var retVal = new IdentityOptions();
            retVal.IsIdentityNotForReplication = isIdentityNotForReplication;
            if (identityIncrement != null)
                retVal.IdentityIncrement = identityIncrement;
            if (identitySeed != null)
                retVal.IdentitySeed = identitySeed;
            return retVal;
        }
        
        public static ColumnWithSortOrder ColumnWithSortOrder(ColumnReferenceExpression column = null, SortOrder sortOrder = SortOrder.NotSpecified)
        {
            var retVal = new ColumnWithSortOrder();
            if (column != null)
                retVal.Column = column;
            retVal.SortOrder = sortOrder;
            return retVal;
        }
        
        public static ColumnReferenceExpression ColumnReferenceExpression(Identifier collation, MultiPartIdentifier multiPartIdentifier = null, ColumnType columnType = ColumnType.Regular)
        {
            var retVal = new ColumnReferenceExpression();
            if (collation != null)
                retVal.Collation = collation;
            if (multiPartIdentifier != null)
                retVal.MultiPartIdentifier = multiPartIdentifier;
            retVal.ColumnType = columnType;
            return retVal;
        }
        
        public static ColumnReferenceExpression ColumnReferenceExpression(string collation, MultiPartIdentifier multiPartIdentifier = null, ColumnType columnType = ColumnType.Regular, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new ColumnReferenceExpression();
            retVal.Collation = Identifier(collation, quoteType);
            retVal.MultiPartIdentifier = multiPartIdentifier;
            retVal.ColumnType = columnType;
            return retVal;
        }
        
        public static ColumnReferenceExpression ColumnReferenceExpression(MultiPartIdentifier multiPartIdentifier = null, ColumnType columnType = ColumnType.Regular)
        {
            var retVal = new ColumnReferenceExpression();
            if (multiPartIdentifier != null)
                retVal.MultiPartIdentifier = multiPartIdentifier;
            retVal.ColumnType = columnType;
            return retVal;
        }
        
        public static ProcedureReference ProcedureReference(SchemaObjectName name = null, Literal number = null)
        {
            var retVal = new ProcedureReference();
            if (name != null)
                retVal.Name = name;
            if (number != null)
                retVal.Number = number;
            return retVal;
        }
        
        public static ProcedureReferenceName ProcedureReferenceName(ProcedureReference procedureReference = null, VariableReference procedureVariable = null)
        {
            var retVal = new ProcedureReferenceName();
            if (procedureReference != null)
                retVal.ProcedureReference = procedureReference;
            if (procedureVariable != null)
                retVal.ProcedureVariable = procedureVariable;
            return retVal;
        }
        
        public static ProcedureParameter ProcedureParameter(Identifier variableName, bool isVarying, DataTypeReference dataType = null, NullableConstraintDefinition nullable = null, ScalarExpression value = null, ParameterModifier modifier = ParameterModifier.None)
        {
            var retVal = new ProcedureParameter();
            if (variableName != null)
                retVal.VariableName = variableName;
            retVal.IsVarying = isVarying;
            if (dataType != null)
                retVal.DataType = dataType;
            if (nullable != null)
                retVal.Nullable = nullable;
            if (value != null)
                retVal.Value = value;
            retVal.Modifier = modifier;
            return retVal;
        }
        
        public static ProcedureParameter ProcedureParameter(string variableName, bool isVarying, DataTypeReference dataType = null, NullableConstraintDefinition nullable = null, ScalarExpression value = null, ParameterModifier modifier = ParameterModifier.None, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new ProcedureParameter();
            retVal.VariableName = Identifier(variableName, quoteType);
            retVal.IsVarying = isVarying;
            retVal.DataType = dataType;
            retVal.Nullable = nullable;
            retVal.Value = value;
            retVal.Modifier = modifier;
            
            return retVal;
        }
        
        public static StatementList List(IEnumerable<TSqlStatement> statements = null)
        {
            var retVal = new StatementList();
            if (statements != null)
                retVal.Statements.AddRange(statements);
            return retVal;
        }
        
        public static StatementList List(params TSqlStatement[] statements)
        {
            var retVal = new StatementList();
            if (statements != null)
                retVal.Statements.AddRange(statements);
            return retVal;
        }
        
        public static MethodSpecifier MethodSpecifier(Identifier assemblyName, Identifier className, Identifier methodName)
        {
            var retVal = new MethodSpecifier();
            if (assemblyName != null)
                retVal.AssemblyName = assemblyName;
            if (className != null)
                retVal.ClassName = className;
            if (methodName != null)
                retVal.MethodName = methodName;
            return retVal;
        }
        
        public static MethodSpecifier MethodSpecifier(string assemblyName, string className, string methodName, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new MethodSpecifier();
            retVal.AssemblyName = Identifier(assemblyName, quoteType);
            retVal.ClassName = Identifier(className, quoteType);
            retVal.MethodName = Identifier(methodName, quoteType);
            return retVal;
        }
        
        public static ProcedureOption ProcedureOption(ProcedureOptionKind optionKind = ProcedureOptionKind.Encryption)
        {
            var retVal = new ProcedureOption();
            retVal.OptionKind = optionKind;
            return retVal;
        }
        
        public static BeginEndBlockStatement BeginEndBlock(StatementList statementList = null)
        {
            var retVal = new BeginEndBlockStatement();
            if (statementList != null)
                retVal.StatementList = statementList;
            return retVal;
        }
        
        public static PredicateSetStatement PredicateSet(bool isOn, SetOptions options = SetOptions.None)
        {
            var retVal = new PredicateSetStatement();
            retVal.IsOn = isOn;
            retVal.Options = options;
            return retVal;
        }
        
        public static ReturnStatement Return(ScalarExpression expression = null)
        {
            var retVal = new ReturnStatement();
            if (expression != null)
                retVal.Expression = expression;
            return retVal;
        }
        
        public static TopRowFilter TopRowFilter(bool percent, bool withTies, ScalarExpression expression = null)
        {
            var retVal = new TopRowFilter();
            retVal.Percent = percent;
            retVal.WithTies = withTies;
            if (expression != null)
                retVal.Expression = expression;
            return retVal;
        }
        
        public static OutputIntoClause OutputIntoClause(TableReference intoTable = null, IEnumerable<SelectElement> selectColumns = null, IEnumerable<ColumnReferenceExpression> intoTableColumns = null)
        {
            var retVal = new OutputIntoClause();
            if (intoTable != null)
                retVal.IntoTable = intoTable;
            if (selectColumns != null)
                retVal.SelectColumns.AddRange(selectColumns);
            if (intoTableColumns != null)
                retVal.IntoTableColumns.AddRange(intoTableColumns);
            return retVal;
        }
        
        public static OutputIntoClause OutputIntoClause(TableReference intoTable = null, IEnumerable<SelectElement> selectColumns = null, params ColumnReferenceExpression[] intoTableColumns)
        {
            var retVal = new OutputIntoClause();
            if (intoTable != null)
                retVal.IntoTable = intoTable;
            if (selectColumns != null)
                retVal.SelectColumns.AddRange(selectColumns);
            if (intoTableColumns != null)
                retVal.IntoTableColumns.AddRange(intoTableColumns);
            return retVal;
        }
        
        public static OutputClause OutputClause(IEnumerable<SelectElement> selectColumns = null)
        {
            var retVal = new OutputClause();
            if (selectColumns != null)
                retVal.SelectColumns.AddRange(selectColumns);
            return retVal;
        }
        
        public static OutputClause OutputClause(params SelectElement[] selectColumns)
        {
            var retVal = new OutputClause();
            if (selectColumns != null)
                retVal.SelectColumns.AddRange(selectColumns);
            return retVal;
        }
        
        public static NamedTableReference NamedTableReference(Identifier alias, SchemaObjectName schemaObject = null, TableSampleClause tableSampleClause = null, TemporalClause temporalClause = null, IEnumerable<TableHint> tableHints = null)
        {
            var retVal = new NamedTableReference();
            if (alias != null)
                retVal.Alias = alias;
            if (schemaObject != null)
                retVal.SchemaObject = schemaObject;
            if (tableSampleClause != null)
                retVal.TableSampleClause = tableSampleClause;
            if (temporalClause != null)
                retVal.TemporalClause = temporalClause;
            if (tableHints != null)
                retVal.TableHints.AddRange(tableHints);
            return retVal;
        }
        
        public static NamedTableReference NamedTableReference(string alias, SchemaObjectName schemaObject = null, TableSampleClause tableSampleClause = null, TemporalClause temporalClause = null, IEnumerable<TableHint> tableHints = null, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new NamedTableReference();
            retVal.Alias = Identifier(alias, quoteType);
            retVal.SchemaObject = schemaObject;
            retVal.TableSampleClause = tableSampleClause;
            retVal.TemporalClause = temporalClause;
            retVal.TableHints.AddRange(tableHints);
            return retVal;
        }
        
        public static NamedTableReference NamedTableReference(Identifier alias, SchemaObjectName schemaObject = null, TableSampleClause tableSampleClause = null, TemporalClause temporalClause = null, params TableHint[] tableHints)
        {
            var retVal = new NamedTableReference();
            if (alias != null)
                retVal.Alias = alias;
            if (schemaObject != null)
                retVal.SchemaObject = schemaObject;
            if (tableSampleClause != null)
                retVal.TableSampleClause = tableSampleClause;
            if (temporalClause != null)
                retVal.TemporalClause = temporalClause;
            if (tableHints != null)
                retVal.TableHints.AddRange(tableHints);
            return retVal;
        }
        
        public static ExecuteInsertSource ExecuteInsertSource(ExecuteSpecification execute = null)
        {
            var retVal = new ExecuteInsertSource();
            if (execute != null)
                retVal.Execute = execute;
            return retVal;
        }
        
        public static SelectInsertSource SelectInsertSource(QueryExpression select = null)
        {
            var retVal = new SelectInsertSource();
            if (select != null)
                retVal.Select = select;
            return retVal;
        }
        
        public static ValuesInsertSource ValuesInsertSource(bool isDefaultValues, IEnumerable<RowValue> rowValues = null)
        {
            var retVal = new ValuesInsertSource();
            retVal.IsDefaultValues = isDefaultValues;
            if (rowValues != null)
                retVal.RowValues.AddRange(rowValues);
            return retVal;
        }
        
        public static ValuesInsertSource ValuesInsertSource(bool isDefaultValues, params RowValue[] rowValues)
        {
            var retVal = new ValuesInsertSource();
            retVal.IsDefaultValues = isDefaultValues;
            if (rowValues != null)
                retVal.RowValues.AddRange(rowValues);
            return retVal;
        }
        
        public static RowValue RowValue(IEnumerable<ScalarExpression> columnValues = null)
        {
            var retVal = new RowValue();
            if (columnValues != null)
                retVal.ColumnValues.AddRange(columnValues);
            return retVal;
        }
        
        public static RowValue RowValue(params ScalarExpression[] columnValues)
        {
            var retVal = new RowValue();
            if (columnValues != null)
                retVal.ColumnValues.AddRange(columnValues);
            return retVal;
        }
        
        public static Identifier Identifier(string value, QuoteType quoteType = QuoteType.NotQuoted)
        {
            var retVal = new Identifier();
            if (value != null)
                retVal.Value = value;
            retVal.QuoteType = quoteType;
            return retVal;
        }


    }
}
