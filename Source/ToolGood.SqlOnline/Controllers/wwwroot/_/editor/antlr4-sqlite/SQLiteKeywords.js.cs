#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4-sqlite/SQLiteKeywords.js")]
        public IActionResult __editor_antlr4_sqlite_SQLiteKeywords_js()
        {
            if (SetResponseHeaders("D41D8CD98F00B204E9800998ECF8427E") == false) { return StatusCode(304); }
            const string s = "";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif