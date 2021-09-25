#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/dfa/DFA.js")]
        public IActionResult __editor_antlr4_dfa_DFA_js()
        {
            if (SetResponseHeaders("A75FC57410F67EED4E17051D05237266") == false) { return StatusCode(304); }
            const string s = "Gz4GQGTTptPqd9LWvtPTmq8KNAFPHJBTF5Q7WQn7/793x79QZlvhvtnYCxf9NaNFbY0kDTXAGDMxzsmvOrqmk3j2B3YMwW9jcAERlZDb3583Gg9YagyVFQv7oRJyMpOyiGGW/BRyBZmWRraN1CBa8Ik/4/x74jFdhCEWLbAx6TezYOb7+1zFdF+MEjjqt/XzTWetBJErqCODIvPoO6qw4Uv4+l6M6XmkCOu5ok1khohd1aJPpYSyGMclyrBBSfFMpiRY6YfOVlX9ovEvWV89kYLmDeqLq1M6cGSRfZhX3hwq16fPt+VgE5e7+OH7hpEa/tFRr8kTVNWeSOvxBMRC0CAmicO/uCHhg6ecDFT49ZpWJMmdjitwPVVTUssooUBRBCu82lGYquH1+3FA6HonRZbYXFydJvM/XF+17Brm44EL46lcWZ8l4sNmt+MjtBQxMx3D48cIcA/y5q/8vttt3FmWyNK/gq6xGJ6O1cuofsNpa6paZ9EsEDO1+yJDYzwhJsleb5d0HD8JIRFJXvLZJVW/D9UNOefOpyUrT8O+wTylBopgJdi2sJ2SDk7sfJ5KzDM5e+0tZ6I0EdoLPe8eY13y1+rgvjzBWw+HBp6Lq5ERYK/UuFe2HxtRdWDVZGRYjkFRw1RnCVwnPcGCtQ5OiMSsE2BQXjORjLcjj3uzJg94Y5iYwAJxQoFMh3LkWfLI";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif