using System.Collections.Generic;
using System.Text;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.PostgreSQL
{
    public class PostgreSQLProcedureProvider : ISqlProcedureProvider
    {
        private readonly ISqlProvider _provider;
        public PostgreSQLProcedureProvider()
        {
            _provider = new PostgreSQLProvider();
        }

        public List<ProcedureEntity> GetProcedures(string connStr, string databaseName)
        {
            var sql = @"SELECT ns.nspname AS SchemaName,p.proname AS ProcedureName,obj_description(p.oid) AS Comment,l.lanname AS Language
from pg_proc p 
LEFT JOIN pg_namespace ns ON ns.oid = p.pronamespace 
LEFT JOIN pg_language l ON l.oid = p.prolang 
WHERE p.prokind='p' and ns.nspname not in ('pg_catalog', 'pg_toast', 'information_schema')";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(databaseName);
            var entities = helper.Select<ProcedureEntity>(sql);
            helper.Dispose();
            foreach (var item in entities) {
                item.DatabaseName = databaseName;
            }
            return entities;
        }

        public ProcedureEntity GetProcedure(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var sql = @"SELECT ns.nspname AS SchemaName,p.proname AS ProcedureName,obj_description(p.oid) AS Comment,l.lanname AS Language 
from pg_proc p 
LEFT JOIN pg_namespace ns ON ns.oid = p.pronamespace 
LEFT JOIN pg_language l ON l.oid = p.prolang 
WHERE p.prokind='p' and ns.nspname=@0 and p.proname=@1 ";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(databaseName);
            var entities = helper.FirstOrDefault<ProcedureEntity>(sql, schemaName, procedureName);
            helper.Dispose();
            return entities;
        }

        public List<ProcedureParamEntity> GetProcedureParams(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var sql = @"SELECT t.oid, n.nspname, t.typname FROM pg_type t LEFT JOIN pg_namespace n ON t.typnamespace = n.oid";
            var sql2 = @"SELECT p.proargnames,p.proargtypes,p.proargmodes AS parammodes,p.proallargtypes AS paramalltypes
from pg_proc p 
LEFT JOIN pg_namespace ns ON ns.oid = p.pronamespace 
WHERE p.prokind='p' and ns.nspname=@0 and p.proname=@1";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(databaseName);
            var types = helper.Select<PostgreSQLType>(sql);
            Dictionary<uint, string> tempTypes = new Dictionary<uint, string>();
            foreach (var item in types) {
                tempTypes[(uint)item.oid] = item.typname;
            }
            types = null;
            var dt = helper.ExecuteDataTable(sql2, schemaName, procedureName);
            //var procedureParams = helper.FirstOrDefault<PostgreSQLProcedureParam>(sql2, schemaName, procedureName);
            helper.Dispose();
            if (dt.Rows.Count == 0) { return new List<ProcedureParamEntity>(); }
            var row = dt.Rows[0];
            if (((uint[])row["proargtypes"]).Length == 0) { return new List<ProcedureParamEntity>(); }

            var proargtypes = (uint[])row["proargtypes"];
            var proargnames = (string[])row["proargnames"];
            if (row["paramalltypes"] is not System.DBNull) {
                proargtypes = (uint[])row["paramalltypes"];
            }
            char[] parammodes = null;
            if (row["parammodes"] is not System.DBNull) {
                parammodes = (char[])row["parammodes"];
            }


            List<ProcedureParamEntity> result = new List<ProcedureParamEntity>();
            for (int i = 0; i < proargnames.Length; i++) {
                var name = proargnames[i];
                var typeId = proargtypes[i];
                var type = tempTypes[typeId];
                bool isOutput = false;
                if (parammodes!=null) {
                    var t = parammodes[i];
                    if (t == 'o') {
                        isOutput = true;
                    }
                }
                ProcedureParamEntity entity = new ProcedureParamEntity() {
                    DatabaseName = databaseName,
                    SchemaName = schemaName,
                    ProcedureName = procedureName,
                    Type = type,
                    ParamName = name,
                    IsOutput = isOutput
                };
                result.Add(entity);
            }
            return result;

        }

        public SqlEditorDto GetExecuteSql(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var procedureParams = GetProcedureParams(connStr, databaseName, schemaName, procedureName);
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("Call \"{0}\".\"{1}\"(", schemaName, procedureName);

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

        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var procedure = GetProcedure(connStr, databaseName, schemaName, procedureName);
            var procedureParams = GetProcedureParams(connStr, databaseName, schemaName, procedureName);
            var definition = GetDefinition(connStr, databaseName, schemaName, procedureName);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("CREATE PROCEDURE \"{0}\".\"{1}\"(", schemaName, procedureName);

            for (int i = 0; i < procedureParams.Count; i++) {
                if (i > 0) { stringBuilder.Append(","); }
                var param = procedureParams[i];
                if (param.IsOutput) {
                    stringBuilder.Append("OUT ");
                }
                stringBuilder.AppendFormat("\"{0}\" {1}", param.ParamName, param.Type);
            }
            stringBuilder.Append(")\r\nAS $BODY$ ");
            stringBuilder.Append(definition);
            stringBuilder.Append("$BODY$\r\n");
            stringBuilder.Append("LANGUAGE ");
            stringBuilder.Append(procedure.Language);
            stringBuilder.Append(";");
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var procedure = GetProcedure(connStr, databaseName, schemaName, procedureName);
            var procedureParams = GetProcedureParams(connStr, databaseName, schemaName, procedureName);
            var definition = GetDefinition(connStr, databaseName, schemaName, procedureName);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("CREATE OR REPLACE PROCEDURE \"{0}\".\"{1}\"(", schemaName, procedureName);

            for (int i = 0; i < procedureParams.Count; i++) {
                if (i > 0) { stringBuilder.Append(","); }
                var param = procedureParams[i];
                if (param.IsOutput) {
                    stringBuilder.Append("OUT ");
                }
                stringBuilder.AppendFormat("\"{0}\" {1}", param.ParamName, param.Type);
            }
            stringBuilder.Append(")\r\nAS $BODY$ ");
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
WHERE p.prokind='p' and ns.nspname=@0 and p.proname=@1";
            var dt = helper.ExecuteDataTable(sql, schemaName, viewName);
            dt.Dispose();
            if (dt.Rows.Count > 0) {
                return dt.Rows[0]["definition"].ToString();
            }
            return null;
        }

    }



}
