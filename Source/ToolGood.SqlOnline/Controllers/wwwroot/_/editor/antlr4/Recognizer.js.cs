#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/Recognizer.js")]
        public IActionResult __editor_antlr4_Recognizer_js()
        {
            if (SetResponseHeaders("89842981D3E69B0868F17506AADABC7D") == false) { return StatusCode(304); }
            const string s = "GzgHAGQr117fa+6AzWzIYRbcf6WIB0kt4Lu0prZgXR2gdP1L8ikg6o5cgUCNlJsDfMpmJy/AaCYDRC2sQYFz3Fe0uPvQtJIri10cLgig26sJnMIuNBVMQdykZHg0dyZcRlywVArA/R83EUIe3iCdDdYN9jcqz77pEjC9N6pz1oHUqLUfV9bfP6l5Y7YRlEkd3FT3cyXkLwWCFnBflQZGq4vpQYTr+PKa25jnKxeY4BmY90zEBgcZ/T1+K58CrBye7592mg+71ZI3YYIE8m5xh2CpD+c6ED1CJoNbDG1yeXN/76hO23QjG/J2V12Q8p35WBt3969IXujx+OnBz9E4TVg6pFLKkPBj6t5Asnl9DCrCMAqER4La9yJ5yfY1V8FoHcQHLEA/+Nc9XliFuc3U7rRt5FqQ+bww3x4cDCbKX+VoW+H818hJ8c9yL0QTkvHG0ab40Hyke0AMuDtyvKeM1MtXO/AaIAYTs7rQs5+C2Zp9vW6c96cjYKJPFmnLZltMeLWF2dwK+69rZsfmJZ1z3jkUytp5JU11d/OAl3Ed3r1OuliWPnp8XIeCucmAvC7chxQA15lkIyeA4wL5ijmI6lpoaQPcGVX1MRiJSc/0gpWMnF6EeaQ4TSALxgCpSQ0OhtkCkW7hx8QSLAzdXIQ5WIAYtkZYBCQVfbll/sKACikYx+nqRrtRu8JtYWMIosagJ4J5RSFcCbsuHLdtLmRZfrc1YeUxBRNv+02UGfc6bMlGgKVszrsSpeBEHpH/eV+7HiGCN10G6K+aMWff7tuqdKIWmGnXHeyWWEo4L7+o53TZFPMA";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif