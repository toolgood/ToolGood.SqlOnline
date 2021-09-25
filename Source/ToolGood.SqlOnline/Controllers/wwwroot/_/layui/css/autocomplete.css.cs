#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/css/autocomplete.css")]
        public IActionResult __layui_css_autocomplete_css()
        {
            if (SetResponseHeaders("CDED022E7E5236109E2E309FBD7B019E") == false) { return StatusCode(304); }
            const string s = "G5kDQMSidalufM3hwZYgDpyTB+AziUpr7f+0KpBXiN6WW0gLGAA54J/pgmeadfG91zAv4LXxng52/c7vlHVv+h1hkf4mItrGaeEQlZzpIhElBk/Ma18P4DBG2HqecmZKFPXx9V0P9ncRnFgO7hDUjuq8oiginmCJs3pfQ9S9ZLz2ICsUpJLIvqaXMokVYYjPpKm6yfWTNBYD0vXux++UZYaDsb3vEgg9G0DQFncReBigyYYEXI1AhMEUoP5D3nX1tNZrZouNmXAL0P7AIzukpLX3rDjEH927tl4gaFN63o+ZGloG1IWTaHL5LlNMMHSG+7DV0xoKkPWZHqcnmB2ezq5SS06R+J4fD1xRFAwe2udZ926V9A8=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif