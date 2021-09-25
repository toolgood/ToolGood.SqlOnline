#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/ATNDeserializationOptions.js")]
        public IActionResult __editor_antlr4_atn_ATNDeserializationOptions_js()
        {
            if (SetResponseHeaders("6EC7668F60E4E1643D546B61DDCEBF2F") == false) { return StatusCode(304); }
            const string s = "Gw8BQESJbaWKaoUbnvFQ3mxvfu5ID5T/3fywNH8Wn7RFGAca1Nwqicij/thp0uRKgw9oI4Ax/DTNIHjf700HKDx/brh/QwxLDNGYjzUI9F6pGERUOJWdrr/vF8V7MulX0nldDOFGN21Y/vT8yz1/BRdPgbGQDUcQU+herNXiurhsJsREczIC";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif