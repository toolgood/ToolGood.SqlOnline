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

namespace ToolGood.SqlOnline.Pages.Admins.Admin
{
    [Route("/admins/admin/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {
        private readonly IAdminApplication _adminApplication;

        public AjaxController(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        [IgnoreAntiforgeryToken]
        [AdminMenu("Admin", "show")]
        [HttpPost]
        public async Task<IActionResult> GetAdminList([FromBody] Req<GetAdminListDto> request)
        {
            try {
                var Page = await _adminApplication.GetAdminPage(request);
                    //request.Data.Name, request.Data.TrueName, request.Data.Phone, request.Data.GroupId,
                    //request.Data.IsFrozen, request.Data.Field, request.Data.Order, request.Data.PageIndex, request.Data.PageSize);
                return LayuiSuccess(Page, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }

        [AdminMenu("Admin", "edit")]
        [HttpPost]
        public async Task<IActionResult> AddAdmin([FromBody] Req<AdminAddDto> request)
        {
            var loginPasswordDto = await _adminApplication.GetSettingValueByCode("LoginPassword");
            var managerPasswordDto = await _adminApplication.GetSettingValueByCode("ManagerPassword");
            var loginPassword = loginPasswordDto != null ? loginPasswordDto.Value : "a123456";
            var managerPassword = managerPasswordDto != null ? managerPasswordDto.Value : "a123456789";


            if (string.IsNullOrEmpty(request.Data.Password)) {
                request.Data.Password = loginPassword;
            }
            if (string.IsNullOrEmpty(request.Data.ManagerPassword)) {
                request.Data.ManagerPassword = managerPassword;
            }

            var b = await _adminApplication.AddAdmin(request);
            if (b == false) return Error(request.Message);
            return Success();
        }

        [AdminMenu("Admin", "edit")]
        [HttpPost]
        public async Task<IActionResult> EditAdmin([FromBody] Req<AdminEditDto> request)
        {
            var b = await _adminApplication.EditAdmin(request);
            if (b == false) return Error(request.Message);
            return Success();
        }

        [AdminMenu("Admin", "delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteAdmin([FromBody] Req<AdminIdDto> request)
        {
            var b = await _adminApplication.DeleteAdmin(request);
            if (b == false) return Error(request.Message);
            return Success();
        }

        [AdminMenu("Admin", "edit")]
        [HttpPost]
        public async Task<IActionResult> ChangePasswordForce([FromBody] Req<AdminChangePwdForceDto> request)
        {
            var loginPasswordDto = await _adminApplication.GetSettingValueByCode("LoginPassword");
            var managerPasswordDto = await _adminApplication.GetSettingValueByCode("ManagerPassword");
            var loginPassword = loginPasswordDto != null ? loginPasswordDto.Value : "a123456";
            var managerPassword = managerPasswordDto != null ? managerPasswordDto.Value : "a123456789";

            request.Data.NewPassword = loginPassword;
            request.Data.NewManagerPassword = managerPassword;
            var b = await _adminApplication.ChangePassword(request);
            if (b == false) return Error(request.Message);
            return Success();
        }

    }
}
