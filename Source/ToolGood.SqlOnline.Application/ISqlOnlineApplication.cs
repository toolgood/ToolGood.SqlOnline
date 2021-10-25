/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dto;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Application
{
    public interface ISqlOnlineApplication
    {
        #region 结构
        Task<List<SqlConnDto>> GetConnList(SqlSearchDto request);
        Task<List<DatabaseEntity>> GetDatabaseNames(SqlSearchDto request);
        Task<List<TableEntity>> GetTableNames(SqlSearchDto request);
        Task<List<TableColumnEntity>> GetTableColumns(SqlSearchDto request);

        Task<List<ViewEntity>> GetViewNames(SqlSearchDto request);
        Task<List<ViewColumnEntity>> GetViewColumns(SqlSearchDto request);

        Task<List<ProcedureEntity>> GetProcedureNames(SqlSearchDto request);
        Task<List<ProcedureParamEntity>> GetProcedureParams(SqlSearchDto request);

        Task<List<FunctionEntity>> GetFunctionNames(SqlSearchDto request);
        Task<List<FunctionParamEntity>> GetFunctionParams(SqlSearchDto request);

        Task<List<TableShowEntity>> GetTableShowColumns(int adminId, int connId, string databaseName);

        #endregion

        #region 文档操作

        Task<bool> AddSqlDoc(Req<SqlDocDto> request);
        Task<bool> EditSqlDoc(Req<SqlDocDto> request);
        Task<bool> DeleteSqlDoc(Req<AdminIdDto> request);

        Task<bool> AddSqlDocTemp(Req<SqlDocDto> request);
        Task<bool> EditSqlDocTemp(Req<SqlDocDto> request);
        Task<bool> DeleteSqlDocTemp(Req<AdminIdDto> request);

        Task<bool> OpenSqlDoc(int adminId, int id);

        Task<Page<AdminSqlDocDto>> GetSelfSqlDocList(AdminSqlDocSearchDto request);

        Task<Page<AdminSqlDocDto>> GetSelfSqlDocTempList(AdminSqlDocSearchDto request);

        Task<Page<AdminSqlDocDto>> GetShareSqlDocList(AdminSqlDocSearchDto request);

        Task<SqlDocDto> GetSelfSqlDoc(int adminId, int id);
        Task<SqlDocDto> GetSelfSqlDocTemp(int adminId, int id);
        Task<SqlDocDto> GetShareSqlDoc(int adminId, int id);

        #endregion

        #region Create/Get Sql
        SqlEditorDto GetSql_New(string sqlType);

        Task<SqlEditorDto> GetSql_New(SqlSearchDto request);

        Task<SqlEditorDto> GetSql_TableSelect100(SqlSearchDto request);

        Task<SqlEditorDto> GetSql_TableSelect100(int adminId, int connId, string databaseName, string schemaName, string tableName);

        Task<SqlEditorDto> GetSql_TableInsert(SqlSearchDto request);
        Task<SqlEditorDto> GetSql_TableUpdate(SqlSearchDto request);
        Task<SqlEditorDto> GetSql_TableDelete(SqlSearchDto request);

        Task<SqlEditorDto> GetSql_ViewSelect100(SqlSearchDto request);

        Task<SqlEditorDto> GetSql_ViewCreate(SqlSearchDto request);
        Task<SqlEditorDto> GetSql_ViewAlter(SqlSearchDto request);

        Task<SqlEditorDto> GetSql_FunctionSelectSql(SqlSearchDto request);

        Task<SqlEditorDto> GetSql_FunctionCreate(SqlSearchDto request);
        Task<SqlEditorDto> GetSql_FunctionAlter(SqlSearchDto request);

        Task<SqlEditorDto> GetSql_ProcedureExecuteSql(SqlSearchDto request);
        Task<SqlEditorDto> GetSql_ProcedureCreate(SqlSearchDto request);
        Task<SqlEditorDto> GetSql_ProcedureAlter(SqlSearchDto request);
        #endregion

        #region ChangeComment
        Task<bool> ChangeComment_Table(int adminId, int connId, string databaseName, string schemaName, string tableName, string comment);

        Task<bool> ChangeComment_TableColumn(int adminId, int connId, string databaseName, string schemaName, string tableName, string columnName, string comment);
        #endregion

        #region ExecuteSql
        Task<string> CreateCommand(Req<ExecuteSqlDto> request);

        bool StopCommand(Req<ExecuteSqlDto> request);

        Task<ExecuteResult> ExecuteSql_Select(Req<ExecuteSqlDto> request);

        Task<ExecuteResult> ExecuteSql_InsertUpdate(Req<ExecuteSqlDto> request);
        Task<ExecuteResult> ExecuteSql_InsertUpdateDelete(Req<ExecuteSqlDto> request);
        Task<ExecuteResult> ExecuteSql_AllPermissions(Req<ExecuteSqlDto> request);
        #endregion

        #region 查询导出

        Task<ExecuteResult> ExecuteSql_Select_Export(ExecuteSqlDto request, int adminId);

        #endregion

    }
}
