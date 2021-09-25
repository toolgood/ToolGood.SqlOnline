#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/ext-split.js")]
        public IActionResult __editor_ace_ext_split_js()
        {
            if (SetResponseHeaders("7E482497A60DF0B02397A2D6CF83CE48") == false) { return StatusCode(304); }
            const string s = "G0EOAIzCtuXzUfwYUxWnKEdWpvP1NRrGc4sXEb4Rl1Z/vYjShAs9gnJO5ePiOnynTN1ggTc3SL9iPz/Xqnnr1uBCJ1X2ZP9sp4pJM9/bR0yraKMSIrGSeAjnjVZN6ULu+9f/kTE8sfa1GqjfcJm65Q2j94wZMYsaYn88Hvy0QphBoGntUyzGok8/GVSVL9KoFXjDZsf/WSmJZ322RcG8s+a/EWhszbz0ya6KwW9lXF9Na0XCligPbiDvjXX50e2jsILlPwVFWbhUTkTAM1G+SZoSx2HtlXUDjfLDlpVgVv5byCVKq9RLYaum+K6O83o5ALxr45iOUbDeY3EFivIYVw+x14IlCdXg0+PbWw0D7i91vc/sortRs6VZb+mrW7ZTRP//bZSEGmSzcm18Hb1hKiF9G02XHYEteYT4DYhAyiMgOlVPFta6IE6CJ3OSaWbjpEQ7spWc+jdYGBBbl7TVn620qS7Q9oFE8AsZ6ZTVfMO2EOuQvYDzRxn2kVStnwyi+GVScurhK2YHeoChbzieoJSq8WHQepvZD2TGvDAdW/rqLFB9L3iEZ3VBBsZWsYdt7Wkx/adTV+hccL8IS3PBggSNbx6b9/jkTkYtyA7VVBgj2MK32Jughdwkwy06FSGjdThN0a4ClcwJlF2890HQpt0cdPMIxtdlmSvvMUAetxRibwwVNRx2sVeVY5rtOZno1uQ42nQT8sB5ydjL7fTM2FjwsJE4KtgWrFXRD/BOxw7SkVxNpPrzDkVa4EtE9aA2XkNssrIRYij0bOmru+jKWjjIWKYbiq/FT2GVWhE/FBAZZq/97y75KQvXlLWOjciE6aQzWAlvTGZSJbSmbRdyrSumIvEz0RdX8OFyclv3j8PUOy6NtUDjVvKBMJhBYFaSMPhWBEcTlE767/kL/1K8DWmStyhTLelp9u77mSA5AZAhEY1sfxsqyVciCaBChLs5Y4xGMwVSG7zLNYVMOBJAkV3vY/E9qw8P1VgijoQe+WIkQOYQqPBu4/gtDj7G1pfQ5+xkMGXKyPcKdC9Gxn/31I4rfWKdAR9I0hKr6akYPS+I3eojUusM7jfv69JWu7bpqHd6eXi/KlJu7N1Ief/JnESw01SmKkDG+CKosqK7stkS+73oVzKTIQsTu1KyBkiyL9eUNJWkywpNaBkUzDHaOPKonbZfxDwJC4Mu/owlFJs+VlcUEYR9hwqFSE/LVkfo+G5TB/x15yzDVDswiS0FBzZ0Ka9G2XErg2Jsb6Cxs4NLCDdTN+j5uh2rcFRj+pwvhNp0fGs1dfnk3ShQ9HtRW+24zc7EiG9dJqi3WNMLZ+tin1RFuEtQd+RX1aUowna6lHMUNY/LcsDLF5YlvISIwBHKwlkeIWsr84hje9UfjXM6481qxtM0kTMUa8LvPO2XqSoT+oyIAA==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif