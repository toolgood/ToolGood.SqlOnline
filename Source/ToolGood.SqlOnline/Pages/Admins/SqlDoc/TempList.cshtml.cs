/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Admins.SqlDoc
{
    [AdminMenu("SqlDocTemp", "show")]
    public class TempListModel : AdminPageModel
    {
        private readonly IAdminApplication _adminApplication;

        public TempListModel(IAdminApplication adminApplication)
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
