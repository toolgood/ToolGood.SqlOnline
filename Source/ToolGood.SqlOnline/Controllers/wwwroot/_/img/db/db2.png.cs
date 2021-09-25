#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/db2.png")]
        public IActionResult __img_db_db2_png()
        {
            if (SetResponseHeaders("E07FC12C78F3A9C534D2B89115D327F6") == false) { return StatusCode(304); }
            const string s = "i3uAiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAv0lEQVQ4jc2SUQ3DMAxEC6EQAiEQCiEQAiEQyiAQCiEQCqEQCuXt5zLdskmdVE1apJPs2L6cr52mvzjAAiThJVa95/OnfAIyUIVkcQXKUJstj64iA8HiKPShYvelq3OCahJdQRDqgDeCzRSseq2/GlRPqhUgjSaWLm0Y7oYVrbJYf3CCbAPP2FzPimfrD/f/gcGHBOxAM5m79o+6r1cEfdcmw1ZgM/nnNwRhIDj8S1wRHGpcNNyEDJxAu+HSD84Duh660hQ3C9gAAAAASUVORK5CYIID";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif