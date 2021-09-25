#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/js/download.js")]
        public IActionResult __js_download_js()
        {
            if (SetResponseHeaders("A24A5B6B16E8E883ECEB6350EE7C3336") == false) { return StatusCode(304); }
            const string s = "G7gJABQhyexlU3t9jR4abVgLUhrKhiulAzcDGMWyHckDkp0CV0vXi0JIUA6svP3bv7TWHZlLiCKoTFwUSFzp3eoKtZN42Rhzctkw58xeqRhyUKcZR/2kRQ+PloEOm4uI1eFgnJdPyPp2kV+toqjWhjPkMQkdfgPLO62SFUu8gMzKPoZS+glYqcbeEHnIEDbTCo8G+2JlHQthueY0ZDH4O5IiU1glgB2Z4rDCgSxRMBq9u+E6zY+zI1ySKYaKiNHvrLMTltfeNdmiI4n757e3/wfHoz0thZFbtxUAGKg3FfeipxQ12W4/+2msv8gwujcHUNr1wit/FVQce9CFr6iXfQALW4WfeuHFBZrPc6LQFdI8nXUTTMOo2DIjZLRg9Cphxs40kEcmlWkZsqiZyjVhxR0ITGDU9LLPV8NmBMljkNiFYmWVUlaVEHJA36BSVVHSC7YxPMZEYM9CdvsuelBQROWuhpYwcZVL47BCBxOgMFfoQmsNKLwNH/WyLQ1AtYC3Eu0HoTwRRSnnLYp28ue3D2uBnVt3mkdmCkH6LJjw6DSlS8/OAKNUGS0SodKZUCaiXMQZlE6RZ++LIZcH32td96ZMEnzlNJQZBSUwWZf8PzofseZ84FxYYuXdLLZYhr+fEEW5L5NyXsZVDAnWAKjnQQvjiGRJwV6YAYC4LCcQ0Tu3tKX9OXTbLCh7OkgpZ94FFgLIJ4z0iDBK+xbnZgHWTEhWiUl6snQS9LUmc0ZDqWsSFMu3qwEVec6Jo1fGCjZhFetrlBqpuZPHx8cwNMrOfV+CNTFMLXCt7/w8lrv/D5fUj2ONgTy2FBnOo57zgJoUq9QMcE+6hHRGp/grFiZ01MlnG9eMYyc/uofCgt9d8974d2SPOzLI40FswAY3smp35BRMl7t8SLF1kDWnxI4AM51A5L6TPjlQEXBZ4ZJCkWJ/mHNYAbact2gYvTpKTy5s145ay+tIGKCmWLIvhzYgcCnbAobQZS1ERPNDADjZBlurHLKhYiPPMpajcNEWoxTGokUUcmok2mxcww6OUjFa9nkCCmkCWXMbgjusYcIR2SarT4hjSIF6vFTlxPMqLpMCqUwR5AWqSiitAcBXBvqMXx9mnp6fHRzKy+8Ozl1EtIdACxGArDIJ/r5xrNX4BVKAQwbd7aCRwKO6eQW5aJV3B9mWaWsENMooyruLdA5hrSvoNWoxV76nFNeWC/s4CXhD62LBlk3fq/urm6l7WBf3s0LfvTGQGRtZ9y9LjBYDhQU0ip2euTxkdc8MdXG4FguE8IsBd36rgM8G2KZRjziclg2YQ2nS/4jOCbglNP5D9zgOcbTYXE98cfn+Xt9MWwEpgWXMlI9wObK4k413tRgARuhaWw+NEX8+JwualTVEgxAR6IY2G6tGXP3wuBW2kvppPphUE+tJwdPhRe3rQVRMXY5F6TQB";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif