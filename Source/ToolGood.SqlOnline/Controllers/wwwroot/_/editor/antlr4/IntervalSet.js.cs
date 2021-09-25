#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/IntervalSet.js")]
        public IActionResult __editor_antlr4_IntervalSet_js()
        {
            if (SetResponseHeaders("066E9C7A42A91763E3070F03E8366A73") == false) { return StatusCode(304); }
            const string s = "G+sPAIzSzPQxHaqqaoknv19NT9fEmccPfb0yzFKXY0TZ1tmYAJ/+YikLFWRxv/FVimBLxjwxadXtHc6lRbQTbkJOP1jxl+TytHF/BNSk7UO+IwAh3NSsnY3RXJeKwAH6i+IZhkeBCaLEeEjZJgH7jljIzMpSwQ7Ue58DoayciwUF+ACswnON2uWZSauaRF8dKszRXzhRReZyC48RotTALHzGWRmsHSCTdU/jhZvHJjpoz8UMpcuvOtBKn8hJHK/e9vCxDmMq+tpEU47qxIAFjYXg3zOnZ7IagdJ3K/j7hYXlFMnrmHiYEjGCEvyMVyIogVpucbiTWMO3UKqOl7l9+vbL/v7XVlbYzBA2KAvi4tPhZPJMwAnKQlrJ7bFDEdam9TQ6iUiQp7bplzuFlo4lZBVZIeb2bZiVK22oKOWIzxe30sBQD7Hv//a+40xJDsspMcCx/SnlyqK7DnUBjNXOE6m74McVrBgHJRzwWqLO9JWrosdI1RLJo3/Sjax84XC2cEDIaBMUvYofAt319/WvKtJAhUGkZg83WiwkCRF5duVkyThn2kbSoDohdqL3sAArilVqWad7G6yOmAkaN2NAKGOpRdG7isgnE08cA38y8OlMa6UQ0k0Cat8+oLPL4qEkA2dLf6muPhbKWWKg6R6IbFmMfMqrBoRVIWntoBYpPPzjLg7+hX+IPAe6DzT9drAMSpzWMWQHwj+ClcPrKIlbkg+KEN+XylGzXWwUvTJuEsz5N6XTXE5uS18ycng4Sxw5tZb2dOsFDBjV0GHypkK82wBbiRiH6xHzNyxUgalDe3TN7qpEoBEQdCp3rsnGQmgHwyavhFUSMRLNz4RcNOopS2oR3TLB1tAGiOS+UeaQqJXmVBF6NkPZl52z7DYUyJS2ei5ESSdSGqmwwcE30enNo6eMkQOZyGuNKXmO0y1uJdkdKxUW1mOtyOjlRlsUjYezo3SlRyOyhmZxK2EZ4Z2w0XXZ8RyuG6XUCTegBdrcKxMBkdCwZgSaTr3IifB6LeGBuUUqUW6hXPw04qs3FXRaWfoiWuY6qg/AQB3eDuFwD2c1jcy1AomOfyYiYBEf+/+K5b0+NqG2WLWI/W1aL1TXTFap7OdRSLq9VYv4UgOcidxjIXk3GwNvPZiulSWLsvcfJ0AoRDfKx7DJMpfPV36+uaXP3dCdVciuG5sVzRZ8BRrg8sB5nAS4s9PRLLBTY/KBXVCqPJwz6eZlxz72oedvm/lj4BGkI1XstE9Xk6jm/2SrZojHZDIODMVjFTHloLXKPUSnn76GoaW1BgkKKXucsM2W7kBXuZaBKDT4JS4pwwa24f4I4p5RFsyBHMqhBIISp093mfFhwJTYW8e8vN8+PD+lsGue1LG03T3cc0lL/PWK+SCRem6G/rcf";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif