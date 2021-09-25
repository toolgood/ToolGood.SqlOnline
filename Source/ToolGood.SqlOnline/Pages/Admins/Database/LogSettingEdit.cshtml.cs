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

                ViewData["Title"] = "新增日志输出方案";
            } else {
                LogSetting = await _databaseConnApplication.GetLogSettingById(id);
                if (LogSetting == null) { return Redirect("/NotFound"); }

                ViewData["Title"] = "编辑日志输出方案";
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
