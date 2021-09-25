#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/CharStreams.js")]
        public IActionResult __editor_antlr4_CharStreams_js()
        {
            if (SetResponseHeaders("D1876F9645857585C0796DCBA0223A71") == false) { return StatusCode(304); }
            const string s = "GzQCAORyTmULFK7u33ck5IQCKtAn2dj/Ok2jGOa/L6U0yiyl1PFiscVgDg/s+Myt4cegjw9z7W5z5GdEhGRuMqIhbRFpkzeEPQkhMt0a2eC6AQoPxCgskiwbx6TAh3crNkmHzQqsu9qIVUFUEn45I27dys60cA/0/+PlGYyFk2HJ/lL8ve2nY93JRpL0UxleMRELu1mOM7jqRfbQcCYNara1e2ztHcLZABUN+9/OXBig75Pbw43NoCJMlcu3v14vdFwSV3+sh9AwIyiZS6ZUywFRYVdPmsFboq3ve2hC7PAsK8gSs1H/m4EvnCT2B/rcv8cTGLQJHgFJjI3egb3lGSd/+HRHvrsf6P15IRI=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif