#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/function.png")]
        public IActionResult __img_db_function_png()
        {
            if (SetResponseHeaders("4CE6818020D7A3AEEAED673DDE9F94E8") == false) { return StatusCode(304); }
            const string s = "C4SAiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAA0ElEQVQ4jZWSWxGEMAxFkbASkICESkECEuoACUiohEqohEqohMMHt0ym27JsZvpBHic3CdPUGOCABBQgAHObMzQVFyACHsj6fgdRx9D4MuDfAhKwd6DHU9EiuUFyi4nN8uUWbAFRnQ/zVsU24ytdJVy2vhhvB9IIcCjBjzauOGakrQbqzePT2aoCNfGC3ZCPSeyeDVibvN0u3CaG4ca/gaVuuv55dU43GGGrilUTJs2cJD2NLmLgmJrl3zNmSXf3PuQMCjy9S3KH7AT5ZbG3mxMiitclbAYTQgAAAABJRU5ErkJgggM=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif