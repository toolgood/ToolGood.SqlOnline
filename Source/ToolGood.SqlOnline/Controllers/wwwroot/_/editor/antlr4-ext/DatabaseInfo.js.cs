#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4-ext/DatabaseInfo.js")]
        public IActionResult __editor_antlr4_ext_DatabaseInfo_js()
        {
            if (SetResponseHeaders("121D0255992B2D11A44AD270F95A170F") == false) { return StatusCode(304); }
            const string s = "G00EAGTcXKpXzYx10tOTC3Az+Od/bqWsLwYh5i9vu4hJpnHIKo3TiR3VuUQ9VOFRUqBjk2vZYQSwwor6moz+nIgSyIoJ9bPC9z8czJQHQTT118orKYSsKzipiVYvJ/ZhU21jtCeIdFTl2ptx/08wevMuTdpYD5y4n4GkGPR1SauuSWDdskvokOb/2GAQGdjoQE0WBTMdZmQpq7fbVput3ok/6twmViVJ6SBgHeDI6NiNJWkz4R5WDfHBRgR5L0GGOt1VfTyNFKAzie0qM7pLZ37K0lhp6K2J1jfwwtpxA7dvSx9ZKPhGv+K4TkR270q+K0YaMio6mc9uAs4kzXz+Liw8Ctqjl6OaIbhEV/5jdjT/Hw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif