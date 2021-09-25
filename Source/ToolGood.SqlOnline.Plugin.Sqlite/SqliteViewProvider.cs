/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.Sqlite
{
    public class SqliteViewProvider : ISqlViewProvider
    {
        private readonly ISqlProvider _provider;
        public SqliteViewProvider()
        {
            _provider = new SqliteProvider();
        }

        public List<ViewEntity> GetViews(string connStr, string databaseName)
        {
            const string sql = "select name from sqlite_master where type='view' ORDER BY name ";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SQLite);
            var dt = helper.ExecuteDataTable(sql);
            List<ViewEntity> list = new List<ViewEntity>();
            foreach (DataRow item in dt.Rows) {
                list.Add(new ViewEntity() {
                    DatabaseName = "main",
                    ViewName = item["name"].ToString()
                });
            }
            helper.Dispose();
            return list;
        }

        public List<ViewColumnEntity> GetViewColumns(string connStr, string databaseName, string schemaName, string viewName)
        {
            const string sql = "select name from sqlite_master where type='view' ORDER BY name ";

            List<ViewColumnEntity> list = new List<ViewColumnEntity>();
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SQLite);
            if (viewName == null) {
                var dt = helper.ExecuteDataTable(sql);
                foreach (DataRow row in dt.Rows) {
                    var tName = row["name"].ToString();
                    var dt2 = helper.ExecuteDataTable($"PRAGMA table_info({tName})");
                    AddToViews(dt2, tName, list);
                }
            } else {
                var dt = helper.ExecuteDataTable($"PRAGMA table_info({viewName})");
                AddToViews(dt, viewName, list);
            }
            helper.Dispose();
            return list;
        }
        private void AddToViews(DataTable dataTable, string tableName, List<ViewColumnEntity> tables)
        {
            foreach (DataRow row in dataTable.Rows) {
                tables.Add(new ViewColumnEntity() {
                    ViewName = tableName,
                    ColumnName = row["name"].ToString(),
                    Type = row["type"].ToString(),
                    IsNullAble = row["notnull"].ToString() == "0",
                });
            }
        }

        public ViewEntity GetView(string connStr, string databaseName, string schemaName, string viewName)
        {
            ViewEntity result = new ViewEntity();
            result.DatabaseName = databaseName;
            result.ViewName = viewName;
            result.Columns = GetViewColumns(connStr, databaseName, schemaName, viewName);
            return result;
        }

        public SqlEditorDto GetSelect100(string connStr, string databaseName, string schemaName, string viewName)
        {
            var viewColumns = GetViewColumns(connStr, databaseName, schemaName, viewName);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT\r\n");
            var maxLength = viewColumns.Max(q => q.ColumnName.Length) + 10;
            for (int i = 0; i < viewColumns.Count; i++) {
                var column = viewColumns[i];
                stringBuilder.Append("\t");


                var columnName = (i > 0 ? "," : "") + "[" + column.ColumnName + "]";
                if (column.ColumnName == "*") {
                    columnName = (i > 0 ? "," : "") + column.ColumnName;
                }
                if (string.IsNullOrEmpty(column.Comment) == false) {
                    stringBuilder.Append(columnName.PadRight(maxLength));
                    stringBuilder.Append($" -- '{column.Comment}' ");
                } else {
                    stringBuilder.Append(columnName);
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendFormat("FROM [{0}]\r\n", viewName);

            stringBuilder.AppendLine();
            stringBuilder.Append("LIMIT 100");

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string viewName)
        {
            var sql = GetViewDefinition(connStr, databaseName, schemaName, viewName);
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = sql };

        }
        public SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string viewName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("DROP VIEW IF EXISTS [{0}];\r\n", viewName);
            var sql = GetViewDefinition(connStr, databaseName, schemaName, viewName);
            stringBuilder.Append(sql);
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }
        public string GetViewDefinition(string connStr, string dataBaseName, string schemaName, string viewName)
        {
            const string sql = @"SELECT sql FROM sqlite_master WHERE name = @0 and type = 'view'";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SQLite);
            var sql2 = helper.FirstOrDefault<string>(sql, viewName);
            helper.Dispose();
            return sql2;
        }



    }
}
