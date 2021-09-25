#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/sqlite.png")]
        public IActionResult __img_db_sqlite_png()
        {
            if (SetResponseHeaders("D7E8C45F9C9D9AFB547E489199727209") == false) { return StatusCode(304); }
            const string s = "C3GAiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAqklEQVQ4jaWRUQ2EMBBEkYAUJFQCEiqhEuqgEpBQCUhAwklAwrufIdk0y95xt8mkH915nYFpejBAAg6ge5czkAOtwCkBZGtezKUnO7vOYgEFOILoVaZugOu44AJU7SVjl/kE5m8Bxbx+TfUi3gF6+HoEMPGb+aB13IsASYCi/9+8lBGgCXDVWO4AWf3aoB3YdK6u2UCKFkdtQArNH8C/mwXwez8AxN3/TfAGYnuErGUtkbUAAAAASUVORK5CYIID";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif