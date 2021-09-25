#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/InputStream.js")]
        public IActionResult __editor_antlr4_InputStream_js()
        {
            if (SetResponseHeaders("C04D0E582F9A413004718F20E99B25EC") == false) { return StatusCode(304); }
            const string s = "G5gFQIzUWS04lm2ablqDadZZr7wpTRb0LWM2WhVFzzmC7jJ6+ul+6lin+n7Hqqyo+m4RQRU08Nlwi3Xe4G8RqOlgptuv0WzxVdGxsj5Eo0tmcenRCMErp+dn/h7Xmg7zIXe0It9/6Cc8DDW69OzFaOYViXm1yw6otMq3q9SBRwfypDP+UUBPokU21tdX1/fyzgrn31b3L54S1JP36bqK1WSTVoGNLgfxrBERpZnBYTb6PEKMHzKMhBIN3t2ArTQzQ6yhRZtUif//z2XUTz8hwPlmZMslbmIx0PivrDCASr3cFX0fXn8iX5+N6RKuu+LaAwCB02lA2JMRI1KI0v8PncsoVAwM7v0c8O/x3ndSEKI+GQLrt0YRTNvOGYDg3IgpUJpCwhmAmeDrbxf69TnIYVcf4mlmV1j5wJ9efMR8Ih+F/HF0UmCcA0kyl5nJAQPBdlkmIj2oUtyzlX5pKRoqmRLnVAgGrcv5lVXQvf9/FSp9z+mknSaIoUOnes7eaYqaT5sctGHgYawPFmsP6lSi8hECigGB6QCvdV/RC+MWyVTsa83oR0obbzqGGI1xEnsf7/0piMwJoMm0462o+z431/HSUvSGly4DGgTlk0AIHAWtJif+b/erSgioYqxFnDVzmElmS55GEIPRF13E5HirtiKSBYg/+uSdoaesLz2lDpfxwOQRq/HkKNATNsk3g9PFAA==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif