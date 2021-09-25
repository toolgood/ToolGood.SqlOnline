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

namespace ToolGood.SqlOnline.Plugin.Mysql
{
    public class MySqlFunctionProvider : ISqlFunctionProvider
    {
        private readonly ISqlProvider _provider;
        public MySqlFunctionProvider()
        {
            _provider = new MySqlProvider();
        }

        public List<FunctionEntity> GetFunctions(string connStr, string databaseName)
        {
            var sql = $"SHOW FUNCTION STATUS WHERE Db='{databaseName}'";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            var dt = helper.ExecuteDataTable(sql);
            helper.Dispose();
            List<FunctionEntity> functionEntities = new List<FunctionEntity>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                var row = dt.Rows[i];
                var entity = new FunctionEntity {
                    DatabaseName = row["Db"].ToString(),
                    FunctionName = row["Name"].ToString(),
                    Comment = row["Comment"].ToString(),
                };
                functionEntities.Add(entity);
            }
            return functionEntities;
        }

        public List<FunctionParamEntity> GetFunctionParams(string connStr, string databaseName, string schemaName, string functionName)
        {
            var sql = "SELECT * FROM information_schema.PARAMETERS where parameter_name is not null and SPECIFIC_SCHEMA=@0 and `ROUTINE_TYPE`='FUNCTION' ";
            if (string.IsNullOrEmpty(functionName) == false) {
                sql += "and SPECIFIC_NAME=@1 ";
            }
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            var dt = helper.ExecuteDataTable(sql, databaseName, functionName);
            helper.Dispose();
            List<FunctionParamEntity> procedureEntities = new List<FunctionParamEntity>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                var row = dt.Rows[i];
                var entity = new FunctionParamEntity {
                    DatabaseName = databaseName,
                    FunctionName = row["SPECIFIC_NAME"].ToString(),
                    ParamName = row["PARAMETER_NAME"].ToString(),
                    Type = row["DATA_TYPE"].ToString(),
                    Length = row["CHARACTER_MAXIMUM_LENGTH"].ToString(),
                    Precision = row["NUMERIC_PRECISION"].ToString(),
                    Scale = row["NUMERIC_SCALE"].ToString(),
                    IsOutput = row["PARAMETER_MODE"].ToString().Contains("OUT"),
                };
                procedureEntities.Add(entity);
            }
            return procedureEntities;
        }

    

        public FunctionEntity GetFunction(string connStr, string databaseName, string schemaName, string functionName)
        {
            var sql = $"SHOW FUNCTION STATUS WHERE Db='{databaseName}' and Name='{functionName}'";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            var dt = helper.ExecuteDataTable(sql);
            helper.Dispose();
            FunctionEntity result = new FunctionEntity();
            result.DatabaseName = databaseName;
            result.FunctionName = functionName;
            result.Comment = dt.Rows[0]["Comment"].ToString();
            result.Params = GetFunctionParams(connStr, databaseName, schemaName, functionName);
            return result;
        }

        public SqlEditorDto GetSelectSql(string connStr, string databaseName, string schemaName, string functionName)
        {
            var functionParams = GetFunctionParams(connStr, databaseName, schemaName, functionName);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE `{0}`;\r\n\r\n", databaseName);
            stringBuilder.AppendFormat("SELECT `{0}`(", functionName);

            for (int i = 0; i < functionParams.Count; i++) {
                var column = functionParams[i];
                if (i>0) {
                    stringBuilder.Append(" , ");
                }
                stringBuilder.Append($"<{column.ParamName} {column.GetColumnType()}>");
            }
            stringBuilder.Append(")");

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string functionName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE `{0}`;\r\n\r\n", databaseName);

            stringBuilder.AppendFormat("delimiter $$\r\n\r\n", functionName);
            var sql = GetFunctionDefinition(connStr, databaseName, schemaName, functionName);
            stringBuilder.Append(sql);
            stringBuilder.Append(" $$");
            stringBuilder.AppendFormat("\r\ndelimiter ;\r\n", functionName);

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string functionName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE `{0}`;\r\n\r\n", databaseName);


            stringBuilder.AppendFormat("DROP PROCEDURE IF EXISTS `{0}`;\r\n\r\n", functionName);
            stringBuilder.AppendFormat("delimiter $$\r\n\r\n", functionName);

            var sql = GetFunctionDefinition(connStr, databaseName, schemaName, functionName);
            stringBuilder.Append(sql);
            stringBuilder.Append(" $$");
            stringBuilder.AppendFormat("\r\ndelimiter ;\r\n", functionName);
 
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public string GetFunctionDefinition(string connStr, string databaseName, string schemaName, string functionName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            try {
                var sql2 = $"SHOW CREATE FUNCTION `{databaseName}`.`{functionName}`";
                var dt2 = helper.ExecuteDataTable(sql2);
                if (dt2.Rows.Count > 0) {
                    return dt2.Rows[0]["Create Function"].ToString();
                }
            } catch (System.Exception) {
            } finally {
                helper.Dispose();
            }
            return null;
        }


    }
}
