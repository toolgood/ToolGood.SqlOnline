/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Sqls.Editors
{

    [AdminMenu("SqlOnlineDesktop", "show")]
    public class SqlEditorModel : AdminPageModel
    {
        private readonly ISqlOnlineApplication _sqlOnlineApplication;

        public SqlEditorModel(ISqlOnlineApplication sqlOnlineApplication)
        {
            _sqlOnlineApplication = sqlOnlineApplication;
        }
        public int ConnId { get; private set; }
        public string DatabaseName { get; private set; }
        public List<DatabaseEntity> DatabaseEntities { get; private set; }


        public async Task<IActionResult> OnGet( int sqlConnId, string databaseName)
        {
            ConnId = sqlConnId;
            DatabaseName = databaseName;
            SqlSearchDto search = new SqlSearchDto() {
                AdminId = AdminDto.Id,
                SqlConnId = sqlConnId,
                Database = databaseName,
            };

            DatabaseEntities = await _sqlOnlineApplication.GetDatabaseNames(search);
            if (string.IsNullOrEmpty(DatabaseName)) {
                if (DatabaseEntities.Count > 0) {
                    DatabaseName = DatabaseEntities[0].DatabaseName;
                }
            }
            return Page();
        }
    }
}
