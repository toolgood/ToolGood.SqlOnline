#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/procedure.png")]
        public IActionResult __img_db_procedure_png()
        {
            if (SetResponseHeaders("85274FB212585A55138FA95FC356AF14") == false) { return StatusCode(304); }
            const string s = "i1WAiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAc0lEQVQ4je3TQQ3AIAyFYSQgZZImAQlzMAmVgASkTAIS/h0GSQMcCttxL+kBkn5AE5z7IoAAmbmIBlbjW2AD0hsglbUV6QCAWPauFSADO89TLEPtAN18AocqExDUyaGAtUY3Gs5gJj+gAFlojqNf6a1Ve26wlj++b1WbogAAAABJRU5ErkJgggM=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif