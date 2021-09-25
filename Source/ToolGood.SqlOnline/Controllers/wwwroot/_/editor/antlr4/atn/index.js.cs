#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/index.js")]
        public IActionResult __editor_antlr4_atn_index_js()
        {
            if (SetResponseHeaders("E1337AFBA020800996177336C5E0489C") == false) { return StatusCode(304); }
            const string s = "Gz8BgBynV/okyC5x5rZSXzIpXWxWLRrKwAsDRTgVno+UDGKCglgeMAddHlgYJNBTuBvY7iYy1+mtorw0702i+58+S3m9plPCiQ/uQs7Y8CXjgKbTfrC5S1sxG5LhA/Fstt7Uu8GigDnD5coTqkI2EnMh/nU/yh7YChQpIAA=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif