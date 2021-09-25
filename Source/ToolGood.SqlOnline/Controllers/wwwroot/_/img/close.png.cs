#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/close.png")]
        public IActionResult __img_close_png()
        {
            if (SetResponseHeaders("116F7A4EE66940D66FA9FDF551A39412") == false) { return StatusCode(304); }
            const string s = "G2sEQGR6dvXvJOa127NqT6iZ6YPmxOXsRf8NJ+mDj73Pd4OWdWGRB/Kd9+ApB6YNYxc1xI9G5Vr1Xu7h7gAAL6NBkw8A8AGWqzOHxNenmRYgjNWWsp2igH1ZTDcx5zxt6j5FSwBwiGsuLGVLSYuoobM9+36XNdfeBVwskXP90aHZv5aYfthzoK/8aAlBwmSXmm5qNvTuoQt6Wwsberc2CKl4uSxWwom49q52mq17h9WEk8bXWRJl0QUSKD62JJNttWxrxaVk7hXfG4hsIqsBRvBYvjAbIRBcgExwIQbBMVKuZCGoCBaKECLWrMXLYiU9lE2Ur9E1jcomLbecokfLa4dKejAKoWhWOe+ymF4dbB2X1cEk1ON0Hk7nYX7CjBZfY5Br73I7mB7FjDwvubouCMmGoVKfIEketLdLAgybT9tkZnzUoYmdonJC5STkbm9GnyLH90dmOqWXa51aHZs6V1HSYE6SotPvTKOGUvm+k93cTIlUAjVPi/FRlVItQBAtX6nTabWEQEjgGgLnoWjV/OwCgFG4MLrfclahL4yRqd1bTDc/KGpuG3wwV/WdFMSHSBC9BQ1pOic3+tAZwnhLWWHMD+kOiv1pPfFy2cRx/R8DAB4ZNcpC7vbUKaT7YYXnsCujgzofHp4XfTiCk99rUX70CVRWTMoP6o5zOnT/4LL5Sn7D6g2w55R8hxh68g/bZd6T1GVWzdf63YWyYMucm3NX1rSFph8Cv/vMdzV1wIOzxRsXh2xPfev0JPBb63zMSe9qnyDX/D9emMToXB+NXoVJ1xWV3DB92D700aAXh5Yz+8U3n5zCZ/ILtrhVHrp5Y2w0tml15rufTd/O4rPCri9aRYxjXCFd/3F9E2ed8F16WD82dwK5ZcO8l0GWm9YFDocNt8unnfxm/L4m/+7q8zE70m3lMXbOYOVxwqt53roRIBwZtVbNelXtYA==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif