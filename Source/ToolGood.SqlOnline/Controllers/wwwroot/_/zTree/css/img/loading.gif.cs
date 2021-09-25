#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/zTree/css/img/loading.gif")]
        public IActionResult __zTree_css_img_loading_gif()
        {
            if (SetResponseHeaders("4F3236673DB18FBB34F4F6A942C2CE12") == false) { return StatusCode(304); }
            const string s = "G3wBsI6ULg/UGfz+qPlvIaBuSFYogCOluwCx5WlJ2iVxYWyBYcaxZT+0MQZcstG6/jUHJ3tDoyhOcKKKHsObTxmdpyl1h5O7LwuHrxeP3////wcg9Z/F3c7Xx8bK005bXZOGAiD1RUvHDHqoAgAnOAEazcQhu15NYe2cUEmshBenctpW0MWJvIapnvREbE3f+PW/N1/ns36pL9zYNx2mTcSSgYkxgA7iSEzm7JDP8XSMKmIlN2FMrfQAA33X7dMNqwUZI+m5zulUZhDQgYE4H+J1a5TWk+moHusRSxKhRxP9fZ3TFc8rCNUwPOds0IP5c7TrzWIlJFI9B5yAORipdX7pPlAqrszqXM0B+mSk4qUqF7UaXW3HETsCEw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/gif");
        }
    }
}
#endif