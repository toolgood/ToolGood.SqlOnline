#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/ATN.js")]
        public IActionResult __editor_antlr4_atn_ATN_js()
        {
            if (SetResponseHeaders("8E9C25B2BE27D9772088A94BB2AB05D8") == false) { return StatusCode(304); }
            const string s = "G8IGAGTTptOasHWnqp81rsq8BdBuXZAf9An/39oKhc6jRh6xrPydRXwG8SriM4hJ5URDvxDSPdTZiV81CJ7kYJ3KAEkxglqJis4jBYMgi48FF3MHwyr5SnwoFMTyc2+wQ3Ljz5h4JK8TRBb3W+T6IDHdCz4IF0HDgjd0oqpt94ijjl3iz61cvsElSolsJvR7kzsCGzNsanRIo9DjY+cNlX67cXkNaMk6zKFRFpvqw2n7LoVqhYyjJxOJHAeKO9l6k5ktAGeBl9ycxS0rwcdl/7Ln9FsxEkGBtoF94Rqeo2zWIQGGrYsJOWi4ykqafAKwuWtW6Zk/pkgSk5sBUfXkukLu2RVcTaC6kw2JRPPTEDOH67gTxpTPcUvoMk1XqG8ynztIpxZNoqqaVssITVS0QFC8H+qDYQ4HqVOxDHY51DbjhCLEIuPWjjzaI2UY8EIpCLSA0g3pBSKVMQMUceZCWTuZYUGq5YPOg8LIA76cXqH0KDMnJ+JN7AL+qieDvdXYE+3pqwFNU/lBiZ0MHzKGWPuXDkv0GLGwwRs5SnY7ObYj1u+nsOE76R2K6+Fv1KOvoa86ttfZAZYhUmQcYLX88kr+Urp6/bh7fHkG0E1uaX3MjY9+X47eDwudmhLQe8nxizhclw0+mRUoyCK2GaKy3j7mpNWid2ohcktDfhpgwfF9Ylwseok4pKCmRcSbFdiNY+BA7V/wQld1GptL3GoVKYkbfbkG9G6G757fmLd2NEE+R19gc4AT26HIorPPZxY=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif