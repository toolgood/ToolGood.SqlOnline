/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Admins
{
    //[AdminMenu("AdminTopDesktop", "show")]
    public class IndexModel : AdminPageModel
    {
        private readonly IAdminApplication _adminApplication;

        public IndexModel(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        public List<TopMenuDto> TopMenus { get; private set; }
        public TreeAdminMenuDto TreeMenuDto { get; private set; }

        public string Logo { get; set; }

        public async Task<IActionResult> OnGetAsync(string code = null)
        {
            TopMenus = new List<TopMenuDto>();
            var topMenus = await _adminApplication.GetTopMenu(AdminDto.Id);
            foreach (var item in topMenus) {
                TopMenus.Add(new TopMenuDto(item, code));
            }
            var logoDto = await _adminApplication.GetSettingValueByCode("Logo");
            if (logoDto != null) {
                Logo = logoDto.Value;
            }
            TopMenuDto topmenu = TopMenus.FirstOrDefault(q => q.Activity);
            if (string.IsNullOrEmpty(code)) {
                var f = TopMenus.FirstOrDefault(q => q.Code == "SqlOnlineDesktop");
                if (f!=null) {
                    return Redirect("/sqls");
                }
                if (topmenu == null && TopMenus.Count > 0) {
                    topmenu = TopMenus[0];
                }
            }
            if (topmenu != null) {
                TreeMenuDto = new TreeAdminMenuDto();
                if (topmenu != null) {
                    topmenu.Activity = true;
                    TreeMenuDto = await _adminApplication.GetSidebarMenu(AdminDto.Id, topmenu.Id, topmenu.Ids);
                }
            }
            ViewData["Title"] = "基础配置";
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
