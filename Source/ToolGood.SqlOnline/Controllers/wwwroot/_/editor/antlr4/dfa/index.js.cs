#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/dfa/index.js")]
        public IActionResult __editor_antlr4_dfa_index_js()
        {
            if (SetResponseHeaders("B750CE5051DD45A25EAE36F57BC2D32C") == false) { return StatusCode(304); }
            const string s = "G+MAIIzUY00ZHEndlvqS6dLFIIjJIBjSwYWD6TyIkaKTA6cvyAIrT6CSOIIgAAvqhmU2Kc5Hgsu49NHqNpog2QT5XpjHppte+3Q61gY4KaxMProMGmotW84T2D4xneUUGgQH";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif