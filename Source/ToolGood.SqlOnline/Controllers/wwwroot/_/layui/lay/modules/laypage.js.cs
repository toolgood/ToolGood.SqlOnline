#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/lay/modules/laypage.js")]
        public IActionResult __layui_lay_modules_laypage_js()
        {
            if (SetResponseHeaders("02FFE550FB40F7EF1B133E4237D23843") == false) { return StatusCode(304); }
            const string s = "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";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif