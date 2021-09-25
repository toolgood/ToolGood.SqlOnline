/*!
 *  ��Ȩ����(C) 2021 ToolGood(��֪��)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Configs;
using ToolGood.SqlOnline.Datas;

namespace ToolGood.SqlOnline.Pages.Admins.Securitys
{
    [AdminMenu("IpBanlist", "edit")]
    public class IpBanEditModel : AdminPageModel
    {
        private readonly IAdminSecurityApplication _adminSecurityApplication;

        public IpBanEditModel(IAdminSecurityApplication ipAddressApplication)
        {
            _adminSecurityApplication = ipAddressApplication;
        }

        public DbSysIpBanlist IPAddress { get; private set; }


        public async Task<IActionResult> OnGetAsync(int id = 0)
        {
            if (id < 0) { return Redirect(UrlSetting.NotFoundUrl); }
            if (id == 0) {
                IPAddress = new DbSysIpBanlist();

                ViewData["Title"] = "����IP������";
            } else {
                IPAddress = await _adminSecurityApplication.GetIpBanById(id);
                if (IPAddress == null) { return Redirect(UrlSetting.NotFoundUrl); }

                ViewData["Title"] = "�༭IP������";
            }
            return Page();
        }

    }
}
