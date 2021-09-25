#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/css/modules/code.css")]
        public IActionResult __layui_css_modules_code_css()
        {
            if (SetResponseHeaders("EB41FB72CF46EE0F786CE194055ACFDA") == false) { return StatusCode(304); }
            const string s = "G+kDAGRwXqpDw+XG+NAh/t9N4ADxxqwNuizwP+jKhoW9gKI0xnStnc/qEQ9FHLCV/NB/4C6+9zpdVy43uSie9AXFOGtuSTn20/XzUd64dMxyrlLYK3GsvKdBLpTyjXcRsHXxC5uStaPF0mE1TKbLZWsWFduNAiUpmLy7UB5WFF4xiVOXv7xxX+p8AVF+plm/OCIAZtkVFTahwqpJou40YCr8CYjfiudLRHXcvI2TLaKWcqPrl4DJvbFrgCP9rXjfxhhnoKsvxKwtVcs8k0VQJekCm8K1TNJcKy5yWiztv3/JaSRyoj/iEE+utb4wTGrTtOTvhwx7SAleTHH2MqoTPYuHpUsMLXe82WZmHnqyG1jBiUuAVARReJBccxAGBA7BViqOPw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif