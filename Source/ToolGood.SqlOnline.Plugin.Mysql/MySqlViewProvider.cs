/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.Mysql
{
    public class MySqlViewProvider : ISqlViewProvider
    {
        private readonly ISqlProvider _provider;
        public MySqlViewProvider()
        {
            _provider = new MySqlProvider();
        }
        public List<ViewEntity> GetViews(string connStr, string databaseName)
        {
            const string sql = @"SELECT table_schema `DataBaseName`,table_name `ViewName`,table_comment `Comment`
FROM information_schema.TABLES
where table_schema = @0 and TABLE_TYPE = 'VIEW'
ORDER BY table_name";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            var result = helper.Select<ViewEntity>(sql, databaseName);
            helper.Dispose();
            return result;
        }

        public List<ViewColumnEntity> GetViewColumns(string connStr, string databaseName, string schemaName, string viewName)
        {
            const string sql = @"SELECT DISTINCT a.table_schema `DataBaseName`,a.table_name `Name`,a.table_comment `Comment`
,b.ORDINAL_POSITION `Index`,b.COLUMN_NAME `ColumnName`,CASE WHEN b.IS_NULLABLE='YES' then 1 else 0 end `IsNullAble`
,CASE WHEN b.COLUMN_KEY='PRI' then 1 else 0 end IsPrimaryKey,case WHEN b.EXTRA='auto_increment' then 1 else 0 end `IsIdentity`
,b.COLUMN_DEFAULT `DefaultValue`,b.DATA_TYPE `Type`,b.COLUMN_COMMENT `ColumnComment`,b.CHARACTER_MAXIMUM_LENGTH `Length`
,b.NUMERIC_PRECISION `Precision`,b.NUMERIC_SCALE `Scale`,a.TABLE_TYPE `TableType`
FROM information_schema.TABLES a
INNER JOIN information_schema.COLUMNS b ON a.table_name = b.TABLE_NAME and a.table_schema=b.TABLE_SCHEMA
WHERE a.table_schema = @0 and TABLE_TYPE='VIEW' and a.table_name=@1
ORDER BY DataBaseName,TableType,Name,`Index` ";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            var list = helper.Select<ViewColumnEntity>(sql, databaseName, viewName);
            helper.Dispose();
            foreach (var item in list) {
                if (item.Type.Contains("(")) {
                    item.Type = item.Type.Substring(0, item.Type.IndexOf('('));
                }
            }
            return list;
        }


        public ViewEntity GetView(string connStr, string databaseName, string schemaName, string viewName)
        {
            const string sql = @"SELECT table_schema `DataBaseName`,table_name `ViewName`,table_comment `Comment`
FROM information_schema.TABLES
where table_schema = @0 and TABLE_TYPE = 'VIEW' and table_name=@1
ORDER BY table_name";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            var view = helper.FirstOrDefault<ViewEntity>(sql, databaseName, viewName);
            helper.Dispose();
            view.Columns = GetViewColumns(connStr, databaseName, schemaName, viewName);
            return view;
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
                var columnName = (i > 0 ? "," : "") + "`" + column.ColumnName + "`";
                stringBuilder.Append(columnName);
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendFormat("FROM `{0}`.`{1}`\r\n", databaseName, viewName);
            stringBuilder.AppendLine();
            stringBuilder.Append("LIMIT 100");

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string viewName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE `{0}`;\r\n\r\n", databaseName);
            var sql = GetViewDefinition(connStr, databaseName, schemaName, viewName);
            stringBuilder.Append(sql);
            stringBuilder.AppendLine();


            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }
        public SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string viewName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE `{0}`;\r\n\r\n", databaseName);
            var sql = GetViewDefinition(connStr, databaseName, schemaName, viewName);
            sql= Regex.Replace(sql, @"\bCREATE\b", "ALTER",RegexOptions.IgnoreCase);
            stringBuilder.Append(sql);
            stringBuilder.AppendLine();

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };

        }
        public string GetViewDefinition(string connStr, string dataBaseName, string schemaName, string viewName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            var sql = $"show create view {dataBaseName}.{viewName};";
            var dt = helper.ExecuteDataTable(sql);
            dt.Dispose();
            if (dt.Rows.Count > 0) {
                return dt.Rows[0]["Create View"].ToString();
            }
            return null;
        }
    }


}
