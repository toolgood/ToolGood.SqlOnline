#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/css/modules/layer/default/layer.css")]
        public IActionResult __layui_css_modules_layer_default_layer_css()
        {
            if (SetResponseHeaders("020BC66B2BE098E2D22B291139B5F08E") == false) { return StatusCode(304); }
            const string s = "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";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif