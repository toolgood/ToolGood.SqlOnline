#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/db/table.png")]
        public IActionResult __img_db_table_png()
        {
            if (SetResponseHeaders("E9F211B4218C0D58E2F169914BB73E5B") == false) { return StatusCode(304); }
            const string s = "i26AiVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAApUlEQVQ4jcWTUQ3DMAxEA2GQCiEQCmEQwqAQCqEQCiEQAsEQXj+Wkzx3/cg6aZbu46zLJY7tlO4G8AAWoA5ikcGT7yMnoACtGwn0V4mvHzQNmGWwhbIaMDmegRo0uzeowOSg28QLYEFTvYH1QwIhZxeayxJstITbfwDvPVYJMee5xU/MDtbz4mq11/yujbNa4mC8Bkl81ZMdGn0StQv7IMp5s/4RB8s/eTz8xU8IAAAAAElFTkSuQmCCAw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif