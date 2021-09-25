#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/database.png")]
        public IActionResult __img_db_database_png()
        {
            if (SetResponseHeaders("CF6AE665DD66A2C4DA14365789F12D3F") == false) { return StatusCode(304); }
            const string s = "i3OAiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAr0lEQVQ4ja2SUQ3DMAxEC6EQCqEQAmEQBiEQzCAQAmEQCiEQAiEQ3j6cqJHqRdlWS/fT9E6+Jy/LnQOswAMIwNHpBUTAA9snswcKcyNWQJ40t1n/CSjWBjvac2hEmTgr4ACkBm0ozCbXyQPJqmABzPVn620qYFTlEuCYA1mAp8VA0O6tp6AH1eQ5+YRRhcJ5eS0kopDTXQzMAPnCHC8MumOSbt1cN0v1WwB20/zrvAHxWIJ7/CWb9QAAAABJRU5ErkJgggM=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif