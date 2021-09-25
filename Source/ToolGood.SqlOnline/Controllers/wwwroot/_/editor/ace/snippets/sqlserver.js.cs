#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/snippets/sqlserver.js")]
        public IActionResult __editor_ace_snippets_sqlserver_js()
        {
            if (SetResponseHeaders("B229378F0B3E585D469342B590668675") == false) { return StatusCode(304); }
            const string s = "G+MIIByJcUzJ1sjVHdPZzLJd4qSutC/ixJQKL0bolNOU9X+Y77CDxyzCIfszs9mU3qjy3nVLK4ChdLAOSgHPYzjfiYIgi+Z8c44XzMcY3fJ5p8ZZstb/tRrf5AjX5fKzzOe8XJZzMu2EeEF+LafsA9Mpe2UX/3lVPPi/gVYUOGxV8vgLg8xAwrVs7oWh/a6p/jVYbqFcTokyLE83E99Lk78n8AtTZnfUgFT8ZwTjUjGESUEU/faSXO+gNmmH73mJiZgFcg9h9VAFEQpzx3E4WvGJI6HC5RGViohbmJ7YITe7F2SL7BtdO1uD29lAWvObgKWDncO9vTY/CcNOeNUldQJf86sHWgHzEd9DdnY2c47vbwCtepJq2aHCAQYp0zEmFdBCUVnAGHIPShLQVkPPAsU3fT8b+cS4hZq97Eer3o/4gSTM+cmVI/SbX64c7pgGxTn+w/1n0EILrDUONQsfSwU7Vk0YmD4dLHr64VSvWdId+S72DDBumtRR7zZSAjoaKRdKb90iZA/imTyy1zyH0DBo8steBHeXufPBgVBw0O2B/wLyoGFgXIbGc/oohd3fJnni8AhPqH+dmmVh7WbBfWbEBMtYRwJW+eC18kcK80uj36+3W1WHr7+tmuBUvRN5P5xR7QnsXm/e2wtDlQpMHoqlUZXNDBESjj73CaBRxs8Zooi6dz4YOJYlR4ZITPG7ExDJkdOU3VG1lKSzPUizRjK9RXzfAlSUgGjG6aDfxIscpDSnEXnWpGnSW6TzwPXuDZyde5xl9FxjXoUhn1jvqEY2GLpW5qcCGMRkehZGiQsaFQ49o2/FRPvuIzeV68sTGJlWxT7LzGeeLJU6N0I2vWvndnr/XE+OXXtO7mynUl9RW/4FIdu59d/ciPMSdYUBiRpvXgdB0aaDzfkqUDNH17NTFcneVFEimzNW+uY99dUS9W/m/apc9qo+Egr1CncEHtW9oe7C6h0uYz5fy+sg1vFfErbB1HluMpGNKRzXdZjVg5SdFXwM/70dqAZadYTAtck=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif