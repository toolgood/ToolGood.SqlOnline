#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/css/sqls.css")]
        public IActionResult __css_sqls_css()
        {
            if (SetResponseHeaders("97CA49F8FCEB5684F128D5C306F13729") == false) { return StatusCode(304); }
            const string s = "G+wDAGRhm6litgEXW7zEh46YTZwLDEYWEUCV551gu+EnPgC9bjIeBAHEA7nXX6cvKEpi7ixDAnw0NCFxAnnj7vCTkat0RfEPvp97B01sjxZ5VTTZcPc6zw2i0cjgLn52PSf3lIDUgbmVE5WMht8vNJHpNnId5XS9GvVi4VU1YuJczweuy96ENkDsL215r9YTPf7Y8z2x7IBOk7UUP8wTxtnMXWAy0Bva17n8f+UH21y/Pt+f9++rC4niuZYAcGt29njHMgYNIqS/4QdxDOiIXwdGQ98HLWCowpwQpsboo2o5gMRQkMRRMoUBzxIqwXSZU04duQ5qhd9mQVAIY5uCO4E8Mv/c1lf1yxn66mBGG0pHhjJSlBYgCk4SKGBQCcBu5S+UdTQYVrkMNHm0ZAK0oqW8d3msAg==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif