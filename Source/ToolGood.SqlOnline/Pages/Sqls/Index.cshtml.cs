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

namespace ToolGood.SqlOnline.Pages.Sqls
{
    [AdminMenu("SqlOnlineDesktop", "show")]
    public class IndexModel : AdminPageModel
    {
        private readonly IAdminApplication _adminApplication;

        public IndexModel(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        public List<TopMenuDto> TopMenus { get; private set; }

        public string Logo { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            const string code = "SqlOnlineDesktop";
            TopMenus = new List<TopMenuDto>();
            var topMenus = await _adminApplication.GetTopMenu(AdminDto.Id);
            foreach (var item in topMenus) {
                TopMenus.Add(new TopMenuDto(item, code));
            }
            var logoDto = await _adminApplication.GetSettingValueByCode("Logo");
            if (logoDto != null) {
                Logo = logoDto.Value;
            }

            ViewData["Title"] = "SQL online";
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
