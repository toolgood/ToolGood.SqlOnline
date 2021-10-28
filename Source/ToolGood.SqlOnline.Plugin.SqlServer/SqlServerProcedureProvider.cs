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
    public class SqlServerProcedureProvider : ISqlProcedureProvider
    {
        private readonly ISqlProvider _provider;
        public SqlServerProcedureProvider()
        {
            _provider = new SqlServerProvider();
        }
 

        public List<ProcedureEntity> GetProcedures(string connStr, string databaseName)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            sqlHelper.ChangeDatabase(databaseName);

            var sql = @"SELECT DB_NAME() DataBaseName,
       SCHEMA_NAME(pr.schema_id) SchemaName,
       pr.name ProcedureName,
	   g.[value] Comment
FROM sys.procedures pr
left join sys.extended_properties g on pr.object_id=g.major_id  and g.name='MS_Description' ";

            sql += "order by pr.schema_id asc,pr.name asc";
            var result = sqlHelper.Select<ProcedureEntity>(sql);
            sqlHelper.Dispose();
            return result;
        }

        public List<ProcedureParamEntity> GetProcedureParams(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            sqlHelper.ChangeDatabase(databaseName);

            var sql = @"SELECT DB_NAME() DataBaseName,
       h.name as SchemaName,
       pr.name ProcedureName,
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
FROM sys.procedures pr
INNER JOIN sys.parameters p ON p.object_id = pr.object_id
INNER JOIN sys.types t ON p.system_type_id = t.system_type_id 
left join sys.schemas h on pr.schema_id=h.schema_id ";
            if (string.IsNullOrEmpty(schemaName)) {
                sql += "order by pr.schema_id asc, pr.name asc ,p.object_id asc";
                return sqlHelper.Select<ProcedureParamEntity>(sql);
            }
            if (string.IsNullOrEmpty(procedureName)) {
                sql += "where h.name=@0 order by pr.name asc ,p.object_id asc";
                return sqlHelper.Select<ProcedureParamEntity>(sql, schemaName);
            }
            sql += "where h.name=@0 and pr.name=@1 order by p.object_id asc";
            var result = sqlHelper.Select<ProcedureParamEntity>(sql, schemaName, procedureName);
            sqlHelper.Dispose();
            return result;
        }



        public ProcedureEntity GetProcedure(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var sqlHelper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            sqlHelper.ChangeDatabase(databaseName);

            var sql = @"SELECT DB_NAME() DataBaseName,
       SCHEMA_NAME(pr.schema_id) SchemaName,
       pr.name ProcedureName,
	   g.[value] Comment
FROM sys.procedures pr
left join sys.extended_properties g on pr.object_id=g.major_id  and g.name='MS_Description' and SCHEMA_NAME(pr.schema_id)=@0 and pr.name=@1 ";

            sql += "order by pr.schema_id asc,pr.name asc";
            var result = sqlHelper.FirstOrDefault<ProcedureEntity>(sql, schemaName, procedureName);
            sqlHelper.Dispose();
            result.Params = GetProcedureParams(connStr, databaseName, schemaName, procedureName);

            return result;


        }

        public SqlEditorDto GetExecuteSql(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var procedureParams = GetProcedureParams(connStr, databaseName, schemaName, procedureName);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}];\r\n\r\n", databaseName);
            stringBuilder.AppendFormat("EXECUTE [{0}]\r\n", procedureName);

            for (int i = 0; i < procedureParams.Count; i++) {
                var column = procedureParams[i];
                stringBuilder.Append('\t');
                if (i>0) {
                    stringBuilder.Append(',');
                }
                stringBuilder.Append($"{column.ParamName}= <{column.GetColumnType()}>");
                stringBuilder.AppendLine();
            }

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }


        public SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string procedureName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}];\r\n\r\n", databaseName);
            var sql = GetProcedureDefinition(connStr, databaseName, schemaName, procedureName);
            stringBuilder.Append(sql);

            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }

        public SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string procedureName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("USE [{0}];\r\n\r\n", databaseName);
            var sql = GetProcedureDefinition(connStr, databaseName, schemaName, procedureName);
            var m = Regex.Match(sql, @"\b(CREATE).+(PROCEDURE|proc)\b", RegexOptions.IgnoreCase);
            if (m.Success) {
                for (int i = 0; i < m.Groups[1].Index; i++) {
                    stringBuilder.Append(sql[i]);
                }
                stringBuilder.Append("ALTER");
                for (int i = m.Groups[1].Index + m.Groups[1].Length; i < sql.Length; i++) {
                    stringBuilder.Append(sql[i]);
                }
            }
            return new SqlEditorDto() { JsMode = _provider.GetJsMode(), SqlType = _provider.GetServerName(), SqlString = stringBuilder.ToString() };
        }
        public string GetProcedureDefinition(string connStr, string databaseName, string schemaName, string procedureName)
        {
            var helper = SqlHelperFactory.OpenDatabase(connStr, _provider.GetProviderFactory(), SqlType.SqlServer);
            helper.ChangeDatabase(databaseName);
            var sql2 = @"SELECT OBJECT_DEFINITION (pr.object_id)
FROM sys.objects pr    
WHERE SCHEMA_NAME(pr.schema_id)=@0 and pr.name=@1";
            var dt2 = helper.FirstOrDefault<string>(sql2, schemaName, procedureName);
            helper.Dispose();
            return dt2;
        }


    }

}
