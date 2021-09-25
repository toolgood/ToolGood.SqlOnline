#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/mode-sql.js")]
        public IActionResult __editor_ace_mode_sql_js()
        {
            if (SetResponseHeaders("192FB9D1B503C93D223AB89184619207") == false) { return StatusCode(304); }
            const string s = "G84HAByFcePz06K1ey2y7OOkRkgyC8+nW1X9rxCrmcmeeVGjgQuSsOIPi01Vup/55J7UJtvouXQlUAi7pezPnRjESYPtmML/v5/LE2s/rtSFpnExfLlv71ybKKYaIWkiQSVRLSci3sTHjLpkuSHMd7oC9JUDVaP/yBhIS2mjltP9dipsuChsUiDU3EEBhHqE/JuCDD6bTJ06twiy8ws9e9gZ0ZwN3hpZ1GnYflSrR1rn1M/QQy+waLC+P5O9J1Pwi2jctxTMA09F3AgyFtovsEmAxTCxj8q2uB1IjVNfwYphXUBcgH2Mag/eBLeELgiR1NHydURAy4BMcOfg7NzxoHd4Up0qA6EJbDHEKHw7jHrQdLj0G6w74A8KaYSbVnlXs4x7KGquIA9CsYx3Dwrui/IwGt0vOIrWU4/cGREQgkszV5d9xrI/zhxbydNy32xaJm1xLMJhFrDujOh+pXHVpPeNxnQ14/38G3uaBKHotTWmOYAoJ+kMC/tO1a75r3bs6KtNbwyvEyLIeZTaHIYsML4ICBZsXGNET/i6IGvlnRj2Y6vQBxCb5507/Fcti955hqRfSbQyKfmJTpumco9w1EriIIuS3wgXpoTiD0seK7grxPqdIH25KJWzOpvK+Fx/LuZjCilh4v8FT/f6kiOilU8ONpD2S9JhUIRp25x2M7WGMFbOrFYIL2ooMkkRlRZRaMlebPMNZbb/ZZ/I2AzDEXpNHk4b9OgR+E2flvNtNOn1G75vbNlWdy6H1/j2JF3hj/t6M8l8W6VQqCi/owxbiZFhSCxQMpz604yAsq1tW9dt+OiZ7F9o9WVFtbGV4lNRlpNFwKuwNJNYnGjf68MQj4b5Xw77kjxZwfW/UNzoIAMItP3mpr+hvWV2xUheCfJP9t4VqQ9L6nYKXS5ftDKC8bUFX+ja7C4xi3vRV5+w/t1L2A4+P+fb3EoIzjkjQOssGFp35Eb2tF7UYSbq2bsAP1iLBWr4ODNz//TOnJUMvgegQREq4fHbKlTMcWu/NcuiD/fh1S8rczDvLcl9+SbV45129PtwLN6pY+XiPItPpaHsrtcvTyM+6Zu3u7YO5gOqkjMOgxKGRxrCT83BvuWb3ZXEanxdHZ8lBA==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif