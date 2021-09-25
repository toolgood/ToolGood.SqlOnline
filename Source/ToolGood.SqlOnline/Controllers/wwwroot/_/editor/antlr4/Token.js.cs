#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/Token.js")]
        public IActionResult __editor_antlr4_Token_js()
        {
            if (SetResponseHeaders("61C9EAEAFEA96E2FCB82A642FADABE84") == false) { return StatusCode(304); }
            const string s = "G6sGIBwJ5cbL3DFaWS+y0plV/eskjY5YKti5qYBSvOOSjsojyLbltG8+DHQvSL9o4Nl/a2oTVjbGAEqf7PzcBtFxy/L87KlsCqQIXFwt6b5aV7uC9V24wH60oflDmhtQAKcEYbgZNGUiHpfa4SVaMmeXl3UEn+HjS0xXfeOh9JrnVfpMk1bp8W9qvmSXU2bJXe+ythHQccWKHrtfl5T8evj05bWO7rhOYoRZQ3uXvWH3GtunkO9s1HkUbpB4YcMc9iz/cvZVZxw786OZTQWq1mQytNNU9erSk7Ff5YuDC6YRwZCLl63fO7UeFtZOwMs1Qp3+ow2s133eE1X95flxCDK5RqjNIhfoptOqzntrjkokncWa2u8CcOu1nk6a6kasEdsV+NgN+b9m9f5lMBIMbhU5hMqcGhB5WYoNFS/qn/UjgnLdP01aastuLOXx2Tn5Xa54/F2KNpLSckU0lLWspA7mYsuyJw2Q7GlAnjrdMJXb9rswp9uRIVM4tZfV9IswbcKXwUlI1DdhFrHnte0L4aaBNxTueFWkK4eAigsPERYQ5KlQ727RAAbnakDEvjdd8XLekbHCJN+1T2cOvNHEoCnYGrtR7++Xi6hPxM/7WY3FftIjfkwXLCZT1bl/9XYgGUz7Ypio5RrvJ/47srkKxpGw9NCevoe0q+hvl8u7vp96hGB4hvNtxXHWnKoFStTHLKtDenDR4UHiSKnzgcMjnMHW8WP7BLl4K8Fxdz1g2wF6sGLguBwmlBAgKK64o0SS/EidMKoK";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif