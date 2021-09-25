#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/ext-rtl.js")]
        public IActionResult __editor_ace_ext_rtl_js()
        {
            if (SetResponseHeaders("53EB20551C2F1B9D2C870867A0C0A7D6") == false) { return StatusCode(304); }
            const string s = "G1cJAGRTTafVDzfiBOGllQzsPkWTzlvppAMXMkfB7vxpP7dBXsJjE6uUSmj/vbubqTaVhFoi49myx+qLeF4DzypqcrCf/fY+LsgYEhieXcdPW1d7AToH7BxAwD+oaXQ6IP8UJban9HPCJUH6ssv4A29L3Fis6r1BgCWuLpORvlTF+DFaSchpkbniCMbEKOCtO33QhUfvU7rM9jJpjk65lg/c2VFG1S0iQzWcC4tPsApNMdmTobiORLP/3rAPBTKDPA3RXs9KYq5+pNgxBAWDuirQhqyPPjKdLsVIZ8R9UCOdjF5bpT35isakIj+hEbERS1rpqM9M6/ZglyXrx2KFwfJfuSRnkeNEPuS/AoCygY7ydY43ZM7MUpapajj6vDGMDZehGjQ5hYdJom8uH6IehlIG6jKLJWOjPrNA8uXwZePQgYg2o+/9eyOmUl2V0NOh/rHH/HA+1HWo3Pm/2o3I9/9u66G5AVci0Dw+T6c7O075odzzVlqS82CDLr326SIW0WZV3rDZda9KbbPoTYy3sh558h9LUsbqO4uSGL840YWPj6iBPn3LrPBDHfz/hPDGqdTyvavJaQhHwCSzcpBxptcGlfIauiPFQ/vOuAxwSG7XGoNI/t8lohzwcmof9knoQ9Vrq3yfBsBGImkCTnTJkAZqTQo1bVUrb/I3IJNSH7QRUcZgeDF5uma1E4nHG+X8R8K1HoTfflZmfNEuf+Rv/PI/H5o9HFt96Q9frH8maWY/Ab3e3jvRHFK7DzeKn7GpYccQOadalmizqPD1sjOtRmykzPHZmAm/GhqBR5jADUeaYtosbl7Beb6h/JKKZAfMWt+GfHpGHlyY+legMWFXvVyusGUyEtgL70+1lyeiruF/lmhel/cNu4QvkcNX1TuP3pAuF6lYzoNAqU3UxeqT2EbZfCnnS2DCa1OjSBzOed4qKk4Edj7hIqIHaKOUqq2nVG0tpWpzKVVUVVfvxyiJEtkHMSRKbTuW/d9VsDWq9kJp0jYWhpktXZySAzMgOGWnwQHjpCC67SB8UxL35WI+0geV1YDe0ayteF9s2JCuUOM1HPYnjfacN/Wuq7AbxHRjF4GvcGjY2XFkV4JqBuqfOy13lZK9SbMq1DoE";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif