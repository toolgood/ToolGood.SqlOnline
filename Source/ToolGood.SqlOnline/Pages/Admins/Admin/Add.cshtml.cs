/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Datas;

namespace ToolGood.SqlOnline.Pages.Admins.Admin
{
    [AdminMenu("Admin", "edit")]
    public class AddModel : AdminPageModel
    {
        private readonly IAdminApplication _adminApplication;

        public AddModel(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }


        public List<DbSysAdminGroup> Groups { get; private set; }

        public string LoginPassword { get; private set; }
        public string ManagerPassword { get; private set; }


        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["Title"] = "添加管理员";

            Groups = await _adminApplication.GetAdminGroupAll();

            var loginPasswordDto = await _adminApplication.GetSettingValueByCode("LoginPassword");
            var managerPasswordDto = await _adminApplication.GetSettingValueByCode("ManagerPassword");
            LoginPassword = loginPasswordDto != null ? loginPasswordDto.Value : "a123456";
            ManagerPassword = managerPasswordDto != null ? managerPasswordDto.Value : "a123456789";

            return Page();
        }
    }
}
