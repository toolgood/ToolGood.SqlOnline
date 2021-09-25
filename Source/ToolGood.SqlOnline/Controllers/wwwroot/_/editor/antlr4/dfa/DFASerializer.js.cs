#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/dfa/DFASerializer.js")]
        public IActionResult __editor_antlr4_dfa_DFASerializer_js()
        {
            if (SetResponseHeaders("94F3E0CC7A50ACC3694982676EBACFDB") == false) { return StatusCode(304); }
            const string s = "G5QEAByF6faYNdNMfZe92qrZphnTYsVv3a/Jm9ImNsYvNUGm+KScD3q6PdF5jM2PcBg6Zr4KlYmN7HLRbRFFFVS20frfn/9RfxHP7XTAtpfkCIWCkZNWozh9sg51K0a9/LXQUHDcg/jjPwiMmGnR/Zir+n/u/oNzVvkaM9AWYKoo2zRIYTc2Q8fZOEl89juq1MipkNKYtQY9s3joRMaRykLH/5Vr34IJAFXBzvTNy+GfGNeM1KTLbVYflhNqMSLvCqnztiTnBLMOR3PE0loEQKkqSaXLOiu5qhCPkprr8u9vaqWRQfjsbOQ4kOyBkBJMFZU+BOA/BWK8722/dbvcz8XVpE3ioSMS2jWYABIZujsc/o7oJupEdTHuQ5CkMb4wwAzXspknoro06jDZf45buwotsRux4GPqu+G+B0g5RR214O9vTcGTL+muzG0yXU4oUJMMU3FpoO/9HX3hdbCfjJq3uqF1TZIudwlYh2chw8jOS0i73wBGGCBaWGMx4OKN7n5Hn6ALEDFWcmd25Gk3fvWG6a3beyOCHCJYXi72BUwo0q69bf1nN1MjlQf5Z3C0OmAMUfXcvrxR38SzR/c8nEN0uBEjGKxp3f8n3z1cP5Wm2zf9KU1sc+GxfJWGoHI=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif