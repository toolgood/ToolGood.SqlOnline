#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/polyfills/fromcodepoint.js")]
        public IActionResult __editor_antlr4_polyfills_fromcodepoint_js()
        {
            if (SetResponseHeaders("FB89A8F5B01C222875E1788A2793849D") == false) { return StatusCode(304); }
            const string s = "G34CIGQ/t3fvMgG1yyeopCZbd3F4UAl2X3sMnJM3msEAKCqt4wF03KnogBeONptzYqdHSHnj5Al44Sx0bRWmG3+ymeyPyhkBIUDdxZ8r1/ZtT/vYamd/77Jp3ydt4eyhjLGpgUvwYZ9SMu4wm74ct+3F/4ySZOJsJRJ/MKPVkhSMSQhHR9J9hiP8z0a81s4CsKX5neZ95OEQR3pR4hRKYualpMdn7HDCJVke1qQe3P9nWCEXqNRqmG3dMIq6G0VF5TXTXJZ1Af8kGpP1RTHG5uPyGVBvpxucrleCQSku3Vw68SIUerZSY8e2TgyyGbv1B53Rs5lzjXt+6CAKZ7VwP9cMk6HgkuIoCqJTidIeQy7Yav4yxo4En5x4LphR5GcxTsSH7nNsRnHgu3jVww4ngEIsTY+oVkoKe+PEi4M0hKMjUZnUHk0HejMHEpAuIBe0Dq/mgk9ZLNGwuWvg/55ylhLXE4BsQGesu7geW1k9O0MOmIzkLAA=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif