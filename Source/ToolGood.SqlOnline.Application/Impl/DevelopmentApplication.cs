/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolGood.SqlOnline.Application.Admins;
using ToolGood.SqlOnline.Datas;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.Dtos.CodeGens;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Application.Impl
{
    public class DevelopmentApplication : ApplicationBase, IDevelopmentApplication
    {

        public async Task<bool> UseDevelopment()
        {
            var helper = GetReadHelper();
            var setting = await helper.FirstOrDefault_Async<DbSysSettingValue>("where Code='UseDevelopment' and IsDelete=0");
            if (setting == null) {
                return false;
            }
            return setting.Value == "1";
        }

        public Task<List<SqlConnDto>> GetConnList()
        {
            var helper = GetReadHelper();
            return helper.Where<DbSqlConn>()
                .Where(q => q.IsDelete == false)
                .Where(q => q.UseDevelopment)
                .OrderBy(q => q.Name)
                .Select_Async<SqlConnDto>();
        }

        public async Task<List<DatabaseEntity>> GetDatabaseNames(int connId)
        {
            const string sql = @"select SqlType,ConnectionString from SqlConn where Id=@0 and IsDelete=0";
            var helper = GetReadHelper();
            var connDto = await helper.FirstOrDefault_Async<SqlConnDto>(sql, connId);
            if (connDto == null) {
                return new List<DatabaseEntity>();
            }
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.GetDatabases(connDto.ConnectionString);
        }

        public async Task<List<TableShowEntity>> GetTableShowColumns(int connId, string databaseName)
        {
            const string sql = @"select SqlType,ConnectionString from SqlConn where Id=@0 and IsDelete=0";
            var helper = GetReadHelper();
            var connDto = await helper.FirstOrDefault_Async<SqlConnDto>(sql, connId);
            if (connDto == null) {
                return new List<TableShowEntity>();
            }
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.GetTableShowList(connDto.ConnectionString, databaseName);
        }


        public async Task<List<TableEntity>> GetTables(int connId, string databaseName)
        {
            const string sql = @"select SqlType,ConnectionString from SqlConn where Id=@0 and IsDelete=0";
            var helper = GetReadHelper();
            var connDto = await helper.FirstOrDefault_Async<SqlConnDto>(sql, connId);
            if (connDto == null) {
                return new List<TableEntity>();
            }
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.GetTables(connDto.ConnectionString, databaseName);
        }

        public async Task<List<ViewEntity>> GetViews(int connId, string databaseName)
        {
            const string sql = @"select SqlType,ConnectionString from SqlConn where Id=@0 and IsDelete=0";
            var helper = GetReadHelper();
            var connDto = await helper.FirstOrDefault_Async<SqlConnDto>(sql, connId);
            if (connDto == null) {
                return new List<ViewEntity>();
            }
            var provider = SqlCache.Instance.GetSqlViewProvider(connDto.SqlType);
            return provider.GetViews(connDto.ConnectionString, databaseName);
        }

        public async Task<List<ProcedureEntity>> GetProcedures(int connId, string databaseName)
        {
            const string sql = @"select SqlType,ConnectionString from SqlConn where Id=@0 and IsDelete=0";
            var helper = GetReadHelper();
            var connDto = await helper.FirstOrDefault_Async<SqlConnDto>(sql, connId);
            if (connDto == null) {
                return new List<ProcedureEntity>();
            }
            var provider = SqlCache.Instance.GetSqlProcedureProvider(connDto.SqlType);
            return provider.GetProcedures(connDto.ConnectionString, databaseName);
        }

        public Task<List<SqlCodeGenDto>> GetSqlCodeGens()
        {
            var helper = GetReadHelper();
            return helper.Where<DbSqlCodeGen>(q => q.IsDelete == false)
                       .Select_Async<SqlCodeGenDto>();

        }

        public Task<string> GetSqlCodeGenTpl(int id)
        {
            var helper = GetReadHelper();
            return helper.FirstOrDefault_Async<string>("select Content from SqlCodeGen where Id=@0 and IsDelete=0", id);
        }


        #region GetProcedureInfo GetTableInfo GetViewInfo

        public async Task<ProcedureInfo> GetProcedureInfo(int connId, string databaseName, string schemaName, string name)
        {
            const string sql = @"select Name,SqlType,ConnectionString from SqlConn where Id=@0 and IsDelete=0";
            var helper = GetReadHelper();
            var connDto = await helper.FirstOrDefault_Async<SqlConnDto>(sql, connId);
            if (connDto == null) {
                return new ProcedureInfo();
            }
            var provider = SqlCache.Instance.GetSqlProcedureProvider(connDto.SqlType);
            var procedure = provider.GetProcedure(connDto.ConnectionString, databaseName, schemaName, name);
            var procedureParams = provider.GetProcedureParams(connDto.ConnectionString, databaseName, schemaName, name);
            ProcedureInfo procedureInfo = new ProcedureInfo() {
                AdminName = "System",
                ServerName = connDto.Name,
                ServerType = connDto.SqlType,
                DatabaseName = databaseName,
                SchemaName = schemaName,
                ProcedureName = procedure.ProcedureName,
                Comment = procedure.Comment,
                OperationTime = System.DateTime.Now,
                Params=new List<ProcedureParameter>()
            };
            for (int i = 0; i < procedureParams.Count; i++) {
                var column = procedureParams[i];
                procedureInfo.Params.Add(new ProcedureParameter() {
                    Index = i,
                    ParamName= column.ParamName,
                    Type = column.Type,
                    Length = column.Length,
                    Precision = column.Precision,
                    Scale = column.Scale,
                    IsOutput=column.IsOutput
                });
            }
            return procedureInfo;
        }

        public async Task<TableViewInfo> GetTableInfo(int connId, string databaseName, string schemaName, string name)
        {
            const string sql = @"select Name,SqlType,ConnectionString from SqlConn where Id=@0 and IsDelete=0";
            var helper = GetReadHelper();
            var connDto = await helper.FirstOrDefault_Async<SqlConnDto>(sql, connId);
            if (connDto == null) {
                return new TableViewInfo();
            }
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            var tableEntity = provider.GetTable(connDto.ConnectionString, databaseName, schemaName, name);
            var tableColumns = provider.GetTableColumns(connDto.ConnectionString, databaseName, schemaName, name);
            TableViewInfo tableInfo = new TableViewInfo() {
                AdminName = "System",
                ServerName = connDto.Name,
                ServerType = connDto.SqlType,
                DatabaseName = databaseName,
                SchemaName = schemaName,
                TableName = tableEntity.TableName,
                Comment = tableEntity.Comment,
                OperationTime = System.DateTime.Now,
                Columns = new List<TableColumnInfo>(),
            };
            for (int i = 0; i < tableColumns.Count; i++) {
                var column = tableColumns[i];
                tableInfo.Columns.Add(new TableColumnInfo() {
                    Index = i,
                    ColumnName = column.ColumnName,
                    Comment = column.Comment,
                    Type = column.Type,
                    Length = column.Length,
                    Precision = column.Precision,
                    Scale = column.Scale,
                    IsPrimaryKey = column.IsPrimaryKey,
                    IsIdentity = column.IsIdentity,
                    IsNullAble = column.IsNullAble,
                    DefaultValue = column.DefaultValue
                });
            }
            return tableInfo;
        }

        public async Task<TableViewInfo> GetViewInfo(int connId, string databaseName, string schemaName, string name)
        {
            const string sql = @"select Name,SqlType,ConnectionString from SqlConn where Id=@0 and IsDelete=0";
            var helper = GetReadHelper();
            var connDto = await helper.FirstOrDefault_Async<SqlConnDto>(sql, connId);
            if (connDto == null) {
                return new TableViewInfo();
            }
            var provider = SqlCache.Instance.GetSqlViewProvider(connDto.SqlType);
            var view = provider.GetView(connDto.ConnectionString, databaseName, schemaName, name);
            var viewColumns = provider.GetViewColumns(connDto.ConnectionString, databaseName, schemaName, name);

            TableViewInfo tableInfo = new TableViewInfo() {
                AdminName = "System",
                ServerName = connDto.Name,
                ServerType = connDto.SqlType,
                DatabaseName = databaseName,
                SchemaName = schemaName,
                TableName = name,
                Comment = view.Comment,
                OperationTime = System.DateTime.Now,
                Columns = new List<TableColumnInfo>()
            };
            for (int i = 0; i < viewColumns.Count; i++) {
                var column = viewColumns[i];
                tableInfo.Columns.Add(new TableColumnInfo() {
                    Index = i,
                    ColumnName = column.ColumnName,
                    Comment = column.Comment,
                    Type = column.Type,
                    Length = column.Length,
                    Precision = column.Precision,
                    Scale = column.Scale,
                    IsNullAble = column.IsNullAble,
                });
            }
            return tableInfo;
        }

        #endregion


    }
}
