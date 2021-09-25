/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.WebCommon.Controllers;

namespace ToolGood.SqlOnline.Pages.Developments
{
    public class TableInfoModel : PageModelBaseCore
    {
        private readonly IDevelopmentApplication _sqlOnlineApplication;

        public TableInfoModel(IDevelopmentApplication sqlOnlineApplication)
        {
            _sqlOnlineApplication = sqlOnlineApplication;
        }


        public List<DatabaseEntity> DatabaseEntities { get; private set; }
        public int ConnId { get; private set; }
        public string DatabaseName { get; private set; }

        public async Task<IActionResult> OnGetAsync(int sqlConnId = 0, string database = null)
        {
            if (sqlConnId <= 0) { return Redirect("/NotFound"); }
            var use = await _sqlOnlineApplication.UseDevelopment();
            if (use == false) { return Redirect("/Developments/Prompt"); }

            ConnId = sqlConnId;
            DatabaseName = database;
            DatabaseEntities = await _sqlOnlineApplication.GetDatabaseNames(sqlConnId);
            if (string.IsNullOrEmpty(DatabaseName)) {
                if (DatabaseEntities.Count > 0) {
                    DatabaseName = DatabaseEntities[0].DatabaseName;
                }
            }
            return Page();
        }



    }
}
