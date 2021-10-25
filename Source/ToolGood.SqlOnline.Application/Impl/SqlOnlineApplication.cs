/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using ToolGood.Common.Utils;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Application.Admins;
using ToolGood.SqlOnline.Configs;
using ToolGood.SqlOnline.Datas;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Datas.Enums;
using ToolGood.SqlOnline.Dto;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.Dtos.CodeGens;
using ToolGood.SqlOnline.IPlugin;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Application.Impl
{
    public class SqlOnlineApplication : ApplicationBase, ISqlOnlineApplication
    {
        private readonly IHttpClientFactory _httpClientFactory;
        class TempCommand
        {
            public DbCommand Command;
            public string SqlType;
            public string Name;
            public int ReadMaxRows;
            public int ChangeMaxRows;
            public DateTime CreateTime;
            public int Authority;
        }
        private static Dictionary<string, TempCommand> _commandDict = new Dictionary<string, TempCommand>();

        public SqlOnlineApplication(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private Task<SqlConnDto> GetConn(int adminId, int connId)
        {
            const string sql = @"select conn.Id,conn.Name,conn.SqlType,conn.ConnectionString,ReadMaxRows
from SqlConn conn
inner join SqlConnPass pass on pass.ConnId=conn.Id
inner join SysAdmin_Group g on g.GroupId=pass.AdminGroupId
where conn.IsDelete=0 and pass.IsDelete=0 and g.AdminId=@0 and conn.Id=@1
limit 1
";
            var helper = GetReadHelper();
            return helper.FirstOrDefault_Async<SqlConnDto>(sql, adminId, connId);
        }

        #region 结构

        public Task<List<SqlConnDto>> GetConnList(SqlSearchDto request)
        {
            const string sql = @"select DISTINCT conn.Id,conn.Name,conn.SqlType
from SqlConn conn
inner join SqlConnPass pass on pass.ConnId=conn.Id
inner join SysAdmin_Group g on g.GroupId=pass.AdminGroupId
where conn.IsDelete=0 and pass.IsDelete=0 and g.AdminId=@0 and pass.CanRead=1
order by conn.Name asc
";
            var helper = GetReadHelper();
            return helper.Select_Async<SqlConnDto>(sql, request.AdminId);
        }

        public async Task<List<DatabaseEntity>> GetDatabaseNames(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.GetDatabases(connDto.ConnectionString);
        }

        public async Task<List<TableEntity>> GetTableNames(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.GetTables(connDto.ConnectionString, request.Database);
        }
        public async Task<List<TableColumnEntity>> GetTableColumns(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.GetTableColumns(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }

        public async Task<List<ViewEntity>> GetViewNames(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlViewProvider(connDto.SqlType);
            return provider.GetViews(connDto.ConnectionString, request.Database);
        }
        public async Task<List<ViewColumnEntity>> GetViewColumns(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlViewProvider(connDto.SqlType);
            return provider.GetViewColumns(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }

        public async Task<List<ProcedureEntity>> GetProcedureNames(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlProcedureProvider(connDto.SqlType);
            return provider.GetProcedures(connDto.ConnectionString, request.Database);
        }
        public async Task<List<ProcedureParamEntity>> GetProcedureParams(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlProcedureProvider(connDto.SqlType);
            return provider.GetProcedureParams(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }

        public async Task<List<FunctionEntity>> GetFunctionNames(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlFunctionProvider(connDto.SqlType);
            return provider.GetFunctions(connDto.ConnectionString, request.Database);
        }
        public async Task<List<FunctionParamEntity>> GetFunctionParams(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlFunctionProvider(connDto.SqlType);
            return provider.GetFunctionParams(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }


        public async Task<List<TableShowEntity>> GetTableShowColumns(int adminId, int connId, string databaseName)
        {
            var connDto = await GetConn(adminId, connId);
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.GetTableShowList(connDto.ConnectionString, databaseName);
        }
        #endregion

        #region 文档操作
        public async Task<bool> AddSqlDoc(Req<SqlDocDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }

            var helper = GetWriteHelper();
            DbSqlDoc doc = new DbSqlDoc() {
                AdminId = request.OperatorId,
                SqlConnId = request.Data.SqlConnId,
                IsShare = request.Data.IsShare,
                DatabaseName = request.Data.DatabaseName,
                Content = request.Data.Content,
                ContentLen = request.Data.Content.Length,
                Title = request.Data.Title,

                LastOpenTime = DateTime.Now,
                AddingAdminId = request.OperatorId,
                AddingTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
                ModifyTime = DateTime.Now
            };
            await helper.Insert_Async(doc);
            request.Data.Id = doc.Id;
            return true;
        }

        public async Task<bool> EditSqlDoc(Req<SqlDocDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            var helper = GetWriteHelper();

            const string sql = @"update SqlDoc set DatabaseName=@0,Content=@1,ContentLen=@2,Title=@3,LastOpenTime=@4,ModifyAdminId=@5,ModifyTime=@6 
where IsDelete=0 and Id=@7";
            var count = await helper.Execute_Async(sql, request.Data.DatabaseName, request.Data.Content, request.Data.Content.Length, request.Data.Title,
                DateTime.Now, request.OperatorId, DateTime.Now, request.Data.Id);

            return count > 0;
        }

        public async Task<bool> DeleteSqlDoc(Req<AdminIdDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Id == null) { throw new ArgumentException("Data.Id is null"); }

            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSqlDoc>(new { IsDelete = true, ModifyTime = DateTime.Now, ModifyAdminId = request.OperatorId }
                                                            , new { Id = request.Data.Id, IsDelete = false });
            if (count > 0) {
                return true;
            } else {
                request.Message = "失败，数据库链接字符串不存在。";
                return false;
            }
        }

        public async Task<bool> AddSqlDocTemp(Req<SqlDocDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            var helper = GetWriteHelper();

            var count = await helper.FirstOrDefault_Async<int>("select count(*) from SqlDocTemp where AdminId=@0", request.OperatorId);
            if (count == 1000) {
                await helper.Execute_Async(@"delete from SqlDocTemp where Id=(select Id from SqlDocTemp 
where AdminId=@0 order by ModifyTime asc limit 1)", request.OperatorId);
            }
            var doc = new DbSqlDocTemp() {
                AdminId = request.OperatorId,
                SqlDocId = request.Data.SqlDocId,
                SqlConnId = request.Data.SqlConnId,
                DatabaseName = request.Data.DatabaseName,
                Content = request.Data.Content,
                ContentLen = request.Data.Content.Length,
                Title = request.Data.Title,

                AddingAdminId = request.OperatorId,
                AddingTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
                ModifyTime = DateTime.Now
            };
            await helper.Insert_Async(doc);
            request.Data.Id = doc.Id;
            return true;
        }
        public async Task<bool> EditSqlDocTemp(Req<SqlDocDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            var helper = GetWriteHelper();

            const string sql = @"update SqlDocTemp set DatabaseName=@0,Content=@1,ContentLen=@2,Title=@3,ModifyAdminId=@4,ModifyTime=@5 
where IsDelete=0 and Id=@6";
            var count = await helper.Execute_Async(sql, request.Data.DatabaseName, request.Data.Content, request.Data.Content.Length, request.Data.Title,
                request.OperatorId, DateTime.Now, request.Data.Id);

            return count > 0;
        }

        public async Task<bool> DeleteSqlDocTemp(Req<AdminIdDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Id == null) { throw new ArgumentException("Data.Id is null"); }

            var helper = GetWriteHelper();
            var count = await helper.Delete_Async<DbSqlDocTemp>("where AdminId=@0 and Id=@1", request.OperatorId, request.Data.Id);

            if (count > 0) {
                return true;
            } else {
                request.Message = "失败，临时文档不存在。";
                return false;
            }
        }

        public async Task<bool> OpenSqlDoc(int adminId, int id)
        {
            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSqlDoc>("set LastOpenTime=@2 where Id=@0 and AdminId=@1 ", id, adminId, DateTime.Now);
            return count > 0;
        }

        public Task<Page<AdminSqlDocDto>> GetSelfSqlDocList(AdminSqlDocSearchDto request)
        {
            const string selectSql = @"select doc.Id,a.TrueName AdminTrueName,doc.LastOpenTime,0 SqlDocType
,doc.Title,doc.ContentLen,conn.Name SqlConnName,doc.DatabaseName,conn.SqlType,doc.ModifyTime";
            const string tableSql = @"from SqlDoc doc
inner join SysAdmin a on a.Id=doc.AdminId
inner join SqlConn conn on conn.Id=doc.SqlConnId";
            string whereSql = @"where doc.IsDelete=0 and a.IsDelete=0 and conn.IsDelete=0 and a.Id=@AdminId ";

            SqlParameterCollection sqlParameters = new SqlParameterCollection();
            sqlParameters.Add("AdminId", request.AdminId);

            if (string.IsNullOrEmpty(request.Title) == false) {
                whereSql += "and doc.Title like @Title ";
                sqlParameters.Add("Title", "%" + request.Title + "%");
            }
            if (string.IsNullOrEmpty(request.SqlConnName) == false) {
                whereSql += "and conn.Name like @SqlConnName ";
                sqlParameters.Add("SqlConnName", "%" + request.SqlConnName + "%");
            }

            var orderSql = " doc.LastOpenTime desc ";
            if (string.IsNullOrWhiteSpace(request.Field) == false) {
                orderSql = $" {request.Field} {request.Order}";
            }

            var helper = GetReadHelper();
            return helper.SQL_Page_Async<AdminSqlDocDto>(request.PageIndex, request.PageSize, selectSql, tableSql
                , orderSql, whereSql, sqlParameters);
        }
        public Task<Page<AdminSqlDocDto>> GetSelfSqlDocTempList(AdminSqlDocSearchDto request)
        {
            const string selectSql = @"select doc.Id,a.TrueName AdminTrueName,1 SqlDocType
,doc.Title,doc.ContentLen,conn.Name SqlConnName,doc.DatabaseName,conn.SqlType,doc.ModifyTime";
            const string tableSql = @"from SqlDocTemp doc
inner join SysAdmin a on a.Id=doc.AdminId
inner join SqlConn conn on conn.Id=doc.SqlConnId";
            string whereSql = @"where doc.IsDelete=0 and a.IsDelete=0 and conn.IsDelete=0 and a.Id=@AdminId ";

            SqlParameterCollection sqlParameters = new SqlParameterCollection();
            sqlParameters.Add("AdminId", request.AdminId);

            if (string.IsNullOrEmpty(request.Title) == false) {
                whereSql += "and doc.Title like @Title ";
                sqlParameters.Add("Title", "%" + request.Title + "%");
            }
            if (string.IsNullOrEmpty(request.SqlConnName) == false) {
                whereSql += "and conn.Name like @SqlConnName ";
                sqlParameters.Add("SqlConnName", "%" + request.SqlConnName + "%");
            }

            var orderSql = " doc.ModifyTime desc ";
            if (string.IsNullOrWhiteSpace(request.Field) == false) {
                orderSql = $" {request.Field} {request.Order}";
            }

            var helper = GetReadHelper();
            return helper.SQL_Page_Async<AdminSqlDocDto>(request.PageIndex, request.PageSize, selectSql, tableSql
                , orderSql, whereSql, sqlParameters);
        }
        public Task<Page<AdminSqlDocDto>> GetShareSqlDocList(AdminSqlDocSearchDto request)
        {
            const string selectSql = @"select doc.Id,a.TrueName AdminTrueName,2 SqlDocType
,doc.Title,doc.ContentLen,conn.Name SqlConnName,doc.DatabaseName,conn.SqlType,doc.ModifyTime";
            const string tableSql = @"from SqlDoc doc
inner join SysAdmin a on a.Id=doc.AdminId
inner join SqlConn conn on conn.Id=doc.SqlConnId";
            string whereSql = @"where doc.IsDelete=0 and a.IsDelete=0 and conn.IsDelete=0 and doc.IsShare=1 ";

            SqlParameterCollection sqlParameters = new SqlParameterCollection();

            if (string.IsNullOrEmpty(request.Title) == false) {
                whereSql += "and doc.Title like @Title ";
                sqlParameters.Add("Title", "%" + request.Title + "%");
            }
            if (string.IsNullOrEmpty(request.SqlConnName) == false) {
                whereSql += "and conn.Name like @SqlConnName ";
                sqlParameters.Add("SqlConnName", "%" + request.SqlConnName + "%");
            }

            var orderSql = " doc.ModifyTime desc ";
            if (string.IsNullOrWhiteSpace(request.Field) == false) {
                orderSql = $" {request.Field} {request.Order}";
            }

            var helper = GetReadHelper();
            return helper.SQL_Page_Async<AdminSqlDocDto>(request.PageIndex, request.PageSize, selectSql, tableSql
                , orderSql, whereSql, sqlParameters);
        }

        public Task<SqlDocDto> GetSelfSqlDoc(int adminId, int id)
        {
            const string sql = @"select doc.Id,doc.Id SqlDocId,conn.Id SqlConnId,doc.DatabaseName,doc.Title,doc.Content,conn.SqlType
from SqlDoc doc
inner join SysAdmin a on a.Id=doc.AdminId
inner join SqlConn conn on conn.Id=doc.SqlConnId 
where doc.IsDelete=0 and a.IsDelete=0 and conn.IsDelete=0 and a.Id=@0 and doc.Id=@1 ";

            var helper = GetReadHelper();
            return helper.FirstOrDefault_Async<SqlDocDto>(sql, adminId, id);
        }
        public Task<SqlDocDto> GetSelfSqlDocTemp(int adminId, int id)
        {
            const string sql = @"select doc.Id,doc.SqlDocId SqlDocId,conn.Id SqlConnId,doc.DatabaseName,doc.Title,doc.Content,conn.SqlType
from SqlDocTemp doc
inner join SysAdmin a on a.Id=doc.AdminId
inner join SqlConn conn on conn.Id=doc.SqlConnId 
where doc.IsDelete=0 and a.IsDelete=0 and conn.IsDelete=0 and a.Id=@0 and doc.Id=@1 ";

            var helper = GetReadHelper();
            return helper.FirstOrDefault_Async<SqlDocDto>(sql, adminId, id);
        }
        public Task<SqlDocDto> GetShareSqlDoc(int adminId, int id)
        {
            const string sql = @"select doc.Id,doc.Id SqlDocId,conn.Id SqlConnId,doc.DatabaseName,doc.Title,doc.Content,conn.SqlType
from SqlDoc doc
inner join SysAdmin a on a.Id=doc.AdminId
inner join SqlConn conn on conn.Id=doc.SqlConnId 
where doc.IsDelete=0 and a.IsDelete=0 and conn.IsDelete=0 and doc.IsShare=1 and doc.Id=@0 ";

            var helper = GetReadHelper();
            return helper.FirstOrDefault_Async<SqlDocDto>(sql, id);
        }

        #endregion

        #region Create/Get Sql
        public SqlEditorDto GetSql_New(string sqlType)
        {
            var provider = SqlCache.Instance.GetSqlCommonProvider(sqlType);
            return provider.GetNew();
        }

        public async Task<SqlEditorDto> GetSql_New(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.GetNew();
        }

        public Task<SqlEditorDto> GetSql_TableSelect100(SqlSearchDto request)
        {
            return GetSql_TableSelect100(request.AdminId, request.SqlConnId, request.Database, request.Schema, request.Search);
        }

        public async Task<SqlEditorDto> GetSql_TableSelect100(int adminId, int connId, string databaseName, string schemaName, string tableName)
        {
            var connDto = await GetConn(adminId, connId);
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.GetSelect100(connDto.ConnectionString, databaseName, schemaName, tableName);
        }
        public async Task<SqlEditorDto> GetSql_TableInsert(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.GetInsertSql(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }
        public async Task<SqlEditorDto> GetSql_TableUpdate(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.GetUpdateSql(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }
        public async Task<SqlEditorDto> GetSql_TableDelete(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.GetDeleteSql(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }

        public async Task<SqlEditorDto> GetSql_ViewSelect100(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlViewProvider(connDto.SqlType);
            return provider.GetSelect100(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }
        public async Task<SqlEditorDto> GetSql_ViewCreate(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlViewProvider(connDto.SqlType);
            return provider.GetCreateSql(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }
        public async Task<SqlEditorDto> GetSql_ViewAlter(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlViewProvider(connDto.SqlType);
            return provider.GetAlterSql(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }
        public async Task<SqlEditorDto> GetSql_FunctionSelectSql(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlFunctionProvider(connDto.SqlType);
            return provider.GetSelectSql(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }
        public async Task<SqlEditorDto> GetSql_FunctionCreate(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlFunctionProvider(connDto.SqlType);
            return provider.GetCreateSql(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }
        public async Task<SqlEditorDto> GetSql_FunctionAlter(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlFunctionProvider(connDto.SqlType);
            return provider.GetAlterSql(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }

        public async Task<SqlEditorDto> GetSql_ProcedureExecuteSql(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlProcedureProvider(connDto.SqlType);
            return provider.GetExecuteSql(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }
        public async Task<SqlEditorDto> GetSql_ProcedureCreate(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlProcedureProvider(connDto.SqlType);
            return provider.GetCreateSql(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }
        public async Task<SqlEditorDto> GetSql_ProcedureAlter(SqlSearchDto request)
        {
            var connDto = await GetConn(request.AdminId, request.SqlConnId);
            var provider = SqlCache.Instance.GetSqlProcedureProvider(connDto.SqlType);
            return provider.GetAlterSql(connDto.ConnectionString, request.Database, request.Schema, request.Search);
        }

        #endregion

        #region ChangeComment
        public async Task<bool> ChangeComment_Table(int adminId, int connId, string databaseName, string schemaName, string tableName, string comment)
        {
            var connDto = await GetConn(adminId, connId);
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.ChangeComment(connDto.ConnectionString, databaseName, schemaName, tableName, comment);
        }

        public async Task<bool> ChangeComment_TableColumn(int adminId, int connId, string databaseName, string schemaName, string tableName, string columnName, string comment)
        {
            var connDto = await GetConn(adminId, connId);
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            return provider.ChangeComment(connDto.ConnectionString, databaseName, schemaName, tableName, columnName, comment);
        }

        #endregion

        #region ExecuteSql

        public async Task<string> CreateCommand(Req<ExecuteSqlDto> request)
        {
            var helper = GetReadHelper();
            if (new int[] { 1, 2, 3, 5 }.Contains(request.Data.Authority)) {
                var admin = await helper.FirstOrDefault_Async<DbSysAdmin>(new { Id = request.Data.AdminId, IsDelete = false, IsFrozen = false });
                if (admin == null) { return "ERROR:当前用户不存在！"; }
                var oldp = DataUtil.CreatePassword(admin.Name, admin.Salt, request.Data.Password);
                if (oldp != admin.ManagerPassword) {
                    return "ERROR:密码错误！";
                }
            }
            string sql = @"select conn.Id,conn.Name,conn.SqlType,conn.ConnectionString,pass.ReadMaxRows,pass.ChangeMaxRows
from SqlConn conn
inner join SqlConnPass pass on pass.ConnId=conn.Id
inner join SysAdmin_Group g on g.GroupId=pass.AdminGroupId
where conn.IsDelete=0 and pass.IsDelete=0 and g.AdminId=@0 and conn.Id=@1 ";
            if (request.Data.Authority == 1) {
                sql += " and pass.CanEdit=1";
            } else if (request.Data.Authority == 2) {
                sql += " and pass.CanDelete=1";
            } else if (request.Data.Authority == 3) {
                sql += " and pass.AllPermissions=1";
            } else if (request.Data.Authority == 4) {
                sql += " and pass.CanDownload=1";
            } else if (request.Data.Authority == 5) {
                sql += " and pass.CanDownload=1 and pass.AllPermissions=1";
            }
            sql += " limit 1";
            var connDto = await helper.FirstOrDefault_Async<SqlConnDto>(sql, request.Data.AdminId, request.Data.SqlConnId);

            if (connDto == null) {
                return "ERROR:该数据库无法访问！";
            }
            var provider = SqlCache.Instance.GetSqlCommonProvider(connDto.SqlType);
            var cmd = provider.CreateCommand(connDto.ConnectionString, request.Data.Database);
            TempCommand tempCommand = new TempCommand() {
                Command = cmd,
                ReadMaxRows = connDto.ReadMaxRows,
                ChangeMaxRows = connDto.ChangeMaxRows,
                SqlType = connDto.SqlType,
                CreateTime = DateTime.Now,
                Name = connDto.Name,
                Authority = request.Data.Authority
            };
            var key = Guid.NewGuid().ToString();
            _commandDict[key] = tempCommand;
            return key;
        }
        public bool StopCommand(Req<ExecuteSqlDto> request)
        {
            if (request.Data.Key != null) {
                if (_commandDict.TryGetValue(request.Data.Key, out TempCommand command)) {
                    _commandDict.Remove(request.Data.Key);
                    if (command.Command != null) {
                        var conn = command.Command.Connection;
                        command.Command.Cancel();
                        command.Command.Dispose();
                        command.Command = null;
                        if (conn != null) {
                            conn.Dispose();
                        }
                        return true;
                    }
                    command = null;
                }
            }
            return false;
        }

        public async Task<ExecuteResult> ExecuteSql_Select(Req<ExecuteSqlDto> request)
        {
            if (string.IsNullOrWhiteSpace(request.Data.Key) == false && _commandDict.TryGetValue(request.Data.Key, out TempCommand command)) {
                try {
                    var provider = SqlCache.Instance.GetSqlCommonProvider(command.SqlType);
                    if (command.Command != null) {
                        var startTime = DateTime.Now;
                        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                        var result = await provider.ExecuteSql_Select(command.Command, request.Data.Sql, command.ReadMaxRows);
                        stopwatch.Stop();
                        try {
                            if (result.Exception != null) {
                                await WriteExecuteSelectLog(request, command.Name, startTime, EnumRunReadResult.Fail, (int)stopwatch.ElapsedMilliseconds, result.Exception);
                            } else {
                                await WriteExecuteSelectLog(request, command.Name, startTime, EnumRunReadResult.Success, (int)stopwatch.ElapsedMilliseconds);
                            }
                        } catch (Exception ex) { }
                        return result;
                    }
                } finally {
                    _commandDict.Remove(request.Data.Key);
                    if (command.Command != null) {
                        var conn = command.Command.Connection;
                        command.Command.Dispose();
                        command.Command = null;
                        if (conn != null) {
                            conn.Dispose();
                        }
                    }
                    command = null;
                }
            }
            return new ExecuteResult() { StartTime = DateTime.Now, Exception = "无法打开连接！" };
        }

        public async Task<ExecuteResult> ExecuteSql_InsertUpdate(Req<ExecuteSqlDto> request)
        {
            if (string.IsNullOrWhiteSpace(request.Data.Key) == false && _commandDict.TryGetValue(request.Data.Key, out TempCommand command)) {
                try {
                    if (command.Authority != request.Data.Authority) {
                        return new ExecuteResult() { StartTime = DateTime.Now, Exception = "权限不一致！" };
                    }

                    var provider = SqlCache.Instance.GetSqlCommonProvider(command.SqlType);
                    if (command.Command != null) {
                        var startTime = DateTime.Now;
                        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                        var result = await provider.ExecuteSql_InsertUpdate(command.Command, request.Data.Sql, command.ReadMaxRows, command.ChangeMaxRows);
                        stopwatch.Stop();
                        try {
                            await WriteExecuteLog(request, command.Name, startTime, result, (int)stopwatch.ElapsedMilliseconds);
                        } catch (Exception ex) { }
                        return result;
                    }

                } finally {
                    _commandDict.Remove(request.Data.Key);
                    if (command.Command != null) {
                        var conn = command.Command.Connection;
                        command.Command.Dispose();
                        command.Command = null;
                        if (conn != null) {
                            conn.Dispose();
                        }
                    }
                    command = null;
                }
            }
            return new ExecuteResult() { StartTime = DateTime.Now, Exception = "无法打开连接！" };
        }
        public async Task<ExecuteResult> ExecuteSql_InsertUpdateDelete(Req<ExecuteSqlDto> request)
        {
            if (string.IsNullOrWhiteSpace(request.Data.Key) == false && _commandDict.TryGetValue(request.Data.Key, out TempCommand command)) {
                try {
                    if (command.Authority != request.Data.Authority) {
                        return new ExecuteResult() { StartTime = DateTime.Now, Exception = "权限不一致！" };
                    }

                    var provider = SqlCache.Instance.GetSqlCommonProvider(command.SqlType);
                    if (command.Command != null) {
                        var startTime = DateTime.Now;
                        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                        var result = await provider.ExecuteSql_Delete(command.Command, request.Data.Sql, command.ReadMaxRows, command.ChangeMaxRows);
                        stopwatch.Stop();
                        try {
                            await WriteExecuteLog(request, command.Name, startTime, result, (int)stopwatch.ElapsedMilliseconds);
                        } catch (Exception ex) { }
                        return result;
                    }
                } finally {
                    _commandDict.Remove(request.Data.Key);
                    if (command.Command != null) {
                        var conn = command.Command.Connection;
                        command.Command.Dispose();
                        command.Command = null;
                        if (conn != null) {
                            conn.Dispose();
                        }
                    }
                    command = null;
                }
            }
            return new ExecuteResult() { StartTime = DateTime.Now, Exception = "无法打开连接！" };
        }
        public async Task<ExecuteResult> ExecuteSql_AllPermissions(Req<ExecuteSqlDto> request)
        {
            if (string.IsNullOrWhiteSpace(request.Data.Key) == false && _commandDict.TryGetValue(request.Data.Key, out TempCommand command)) {
                try {
                    if (command.Authority != request.Data.Authority) {
                        return new ExecuteResult() { StartTime = DateTime.Now, Exception = "权限不一致！" };
                    }

                    var provider = SqlCache.Instance.GetSqlCommonProvider(command.SqlType);
                    if (command.Command != null) {
                        var startTime = DateTime.Now;
                        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                        var result = await provider.ExecuteSql_AllPermissions(command.Command, request.Data.Sql);
                        stopwatch.Stop();
                        try {
                            await WriteExecuteLog(request, command.Name, startTime, result, (int)stopwatch.ElapsedMilliseconds);
                        } catch (Exception ex) { }
                        return result;
                    }
                } finally {
                    _commandDict.Remove(request.Data.Key);
                    if (command.Command != null) {
                        var conn = command.Command.Connection;
                        command.Command.Dispose();
                        command.Command = null;
                        if (conn != null) {
                            conn.Dispose();
                        }
                    }
                    command = null;
                }
            }
            return new ExecuteResult() { StartTime = DateTime.Now, Exception = "无法打开连接！" };
        }


        private async Task WriteExecuteLog(Req<ExecuteSqlDto> request, string SqlConnectionName, DateTime addingTime, ExecuteResult result, int runTime)
        {
            var helper = GetReadHelper();
            var admin = await helper.Where<DbSysAdmin>(q => q.IsDelete == false).Where(q => q.Id == request.OperatorId).FirstOrDefault_Async();
            var setttings = await helper.Select_Async<DbSqlQueryLogSetting>("where IsDelete=0 and IsFrozen=0 ");
            helper.Dispose();

            DbSqlExecuteLog executeLog = new DbSqlExecuteLog() {
                AddingTime = addingTime,
                AdminId = admin.Id,
                AdminJobNo = admin.JobNo,
                AdminName = admin.Name,
                AdminPhone = admin.Phone,
                AdminTrueName = admin.TrueName,
                SqlConnectionId = request.Data.SqlConnId,
                SqlConnectionName = SqlConnectionName,
                DatabaseName = request.Data.Database,
                Sql = request.Data.Sql,
                RunReadResult = string.IsNullOrEmpty(result.Exception) ? EnumRunReadResult.Success : EnumRunReadResult.Fail,
                RunTime = runTime,
                Exception = result.Exception,
                Items = new List<DbSqlExecuteLogItem>()
            };
            if (result.Result != null) {
                foreach (var item in result.Result) {
                    if (string.IsNullOrEmpty(item.OldData) == false) {
                        var it = new DbSqlExecuteLogItem() { OldData = item.OldData, Sql = item.Sql, };
                        executeLog.Items.Add(it);
                    }
                }
            }
            foreach (var item in setttings) {
                if (item.LogType == 1) {
                    var logHelper = Config.GetLogSqlHelper(DateTime.Now);
                    WriteDbSqlExecuteLog(logHelper, executeLog);
                    logHelper.Dispose();
                } else if (item.LogType == 2) {
                    await PostContent(item.Data, executeLog.ToJson());
                } else if (item.LogType == 3) {
                    var logHelper = SqlHelperFactory.OpenDatabase(item.Data, SqlType.SqlServer);
                    WriteDbSqlExecuteLog(logHelper, executeLog);
                    logHelper.Dispose();
                } else if (item.LogType == 4) {
                    var logHelper = SqlHelperFactory.OpenDatabase(item.Data, SqlType.MySql);
                    WriteDbSqlExecuteLog(logHelper, executeLog);
                    logHelper.Dispose();
                }
            }
        }
        private void WriteDbSqlExecuteLog(SqlHelper helper, DbSqlExecuteLog executeLog)
        {
            helper.Insert(executeLog);
            var id = executeLog.Id;
            foreach (var item in executeLog.Items) {
                item.SqlExecuteLogId = id;
                helper.Insert(item);
            }
        }


        private async Task WriteExecuteSelectLog(Req<ExecuteSqlDto> request, string SqlConnectionName, DateTime addingTime, EnumRunReadResult result, int runTime, string exception = null)
        {
            var helper = GetReadHelper();
            var admin = await helper.Where<DbSysAdmin>(q => q.IsDelete == false).Where(q => q.Id == request.OperatorId).FirstOrDefault_Async();
            var setttings = await helper.Select_Async<DbSqlQueryLogSetting>("where IsDelete=0 and IsFrozen=0 ");
            helper.Dispose();

            DbSqlQueryLog readLog = new DbSqlQueryLog() {
                AddingTime = addingTime,
                AdminId = admin.Id,
                AdminJobNo = admin.JobNo,
                AdminName = admin.Name,
                AdminPhone = admin.Phone,
                AdminTrueName = admin.TrueName,
                SqlConnectionId = request.Data.SqlConnId,
                SqlConnectionName = SqlConnectionName,
                DatabaseName = request.Data.Database,
                Sql = request.Data.Sql,
                RunReadResult = result,
                RunTime = runTime,
                Exception = exception
            };
            foreach (var item in setttings) {
                if (item.LogType == 1) {
                    var logHelper = Config.GetLogSqlHelper(DateTime.Now);
                    logHelper.Insert(readLog);
                    logHelper.Dispose();
                } else if (item.LogType == 2) {
                    await PostContent(item.Data, readLog.ToJson());
                } else if (item.LogType == 3) {
                    var logHelper = SqlHelperFactory.OpenDatabase(item.Data, SqlType.SqlServer);
                    logHelper.Insert(readLog);
                    logHelper.Dispose();
                } else if (item.LogType == 4) {
                    var logHelper = SqlHelperFactory.OpenDatabase(item.Data, SqlType.MySql);
                    logHelper.Insert(readLog);
                    logHelper.Dispose();
                }
            }
        }
        private async Task WriteExecuteSelectLog(int adminId, ExecuteSqlDto data, string SqlConnectionName, DateTime addingTime, EnumRunReadResult result, int runTime, string exception = null)
        {
            var helper = GetReadHelper();
            var admin = await helper.Where<DbSysAdmin>(q => q.IsDelete == false).Where(q => q.Id == adminId).FirstOrDefault_Async();
            var setttings = await helper.Select_Async<DbSqlQueryLogSetting>("where IsDelete=0 and IsFrozen=0 ");
            helper.Dispose();

            DbSqlQueryLog readLog = new DbSqlQueryLog() {
                AddingTime = addingTime,
                AdminId = admin.Id,
                AdminJobNo = admin.JobNo,
                AdminName = admin.Name,
                AdminPhone = admin.Phone,
                AdminTrueName = admin.TrueName,
                SqlConnectionId = data.SqlConnId,
                SqlConnectionName = SqlConnectionName,
                DatabaseName = data.Database,
                Sql = data.Sql,
                RunReadResult = result,
                RunTime = runTime,
                Exception = exception
            };
            foreach (var item in setttings) {
                if (item.LogType == 1) {
                    var logHelper = Config.GetLogSqlHelper(DateTime.Now);
                    logHelper.Insert(readLog);
                    logHelper.Dispose();
                } else if (item.LogType == 2) {
                    await PostContent(item.Data, readLog.ToJson());
                } else if (item.LogType == 3) {
                    var logHelper = SqlHelperFactory.OpenDatabase(item.Data, SqlType.SqlServer);
                    logHelper.Insert(readLog);
                    logHelper.Dispose();
                } else if (item.LogType == 4) {
                    var logHelper = SqlHelperFactory.OpenDatabase(item.Data, SqlType.MySql);
                    logHelper.Insert(readLog);
                    logHelper.Dispose();
                }
            }
        }

        private async Task PostContent(string url, string json)
        {
            var httpClient = _httpClientFactory.CreateClient();
            try {
                var message = new HttpRequestMessage() {
                    RequestUri = new Uri(url),
                    Method = new HttpMethod("POST"),
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                message.Headers.Add("Accept", "*/*");
                message.Headers.Add("User-Agent", "Mozilla/5.0 ToolGood.TextFilter/1.0");

                await httpClient.SendAsync(message);
            } catch {
            } finally {
                httpClient.Dispose();
            }
        }

        #endregion

        #region 查询导出

        public async Task<ExecuteResult> ExecuteSql_Select_Export(ExecuteSqlDto request, int adminId)
        {
            if (string.IsNullOrWhiteSpace(request.Key) == false && _commandDict.TryGetValue(request.Key, out TempCommand command)) {
                try {
                    if (command.Authority != request.Authority) {
                        return new ExecuteResult() { StartTime = DateTime.Now, Exception = "权限不一致！" };
                    }
                    int readMaxRows = command.ReadMaxRows;
                    if (command.Authority == 5) { readMaxRows = int.MaxValue; }

                    var provider = SqlCache.Instance.GetSqlCommonProvider(command.SqlType);
                    if (command.Command != null) {
                        var startTime = DateTime.Now;
                        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                        var result = await provider.ExecuteSql_Select(command.Command, request.Sql, readMaxRows);
                        stopwatch.Stop();
                        try {
                            if (result.Exception != null) {
                                await WriteExecuteSelectLog(adminId, request, command.Name, startTime, EnumRunReadResult.Fail, (int)stopwatch.ElapsedMilliseconds, result.Exception);
                            } else {
                                await WriteExecuteSelectLog(adminId, request, command.Name, startTime, EnumRunReadResult.Success, (int)stopwatch.ElapsedMilliseconds);
                            }
                        } catch (Exception ex) { }
                        return result;
                    }
                } finally {
                    _commandDict.Remove(request.Key);
                    if (command.Command != null) {
                        var conn = command.Command.Connection;
                        command.Command.Dispose();
                        command.Command = null;
                        if (conn != null) {
                            conn.Dispose();
                        }
                    }
                    command = null;
                }
            }
            return new ExecuteResult() { StartTime = DateTime.Now, Exception = "无法打开连接！" };

        }

        #endregion


    }
}
