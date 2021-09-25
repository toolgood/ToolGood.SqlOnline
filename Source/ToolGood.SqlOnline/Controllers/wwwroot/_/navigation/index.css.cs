#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/navigation/index.css")]
        public IActionResult __navigation_index_css()
        {
            if (SetResponseHeaders("260AB43C4C2561B9625BB5E6ED82C877") == false) { return StatusCode(304); }
            const string s = "GyQHAGRV5/WOMHJajkfn+AdqeYiW//dSDlUPpAQFLdrlJ7m1qd20qcV1cxP1FazAWCfW3Px5YhGgmajWe7Sm5tiSccUKWm7H+kVGi8L/KlWVSaYmUT9VoD0A9jPg83JVgAyjAqaZsgoYj/B8y83M60c7TkYjFlSVBX4VaM1XMnJlr+EpBLRGG+HHCRD8GdDJU8yRGBz4cmy8BtxxR+7jqQFYWI7Q3QGLw6PZ4oARjVD9RpWXGpQRd5p2H/BjGj4bNtX07Sg5V4lVWqBPgJBF63EUnzC2HgtRaXTuSZCYreBVqQWLK6U08QfDcqQfsWirFaSAEH34FbIOgiALcMXsOBmhNH9DgYIVUFbR2gTaQwWpxWdknjSQrmFYDiAoGH8KeJ6UDmVietqdm6oZLl1bz0yMbI5oMnX2TGhyYC+XGSmAbuOY/ZURp513nnrvjDioG3QCifiRQhAb8bTc4Fc6o2ANqz8zhkwV8guLZqJR1STs0DmEFFVBF7U5sBigIAi7AVcBJLxiTEFqRQo5V7qxXLoZUQflze22MCN7lpK7+0+24gcWzVr6ZvoZj9SQ6kcpymmlf8yEVo7M0CoQcfvkr94UCfjQm/16oJNB4PSGIqREFvElczGct7dijQE=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif