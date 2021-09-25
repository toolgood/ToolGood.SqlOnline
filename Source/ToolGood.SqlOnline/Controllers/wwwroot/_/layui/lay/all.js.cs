#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/lay/all.js")]
        public IActionResult __layui_lay_all_js()
        {
            if (SetResponseHeaders("488905DE9A518C28AD7BAE881F808811") == false) { return StatusCode(304); }
            const string s = "G3gAIMRvJz/tl+GuxE6mzXJiUG/yK4lO7ogH8KJ2sEDDJDt70BkrgWPhbUHv7g7FHqGyS2tCI9gVbc50+NU+gjphLOCbfW1z/cZbIvkSCxNORwGyrk4G";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif