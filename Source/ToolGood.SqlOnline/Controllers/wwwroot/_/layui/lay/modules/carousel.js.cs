#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/lay/modules/carousel.js")]
        public IActionResult __layui_lay_modules_carousel_js()
        {
            if (SetResponseHeaders("DD90DFCC172C1EA4EBF8A9A08F14BC4A") == false) { return StatusCode(304); }
            const string s = "GzMPAByF6caeReSFZam6Ga759qq6e5mkpwawuXSaoOa0LVtnJPKIOxpMPvHtPmxjyha3sfGHZwiCI2i0Ws0iSHA55X9B+seaYSk7LcKUXK5l5bcsTJWu/aJwnboandtsLk+QEnCSz5dIEdhaALCyNsbPX7ZRcKD+eLFt0GFJDmYPks/+vxqax6iFDq3+dz0Hbh0Vv02POHAhaz/g3+Tvhy3THta2/MObjwfrgbyoLouDqJA+ZYQBqJeCEvxNxhVxameTB905THGhErBmmd2ZZvRqTsyjFZLI2ld38syqW8rnV6bWy7ghuogLyWyUCy9CCNGvooYV1HYoz4zNGa/IEZVmXpNg6tHR6OkpGEChV5m1mc9RCa2AYF0gCAJ5cn+zQCOoyycvnnzkEcSyFtnolTsDK8gh54nyiWxz3oGwkGXOA4AgIed45yaE8R70y4tR2wMXKsZBvkXdVdi/lhphjpnj0Y6Dad4ILga76EeMq3ByvANWm8r5MTmNlpDKUvDfRs1jKCFMESBoAGUSoPQI1fF5efo9pTG9LYFMMqIlZDQKhGVreZQvllup3MqmNjPZf3JDVrRDeytqEMwG6oiuYU3kNuE37g138IZ7eBq/NPFwdXRi5jI5ctpUX0Oz1ofsV+8u8uMVFrm2ulrGjJYOodEDIX3bvEAVU0bPA66kQxL0wwfQQXzDbKJPb4gFtfCu6BwTBoxa8do+CysMxH8qnY17IIB5cna44QSvNzMlKm1/lVNWk3sHptChSrZ9/xfyAgUe8kkRgiYGBXpHPbYXsdCHGt8I3Wrd9fDkhettGxj05lgHoaR0NIFWujZnJkqK0vxYTfkMLSb816RWrNY+uFNqAsBt5egEfMKTlow+TSzTRR03Y+zl5dgOY0w5YLbklJsGXTo0iRfhFH+k55uro7RWUgqrOw4u7SlgiaeZBMQ91msCVKr5MSyWdstiitxrb0JKBodLtA7JCHwTbjuZI9TSp00LkfWPUN1DD2PwJo3pBjMZ1UwXRjduk5RkdZCIIP4cHOiHd6wXD+3F6mSUxozlZhiPqD4fJnFkdZOlVohmv3d65rj2fHnrU84BWNY2QB84U8MWtiLuQoE0/mssOcRwYj3iAAVjvZnZFNihGI1qED0SIkr7PiaI5QXL2gLZebpX4DCS67K4mclENGp7tipTa/afaZ9uUF/iUE/y59f5KlEbLtAGlKspKVCzc9UpX+yrBsV46hbx7SZBtTFTOSnBGne9zTpo3XCEdmTKwDUSBz+H2esd4zg9gHncIgYD89DnudfDrGpYh5psMf4ZdhjIKRT7GcSLE8khicZBIQj47f21gNiwQVJ2xwCig2c/WkYsjiDxRcIOI3aof+bAFiVULmoJeZVYcw6VsWy99gdS6sdp+RxX6FPWkiLzfUq3I0rH/jKV5lqgrxbOzBpR/mDnoBNyV3Q06D9JDHt5IRNRGnV36hR0JzkT6/Wc1bGL3bPw1jCLe98HmdjxvDVxOF9KZ+oc4kq2YIa8jSmjWWxqF29oSDFDunuVPCSPhqVaGkg7SObH4bT1FuhkXkaiUSz2G4SzDOdoKxO/OEIPxOi6+PARsE5H2qTgJntuJdvA4E2ZyrLPZnZNLdCyqaGw/RVnjQm9fVRH8L7TeJBf/AJ6VEvVGcQPRY12tlUVbFDLborC0QGFO7PY2ooHipyHOzM/UuKuaxC0nuAL1t7PnIxjl0Ft1nEE";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif