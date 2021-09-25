#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/error/ErrorListener.js")]
        public IActionResult __editor_antlr4_error_ErrorListener_js()
        {
            if (SetResponseHeaders("F7CFE3120DEF60AA733CE947A2335F2C") == false) { return StatusCode(304); }
            const string s = "G3IEAMS3d3NauT8R7HaxYasMkwWaSj7ywDv/59JvEx1hq0SkjfZm2x3Iou0yOMEOz/1ua4I0PmEQ3VjqSw6JeARP3RGUEqn/gQixNNKC9v8SCjADuNiPSO3WKMuyFvVHwqu3s3nyl5sbeDCJND0CORkY+xqZNB8Nq+K2zGUqGb3wTxAZV32r+t2lGQUrMpv/LRJt5kmxmZRaEbIXfGUdZHoShRAlCxjzN2YU+Y2o6FGfjm2fCZRb23KaKc/Oy1au+KUBAgn6YKS9f9HffEMyY37G+OKb+An0wPnASP8EehLBdZBALSOoz4oI30W/W9vXD5n2rUxt/OkmixLhBgELBXbSMqvgwYpu32J6sLo52xovtG2HfelSz7O6sku3pAPFpGdLuM8UxiZ2q0CB5MGpt8t6SAA=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif