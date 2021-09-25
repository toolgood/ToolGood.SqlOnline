#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/tree/index.js")]
        public IActionResult __editor_antlr4_tree_index_js()
        {
            if (SetResponseHeaders("E4FA3CB2B801CAFBA32E947EE8BDB0B0") == false) { return StatusCode(304); }
            const string s = "G90AQIzTHbGZtyIF0c5pN7HQrh9WSbIrNyil/gm3b+z625xSZDne8yzOtbboDUs88tr2yCs0bMgYl3cxdQUX8sIs2mgKtWBKhE/7h3IX0gib61SjK/x2WlBBCz0M3y9RqYoOuzrOGA==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif