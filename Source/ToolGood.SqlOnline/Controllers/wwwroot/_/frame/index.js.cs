#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/frame/index.js")]
        public IActionResult __frame_index_js()
        {
            if (SetResponseHeaders("74C958ECA367877ECFDA09D4A620F46A") == false) { return StatusCode(304); }
            const string s = "G5kCAIzUWQ2Lo23NqXQzSgroWNihLo0y+P7dsvELqVeBvD7fTT/WU2xsmsPbGN2PSrM0sA1azyOEdJuHEXT1pKktJTqCAG9JOBIUZDpcjBF8GD4BZVLhpX3f2d+20iKsa2Wv4H7Pu3cKfJu3/W6jiTO0nVqhIZehUUEbF4FjAIvncF6tArmq3fCugSg1T48kQ3xFrziCWdCNDFZrUOE+N50OBkgGMqLyUat1gbW0WLHstgwG0cFdqOkraGkIfL5JS06YhhJ+Mp7DJgSwBF+EI/0JyZWM96iKIrJt+v7GsbgMB4SbgMkqAbhMDP7t+XLQ/idznABeD/0pw3uUQvmtK9p7xo12CRkC";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif