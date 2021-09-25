#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/root.png")]
        public IActionResult __img_db_root_png()
        {
            if (SetResponseHeaders("676BAFDE7036805E892B54B47E256727") == false) { return StatusCode(304); }
            const string s = "C4qAiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAA3ElEQVQ4jY2TURnDIAyEkVAJk1AJkzAJk1AJdYCESkBCJSBhEpDw76EJDRnwLS/lS8jdcUlDGASwAgk4gcfoXq9xATag0Mb+T7OyFiAKewJ2AclDNYY1Ay/JJSAZ8I8AbTPWxdQqgMm1arqo18Wn1IoqcjWAEvQg37eYGCWnHgBE01wNDqbxMI4X8WQx/tg4NKcAm2GtapxkO9YopNgnVFaj5nRn3ZH6bGtizygFfs9MXLlGAm7bcGOkNTgDq18kbIHfRcreYL+NjRrun6nPOgrubdMYs05AVM2U9QspP1oU3FYFgQAAAABJRU5ErkJgggM=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif