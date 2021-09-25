/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;

namespace ToolGood.SqlOnline.Pages.Admins.MachineCode
{
    [AdminMenu("MachineCode", "edit")]
    public class BindingModel : AdminPageModel
    {
        private readonly IAdminApplication _adminApplication;

        public BindingModel(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }
        public Dictionary<int, string> AdminInfos;

        public async Task<IActionResult> OnGetAsync()
        {
            var all = await _adminApplication.GetAdminSimpleAll();
            AdminInfos = new Dictionary<int, string>();

            foreach (var item in all) {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"[{item.Id}]{item.Name}\t(");
                if (string.IsNullOrEmpty(item.JobNo) == false) {
                    stringBuilder.Append("[");
                    stringBuilder.Append(item.JobNo);
                    stringBuilder.Append("]");
                }
                stringBuilder.Append(item.TrueName);
                stringBuilder.Append(")");
                if (string.IsNullOrEmpty(item.Phone) == false) {
                    stringBuilder.Append('\t');
                    stringBuilder.Append(item.Phone);
                }
                AdminInfos[item.Id] = stringBuilder.ToString();
            }
            return Page();
        }
    }
}
