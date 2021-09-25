#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/mode-doTjs.js")]
        public IActionResult __editor_ace_mode_doTjs_js()
        {
            if (SetResponseHeaders("0A53E0C37F79A7D2E0157FA4A2639344") == false) { return StatusCode(304); }
            const string s = "G5sEAMRUU6f1cS4RAYYPeDShPXNWlyobDBJ9vhUZMbfSFvNuNenPHLW2htWJNljCrnNk0ZLICRXwHHihTk90b4/wR8aQMVVyp9bzZ9H81Bcq9aJuBj7A4R4YkHbB0OCCRTz7tXteFP9ggazayCZEEzAlmrJ+Yrd/cZEbmtHWIaubyh4NFKj11kcHEdUdYts1wcwhuQuU8bWP8t24/GJKbMkGyd2mogSUKiC4A5y65jP9m1v1jeQdqPuOFN3zmFnM+iJtnzvEk7Qxh103f8odHcec/wBX4R2GPeYZHMPQ7GG3+gnzBIlUoB/+7uGpe+vFpCeBD6KJlCBv7URJW3kZu6/VrmIjbcJxtZ2vEmlG5ZdBVWCBGScLdUVq2yC5Lx/SAWoyyoTDO7Y2gooeNAVaPCbT/G44EIp3cNxJWkX9onFLLJw9NOOEkvuWczyFAu4IAHsr/ngceg/a0+OhpC8TxLR3DtEhbeKw7y1VLKiArsId5hGRpzucqeRB7XqPo8Eetts9gXtoytPTmzXXGkLWjJRWHUc=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif