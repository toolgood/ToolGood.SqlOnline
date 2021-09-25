/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Pages.Sqls.Docs
{
    [Route("/Sqls/Docs/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {
        private readonly ISqlOnlineApplication _sqlOnlineApplication;
        public AjaxController(ISqlOnlineApplication sqlOnlineApplication)
        {
            _sqlOnlineApplication = sqlOnlineApplication;
        }

        [IgnoreAntiforgeryToken]
        [AdminMenu("SqlOnlineDesktop", "show")]
        [HttpPost]
        public async Task<IActionResult> GetSqlDocList([FromBody] Req<AdminSqlDocSearchDto> request)
        {
            request.Data.AdminId = request.OperatorId;
            if (request.Data.SqlDocType == 0) {
                var page = await _sqlOnlineApplication.GetSelfSqlDocList(request.Data);
                return LayuiSuccess(page, request.PasswordString);
            } else if (request.Data.SqlDocType == 1) {
                var page = await _sqlOnlineApplication.GetSelfSqlDocTempList(request.Data);
                return LayuiSuccess(page, request.PasswordString);
            } else if (request.Data.SqlDocType == 2) {
                var page = await _sqlOnlineApplication.GetShareSqlDocList(request.Data);
                return LayuiSuccess(page, request.PasswordString);
            }
            return Error();
        }

        [AdminMenu("SqlOnlineDesktop", "show")]
        [HttpPost]
        public async Task<IActionResult> EditSqlDoc([FromBody] Req<SqlDocDto> request)
        {
            bool b;
            if (request.Data.Id > 0) {
                b = await _sqlOnlineApplication.EditSqlDoc(request);
            } else {
                b = await _sqlOnlineApplication.AddSqlDoc(request);
            }
            if (b == false) return Error(request.Message);
            return Success(request.Data.Id);
        }

        [AdminMenu("SqlOnlineDesktop", "show")]
        [HttpPost]
        public async Task<IActionResult> DeleteSqlDoc([FromBody] Req<AdminIdDto> request)
        {
            var b = await _sqlOnlineApplication.DeleteSqlDoc(request);
            if (b == false) return Error(request.Message);
            return Success();
        }

        [AdminMenu("SqlOnlineDesktop", "show")]
        [HttpPost]
        public async Task<IActionResult> EditSqlDocTemp([FromBody] Req<SqlDocDto> request)
        {
            bool b;
            if (request.Data.Id > 0) {
                b = await _sqlOnlineApplication.EditSqlDocTemp(request);
            } else {
                b = await _sqlOnlineApplication.AddSqlDocTemp(request);
            }
            if (b == false) return Error(request.Message);
            return Success(request.Data.Id);
        }

        [AdminMenu("SqlOnlineDesktop", "show")]
        [HttpPost]
        public async Task<IActionResult> DeleteSqlDocTemp([FromBody] Req<AdminIdDto> request)
        {
            var b = await _sqlOnlineApplication.DeleteSqlDocTemp(request);
            if (b == false) return Error(request.Message);
            return Success();
        }






    }
}
