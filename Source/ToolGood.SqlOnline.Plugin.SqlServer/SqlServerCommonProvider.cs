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

namespace ToolGood.SqlOnline.Plugin.SqlServer
{
    public class SqlServerCommonProvider : ExecuteProviderBase, ISqlCommonProvider
    {
        public SqlServerCommonProvider()
        {
            _provider = new SqlServerProvider();
        }

        private void UseDatabase(SqlHelper helper, string dataBaseName)
        {
            helper.Execute("Use [" + dataBaseName + "]");
        }


        public List<DatabaseEntity> GetDatabases(string connStr)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);

            const string sql = @"SELECT name AS 'DatabaseName'
FROM SYS.DATABASES
WHERE name NOT IN ('master', 'model', 'msdb', 'tempdb', 'Resource', 'distribution' , 'reportserver', 'reportservertempdb')
order by name";
            var result = helper.Select<DatabaseEntity>(sql);
            helper.Dispose();
            return result;
        }

        public List<TableEntity> GetTables(string connStr, string databaseName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);

            helper.ChangeDatabase(databaseName);
            var sql = @"select DB_NAME() AS DataBaseName ,SCHEMA_NAME(d.schema_id) SchemaName, d.name TableName,isnull(f.value,'') Comment
from sys.objects d 
left join sys.extended_properties f on d.object_id=f.major_id and f.minor_id=0 
where d.type='U' ";
            sql += "order by SchemaName,TableName";
            var result = helper.Select<TableEntity>(sql);
            helper.Dispose();
            return result;
        }
        public List<TableColumnEntity> GetTableColumns(string connStr, string databaseName, string schemaName, string tableName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            helper.ChangeDatabase(databaseName);

            var sql = @"SELECT 
       DB_NAME() AS DataBaseName ,
	   h.name as SchemaName,
       d.name as TableName, 
       a.colorder ColumnIndex, 
       a.name ColumnName, 
       COLUMNPROPERTY(a.id,a.name,'IsIdentity') IsIdentity, 
	   (case when (SELECT count(*) FROM sysobjects WHERE (name in (SELECT name FROM sysindexes WHERE (id = a.id) AND (indid IN (SELECT indid FROM sysindexkeys WHERE (id = a.id) AND (colid IN (SELECT colid FROM syscolumns WHERE (id = a.id) AND (name = a.name))))))) AND (xtype = 'PK'))>0 then 1 else 0 end) IsPrimaryKey,
	   b.name 'Type', 
       a.length 'Length', 
       COLUMNPROPERTY(a.id,a.name,'PRECISION') 'Precision', 
       COLUMNPROPERTY(a.id,a.name,'Scale') 'Scale', 
       a.isnullable  'IsNullAble', 
       e.text DefaultValue, 
       g.[value] Comment
FROM syscolumns a 
inner join sys.objects d on a.id=d.object_id  
left join systypes b on a.xtype=b.xusertype 
left join syscomments e on a.cdefault=e.id 
left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id  
left join sys.schemas h on d.schema_id=h.schema_id
where  d.type='U' ";

            if (string.IsNullOrEmpty(schemaName)) {
                sql += "order by d.schema_id,d.name,a.colorder asc";
                return helper.Select<TableColumnEntity>(sql);
            }
            if (string.IsNullOrEmpty(tableName)) {
                sql += "and h.name=@0  order by d.name,a.colorder asc";
                return helper.Select<TableColumnEntity>(sql, schemaName);
            }
            sql += "and h.name=@0  and d.name=@1 order by a.colorder asc";
            var result = helper.Select<TableColumnEntity>(sql, schemaName, tableName);
            helper.Dispose();
            return result;
        }


        public TableEntity GetTable(string connStr, string databaseName, string schemaName, string tableName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);

            helper.ChangeDatabase(databaseName);
            var sql = @"select DB_NAME() AS DataBaseName ,SCHEMA_NAME(d.schema_id) SchemaName, d.name TableName,isnull(f.value,'') Comment
from sys.objects d 
left join sys.extended_properties f on d.object_id=f.major_id and f.minor_id=0 
where d.type='U' and SCHEMA_NAME(d.schema_id)=@0  and d.name=@1  ";
            sql += "order by SchemaName,TableName";
            var result = helper.FirstOrDefault<TableEntity>(sql, schemaName, tableName);
            helper.Dispose();
            result.Columns = GetTableColumns(connStr, databaseName, schemaName, tableName);
            return result;
        }

        public List<TableShowEntity> GetTableShowList(string connStr, string databaseName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            helper.ChangeDatabase(databaseName);

            var sql = @"SELECT 
        h.name as SchemaName,
       d.name as Name, 
       f.[value] Comment,
       a.colorder ColumnIndex, 
       a.name ColumnName, 
       COLUMNPROPERTY(a.id,a.name,'IsIdentity') IsIdentity, 
	   (case when (SELECT count(*) FROM sysobjects WHERE (name in (SELECT name FROM sysindexes WHERE (id = a.id) AND (indid IN (SELECT indid FROM sysindexkeys WHERE (id = a.id) AND (colid IN (SELECT colid FROM syscolumns WHERE (id = a.id) AND (name = a.name))))))) AND (xtype = 'PK'))>0 then 1 else 0 end) IsPrimaryKey,
	   b.name 'Type', 
       a.length 'Length', 
       COLUMNPROPERTY(a.id,a.name,'PRECISION') 'Precision', 
       COLUMNPROPERTY(a.id,a.name,'Scale') 'Scale', 
       a.isnullable  'IsNullAble', 
       e.text DefaultValue, 
       g.[value] ColumnComment,
       d.type TableType
FROM syscolumns a 
inner join sys.objects d on a.id=d.object_id  
left join systypes b on a.xtype=b.xusertype 
left join syscomments e on a.cdefault=e.id 
left join sys.extended_properties f on d.object_id=f.major_id and f.minor_id=0 
left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id  
left join sys.schemas h on d.schema_id=h.schema_id
where  d.type in ('U','V') order by SchemaName,Name ";
            var result = helper.Select<TableShowEntity>(sql);
            helper.Dispose();
            return result;
        }

        public SqlEditorDto GetNew()
        {
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName() };
        }

        public SqlEditorDto GetSelect100(string connStr, string databaseName, string schemaName, string tableName)
        {
            var tableColumns = GetTableColumns(connStr, databaseName, schemaName, tableName);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}]\r\n", databaseName);
            stringBuilder.AppendLine();

            stringBuilder.AppendLine("SELECT TOP 100");
            var maxLength = tableColumns.Max(q => q.ColumnName.Length) + 10;
            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                stringBuilder.Append('\t');
                if (i > 0) {
                    stringBuilder.Append(',');
                }
                stringBuilder.Append($"[{column.ColumnName}]".PadRight(maxLength));
                stringBuilder.Append('\t');
                if (string.IsNullOrWhiteSpace(column.Comment) == false) {
                    stringBuilder.Append("-- ");
                    stringBuilder.Append(column.Comment.Trim());
                }
                stringBuilder.AppendLine();
            }
            if (schemaName == "dbo") {
                stringBuilder.AppendFormat("FROM [{0}]\r\n", tableName);
            } else {
                stringBuilder.AppendFormat("FROM [{0}].[{1}]\r\n", schemaName, tableName);
            }
            //stringBuilder.AppendLine("WHERE 1=1");
            stringBuilder.AppendLine();
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetInsertSql(string connStr, string databaseName, string schemaName, string tableName)
        {
            var tableColumns = GetTableColumns(connStr, databaseName, schemaName, tableName);
            tableColumns.RemoveAll(q => q.IsIdentity);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}]\r\n\r\n", databaseName);
            stringBuilder.AppendFormat("INSERT INTO [{0}].[{1}](\r\n", schemaName, tableName);
            var maxLength = tableColumns.Max(q => q.ColumnName.Length) + 10;
            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                stringBuilder.Append("\t");

                var columnName = (i > 0 ? "," : "") + "[" + column.ColumnName + "]";
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
            stringBuilder.AppendFormat("USE [{0}]\r\n\r\n", databaseName);

            stringBuilder.AppendFormat("UPDATE [{0}].[{1}] SET \r\n", schemaName, tableName);
            var maxLength = tableColumns.Max(q => q.ColumnName.Length) + 10;
            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                stringBuilder.Append("\t");
                var columnName = $"[{column.ColumnName}]";
                stringBuilder.Append(columnName.PadRight(maxLength));
                stringBuilder.Append($"= '{column.GetColumnType()}'".PadRight(15));
                if (i < tableColumns.Count - 1) {
                    stringBuilder.Append(',');
                }
                if (string.IsNullOrEmpty(column.Comment) == false) {
                    stringBuilder.Append($"\t -- {column.Comment.Trim()} ");
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.Append("WHERE <where condition>");
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }
        public SqlEditorDto GetDeleteSql(string connStr, string databaseName, string schemaName, string tableName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}]\r\n\r\n", databaseName);
            stringBuilder.AppendFormat("DELETE FROM [{0}].[{1}] WHERE <where condition> \r\n", schemaName, tableName);
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string tableName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}]\r\n\r\n", databaseName);
            var table = GetTableDefinition(connStr, databaseName, schemaName, tableName);
            stringBuilder.Append(table);
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public string GetTableDefinition(string connStr, string dataBaseName, string schemaName, string tableName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            UseDatabase(helper, dataBaseName);
            const string sql = @"select OBJECT_DEFINITION (d.object_id) AS 'Definition'
FROM sys.objects d 
where d.type='U' and SCHEMA_NAME(d.schema_id)=@0 and  d.name=@1 ";
            var sql2 = helper.FirstOrDefault<string>(sql, schemaName, tableName);
            helper.Dispose();
            return sql2;
        }

        public bool ChangeComment(string connStr, string databaseName, string schemaName, string tableName, string comment)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            UseDatabase(helper, databaseName);
            const string sql = @"IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 'SCHEMA', @0, 'TABLE', @1, NULL, NULL)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = @2, @level0type = 'SCHEMA', @level0name = @1, @level1type = 'TABLE', @level1name = @2
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = @2, @level0type = 'SCHEMA', @level0name = @0, @level1type = 'TABLE', @level1name = @1
";
            helper.Execute(sql, schemaName, tableName, comment);
            helper.Dispose();
            return true;
        }

        public bool ChangeComment(string connStr, string databaseName, string schemaName, string tableName, string columnName, string comment)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            const string sql = @"IF ((SELECT COUNT(*) from fn_listextendedproperty('MS_Description', 'SCHEMA', @0, 'TABLE',@1, 'COLUMN',@2)) > 0) 
EXEC sp_updateextendedproperty @name = N'MS_Description', @value = @3, @level0type = 'SCHEMA', @level0name = @0, @level1type = 'TABLE', @level1name = @1, @level2type = 'COLUMN', @level2name = @2
ELSE
EXEC sp_addextendedproperty @name = N'MS_Description', @value = @3, @level0type = 'SCHEMA', @level0name = @0, @level1type = 'TABLE', @level1name = @1, @level2type = 'COLUMN', @level2name = @2
";
            helper.Execute(sql, schemaName, tableName, columnName, comment);
            helper.Dispose();
            return true;

        }


    }
}
