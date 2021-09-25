/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;
using ToolGood.WebCommon.Utils;

namespace ToolGood.SqlOnline.Pages.Admins.CodeGens
{
    [Route("/admins/CodeGens/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {
        private readonly IAdminDatabaseConnApplication _databaseConnApplication;

        public AjaxController(IAdminDatabaseConnApplication databaseConnApplication)
        {
            _databaseConnApplication = databaseConnApplication;
        }


        [IgnoreAntiforgeryToken]
        [AdminMenu("TableTpl", "show")]
        [HttpPost]
        public async Task<IActionResult> GetTableTplList([FromBody] Req<SqlCodeGenSearchDto> request)
        {
            request.Data.AdminId = AdminDto.Id;
            request.Data.TplType = 1;
            try {
                var languageTypeDict = ToolGood.Common.Utils.EnumUtil.GetDescriptions(typeof(ToolGood.SqlOnline.Datas.Enums.EnumTplLanguageType));
                var languageTypeDict2 = new Dictionary<string, string>();
                foreach (var item in languageTypeDict) {
                    languageTypeDict2[((ToolGood.SqlOnline.Datas.Enums.EnumTplLanguageType)item.Key).ToString()] = item.Value;
                }

                var Page = await _databaseConnApplication.GetSqlCodeGenList(request);
                foreach (var item in Page.Items) {
                    item.Language = languageTypeDict2[item.Language];
                }
                return LayuiSuccess(Page, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }

        [AdminMenu("TableTpl", "edit")]
        [HttpPost]
        public async Task<IActionResult> EditTableTpl([FromBody] Req<SqlCodeGenDto> request)
        {
            request.Data.TplType = 1;
            bool b;
            if (request.Data.Id > 0) {
                b = await _databaseConnApplication.EditCodeGen(request);
            } else {
                b = await _databaseConnApplication.AddCodeGen(request);
            }
            if (b == false) return Error(request.Message);
            return Success();
        }

        [AdminMenu("TableTpl", "delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteTableTpl([FromBody] Req<AdminIdDto> request)
        {
            var b = await _databaseConnApplication.DeleteDatabaseConn(request);
            if (b == false) return Error(request.Message);
            return Success();
        }


        [IgnoreAntiforgeryToken]
        [AdminMenu("ProcedureTpl", "show")]
        [HttpPost]
        public async Task<IActionResult> GetProcedureTplList([FromBody] Req<SqlCodeGenSearchDto> request)
        {
            request.Data.AdminId = AdminDto.Id;
            request.Data.TplType = 2;
            try {
                var languageTypeDict = ToolGood.Common.Utils.EnumUtil.GetDescriptions(typeof(ToolGood.SqlOnline.Datas.Enums.EnumTplLanguageType));
                var languageTypeDict2 = new Dictionary<string, string>();
                foreach (var item in languageTypeDict) {
                    languageTypeDict2[((ToolGood.SqlOnline.Datas.Enums.EnumTplLanguageType)item.Key).ToString()] = item.Value;
                }

                var Page = await _databaseConnApplication.GetSqlCodeGenList(request);
                foreach (var item in Page.Items) {
                    item.Language = languageTypeDict2[item.Language];
                }
                return LayuiSuccess(Page, request.PasswordString);
            } catch (Exception ex) {
                LogUtil.Error(ex);
            }
            return LayuiError("系统出了个小差！");
        }


        [AdminMenu("ProcedureTpl", "edit")]
        [HttpPost]
        public async Task<IActionResult> EditProcedureTpl([FromBody] Req<SqlCodeGenDto> request)
        {
            request.Data.TplType = 2;
            bool b;
            if (request.Data.Id > 0) {
                b = await _databaseConnApplication.EditCodeGen(request);
            } else {
                b = await _databaseConnApplication.AddCodeGen(request);
            }
            if (b == false) return Error(request.Message);
            return Success();
        }

        [AdminMenu("ProcedureTpl", "delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteProcedureTpl([FromBody] Req<AdminIdDto> request)
        {
            var b = await _databaseConnApplication.DeleteDatabaseConn(request);
            if (b == false) return Error(request.Message);
            return Success();
        }


    }
}
