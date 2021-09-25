/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.Mysql
{
    public class MySqlCommonProvider : ExecuteProviderBase, ISqlCommonProvider
    {
        public MySqlCommonProvider()
        {
            _provider = new MySqlProvider();
        }

        public List<DatabaseEntity> GetDatabases(string connStr)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);

            var list = helper.Select<string>("select SCHEMA_NAME from information_schema.SCHEMATA");
            helper.Dispose();
            list.RemoveAll(q => q.ToLower() == "information_schema");
            list.RemoveAll(q => q.ToLower() == "performance_schema");
            list.RemoveAll(q => q.ToLower() == "mysql");
            list.RemoveAll(q => q.ToLower() == "sys");

            List<DatabaseEntity> databaseTrees = new List<DatabaseEntity>();
            foreach (var schema in list) {
                DatabaseEntity databases = new DatabaseEntity() { DatabaseName = schema };
                databaseTrees.Add(databases);
            }
            return databaseTrees;
        }
        public List<TableEntity> GetTables(string connStr, string databaseName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            const string s = @"SELECT TABLE_NAME as TName,TABLE_COMMENT as CComment from information_schema.`TABLES` WHERE TABLE_SCHEMA=@0 And TABLE_TYPE='BASE TABLE'";
            var schemaInfos = helper.Select<SchemaInfo>(s, databaseName);
            helper.Dispose();

            List<TableEntity> tableInfos = new List<TableEntity>();
            foreach (var item in schemaInfos) {
                TableEntity tableInfo = new TableEntity() {
                    DatabaseName = databaseName,
                    TableName = item.TName,
                    Comment = item.CComment,
                    Columns = new List<TableColumnEntity>()
                };
                tableInfos.Add(tableInfo);
            }
            return tableInfos;
        }
        public List<TableColumnEntity> GetTableColumns(string connStr, string databaseName, string schemaName, string tableName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);

            const string sql = @"SELECT TABLE_NAME as TName,COLUMN_NAME as CName,COLUMN_TYPE as CType,COLUMN_COMMENT as CComment,COLUMN_DEFAULT as CDefault,IS_NULLABLE as CIsNull
from information_schema.`COLUMNS` WHERE TABLE_SCHEMA=@0 and TABLE_NAME=@1";
            var schemaInfos2 = helper.Select<SchemaInfo>(sql, databaseName, tableName);
            helper.Dispose();

            List<TableColumnEntity> columnInfos = new List<TableColumnEntity>();
            foreach (var column in schemaInfos2) {
                TableColumnEntity columnInfo = new TableColumnEntity() {
                    ColumnName = column.CName,
                    Type = column.CType,
                    Comment = column.CComment,
                    DefaultValue = column.CDefault,
                    IsNullAble = column.CIsNull.ToUpper() == "YES"
                };
                columnInfos.Add(columnInfo);
            }
            return columnInfos;
        }

        public List<TableShowEntity> GetTableShowList(string connStr, string databaseName)
        {
            var sql = @"SELECT DISTINCT a.table_schema `DataBaseName`,a.table_name `Name`,a.table_comment `Comment`
,b.ORDINAL_POSITION `Index`,b.COLUMN_NAME `ColumnName`,CASE WHEN b.IS_NULLABLE='YES' then 1 else 0 end `IsNullAble`
,CASE WHEN b.COLUMN_KEY='PRI' then 1 else 0 end IsPrimaryKey,case WHEN b.EXTRA='auto_increment' then 1 else 0 end `IsIdentity`
,b.COLUMN_DEFAULT `DefaultValue`,b.DATA_TYPE `Type`,b.COLUMN_COMMENT `ColumnComment`,b.CHARACTER_MAXIMUM_LENGTH `Length`
,b.NUMERIC_PRECISION `Precision`,b.NUMERIC_SCALE `Scale`,a.TABLE_TYPE `TableType`
FROM information_schema.TABLES a
INNER JOIN information_schema.COLUMNS b ON a.table_name = b.TABLE_NAME and a.table_schema=b.TABLE_SCHEMA
WHERE a.table_schema = @0  
ORDER BY DataBaseName,TableType,Name,`Index` ";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            var list = helper.Select<TableShowEntity>(sql, databaseName);
            helper.Dispose();
            foreach (var item in list) {
                if (item.Type.Contains("(")) {
                    item.Type = item.Type.Substring(0, item.Type.IndexOf('('));
                }
            }
            return list;
        }

        public TableEntity GetTable(string connStr, string databaseName, string schemaName, string tableName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            const string s = @"SELECT TABLE_NAME as TName,TABLE_COMMENT as CComment from information_schema.`TABLES` WHERE TABLE_SCHEMA=@0 and TABLE_NAME=@1";
            var schemaInfo = helper.FirstOrDefault<SchemaInfo>(s, databaseName, tableName);
            helper.Dispose();
            TableEntity tableInfo = new TableEntity() {
                DatabaseName = databaseName,
                TableName = tableName,
                Comment = schemaInfo.CComment,
            };
            tableInfo.Columns = GetTableColumns(connStr, databaseName, schemaName, tableName);
            return tableInfo;
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

            var maxLength = tableColumns.Max(q => q.ColumnName.Length) + 10;
            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                stringBuilder.Append('\t');
                if (i > 0) {
                    stringBuilder.Append(',');
                }
                stringBuilder.Append($"`{column.ColumnName}`".PadRight(maxLength));
                stringBuilder.Append('\t');
                if (string.IsNullOrWhiteSpace(column.Comment) == false) {
                    stringBuilder.Append("-- ");
                    stringBuilder.Append(column.Comment.Trim());
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine($"FROM `{databaseName}`.`{tableName}` ");
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
            stringBuilder.AppendFormat("USE `{0}`;\r\n\r\n", databaseName);
            stringBuilder.AppendFormat("INSERT INTO `{0}`(\r\n", tableName);

            var maxLength = tableColumns.Max(q => q.ColumnName.Length) + 10;
            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                stringBuilder.Append("\t");

                var columnName = (i > 0 ? "," : "") + "`" + column.ColumnName + "`";
                if (string.IsNullOrEmpty(column.Comment) == false) {
                    stringBuilder.Append(columnName.PadRight(maxLength));
                    stringBuilder.Append($" -- {column.Comment.Trim()} ");
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
                stringBuilder.Append($"\t-- {column.ColumnName.PadRight(20)} {column.Comment.Trim()} ");
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
            stringBuilder.AppendFormat("USE `{0}`;\r\n\r\n", databaseName);
            stringBuilder.AppendFormat("UPDATE `{0}` SET \r\n", tableName);
            var maxLength = tableColumns.Max(q => q.ColumnName.Length) + 10;

            tableColumns.RemoveAll(q => q.IsIdentity);

            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                stringBuilder.Append('\t');
                var columnName = $"`{column.ColumnName}`";
                stringBuilder.Append(columnName.PadRight(maxLength));
                stringBuilder.Append($"= '{column.GetColumnType()}'".PadRight(15));
                if (i < tableColumns.Count - 1) {
                    stringBuilder.Append(',');
                }
                if (string.IsNullOrEmpty(column.Comment) == false) {
                    stringBuilder.Append($"\t -- '{column.Comment.Trim()}' ");
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.Append("WHERE <where condition>");
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetDeleteSql(string connStr, string databaseName, string schemaName, string tableName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE `{0}`;\r\n\r\n", databaseName);
            stringBuilder.AppendFormat("DELETE FROM `{0}` WHERE <where condition> \r\n", tableName);
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string tableName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE `{0}`;\r\n\r\n", databaseName);
            var table = GetTableDefinition(connStr, databaseName, schemaName, tableName);
            stringBuilder.Append(table);
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public string GetTableDefinition(string connStr, string dataBaseName, string schemaName, string tableName)
        {
            var sql2 = $"SHOW CREATE Table `{dataBaseName}`.`{tableName}`";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            var dt2 = helper.ExecuteDataTable(sql2);
            helper.Dispose();
            if (dt2.Rows.Count > 0) {
                return dt2.Rows[0]["Create Table"].ToString();
            }
            return null;
        }


        public bool ChangeComment(string connStr, string databaseName, string schemaName, string tableName, string comment)
        {
            if (databaseName.Contains("`")) { return false; }
            if (tableName.Contains("`")) { return false; }

            var sql = $"ALTER TABLE `{databaseName}`.`{tableName}` COMMENT = @0;";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            helper.Execute(sql, comment);
            helper.Dispose();

            return true;
        }

        public bool ChangeComment(string connStr, string databaseName, string schemaName, string tableName, string columnName, string comment)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.MySql);
            const string sql = @"SELECT TABLE_NAME as TName,COLUMN_NAME as CName,COLUMN_TYPE as CType,COLUMN_COMMENT as CComment,COLUMN_DEFAULT as CDefault,IS_NULLABLE as CIsNull, EXTRA,CHARACTER_SET_NAME,COLLATION_NAME
from information_schema.`COLUMNS` WHERE TABLE_SCHEMA=@0 and TABLE_NAME=@1 and COLUMN_NAME=@2";
            var schemaInfos2 = helper.FirstOrDefault<SchemaInfo>(sql, databaseName, tableName, columnName);

            if (schemaInfos2 != null) {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"ALTER TABLE `{databaseName}`.`{tableName}` ");
                stringBuilder.Append($"MODIFY COLUMN `{columnName}` {schemaInfos2.CType} ");
                if (string.IsNullOrEmpty(schemaInfos2.CHARACTER_SET_NAME) == false) {
                    stringBuilder.Append($"CHARACTER SET {schemaInfos2.CHARACTER_SET_NAME} ");
                }
                if (string.IsNullOrEmpty(schemaInfos2.COLLATION_NAME) == false) {
                    stringBuilder.Append($"COLLATE {schemaInfos2.COLLATION_NAME} ");
                }
                if (schemaInfos2.CIsNull.ToUpper() == "YES") {
                    stringBuilder.Append("NULL ");
                } else {
                    stringBuilder.Append("NOT NULL ");
                }
                if (schemaInfos2.EXTRA == "auto_increment") {
                    stringBuilder.Append("AUTO_INCREMENT ");
                }
                if (schemaInfos2.CDefault != null) {
                    stringBuilder.Append($"DEFAULT {schemaInfos2.CDefault} ");
                }
                stringBuilder.Append($"COMMENT @0");

                helper.Execute(stringBuilder.ToString(), comment);
            }
            helper.Dispose();
            return true;

        }



    }
}
