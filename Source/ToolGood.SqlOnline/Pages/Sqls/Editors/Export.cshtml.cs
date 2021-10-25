using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToolGood.SqlOnline.Pages.Sqls.Editors
{

    [AdminMenu("SqlOnlineDesktop", "show")]
    public class ExportModel : AdminPageModel
    {
        public void OnGet()
        {
        }
    }
}
