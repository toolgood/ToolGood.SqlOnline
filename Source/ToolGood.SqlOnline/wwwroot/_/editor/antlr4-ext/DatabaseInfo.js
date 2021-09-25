
function ServerInfo() {
    var getDatabaseUrl = "";
    var getSchemaUrl = "";
    var getTableUrl = "";
    var getTableColumnUrl = "";
    var getProcedureUrl = "";
    var getProcedureParamUrl = "";

    this.ServerId = 0;
    this.CurrDatabase = "";
    this.Databases = {};
    this.DatabaseNames = [];



    function loadDatabases(callback) {
        $.post(getDatabaseUrl, { serverId: this.ServerId }, function (obj) { callback(obj); }, "JSON");
    }
    function loadSchemas(database, callback) {
        var databaseStr = Array.isArray(a) ? database.jion(",") : database;
        $.post(getSchemaUrl, { serverId: this.ServerId, database: databaseStr }, function (obj) { callback(obj); }, "JSON");
    }
    function loadTables(database, schema, callback) {
        $.post(getTableUrl, { serverId: this.ServerId, database: database, schema: schema }, function (obj) { callback(obj); }, "JSON");
    }
    function loadColumns(database, schema, table, callback) {
        var tableStr = Array.isArray(a) ? table.jion(",") : table;
        $.post(getTableColumnUrl, { serverId: this.ServerId, database: database, schema: schema, table: tableStr }, function (obj) { callback(obj); }, "JSON");
    }
    function loadProcedures(database, schema, callback) {
        $.post(getProcedureUrl, { serverId: this.ServerId, database: database, schema: schema }, function (obj) { callback(obj); }, "JSON");
    }
    function loadProcedureParams(database, schema, procedure, callback) {
        var procedureStr = Array.isArray(a) ? procedure.jion(",") : procedure;
        $.post(getProcedureParamUrl, { serverId: this.ServerId, database: database, schema: schema, procedure: procedureStr }, function (obj) { callback(obj); }, "JSON");
    }
    function mergeDatabase(databases) {
        var dbs = {};
        var names = [];
        for (var index = 0; index < databases.length; index++) {
            var db = databases[index];
            var dbInfo = this.Databases[db.Name];
            names.push(db.Name);
            if (dbInfo) {
                dbs[db.Name] = dbInfo;
            } else {
                var newDb = new DatabaseInfo();
                newDb.Name = db.Name;
                newDb.UseDatabase = db.UseDatabase;
                newDb.UseSchema = db.UseSchema;
                newDb.IsLoad = db.Name == this.CurrDatabase;
                dbs[db.Name] = newDb;
            }
        }
        this.Databases = dbs;
        this.DatabaseNames = names;
    }
    function mergeSchema(database, schemas) {
        var dbs = {};
        var names = [];
        for (var index = 0; index < schemas.length; index++) {
            var db = schemas[index];
            var dbInfo = database.Schemas[db.Name];
            names.push(db.Name);
            if (dbInfo) {
                dbs[db.Name] = dbInfo;
            } else {
                var newDb = new SchemaInfo();
                newDb.Name = db.Name;
                newDb.UseSchema = db.UseSchema;
                dbs[db.Name] = newDb;
            }
        }
        database.Schemas = dbs;
        database.SchemaNames = names;
        database.IsLoad = true;
    }
    function mergeTable(schema, tables) {
        var dbs = {};
        var names = [];
        for (var index = 0; index < tables.length; index++) {
            var db = tables[index];
            var dbInfo = schema.Tables[db.Name];
            names.push(db.Name);
            if (!dbInfo) {
                dbInfo = new TableInfo();
                dbInfo.Name = db.Name;
            }
            dbInfo.Comment = db.Comment;
            dbInfo.IsView = db.IsView;
            dbs[db.Name] = dbInfo;
        }
        schema.Tables = dbs;
        schema.TableNames = names;
        schema.IsLoad = true;
    }
    function mergeTableColumn(tables, columns) {
        for (var index = 0; tables < array.length; index++) {
            var table = tables[index];
            var dbs = {};

            for (let j = 0; j < columns.length; j++) {
                var column = columns[j];
                if (column.TableName == table.Name) {
                    var col = new ColInfo();
                    col.Name = column.Name;
                    col.Comment = column.Comment;
                    col.ColType = column.Type;

                    dbs[column.Name] = col;
                }
            }
            table.Columns = dbs;
            table.IsLoad = true;
        }
    }
    function mergeProcedure(schema, procedures) {
        var dbs = {};
        var names = [];
        for (var index = 0; index < procedures.length; index++) {
            var db = procedures[index];
            var dbInfo = schema.Procedures[db.Name];
            names.push(db.Name);
            if (!dbInfo) {
                dbInfo = new ProcedureInfo();
                dbInfo = db.Name;
            }
            dbInfo.Comment = db.Comment;
            dbInfo.IsFunction = db.IsFunction;
            dbInfo.ReturnType = db.ReturnType;
            dbs[db.Name] = dbInfo;
        }
        schema.Procedures = dbs;
        schema.ProcedureNames = names;
        schema.IsLoad = true;
    }
    function mergeProcedureParam(procedures, params) {
        for (var index = 0; index < procedures.length; index++) {
            var procedure = procedures[index];
            var dbs = [];
            for (let j = 0; j < params.length; j++) {
                var param = params[j];
                if (column.TableName == param.Name) {
                    var col = new ProcedureParamInfo();
                    col.Name = param.Name;
                    col.Comment = param.Comment;
                    col.ParamType = param.Type;
                    dbs[col.Name] = col;
                }
            }
            procedure.Params = dbs;
            procedure.IsLoad = true;
        }
    }

    this.init = function (serverId, currDatabase) {
        this.ServerId = serverId;
        this.CurrDatabase = currDatabase;
        this.updateDatabases();
    }
    this.updateDatabases = function () {
        // 加载 数据库名
        loadDatabases(function (databases) {
            mergeDatabase(databases);
            for (var databaseName in this.Databases) {
                if (!this.Databases.hasOwnProperty(databaseName)) { continue; }
                var database = this.Databases[databaseName];

                // 加载 Schemas
                loadSchemas(databaseName, function (schemas) {
                    mergeSchema(database, schemas);
                    for (var schemaName in database.Schemas) {
                        if (!database.Schemas.hasOwnProperty(schemaName)) { continue; }

                        var schema = database.Schemas[schemaName];
                        // 加载 表、视图
                        loadTables(databaseName, schemaName, function (tables) {
                            mergeTable(schema, tables);
                            var tableNames = schema.getLoadTableNames();
                            // 加载 表、视图 的列
                            loadColumns(databaseName, schemaName, tableNames, function (columns) {
                                mergeTableColumn(schema.Tables, columns);
                            });
                        });
                        // 加载存储过程
                        loadProcedures(databaseName, schemaName, function (procedures) {
                            mergeProcedure(schema, procedures);
                            var procedureNames = schema.getLoadProcedureNames();
                            // 加载存储过程的参数
                            loadProcedureParams(databaseName, schemaName, procedureNames, function (params) {
                                mergeProcedureParam(schema.Procedures, params);
                            })
                        })
                    }
                })
            }
        });
    }



}


