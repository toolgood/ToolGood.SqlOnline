/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.Sqlite
{
    public class SqliteCommonProvider : ExecuteProviderBase, ISqlCommonProvider
    {
        public SqliteCommonProvider()
        {
            _provider = new SqliteProvider();
        }

        public List<DatabaseEntity> GetDatabases(string connStr)
        {
            return new List<DatabaseEntity>() {
                new DatabaseEntity(){DatabaseName="main" }
            };
        }
        public List<TableEntity> GetTables(string connStr, string databaseName)
        {
            const string sql = "select name from sqlite_master where type='table' and name<>'sqlite_sequence' ORDER BY name ";
            List<TableEntity> list = new List<TableEntity>();

            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SQLite);
            var dt = helper.ExecuteDataTable(sql);
            helper.Dispose();
            foreach (DataRow item in dt.Rows) {
                var entity = new TableEntity() {
                    DatabaseName = "main",
                    TableName = item["name"].ToString()
                };
                list.Add(entity);
            }
            return list;
        }
        public List<TableColumnEntity> GetTableColumns(string connStr, string databaseName, string schemaName, string tableName)
        {
            const string sql = "select name from sqlite_master where type='table' and name<>'sqlite_sequence' ";

            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SQLite);
            List<TableColumnEntity> list = new List<TableColumnEntity>();
            if (tableName == null) {
                var dt = helper.ExecuteDataTable(sql);
                foreach (DataRow row in dt.Rows) {
                    var tName = row["name"].ToString();
                    var dt2 = helper.ExecuteDataTable($"PRAGMA table_info({tName})");
                    AddToTables(dt2, tName, list);
                }
            } else {
                var dt = helper.ExecuteDataTable($"PRAGMA table_info({tableName})");
                AddToTables(dt, tableName, list);
            }
            helper.Dispose();
            return list;
        }
        private void AddToTables(DataTable dataTable, string tableName, List<TableColumnEntity> tables)
        {
            foreach (DataRow row in dataTable.Rows) {
                var entity = new TableColumnEntity() {
                    TableName = tableName,
                    ColumnName = row["name"].ToString(),
                    Type = row["type"].ToString(),
                    IsNullAble = row["notnull"].ToString() == "0",
                    DefaultValue = row["dflt_value"]?.ToString(),
                    IsPrimaryKey = row["pk"].ToString() == "1"
                };

                tables.Add(entity);
            }
        }



        public TableEntity GetTable(string connStr, string databaseName, string schemaName, string tableName)
        {
            TableEntity result = new TableEntity();
            result.DatabaseName = databaseName;
            result.TableName = tableName;
            result.Columns = GetTableColumns(connStr, databaseName, schemaName, tableName);
            return result;
        }

        public List<TableShowEntity> GetTableShowList(string connStr, string databaseName)
        {
            const string tablesql = "select name from sqlite_master where type='table' and name<>'sqlite_sequence' and name<>'oviki_comment' ORDER BY name ";
            const string viewsql = "select name from sqlite_master where type='view' ORDER BY name ";
            //const string viewsql = @"SELECT name FROM sqlite_master WHERE  type = 'view'";

            List<TableShowEntity> list = new List<TableShowEntity>();

            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SQLite);
            var dt = helper.ExecuteDataTable(tablesql);
            foreach (DataRow item in dt.Rows) {
                var dt2 = helper.ExecuteDataTable($"PRAGMA table_info({item["name"].ToString()})");
                foreach (DataRow row in dt2.Rows) {
                    var entity = new TableShowEntity() {
                        TableType = "t",
                        Name = item["name"].ToString(),
                        ColumnName = row["name"].ToString(),
                        Type = row["type"].ToString(),
                        IsNullAble = row["notnull"].ToString() == "0",
                        DefaultValue = row["dflt_value"]?.ToString(),
                        IsPrimaryKey = row["pk"].ToString() == "1",
                    };
                    list.Add(entity);
                }
            }
            dt = helper.ExecuteDataTable(viewsql);
            helper.Dispose();
            foreach (DataRow item in dt.Rows) {
                var dt2 = helper.ExecuteDataTable($"PRAGMA table_info({item["name"].ToString()})");
                foreach (DataRow row in dt2.Rows) {
                    list.Add(new TableShowEntity() {
                        TableType = "v",
                        Name = item["name"].ToString(),
                        ColumnName = row["name"].ToString(),
                        Type = row["type"].ToString(),
                        IsNullAble = row["notnull"].ToString() == "0",
                        DefaultValue = row["dflt_value"]?.ToString(),
                        IsPrimaryKey = row["pk"].ToString() == "1",
                    });
                }
            }
            return list;
        }

        public SqlEditorDto GetNew()
        {
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName() };
        }

        public SqlEditorDto GetSelect100(string connStr, string databaseName, string schemaName, string tableName)
        {
            var tableColumns = GetTableColumns(connStr, databaseName, schemaName, tableName);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("SELECT ");
            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                stringBuilder.Append('\t');
                if (i > 0) {
                    stringBuilder.Append(',');
                }
                stringBuilder.Append("[" + column.ColumnName + "]");
                stringBuilder.Append('\t');
                if (string.IsNullOrWhiteSpace(column.Comment) == false) {
                    stringBuilder.Append("-- ");
                    stringBuilder.Append(column.Comment);
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine($"FROM [{tableName}] ");
            //stringBuilder.AppendLine("WHERE 1=1");

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.Append($"LIMIT 100 ");
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetInsertSql(string connStr, string databaseName, string schemaName, string tableName)
        {
            var tableColumns = GetTableColumns(connStr, databaseName, schemaName, tableName);
            tableColumns.RemoveAll(q => q.IsIdentity);

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("INSERT INTO [{0}](\r\n", tableName);
            var maxLength = tableColumns.Max(q => q.ColumnName.Length) + 10;
            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                stringBuilder.Append("\t");

                var columnName = (i > 0 ? "," : "") + "[" + column.ColumnName + "]";
                if (string.IsNullOrEmpty(column.Comment) == false) {
                    stringBuilder.Append(columnName.PadRight(maxLength));
                    stringBuilder.Append($" -- '{column.Comment}' ");
                } else {
                    stringBuilder.Append(columnName);
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendFormat(") VALUES (\r\n", tableName);
            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                stringBuilder.Append("    ");
                stringBuilder.Append(i > 0 ? "," : " ");

                var columnName = $"'{column.GetColumnType()}'";
                stringBuilder.Append(columnName.PadRight(maxLength));
                stringBuilder.Append($"\t-- {column.ColumnName.PadRight(20)} {column.Comment} ");
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendFormat(")");
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }
        public SqlEditorDto GetUpdateSql(string connStr, string databaseName, string schemaName, string tableName)
        {
            var tableColumns = GetTableColumns(connStr, databaseName, schemaName, tableName);
            tableColumns.RemoveAll(q => q.IsIdentity);
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendFormat("UPDATE [{0}] SET \r\n", tableName);
            var maxLength = tableColumns.Max(q => q.ColumnName.Length) + 10;
            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                stringBuilder.Append('\t');
                var columnName = $"[{column.ColumnName}]";
                stringBuilder.Append(columnName.PadRight(maxLength));
                stringBuilder.Append($"= '{column.GetColumnType()}'".PadRight(15));
                if (i < tableColumns.Count - 1) {
                    stringBuilder.Append(',');
                }
                if (string.IsNullOrEmpty(column.Comment) == false) {
                    stringBuilder.Append($"\t -- '{column.Comment}' ");
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.Append("WHERE <where condition>");
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }
        public SqlEditorDto GetDeleteSql(string connStr, string databaseName, string schemaName, string tableName)
        {
            var sql = $"DELETE FROM [{tableName}] WHERE <where condition> \r\n";
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = sql };
        }
        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string tableName)
        {
            const string sql = @"SELECT sql FROM sqlite_master WHERE name = @0 and type = 'table'";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SQLite);
            var sql2 = helper.FirstOrDefault<string>(sql, tableName);
            helper.Dispose();
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = sql2 };
        }


        public bool ChangeComment(string connStr, string databaseName, string schemaName, string tableName, string comment)
        {
            return false;
        }

        public bool ChangeComment(string connStr, string databaseName, string schemaName, string tableName, string columnName, string comment)
        {
            return false;
        }


        public override DbCommand CreateCommand(string connStr, string databaseName)
        {
            var factory = _provider.GetProviderFactory();
            var conn = factory.CreateConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            DbCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            return cmd;
        }



    }
}
