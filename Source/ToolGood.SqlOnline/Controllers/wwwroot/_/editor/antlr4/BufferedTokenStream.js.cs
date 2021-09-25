#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/BufferedTokenStream.js")]
        public IActionResult __editor_antlr4_BufferedTokenStream_js()
        {
            if (SetResponseHeaders("01D8252EC3CFA1BE7540473A9D4A30D2") == false) { return StatusCode(304); }
            const string s = "G5UOAIzCtuXzU/VRh1Ijxjgy01lVy1F4pLHsgRZ54XMVYnchwmdzKb25JirdF08iyvJMzlcVJtRkAmfuISRQsb/2a9VSIsWTUK41eG//7qkg4k1s394wyx4mmqhitTA0hgSxYRZt4naajYL3pyJDMsmCQpOh/8QKEufR9N1ABsRYBAxZwTCeinhz7Qrd303Y5VK4NXu0qdfTz3p2+71wTLz/iCcKWNbQ8K/BOxFTf3maIDnvrp96ieNp0mP+arKiN69M/JYKpcfHDyDZf0mGlqmjOGvI69r2dok4Bngl7C1optt/TksEpDWRCXVTWiA8oJfLyTg7JgzbBDjhvyEWBozdVFWccRUdz3gMr/HDYUVnYqXaR+sSd6mqJW6u0GW/Gv4+GRNE+hNEZykaIvq1U4ms4r1W5dBDS5YnXrvLi7272LXIQWNmH95fymIAa3/7dPH6891G4iv4rH56kkR/itOR1SFOqf57sjmbKQjKZUj2uB7ilAnzLNH1vN3mAkX7ijKrBEDmq6hYUGLNxSFMmIVuphiSUFPQBlke/6hGuyQgUwQqrNWvhhIKCz8YApWsllQyFcnOAAokgWBW05vlYCMYwAkk9bzdBDYn30xxxF7oKzJrBZKkNru0xLIhAZjlZmMk4aWeYWGw8x/B2ocUBICUZ4/bI0SZpu5UYwQQxF9haUTtAG1r9YSGAmqxQ/clQEBT7tMOQ2fKz+ccXiUseyuzmY5hWfB/xw0Oy9zuLxU0cv9+K1ZGFXN8JYS32cVg1d/1JBEh5TH1K7V9nxkNqNsJAHpUfYsbos+1V3QybJha0KVkdDKhk26uylJMmsZVx5XcMvRJI+ghdVomTi2YglTaRDNLUQu4ZYFwyKpQhgET6PJI5zjNYGewGrV3vZUpUHhgO2qrDpRoTtiExBMDNrUkIDUeHtQcwwMwmYcyMwDOpEzhI5RYB8n8Jp82Y4BeiSqP0SLXPhi18jH8c5RHYOlRF5ZaYvx2E2KJzMyapQ7vlaODNWOf5chsWux9R5pyUwxGypxFC+aGVDE0bmqQLT/XbVjaNFk/EDEx74fVDlS/3FRxOEp0TAE/GKh9w5/UHhgOPWqwgJClJLAU+syVr56WqvIstiy/29nGvbSEFGO14u0xw56j/f9XoHHTxI5K+UHsnHXoAkpdzka3RhpLKKFc2kgRczqnjBL4SK5iBXvwb991RPVQNle3VjfqUeZ9YaTeEp0OEaVmhpOqaC6KZEFfJnLOBwK7MgA+xhwMZ+YJ7tCpImfKilGFZiENGKb5IzB8kYVTxGWrsxK4e36vTxPKU9n5TG/9w2ymELHeJU0mAQHw1hd/SU1S8hVoNW2TVEgihVD52XOxYzsXzFyBr2HxgobIgQhsYulYjWgA82ggTcWxVA+QDDRBxi0dGXcc0xxobPtjcbhhtQ0EVeKpKu4a7nJ60KrfCuBi8qN3oRP/+puA+1xtoqmX9UzT2GfhRhEB";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif