#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/prism/prism.css")]
        public IActionResult __prism_prism_css()
        {
            if (SetResponseHeaders("D8E7851D325955D8BB6330B3AE4AD2CF") == false) { return StatusCode(304); }
            const string s = "G9oSAByFcZvzd+rCjVAUxWNiWhxbqpj8uzgDz0Si2N5Q26bmtPKtKK3AxdOSKspXjHwgf4mOanPz/fb2c801QGIcawzWwCS54Zx5Q6lNtTZ5L9uq4q9iv6U1YXClSYXCxlCyw3RNJAgKePg7/Ct+qnVy+TGkfjAM5+su97WI87SgpKg9xUj84wktFLGspxQdreujtMMY1MfdNGtbzOw9cX+upUSqG+OnEmMCaR2B+6o+5WDMS7DjFNpEptO02k8Xzxm7Zuz+qgKHfE//wGgHn7HIj5T8i7j0I7C7druQiuEkoYzaoTRD6KnObcV6IOZ2oYvKMu4yucgl8UWNIx/EntjQJevep2XBnV7K/H20nMKnLugUK9n3YM6F5IXX/CNsM/p9SDzfTf7aySrLW4tzn5iymmmZGEjb7GxtiuOJPWOI+tv0cSAl7aIzYOnCkUVU1H7evTrxtnZR4GWh9UXxdTIkRXTB4UrCRKc9RPf/tupEPD/lsGhCxjexcChalr37f2U4XgOVPMvT7egpienpiihev99hXvaAbjJEWOndfSEvGrHjtu273uotU6t+UiJa/L2S/bkeqCamQBPDHI8uHy507uxpzUIZMImbXNT1Oia4SVwvZkWSk9cx81pwxKT+W0BFYvbKi1ZWcaPe/t/ck6LyVXcQYVk1K39DX4iRw5zrHW0Xn70MPZbW27F5vtTnAZjIeqaZ9op8vigmnMa6m7c5m6dxizfPB1i24bQm0dp3go47ukyRtpSjntyOj483nUfGze3OsIublP73XuuT4x6egKnGZW+8xeYLeYcYR6q1RDFg4t0XFOznNyFhGzoAE0BjCIEPjEDGvcQhdGEEsonyLDJn+QyBdLTJO1QHNEIau/aN8V82do3OaSvXwH9zz4XE+xnZ3OcIKAVR71i/hOwSjqAEclE7x/4+BG6d9yIjJmsgwRlcUl7gIqemKcCQoWo4sYn/9zdz2IbLpfAJ/DuoHfpDPKznSlHzSFWV7Wi0aXZ4A9YlP6orTY9eqnYVcp0x7t0X0qzQ1Pa6JJ3kWZ0ELIjeMEGUjvfLEcVf0xD+w7owfyt+T8BLCfPlBR9Or4WkbUnAVDDl+ZTQ0a4aRtFmsNjB4GkhXJe3UyZujBtdwpe624Wd2l7iDQQUBG0J7YKWByyFRIW8rKTXbSASaZbyCSDTuYhm8bMiDXeOfUtdMEki8AfRmJoL6EWhR3tjYe0STbWaaJWqowFT6B8M75MpjSKUtARAWUT9wzjgsAUkQIX91XWoQBRyRBih5hLMT3OQ1zNUjpNNBb84pgKm4GJOC5lDXNWyXPLn+CpU1rr7hgKwgLyflSKVPV1KKyV2qcZ+YFM6TGKfRVi6Onh5WmvakFbIJRoRCwQnclrmd6cF1CSrSwYteaK6DrFSpJ6r1e9PNwufoESvcIBytsnNDT9WmVaU8K+Mu6izijrpDPpjBeZaICwB9gmoZ9M3WcsB+WgnGxbcW1FU0RdwGFT3Tcq3U/3ng2HPoZXDLq+ASWt52KCXnQ8qOslxAKqDQq4mgoziQglai+u25612m+79bqv7WpvpZaKAEbd9ZmNt9JOT5rXmHnCXPZJ5wXPWWSKGJW3l7ymgm5XHol4rC7LeIj5bsC04t8nyAAw3F/lffVMDYfWwQqJ7HiHPzyg/OohvlZ5s0YoD";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif