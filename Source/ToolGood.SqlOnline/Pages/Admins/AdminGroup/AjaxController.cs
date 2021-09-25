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

namespace ToolGood.SqlOnline.Pages.Admins.AdminGroup
{
    [Route("/admins/AdminGroup/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {
        private readonly IAdminApplication _adminApplication;

        public AjaxController(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        [IgnoreAntiforgeryToken]
        [AdminMenu("AdminGroup", "show")]
        [HttpPost]
        public async Task<IActionResult> GetAdminGroupList([FromBody] Req<GetAdminGroupListDto> request)
        {
            try {
                var Page = await _adminApplication.GetAdminGroupPage(request);
                return LayuiSuccess(Page, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }


        [AdminMenu("AdminGroup", "edit")]
        [HttpPost]
        public async Task<IActionResult> EditAdminGroup([FromBody] Req<AdminGroupEditDto> request)
        {
            request.Data.Menus.RemoveAll(q => q.Pass == false);

            bool b;
            if (request.Data.Id == 0) {
                b = await _adminApplication.AddAdminGroup(request);
            } else {
                b = await _adminApplication.EditAdminGroup(request);
            }
            if (b == false) return Error(request.Message);
            return Success();
        }

        [AdminMenu("AdminGroup", "delete")]
        [HttpPost]
        public async Task<IActionResult> AdminGroupDelete([FromBody] Req<AdminIdDto> request)
        {
            var b = await _adminApplication.DeleteAdminGroup(request);
            if (b == false) return Error(request.Message);
            return Success();
        }

        //[AdminMenu("Admin", "edit")]
        //[HttpPost]
        //public async Task<IActionResult> GroupRelationshipPost([FromBody] AdminRelationshipsRequest request)
        //{
        //    request.Data.Items.RemoveAll(q => q.ButtonCode == null);

        //    var b = await _adminApplication.UpdateMenuPassByGroupId(request);
        //    if (b == false) return Error(request.ErrorMessage);
        //    return Success();
        //}

        //[AdminMenu("Admin", "edit")]
        //[HttpPost]
        //public async Task<IActionResult> MenuRelationshipPost([FromBody] AdminRelationshipsRequest request)
        //{
        //    request.Data.Items.RemoveAll(q => q.ButtonCode == null);

        //    var b = await _adminApplication.UpdateGroupPassByMenuId(request);
        //    if (b == false) return Error(request.ErrorMessage);
        //    return Success();
        //}


    }
}
