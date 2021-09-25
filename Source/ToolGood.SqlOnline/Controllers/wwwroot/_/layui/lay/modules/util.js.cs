#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/lay/modules/util.js")]
        public IActionResult __layui_lay_modules_util_js()
        {
            if (SetResponseHeaders("3EF12624499C8A458AAA11528FFF440E") == false) { return StatusCode(304); }
            const string s = "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";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif