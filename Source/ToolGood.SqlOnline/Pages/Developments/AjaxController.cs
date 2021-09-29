/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon.Controllers;
using ToolGood.WebCommon.Utils;

namespace ToolGood.SqlOnline.Pages.Developments
{
    [Route("/Developments/Ajax/{action}")]
    public class AjaxController : ApiControllerCore
    {
        private readonly IDevelopmentApplication _sqlOnlineApplication;

        public AjaxController(IDevelopmentApplication sqlOnlineApplication)
        {
            _sqlOnlineApplication = sqlOnlineApplication;
        }


        #region GetSqlTree
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetSqlTree([FromBody] SqlSearchDto request)
        {
            var use = await _sqlOnlineApplication.UseDevelopment();
            if (use == false) { return LayuiError("系统出了个小差！"); }

            try {
                if (string.IsNullOrEmpty(request.SearchType)) {
                    var connDtos = await _sqlOnlineApplication.GetConnList();
                    return LayuiSuccess(GetSqlTree(connDtos, request), null);
                } else if (request.SearchType == "GetDatabaseNames") {
                    var databaseEntities = await _sqlOnlineApplication.GetDatabaseNames(request.SqlConnId);
                    return LayuiSuccess(GetSqlTree(databaseEntities, request), null);
                }

            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }
        private List<SqlTreeDto> GetSqlTree(List<SqlConnDto> sqlConnDtos, SqlSearchDto searchDto)
        {
            List<SqlTreeDto> tree = new List<SqlTreeDto>();
            foreach (var node in sqlConnDtos) {
                var dto = new SqlTreeDto() {
                    isParent = true,
                    id = "c-" + Guid.NewGuid().ToString().Replace("-", ""),
                    pId = "root",
                    name = node.Name,
                    open = false,
                    icon = GetIcon(node.SqlType.ToLower()),
                    SearchType = "GetDatabaseNames",
                    SqlConnId = node.Id,
                    SqlType = node.SqlType,
                };
                if (node.SqlType.Equals("sqlite", StringComparison.OrdinalIgnoreCase)) {
                    dto.isParent = null;
                }
                tree.Add(dto);
            }
            return tree;
        }
        private List<SqlTreeDto> GetSqlTree(List<DatabaseEntity> sqlConnDtos, SqlSearchDto searchDto)
        {
            List<SqlTreeDto> tree = new List<SqlTreeDto>();
            foreach (var node in sqlConnDtos) {
                var dto = new SqlTreeDto() {
                    id = "db-" + Guid.NewGuid().ToString().Replace("-", ""),
                    pId = searchDto.Id,
                    name = node.DatabaseName,
                    open = false,
                    icon = GetIcon("database"),
                    SqlType = searchDto.SqlType,
                    SqlConnId = searchDto.SqlConnId,
                    Database = node.DatabaseName,
                };
                tree.Add(dto);
            }
            return tree;
        }
        private string GetIcon(string name)
        {
            return $"/_/img/db/{name}.png";
        }
        #endregion

        #region GetDatabaseInfos
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetDatabaseInfos([FromForm] SqlSearchDto request)
        {
            var use = await _sqlOnlineApplication.UseDevelopment();
            if (use == false) { return Error("Not use development"); }

            var list = await _sqlOnlineApplication.GetTableShowColumns(request.SqlConnId, request.Database);
            if (list != null) {
                Dictionary<string, StructureModel> dict = new Dictionary<string, StructureModel>();
                List<StructureModel> models = new List<StructureModel>();
                foreach (var item in list) {
                    StructureModel model;
                    var key = item.SchemaName + "." + item.Name;
                    if (dict.TryGetValue(key, out model) == false) {
                        model = new StructureModel();
                        if (string.IsNullOrEmpty(item.SchemaName) || item.SchemaName == "dbo" || item.SchemaName == "public") {
                            model.Name = item.Name;
                        } else {
                            model.Name = key;
                        }
                        model.Comment = item.Comment;
                        if (item.Type == "t" || item.Type == "table") {
                            model.Type = "t";
                        } else {
                            model.Type = "v";
                        }
                        dict[key] = model;
                        models.Add(model);
                    }
                    model.Items.Add(new StructureItemModel(item));
                }
                return Success(models, null);
            }
            return Error();
        }
        #endregion

        #region GetTableViewProcedure
        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetTableViewProcedure([FromForm] SqlSearchDto request)
        {
            try {
                #region TableViewProcedure
                var tables = await _sqlOnlineApplication.GetTables(request.SqlConnId, request.Database);
                var views = await _sqlOnlineApplication.GetViews(request.SqlConnId, request.Database);
                var procedures = await _sqlOnlineApplication.GetProcedures(request.SqlConnId, request.Database);
                var Infos = new List<TableViewProcedure>();
                foreach (var table in tables) {
                    Infos.Add(new TableViewProcedure() {
                        TplTypeId = 1,
                        Type = "table",
                        SchemaName = table.SchemaName,
                        Name = table.TableName,
                        Comment = table.Comment
                    });
                }
                foreach (var view in views) {
                    Infos.Add(new TableViewProcedure() {
                        TplTypeId = 1,
                        Type = "view",
                        SchemaName = view.SchemaName,
                        Name = view.ViewName,
                        Comment = view.Comment
                    });
                }
                foreach (var procedure in procedures) {
                    Infos.Add(new TableViewProcedure() {
                        TplTypeId = 2,
                        Type = "proc",
                        SchemaName = procedure.SchemaName,
                        Name = procedure.ProcedureName,
                        Comment = procedure.Comment
                    });
                }
                #endregion
                return LayuiSuccess(Infos, null);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }
        public class TableViewProcedure
        {
            public int TplTypeId { get; set; }
            public string Type { get; set; }

            public string SchemaName { get; set; }

            public string Name { get; set; }

            public string Comment { get; set; }
        }
        #endregion

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetTableViewProcedureData([FromForm] SqlSearchDto request)
        {
            var use = await _sqlOnlineApplication.UseDevelopment();
            if (use == false) { return Error("系统出了个小差！"); }
            try {
                if (request.SearchType.Equals("table", StringComparison.OrdinalIgnoreCase)) {
                    var tableView = await _sqlOnlineApplication.GetTableInfo(request.SqlConnId, request.Database, request.Schema, request.Search);
                    return Success(tableView, null);
                } else if (request.SearchType.Equals("view", StringComparison.OrdinalIgnoreCase)) {
                    var tableView = await _sqlOnlineApplication.GetViewInfo(request.SqlConnId, request.Database, request.Schema, request.Search);
                    return Success(tableView, null);
                } else if (request.SearchType.Equals("proc", StringComparison.OrdinalIgnoreCase)) {
                    var tableView = await _sqlOnlineApplication.GetProcedureInfo(request.SqlConnId, request.Database, request.Schema, request.Search);
                    return Success(tableView, null);
                }
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return Error("系统出了个小差！");
        }


        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetCodeGenTpl([FromForm] AdminIdDto request)
        {
            var use = await _sqlOnlineApplication.UseDevelopment();
            if (use == false) { return Error("系统出了个小差！"); }
            try {
                var str = await _sqlOnlineApplication.GetSqlCodeGenTpl(request.Id.Value);
                return Success(str, null);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return Error("系统出了个小差！");
        }



    }
}
