/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Configs;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Sqls.Editors
{
    [AdminMenu("SqlOnlineDesktop", "show")]
    public class MainModel : AdminPageModel
    {
        private readonly ISqlOnlineApplication _sqlOnlineApplication;

        public MainModel(ISqlOnlineApplication sqlOnlineApplication)
        {
            _sqlOnlineApplication = sqlOnlineApplication;
        }
        public int ConnId { get; private set; }
        public string DatabaseName { get; private set; }
        public List<DatabaseEntity> DatabaseEntities { get; private set; }

        public int TempId { get; private set; }
        public int DocId { get; private set; }
        public string Title { get; private set; }
        public string SqlString { get; private set; }
        public string JsMode { get; private set; }


        public async Task<IActionResult> OnGet(string action, int id, int sqlConnId, string database, string schema, string name)
        {
            ConnId = sqlConnId;
            DatabaseName = database;
            SqlSearchDto search = new SqlSearchDto() {
                AdminId = AdminDto.Id,
                SqlConnId = sqlConnId,
                Database = database,
                Schema = schema,
                Search = name
            };
            SqlEditorDto r = null;
            if (action == "new") {
                r = await _sqlOnlineApplication.GetSql_New(search);
            } else if (action == "TableSelect100") {
                r = await _sqlOnlineApplication.GetSql_TableSelect100(search);
            } else if (action == "TableInsertSql") {
                r = await _sqlOnlineApplication.GetSql_TableInsert(search);
            } else if (action == "TableUpdateSql") {
                r = await _sqlOnlineApplication.GetSql_TableUpdate(search);
            } else if (action == "TableDeleteSql") {
                r = await _sqlOnlineApplication.GetSql_TableDelete(search);
            } else if (action == "ViewSelect100") {
                r = await _sqlOnlineApplication.GetSql_ViewSelect100(search);
            } else if (action == "ViewCreateSql") {
                r = await _sqlOnlineApplication.GetSql_ViewCreate(search);
            } else if (action == "ViewAlterSql") {
                r = await _sqlOnlineApplication.GetSql_ViewAlter(search);
            } else if (action == "FunctionSelectSql") {
                r = await _sqlOnlineApplication.GetSql_FunctionSelectSql(search);
            } else if (action == "FunctionCreateSql") {
                r = await _sqlOnlineApplication.GetSql_FunctionCreate(search);
            } else if (action == "FunctionAlterSql") {
                r = await _sqlOnlineApplication.GetSql_FunctionAlter(search);
            } else if (action == "ProcedureExecuteSql") {
                r = await _sqlOnlineApplication.GetSql_ProcedureExecuteSql(search);
            } else if (action == "ProcedureCreateSql") {
                r = await _sqlOnlineApplication.GetSql_ProcedureCreate(search);
            } else if (action == "ProcedureAlterSql") {
                r = await _sqlOnlineApplication.GetSql_ProcedureAlter(search);

            } else if (action == "openDoc") {
                var sqlDoc = await _sqlOnlineApplication.GetSelfSqlDoc(AdminDto.Id, id);
                if (sqlDoc == null) { return Redirect(UrlSetting.NotFoundUrl); }
                await _sqlOnlineApplication.OpenSqlDoc(AdminDto.Id, id);
                DocId = sqlDoc.SqlDocId;
                Title = sqlDoc.Title;
                SqlString = sqlDoc.Content;
                DatabaseName = sqlDoc.DatabaseName;
                ConnId = sqlDoc.SqlConnId;
                search.SqlConnId = sqlDoc.SqlConnId;
                r = _sqlOnlineApplication.GetSql_New(sqlDoc.SqlType);
            } else if (action == "openDocTemp") {
                var sqlDoc = await _sqlOnlineApplication.GetSelfSqlDocTemp(AdminDto.Id, id);
                if (sqlDoc == null) { return Redirect(UrlSetting.NotFoundUrl); }
                TempId = sqlDoc.Id;
                DocId = sqlDoc.SqlDocId;
                Title = sqlDoc.Title;
                SqlString = sqlDoc.Content;
                DatabaseName = sqlDoc.DatabaseName;
                ConnId = sqlDoc.SqlConnId;
                search.SqlConnId = sqlDoc.SqlConnId;
                r = _sqlOnlineApplication.GetSql_New(sqlDoc.SqlType);
            } else if (action == "openDocShare") {
                var sqlDoc = await _sqlOnlineApplication.GetShareSqlDoc(AdminDto.Id, id);
                if (sqlDoc == null) { return Redirect(UrlSetting.NotFoundUrl); }
                Title = sqlDoc.Title;
                SqlString = sqlDoc.Content;
                DatabaseName = sqlDoc.DatabaseName;
                ConnId = sqlDoc.SqlConnId;
                search.SqlConnId = sqlDoc.SqlConnId;
                r = _sqlOnlineApplication.GetSql_New(sqlDoc.SqlType);
            }
            if (r == null) { return Redirect(UrlSetting.NotFoundUrl); }
            JsMode = r.JsMode;
            if (string.IsNullOrEmpty(SqlString)) { SqlString = r.SqlString; }


            DatabaseEntities = await _sqlOnlineApplication.GetDatabaseNames(search);
            if (string.IsNullOrEmpty(DatabaseName)) {
                if (DatabaseEntities.Count > 0) {
                    DatabaseName = DatabaseEntities[0].DatabaseName;
                }
            }
            return Page();
        }








    }
}
