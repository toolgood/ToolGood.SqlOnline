/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Configs;
using ToolGood.SqlOnline.Datas;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Admins.Admin
{
    [AdminMenu("Admin", "edit")]
    public class EditModel : AdminPageModel
    {
        private readonly IAdminApplication _adminApplication;

        public EditModel(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        public DbSysAdmin SysAdmin { get; private set; }
        public List<DbSysAdminGroup> Groups { get; private set; }
        public List<RelationshipDto> RelationshipDtos { get; private set; }



        public async Task<IActionResult> OnGetAsync(int id = 0)
        {
            if (id <= 0) { return Redirect(UrlSetting.NotFoundUrl); }
            SysAdmin = await _adminApplication.GetAdminById(id);
            if (SysAdmin == null) { return Redirect(UrlSetting.NotFoundUrl); }

            Groups = await _adminApplication.GetAdminGroupAll();
            RelationshipDtos = await _adminApplication.GetGroupsByAdminId(id);


            ViewData["Title"] = "编辑管理员";

            return Page();
        }


    }
}
