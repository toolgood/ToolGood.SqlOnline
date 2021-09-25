/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dto;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Pages.Sqls.Editors
{

    [Route("/Sqls/Editors/Ajax/{action}")]

    [AdminMenu("SqlOnlineDesktop", "show")]
    public class AjaxController : AdminApiController
    {
        private readonly ISqlOnlineApplication _sqlOnlineApplication;

        public AjaxController(ISqlOnlineApplication sqlOnlineApplication)
        {
            _sqlOnlineApplication = sqlOnlineApplication;
        }


        [HttpPost]
        public async Task<IActionResult> CreateExecute([FromBody] Req<ExecuteSqlDto> request)
        {
            request.Data.AdminId = AdminDto.Id;
            var executeResult = await _sqlOnlineApplication.CreateCommand(request);
            if (executeResult.StartsWith("ERROR:")) {
                return Error(executeResult);
            }
            return Success(executeResult, request.PasswordString);
        }

        [HttpPost]
        public IActionResult StopExecute([FromBody] Req<ExecuteSqlDto> request)
        {
            request.Data.AdminId = AdminDto.Id;
            var executeResult = _sqlOnlineApplication.StopCommand(request);
            return Success(executeResult, request.PasswordString);
        }

        [HttpPost]
        public async Task<IActionResult> DoExecuteSelectSql([FromBody] Req<ExecuteSqlDto> request)
        {
            request.Data.AdminId = AdminDto.Id;
            var executeResult = await _sqlOnlineApplication.ExecuteSql_Select(request);
            return Success(executeResult, request.PasswordString);
        }

        [HttpPost]
        public async Task<IActionResult> DoExecuteSql([FromBody] Req<ExecuteSqlDto> request)
        {
            request.Data.AdminId = AdminDto.Id;
            ExecuteResult executeResult;
            if (request.Data.Authority==1) {
                executeResult = await _sqlOnlineApplication.ExecuteSql_InsertUpdate(request);
            } else if (request.Data.Authority == 2) {
                executeResult = await _sqlOnlineApplication.ExecuteSql_InsertUpdateDelete(request);
            } else if (request.Data.Authority == 3) {
                executeResult = await _sqlOnlineApplication.ExecuteSql_AllPermissions(request);
            } else {
                executeResult = await _sqlOnlineApplication.ExecuteSql_Select(request);
            }
            return Success(executeResult, request.PasswordString);
        }


    }
}
