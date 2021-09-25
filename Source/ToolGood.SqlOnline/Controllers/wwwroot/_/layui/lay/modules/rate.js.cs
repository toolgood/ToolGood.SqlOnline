#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/lay/modules/rate.js")]
        public IActionResult __layui_lay_modules_rate_js()
        {
            if (SetResponseHeaders("CF3015453F74EDC69E2DEC10037D4924") == false) { return StatusCode(304); }
            const string s = "G/8KAIyUqZnTrarl/svk52CbL56+dOl4/jVta9UwaBwBRUZkOc2nYRtTtjjGgcvdgLcMOMg+tJ+rAw+FWCaxj3c2uc/8DhO9P7mh3mgkQiR71hbUxmD5y8QAPEJW8cbTk5Oj6uG55iCuCv3PRW3qs1T/ht/iRcMfdxD9xELBMwDacX52cd/MUse4/8+zR88vm09Oj1hQXHQLnzhdAWn+cBtxhWWWas5Jhwwe6XTmiXRCNyVe7dDl1kJrtZxeVkdGbf93zLlcuTOaJTtB4J1Uxo27elrteZrz/6MGRLCOeopIVxdaz0hR3k6o4NQUQcUvYQPsG0lo5G2l0MCmTUUe1JI5SmGqdK2zZBusE1Z7rqLtogIIZ0mTF7lcCgnDsoeAAoNCctNUOcKLWLv3JboJlmCvjwmueB2gedo2qimB99DH+z4btsdRtqBUU3ezMYnagTemlaya9vqfspHAQosQ4TzkGiWN7pqiotMdn1NgITJwBN3wPLVY6owSwEUHwI4JMXpAedKRNeoaOFouqa4JR8XwEUmZxnR+1TQeGgdASkITzbT345TaaCXIF85EF1K0WmV2sWxpqeu2uZI1UmZh3T14zEySOVigPhwm4x08x+wgGuY4m6Mbtva6G2G7hMTb70Qk5SbZrPfVhqoxYznCqgPqVXAAaGAHJHzuZbuKn/BiDE7cbIOypN8yrr3t4z4FSUfJTr3J3egWNI0qLYOBnwchos80Bt05H0Vkux1sV3XCMDV2anR0V8fd01116HVqpqcen2Doq14CMINnHuyadG0AItMYfYYN/HEBBRPMGt6Bn9lvOv2e598NyX1gwQT0+3dnH66c/Z8FtQl9Gov3Z17EuVX00bKxuIuxQE8UhjRIft6q65oU0EhfslgtAzSLjdqwjH3oXaP7cEh2VzLGLE+Q5jwhc6zVKGflNRyO43MCoGJyjFpbIegkYf1dmnSBAv6iTpBj+Eba2ELoxE/dKrjLdLYycDLnNHCugBOD7Y/CpxAtoglax6BMB/JMysWzROxennm9uaD//R/fQxtkzH7ZspxeJAP6MLyx11h86WJdqx1JR3HrQrAHLRFUXVN8VC52rysrI2UiHMu8Oo6hC1QK15UJRzk4YjU1NmNBYvKPXcgtmGAA2XlZSvYb8Q2kr+jtwd77FYR4mTyLAZGLQfqzM8X13pt3y3oFAqjcH7+utxGD91/XyVdV8gGepz8MS7zSCjW3+qql+BTwbllibpTvlbaxTneGGq4fi4/tWWfCgg==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif