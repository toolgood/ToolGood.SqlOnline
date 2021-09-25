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
    [AdminMenu("SecuritySetting", "show")]
    public class SecuritySettingModel : AdminPageModel
    {
        private readonly IAdminSecurityApplication _adminSecurityApplication;

        public SecuritySettingModel(IAdminSecurityApplication adminSecurityApplication)
        {
            _adminSecurityApplication = adminSecurityApplication;
        }
        public SecuritySettingDto Dto { get; set; }


        public async Task<IActionResult> OnGet()
        {
            Dto = await _adminSecurityApplication.GetSecuritySetting();
            return Page();
        }
    }
}
