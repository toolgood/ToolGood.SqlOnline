using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.Application;
using ToolGood.ApplicationCommon;
using ToolGood.Infrastructure;
using ToolGood.TransDto;

namespace ToolGood.SqlOnline.Areas.Admin.Controllers
{
    [Area(KeywordString.AdminArea)]
    public class DatabaseController : AdminBaseController
    {
        private readonly IDatabaseInfoApplication _databaseInfoApplication;

        public DatabaseController(IDatabaseInfoApplication databaseInfoApplication)
        {
            _databaseInfoApplication = databaseInfoApplication;
        }

        [HttpGet]
        [AdminAuth("Admin-Database", ActionPermissionType.Show)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AdminAuth("Admin-Database", ActionPermissionType.Show)]
        public IActionResult GetPageList(PageQueryModel model)
        {
            if (model == null) { return LayuiError("参数无效！"); }
            var r = _databaseInfoApplication.GetPageInfos(model.Page, model.PageSize);
            if (r.IsSuccess) { return LayuiSuccess(r.Value); }
            return LayuiError(r.Message);
        }

        [HttpPost]
        [AdminAuth("Admin-Database", ActionPermissionType.Show)]
        public IActionResult GetInfo(int id = 0)
        {
            if (id == 0) { return Error("Id 无效！"); }
            var r = _databaseInfoApplication.GetInfo(id);
            if (r.IsSuccess) {
                return Success(r.Value);
            }
            return Error(r.Message);
        }


        [HttpGet]
        [AdminAuth("Admin-Database", ActionPermissionType.Add)]
        public IActionResult AddInfo()
        {
            return View();
        }

        [HttpPost]
        [AdminAuth("Admin-Database", ActionPermissionType.Add)]
        public IActionResult AddInfo(DatabaseModel model)
        {
            if (model == null) { return LayuiError("参数无效！"); }
            var db = model.ToDatabaseInfo();
            db.CreateAdminId = Admin.Id;
            db.CreateAdminName = Admin.Name;

            var r = _databaseInfoApplication.AddInfo(db);
            if (r.IsSuccess) { return Success(); }
            return Error(r.Message);
        }

        [HttpGet]
        [AdminAuth("Admin-Database", ActionPermissionType.Show)]
        public IActionResult EditInfo(int id)
        {
            ViewData["id"] = id;
            return View();
        }

        [HttpPost]
        [AdminAuth("Admin-Database", ActionPermissionType.Add)]
        public IActionResult EditInfo(DatabaseModel model)
        {
            if (model == null) { return LayuiError("参数无效！"); }
            var db = model.ToUpDatabaseInfo();
            db.ChangeAdminId = Admin.Id;
            db.ChangeAdminName = Admin.Name;

            var r = _databaseInfoApplication.EditInfo(db);
            if (r.IsSuccess) { return Success(); }
            return Error(r.Message);
        }



        [HttpPost]
        [AdminAuth("Admin-Database", ActionPermissionType.Delete)]
        public IActionResult DeleteInfo(int id)
        {
            var r = _databaseInfoApplication.DeleteInfo(id, Admin.Id, Admin.Name);
            if (r.IsSuccess) {
                return Success();
            }
            return Error(r.Message);
        }


    }
}