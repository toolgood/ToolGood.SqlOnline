#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/mongodb.png")]
        public IActionResult __img_db_mongodb_png()
        {
            if (SetResponseHeaders("24F9FA6AEFD5270C0BEB032343CB97F5") == false) { return StatusCode(304); }
            const string s = "C12AiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAgklEQVQ4jbXSURGAIBBFUSMQgShEIAIRaGIUIxjBCEQwwvXDdVwdmVl2xvf/DrDLNP0ZoADJW85AE6SOloOUEYChmwCVOxewjgDLBwAQrcDeAWzP4BkN2IapBugG5g5QrECUQnttJJsAQQqwcX4oZLDBDCgkyFrTUFkjrqICfCdbcwDh5wefNnC5WwAAAABJRU5ErkJgggM=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif