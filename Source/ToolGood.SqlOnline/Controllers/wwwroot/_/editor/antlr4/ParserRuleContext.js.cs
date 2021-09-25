#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/ParserRuleContext.js")]
        public IActionResult __editor_antlr4_ParserRuleContext_js()
        {
            if (SetResponseHeaders("A7B240A48D2B9ECC9A6168EF0083B18D") == false) { return StatusCode(304); }
            const string s = "G48JAIzUWK04y8yyadUCnWb01FpWsHKd0oQK7P3Isjnh5If7qS2Q0hWqtix18+4nJR4B5v6A2AE5M6tmzR7iZgesyP+KtkrwKyRw0AWK6xW7318JjU3RysrBdi8GB0myJAHhSI1aNFBZdEpzYovq9dXR3M4UNYDn/hs03VOwsfv0DFrxEhtxA1IOTP6LrWIqkPZ0V1YRVsafJOIolatjHEmFPSSroLiFd8RLjbL3PpCz49+msHunUQawici4kzgDx/T+2l9ebRBwpbbbtjXJSLfi17JBEtIiKGWXViHTPzhji2G3UdJc6Qdeyq4YF9tT8Jf22nANoAvDSP88n7EF10kI7YZ91jjEiJ2ggK1LsnRZO2JTI8h3w9NTKudKf8gpXpV/0za1OIt7917Mq5tVAWSwV0FMiU0b6J8vHqd/psBqhfA1a+GS3eNoMWuYLhZ+3CcHzWTISymTzKg+CzEpTG1klyL0j6v0OF02rluu5Jdzm6j9iJQUeqnTIAxmbCiWh5sM2iYkSZrfSkEcvD6Zow7o/VrHUVvduF4G/8scu15jF5spP7UryyOGLnTrC8df9Gj082WoaSI1lUc5dyUJ/2EfQ1llT6a1WLfIpPQsvGGr5Jr5mQ3ZVbsu6JJyGKpa1/azCwpt/tvE8wKyXOvYnAO2CDP+A3WDu6cnL9/1JtxagyoIDhDhx85brfqQc7BtjSrSfVEl3dl8ILSWoMoW3MoSgSygCSphwjnT7F2chNjlrYuODic6TcWkln8uBYMKM2GL1SLg4VoQ93MbwAO3BzZQhT7CVW1eu9+89bXuNIsvnHrlZBfUvALZgVJ2oMayvuKy45uObFN6vJsU9FHk+LgaMUD/YuX7QYdbuxvEO91+CMUrLu+HFUH421xdkgA=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif