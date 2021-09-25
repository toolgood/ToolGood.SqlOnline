#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/FileStream.js")]
        public IActionResult __editor_antlr4_FileStream_js()
        {
            if (SetResponseHeaders("556CF4F156F13EFB7BDCAC7FC0082FDB") == false) { return StatusCode(304); }
            const string s = "GzIBIMTaZuofROE565gcK2EhDgTLDfQNC7N2Gub5XalPyetHKTaRZX3DZtzo3EIhKijw4yzMZNAvZfmAo29OuKGWpv/96cly29EPy1cC0Xgesu+V7AMGBMRdoab4306DGI1sqC1JeO7IWUj1wSsJ2T2kVZympAbfoOdeVscnx+Vr9mXlLOTWBGwiQcQPyTChvIplwjnb76Ra4TLQQLRCAQ==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif