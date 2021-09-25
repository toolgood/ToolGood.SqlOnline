/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.Common.Utils;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Configs;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;
using ToolGood.WebCommon.Utils;

namespace ToolGood.SqlOnline.Pages.Admins.Securitys
{
    [Route("/admins/Securitys/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {
        private readonly IAdminApplication _adminApplication;
        private readonly IAdminSecurityApplication _adminSecurityApplication;
        public AjaxController(IAdminSecurityApplication adminSecurityApplication, IAdminApplication adminApplication)
        {
            _adminSecurityApplication = adminSecurityApplication;
            _adminApplication = adminApplication;
        }

        [IgnoreAntiforgeryToken]
        [AdminMenu("AdminMenu", "show")]
        [HttpPost]
        public async Task<IActionResult> GetAdminMenuList([FromBody] Req<AdminIdDto> request)
        {
            try {
                var menus = await _adminApplication.GetMenuAll();
                var SysAdminMenus = ListOrderUtil.OrderByAscOnPid(menus, q => q.ParentId, q => q.Id);
                var Buttons = await _adminApplication.GetMenuButtonAll();
                foreach (var item in SysAdminMenus) {
                    List<string> list = new List<string>();
                    foreach (var actionName in item.Buttons.Split('|')) {
                        list.Add(Buttons.FirstOrDefault(q => q.ButtonCode == actionName).SelectNull(q => q.ButtonName));
                    }
                    list.RemoveAll(q => string.IsNullOrEmpty(q));
                    item.Buttons = string.Join("、", list);
                }
                return LayuiSuccess(SysAdminMenus, request.PasswordString);

            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }


        [AdminMenu("AdminMenu", "edit")]
        [HttpPost]
        public async Task<IActionResult> AdminModeEditPost([FromBody] Req<AdminModeEditDto> request)
        {
            if (AdminDto.IsAdminMode() == false && await _adminApplication.GetMenuCheck(ViewData["MenuCode"].ToSafeString(), ViewData["ButtonCode"].ToSafeString())) {
                if (string.IsNullOrEmpty(request.OperatorPassword)) { return Error("TryAdminMode"); }
                if (await _adminApplication.CheckPassword(request.OperatorId, request.OperatorPassword, request) == false) { return Error("密码不正确"); }
                AdminDto.SetAdminMode(DateTime.Now.AddMinutes(request.AdminModeTime));
                SetSession(SessionSetting.AdminSession, AdminDto);
            }

            var b = await _adminApplication.EditMenuMode(request);
            if (b == false) return Error(request.Message);
            return Success();
        }



        [IgnoreAntiforgeryToken]
        [AdminMenu("IpBanlist", "show")]
        [HttpPost]
        public async Task<IActionResult> GetIpBanList([FromBody] Req<GetIpAddressDto> request)
        {
            try {
                var Pages = await _adminSecurityApplication.GetIpBanPage(request);
                return LayuiSuccess(Pages, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }

        [AdminMenu("IpBanlist", "edit")]
        [HttpPost]
        public async Task<IActionResult> EditIpBan([FromBody] Req<IpAddressEditDto> request)
        {
            bool b;
            if (request.Data.Id > 0) {
                b = await _adminSecurityApplication.EditIpBan(request);
            } else {
                b = await _adminSecurityApplication.AddIpBan(request);
            }
            if (b == false) return Error(request.Message);
            await IpAddressHelper.InitIpBlack(_adminSecurityApplication);
            return Success();
        }

        [AdminMenu("IpBanlist", "delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteIpBan([FromBody] Req<AdminIdDto> request)
        {
            var b = await _adminSecurityApplication.DeleteIpBan(request);
            if (b == false) return Error(request.Message);
            await IpAddressHelper.InitIpBlack(_adminSecurityApplication);
            return Success();
        }



        [IgnoreAntiforgeryToken]
        [AdminMenu("IpAllowlist", "show")]
        [HttpPost]
        public async Task<IActionResult> GetIpAllowList([FromBody] Req<GetIpAddressDto> request)
        {

            try {
                var Pages = await _adminSecurityApplication.GetIpAllowPage(request);
                return LayuiSuccess(Pages, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }

        [AdminMenu("IpAllowlist", "edit")]
        [HttpPost]
        public async Task<IActionResult> EditIpAllow([FromBody] Req<IpAddressEditDto> request)
        {
            bool b;
            if (request.Data.Id > 0) {
                b = await _adminSecurityApplication.EditIpAllow(request);
            } else {
                b = await _adminSecurityApplication.AddIpAllow(request);
            }
            if (b == false) return Error(request.Message);
            await IpAddressHelper.InitIpWhite(_adminSecurityApplication);
            return Success();
        }

        [AdminMenu("IpAllowlist", "delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteIpAllow([FromBody] Req<AdminIdDto> request)
        {
            var b = await _adminSecurityApplication.DeleteIpAllow(request);
            if (b == false) return Error(request.Message);
            await IpAddressHelper.InitIpWhite(_adminSecurityApplication);
            return Success();
        }


        [IgnoreAntiforgeryToken]
        [AdminMenu("AdminLoginLog", "show")]
        [HttpPost]
        public async Task<IActionResult> GetAdminLoginList([FromBody] Req<GetAdminLoginListDto> request)
        {
            try {
                var Pages = await _adminSecurityApplication.GetAdminLoginLogPage(request.Data.Name, request.Data.Success, request.Data.Ip, request.Data.Field, request.Data.Order, request.Data.PageIndex, request.Data.PageSize);
                return LayuiSuccess(Pages, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }

        [IgnoreAntiforgeryToken]
        [AdminMenu("AdminOperationLog", "show")]
        [HttpPost]
        public async Task<IActionResult> GetAdminOperationList([FromBody] Req<GetAdminOperationListDto> request)
        {
            try {
                var Pages = await _adminSecurityApplication.GetAdminOperationLogPage(request.Data.Name, request.Data.Message, request.Data.Ip, request.Data.Field, request.Data.Order, request.Data.PageIndex, request.Data.PageSize);
                return LayuiSuccess(Pages, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }

        [HttpPost]
        [AdminMenu("SecuritySetting", "edit")]
        public async Task<IActionResult> EditSecuritySetting([FromBody] Req<SecuritySettingDto> request)
        {
            var b = await _adminSecurityApplication.SetSecuritySetting(request);
            if (b == false) return Error(request.Message);
            CacheHelper.UpdateWatermarkText();
            return Success();
        }

    }
}
