#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/dbs/icon-table.gif")]
        public IActionResult __img_dbs_icon_table_gif()
        {
            if (SetResponseHeaders("C36B88C12CAD2F3E15244CDC21864B1F") == false) { return StatusCode(304); }
            const string s = "CzGAR0lGODlhDQAMAKIAAJmZzFuZzFtbmShbzCgoW////wAAAAAAACH5BAEHAAUALAAAAAANAAwAAAMoOLrM8TCGRpcoGOCiMbnZ1hUfYJ4o8ImsB3ItKaT0Gt5yPH5E7/+9BAA7Aw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/gif");
        }
    }
}
#endif