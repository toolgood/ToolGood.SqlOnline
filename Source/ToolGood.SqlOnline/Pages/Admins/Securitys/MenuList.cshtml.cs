/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Admins.Securitys
{
    [AdminMenu("AdminMenu", "show")]
    public class MenuListModel : AdminPageModel
    {
        private readonly IAdminApplication _adminApplication;

        public MenuListModel(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }
        public IAdminButtonPass ButtonPass { get; private set; }


        public async Task<IActionResult> OnGetAsync()
        {

            ButtonPass = await _adminApplication.GetButtonPassByAdminId(AdminDto.Id, ViewData["MenuCode"].ToString());
            return Page();
        }
    }
}
