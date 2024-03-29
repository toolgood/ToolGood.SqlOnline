/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Admins.Securitys
{
    [AdminMenu("IpBanlist", "show")]
    public class IpBanListModel : AdminPageModel
    {
        private readonly IAdminApplication _adminApplication;

        public IpBanListModel(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        public IAdminButtonPass ButtonPass { get; private set; }

        public async void OnGetAsync()
        {
            ButtonPass = await _adminApplication.GetButtonPassByAdminId(AdminDto.Id, ViewData["MenuCode"].ToString());

        }




    }
}
