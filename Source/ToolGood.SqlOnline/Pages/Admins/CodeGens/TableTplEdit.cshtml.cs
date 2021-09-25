/*!
 *  ��Ȩ����(C) 2021 ToolGood(��֪��)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Admins.CodeGens
{
    [AdminMenu("TableTpl", "show")]
    public class TableTplEditModel : AdminPageModel
    {
        private readonly IAdminDatabaseConnApplication _databaseConnApplication;

        public TableTplEditModel(IAdminDatabaseConnApplication databaseConnApplication)
        {
            _databaseConnApplication = databaseConnApplication;
        }

        public SqlCodeGenDto TplContent { get; private set; }

        public async Task<IActionResult> OnGetAsync(int id = 0, int copy = 0)
        {
            if (id < 0) { return Redirect("/NotFound"); }
            if (id == 0) {
                TplContent = new SqlCodeGenDto();
                TplContent.Content = "";

                ViewData["Title"] = "����ģ��";
            } else {
                TplContent = await _databaseConnApplication.GetSqlCodeGenById(id);
                if (TplContent == null) { return Redirect("/NotFound"); }
                ViewData["Title"] = "�༭ģ��";

                if (copy == 1) {
                    TplContent.Id = 0;
                    TplContent.Title = "";
                    ViewData["Title"] = "����ģ��";
                }
            }
            return Page();
        }
    }
}
