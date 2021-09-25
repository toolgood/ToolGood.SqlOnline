/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Pages.Admins.User
{
    [Route("/admins/User/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {
        private readonly IAdminApplication _adminApplication;

        public AjaxController(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        [HttpPost]
        public async Task<IActionResult> AdminMemberChangeManagerPassword([FromBody] Req<AdminChangePwdDto> request)
        {
            request.Data.AdminId = AdminDto.Id;
            var b = await _adminApplication.ChangeManagerPassword(request);
            if (b == false) return Error(request.Message);
            return Success();
        }

        [HttpPost]
        public async Task<IActionResult> AdminMemberChangePassword([FromBody] Req<AdminChangePwdDto> request)
        {
            request.Data.AdminId = AdminDto.Id;
            var b = await _adminApplication.ChangePassword(request);
            if (b == false) return Error(request.Message);
            return Success();
        }

        [HttpPost]
        public async Task<IActionResult> EditAdminMember([FromBody] Req<AdminEditDto> request)
        {
            request.Data.Id = AdminDto.Id;
            request.Data.Groups = new List<int>();
            request.Data.EditGroups = false;

            var b = await _adminApplication.EditAdmin(request);
            if (b == false) return Error(request.Message);
            return Success();
        }

    }
}
