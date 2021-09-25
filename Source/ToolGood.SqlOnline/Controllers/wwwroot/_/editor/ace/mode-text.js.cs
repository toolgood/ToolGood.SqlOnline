#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/mode-text.js")]
        public IActionResult __editor_ace_mode_text_js()
        {
            if (SetResponseHeaders("C992A7984904047FC1B96A25AF0526EC") == false) { return StatusCode(304); }
            const string s = "G4gAAKwOeG7FD+fxhCohahmjtQlK19gUCStLt62eSmGpvb42cXnp4ZQDl5Lg2712OFF6svQsmLOdIGmq3+l/NMbOlESVd5YcLBAU95EcBDnwAvhGzkh7umYB";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif