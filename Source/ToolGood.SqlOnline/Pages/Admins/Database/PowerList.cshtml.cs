/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Datas;

namespace ToolGood.SqlOnline.Pages.Admins.Database
{
    [AdminMenu("DatabasePower", "show")]
    public class PowerListModel : AdminPageModel
    {
        private readonly IAdminApplication _adminApplication;

        public PowerListModel(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        public List<DbSysAdminGroup> AdminGroups { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AdminGroups = await _adminApplication.GetAdminGroupAll();
            ViewData["Title"] = "数据库权限";
            return Page();
        }

    }
}
