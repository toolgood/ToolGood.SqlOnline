/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.SqlServer
{
    public class SqlServerFunctionProvider : ISqlFunctionProvider
    {
        private readonly ISqlProvider _provider;
        public SqlServerFunctionProvider()
        {
            _provider = new SqlServerProvider();
        }


        public List<FunctionEntity> GetFunctions(string connStr, string databaseName)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            sqlHelper.ChangeDatabase(databaseName);

            var sql = @"SELECT DB_NAME() DataBaseName,
       SCHEMA_NAME(sp.schema_id) SchemaName,
       sp.name FunctionName,
       g.[value] Comment
FROM sys.objects AS sp
left join sys.extended_properties g on sp.object_id= g.major_id  and g.name= 'MS_Description'
WHERE sp.type IN ('FN', 'IF', 'TF')  AND ISNULL(sp.is_ms_shipped, 0) = 0 ";
            sql += "order by sp.schema_id asc,sp.name asc";
            var result = sqlHelper.Select<FunctionEntity>(sql);
            sqlHelper.Dispose();
            return result;
        }

        public List<FunctionParamEntity> GetFunctionParams(string connStr, string databaseName, string schemaName, string functionName)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            sqlHelper.ChangeDatabase(databaseName);

            var sql = @"SELECT DB_NAME() DataBaseName,
       SCHEMA_NAME(pr.schema_id) SchemaName,
       pr.name FunctionName,
       p.name ParamName,
       t.name 'Type',
       p.max_length 'Length',
       p.precision 'Precision',
       p.scale 'Scale',
       p.is_output 'IsOutput',
       p.default_value 'DefaultValue',
       p.has_default_value 'HasDefaultValue',
       0 'IsNullAble',
       p.is_readonly 'IsReadonly'
FROM sys.objects pr    
INNER JOIN sys.parameters p ON p.object_id = pr.object_id
INNER JOIN sys.types t ON p.system_type_id = t.system_type_id
WHERE pr.type IN ('FN', 'IF', 'TF') AND ISNULL(pr.is_ms_shipped, 0) = 0 and (p.name is not null and p.name<>'')
and SCHEMA_NAME(pr.schema_id)=@0 and pr.name=@1 order by p.object_id asc
";
            var result = sqlHelper.Select<FunctionParamEntity>(sql, schemaName, functionName);
            sqlHelper.Dispose();
            return result;
        }



        public FunctionEntity GetFunction(string connStr, string databaseName, string schemaName, string functionName)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            sqlHelper.ChangeDatabase(databaseName);

            var sql = @"SELECT DB_NAME() DataBaseName,
       SCHEMA_NAME(sp.schema_id) SchemaName,
       sp.name FunctionName,
       g.[value] Comment
FROM sys.objects AS sp
left join sys.extended_properties g on sp.object_id= g.major_id  and g.name= 'MS_Description'
WHERE sp.type IN ('FN', 'IF', 'TF')  AND ISNULL(sp.is_ms_shipped, 0) = 0 and SCHEMA_NAME(sp.schema_id)=@0 and sp.name=@1";

            var result = sqlHelper.FirstOrDefault<FunctionEntity>(sql, schemaName, functionName);
            sqlHelper.Dispose();
            result.Params = GetFunctionParams(connStr, databaseName, schemaName, functionName);
            return result;


        }

        public SqlEditorDto GetSelectSql(string connStr, string databaseName, string schemaName, string functionName)
        {
            var functionParams = GetFunctionParams(connStr, databaseName, schemaName, functionName);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}];\r\n\r\n", databaseName);
            stringBuilder.AppendFormat("SELECT [{0}].[{1}] (", schemaName,functionName);

            HashSet<string> paramNames = new HashSet<string>();
            for (int i = 0; i < functionParams.Count; i++) {
                var column = functionParams[i];
                if (paramNames.Add(column.ParamName)) {
                    if (i > 0) {
                        stringBuilder.Append(" , ");
                    }
                    stringBuilder.Append($"<{column.ParamName} {column.GetColumnType()}>");
                }
            }
            stringBuilder.Append(")");
 
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string functionName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}];\r\n\r\n", databaseName);
            var sql = GetFunctionDefinition(connStr, databaseName, schemaName, functionName);
            stringBuilder.Append(sql);

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string functionName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}];\r\n\r\n", databaseName);
            var sql = GetFunctionDefinition(connStr, databaseName, schemaName, functionName);
            var m = Regex.Match(sql, @"\bCREATE\b", RegexOptions.IgnoreCase);
            if (m.Success) {
                for (int i = 0; i < m.Index; i++) {
                    stringBuilder.Append(sql[i]);
                }
                stringBuilder.Append("ALTER");
                for (int i = m.Index + m.Length; i < sql.Length; i++) {
                    stringBuilder.Append(sql[i]);
                }
            }
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }
        public string GetFunctionDefinition(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            helper.ChangeDatabase(databaseName);

            var sql2 = @"SELECT OBJECT_DEFINITION (pr.object_id)
FROM sys.objects pr    
WHERE pr.type IN ('FN', 'IF', 'TF') 
and SCHEMA_NAME(pr.schema_id)=@0 and pr.name=@1";
            var dt2 = helper.FirstOrDefault<string>(sql2, schemaName, procedureName);
            helper.Dispose();
            return dt2;
        }

    }
}
