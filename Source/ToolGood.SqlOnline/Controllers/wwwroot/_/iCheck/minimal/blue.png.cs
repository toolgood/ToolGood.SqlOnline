#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/iCheck/minimal/blue.png")]
        public IActionResult __iCheck_minimal_blue_png()
        {
            if (SetResponseHeaders("4A709F8CF673F2B25537F8547CC6DB07") == false) { return StatusCode(304); }
            const string s = "izWCiVBORw0KGgoAAAANSUhEUgAAAMgAAAAUCAYAAADIpHLKAAAEM0lEQVR4Xu2bwWsTTRiHu1Vyk4948Fb9KBR6Vk8iCEkF79WKiKBStYJWRNCTqL0pHjQVIiXQg6j4ff+AWhWCehHxmtBCDioKggbFi4WwPlmGtzTsZHY6i7trd+Fht+zMw8u7+XWS7MZbXFz0B2LYdtbeeUogPke8t5PbY/ONjIwE9bXb7Vh8xWIxfp9mW1paKrE7CCXYqgTv4QX8B89Drke/Avr6vl8/KD4z5vrovZWPnvb10StHn7l/n4/uCPYblcMDl80Xc/w+CV8YXMwoF9A3vRgtGx67T1P3KLsqbIEa3IKWEgzDPqjAFzgNTUMBkXz/XHoU+Oit+FzqY1zgIyhNQx8i+RgX+Oi7yefUPwlITvogHHvU6nAV5lgBOz2CRhfGVVQ4XsE41DXKVT7AJ/9gxEc4xMfxOOfrcdXH8Tjj6ppwrPKxSoT6GCc+jscZ5+rr279B+Cs3Lq6f5vpk1dCvHP/DYYJRlXCE04E7cEjNGQ1Rig+qoPURiA6Ijz6OutTXPQfiU3N7eyE+glEFra97DsTHXEufuX/pDkgeDo/dPMwQjGcW2gW4pubikE18ENlHSMRHPz3X+hgrvsCx0gvxEYzIPsaKD4fBZ9+/1AUkD4dQhk1QXYP+rppbdvPlvqwGxE9pOGRlcDmvmICavG2xowM1mOj1gbWPVUR8pvrofQFuw1f4po4LPauI1idvgyxgTmQfvS/AbfgK39RxQde/LK8gvikcaQyJhMNMCZ44lPAYSgn4bsA0bIaiOp7Jan1Zf4vlm8LBfz8vBSGRvy3uewxBy6GAFgwl4DsSIjiR3fqy/xnETzYc5pDYhUP45XhztODuy30ZD4iQunBYrBw6PsGwQwHb4GMCvnshglpW68tqQDyNIOlwyMrgcl7xBvY5lDEGrxPwXYQKtBUVuJzV+rJ8J91L6cohITCsHCbuw03uFcyu4ZusDTAJF3p9MAsdy/tJ4jPVR/+X2Z1T6O7xaH30bNb2myzmRPbhDq0Ph6Z/2X6L5RnCkXhI7MMhPIWfMAW22yk1d8HVl/uy/xnEM4Yj+ZDI3uKOs8/uGFxhFSlbTN0LV+F4zworPihbrB7io8e+a32MFV/gWOmT+FgRIvsYKz4cOp9z/yQgWV9J0hwSW9RTrwfgASGZgn7XaRDOwEM1pxGiFB9MAT5tMAZBfISj4VJf9xyIj7mNkD6Jj5BMgdbXPQfiY66DT9+/TH0GUb8LWVcQkrp6YnYeznI8p25itaAA/8IYnIQfsNvwuHsdxAeBjzBofYSjGWd9Kli6FbdOMMTHsdHHnFh9/R539yGW7U/7uDBxPxKSgE+/khCMXWr53w/TMATL8AFewnlYiNjzJkTyEQ4/zvoYa/QRkia9jORjbFSfU/+6vygciOkXhYmsHDR+vf9uxOl6EIT13D75Fkv3i0LP93VBzMnJt98eaizLhOEn0QAAAABJRU5ErkJgggM=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/png");
        }
    }
}
#endif