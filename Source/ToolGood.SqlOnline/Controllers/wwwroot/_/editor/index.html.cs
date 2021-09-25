#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/index.html")]
        public IActionResult __editor_index_html()
        {
            if (SetResponseHeaders("337D00F23D262596AC026CED617D380A") == false) { return StatusCode(304); }
            const string s = "G1AHIGQzp6+vq4GSnoIFguGPXFJGLtke0NFl8icdn1M1Ak1xd2Z2v/ZmT8HDAAYrUx7idJbOP40ogQo1voUuTXUoWTiggeblILQRxiVMTAVKEDUzgqFIFhWjvpaTpnoMJPBMjaCAqAojZqzF1lk/ybQDvzGVC9DqCCsp4R+pELnCkrSE8E11Yj6jA/lXX/c8cXM1nXkzVjTghYrNXrT+RmIcBhsJOb+QIh+p6Y5ubkdzgy+aH0sdidaNWHx+qqIFiSZex0hLVZhHc1QSKv5CgcvlEklEviOaAYZOnrhLpmvIKgEYBmnMjRpCPyxx5hx/2VlZzHHJ5nBIfPVbuYPvmb+02r4wWeZQ6WNLN6sojNSrl51thTsz7mAHwiHRI95Me2HkmUOcAWy4nQ+/4lbAIQDi/5jeb1ZrlcTtt6hJg0hCvtukiHCt0VbO8Zl1cCme8R/jJ6RQDOJpBVmMOjyvfZVqG7bVavGRR3EiotZW0YrtCNLD+2aaREqkA6l8GqPfxOaxsDL1i4XMBrHlmoO3iP9qAFr9bpP5VUQ6iPCXxXEGWZnW5F7lGQIEZZLIfx0HgUxW3OnrKIrgGcIEOYkYQW8YXLKaOa40Kd+wpYQO53c1hvSQRvyWDMxRIQ07EOCSKf+CFzsQ5gQeQ9YkowwQTkIuReB4nFppJFcqesRvdi8H4sk0z0NgPZzrwB4S/kEWTsC9/OD+d2Msyd7sAjWQjt0BR5KxEYSh8TzAc+I2dLlUii0DCWtb7qzS2Qx3OvAQM91htv6Vbq6mL5an/MVafeqZo3qxHGyZm7e+cI+stasosiauUsdAODsU14Bk69PdcglrgYCiFNd2/JkGg8/fSb8dO3Lqd0lFhCxOx8zouHe4BJhzPKKpwGF5CO/QG77FFoRZRfE0Oh4xS+cVkfnIbkrT71OwNs4fUjp0juzOiVoqrECpuWYHP3I3YhlBE2r/5FjSlLOkYC5tulLePbHlWj5myuPWD+/I0gtpeifspmR9M+x3QAYDEdD+MYYHPwEZ+U25DLm/iee4kkz2HdE6RRm238wRDqDVldKWntZ/VwUq705LkDb5ypSmmgI=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/html");
        }
    }
}
#endif