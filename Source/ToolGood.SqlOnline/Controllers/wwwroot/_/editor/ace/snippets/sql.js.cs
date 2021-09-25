#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/snippets/sql.js")]
        public IActionResult __editor_ace_snippets_sql_js()
        {
            if (SetResponseHeaders("8C49AB1D64BF03CA55F60B32A3B61522") == false) { return StatusCode(304); }
            const string s = "GzAEAIzUYK02C93/NjOlfVwabymhfohvu9T56vN753yVs1NqRpI1zc/dgBQg+yZjlmIqsomIhC6jg5vksqL6I2MEmLJj7tAtCmBmc+vIaAFjRDOMMJYoIsIsMJEm1B8p0wotl3+FAYGfGojbWXLkYwiUcPI0pMr9bEEaiTS/T3E/h4YEcFpD9kNAsBczVoJpQ7g47AtjcZcxMkQUp3U8MB+3YW0jiDbec72rgszmSHBg8S0ox26BOFUhjrMityWsTE0y+7tPXw7lq4OSTrkzGI4uzo5zrmcgd0vidQc53T1x2HFmOBaOCyfHozt25GMxTyl/EdZ6K652OxEjWu4WxWYBCG3LWm4bZmFEz248KznWvNsIvTRYZocQA/Zob1IM9rVXK0NeB8vKjnxDPcCbticdFNu7qxO4mJ6dOlA3Mc3mKdsIKBG7yUHlZN0i51QIjFYY3z4OtPAHZd9FhMayVt6Xglr16/Kvq68PoNL/8emtycUFYSbrtVLS8jH5Nxw1lqpmiPJBBw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif