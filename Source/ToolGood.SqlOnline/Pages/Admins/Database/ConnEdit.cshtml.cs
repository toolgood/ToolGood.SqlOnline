/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Admins.Database
{
    [AdminMenu("DatabaseConn", "edit")]
    public class ConnEditModel : AdminPageModel
    {
        private readonly IAdminDatabaseConnApplication _databaseConnApplication;

        public ConnEditModel(IAdminDatabaseConnApplication databaseConnApplication)
        {
            _databaseConnApplication = databaseConnApplication;
        }

        public SqlConnDto SqlConnection { get; private set; }

        public List<string> SqlTypes;

        public async Task<IActionResult> OnGetAsync(int id = 0)
        {
            if (id < 0) { return Redirect("/NotFound"); }
            if (id == 0) {
                SqlConnection = new SqlConnDto();

                ViewData["Title"] = "新增连接字符串";
            } else {
                SqlConnection = await _databaseConnApplication.GetDatabaseConnById(id);
                if (SqlConnection == null) { return Redirect("/NotFound"); }

                ViewData["Title"] = "编辑连接字符串";
            }
            SqlTypes = _databaseConnApplication.GetSqlTypes();
            return Page();
        }

 
    }
}
