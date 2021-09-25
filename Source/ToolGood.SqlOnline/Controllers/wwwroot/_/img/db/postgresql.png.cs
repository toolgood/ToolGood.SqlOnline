#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/postgresql.png")]
        public IActionResult __img_db_postgresql_png()
        {
            if (SetResponseHeaders("E43E7FB6A2F484DFA44059088BAB71C2") == false) { return StatusCode(304); }
            const string s = "C3CAiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAqElEQVQ4jc2TsRGDMAxFGYERPAIDUHgERsgIjJANGIUyZUZhBI3w0kiHIJaOy6VAd7+wbP//Lcld968AemAwtHLACJSIYAUE2BQjsLj1BrzYo5zVD0m9UBtCDz1bfbIAuPUESOB0SAmAqk9ZTvtvV4sjgWMWxRzYfmYOTKEPrFfXlbwGWdyXwObAD1ILk9ai2QWbOkmwaie+CS7aN3Uh+hMXa9Bs9U/xAbCVmisxDE2HAAAAAElFTkSuQmCCAw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif