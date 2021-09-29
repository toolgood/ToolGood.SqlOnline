using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.PostgreSQL
{
    public class PostgreSQLViewProvider : ISqlViewProvider
    {
        private readonly ISqlProvider _provider;
        public PostgreSQLViewProvider()
        {
            _provider = new PostgreSQLProvider();
        }


        public List<ViewEntity> GetViews(string connStr, string databaseName)
        {
            const string sql = @"select ns.nspname SchemaName, cls.relname as ViewName,(select description from pg_description where objoid=cls.oid and objsubid=0) as Comment 
from pg_class  cls
join pg_catalog.pg_namespace as ns on ns.oid = cls.relnamespace
where cls.relkind ='v' and ns.nspname not in ('pg_catalog', 'pg_toast', 'information_schema')
order by ViewName;";
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(databaseName);
            var tableEntities = helper.Select<ViewEntity>(sql, databaseName);
            helper.Dispose();
            foreach (var item in tableEntities) {
                item.DatabaseName = databaseName;
            }
            return tableEntities;
        }

        public ViewEntity GetView(string connStr, string databaseName, string schemaName, string viewName)
        {
            const string sql = @"select ns.nspname SchemaName, cls.relname as ViewName,(select description from pg_description where objoid=cls.oid and objsubid=0) as Comment 
from pg_class  cls
join pg_catalog.pg_namespace as ns on ns.oid = cls.relnamespace
where cls.relkind ='v' and ns.nspname =@0 and cls.relname=@1
order by ViewName;";

            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(databaseName);
            var tableEntities = helper.FirstOrDefault<ViewEntity>(sql, schemaName, viewName);
            helper.Dispose();
            tableEntities.DatabaseName = databaseName;
            return tableEntities;
        }

        public List<ViewColumnEntity> GetViewColumns(string connStr, string databaseName, string schemaName, string viewName)
        {
            var sql = @"SELECT col.table_schema AS SchemaName, col.table_name TableName,col.ordinal_position AS Index, col.column_name ColumnName, col.character_maximum_length Length,
case when col.is_nullable='YES' then 'true' else 'false' end IsNullAble, 
col.numeric_precision AS Precision, col.numeric_scale Scale,
col_description(c.oid, col.ordinal_position) AS Comment,
col.column_default AS DefaultValue,
case when col.is_identity='YES' then 'true' else 'false' end IsIdentity, 
col.data_type AS col_type,
bt.typname AS elem_name
FROM information_schema.columns AS col 
LEFT JOIN pg_namespace ns ON ns.nspname = col.table_schema 
LEFT JOIN pg_class c ON col.table_name = c.relname AND c.relnamespace = ns.oid 
LEFT JOIN pg_attrdef a ON c.oid = a.adrelid AND col.ordinal_position = a.adnum 
LEFT JOIN pg_attribute b ON b.attrelid = c.oid AND b.attname = col.column_name 
LEFT JOIN pg_type et ON et.oid = b.atttypid 
LEFT JOIN pg_collation coll ON coll.oid = b.attcollation 
LEFT JOIN pg_namespace colnsp ON coll.collnamespace = colnsp.oid 
LEFT JOIN pg_type bt ON et.typelem = bt.oid LEFT JOIN pg_namespace nbt ON bt.typnamespace = nbt.oid 
WHERE col.table_schema =@0 AND col.table_name =@1
ORDER BY col.table_schema, col.table_name, col.ordinal_position";

            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(databaseName);
            var entities = helper.Select<TableColumnTemp>(sql, schemaName, viewName);
            helper.Dispose();

            List<ViewColumnEntity> result = new List<ViewColumnEntity>();
            foreach (var temp in entities) {
                ViewColumnEntity entity = new ViewColumnEntity() {
                    DatabaseName = databaseName,
                    SchemaName = temp.SchemaName,
                    ViewName = temp.TableName,
                    Index = temp.Index,
                    ColumnName = temp.ColumnName,
                    Comment = temp.Comment,
                    Length = temp.Length,
                    Precision = temp.Precision,
                    Scale = temp.Scale,
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

        public SqlEditorDto GetSelect100(string connStr, string databaseName, string schemaName, string viewName)
        {
            var viewColumns = GetViewColumns(connStr, databaseName, schemaName, viewName);
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT\r\n");
            var maxLength = viewColumns.Max(q => q.ColumnName.Length) + 10;
            for (int i = 0; i < viewColumns.Count; i++) {
                var column = viewColumns[i];
                stringBuilder.Append("\t");
                var columnName = (i > 0 ? "," : "") + "\"" + column.ColumnName + "\"";
                stringBuilder.Append(columnName);
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendFormat("FROM \"{0}\".\"{1}\"\r\n", schemaName, viewName);
            stringBuilder.AppendLine();

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string viewName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("CREATE VIEW \"{0}\".\"{1}\" AS \r\n", schemaName, viewName);
            var definition = GetViewDefinition(connStr, databaseName, schemaName, viewName);
            stringBuilder.Append(definition);

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }
        public SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string viewName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("DROP VIEW \"{0}\".\"{1}\";\r\n\r\n", schemaName, viewName);
            stringBuilder.AppendFormat("CREATE VIEW \"{0}\".\"{1}\" AS \r\n", schemaName, viewName);
            var definition = GetViewDefinition(connStr, databaseName, schemaName, viewName);
            stringBuilder.Append(definition);
            
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public string GetViewDefinition(string connStr, string dataBaseName, string schemaName, string viewName)
        {
            // SELECT definition FROM pg_views where schemaname=@0 and viewname=@1
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.PostgreSQL);
            helper.ChangeDatabase(dataBaseName);
            var sql = "SELECT pg_get_viewdef(c.oid, true) AS definition FROM pg_class c LEFT JOIN pg_namespace n ON c.relnamespace = n.oid WHERE c.relkind = 'v'::\"char\" AND n.nspname = @0 AND c.relname = @1;";
            var dt = helper.ExecuteDataTable(sql, schemaName, viewName);
            dt.Dispose();
            if (dt.Rows.Count > 0) {
                return dt.Rows[0]["definition"].ToString();
            }
            return null;
        }
    }



}
