#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/mysql.png")]
        public IActionResult __img_db_mysql_png()
        {
            if (SetResponseHeaders("8E96900BB2549C7A3910C3724A460B16") == false) { return StatusCode(304); }
            const string s = "C3aAiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAtElEQVQ4jaWSSxEDIRBEVwISkIAEJCBhJSAhDpCwUpCABCREwsshTVUSPqGSOU1B8+iumeP4twAH3IEKpF8ARoAAZIEykAC7C4lAVu8FiwL7XUgFnPrzI57bASQgqg/Nvvq6AwhAmdzlBt+JEQbnVlHsDmAoUsRr9djNIujeLF1ofFV280RzA0qb0uwHz7N60ftHfRzRi0AzF01DB5H98hpnALja0i2n8mUv0mjcI+EJmJXmAUvcMZ/UWt9BAAAAAElFTkSuQmCCAw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif