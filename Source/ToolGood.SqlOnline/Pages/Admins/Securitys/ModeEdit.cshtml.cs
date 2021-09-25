/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using ToolGood.Common.Utils;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Datas;

namespace ToolGood.SqlOnline.Pages.Admins.Securitys
{
    [AdminMenu("AdminMenu", "edit")]
    public class ModeEditModel : AdminPageModel
    {
        private readonly IAdminApplication _adminApplication;

        public ModeEditModel(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }
        public List<DbSysAdminMenu> SysAdminMenus { get; private set; }
        public List<DbSysAdminMenuButton> Buttons { get; private set; }
        public List<DbSysAdminMenuCheck> AdminMenuChecks { get; private set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var menus = await _adminApplication.GetMenuAll();
            SysAdminMenus = ListOrderUtil.OrderByAscOnPid(menus, q => q.ParentId, q => q.Id);
            AdminMenuChecks = await _adminApplication.GetMenuChecksAll();
            Buttons = await _adminApplication.GetMenuButtonAll();

            ViewData["Title"] = "编辑管理员模式";
            ViewData["Comment"] = "";
            return Page();
        }

        public HtmlString GetButtonName(string action)
        {
            var str = Buttons.Where(q => q.ButtonCode == action).FirstOrDefault().SelectNull(q => q.ButtonName).ToSafeString();
            return new HtmlString(str);
        }

        public HtmlString GetButtonId(string action)
        {
            var str = Buttons.Where(q => q.ButtonCode == action).FirstOrDefault().SelectNull(q => q.Id).ToSafeString();
            return new HtmlString(str);
        }

        public HtmlString IsChecked(int menuId, string action)
        {
            var f = AdminMenuChecks.Any(q => q.MenuId == menuId && q.ButtonCode == action);
            if (f) {
                return new HtmlString("checked");
            }
            return new HtmlString("");
        }

    }
}
