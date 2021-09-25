#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/ATNType.js")]
        public IActionResult __editor_antlr4_atn_ATNType_js()
        {
            if (SetResponseHeaders("6E9D401385F436AB401D80E0FB048DA2") == false) { return StatusCode(304); }
            const string s = "ixmAZnVuY3Rpb24gbigpe31uLkxFWEVSPTA7bi5QQVJTRVI9MTtleHBvcnRzLkFUTlR5cGU9bgM=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif