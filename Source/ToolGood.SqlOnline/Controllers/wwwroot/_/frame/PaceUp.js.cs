#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/frame/PaceUp.js")]
        public IActionResult __frame_PaceUp_js()
        {
            if (SetResponseHeaders("EC38E800B5EEA5D35C98293D6301AFB8") == false) { return StatusCode(304); }
            const string s = "G6sBIIzDOBb80RRhsQIaW5fy8eHqUL0OEWpy5L+g2x404tYCCSTLnceaWv/bbW1BK/olicR9FAcYWBJJGpSeA5GYTFXfOl0p3PAtW25gPusKFspTnLYQY3PYLr73Ws7iLWZh2FoP0G+b3zqsV3c2j06Oc9aB+og1GQ/FqABnyIo3KKktRBzmhfsE4fTodg1xeOikqOHQaAWDah6Hal9RahOW+4mZoaJ4mrq6iivhmGf2DujwERru3ViABUSuV2fkkH7uvw8=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif