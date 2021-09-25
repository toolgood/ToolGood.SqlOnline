#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/polyfills/codepointat.js")]
        public IActionResult __editor_antlr4_polyfills_codepointat_js()
        {
            if (SetResponseHeaders("C4DA8492BA2B62F2FD07C830DAAFF042") == false) { return StatusCode(304); }
            const string s = "Gy8CYGRzavwJcgJ8VuZWBmAPaLv9o8NRsjtwUy4KMCC/39J7KbAsPRcs4HNBQh/gVM9LeD2YQRseRJi5hsbuYFr4p3vvHC9uEiL4ciA2ek7geOy4sJYHeXnomeF+MUxwiqWdPOD3tKN9P85udegvrCclT6SX/Vp61FsntWyx/vaxCBvJOr2Kf2lGQiKJh+UN4JzGU0PB9kF1QX4EWKRWeYDLSEuV8soTGZHSaaC7ek4MFeRZ5FA6qwM2BhENwLriMB/4FuGjAToUDyi8PTOfBWlInAtCDyyqvH2nGq/QkGalEBIH53Q/qBVWUEf/BpasMY6DPOFcVhgnoZ9zfq3J9DkXSlw0fQsoOAkDD1AbaRiFAI2QthYZjIHyt6aymZ0HflLIT/loHgKTqqUbTbc26OsFWce+Hh3/LoXmWZOSFOj8gQJZF+kjAA==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif