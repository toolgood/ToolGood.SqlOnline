#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/ext-themelist.js")]
        public IActionResult __editor_ace_ext_themelist_js()
        {
            if (SetResponseHeaders("17860A7CFCFF4711EC13E78AD54434DE") == false) { return StatusCode(304); }
            const string s = "G9gFAIyTpR7l4cjKnL6+mS0G8FVADmjOtpqcQUpPsoNtlq4dk8fv5+qiaAliIZS/uycT87Zmen+rphAKldCJRGIYNobro1mDpy5Z1Zcp1H9kjACo29RTv0Y28LgDXRRjBVAVywBHq6iWV3/bNDVfXiB3uuS8cOhx3yjZqZPZJHG5xb+0vd/tYrFGBVS721KbR65POHPW7CoxUvuMJiaYF03ZRFOhDUPnDQ1zxuuS/VUeRK4Xv4WC2VebkvBcRys7RjlMBVW+J29ZTHuHguBXWUGu7zYXy/L8gfxcm3tOw5Gz6ee7HS0eBri/1FEs9cX1OAuYTPgEdIfagxXo1IisA4SRNJQlo1W656jKAQylJpURC/88EwrzlADOtGxAqW3BSOPWpeTF3DRlGBIJcUhSypo1QwPuxAZxh6NU79d1CkQW9SuhqUQOlUUAl1H+KRFT91oCLqy8oxm/mWB15gQZVAqOwp+ssSMf4emTWhSrUjkhBnJOApoPVqJVd1sBShhUEQHBZIBJnZYgbhoaCzWKwGk9Jx2Nurv6XRwTjSfwLNxpzYnr8AYgF+qV2RIOI1PV7QSV/tnaYlAyN4W7iI+fbLXcA1XwK2sXVyec+sNZAvntgwMh40vXJgEUsVaxBH7pNzIatyvCRnW/xfum2265NN1WXVXfN4c1KJTVYa9ck/3FOdoBO6Hafh2h6gN2SjdR5Rq76ucDtTvt7/tfYAib5f82p4p6e7vL/X2pyX/5v1xrl4Ny5frzwmY3pKS8eBc5U67l/X20QyrYuiHXv4QYXskObz6jDVjelP+Tyc0y";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif