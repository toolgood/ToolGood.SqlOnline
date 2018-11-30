using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using ToolGood.PluginBase;
using ToolGood.ReadyGo3;


namespace ToolGood.Plugin.Mysql
{
    public class MySqlProvider : ISqlProvider
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public string GetServerName()
        {
            return "Mysql";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DbProviderFactory GetProviderFactory()
        {
            return MySql.Data.MySqlClient.MySqlClientFactory.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServerTree GetServerInfo(string name, string connStr)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, GetProviderFactory(), SqlType.MySql);

            var list = sqlHelper.Select<string>("select SCHEMA_NAME from information_schema.SCHEMATA");
            list.RemoveAll(q => q.ToLower() == "information_schema");
            list.RemoveAll(q => q.ToLower() == "performance_schema");
            list.RemoveAll(q => q.ToLower() == "mysql");
            list.RemoveAll(q => q.ToLower() == "sys");

            List<DatabaseTreeNode> databaseTrees = new List<DatabaseTreeNode>();
            foreach (var schema in list) {
                DatabaseTreeNode databases = new DatabaseTreeNode() { Name = schema };
                databaseTrees.Add(databases);
                var sql = "SELECT TABLE_NAME from information_schema.`TABLES` where TABLE_SCHEMA=@0";
                var tableNames = sqlHelper.Select<string>(sql, schema);

                databases.Children = new List<TableTreeNode>();
                foreach (var tableName in tableNames) {
                    TableTreeNode dto = new TableTreeNode() { Name = tableName };
                    databases.Children.Add(dto);
                }
            }
            ServerTree serverTree = new ServerTree() { Name = name, Children = databaseTrees };

            return serverTree;
        }

        /// <summary>
        /// 获取数据库名称列表
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        public List<DatabaseTreeNode> GetDatabases(string connStr)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, GetProviderFactory(), SqlType.MySql);

            var list = sqlHelper.Select<string>("select SCHEMA_NAME from information_schema.SCHEMATA");
            list.RemoveAll(q => q.ToLower() == "information_schema");
            list.RemoveAll(q => q.ToLower() == "performance_schema");
            list.RemoveAll(q => q.ToLower() == "mysql");
            list.RemoveAll(q => q.ToLower() == "sys");

            List<DatabaseTreeNode> databaseTrees = new List<DatabaseTreeNode>();
            foreach (var schema in list) {
                DatabaseTreeNode databases = new DatabaseTreeNode() { Name = schema };
                databaseTrees.Add(databases);
            }

            return databaseTrees;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public List<SqlTableInfo> GetFullTableInfos(string connStr, string databaseName)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, GetProviderFactory(), SqlType.MySql);
            const string s = @"SELECT TABLE_NAME as TName,TABLE_COMMENT as CComment from information_schema.`TABLES` WHERE TABLE_SCHEMA=@0";
            var schemaInfos = sqlHelper.Select<SchemaInfo>(s, databaseName);

            List<SqlTableInfo> tableInfos = new List<SqlTableInfo>();
            foreach (var item in schemaInfos) {
                SqlTableInfo tableInfo = new SqlTableInfo() {
                    DatabaseName = databaseName,
                    TableName = item.TName,
                    Comment = item.CComment,
                    UseSchema = false,
                    Columns = new List<SqlColumnInfo>()
                };
                tableInfos.Add(tableInfo);
            }

            const string sql = @"SELECT TABLE_NAME as TName,COLUMN_NAME as CName,COLUMN_TYPE as CType,COLUMN_COMMENT as CComment,COLUMN_DEFAULT as CDefault,IS_NULLABLE as CIsNull
from information_schema.`COLUMNS` WHERE TABLE_SCHEMA=@0";
            var schemaInfos2 = sqlHelper.Select<SchemaInfo>(sql, databaseName);

            foreach (var tableInfo in tableInfos) {
                var columns = schemaInfos2.Where(q => q.TName == tableInfo.TableName).ToList();
                foreach (var column in columns) {
                    SqlColumnInfo columnInfo = new SqlColumnInfo() {
                        ColumnName = column.CName,
                        ColumnType = column.CType,
                        Comment = column.CComment,
                        DefaultString = column.CDefault,
                        AllowNull = column.CIsNull.ToUpper() == "YES"
                    };
                    tableInfo.Columns.Add(columnInfo);
                }
            }

            return tableInfos;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public List<SqlTableInfo> GetTableInfos(string connStr, string databaseName)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, GetProviderFactory(), SqlType.MySql);
            const string s = @"SELECT TABLE_NAME as TName,TABLE_COMMENT as CComment from information_schema.`TABLES` WHERE TABLE_SCHEMA=@0";
            var schemaInfos = sqlHelper.Select<SchemaInfo>(s, databaseName);

            List<SqlTableInfo> tableInfos = new List<SqlTableInfo>();
            foreach (var item in schemaInfos) {
                SqlTableInfo tableInfo = new SqlTableInfo() {
                    DatabaseName = databaseName,
                    TableName = item.TName,
                    Comment = item.CComment,
                    UseSchema = false,
                    Columns = new List<SqlColumnInfo>()
                };
                tableInfos.Add(tableInfo);
            }
            return tableInfos;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public List<SqlColumnInfo> GetColumnInfos(string connStr, string databaseName, string tableName)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, GetProviderFactory(), SqlType.MySql);

            const string sql = @"SELECT TABLE_NAME as TName,COLUMN_NAME as CName,COLUMN_TYPE as CType,COLUMN_COMMENT as CComment,COLUMN_DEFAULT as CDefault,IS_NULLABLE as CIsNull
from information_schema.`COLUMNS` WHERE TABLE_SCHEMA=@0 and TABLE_NAME=@1";
            var schemaInfos2 = sqlHelper.Select<SchemaInfo>(sql, databaseName);

            List<SqlColumnInfo> columnInfos = new List<SqlColumnInfo>();

            foreach (var column in schemaInfos2) {
                SqlColumnInfo columnInfo = new SqlColumnInfo() {
                    ColumnName = column.CName,
                    ColumnType = column.CType,
                    Comment = column.CComment,
                    DefaultString = column.CDefault,
                    AllowNull = column.CIsNull.ToUpper() == "YES"
                };
                columnInfos.Add(columnInfo);
            }
            return columnInfos;
        }


    }
    public class SchemaInfo
    {
        public string Schema { get; set; }

        public string TName { get; set; }

        public string CName { get; set; }

        public string CType { get; set; }

        public string CComment { get; set; }

        public string CDefault { get; set; }

        public string CIsNull { get; set; }
    }
}
