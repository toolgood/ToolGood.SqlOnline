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
using ToolGood.SqlOnline.Configs;
using ToolGood.SqlOnline.Datas;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Admins.AdminGroup
{
    [AdminMenu("AdminGroup", "edit")]
    public class EditModel : AdminPageModel
    {
        private readonly IAdminApplication _adminApplication;

        public EditModel(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        public List<DbSysAdminMenuButton> Buttons { get; private set; }
        public List<DbSysAdminMenu> Menus { get; private set; }
        public DbSysAdminGroup AdminGroup { get; private set; }
        public List<RelationshipDto> RelationshipDtos { get; private set; }


        public async Task<IActionResult> OnGetAsync(int id = 0)
        {
            if (id < 0) { return Redirect(UrlSetting.NotFoundUrl); }
            if (id == 0) {
                AdminGroup = new DbSysAdminGroup();
                AdminGroup.OrderNum = 10000;
                RelationshipDtos = new List<RelationshipDto>();

                ViewData["Title"] = "新增角色";
            } else {
                AdminGroup = await _adminApplication.GetAdminGroupById(id);
                if (AdminGroup == null) { return Redirect(UrlSetting.NotFoundUrl); }
                RelationshipDtos = await _adminApplication.GetMenusByGroupId(id);

                ViewData["Title"] = "编辑角色";
            }

            Buttons = await _adminApplication.GetMenuButtonAll();
            var menus = await _adminApplication.GetMenuAll();
            Menus = ListOrderUtil.OrderByAscOnPid(menus, 0, q => q.ParentId, q => q.Id);
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
            var f = RelationshipDtos.Any(q => q.MenuId == menuId && q.ButtonCode == action);
            if (f) {
                return new HtmlString("checked");
            }
            return new HtmlString("");
        }
    }
}
