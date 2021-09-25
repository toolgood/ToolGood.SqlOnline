#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layer/layer.js")]
        public IActionResult __layer_layer_js()
        {
            if (SetResponseHeaders("556BCEF90F730B2E298EBC7F62F93202") == false) { return StatusCode(304); }
            const string s = "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";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif