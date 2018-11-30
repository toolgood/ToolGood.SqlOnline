using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.Application;
using ToolGood.ApplicationCommon;
using ToolGood.DomainService;
using ToolGood.Infrastructure;

namespace ToolGood.SqlOnline.Areas.SqlQuery.Controllers
{
    [Area("SqlQuery")]
    public class MainController : AdminBaseController
    {
        private readonly IAdminApplication _adminApplication;
        private readonly IAdminMenuApplication _adminMenuApplication;
        private readonly ISqlQueryApplication _sqlQueryApplication;
        private readonly ISqlProviderService _sqlProviderService;

        public MainController(IAdminApplication adminApplication, IAdminMenuApplication adminMenuApplication, ISqlQueryApplication sqlQueryApplication
            , ISqlProviderService sqlProviderService)
        {
            _adminApplication = adminApplication;
            _adminMenuApplication = adminMenuApplication;
            _sqlQueryApplication = sqlQueryApplication;
            _sqlProviderService = sqlProviderService;
        }


        [HttpGet]
        [AdminAuth("SqlDesktop", ActionPermissionType.Navbar)]
        public IActionResult Index()
        {
            var topMenus = _adminMenuApplication.GetTopAdminMenu(Admin.GroupId, "SqlDesktop").Value;
            ViewData["topMenu"] = topMenus;
            _sqlProviderService.InitSqlProvider();
            var trees = _sqlProviderService.GetServerTrees(Admin.GroupId);
            if (trees.Count > 0) {
                trees[0].Spread = true;
            }

            ViewData["trees"] = trees;

            return View();
        }

        [HttpGet]
        [AdminAuth("SqlDesktop", ActionPermissionType.Navbar)]
        public IActionResult NewQuery(string serverName, string databaseName)
        {
            ViewData["serverName"] = serverName;
            ViewData["databaseName"] = databaseName;
            ViewData["databaseNames"] = _sqlProviderService.GetDatabaseInfos(Admin.GroupId, serverName);

            return View();
        }

        [HttpGet]
        [AdminAuth("SqlDesktop", ActionPermissionType.Navbar)]
        public IActionResult SelectList200(string serverName, string databaseName, string tableName)
        {

            return View();
        }

        [HttpPost]
        [AdminAuth("SqlDesktop", ActionPermissionType.Navbar)]
        public IActionResult RunSql(string serverName, string databaseName, string sql)
        {
            try {
                var result = _sqlProviderService.RunSql(Admin.GroupId, serverName, databaseName, sql);
                return Success(result);
            } catch (Exception) {

            }
            return Error("失败");
        }


        [HttpPost]
        [AdminAuth("SqlDesktop", ActionPermissionType.Navbar)]
        public IActionResult GetDatabaseInfo(string serverName, string databaseName)
        {
            try {
                var result = _sqlProviderService.GetTableTrees(Admin.GroupId, serverName, databaseName);
                return Success(result);
            } catch (Exception) {
            }
            return Error("失败");
        }


    }
}