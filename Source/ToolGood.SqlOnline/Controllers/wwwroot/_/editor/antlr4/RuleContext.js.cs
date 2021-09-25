#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/RuleContext.js")]
        public IActionResult __editor_antlr4_RuleContext_js()
        {
            if (SetResponseHeaders("55AD7DD8F58AC3E92077A258FC526E2C") == false) { return StatusCode(304); }
            const string s = "G0UFABwJdgzlH4lRSka+qf6mFUSzL0pbYx51Yqtb6Q4nij7nyFQ7nai+ii1v4JwMB73CoooeUbTot0bZ8Xw/yJmuQ5jOFbxhZj9O0FOnLEVUsk4fHGnJ85jpnBg/th9oKdmHjyqJfq1Mhu9kNAYppR3HVI2jaojQT4663lKM/WWKkXx3nsWfmWOjRCWfeD/G3L8RYSZTv38w7Q8zAnpK/38D1iVoFxMGwUP+MjJSdmzGk/Idvp5XCdqFm7wn4FiaRgwDKxxuBV0H4vISvELD1WNrMOLt2KpoWZa7mHnGGHFAbAVg4CPCeYnWsrE70QUJy09sUOzRZ44Ko9y/Z50+bJ6xsGx4KR4igWsiXC2ASQGAYZ9owprlQ/fx8FkReBXCqYKS89V/JR6nibwJziHDZ5AdOZmjmMWHErM7SCaUdO/ax8kgrpxhglS39fAYBjl2Xz0Ei8AMraOZLpXnNQiOyGUI7/aqxQo9dKxbuc42zgodPKOYZn2V5ScHgsaKcOLMc4BKSguGCtsnC+ROJ9AVBQGPtL3dowxDyIHavqDkE+dLcamB4GLUTy5DukKKPHPdtxl5D8P2dt7Vc3V/2NerfDMRNZlTg9mzwBNVVStFYYWbwle/SWskIY0xhmyAoEQ3VAE=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif