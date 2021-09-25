#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/view.png")]
        public IActionResult __img_db_view_png()
        {
            if (SetResponseHeaders("FA136B0BC66E6FD5E3F65C3129898984") == false) { return StatusCode(304); }
            const string s = "i4CAiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAyUlEQVQ4jc2SsQ3DMAwENYJH0AgeQQO4SJ/GA6RwncobeIJAI2gEj+AiA3iEjHApQhvMQ4CNVCHwDZ98PkUFoAUGYBTEIGH5SZPFiMEhA1nqMvAArsDsiUWnmatZmicgAk+g2/kjAbVt3A1IQD4jMAFJ+A64A+WMQGM1SfhPn1l8AavDAoyuYRf5apapjUeQsHy2q0Xlfw+zVCrQnXO1BpiBi+23YQCKEyiWjw4tsJz9SNW9/0sgyRkPBba/EewB1wp6V5xskKJ/AxXYFiIwuB45AAAAAElFTkSuQmCCAw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif