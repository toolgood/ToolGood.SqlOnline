#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/snippets/pgsql.js")]
        public IActionResult __editor_ace_snippets_pgsql_js()
        {
            if (SetResponseHeaders("7BEF86E38E1E0B8F6A11D5EEDD50BE4B") == false) { return StatusCode(304); }
            const string s = "G40AAORtW98hCZFjuVY0IiSCEMNeBM04cCnd3bynEQVC05Ol5wdzMwRJU/2mD/1ojK3AB4ctd73BPuNsaDkCo7dFlCEiUDeA6PTUC9IHMRlTZGf06aQF";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif