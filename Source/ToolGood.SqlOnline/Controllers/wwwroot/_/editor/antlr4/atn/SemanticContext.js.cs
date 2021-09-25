#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/SemanticContext.js")]
        public IActionResult __editor_antlr4_atn_SemanticContext_js()
        {
            if (SetResponseHeaders("6F25055B0AE8B1BFDAD5F40853B87C21") == false) { return StatusCode(304); }
            const string s = "G24QAIzCce8Tn9baEht2VuqsamMVmtP4oljznS4bPVAFVAXIdtgOoWobKc2px5GgUYq83IWaLulae9qvxdtDSyEV4j9tRFLmdlZQkfd3EdeqoZKLdmrDxmh1HcoiyhBfoQmKSR6UN5lzqxwZMoNFkU8rCpSQb/hRuu0M3jGpB8aDEewMVedNlbBcZ6lPxKBnsnny8d3MtkKkLGJyS8a8xVzEiRb9Ce2vy4phtj6UeDPoBImj+xcWD7d0b3qB2aGyWW9y+lWZVxDWtlwW6cYu+kvkjqG5lO2n6ZPKm/Hba2Q4k4dkx9I4sFVXzle3Is/GppNhf5ttqKhk7BrYSLSmhkFa+EdqM57t4eu1GKHN9S7IMEy4DDFc1Ea7kLkSZQG4HZ2KOCTCWKb1NvDPgWRRmPhC6BRcO7p+0NY/0tNmtgFIn1X/1+RBa/4wTW1UVV17EIP4+KJow3u00rI1e/d5/reT+UYZ+1tbrnr+Dpc1Qmvw6dYD8/flWw7eJYOjvrnEqDOt5We9T9VJNMz/Do+t542zChiJLFR6RN20tFU+WVW/SD9TGSFMt0NpQU5GJ56l22/rOsvwyJMQxcv3Ot/S1+hkgK3qsMCH0kszHjjItDevto9kO5XrlROqrXQIsxwqokwEP/56ncgoSqigXoUIizLimfD/4p+7g+piiSHV3yFIZ7lGRAg0f/pINFxJNnR2Avxxedj6/hPrzt58jZPjUkIEMLn7paYjJggugeVmkWD1xNMaK0ztIOgv3fCnmOR6ehKixNOfbJPQMCckJVu1lgMUeoG5WSyQGYqP8YeTVaVbFuO5Rl1WLFRVrPwSbYv5O7gFdRPDJBCf0WJkKMUjYuAYbVcEgTRzcw+HzNgTlr6RGdAL4LnZE/dr1bKJ1eZqUYD5/KEx/3ZdW0B4MRvkwpGcAY3QmXJIOeY3UAI4zDrKUbmVpZe8f4vW0ckcTw39qdR6kjAjVbgH3AQIDAQXCMEIM6ggCyLxDOO6nlA4YTaS18PHszJsZm0eKlsfAVyC+lEn/PyFx0uTnI7amibaDLGQAaMqcvIkI2l97vakZfhbzeLw4HC88f65cEnJxApRbnPj/Kxqs9CNoY0xPFesI2BlioAiImRH80qtc3QwycOhCXl0y06MuvCXRNXHZq4KsuYBhyYOFRTOtCpjfFaxapiMToC2LLWz5ovMHzCG0rqi4i23dGovmxlhxV0ofpE37+E2njcR6gPRqlQ7f9APUOFS/ufPKqzyzHUVlzD3/2hmz+oKFaF1jtDnlLM9yQnd6N9VuV6FX56kul7lt2887bcW/WI7GSbQOay1xZ5NMIUuTw8=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif