#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/css/treetable.css")]
        public IActionResult __layui_css_treetable_css()
        {
            if (SetResponseHeaders("6024A556FEB6D5B91EA11D93CFC4BBF3") == false) { return StatusCode(304); }
            const string s = "G+kAYBwHbuzE6/EpJbe3bd3GUb6d2zhSEoJoSI2IUgTTvUZLv7HxhxzyDuOIY3zfMI0N3IbPF7HIwkOFO0UaxKAXLQKfzv1RP7FpD38RIuzbgsb3aPHCTmWfJjDoMPKWOJIDnsJYMqGpZhqvkT4=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif