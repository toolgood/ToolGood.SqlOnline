using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.ApplicationCommon;
using ToolGood.Infrastructure;

namespace ToolGood.Server.Areas.Admin.Controllers
{
    [Area(KeywordString.AdminArea)]
    public class HelpController : AdminBaseController
    {
        [HttpGet]
        [AdminAuth("Help-About", ActionPermissionType.Show)]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        [AdminAuth("Help-Feedback", ActionPermissionType.Show)]
        public IActionResult Feedback()
        {
            return View();
        }

        [HttpGet]
        [AdminAuth("Help-Doc", ActionPermissionType.Show)]
        public IActionResult Doc()
        {
            return View();
        }
    }
}