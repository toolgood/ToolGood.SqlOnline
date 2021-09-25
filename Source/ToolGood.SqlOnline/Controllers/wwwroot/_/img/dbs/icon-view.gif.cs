#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/dbs/icon-view.gif")]
        public IActionResult __img_dbs_icon_view_gif()
        {
            if (SetResponseHeaders("7F5C7D704E8AA5D29929ED31556532A5") == false) { return StatusCode(304); }
            const string s = "izWAR0lGODlhDQAMAKIAAKbe5cfR5YfG6aKxzm+Mxf8AAP///wAAACH5BAEHAAYALAAAAAANAAwAAAMxKLrM8DCCRtcwpui8DblYgWXjF5zFGaTnN65ZUIIGnM4qmgcuJosyTK/2mhGOyOQxAQA7Aw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/gif");
        }
    }
}
#endif