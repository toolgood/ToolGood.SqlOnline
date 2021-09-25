#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/CommonTokenStream.js")]
        public IActionResult __editor_antlr4_CommonTokenStream_js()
        {
            if (SetResponseHeaders("D22AADAB8BF0E38D6FA9A6548EFC9957") == false) { return StatusCode(304); }
            const string s = "G6gDIByFjRs5kx0xJLlIr8eSpTqnvfjv8BrnWt5IO3HoKzkuENTJWrbxSlOVZrsiyFKIqs7/uBbxCi/Yqu1/W32RBRJtFRTAzTfndubWMMIoqH7ObXc6XxEhyeVNIXHNGOqlMgeis66we5m+5hYMAJYSXetPJLC5mXlgX3w1tD+PAQuGclRTTtqEz5RW3LSUNXjOJ3M6Cv9DxCZTQUqLj9YALYYPeonSY4ElbT9yrm8GkrzdkQ56HA6PVZngg89yBngiF34E5tKXV0loWeW/6tUJbV/SMwmA8Pc7mrDhSRKyGYo5S7f9koXcLzdFpEkgmkukDjXMoTPWi7aIi1NbghFNwq4ZPZZC7NyqFnblNmeoIH2sLM3AnccOY8hynJ7pRpU7T1AIA+JX5D+p3nIg60TcapEF1SQz5ByCbmxQ8cvJhmpoflYJA+sqxGKvQeiumzoOpTTiAPRFqDcxI8fyJ3Ec5kwpXCjJyiVOVgDHmVaLTJZdfO9IoOaPuzj8JTgJ89fjpOG/m6u/63sQ";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif