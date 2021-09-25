#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/js/timediff.js")]
        public IActionResult __js_timediff_js()
        {
            if (SetResponseHeaders("8913B3A60D382C45FC6716CA4E7FCCD2") == false) { return StatusCode(304); }
            const string s = "G5oGAByJcazIem6FmOksq9rFBbJ1PpS0oBFVcc+P4nxIrO9nk6+uB5pxLkESBbsJ7GX3P7cMoUDLtElNd/f/MLMmtiFRNEQeIVLJnhMgQ5jOFSoB2on9M7wV4f7BrqaDTnCFwYz6qf9v2os1dgBE/6YY2bGK2oV6e+i4sosGT7b+b+UqIn4YUj67trXYNkGzFaUbT9liejqTZ8kP3mdFKzyeDvL39saXwNS75QdAdh8giZEH/g12nccmRYxX3E5rTh6Xzk892z/CfA47JQ7nwKg4AdYwQevAkFkG5mRpds3F+WyX+Tf1SXg5G8oouUMMbm+leuQBUEpbKpTmqpPUFABnVWR9zROmJQFwu4+RP6/GOt5E2IsqSIQ4qK8lBlmpjyUEPNVXEkbm6mOJY3ZR30qU2Up9KPHcvlxvt2SLUKuAnjbD/dDJDgT70Ik+IT5sQk5cD5swE8tDJ7YM4OETUsbggImkGq7DJoqgNB0keQAB+tQVlLYCm8fQvLhe+7xySu65SPnA/1ZaH9wWYymKQ05H7tqD+m8dlt6tNXZtop5e3t+57+eMu6Ye2rUzlg/dFLr0C1P5G+pd/CH5Y2tdA44KUCN9EaJ+1h/jFfy0I/WhQTVFaspNOViAegkBgnOO7VC3p8v8UifPsh9s1Wuxxld47dv8TD9kLy/Z0lmOffMaaFNTbV26C/zH81mpewGC/D4vJXFHOq4kxWvdmhjZkpoP4/2N";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif