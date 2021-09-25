#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/error/Errors.js")]
        public IActionResult __editor_antlr4_error_Errors_js()
        {
            if (SetResponseHeaders("145E1D87020344A13C0EE55296EA310F") == false) { return StatusCode(304); }
            const string s = "G08JAIzCtm3zc15iqaotdqaqOa3/KQIPJmhkFXNJ30rt8hcmXa41U/1dH3wtJ1gciKb53+/X6qb90xliWAmZFt65j7si0nF9hpg3k+lkIjGhIm64rJ1Ulv8wcGaYJQn88UG+NjyRG/TcgD6hUr4Ru7m+fd2TJgZNlcm3cQq2GmzMEHPIdImIdMsqzcFJZYFKzLW4ktjkp0fMkRggXoG5TME9t/HN7Cio0T08KBCtxWL0bBMRDaPO5TuTZb7PGBemJEvTKm7qjSOddNWoLxOUam1naZX0WtRf1lt5JnRRz1GJqpm6iuVidFQorIi2X0nqQHhrY7XcPWvVeWP24O/fFLr4W+7/f9jlbXx72evNx/twcujoGwRumthJEo3K6A8rd7xwsR7cCpdgF1xSstiBdjuuqpA+HBf+CmERAaUivRxckaVNH0ohEtSyS0GQJFVLCUQj5Fn9/2/YbUYZCX2zYJDpSUH67DqK4UnhNK68c64lrK7HxvgmHTDVu037uofezvu2wCDyq1W9Th7GryhRug4eBGK8L3ICG99o+12QroNUPZvcuWypR2Z8ckeapFMT6isusfo/MmGtWXsdpf1aNFqwrbIqlLbLoDVBDE4YwkqgZI41YE9bp/puN6Korcf7aHEKeUtjSqosHPBovXkSfMCcpBWzGB8TQBW9UN3dfYeyMDpfrnrUsaaoIpxcJZ6chaEGxZnwiQ59j83xphS+arZjELNu4WBKIo7hXOIr71tP4hph9RcAOmv7fT/a+A7YCW0vvsX37PEKTaleFYTTa6gjkXDQBXCGwwAawEmxxKmicQF2vkKem2u4Ov+Qz980pZmo82fH5K4o4bhof4OoajtpNkKZYjHaCGOvsKOdzwtgtr7Ocd+ZcvBAz03JkKDsPaLtfGwZUc95e7sub8w=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif