/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Configs;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Pages.Admins.Tools
{
    [Route("/admins/Tools/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {
        private readonly IAdminApplication _adminApplication;

        public AjaxController(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Req<AdminModeDto> request)
        {
            if (AdminDto.IsAdminMode() == false) {
                //if (string.IsNullOrEmpty(request.OperatorPassword)) { return Error("TryAdminMode"); }
                if (await _adminApplication.CheckPassword(request.OperatorId, request.OperatorPassword, request) == false) { return Error("密码不正确"); }
                AdminDto.SetAdminMode(DateTime.Now.AddMinutes(request.AdminModeTime));
                SetSession(SessionSetting.AdminSession, AdminDto);
            }
            return Success();
        }

        [HttpPost]
        [HttpGet]
        public IActionResult Logout()
        {
            AdminDto.SetAdminMode(DateTime.Now.AddMinutes(-10));
            SetSession(SessionSetting.AdminSession, AdminDto);
            if (Request.Headers.TryGetValue("Referer", out StringValues value)) {
                var val = value.ToSafeString();
                return Redirect(val);
            }
            return Redirect("/");
        }
    }
}
