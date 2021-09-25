#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/snippets/mysql.js")]
        public IActionResult __editor_ace_snippets_mysql_js()
        {
            if (SetResponseHeaders("29C4D9C1A0D3D5BEE6E81512CCA3CFBB") == false) { return StatusCode(304); }
            const string s = "G40AAORtW98hCZFjuVY0IhxBSWEnImjGgUvp7uY9jSgQmp4sPT+YmyFImuo3telHY2wGPtitr6XHeV7fA0qMwOhtEWWICNQNIDo99YL0QQzGJNkZfTppAQ==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif