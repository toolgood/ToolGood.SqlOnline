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

namespace ToolGood.SqlOnline.Plugin.SqlServer
{
    public class SqlServerViewProvider : ISqlViewProvider
    {
        private readonly ISqlProvider _provider;
        public SqlServerViewProvider()
        {
            _provider = new SqlServerProvider();
        }

        public List<ViewEntity> GetViews(string connStr, string databaseName)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            sqlHelper.ChangeDatabase(databaseName);

            var sql = @"select DB_NAME() AS DataBaseName ,SCHEMA_NAME(d.schema_id) SchemaName, d.name ViewName,isnull(f.value,'') Comment
FROM sys.objects d 
left join sys.extended_properties f on d.object_id=f.major_id and f.minor_id=0 
where d.type='V' ";
            sql += "order by SchemaName,ViewName";
            var result = sqlHelper.Select<ViewEntity>(sql);
            sqlHelper.Dispose();
            return result;
        }

        public List<ViewColumnEntity> GetViewColumns(string connStr, string databaseName, string schemaName, string viewName)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);

            sqlHelper.ChangeDatabase(databaseName);
            var sql = @"SELECT 
       DB_NAME() AS DataBaseName ,
       d.name as ViewName, 
	   h.name as SchemaName,
       a.colorder ColumnIndex, 
       a.name ColumnName, 
       b.name 'Type', 
       a.length 'Length', 
       COLUMNPROPERTY(a.id,a.name,'PRECISION') 'Precision', 
       COLUMNPROPERTY(a.id,a.name,'Scale') 'Scale', 
       a.isnullable 'IsNullAble', 
       g.[value],''  Comment
FROM syscolumns a 
inner join sys.objects d on a.id=d.object_id  
left join systypes b on a.xtype=b.xusertype 
left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id  
left join sys.schemas h on d.schema_id=h.schema_id
where  d.type='V' ";
            if (string.IsNullOrEmpty(schemaName)) {
                return sqlHelper.Select<ViewColumnEntity>(sql);
            }
            if (string.IsNullOrEmpty(viewName)) {
                sql += "and h.name=@0";
                return sqlHelper.Select<ViewColumnEntity>(sql, schemaName);
            }
            sql += "and h.name=@0 and d.name=@1";
            var result = sqlHelper.Select<ViewColumnEntity>(sql, schemaName, viewName);
            sqlHelper.Dispose();
            return result;
        }


        public ViewEntity GetView(string connStr, string databaseName, string schemaName, string viewName)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);

            sqlHelper.ChangeDatabase(databaseName);
            var sql = @"select DB_NAME() AS DataBaseName ,SCHEMA_NAME(d.schema_id) SchemaName, d.name ViewName,isnull(f.value,'') Comment
FROM sys.objects d 
left join sys.extended_properties f on d.object_id=f.major_id and f.minor_id=0 
where d.type='V' and SCHEMA_NAME(d.schema_id)=@0 and d.name=@1 ";
            var result = sqlHelper.FirstOrDefault<ViewEntity>(sql, schemaName, viewName);
            sqlHelper.Dispose();
            result.Columns = GetViewColumns(connStr, databaseName, schemaName, viewName);
            return result;

        }

        public SqlEditorDto GetSelect100(string connStr, string databaseName, string schemaName, string viewName)
        {
            var viewColumns = GetViewColumns(connStr, databaseName, schemaName, viewName);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}];\r\n", databaseName);
            stringBuilder.AppendLine();
            stringBuilder.Append("SELECT TOP 100\r\n");
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
            if (schemaName == "dbo") {
                stringBuilder.AppendFormat("FROM [{0}]\r\n", viewName);
            } else {
                stringBuilder.AppendFormat("FROM [{0}].[{1}]\r\n", schemaName, viewName);
            }

            stringBuilder.AppendLine();
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string viewName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}];\r\n\r\n", databaseName);
            var sql = GetTableDefinition(connStr, databaseName, schemaName, viewName);
            stringBuilder.AppendFormat(sql);

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }
        public SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string viewName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}];\r\n\r\n", databaseName);
            var sql = GetTableDefinition(connStr, databaseName, schemaName, viewName);
            sql = Regex.Replace(sql, @"\bCREATE\b", "ALTER", RegexOptions.IgnoreCase);
            stringBuilder.AppendFormat(sql);

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public string GetTableDefinition(string connStr, string databaseName, string schemaName, string tableName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            helper.ChangeDatabase(databaseName);
            const string sql = @"SELECT OBJECT_DEFINITION (pr.object_id)
FROM sys.objects pr    
WHERE pr.type = 'V' 
and SCHEMA_NAME(pr.schema_id)=@0 and pr.name=@1";
            var sql2 = helper.FirstOrDefault<string>(sql, schemaName, tableName);
            helper.Dispose();
            return sql2;
        }



    }
}
