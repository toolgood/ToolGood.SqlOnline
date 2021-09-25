#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/error/DiagnosticErrorListener.js")]
        public IActionResult __editor_antlr4_error_DiagnosticErrorListener_js()
        {
            if (SetResponseHeaders("C7CD974951BF0D73C4E12267276C56C8") == false) { return StatusCode(304); }
            const string s = "Gy8FAIyUrr50q6per8NLhAjGdxSm8/iCXmvIYAflQXXKUTb/l9NPB8h70xp0R9feWBgVhr2o52sgL5DQygR2vTle0MoUgWPBcqGqsjairx3VZGKtbXp6cpyOoAFEmtiZXeEyZgHiWwF4y7y9cv1YAPlc/eikJFhUEt6Zp9XXGZAvs8ilkRFiyAmhJ39xk7HPvICDL/kYY/rvBJJKnP0sl1lsjeO87ezzSYeq/ttrtXNGSjNVyvRKBf9T2i44WK3lFGjeIiAp6V4EIoczvHokWHsiRhVkMMKue+RaPPvw5UAGgGlq/7LBBDMCzdEJmJi1ExYEhy88chz+6P74QLWWe5ipIkYgB2mtFdf8i/WGS1hwXZFgPceGKkcjyIhuTHVHBXccyr0ZBwbhpxwDudeOMOrTj7ao65f676M3178/+tHecePdF2cWH/aIGqri/OiWR8QCAVnO9eDdejOz2C/YLAQRtcDQWPd9a/UgueSo7AQgzrug5+S63raW5w9URL7AN0Epka3eIzn6wYvQhSPQaQRXeqXjvdyor6RJc7CNJgGtq7KUyxyqoPhgJpU+6d5Z/fUzN4sWsgsqP768hD/RwnH8+OHoYQr/EOvy1MHitBXZBdQBRlhhTUWqZ/lSHwcri+tzklHFAA==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif