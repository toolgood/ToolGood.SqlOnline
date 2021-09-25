#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/mode-mysql.js")]
        public IActionResult __editor_ace_mode_mysql_js()
        {
            if (SetResponseHeaders("CE61B2FA94A4C1E4F9F969592469137B") == false) { return StatusCode(304); }
            const string s = "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";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif