#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/PredictionMode.js")]
        public IActionResult __editor_antlr4_atn_PredictionMode_js()
        {
            if (SetResponseHeaders("D4A9ECA50B527336F4908F3C379A6E0E") == false) { return StatusCode(304); }
            const string s = "G+kJIByFcWO9iU1rbf3Iu+hcuV7fToY4wfIUx1Y66rGbhb0O1JZSVSdd2jIExYFHYW+tvtjc/GrsV79obdh0hpAp5fbtfk8uCfe7Q1RCJRJpUMnUQgyoCJu4rrPU4C8eTs82GFCqMeZgyHzUwT8FbhEVIjqyPMwLF4OTxU3uT2vno4t5ZXP3JMfkHi5yIOlEITdsr9SZ01ApCGtSyRRKRUFauOo0DEJ+WqfCICyxds3ZOCttfALPYtng9LHTVh2eeEmNTFcew1+OtR2jH8ljP9Vr0iD8XB/7eYkgxwhKm29ki0GzSbGz6pw7Ht+8aedPNrlWvlbZFEvD7wu5SQnZxascoWAyigmbrvTOKn15trEbytMvI1c2ogvfJgoXS3KGFrvHA42bphbpIQNwLjHJ5xh5y0TUwiJjqnITPL/gf+kFkKRPX0+BophUCqHs6UM/pikS4SFdP7jQCjfDKwP/U1wxecQbgP6Bw+WYYnc8wnIJnZ0ETy/S4nKyfac+f5Hjwe7NyRqAIzgmgkVhoThjzzN0ck2gJMXbFTqDsg1why0DY5qsATTRXVbbxhPoTR9OtdPT1BkgvXQ4hfDUIRP3zDs/CWc5f++4k6zZ0macfcE8NPXFXTHxZUUznLDCgc4YY4B65dhhGWPGT6UWJlUU0iXhkbDGWgbz5472aPahoHURbAAVCx7ghqS8SURxLqC+/7PYOz3T0EFEq70Wp+CPo4w74MEpByUTEQ5wS4G8WUvJbz4TWQaDYfwEc0aWotY4rVRqzYD94HYhNykTiA+lPwPspCeJqLXfYPfQJtccbr6I5pyTxeh7AAf9z1mKz4uPnQ90DYQXnH0nxxEndX59Zfs2xjBzLQ/1DOWC1gY+w26/gi08b5s7xEDvZUrYhqJvK+gOInt9jrHipejj4PpET81YMENcahiaSzG6zRgIxggmB/2wgOJL2EySrLiFjxZgU8XAAyVsyYcMJ8et14fQlxtvGK/X3TwmsjfyAk7a5TXD7VaKPag3w2AuPnsrytKl1Gwjym81ZPZNNXE106kO8siZ8HTbz7qiKao/dNob7vmkTSGOVOzke21d4+gpTj1JGg==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif