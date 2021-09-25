#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/ext-statusbar.js")]
        public IActionResult __editor_ace_ext_statusbar_js()
        {
            if (SetResponseHeaders("07FA939EFEFE2A963FE2DF1FCE26D765") == false) { return StatusCode(304); }
            const string s = "GzgEAMQ0s1Xta/4Bj87vEy0nQy4a2ryXMins1aX6+IQBwK2X0ivWAlgqWCwfwJ+AxSJ2BXgxV362QlEZJXFBOkqr7/e032QeXqF3oLEMhRQxkIkmoWN9Hrs8LZepC7G4ggdd5DEJ2mjB6tnSg8CsrFDzrmZki1EPc632NmuyUn7KS+KcUHafoA4XNWYFfLlCTzRF9jewvFheMcbefST0AwkdPYtyKI5naM+6A0ewUe48/1oa6FqtA0XmRDgv/7u48ZbR8m6eVoU09ZQeDK7AMcbHWltmgeRk9HdoR82NVu3HxaH6Ebi5Goz8y2O2WPt8h8CrNr8G6XTlX3d4uODFpdP1/x//YKQ7vHygg8lBWhuqn39WX7Gp3qfKLC1Iy+UyxlnUULJ+eDgIbm+uwMDu6K3BYaC9jwbOQGZQ2KPxt3EXoh4VJ29nZEbh78juP2aDQJDIZNVc2uvytZ9M+6m9ywQn/QUcJtZOWLoRig0YB+nHYy/cD8J9izoaINm4Yyqe7pB0byV4wNBfLbYuq+gmIVAhnfDle/3wBBgjjbQmalBuURvjpPNlePU6UoAEXMXma7lDfoi/Nv+6+riApCV3GFFlHB72VHgfpDJ8in/NERuqGWLkygc=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif