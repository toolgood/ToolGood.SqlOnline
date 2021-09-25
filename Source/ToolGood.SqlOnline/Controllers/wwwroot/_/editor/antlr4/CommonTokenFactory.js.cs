#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/CommonTokenFactory.js")]
        public IActionResult __editor_antlr4_CommonTokenFactory_js()
        {
            if (SetResponseHeaders("CA4C4B7B943445F2D4B08949EFEF9BD1") == false) { return StatusCode(304); }
            const string s = "G+kBYIzDOCb8EIt40jJyU2XbnTCSYOH+MAbXq2SsZ+1icZm6CfSgMkfT2jo2d48JdgElnkoQbg0RrVd9omZzu1pPA5k0z8XnE/bnLklFwRLnygZEP5bOQSow5yheRxAAuU1493w6iiDMbASmTjzqmZtI5dGH0GkVUHmg4apWjlQiny+ynVPR0DQ9mMhkBr+C8XLjjlUF/0v7opPrnUnixBrDC6CyO9UoRZU92ThT+Ho0MoV0jcHp70Cc/AwzNcbriN6ncfh8+PP+jYrPRyzwajuNx02KPp6k7LL1HmU57cU2gh7FneOqe+1Drr+CKAvgktHQYhA=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif