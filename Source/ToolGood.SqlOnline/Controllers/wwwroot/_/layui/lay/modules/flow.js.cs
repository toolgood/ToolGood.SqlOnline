#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/lay/modules/flow.js")]
        public IActionResult __layui_lay_modules_flow_js()
        {
            if (SetResponseHeaders("48E35EA8229286DB4720F37B69BF3ABC") == false) { return StatusCode(304); }
            const string s = "G/4HABwJdszJYJw6jLDoUlV3L5OI5KB/8wUxpRF8KnVLNm+pME2GUAHMpgoM25a2RScHzN+3IMAoCLCk9VUzSfN/LPXqeYmc8L1T+X3VYb1oS4qi/VHRhW3ePPECmZNi+qp5zkmgJlvN8vr6avYa8t7/F1tzmqwSzu4/g9+e/6Qc+FWFwKzqX9fvp5XIBph4Pr2Zg73C1ex6eVUHBsYN4pXTXYK6XgSHF/Gx2aXD2PtGoaF2jFxw30Oyg+JF6nle2Neah/luWBhnd7OTvEXvRzPWVkgWYoPa+hJRygfH+rlaF1+Wtpxrq9DSKdRXcQdhn2yeAX8LnTijhgaqaKI91dRq24BjF8J6/sQjOFVv6x3SpBmotP4+BcOr/ZDuCsHxn0Q6sFO72xCeZXTLwg5vptEL62Zu5ZRFKU8y2nBD7e5CEBKBNYX3a03QlqvfJaUllDC3toQMi6KyY106ENfiL74s7TxBlkeYxwDGC3rdsJCIoQEyW4tynppUFF+WpjzSc2f3pUA9/cQcewlVEdLLYAjT2cRlsKj3WFuon+kcXgnUVnEEnp7Tc8+crZIs16C47leWokXYYG4bCxiB9n6DeuBeN5z9vlbKFjDqOoiubzjJdGFBTy772JziaIhZlAsIKe01kKYn2te5G7CWuHcg2L+XIDIHHY9ncVVDCE0IpfVRZ6GHn/tozie7+ykltOzV1mKDSwgZ8ki6IxUzu/spaLsq3zVHREpuUcj8Xt/3BucZceUmGzbfuPH3gBoqKeW8HyqkZJBy+vARjwZEKaFiWUDVF3rertz/WEcQ81EXzWi205QLnuvQPuIPgOfGXHjpCXK+ukrlXaqnhV30BR+kBChrMdKjLENIKQa7Y2QyAXXwTtILm+RAXvMX6c5GLVsFFfKLIgSxFTX5VrVRbhr1ZJX1Qo4q9mlud72a0UutBvdf3eFBNU52L6zpFqmPgHm2L6BBtyV7KduCKynBKTOOPYihrwSGAAPv4ltzWqQYSG5hCK45xE5o5B5OhAFP1R00qGsO0oDXJtZ4M/snPX7Ovko5wYi6SdOIiJEmUCTTH99yvYIpBIuhtJbBbt6JTbolTwPtiTWgs3oO5l6HLTjaor68/gON78Fzpn2Rnz378lE+TdE2MIz7ZI+0d5L6g8ioGJ5qYaAtUsOe9uUJz+t1cRC2Cdh3fLxYMu+lPXe13jwKVW0TqGItOYz0LCNOceAki6OomaJ2IKTaJegzGMcsRgQ=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif