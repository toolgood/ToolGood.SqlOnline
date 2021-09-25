#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/js/jquery.cookie.js")]
        public IActionResult __js_jquery_cookie_js()
        {
            if (SetResponseHeaders("D52A0477629F3366AEC2CFFE8CEFDD12") == false) { return StatusCode(304); }
            const string s = "G6kFAIzURnfiRmLIf3Sa+y0T/gOnZ77FtTQpP6ypYybMIHddEaa7KamWhs7fbxvRUiNSglVi4N071DSxnxBtRAnJIiNp7NiQLfUzGcsDK2vy+ewumTexQVTg+J8rxkf9oh+NjE2oadXe94dhFeAb1649uLZ/75pAt4hvwgCXEdtBOqEXvMrf3dAlPxnBnKLBxkiq+P/90iMFOTLkXbH7A64lLYz2gAjRfZSm/+e0+9gUL58qyWsuqpidZ+QwJAdMIhsVsWPQRHtQKWpQrxxggoqgfUT4SfE7kXgVSj539bp1UxFMZAcDIpuknfSbrfDW41U/RRCaYkCZCG63353PelMSnQ1gZ9dTQQYe0z1E5CFNVZCwrEKOHnnxCFxWLJeU79n02P+yEyGoid4A2L49e2lpEP8rma8MTAkB7G+qfsDP+lCSPPC4xlOuVZ5b4bQNEklFb2rlJS4RtO8WiZ2rRcHEH2CG2J8vs3zPvYTqGoPYLaAGrrlrHnlt/U4JirxK4PTHY5rOXPDXgMO+TsL2zJG/Ru61COU77sAws3daXMkJJ7etmKZqFNoMr38bRBM+47Xutv0///7uNalK/Stu/zdk4/zr55ftJ4Bl+6eUXg1gEuKtqsGu+Auymw+jMSPV6b7+L7iEYYi4QMHAGb7TJ9yQjJoCTviLPvCGD9RdLNypiaSplltIgghWryXfZSjmr+jcNfJZAsNU/KfQ6L9sQjBlxU4eeCXnvuhq3feVdVmWqA1HOZeu4uoWAk+yU8cFQlID8EGO/gIDkP4vBxGZcH2XVh24htXGFXG43dTh7yIgIn4mr773cqrkgAiJnfdocaCS17wN0wExCXnBnr34KnBJCHtEjCm/52sSXTCLxwjwzHwEMQI=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif