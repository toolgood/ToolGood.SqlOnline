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

namespace ToolGood.SqlOnline.Plugin.PostgreSQL
{
    public partial class PostgreSQLCommonProvider : ExecuteProviderBase, ISqlCommonProvider
    {
        public PostgreSQLCommonProvider()
        {
            _provider = new PostgreSQLProvider();
        }

        public List<DatabaseEntity> GetDatabases(string connStr)
        {
            const string sql = @"SELECT datname as DatabaseName FROM pg_database where datname not in('postgres','template1','template0')";

            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            var entities = helper.Select<DatabaseEntity>(sql);
            helper.Dispose();
            return entities;
        }

        public List<TableEntity> GetTables(string connStr, string databaseName)
        {
            const string sql = @"select ns.nspname SchemaName, cls.relname as TableName ,(select description from pg_description where objoid=cls.oid and objsubid=0) as Comment 
from pg_class  cls
join pg_catalog.pg_namespace as ns on ns.oid = cls.relnamespace
where cls.relkind ='r' and ns.nspname not in ('pg_catalog', 'pg_toast', 'information_schema')
order by TableName;";

            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(databaseName);
            var entities = helper.Select<TableEntity>(sql);
            helper.Dispose();
            foreach (var item in entities) {
                item.DatabaseName = databaseName;
            }
            return entities;
        }

        public List<TableColumnEntity> GetTableColumns(string connStr, string databaseName, string schemaName, string tableName)
        {
            var sql = @"SELECT col.table_schema AS SchemaName, col.table_name TableName,col.ordinal_position AS Index, col.column_name ColumnName, col.character_maximum_length Length,
case when col.is_nullable='YES' then 'true' else 'false' end IsNullAble, 
col.numeric_precision AS Precision, col.numeric_scale Scale,
col_description(c.oid, col.ordinal_position) AS Comment,
col.column_default AS DefaultValue,
case when col.is_identity='YES' then 'true' else 'false' end IsIdentity, 
col.data_type AS col_type,
bt.typname AS elem_name,
(case when(select count(*) from pg_constraint where conrelid = b.attrelid and conkey[1] = b.attnum and contype = 'p') > 0 then 'true' else 'false' end) IsPrimaryKey
FROM information_schema.columns AS col 
LEFT JOIN pg_namespace ns ON ns.nspname = col.table_schema 
LEFT JOIN pg_class c ON col.table_name = c.relname AND c.relnamespace = ns.oid 
LEFT JOIN pg_attrdef a ON c.oid = a.adrelid AND col.ordinal_position = a.adnum 
LEFT JOIN pg_attribute b ON b.attrelid = c.oid AND b.attname = col.column_name 
LEFT JOIN pg_type et ON et.oid = b.atttypid 
LEFT JOIN pg_collation coll ON coll.oid = b.attcollation 
LEFT JOIN pg_namespace colnsp ON coll.collnamespace = colnsp.oid 
LEFT JOIN pg_type bt ON et.typelem = bt.oid LEFT JOIN pg_namespace nbt ON bt.typnamespace = nbt.oid 
WHERE col.table_schema = @0 AND col.table_name = @1
ORDER BY col.table_schema, col.table_name, col.ordinal_position";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(databaseName);
            var entities = helper.Select<TableColumnTemp>(sql, schemaName, tableName);
            helper.Dispose();

            List<TableColumnEntity> result = new List<TableColumnEntity>();
            foreach (var temp in entities) {
                TableColumnEntity entity = new TableColumnEntity() {
                    DatabaseName = databaseName,
                    SchemaName = temp.SchemaName,
                    TableName = temp.TableName,
                    Index = temp.Index,
                    ColumnName = temp.ColumnName,
                    Comment = temp.Comment,
                    DefaultValue = temp.DefaultValue,
                    Length = temp.Length,
                    Precision = temp.Precision,
                    Scale = temp.Scale,
                    IsPrimaryKey = temp.IsPrimaryKey,
                    IsIdentity = temp.IsIdentity,
                    IsNullAble = temp.IsNullAble,
                    Type = temp.col_type,
                };
                if (entity.Type == "ARRAY") {
                    entity.Type = temp.elem_name + "[]";
                }
                result.Add(entity);
            }
            return result;
        }

        public List<TableShowEntity> GetTableShowList(string connStr, string databaseName)
        {
            var sql = @"SELECT col.table_schema AS SchemaName, col.table_name TableName, 
col.ordinal_position AS Index, col.column_name ColumnName, col.character_maximum_length Length,
case when col.is_nullable='YES' then 'true' else 'false' end IsNullAble, 
col.numeric_precision AS Precision, col.numeric_scale Scale,
col_description(c.oid, col.ordinal_position) AS Comment,
col.column_default AS DefaultValue,
case when col.is_identity='YES' then 'true' else 'false' end IsIdentity, 
col.data_type AS col_type,
bt.typname AS elem_name,c.relkind TableType,
(case when(select count(*) from pg_constraint where conrelid = b.attrelid and conkey[1] = b.attnum and contype = 'p') > 0 then 'true' else 'false' end) IsPrimaryKey
FROM information_schema.columns AS col 
LEFT JOIN pg_namespace ns ON ns.nspname = col.table_schema 
LEFT JOIN pg_class c ON col.table_name = c.relname AND c.relnamespace = ns.oid 
LEFT JOIN pg_attrdef a ON c.oid = a.adrelid AND col.ordinal_position = a.adnum 
LEFT JOIN pg_attribute b ON b.attrelid = c.oid AND b.attname = col.column_name 
LEFT JOIN pg_type et ON et.oid = b.atttypid 
LEFT JOIN pg_collation coll ON coll.oid = b.attcollation 
LEFT JOIN pg_namespace colnsp ON coll.collnamespace = colnsp.oid 
LEFT JOIN pg_type bt ON et.typelem = bt.oid LEFT JOIN pg_namespace nbt ON bt.typnamespace = nbt.oid 
WHERE  c.relkind in ('r','v') and ns.nspname not in ('pg_catalog', 'pg_toast', 'information_schema')
ORDER BY col.table_schema,c.relkind, col.table_name, col.ordinal_position";

            const string sql_table = @"select ns.nspname SchemaName, cls.relname as TableName ,(select description from pg_description where objoid=cls.oid and objsubid=0) as Comment 
from pg_class  cls
join pg_catalog.pg_namespace as ns on ns.oid = cls.relnamespace
where cls.relkind in ('r','v') and ns.nspname not in ('pg_catalog', 'pg_toast', 'information_schema')
order by TableName;";
            Dictionary<string, string> comments = new Dictionary<string, string>();

            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(databaseName);
            var entities = helper.Select<TableEntity>(sql_table);
            foreach (var entity in entities) {
                comments[entity.SchemaName + "." + entity.TableName] = entity.Comment;
            }
            var entities2 = helper.Select<TableColumnTemp>(sql );
            helper.Dispose();
            List<TableShowEntity> result = new List<TableShowEntity>();
            foreach (var temp in entities2) {
                TableShowEntity entity = new TableShowEntity() {
                    SchemaName = temp.SchemaName,
                    Name = temp.TableName,
                    Index = temp.Index,
                    ColumnName = temp.ColumnName,
                    ColumnComment = temp.Comment,
                    DefaultValue = temp.DefaultValue,
                    Length = temp.Length,
                    Precision = temp.Precision,
                    Scale = temp.Scale,
                    IsPrimaryKey = temp.IsPrimaryKey,
                    IsIdentity = temp.IsIdentity,
                    IsNullAble = temp.IsNullAble,
                    Type = temp.col_type,
                    TableType=temp.TableType,
                    
                };
                if (entity.Type == "ARRAY") {
                    entity.Type = temp.elem_name + "[]";
                }
                if (entity.TableType == "r") {
                    entity.TableType = "t";
                }
                if (comments.TryGetValue(temp.SchemaName+"."+ temp.TableName,out string comment)) {
                    entity.Comment = comment;
                }
                result.Add(entity);
            }
            return result;
            //            select
            //(select relname || '--' || (select description from pg_description where objoid = oid and objsubid = 0) as comment from pg_class where oid = a.attrelid) as table_name,
            //a.attname as column_name,
            //format_type(a.atttypid, a.atttypmod) as data_type,
            //(case when atttypmod-4 > 0 then atttypmod-4 else 0 end)data_length,
            //(case when(select count(*) from pg_constraint where conrelid = a.attrelid and conkey[1] = attnum and contype = 'p') > 0 then 'Y' else 'N' end) as 主键约束,
            //(case when(select count(*) from pg_constraint where conrelid = a.attrelid and conkey[1] = attnum and contype = 'u') > 0 then 'Y' else 'N' end) as 唯一约束,
            //(case when(select count(*) from pg_constraint where conrelid = a.attrelid and conkey[1] = attnum and contype = 'f') > 0 then 'Y' else 'N' end) as 外键约束,
            //(case when a.attnotnull = true then 'Y' else 'N' end) as nullable,
            //col_description(a.attrelid, a.attnum) as comment
            //from pg_attribute a
            //where attstattarget = -1 and attrelid in (select oid from pg_class where relname in(select relname from pg_class where relkind = 'r' and relname is not null ))
            //order by table_name,a.attnum;
        }

        public TableEntity GetTable(string connStr, string databaseName, string schemaName, string tableName)
        {
            const string sql = @"select cls.relname as TableName,(select description from pg_description where objoid=cls.oid and objsubid=0) as Comment 
from pg_class  cls
join pg_catalog.pg_namespace as ns on ns.oid = cls.relnamespace
where cls.relkind ='r' and ns.nspname =@0 and cls.relname=@1
order by TableName;";

            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            var tableEntities = helper.FirstOrDefault<TableEntity>(sql, databaseName, tableName);
            helper.Dispose();
            tableEntities.DatabaseName = databaseName;
            return tableEntities;
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
                stringBuilder.Append($"\"{column.ColumnName}\"".PadRight(maxLength));
                stringBuilder.Append('\t');
                if (string.IsNullOrWhiteSpace(column.Comment) == false) {
                    stringBuilder.Append("-- ");
                    stringBuilder.Append(column.Comment.Trim());
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine($"FROM \"{schemaName}\".\"{tableName}\" ");
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
            stringBuilder.AppendFormat("INSERT INTO \"{0}\".\"{1}\"(\r\n", schemaName, tableName);

            var maxLength = tableColumns.Max(q => q.ColumnName.Length) + 10;
            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                stringBuilder.Append("\t");

                var columnName = (i > 0 ? "," : "") + "\"" + column.ColumnName + "\"";
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
                if (column.Comment != null) {
                    stringBuilder.Append($"\t-- {column.ColumnName.PadRight(20)} {column.Comment.Trim()} ");
                } else {
                    stringBuilder.Append($"\t-- {column.ColumnName.PadRight(20)} ");
                }
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
            stringBuilder.AppendFormat("UPDATE \"{0}\".\"{1}\" SET \r\n", schemaName, tableName);
            var maxLength = tableColumns.Max(q => q.ColumnName.Length) + 10;

            tableColumns.RemoveAll(q => q.IsIdentity);

            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                stringBuilder.Append('\t');
                var columnName = $"\"{column.ColumnName}\"";
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
            stringBuilder.AppendFormat("DELETE FROM \"{0}\".\"{1}\" WHERE <where condition> \r\n", schemaName, tableName);
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }
        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string tableName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }


        public bool ChangeComment(string connStr, string databaseName, string schemaName, string tableName, string comment)
        {
            string sql = $"COMMENT ON TABLE \"{schemaName}\".\"{tableName}\" IS @0;";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.Execute(sql, comment);
            helper.Dispose();
            return true;
        }

        public bool ChangeComment(string connStr, string databaseName, string schemaName, string tableName, string columnName, string comment)
        {
            string sql = $"COMMENT ON COLUMN \"{schemaName}\".\"{tableName}\".\"{columnName}\" IS @0;";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.Execute(sql, comment);
            helper.Dispose();
            return true;
        }





    }



}
