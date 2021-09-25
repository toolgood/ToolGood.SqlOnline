#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/oracle.png")]
        public IActionResult __img_db_oracle_png()
        {
            if (SetResponseHeaders("69FD4E01E47190CAA506C94B83585758") == false) { return StatusCode(304); }
            const string s = "i2WAiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAk0lEQVQ4je2RUREDIQxETwISkIAEJCABCZUQB0hAAhJOwkmphNefvWl6A1cBbWYysMlkk0227W8fBkTAgAH0iRsQVsUBaEC8aRCAsUpkIAtnoJxkDicgzQg68ACqphj674o34QgcM4LDdWkiHK7QlE/AU7FyJakiCE5KOV83QZVPpezLLb931b9dwpyEq9vdlX7VXpy/BX0ubUobAAAAAElFTkSuQmCCAw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif