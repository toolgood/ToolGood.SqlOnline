/*!
 *  ��Ȩ����(C) 2021 ToolGood(��֪��)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Admins.Database
{
    public class LogSettingEditModel : AdminPageModel
    {

        private readonly IAdminDatabaseConnApplication _databaseConnApplication;

        public LogSettingEditModel(IAdminDatabaseConnApplication databaseConnApplication)
        {
            _databaseConnApplication = databaseConnApplication;
        }

        public SqlQueryLogSettingDto LogSetting { get; private set; }

        public Dictionary<int,string> Types { get; private set; }
        public async Task<IActionResult> OnGetAsync(int id = 0)
        {
            if (id < 0) { return Redirect("/NotFound"); }
            if (id == 0) {
                LogSetting = new SqlQueryLogSettingDto();

                ViewData["Title"] = "������־�������";
            } else {
                LogSetting = await _databaseConnApplication.GetLogSettingById(id);
                if (LogSetting == null) { return Redirect("/NotFound"); }

                ViewData["Title"] = "�༭��־�������";
            }
            Types = new Dictionary<int, string>();
            Types[1] = "Sqlite";
            Types[2] = "Web Post";
            Types[3] = "Sql Server";
            Types[4] = "Mysql";

            return Page();
        }
    }
}
