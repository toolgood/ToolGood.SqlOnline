#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/css/dbs.css")]
        public IActionResult __css_dbs_css()
        {
            if (SetResponseHeaders("B7900A3A2E5A94B225BB95A2C88E8FD2") == false) { return StatusCode(304); }
            const string s = "G8UNAIyUbuaka7asWj6isGdCFbE6Xp4sFyac2NsOkXa43y+Z1oiHoo1SP7e9ffHjJ6JYx9Puti/moWCp0yo5soxNn42wcvVoKM9MxWWu7PuLy3dsemSfb5f8btKF4Ze3h3z8W/yhvuTUadoVkKQGRVDw5YNKfwrIeBKdy7sn8Dmct9qCgBjJfGRv+E2eCxcuCWlcUtnvQQMq9zuvfWRXw40mthSnASvcLCSED0I/LatCEuUsovrO8yDTtXdiKmq9zrmGEksbi1xXSlaYjUHanPAd69QHiqSHea5jAF6+KY0Eh08PxxnkgNUh4WM6UrovTF4QSAopy2ghtATgBbOUdoAsDGTi48MxMyBdRwwU+J85nsOjVgqncmGGRSCY4pFtVAyI1bFxRCErTrv4w37h2Fl+xE4KeTM1InyRp+1c2tWAsw3y8/PMaGtg39LVSAVXLToYckZ+abPCnmKYeyVLvj9UoXKVCxDuDk0n3ZwPUyx9gFTvm1FWzyAp0sH3AJpuOAQPiQDR4+9OXc5O2UCj1bWsKz1dSv7UKgq0PlgW4Dr9wqfFN8kqXSEwgixhhuDegy241n8z3YPh9Iepr2b8i2ak8/LttktDOgUT8B7KQf1AECRFmThsCqyqIgI3AhdFFrQpmo04IgHMJzIADmqO/7OCu/tcCzIH72X+zBruUfqhi6tZocqTBL/IP2xRNDZMqYaTDHqAp0RTvHOPCZO4MDfgVPlC6id3dxFEUgfmrp17HINaveSk84S1Iglf5ORBWtN96DY1InvSn86p1SxBfpHDZWyS91KxUs3ZJmWpekkEVlrX8BIiHW2UqUO2qduPjgY+ZPDwdBU7NqJIJfkX4pAnxkwsWYghJ/WJCObPk1+fKFNaCspmxCKYsfaTcUcZJtc5Z+7jq/h0TbtM4152PwvqgIjXDkAM5muWdpxNhw53NvzgMuGnc8vWcAXJanbgwMvklRm7xUYUMOmRenungLhZEU1GQ6/bcU24aiIcnM19hydgOJp3ojQrxixcz/Q/YDFXYwNYdbOqblbVzCqrGxSrsWLB91kZLIHOM3kNM5Vv9ZXDqCjQdxkkUDb5CsCVRka8";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif