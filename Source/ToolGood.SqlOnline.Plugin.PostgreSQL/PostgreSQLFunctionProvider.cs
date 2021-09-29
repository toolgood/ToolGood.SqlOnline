/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Text;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.PostgreSQL
{
    public class PostgreSQLFunctionProvider : ISqlFunctionProvider
    {
        private readonly ISqlProvider _provider;
        public PostgreSQLFunctionProvider()
        {
            _provider = new PostgreSQLProvider();
        }

        public List<FunctionEntity> GetFunctions(string connStr, string databaseName)
        {
            var sql = @"SELECT ns.nspname AS SchemaName,p.proname AS FunctionName,obj_description(p.oid) AS Comment,l.lanname AS Language
,typ.typname AS ReturnType
from pg_proc p 
LEFT JOIN pg_type typ ON typ.oid = p.prorettype 
LEFT JOIN pg_namespace ns ON ns.oid = p.pronamespace 
LEFT JOIN pg_language l ON l.oid = p.prolang 
WHERE p.prokind='f' and ns.nspname not in ('pg_catalog', 'pg_toast', 'information_schema')";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(databaseName);
            var entities = helper.Select<FunctionEntity>(sql);
            helper.Dispose();
            foreach (var item in entities) {
                item.DatabaseName = databaseName;
            }
            return entities;
        }

        public FunctionEntity GetFunction(string connStr, string databaseName, string schemaName, string functionName)
        {
            var sql = @"SELECT ns.nspname AS SchemaName,p.proname AS FunctionName,obj_description(p.oid) AS Comment,l.lanname AS Language 
,typ.typname AS ReturnType
from pg_proc p 
LEFT JOIN pg_type typ ON typ.oid = p.prorettype 
LEFT JOIN pg_namespace ns ON ns.oid = p.pronamespace 
LEFT JOIN pg_language l ON l.oid = p.prolang 
WHERE p.prokind='f' and ns.nspname=@0 and p.proname=@1 ";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(databaseName);
            var entities = helper.FirstOrDefault<FunctionEntity>(sql, schemaName, functionName);
            helper.Dispose();
            return entities;
        }

        public List<FunctionParamEntity> GetFunctionParams(string connStr, string databaseName, string schemaName, string functionName)
        {
            var sql = @"SELECT t.oid, n.nspname, t.typname FROM pg_type t LEFT JOIN pg_namespace n ON t.typnamespace = n.oid";
            var sql2 = @"SELECT p.proargnames,p.proargtypes,p.proargmodes AS parammodes,p.proallargtypes AS paramalltypes
from pg_proc p 
LEFT JOIN pg_namespace ns ON ns.oid = p.pronamespace 
WHERE p.prokind='f' and ns.nspname=@0 and p.proname=@1";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(databaseName);
            var types = helper.Select<PostgreSQLType>(sql);
            Dictionary<uint, string> tempTypes = new Dictionary<uint, string>();
            foreach (var item in types) {
                tempTypes[(uint)item.oid] = item.typname;
            }
            types = null;
            var dt = helper.ExecuteDataTable(sql2, schemaName, functionName);
            helper.Dispose();
            if (dt.Rows.Count == 0) { return new List<FunctionParamEntity>(); }
            var row = dt.Rows[0];
            if (((uint[])row["proargtypes"]).Length == 0) { return new List<FunctionParamEntity>(); }

            var proargtypes = (uint[])row["proargtypes"];
            var proargnames = (string[])row["proargnames"];
            if (row["paramalltypes"] is not System.DBNull) {
                proargtypes = (uint[])row["paramalltypes"];
            }
            char[] parammodes = null;
            if (row["parammodes"] is not System.DBNull) {
                parammodes = (char[])row["parammodes"];
            }

            List<FunctionParamEntity> result = new List<FunctionParamEntity>();
            for (int i = 0; i < proargnames.Length; i++) {
                var name = proargnames[i];
                var typeId = proargtypes[i];
                var type = tempTypes[typeId];
                bool isOutput = false;
                if (parammodes != null) {
                    var t = parammodes[i];
                    if (t == 'o') {
                        isOutput = true;
                    }
                }
                FunctionParamEntity entity = new FunctionParamEntity() {
                    DatabaseName = databaseName,
                    SchemaName = schemaName,
                    FunctionName = functionName,
                    Type = type,
                    ParamName = name,
                    IsOutput = isOutput
                };
                result.Add(entity);
            }
            return result;
        }

        public SqlEditorDto GetSelectSql(string connStr, string databaseName, string schemaName, string functionName)
        {
            var procedureParams = GetFunctionParams(connStr, databaseName, schemaName, functionName);
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("SELECT \"{0}\".\"{1}\"(", schemaName, functionName);

            for (int i = 0; i < procedureParams.Count; i++) {
                var column = procedureParams[i];
                if (i > 0) {
                    stringBuilder.Append(" , ");
                }
                stringBuilder.Append($"<{column.ParamName} {column.GetColumnType()}>");
            }
            stringBuilder.Append(")");

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string functionName)
        {
            var procedure = GetFunction(connStr, databaseName, schemaName, functionName);
            var procedureParams = GetFunctionParams(connStr, databaseName, schemaName, functionName);
            var definition = GetDefinition(connStr, databaseName, schemaName, functionName);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("CREATE FUNCTION \"{0}\".\"{1}\"(", schemaName, functionName);

            for (int i = 0; i < procedureParams.Count; i++) {
                if (i > 0) { stringBuilder.Append(","); }
                var param = procedureParams[i];
                if (param.IsOutput) {
                    stringBuilder.Append("OUT ");
                }
                stringBuilder.AppendFormat("\"{0}\" {1}", param.ParamName, param.Type);
            }
            stringBuilder.Append($")\r\nRETURNS \"{procedure.ReturnType}\" AS $BODY$ ");
            stringBuilder.Append(definition);
            stringBuilder.Append("$BODY$\r\n");
            stringBuilder.Append("LANGUAGE ");
            stringBuilder.Append(procedure.Language);
            stringBuilder.Append(";");
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string functionName)
        {
            var procedure = GetFunction(connStr, databaseName, schemaName, functionName);
            var procedureParams = GetFunctionParams(connStr, databaseName, schemaName, functionName);
            var definition = GetDefinition(connStr, databaseName, schemaName, functionName);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("CREATE OR REPLACE FUNCTION \"{0}\".\"{1}\"(", schemaName, functionName);

            for (int i = 0; i < procedureParams.Count; i++) {
                if (i > 0) { stringBuilder.Append(","); }
                var param = procedureParams[i];
                if (param.IsOutput) {
                    stringBuilder.Append("OUT ");
                }
                stringBuilder.AppendFormat("\"{0}\" {1}", param.ParamName, param.Type);
            }
            stringBuilder.Append($")\r\nRETURNS \"{procedure.ReturnType}\" AS $BODY$ ");
            stringBuilder.Append(definition);
            stringBuilder.Append("$BODY$\r\n");
            stringBuilder.Append("LANGUAGE ");
            stringBuilder.Append(procedure.Language);
            stringBuilder.Append(";");
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }


        public string GetDefinition(string connStr, string dataBaseName, string schemaName, string viewName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(dataBaseName);
            var sql = @"SELECT p.prosrc AS definition
from pg_proc p 
LEFT JOIN pg_namespace ns ON ns.oid = p.pronamespace 
WHERE p.prokind='f' and ns.nspname=@0 and p.proname=@1";
            var dt = helper.ExecuteDataTable(sql, schemaName, viewName);
            dt.Dispose();
            if (dt.Rows.Count > 0) {
                return dt.Rows[0]["definition"].ToString();
            }
            return null;
        }

    }



}
