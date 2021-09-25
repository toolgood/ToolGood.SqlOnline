#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/error/index.js")]
        public IActionResult __editor_antlr4_error_index_js()
        {
            if (SetResponseHeaders("ED0D24155F8875F08B036021204869BE") == false) { return StatusCode(304); }
            const string s = "G1UCYIzUYs2Zj2Rtbtp/clJ2cVAaK5raHzviQBLeH4oUy1tIRWueS4SiIZXngk0yShaSRreJVyHUW3MYvsMrV5cxypOgLwrlcTVITP8/Yo6MmSaf82YC5nV432BXdvwwLsBJQquDSgdXOfjJYLGla9X7y/Cwq2NwaKzPRn2/uZw9SDpfLTBnMEcSDfbSlOPKq9kUr/aNoMHDpdZMSB8A";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif