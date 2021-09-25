/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;
using ToolGood.WebCommon.Utils;

namespace ToolGood.SqlOnline.Pages.Admins.Database
{
    [Route("/admins/Database/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {
        private readonly IAdminDatabaseConnApplication _databaseConnApplication;

        public AjaxController(IAdminDatabaseConnApplication databaseConnApplication)
        {
            _databaseConnApplication = databaseConnApplication;
        }


        [IgnoreAntiforgeryToken]
        [AdminMenu("DatabaseConn", "show")]
        [HttpPost]
        public async Task<IActionResult> GetConnList([FromBody] Req<SearchDto> request)
        {
            try {
                var Page = await _databaseConnApplication.GetDatabaseConnAll(request);
                return LayuiSuccess(Page, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }

        [AdminMenu("DatabaseConn", "edit")]
        [HttpPost]
        public async Task<IActionResult> EditConn([FromBody] Req<SqlConnDto> request)
        {
            bool b;
            if (request.Data.Id > 0) {
                b = await _databaseConnApplication.EditDatabaseConn(request);
            } else {
                b = await _databaseConnApplication.AddDatabaseConn(request);
            }
            if (b == false) return Error(request.Message);
            return Success();
        }

        [AdminMenu("DatabaseConn", "delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteConn([FromBody] Req<AdminIdDto> request)
        {
            var b = await _databaseConnApplication.DeleteDatabaseConn(request);
            if (b == false) return Error(request.Message);
            return Success();
        }

        [IgnoreAntiforgeryToken]
        [AdminMenu("DatabasePower", "show")]
        [HttpPost]
        public async Task<IActionResult> GetConnPowerList([FromBody] Req<SearchDto> request)
        {
            try {
                var Page = await _databaseConnApplication.GetConnPowerList(request);
                List<SqlConnPassDto> result = new List<SqlConnPassDto>();
                foreach (var item in Page) {
                    if (item.CanRead) { result.Add(item); }
                }
                foreach (var item in Page) {
                    if (item.CanRead == false) {
                        item.ReadMaxRows = 500;
                        item.ChangeMaxRows = 10;
                        result.Add(item);
                    }
                }
                return LayuiSuccess(result, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }

        [AdminMenu("DatabaseConn", "edit")]
        [HttpPost]
        public async Task<IActionResult> EditConnPass([FromBody] Req<SqlConnPassEditDto> request)
        {
            var b = await _databaseConnApplication.EditDatabaseConnPass(request);
            if (b == false) return Error(request.Message);
            return Success();
        }



        [IgnoreAntiforgeryToken]
        [AdminMenu("DatabaseLog", "show")]
        [HttpPost]
        public async Task<IActionResult> GetLogSettingList([FromBody] Req<SearchDto> request)
        {
            try {
                var Page = await _databaseConnApplication.GetLogSettingList(request);
                return LayuiSuccess(Page, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }

        [AdminMenu("DatabaseLog", "edit")]
        [HttpPost]
        public async Task<IActionResult> EditLogSetting([FromBody] Req<SqlQueryLogSettingDto> request)
        {
            bool b;
            if (request.Data.Id > 0) {
                b = await _databaseConnApplication.EditLogSetting(request);
            } else {
                b = await _databaseConnApplication.AddLogSetting(request);
            }
            if (b == false) return Error(request.Message);
            return Success();
        }

        [AdminMenu("DatabaseLog", "delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteLogSetting([FromBody] Req<AdminIdDto> request)
        {
            var b = await _databaseConnApplication.DeleteLogSetting(request);
            if (b == false) return Error(request.Message);
            return Success();
        }




    }
}
