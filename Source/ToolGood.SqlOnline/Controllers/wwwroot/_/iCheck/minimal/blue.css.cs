#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/iCheck/minimal/blue.css")]
        public IActionResult __iCheck_minimal_blue_css()
        {
            if (SetResponseHeaders("187B4E2B813ADECCB5F540048A05E214") == false) { return StatusCode(304); }
            const string s = "G0AEAER1U9lLeOi+LeP5M4NyNxjUkSRk/3fu+TGt0KOKTmBe/1bFA5vPd2t/iCGBdYn5pvtCHQJTGsHnfPXAxFqrQkNkm7G+MQsnLblw2R7LPvOUf5U+eQ0rPXfpK04uGqeplOOx/MT+CrMlFFrdtnjFB2Msiox+pIlQgqKVYgdoHEx1G4DQrWqteI9oLSRy0bMa/gaA7sJEbjkJS1Xirl5UG6yvC6nSBr8dIaulD074cdjIXgtl1cJmjGr1slxSCQQNxEJaSRzRSFae86Hc52gi4nd9zpV32gK+6al5VTVdp+srbY4WdxZkqb+lqg9khxE=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif