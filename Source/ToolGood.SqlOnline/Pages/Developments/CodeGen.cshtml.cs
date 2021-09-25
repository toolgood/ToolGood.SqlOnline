/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon.Controllers;

namespace ToolGood.SqlOnline.Pages.Developments
{
    public class CodeGenModel : PageModelBaseCore
    {
        private readonly IDevelopmentApplication _sqlOnlineApplication;
        private readonly IAdminApplication _adminApplication;

        public CodeGenModel(IDevelopmentApplication sqlOnlineApplication, IAdminApplication adminApplication)
        {
            _sqlOnlineApplication = sqlOnlineApplication;
            _adminApplication = adminApplication;
        }
        public List<TopMenuDto> TopMenus { get; private set; }

        public string Logo { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            var use = await _sqlOnlineApplication.UseDevelopment();
            if (use == false) { return Redirect("/Developments/Prompt"); }

            var logoDto = await _adminApplication.GetSettingValueByCode("Logo");
            if (logoDto != null) {
                Logo = logoDto.Value;
            }
            TopMenus = new List<TopMenuDto>();
            TopMenus.Add(new TopMenuDto() {
                Name = "数据结构",
                Url = "/Developments",
            });
            TopMenus.Add(new TopMenuDto() {
                Activity = true,
                Name = "代码生成器",
                Url = "/Developments/CodeGen",
            });

            ViewData["Title"] = "开发环境";
            return Page();
        }



        public HtmlString GetMenuClass(TopMenuDto dto)
        {
            if (dto.Activity) {
                return new HtmlString("class=\"layui-this\"");
            }
            return null;
        }
    }
}
