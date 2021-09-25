#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("robots.txt")]
        public IActionResult robots_txt()
        {
            if (SetResponseHeaders("18D954CAC167BE80C8303AA1BF00D4F2") == false) { return StatusCode(304); }
            const string s = "G8MC4BwJNq5xIdl6Wym8G7itkAh9Ge00q6IvCZVjXru/ZjtQCaivaCBBQkl3SddtkMAwSLJF6TYRNcLjbkWtjg6prg2fmcNq65Ow3yfJX69nZoh3ztnhE88ACc87qQeIQg7vchxIGm1mSFdKbJHThztoDiZeZSCfe2W5zGLcZhbxfnGjuiR7A/9Xy+S7E0wR6/VFu/s/5NQj3KvAsX9PVFghiSr1POAUSZVOqux9e0bZblHj7gXI+lazAQ==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/plain");
        }
    }
}
#endif