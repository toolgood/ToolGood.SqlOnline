#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/ATNConfig.js")]
        public IActionResult __editor_antlr4_atn_ATNConfig_js()
        {
            if (SetResponseHeaders("C42ACCC75CBE4AA5E420C7BB3886740E") == false) { return StatusCode(304); }
            const string s = "G5cMIMRUNac11iCQeELxrzKYUspUuh/gJ6ED5dJls5Y6Y9aQCTLCnqfgI+leKa3B3vR7l9I6DWEOC6AoEMaGsOVnTrgVaZXyJz7JBeIYLGI2UtlV1T9fX5ep6H87tfl+XB5eUXbDkE85Epv3sKd1+qZRECOV5Oebdx3D8Sf6AvtNW5bCmUz/R7HUS7t6sa3Wu/mWHl6h/tmlqp/aZX4xexXKDaVTfYnWR+O6qE5UMI80mh3LmctqbcXS4eiP9/7UH8SS2e3bddX522ttpizGoyYK7jTALszW4DpkwAxx/SQvZTEP0rQs0LT4GhrVOUp7RlHAKlIDJJ3WosAgLFEFK60zYwlXCpY0bARayrGALo9WS6f9mTsN01YkcM3gu6jg+ua7BIJYrV8qH5lxgViQrFAU6bX/6csqXI8XoX0opnag064PWKMvnWRCONJQ7FpO7xI23Mq3rUeDb5e+f/7d6WGTtmWMCePOk5nQOOdmOluaTMdMI9x8Cmlx9eLoksTo5FSbSRfEgIecK+2xwIl4XkqN+GnNJez1I3DaLelQ5ymWpS2/UUUvDtJEhugi1IVaiRol3nGMhDoc9aLBugHDL/I4PEp8tBPKwzSod7qett2DRc0ScBt2KpHr1vuN8gM0jMB22vFzg/P3kbKkIMlIPZt/wn0Tdctpct9HdyD8bFVVLDdtxrbu3xhmC748DsJ7GJ0xDo8gVIQiVn8HtAMZ54baRHdfMsc4Q8VCnlQJjwDH15dn5GZqB/1bQznswJec97CJXdMknh2yHaxHMJS/otNdVQYWZrLq0ww8eXwXKEzG1z8JQqGOixOCKao/yBUq+dT2fyCehJuQQ+DKvrwjCaZpuSMF4da0pbBMtGmpXQ7y+a73IUD+ju/fhbGDSOp19Z7cmEOssA4wUk8RzD81FEWHYLLjHHQsyXQHedI4EpW1D1arPb8YMabAm6iBnhUjjfJYy7zEuI+2uV0jN6wrYvRauwKiyYQSGmC8mBt0CAaez1SlkJUFKOdqBNuBPC/WGlqWzGJEclzH2J0K3P7Vi6MRdqmTaw+EZni1AA==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif