using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpCompress.Archives.Zip;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;
using ToolGood.WebCommon.Utils;

namespace ToolGood.SqlOnline.Pages.Sqls
{
    [Route("/Sqls/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {
        private readonly ISqlOnlineApplication _sqlOnlineApplication;
        public AjaxController(ISqlOnlineApplication sqlOnlineApplication)
        {
            _sqlOnlineApplication = sqlOnlineApplication;
        }

        #region GetSqlTree
        [IgnoreAntiforgeryToken]
        [AdminMenu("SqlOnlineDesktop", "show")]
        [HttpPost]
        public async Task<IActionResult> GetSqlTree([FromBody] Req<SqlSearchDto> request)
        {
            request.Data.AdminId = request.OperatorId;
            try {
                if (string.IsNullOrEmpty(request.Data.SearchType)) {
                    var connDtos = await _sqlOnlineApplication.GetConnList(request.Data);
                    return LayuiSuccess(GetSqlTree(connDtos, request.Data), request.PasswordString);
                } else if (request.Data.SearchType == "GetDatabaseNames") {
                    var databaseEntities = await _sqlOnlineApplication.GetDatabaseNames(request.Data);
                    return LayuiSuccess(GetSqlTree(databaseEntities, request.Data), request.PasswordString);
                } else if (request.Data.SearchType == "GetTableNames") {
                    var tableEntities = await _sqlOnlineApplication.GetTableNames(request.Data);
                    return LayuiSuccess(GetSqlTree(tableEntities, request.Data), request.PasswordString);
                } else if (request.Data.SearchType == "GetViewNames") {
                    var viewEntities = await _sqlOnlineApplication.GetViewNames(request.Data);
                    return LayuiSuccess(GetSqlTree(viewEntities, request.Data), request.PasswordString);
                } else if (request.Data.SearchType == "GetProcedureNames") {
                    var procedureEntities = await _sqlOnlineApplication.GetProcedureNames(request.Data);
                    return LayuiSuccess(GetSqlTree(procedureEntities, request.Data), request.PasswordString);
                } else if (request.Data.SearchType == "GetFunctionNames") {
                    var functionEntities = await _sqlOnlineApplication.GetFunctionNames(request.Data);
                    return LayuiSuccess(GetSqlTree(functionEntities, request.Data), request.PasswordString);
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
                tree.Add(dto);
                if (node.SqlType.Equals("sqlite", StringComparison.OrdinalIgnoreCase)) {
                    dto.SearchType = null;
                    tree.Add(CreateTableTree(dto, searchDto));
                    tree.Add(CreateViewTree(dto, searchDto));
                }
            }
            return tree;
        }
        #region GetSqlTree DatabaseEntity
        private List<SqlTreeDto> GetSqlTree(List<DatabaseEntity> sqlConnDtos, SqlSearchDto searchDto)
        {
            List<SqlTreeDto> tree = new List<SqlTreeDto>();
            foreach (var node in sqlConnDtos) {
                var dto = new SqlTreeDto() {
                    isParent = true,
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
                tree.Add(CreateTableTree(dto, searchDto));
                tree.Add(CreateViewTree(dto, searchDto));
                tree.Add(CreateProcedureTree(dto, searchDto));
                tree.Add(CreateFunctionTree(dto, searchDto));
            }
            return tree;
        }
        private SqlTreeDto CreateTableTree(SqlTreeDto dto, SqlSearchDto searchDto)
        {
            return new SqlTreeDto() {
                isParent = true,
                id = "ts-" + Guid.NewGuid().ToString().Replace("-", ""),
                pId = dto.id,
                name = "表",
                open = true,
                icon = GetIcon("table"),
                SqlType = dto.SqlType,
                SqlConnId = dto.SqlConnId,
                Database = dto.Database,
                Search = dto.Search,
                Schema = dto.Schema,
                SearchType = "GetTableNames",
            };
        }
        private SqlTreeDto CreateViewTree(SqlTreeDto dto, SqlSearchDto searchDto)
        {
            return new SqlTreeDto() {
                isParent = true,
                id = "vs-" + Guid.NewGuid().ToString().Replace("-", ""),
                pId = dto.id,
                name = "视图",
                open = true,
                icon = GetIcon("view"),
                SqlType = dto.SqlType,
                SqlConnId = dto.SqlConnId,
                Database = dto.Database,
                SearchType = "GetViewNames",
            };
        }
        private SqlTreeDto CreateProcedureTree(SqlTreeDto dto, SqlSearchDto searchDto)
        {
            return new SqlTreeDto() {
                isParent = true,
                id = "ps-" + Guid.NewGuid().ToString().Replace("-", ""),
                pId = dto.id,
                name = "存储过程",
                open = true,
                icon = GetIcon("procedure"),
                SqlType = searchDto.SqlType,
                SqlConnId = searchDto.SqlConnId,
                Database = dto.Database,
                SearchType = "GetProcedureNames",
            };
        }
        private SqlTreeDto CreateFunctionTree(SqlTreeDto dto, SqlSearchDto searchDto)
        {
            return new SqlTreeDto() {
                isParent = true,
                id = "fs-" + Guid.NewGuid().ToString().Replace("-", ""),
                pId = dto.id,
                name = "函数",
                open = true,
                icon = GetIcon("function"),
                SqlType = searchDto.SqlType,
                SqlConnId = searchDto.SqlConnId,
                Database = dto.Database,
                SearchType = "GetFunctionNames",
            };
        }
        #endregion

        private List<SqlTreeDto> GetSqlTree(List<TableEntity> sqlConnDtos, SqlSearchDto searchDto)
        {
            List<SqlTreeDto> tree = new List<SqlTreeDto>();
            foreach (var node in sqlConnDtos) {
                var dto = new SqlTreeDto() {
                    id = "t-" + Guid.NewGuid().ToString().Replace("-", ""),
                    pId = searchDto.Id,
                    name = node.TableName,
                    SqlType = searchDto.SqlType,
                    SqlConnId = searchDto.SqlConnId,
                    Database = node.DatabaseName,
                    Schema = node.SchemaName,
                    Search = node.TableName,
                    icon = GetIcon("table"),

                };
                if (searchDto.SqlType.Equals("sqlserver", StringComparison.OrdinalIgnoreCase)) {
                    if (node.SchemaName != "dbo") {
                        dto.name = node.SchemaName + "." + node.TableName;
                    }
                }
                if (searchDto.SqlType.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase)) {
                    if (node.SchemaName != "public") {
                        dto.name = node.SchemaName + "." + node.TableName;
                    }
                }
                tree.Add(dto);
            }
            return tree;
        }
        private List<SqlTreeDto> GetSqlTree(List<ViewEntity> sqlConnDtos, SqlSearchDto searchDto)
        {
            List<SqlTreeDto> tree = new List<SqlTreeDto>();
            foreach (var node in sqlConnDtos) {
                var dto = new SqlTreeDto() {
                    id = "v-" + Guid.NewGuid().ToString().Replace("-", ""),
                    pId = searchDto.Id,
                    name = node.ViewName,
                    SqlType = searchDto.SqlType,
                    SqlConnId = searchDto.SqlConnId,
                    Database = node.DatabaseName,
                    Schema = node.SchemaName,
                    Search = node.ViewName,
                    icon = GetIcon("view"),
                };
                if (searchDto.SqlType.Equals("sqlserver", StringComparison.OrdinalIgnoreCase)) {
                    if (node.SchemaName != "dbo") {
                        dto.name = node.SchemaName + "." + node.ViewName;
                    }
                }
                if (searchDto.SqlType.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase)) {
                    if (node.SchemaName != "public") {
                        dto.name = node.SchemaName + "." + node.ViewName;
                    }
                }
                tree.Add(dto);
            }
            return tree;
        }
        private List<SqlTreeDto> GetSqlTree(List<ProcedureEntity> sqlConnDtos, SqlSearchDto searchDto)
        {
            List<SqlTreeDto> tree = new List<SqlTreeDto>();
            foreach (var node in sqlConnDtos) {
                var dto = new SqlTreeDto() {
                    id = "p-" + Guid.NewGuid().ToString().Replace("-", ""),
                    pId = searchDto.Id,
                    name = node.ProcedureName,
                    SqlType = searchDto.SqlType,
                    SqlConnId = searchDto.SqlConnId,
                    Database = node.DatabaseName,
                    Schema = node.SchemaName,
                    Search = node.ProcedureName,
                    icon = GetIcon("procedure"),
                };
                if (searchDto.SqlType.Equals("sqlserver", StringComparison.OrdinalIgnoreCase)) {
                    if (node.SchemaName != "dbo") {
                        dto.name = node.SchemaName + "." + node.ProcedureName;
                    }
                }
                if (searchDto.SqlType.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase)) {
                    if (node.SchemaName != "public") {
                        dto.name = node.SchemaName + "." + node.ProcedureName;
                    }
                }
                tree.Add(dto);
            }
            return tree;
        }

        private List<SqlTreeDto> GetSqlTree(List<FunctionEntity> sqlConnDtos, SqlSearchDto searchDto)
        {
            List<SqlTreeDto> tree = new List<SqlTreeDto>();
            foreach (var node in sqlConnDtos) {
                var dto = new SqlTreeDto() {
                    id = "f-" + Guid.NewGuid().ToString().Replace("-", ""),
                    pId = searchDto.Id,
                    name = node.FunctionName,
                    SqlType = searchDto.SqlType,
                    SqlConnId = searchDto.SqlConnId,
                    Database = node.DatabaseName,
                    Schema = node.SchemaName,
                    Search = node.FunctionName,
                    icon = GetIcon("function"),
                };
                if (searchDto.SqlType.Equals("sqlserver", StringComparison.OrdinalIgnoreCase)) {
                    if (node.SchemaName != "dbo") {
                        dto.name = node.SchemaName + "." + node.FunctionName;
                    }
                }
                if (searchDto.SqlType.Equals("PostgreSQL", StringComparison.OrdinalIgnoreCase)) {
                    if (node.SchemaName != "public") {
                        dto.name = node.SchemaName + "." + node.FunctionName;
                    }
                }
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
        public async Task<IActionResult> GetDatabaseInfos([FromBody] Req<SqlSearchDto> request)
        {
            var list = await _sqlOnlineApplication.GetTableShowColumns(AdminDto.Id, request.Data.SqlConnId, request.Data.Database);
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
                        if (item.TableType.Trim() == "t" || item.TableType == "BASE TABLE"
                            || item.TableType.Equals("table", StringComparison.OrdinalIgnoreCase)
                            || item.TableType.Trim().Equals("u", StringComparison.OrdinalIgnoreCase)
                            ) {
                            model.Type = "t";
                        } else {
                            model.Type = "v";
                        }
                        dict[key] = model;
                        models.Add(model);
                    }
                    model.Items.Add(new StructureItemModel(item));
                }
                return Success(models, request.PasswordString);
            }
            return Error();
        }
        #endregion

        [IgnoreAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> GetOutTableInfo(SqlSearchDto dto)
        {
            var list = await _sqlOnlineApplication.GetTableShowColumns(AdminDto.Id, dto.SqlConnId, dto.Database);
            List<StructureModel> models = new List<StructureModel>();
            if (list != null) {
                Dictionary<string, StructureModel> dict = new Dictionary<string, StructureModel>();
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
                        if (item.TableType.Trim() == "t" || item.TableType == "BASE TABLE"
                            || item.TableType.Equals("table", StringComparison.OrdinalIgnoreCase)
                            || item.TableType.Trim().Equals("u", StringComparison.OrdinalIgnoreCase)
                            ) {
                            model.Type = "t";
                        } else {
                            model.Type = "v";
                        }
                        dict[key] = model;
                        models.Add(model);
                    }
                    model.Items.Add(new StructureItemModel(item));
                }
            }
            var filePath = MyHostingEnvironment.MapWebRootPath("/_/outTableInfo/index.html");
            var html = System.IO.File.ReadAllText(filePath);
            StringBuilder stringBuilder = new StringBuilder(html);
            stringBuilder.Replace("[[DatabaseName]]", dto.Database);
            stringBuilder.Replace("[[data]]", Newtonsoft.Json.JsonConvert.SerializeObject(models));

            using (var archive = ZipArchive.Create()) {
                var folderPath = MyHostingEnvironment.MapWebRootPath("/_/outTableInfo/");
                archive.AddEntry("css/dbs.css", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, "css/dbs.css")).OpenRead(), true);
                archive.AddEntry("css/global.css", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, "css/global.css")).OpenRead(), true);
                archive.AddEntry("img/dbs/bg.gif", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, "img/dbs/bg.gif")).OpenRead(), true);
                archive.AddEntry("img/dbs/icon-proc.gif", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, "img/dbs/icon-proc.gif")).OpenRead(), true);
                archive.AddEntry("img/dbs/icon-table.gif", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, "img/dbs/icon-table.gif")).OpenRead(), true);
                archive.AddEntry("img/dbs/icon-view.gif", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, "img/dbs/icon-view.gif")).OpenRead(), true);

                archive.AddEntry(@"js\layer\skin\default\icon.png", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, @"js\layer\skin\default\icon.png")).OpenRead(), true);
                archive.AddEntry(@"js\layer\skin\default\icon-ext.png", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, @"js\layer\skin\default\icon-ext.png")).OpenRead(), true);

                archive.AddEntry(@"js\layer\skin\default\layer.css", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, @"js\layer\skin\default\layer.css")).OpenRead(), true);
                archive.AddEntry(@"js\layer\skin\default\loading-0.gif", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, @"js\layer\skin\default\loading-0.gif")).OpenRead(), true);
                archive.AddEntry(@"js\layer\skin\default\loading-1.gif", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, @"js\layer\skin\default\loading-1.gif")).OpenRead(), true);
                archive.AddEntry(@"js\layer\skin\default\loading-2.gif", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, @"js\layer\skin\default\loading-2.gif")).OpenRead(), true);


                archive.AddEntry(@"js\layer\layer.js", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, @"js\layer\layer.js")).OpenRead(), true);
                archive.AddEntry(@"js\clipboard.js", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, @"js\clipboard.js")).OpenRead(), true);
                archive.AddEntry(@"js\doT.min.js", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, @"js\doT.min.js")).OpenRead(), true);
                archive.AddEntry(@"js\jquery.min.js", new System.IO.FileInfo(System.IO.Path.Combine(folderPath, @"js\jquery.min.js")).OpenRead(), true);

                archive.AddEntry(@"index.html", new System.IO.MemoryStream(Encoding.UTF8.GetBytes(stringBuilder.ToString())), true);

                using (var ms = new System.IO.MemoryStream()) {
                    archive.SaveTo(ms);
                    return File(ms.ToArray(), "application/zip",  dto.Database + "_tableInfo.zip");
                }
            }
        }


    }
}
