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

namespace ToolGood.SqlOnline.Pages.Admins.MachineCode
{
    [Route("/admins/MachineCode/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {
        private readonly IAdminApplication _adminApplication;

        public AjaxController(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        [IgnoreAntiforgeryToken]
        [AdminMenu("MachineCode", "show")]
        [HttpPost]
        public async Task<IActionResult> GetAdminMachineCodeList([FromBody] Req<AdminMachineCodeSearchDto> request)
        {
            try {
                var Page = await _adminApplication.GetAdminMachineCodePage(request);
                return LayuiSuccess(Page, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }
        [AdminMenu("MachineCode", "edit")]
        [HttpPost]
        public async Task<IActionResult> AddAdminMachineCode([FromBody] Req<AdminMachineCodeEditDto> request)
        {
            var b = await _adminApplication.AddAdminMachineCode(request);
            if (b == false) return Error(request.Message);
            return Success();
        }

        [AdminMenu("MachineCode", "delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteAdminMachineCode([FromBody] Req<AdminIdDto> request)
        {
            var b = await _adminApplication.DeleteAdminMachineCode(request);
            if (b == false) return Error(request.Message);
            return Success();
        }





    }
}
