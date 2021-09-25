#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/zTree/css/img/line_conn.gif")]
        public IActionResult __zTree_css_img_line_conn_gif()
        {
            if (SetResponseHeaders("A2649B0B087AE0F894092A090D95D3E2") == false) { return StatusCode(304); }
            const string s = "CxaAR0lGODlhCQACAIAAAMzMzP///yH5BAEAAAEALAAAAAAJAAIAAAIEjI9pUAA7Aw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/gif");
        }
    }
}
#endif