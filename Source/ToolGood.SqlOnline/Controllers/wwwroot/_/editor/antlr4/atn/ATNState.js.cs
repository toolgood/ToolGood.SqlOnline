#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/ATNState.js")]
        public IActionResult __editor_antlr4_atn_ATNState_js()
        {
            if (SetResponseHeaders("58DE41E02BFD3730308EACE59F462A6D") == false) { return StatusCode(304); }
            const string s = "G8UNAJwFdsvHV59lNOo5WcrCqIjk63r1dE06x/VLr3vfMinfCGeYChu6EaZDZzEkFHbTLY2OwLW16mAMzcdYK1r14d2cJoDfRYH8pwfMF8Qt+a2OvQF21OWkcwLUKxuLFpPCLuK57VDuOqjOHOJgWI4SlCVnwf0CJvORw/CYb0Dq+kFZ4o6RZsGdo68VOHZLyMf+CVd+3NIg9gz/Qkt5luTR7PuhSYuKLiepHVt6A8KPH/XlXxjr7kuZxdLzjwjI7aIwActK+Xr6OkrbYi0AtAVuDmE7pfPyyaUwdnynPR8Y4Or2enrbV3OGAih4ZmWPO0ZliBYMCLXKzPnDmfXVsiUOEF5Ir2U3v+jxaX5iKRBBjmcXpY/Z7zu8nzJ5akk8SYPOBazmgRRrbh+pWN0+0ohZCwnEGlnlCyluQY+z2x1lgcczBXzEEd+xMz2s3WEM3EkYsELTu9/m+RUeEbkjPUDFSz3ykzijM/jGG+uUnSNxVNSxNuBDpeS4B0T1PlqefARDKB7Bwsn72KK+iyMT3cWVUR/Ew8KH8fFjCs4UxG7vuJCuH9K8rU9mV1/yI0in0FtAeWJhawFXdzkCZyDAOwbFmEEZBdhEwIGAGxUQQ4EQZc5ZwOkJCwN1Uj8Sdakbg4Yj3RCPaKnWcsV/z9ySSHSZvAgPRng+hY5MZXkPMLMHmq7Qp3k/VCT0WHEvx3oSVZ933N3WnJGXUWmbFWugBfPTlZWw3hZq1nqn58UA9qZS3pTifW591MbtuS5WFPPuK/qfBmtg2yTNN8geFo3lv3TN2NPauZ/iX6ZIJ9pG/o8PktgV1p2K9iwfIGVkFgjG7AwbY9iOFSNMsSuCARMyCAhDGZCESXaJGmPUTkUjTMwg8hhv580MmJndjDPGODunGjDVrvo2xnwPfVPudur47aEmFmN+ToNfVXJoCfhjxbhPmrw2RUt4333edu4JM3pqjMwrlZqFcVglyVLalD1kx52LVL1VvumJFxRVz3s7o0d35Emxrz7jp3z4RdEFrGJcjHvVRYknq7prLw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif