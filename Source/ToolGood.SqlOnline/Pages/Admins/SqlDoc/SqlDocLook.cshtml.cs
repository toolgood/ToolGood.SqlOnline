/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Admins.SqlDoc
{
    [AdminMenu("SqlDoc", "show")]
    public class SqlDocLookModel : AdminPageModel
    {
        private readonly IAdminDatabaseConnApplication _databaseConnApplication;

        public SqlDocLookModel(IAdminDatabaseConnApplication databaseConnApplication)
        {
            _databaseConnApplication = databaseConnApplication;
        }


        public AdminSqlDocDto SqlDoc { get; private set; }


        public async Task<IActionResult> OnGetAsync(int id = 0)
        {
            if (id <= 0) { return Redirect("/NotFound"); }
            SqlDoc = await _databaseConnApplication.GetSqlDocById(id);
            if (SqlDoc == null) { return Redirect("/NotFound"); }

            ViewData["Title"] = "查看SQL";
            return Page();
        }


    }
}
