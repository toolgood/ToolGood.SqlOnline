#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/css/dropdown.css")]
        public IActionResult __layui_css_dropdown_css()
        {
            if (SetResponseHeaders("16B206544B7215FFE5670FB038D513EF") == false) { return StatusCode(304); }
            const string s = "G2ADAGTZXKrE24CLIqlfzBa2GTq1EjKtmEa6Rpcx8Hv9G1enA5Xdgrqlpk2Kj3ncxfdeA/bt9/bEXFjJK2e6fqQ/cu9e9DFJexunSKZM8g3KKpdzpwtRJjeFvagCvNSoFC5LxqtzmBFF1hLJ2eJLmcQKNYaHTUtlaockicy6wZiLxcHtNVjirN7XSBF8xn6FlDJ0ZeW8ELTXkRQLXwqMZWC6yK1r2dVvlmfMNWQcmoJt26Do3SjuOoTtlVnyBjyeFfGLrN9qI164y+WkyR516025bJ13rAehKDq+YAPRemJ0wviVjX0HHWA1bVUdbPAh43phf327w7hBU1sTkGU3kgBOFLpL/gE=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif