/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Datas;

namespace ToolGood.SqlOnline.Pages.Admins.User
{
    public class EditModel : AdminPageModel
    {
        private readonly IAdminApplication _adminApplication;

        public EditModel(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        public DbSysAdmin Admin { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Admin = await _adminApplication.GetAdminById(this.AdminDto.Id);
            return Page();
        }
    }
}
