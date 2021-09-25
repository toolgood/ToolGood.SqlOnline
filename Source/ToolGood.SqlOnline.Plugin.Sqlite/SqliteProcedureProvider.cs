/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.Sqlite
{
    public class SqliteProcedureProvider : ISqlProcedureProvider
    {
        private readonly ISqlProvider _provider;
        public SqliteProcedureProvider()
        {
            _provider = new SqliteProvider();
        }

        public List<ProcedureEntity> GetProcedures(string connStr, string databaseName)
        {
            return new List<ProcedureEntity>();
        }

        public List<ProcedureParamEntity> GetProcedureParams(string connStr, string databaseName, string schemaName, string procedureName)
        {
            return new List<ProcedureParamEntity>();
        }



        public ProcedureEntity GetProcedure(string connStr, string databaseName, string schemaName, string procedureName)
        {
            return new ProcedureEntity();
        }

        public SqlEditorDto GetExecuteSql(string connStr, string databaseName, string schemaName, string procedureName)
        {
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = "" };
        }

        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string procedureName)
        {
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = "" };
        }

        public SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string procedureName)
        {
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = "" };
        }



    }
}
