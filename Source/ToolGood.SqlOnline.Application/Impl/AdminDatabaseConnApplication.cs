/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Application.Admins;
using ToolGood.SqlOnline.Datas;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.Dtos.CodeGens;
using ToolGood.SqlOnline.IPlugin;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Application.Impl
{
    public class AdminDatabaseConnApplication : ApplicationBase, IAdminDatabaseConnApplication
    {
        public List<String> GetSqlTypes()
        {
            return SqlCache.Instance.GetSqlTypes();
        }


        #region 数据库链接字符串
        public async Task<bool> AddDatabaseConn(Req<SqlConnDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Name == null) { throw new ArgumentException("Data.Name is null"); }
            if (request.Data.ConnectionString == null) { throw new ArgumentException("Data.ConnectionString is null"); }

            var helper = GetWriteHelper();
            var db = new DbSqlConn() {
                Name = request.Data.Name,
                Comment = request.Data.Comment,
                ConnectionString = request.Data.ConnectionString,
                SqlType = request.Data.SqlType,
                UseDevelopment = request.Data.UseDevelopment,

                AddingAdminId = request.OperatorId,
                AddingTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
                ModifyTime = DateTime.Now
            };
            await helper.Insert_Async(db);
            request.Data.Id = db.Id;

            return true;
        }

        public async Task<bool> EditDatabaseConn(Req<SqlConnDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Id == 0) { throw new ArgumentException("Data.Id is 0."); }
            if (request.Data.Name == null) { throw new ArgumentException("Data.Name is null"); }
            if (request.Data.ConnectionString == null) { throw new ArgumentException("Data.ConnectionString is null"); }

            var helper = GetWriteHelper();

            var count = await helper.Update_Async<DbSqlConn>(new {
                Name = request.Data.Name,
                Comment = request.Data.Comment,
                ConnectionString = request.Data.ConnectionString,
                SqlType = request.Data.SqlType,
                UseDevelopment = request.Data.UseDevelopment,


                ModifyTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
            }, new { IsDelete = false, Id = request.Data.Id });
            if (count == 0) {
                request.Message = "失败,数据库链接字符串不存在！";
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteDatabaseConn(Req<AdminIdDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Id == null) { throw new ArgumentException("Data.Id is null"); }

            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSqlConn>(new { IsDelete = true, ModifyTime = DateTime.Now, ModifyAdminId = request.OperatorId }
                                                            , new { Id = request.Data.Id, IsDelete = false });
            if (count > 0) {
                await helper.Update_Async<DbSqlConnPass>(new { IsDelete = true }, new { ConnId = request.Data.Id, IsDelete = false });

                return true;
            } else {
                request.Message = "失败，数据库链接字符串不存在。";
                return false;
            }
        }

        public Task<List<SqlConnDto>> GetDatabaseConnAll(Req<SearchDto> request)
        {
            var helper = GetReadHelper();
            return helper.Where<DbSqlConn>()
                .Where(q => q.IsDelete == false)
                .IfSet(request.Data.Search).Where(q => q.Name.Contains(request.Data.Search))
                .OrderBy(q => q.Name)
                .Select_Async<SqlConnDto>();
        }

        public Task<List<SqlConnDto>> GetDatabaseConnAllWithAdminId(Req<SearchDto> request)
        {
            var sql = @"select t3.* 
from SysAdminGroup t1 
inner Join SqlConnPass t2 ON t1.Id=t2.AdminGroupId 
inner join SqlConn t3 on t2.ConnId=t3.Id
inner join SysAdmin_Group t4 on t4.GroupId=t1.Id
where t1.IsDelete=0 and t2.IsDelete=0 and t3.IsDelete=0 and t4.AdminId=@0 and CanRead=1";
            List<object> objs = new List<object>();
            objs.Add(request.OperatorId);

            if (string.IsNullOrEmpty(request.Data.Search) == false) {
                sql += " and t3.Name like @1";
                objs.Add("%" + request.Data.Search + "%");
            }
            sql += " order by t3.NodeId,t3.Name";

            var helper = GetReadHelper();
            return helper.Select_Async<SqlConnDto>(sql, objs.ToArray());
        }

        public Task<SqlConnDto> GetDatabaseConnById(int id)
        {
            var helper = GetReadHelper();
            return helper.Where<DbSqlConn>()
                .Where(q => q.IsDelete == false)
                .Where(q => q.Id == id)
                .FirstOrDefault_Async<SqlConnDto>();
        }

        #endregion

        #region 数据库链接字符串 PASS
        public async Task<bool> EditDatabaseConnPass(Req<SqlConnPassEditDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.AdminGroupId == 0) { throw new ArgumentException("Data.Id is 0."); }
            if (request.Data.p == null) { throw new ArgumentException("Data.p is 0."); }


            var helper = GetWriteHelper();

            List<DbSqlConnPass> databaseConnPasses = new List<DbSqlConnPass>();
            foreach (var item in request.Data.p) {
                if (item.Id == 0) {
                    var db = new DbSqlConnPass() {
                        AdminGroupId = request.Data.AdminGroupId,
                        ConnId = item.ConnId,
                        CanRead = item.CanRead,
                        CanDelete = item.CanDelete,
                        ReadMaxRows = item.ReadMaxRows,
                        CanEdit = item.CanEdit,
                        AllPermissions = item.AllPermissions,
                        CanDownload = item.CanDownload,
                        ChangeMaxRows = item.ChangeMaxRows,

                        AddingAdminId = request.OperatorId,
                        AddingTime = DateTime.Now,
                        ModifyAdminId = request.OperatorId,
                        ModifyTime = DateTime.Now
                    };
                    databaseConnPasses.Add(db);
                } else {
                    await helper.Update_Async<DbSqlConnPass>(new {
                        CanRead = item.CanRead,
                        CanEdit = item.CanEdit,
                        CanDelete = item.CanDelete,
                        ReadMaxRows = item.ReadMaxRows,
                        AllPermissions = item.AllPermissions,
                        CanDownload = item.CanDownload,
                        ChangeMaxRows = item.ChangeMaxRows,


                        ModifyTime = DateTime.Now,
                        ModifyAdminId = request.OperatorId,
                    }, new { IsDelete = false, Id = item.Id });
                }
            }
            if (databaseConnPasses.Count > 0) {
                await helper.InsertList_Async(databaseConnPasses);
            }
            var sql = @"update SqlConnPass set IsDelete=1
where IsDelete=0 and Id IN (select min(Id) from SqlConnPass where IsDelete=0 Group by AdminGroupId,ConnId having Count(Id)>1)
";
            if (helper._SqlType == SqlType.MySql || helper._SqlType == SqlType.MariaDb) {
                sql = @"update SqlConnPass set IsDelete=1
            where IsDelete=0 and Id IN (select Id from (select min(Id) from SqlConnPass where IsDelete=0 Group by AdminGroupId,ConnId having Count(Id)>1) t)
            ";
            }
            var count = 1;
            while (count > 0) { count = await helper.Execute_Async(sql); }

            return true;
        }

        public async Task<bool> DeleteDatabaseConnPass(Req<AdminIdDto> request)
        {
            var helper = GetReadHelper();
            return await helper.Where<DbSqlConn>()
                .Where(q => q.IsDelete == false)
                .Where(q => q.Id == request.Data.Id)
                .Delete_Async() > 0;
        }

        public Task<SqlConnPassDto> GetDatabaseConnPassById(int id)
        {
            var helper = GetReadHelper();
            return helper.Where<DbSqlConnPass>()
                .Where(q => q.IsDelete == false)
                .Where(q => q.Id == id)
                .FirstOrDefault_Async<SqlConnPassDto>();
        }

        public Task<List<SqlConnPassDto>> GetConnPowerList(Req<SearchDto> request)
        {
            var sql = @"select t1.Id ConnId,t1.Name,t1.Comment,t1.SqlType,t2.AdminGroupId, t2.Id,t2.CanRead,t2.ReadMaxRows,t2.CanEdit,t2.ChangeMaxRows
,t2.CanDelete,t2.AllPermissions,t2.CanDownload
from SqlConn t1
left join SqlConnPass t2 on t1.Id=t2.ConnId and t2.AdminGroupId=@0 and t2.IsDelete=0
where t1.IsDelete=0   
";
            if (string.IsNullOrWhiteSpace(request.Data.Search) == false) {
                sql += "and t1.Name like @1";
            }
            var helper = GetReadHelper();
            return helper.Select_Async<SqlConnPassDto>(sql + " order by t1.Name asc", request.Data.Id, "%" + request.Data.Search + "%");
        }



        public Task<Page<SqlConnPassDto>> GetDatabaseConnPage(Req<SearchDto> requeste)
        {
            return null;
        }

        public async Task<List<SqlConnPassDto>> GetDatabaseConnPassListByAdminGroupId(int adminGropId)
        {
            var sql = @"select t1.Name,t1.SqlType,t1.Id ConnId,t1.Comment,
t2.Id,t2.CanRead,t2.ReadMaxRows,t2.CanChange,t2.UpdateMaxRows,t2.CanDelete
,t2.DeleteMaxRows,t2.CanTruncate,t2.CanExecute,t2.CanChangeTable,t2.CanGrant,t2.CanDesign
from SqlConn t1 
Left Join SqlConnPass t2 on t2.ConnId=t1.Id and t2.IsDelete=0 and t2.AdminGroupId=@0   
where t1.IsDelete=0  
order by t3.NodeName,t1.Name
";
            var helper = GetReadHelper();
            var list = await helper.Select_Async<SqlConnPassDto>(sql, adminGropId);
            foreach (var item in list) {
                if (item.Id == 0) {
                    item.ReadMaxRows = 500;
                }
            }
            return list;
        }

        #endregion


        #region SqlQueryLogSetting

        public async Task<bool> AddLogSetting(Req<SqlQueryLogSettingDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Name == null) { throw new ArgumentException("Data.Name is null"); }

            var helper = GetWriteHelper();
            var db = new DbSqlQueryLogSetting() {
                Name = request.Data.Name,
                LogType = request.Data.LogType,
                Data = request.Data.Data,
                IsFrozen = request.Data.IsFrozen,

                AddingAdminId = request.OperatorId,
                AddingTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
                ModifyTime = DateTime.Now
            };
            await helper.Insert_Async(db);
            request.Data.Id = db.Id;

            return true;
        }
        public async Task<bool> EditLogSetting(Req<SqlQueryLogSettingDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Id == 0) { throw new ArgumentException("Data.Id is 0."); }
            if (request.Data.Name == null) { throw new ArgumentException("Data.Name is null"); }

            var helper = GetWriteHelper();

            var count = await helper.Update_Async<DbSqlQueryLogSetting>(new {
                Name = request.Data.Name,
                LogType = request.Data.LogType,
                Data = request.Data.Data,
                IsFrozen = request.Data.IsFrozen,

                ModifyAdminId = request.OperatorId,
                ModifyTime = DateTime.Now
            }, new { IsDelete = false, Id = request.Data.Id });
            if (count == 0) {
                request.Message = "失败,输出日志不存在！";
                return false;
            }
            return true;
        }
        public async Task<bool> DeleteLogSetting(Req<AdminIdDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Id == null) { throw new ArgumentException("Data.Id is null"); }

            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSqlQueryLogSetting>(new { IsDelete = true, ModifyTime = DateTime.Now, ModifyAdminId = request.OperatorId }
                                                            , new { Id = request.Data.Id, IsDelete = false });
            if (count > 0) {
                return true;
            } else {
                request.Message = "失败，输出日志不存在。";
                return false;
            }
        }
        public Task<Page<SqlQueryLogSettingDto>> GetLogSettingList(Req<SearchDto> request)
        {
            var helper = GetReadHelper();
            return helper.Where<DbSqlQueryLogSetting>()
                .Where(q => q.IsDelete == false)
                .IfSet(request.Data.Search).Where(q => q.Name.Contains(request.Data.Search))
                .OrderBy(q => q.Name)
                .Page_Async<SqlQueryLogSettingDto>(request.Data.PageIndex, request.Data.PageSize);
        }
        public Task<SqlQueryLogSettingDto> GetLogSettingById(int id)
        {
            var helper = GetReadHelper();

            return helper.Where<DbSqlQueryLogSetting>()
                .Where(q => q.IsDelete == false)
                .Where(q => q.Id == id)
                .FirstOrDefault_Async<SqlQueryLogSettingDto>();
        }
        #endregion


        #region SqlDoc
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
        public Task<AdminSqlDocDto> GetSqlDocById(int id)
        {
            const string sql = @"select doc.Id,a.Name AdminName,a.JobNo AdminJobNo,a.TrueName AdminTrueName,a.Phone AdminPhone
,doc.Title,doc.ContentLen,conn.Name SqlConnName,doc.DatabaseName,conn.SqlType,doc.ModifyTime
,doc.Content
from SqlDoc doc
inner join SysAdmin a on a.Id=doc.AdminId
inner join SqlConn conn on conn.Id=doc.SqlConnId
where doc.IsDelete=0 and a.IsDelete=0 and conn.IsDelete=0
and doc.Id=@0
";
            var helper = GetReadHelper();
            return helper.FirstOrDefault_Async<AdminSqlDocDto>(sql, id);
        }
        public Task<Page<AdminSqlDocDto>> GetSqlDocList(Req<AdminSqlDocSearchDto> request)
        {
            const string selectSql = @"select doc.Id,a.Name AdminName,a.JobNo AdminJobNo,a.TrueName AdminTrueName,a.Phone AdminPhone
,doc.Title,doc.ContentLen,conn.Name SqlConnName,doc.DatabaseName,conn.SqlType,doc.ModifyTime";
            const string tableSql = @"from SqlDoc doc
inner join SysAdmin a on a.Id=doc.AdminId
inner join SqlConn conn on conn.Id=doc.SqlConnId";
            string whereSql = @"where doc.IsDelete=0 and a.IsDelete=0 and conn.IsDelete=0 ";

            SqlParameterCollection sqlParameters = new SqlParameterCollection();
            if (string.IsNullOrEmpty(request.Data.AdminName) == false) {
                whereSql += "and a.Name = @AdminName ";
                sqlParameters.Add("AdminName", request.Data.AdminName);
            }
            if (string.IsNullOrEmpty(request.Data.AdminJobNo) == false) {
                whereSql += "and a.JobNo=@AdminJobNo ";
                sqlParameters.Add("AdminJobNo", request.Data.AdminJobNo);
            }
            if (string.IsNullOrEmpty(request.Data.AdminTrueName) == false) {
                whereSql += "and a.TrueName like @AdminTrueName ";
                sqlParameters.Add("AdminTrueName", "%" + request.Data.AdminTrueName + "%");
            }
            if (string.IsNullOrEmpty(request.Data.Title) == false) {
                whereSql += "and doc.Title like @Title ";
                sqlParameters.Add("Title", "%" + request.Data.Title + "%");
            }
            if (string.IsNullOrEmpty(request.Data.SqlConnName) == false) {
                whereSql += "and conn.Name like @SqlConnName ";
                sqlParameters.Add("SqlConnName", "%" + request.Data.SqlConnName + "%");
            }

            var orderSql = " doc.Id desc ";
            if (string.IsNullOrWhiteSpace(request.Data.Field) == false) {
                orderSql = $" {request.Data.Field} {request.Data.Order}";
            }

            var helper = GetReadHelper();
            return helper.SQL_Page_Async<AdminSqlDocDto>(request.Data.PageIndex, request.Data.PageSize, selectSql, tableSql
                , orderSql, whereSql, sqlParameters);
        }

        #endregion

        #region SqlDocTemp
        public async Task<bool> DeleteSqlDocTemp(Req<AdminIdDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Id == null) { throw new ArgumentException("Data.Id is null"); }

            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSqlDocTemp>(new { IsDelete = true, ModifyTime = DateTime.Now, ModifyAdminId = request.OperatorId }
                                                            , new { Id = request.Data.Id, IsDelete = false });
            if (count > 0) {
                return true;
            } else {
                request.Message = "失败，数据库链接字符串不存在。";
                return false;
            }
        }
        public Task<AdminSqlDocDto> GetSqlDocTempById(int id)
        {
            const string sql = @"select doc.Id,a.Name AdminName,a.JobNo AdminJobNo,a.TrueName AdminTrueName,a.Phone AdminPhone
,doc.Title,doc.ContentLen,conn.Name SqlConnName,doc.DatabaseName,conn.SqlType,doc.ModifyTime
,doc.Content
from SqlDocTemp doc
inner join SysAdmin a on a.Id=doc.AdminId
inner join SqlConn conn on conn.Id=doc.SqlConnId
where doc.IsDelete=0 and a.IsDelete=0 and conn.IsDelete=0
and doc.Id=@0
";
            var helper = GetReadHelper();
            return helper.FirstOrDefault_Async<AdminSqlDocDto>(sql, id);
        }
        public Task<Page<AdminSqlDocDto>> GetSqlDocTempList(Req<AdminSqlDocSearchDto> request)
        {
            const string selectSql = @"select doc.Id,a.Name AdminName,a.JobNo AdminJobNo,a.TrueName AdminTrueName,a.Phone AdminPhone
,doc.Title,doc.ContentLen,conn.Name SqlConnName,doc.DatabaseName,conn.SqlType,doc.ModifyTime";
            const string tableSql = @"from SqlDocTemp doc
inner join SysAdmin a on a.Id=doc.AdminId
inner join SqlConn conn on conn.Id=doc.SqlConnId";
            string whereSql = @"where doc.IsDelete=0 and a.IsDelete=0 and conn.IsDelete=0 ";

            SqlParameterCollection sqlParameters = new SqlParameterCollection();
            if (string.IsNullOrEmpty(request.Data.AdminName) == false) {
                whereSql += "and a.Name = @AdminName ";
                sqlParameters.Add("AdminName", request.Data.AdminName);
            }
            if (string.IsNullOrEmpty(request.Data.AdminJobNo) == false) {
                whereSql += "and a.JobNo=@AdminJobNo ";
                sqlParameters.Add("AdminJobNo", request.Data.AdminJobNo);
            }
            if (string.IsNullOrEmpty(request.Data.AdminTrueName) == false) {
                whereSql += "and a.TrueName like @AdminTrueName ";
                sqlParameters.Add("AdminTrueName", "%" + request.Data.AdminTrueName + "%");
            }
            if (string.IsNullOrEmpty(request.Data.Title) == false) {
                whereSql += "and doc.Title like @Title ";
                sqlParameters.Add("Title", "%" + request.Data.Title + "%");
            }
            if (string.IsNullOrEmpty(request.Data.SqlConnName) == false) {
                whereSql += "and conn.Name like @SqlConnName ";
                sqlParameters.Add("SqlConnName", "%" + request.Data.SqlConnName + "%");
            }

            var orderSql = " doc.Id desc ";
            if (string.IsNullOrWhiteSpace(request.Data.Field) == false) {
                orderSql = $" {request.Data.Field} {request.Data.Order}";
            }

            var helper = GetReadHelper();
            return helper.SQL_Page_Async<AdminSqlDocDto>(request.Data.PageIndex, request.Data.PageSize, selectSql, tableSql
                , orderSql, whereSql, sqlParameters);
        }
        #endregion

        #region CodeGens

        public async Task<bool> AddCodeGen(Req<SqlCodeGenDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }

            var helper = GetWriteHelper();
            DbSqlCodeGen doc = new DbSqlCodeGen() {
                Language = request.Data.Language,
                Title = request.Data.Title,
                Content = request.Data.Content,
                TplType = request.Data.TplType,
                Namespace = request.Data.Namespace,
                Comment = request.Data.Comment,

                AddingAdminId = request.OperatorId,
                AddingTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
                ModifyTime = DateTime.Now
            };
            await helper.Insert_Async(doc);
            request.Data.Id = doc.Id;
            return true;
        }
        public async Task<bool> EditCodeGen(Req<SqlCodeGenDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            var helper = GetWriteHelper();

            const string sql = @"update SqlCodeGen set Language=@0,Title=@1,Content=@2,TplType=@3,Namespace=@4,ModifyAdminId=@5,ModifyTime=@6,Comment=@7
where IsDelete=0 and Id=@8";
            var count = await helper.Execute_Async(sql, request.Data.Language, request.Data.Title, request.Data.Content, request.Data.TplType,
                request.Data.Namespace, request.OperatorId, DateTime.Now, request.Data.Comment, request.Data.Id);

            return count > 0;
        }
        public async Task<bool> DeleteCodeGen(Req<AdminIdDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Id == null) { throw new ArgumentException("Data.Id is null"); }

            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSqlCodeGen>(new { IsDelete = true, ModifyTime = DateTime.Now, ModifyAdminId = request.OperatorId }
                                                            , new { Id = request.Data.Id, IsDelete = false });
            if (count > 0) {
                return true;
            } else {
                request.Message = "失败，数据库链接字符串不存在。";
                return false;
            }
        }

        public Task<Page<SqlCodeGenDto>> GetSqlCodeGenList(Req<SqlCodeGenSearchDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }

            var helper = GetReadHelper();
            return helper.Where<DbSqlCodeGen>(q => q.IsDelete == false)
                  .IfTrue(request.Data.TplType > 0).Where(q => q.TplType == request.Data.TplType)
                  .IfSet(request.Data.Language).Where(q => q.Language == request.Data.Language)
                  .IfSet(request.Data.Search).Where(q => q.Title.Contains(request.Data.Search))
                  .Page_Async<SqlCodeGenDto>(request.Data.PageIndex, request.Data.PageSize);
        }

        public Task<SqlCodeGenDto> GetSqlCodeGenById(int id)
        {
            var helper = GetReadHelper();
            return helper.Where<DbSqlCodeGen>(q => q.IsDelete == false)
                    .Where(q => q.Id == id)
                    .FirstOrDefault_Async<SqlCodeGenDto>();
        }

        public Task<ProcedureInfo> GetProcedureInfo(Req<SqlCodeGenSearchDto> request)
        {
            throw new NotImplementedException();
        }

        public Task<TableViewInfo> GetTableInfo(Req<SqlCodeGenSearchDto> request)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
