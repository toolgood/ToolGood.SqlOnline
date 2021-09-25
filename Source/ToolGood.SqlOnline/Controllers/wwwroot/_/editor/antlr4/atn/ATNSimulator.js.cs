#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/ATNSimulator.js")]
        public IActionResult __editor_antlr4_atn_ATNSimulator_js()
        {
            if (SetResponseHeaders("7E88ABA53B981745FE52CDB3B726B9FC") == false) { return StatusCode(304); }
            const string s = "G60BAMS3d3NaOaYEqIhuordj9mr5rQ+eA/ZVvwKkAuh6K8ILFIS74uWtGeYyLy2hCj0IlhyicPMsbMEAVkKW4KBD8C5UMJBW8ecVyYDED+nFUrFY8gK7NF9OPh4i5mLaWbFuOTmuYy5lB1/KSnRk3QbFAbLK+AwzQKcw9I3Z9hSOLDVEdZI4u+fFyr70UHwY8dMA/lMTqlbqrXq71qwJSzjE3EORJ5mGLt4MiN1F4G8cEB5/MPAjzTxD9DiBSYzyvms3ottZwv+eywA040Ocq8wWkT8A";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif