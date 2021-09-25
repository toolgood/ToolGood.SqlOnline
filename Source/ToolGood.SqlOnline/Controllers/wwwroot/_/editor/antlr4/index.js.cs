#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/index.js")]
        public IActionResult __editor_antlr4_index_js()
        {
            if (SetResponseHeaders("D8C759E8AF0F60686ECF290552C494A5") == false) { return StatusCode(304); }
            const string s = "G5wDAIzUYs2Zj2TppvKnUG18U8VQqBW/jjWeZ0WKlZtpjZZkSZbiGHRpEuHvjq+udPnzKuqmVCbI1C9NqkP2Ff3RrKbSzX/OEMDYUtm1WjiaEyHEVaX08diX/4guLzuk9KvtKkkK2dHdnhlgh6lpcv6okNVhdo8osgsToN2RVpaANXCyLOFQyVVySiBdVyBSnF25tNtKkkIZsriyWMqk6wpEiv8k48IHnvAKYHOXGwFsgrFvK1yU8qMYwAZOUpGHqCvQtHt36X7I8410IbyH31+CJY/R9iNv7xG/NWsrS3YvpOhFOWgbIoYLWY0qSsDrnPqAUBaVOi5XA1gD";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif