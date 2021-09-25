#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/ext-error_marker.js")]
        public IActionResult __editor_ace_ext_error_marker_js()
        {
            if (SetResponseHeaders("4D964ECF7E76C6AF84614254FAACE825") == false) { return StatusCode(304); }
            const string s = "G48AAETdlvrC+EF8SE9JREReH8QgiDo5cPklaMt/CoOArydLsmDuhiBpot+eKz4aYxr0LO4xnhrEHxu9xDj17RHkkDsqN0gn5x4CvEgxusI29etRCw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif