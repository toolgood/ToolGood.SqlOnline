#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/close-square.png")]
        public IActionResult __img_close_square_png()
        {
            if (SetResponseHeaders("BD0129F8A475A4DE91CA5B4056DB8474") == false) { return StatusCode(304); }
            const string s = "C2OAiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAjklEQVQ4jWNgoAb4L8c2/78c23sS8XxkA97/l2P7TyqmkQGybAE4DHz/X4bFAVkOlwsa/itwKKCJvf+vwGrwX469gFgvIBuCVTMxYQAxBIdmYgx4/1+BQeC/AoMArkDGZwCys7GFCV4DsPkZqyHYDcDtZ5ghBF0wH0/imU+MAWSlRHy2YsfybOuJzKv4AQDWmIn8vl50bQAAAABJRU5ErkJgggM=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif