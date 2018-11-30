using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.Application;
using ToolGood.ApplicationCommon;
using ToolGood.Infrastructure;

namespace ToolGood.Server.Areas.Admin.Controllers
{
    [Area(KeywordString.AdminArea)]
    public class LogController : AdminBaseController
    {
        private readonly IAdminApplication _adminApplication;

        public LogController(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        [HttpGet]
        [AdminAuth("Log-AdminLoginLog", ActionPermissionType.Show)]
        public IActionResult AdminLoginLog()
        {
            return View();
        }

        [HttpPost]
        [AdminAuth("Log-AdminLoginLog", ActionPermissionType.Show)]
        public IActionResult AdminLoginLogQuery(int page = 1, int pageSize = 50)
        {
            var r = _adminApplication.AdminLoginLogQuery(page, pageSize);
            if (r.IsSuccess) {
                return LayuiSuccess(r.Value);
            }
            return LayuiError(r.Message);
        }

        [HttpGet]
        [AdminAuth("Log-SqlQueryLog", ActionPermissionType.Show)]
        public IActionResult SqlQueryLog()
        {
            return View();
        }

        [HttpPost]
        [AdminAuth("Log-SqlQueryLog", ActionPermissionType.Show)]
        public IActionResult SqlQueryLogQuery(int page = 1, int pageSize = 50)
        {
            var r = _adminApplication.SqlQueryLogQuery(page, pageSize);
            if (r.IsSuccess) {
                return LayuiSuccess(r.Value);
            }
            return LayuiError(r.Message);
        }


    }
}