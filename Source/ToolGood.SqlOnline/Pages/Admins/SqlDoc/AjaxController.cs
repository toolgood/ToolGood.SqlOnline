/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;
using ToolGood.WebCommon.Utils;

namespace ToolGood.SqlOnline.Pages.Admins.SqlDoc
{
    [Route("/admins/SqlDoc/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {
        private readonly IAdminDatabaseConnApplication _databaseConnApplication;

        public AjaxController(IAdminDatabaseConnApplication databaseConnApplication)
        {
            _databaseConnApplication = databaseConnApplication;
        }


        [IgnoreAntiforgeryToken]
        [AdminMenu("SqlDoc", "show")]
        [HttpPost]
        public async Task<IActionResult> GetSqlDocList([FromBody] Req<AdminSqlDocSearchDto> request)
        {
            try {
                var Page = await _databaseConnApplication.GetSqlDocList(request);
                return LayuiSuccess(Page, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }
 
        [AdminMenu("SqlDoc", "delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteSqlDoc([FromBody] Req<AdminIdDto> request)
        {
            var b = await _databaseConnApplication.DeleteSqlDoc(request);
            if (b == false) return Error(request.Message);
            return Success();
        }


        [IgnoreAntiforgeryToken]
        [AdminMenu("SqlDocTemp", "show")]
        [HttpPost]
        public async Task<IActionResult> GetSqlDocTempList([FromBody] Req<AdminSqlDocSearchDto> request)
        {
            try {
                var Page = await _databaseConnApplication.GetSqlDocTempList(request);
                return LayuiSuccess(Page, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }

        [AdminMenu("SqlDocTemp", "delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteSqlDocTemp([FromBody] Req<AdminIdDto> request)
        {
            var b = await _databaseConnApplication.DeleteSqlDocTemp(request);
            if (b == false) return Error(request.Message);
            return Success();
        }

    }
}
