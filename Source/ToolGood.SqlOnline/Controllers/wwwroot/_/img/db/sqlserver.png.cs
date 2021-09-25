#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/sqlserver.png")]
        public IActionResult __img_db_sqlserver_png()
        {
            if (SetResponseHeaders("73DD8BA0B737BCE3A2D3DC0D14D40940") == false) { return StatusCode(304); }
            const string s = "i3qAiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAvUlEQVQ4jaWTURGEMAxEkVAJSDgJlYAEJFRCHSDhJCABCZWABCS8+2h6Fzop7cztH4G82U3CNBkCnFXvCghAAi6yErCMNjvVqLUPuQIWoxmpuwIDotQicGhAbADmB/hbA06JkNQHSd7tLXc6/0vnBDywKrjpTgM8eQsR8NVwLd3yWxbPasirwOPNLf31ha/VH2zWD60JB8N+ObL07/q2On85kI088bH1PVylH1pfTzKHArmAAwhDzRXI0fiZPhCuSgnzfa9tAAAAAElFTkSuQmCCAw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif