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
    public class MySqlProcedureProvider : ISqlProcedureProvider
    {
        private readonly ISqlProvider _provider;
        public MySqlProcedureProvider()
        {
            _provider = new MySqlProvider();
        }

        public List<ProcedureEntity> GetProcedures(string connStr, string databaseName)
        {
            var sql = $"SHOW PROCEDURE STATUS WHERE Db='{databaseName}'";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            var dt = helper.ExecuteDataTable(sql);
            helper.Dispose();
            List<ProcedureEntity> procedureEntities = new List<ProcedureEntity>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                var row = dt.Rows[i];
                var entity = new ProcedureEntity {
                    DatabaseName = row["Db"].ToString(),
                    ProcedureName = row["Name"].ToString(),
                    Comment = row["Comment"].ToString(),
                };
                procedureEntities.Add(entity);
            }
            return procedureEntities;
        }

        public List<ProcedureParamEntity> GetProcedureParams(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var sql = "SELECT * FROM information_schema.PARAMETERS where parameter_name is not null and SPECIFIC_SCHEMA=@0 ";
            if (string.IsNullOrEmpty(procedureName) == false) {
                sql += "and SPECIFIC_NAME=@1 ";
            }
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            var dt = helper.ExecuteDataTable(sql, databaseName, procedureName);
            helper.Dispose();
            List<ProcedureParamEntity> procedureEntities = new List<ProcedureParamEntity>();
            for (int i = 0; i < dt.Rows.Count; i++) {
                var row = dt.Rows[i];
                var entity = new ProcedureParamEntity {
                    DatabaseName = databaseName,
                    ProcedureName = row["SPECIFIC_NAME"].ToString(),
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



        public ProcedureEntity GetProcedure(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var sql = $"SHOW PROCEDURE STATUS WHERE Db='{databaseName}' and Name='{procedureName}'";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            var dt = helper.ExecuteDataTable(sql);
            helper.Dispose();
            ProcedureEntity result = new ProcedureEntity();
            result.DatabaseName = databaseName;
            result.ProcedureName = procedureName;
            result.Comment = dt.Rows[0]["Comment"].ToString();
            result.Params = GetProcedureParams(connStr, databaseName, schemaName, procedureName);
            return result;

        }

        public SqlEditorDto GetExecuteSql(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var procedureParams = GetProcedureParams(connStr, databaseName, schemaName, procedureName);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE `{0}`;\r\n\r\n", databaseName);

            stringBuilder.AppendFormat("Call `{0}`(", procedureName);

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
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE `{0}`;\r\n\r\n", databaseName);

            stringBuilder.AppendFormat("delimiter $$\r\n\r\n", procedureName);
            var sql = GetProcedureDefinition(connStr, databaseName, schemaName, procedureName);
            stringBuilder.Append(sql);
            stringBuilder.Append(" $$");
            stringBuilder.AppendFormat("\r\ndelimiter ;\r\n", procedureName);

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string procedureName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE `{0}`;\r\n\r\n", databaseName);

            stringBuilder.AppendFormat("DROP PROCEDURE IF EXISTS `{0}`;\r\n\r\n", procedureName);
            stringBuilder.AppendFormat("delimiter $$\r\n\r\n", procedureName);

            var sql = GetProcedureDefinition(connStr, databaseName, schemaName, procedureName);
            stringBuilder.Append(sql);
            stringBuilder.Append(" $$");
            stringBuilder.AppendFormat("\r\ndelimiter ;\r\n", procedureName);


            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }
        public string GetProcedureDefinition(string connStr, string dataBaseName, string schemaName, string procedureName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            try {
                var sql2 = $"SHOW CREATE PROCEDURE `{dataBaseName}`.`{procedureName}`";
                var dt2 = helper.ExecuteDataTable(sql2);
                if (dt2.Rows.Count > 0) {
                    return dt2.Rows[0]["Create Procedure"].ToString();
                }
            } catch (System.Exception) {
            } finally {
                helper.Dispose();
            }
            return null;
        }


    }
}