function DatabaseInfo() {
    this.Name = "";
    this.Schemas = {};
    this.SchemaNames = [];

    this.UseDatabase = false;
    this.UseSchema = false;
    this.IsLoad = false;
    this.getType = function () { return "Database"; }
}

function SchemaInfo() {
    this.Name = "";
    this.Tables = {};
    this.TableNames = [];
    this.Procedures = {};
    this.ProcedureNames = [];

    this.UseSchema = false;
    this.IsLoad = false;

    this.getType = function () { return "Schema"; }
    this.getLoadTableNames = function () {
        var tableNames = [];
        for (var name in Tables) {
            if (Tables.hasOwnProperty(name)) {
                var table = Tables[name];
                if (table.IsLoad) {
                    tableNames.push(name);
                }
            }
        }
        return tableNames;
    }
    this.getLoadProcedureNames = function () {
        var procedureNames = [];
        for (var name in Procedures) {
            if (Procedures.hasOwnProperty(name)) {
                var procedure = Procedures[name];
                if (procedure.IsLoad) {
                    procedureNames.push(name);
                }
            }
        }
        return procedureNames;
    }
}

function TableInfo() {
    this.Name = "";
    this.Comment = "";
    this.Columns = {};

    this.IsView = false;
    this.IsLoad = false;
    this.getType = function () { return "Table"; }
}

function ColInfo() {
    this.Name = "";
    this.Comment = "";
    this.ColType = "";
    this.getType = function () { return "Column"; }
}

function ProcedureInfo() {
    this.Name = "";
    this.Comment = "";
    this.Params = {};
    this.ReturnType = null;
    this.IsFunction = false;

    this.IsLoad = false;
    this.getType = function () { return "Procedure"; }
}

function ProcedureParamInfo() {
    this.Name = "";
    this.Comment = "";
    this.ParamType = "";
    this.getType = function () { return "ProcedureParam"; }
}
