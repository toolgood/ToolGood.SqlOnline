#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/login/login.css")]
        public IActionResult __login_login_css()
        {
            if (SetResponseHeaders("2F0D6CF09B9D84D60514CD6AB7F3C9C5") == false) { return StatusCode(304); }
            const string s = "G7YHAOT/6Wxac+JAP5o2TGc6rPeIGqnyuFpO+zgGp7AW4Zjb+3tKKaqXpO/ept5HGFCZyNKFRKcYJ6PDXCf6m3XCIAk90fy9CxSAedlWp6ovognF0Odo3A2nYr4k4pETQUtzcQtJ57yZEJEAlOotyoKfAbEuci5af7Omt+fo/CNrd9lSJSb4y7bZQOfwLnqcK9OCirhS4Mg25JgKlVGJJ06Xbmm5zhx4GhX8U/5qUgAr4fEi4nMAbFRP64s44+T7vuQuFgKzhJZbCsBSS0V8qOhkYSiIkGMUR9HgolekchQbBozv4YtJX+DOHSC3rQoYDCuZzFQWFp7Hs0mrrY2oC52PtTNoVKkAVtV4eGgSQU7hA3JVn1BpA234Blc9HrZLABbFwyEVsKNgRhF3CYajieUKEdwoWwnI91Ei2FqGVHDPjmhEGIPj46pSJs82mvHNCDRH0BL3Vb3NlgACOh7a83MRm1tAJz8CobxDECTWhV0KDNHLf4EY54ioOz2zL3sSL73jsT4mGuCYwJsUl5a4iOcYiEe1kESx7sQiqTRpcZF6ONC4s1hJY1h9jkad6atx25oBd9SlOpFSARECGMOqC/EHrWJLXOxBO/BK6IE1U0cRi2Oo4hTB0HFM0w1kC1AF73Fl4txoxkJ4ZRWAldLHqAglzCfgq9i8PXInDRBN8VEJSSCKunCW21rLK7nZSVp3K0JziJCnkdrVqgcUXGk4IldE/cj1z664erkiCU4hFqyL7HITMdoIqNq1DCM0Yv6PdLjlahPYhvhF1nRxKU+pvcWzoCsC80Je6WUYuzmJrCdJEm5ICYRsYdn2qIjIBjTxBhuR4D1DM6v/ClV8ZZF4e1Gcq+U985VwPDwngayQdqUB";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif