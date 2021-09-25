#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/lay/modules/code.js")]
        public IActionResult __layui_lay_modules_code_js()
        {
            if (SetResponseHeaders("4E62D306728FDA11454C95A27BD973CA") == false) { return StatusCode(304); }
            const string s = "G18EAMRKXVatswHpxJlUBmrasAFHj3RxTF+LjnRK+1hFaDVlWyRPcDcbyg7aWeOvfPDEoq8uh+mvjOHAbDQ3FDcnbyzkrFJY+0u8JFSnKOLoM1pLNki2q8bys+8sB7JxHnHD0XZTFnWm9uMAhLq0+9h0zZ3pfLdIKz4TvN9B3aDxdQRDX2kMTZYimJZBl6/QR1qrqOiN+j1RZGwdjXZM0INiwgxoav9kGgMkX3BQ4zE3pxUfVzrSWRcDT5wvs6n2+y2Hry66W4qXvN87M/9Ctz7tnfvzG8+fnvOvSl9L/el0+CNjsoOC5xZAwIZkzbLf74SZgkHAKE96WgHYnSHQQ+YUS1b+sXJd1vxXrzyyxE8Zk9LWf31vv/ffyx9JLJFYP4GFQHKBz678Xh/nrX7+YjkOkRnCjvGjSMrpNpu1rgMaZMnQB/o4Y9KWFKy8o1nlgWMmRJL72JOzt3B1RdaSBTFxERhMVz+LID/VBz/o4dH1U9rJVj4Sz6aWqMQ9rVfnABJTaTRiAOsAH3BuDd1IEYGNITFL6TH1nE/ypfwEOkvywb47d3eUObpNptn1PJZnUyi09yTWZ9BDYi6OfOT5buLmM8g0Cs3/eNAQtSX0npRHSf1QUA0X4c5OMQ==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif