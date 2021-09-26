#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/login/adminLogin.js")]
        public IActionResult __login_adminLogin_js()
        {
            if (SetResponseHeaders("B1688E23385A38778E27DD7B5F2B4453") == false) { return StatusCode(304); }
            const string s = "G1MMQJwHtmuLsDi+FVOcxKXACUeesPb9ndqdx/0uB6mtdpD2SSkHUQ7FgKWb0jQTm+NHphzBot+cqt+3dEpnXt4yn/vp7iZDJKo0l1YX+t5BX+EMCovWtEJG6rqAcHGqe7N/ASwgq/Lt/xWIVEAVULP2Nao6rDmVX0Uh0ElPKbMnYs1C1z6iPjvq9PUTHHdi1N6utvVskCtnXqSz50J6AvsDZHfqB0c7veE10H4Io25ZAHl28BPGg1v9HHdqWXZMYTxSKTsf3T6/nFV75X8e6ALrgsNnIm1TvhQNgJSTqjivIiycQ7gAx3EZZzCCwb+3ZOzrRhGlvUxvhjUc3TnO1DVvqLJTiiJbrL4PzRFyUpCSV1SxfAUJl+qaGF+rxdRokTXTGZ7aq/xkK9unHLD2bp9Jl7c+iBldQISxTG4ETrGaq94StO+3X28vGn7VU1u0TnKvQQju614+NgeC2nKFO6l7vRxePdCB/sPONmRz6zjoEHtnVh0IDmxuvVPYIni8Zys+7gU4ACzsiEsHa6uqDswmOt3EUZj5o+RSoEeFDTRmeTieKq2F2Ttp5EEVVSEPOTcMWEIF7R2ChZrBIXsvQlfsJwogarlg5oqUzOPpdkE1KvrVZfKpEzUDRRU0q/ki0hxkD3s/vxwqn/xMwL1Na8dCR/3K1eSVbkamaCY+VnsNf6bRqLzmRCCVR06NUX5/O8jkiHuFkHRZ+uQouU83iT2N9HRqKOpYjlg0DEUhqzodlDOleqXxBGudzD3Vw50jKA8EwNANoeU0wVTt7J+3+k9z4Dcwgxh4SA90O1zYr/uNaU3ch3S0EilSqhz6UPxnPleOjB/xqp8kR0vfSu7Gx6OB5tFt9dE0deV8p3fKLGjY3Hr5sSnLqJzfpDvJedQV2nS6rfsFJJYtHoImt8vVxbRNrnsCyX2lIhUK0HfS/Su756S6CNu71sMqjJni/L66cP6gyJyt9AXkoP5Xy4mG0inITbJMKxaRvqQBSXXVuo3hlIRku6ONvSfpMtuVpE6yzgNPHGn0/bb+aTiPbubUt/QgYLvrvj0haMTAUwTksEqlFQBW+q2K0GbZnJ3fDRoDPKWX/cMDokEsJ2OJGOdBwDYpz1qVqd49lscMlLPkHHXwFAELYQUJvsq25rcbWCnlaZPb5YEN/7JMMcRAuAZ3IBeKZYYCmJ8laF2hzT4Pdz9IFR3E7KH9solmMVQLI8wAZ4HSFm6ZXmOuwaKX139I0dlvgBRPhfgcz8L0GBp+mxASBdQVdv7YYOFYQzdpobGZ7iZJhQ2KTo+aVeMc7ylKUy9owQnz9C63kacqhR2rNht7VXZt6+fN9tofkTMA";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif