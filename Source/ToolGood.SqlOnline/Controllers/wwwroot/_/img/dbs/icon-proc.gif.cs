#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/dbs/icon-proc.gif")]
        public IActionResult __img_dbs_icon_proc_gif()
        {
            if (SetResponseHeaders("A7EA7EA5025216A05AD40B3970ED3C00") == false) { return StatusCode(304); }
            const string s = "iyaAR0lGODlhDQAMAJEAAP///wCAgP///wAAACH5BAEHAAIALAAAAAANAAwAAAIfjI8Iyw3mIlRnzRbsy1C/zUzd9WWhWZmWGq0tlsRGAQA7Aw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/gif");
        }
    }
}
#endif