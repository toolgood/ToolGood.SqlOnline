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
    public class SqliteFunctionProvider : ISqlFunctionProvider
    {
        private readonly ISqlProvider _provider;
        public SqliteFunctionProvider()
        {
            _provider = new SqliteProvider();
        }

        public List<FunctionEntity> GetFunctions(string connStr, string databaseName)
        {
            return new List<FunctionEntity>();
        }

        public List<FunctionParamEntity> GetFunctionParams(string connStr, string databaseName, string schemaName, string functionName)
        {
            return new List<FunctionParamEntity>();
        }

        public FunctionEntity GetFunction(string connStr, string databaseName, string schemaName, string functionName)
        {
            return new FunctionEntity();
        }

        public SqlEditorDto GetSelectSql(string connStr, string databaseName, string schemaName, string functionName)
        {
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = "" };
        }

        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string functionName)
        {
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = "" };
        }

        public SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string functionName)
        {
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = "" };
        }

    }
}
