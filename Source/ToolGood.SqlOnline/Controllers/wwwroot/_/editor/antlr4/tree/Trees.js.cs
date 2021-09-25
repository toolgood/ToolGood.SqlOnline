#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/tree/Trees.js")]
        public IActionResult __editor_antlr4_tree_Trees_js()
        {
            if (SetResponseHeaders("9337502268036132F52BBEC1DD20BD89") == false) { return StatusCode(304); }
            const string s = "G8YGAIzCtu2xqNWp2ilPVjqtajGF5m50I6BFrEPpT6FyiNK/PggHUWF0OpXeHO7W0KwGklLE/X/uW+kKGFgC4QZUZr3btm4Uvf9bF4w6WZo1OIOjNjiAc0ZF3H7Ua5OsWMHtDK2i6IfMR4ls/RpdUxhTfKbwd81ZREMQlsRtDl/IcBQ/5ByVgpzox+TYtXOEJ2FgyLaQMcy3wTpi11wXC9Z/QcR1Nqdfk5QUkRFw6+vhXYrF7OOJMzx/elfIjs2ByMfm2xPO2TKNhZLrQxlOkEWyePoW0pIylp6Ey+3WVnKL7lhqBIyCSrGEOgMFBHFNswDRfLmUcQrWIr1EiAQDNERtoY4/IcipWBpJBo4GTdBFiypQMur/ACzaCyVyyrXXeXalx6VS5JGVNdKVoo8t/8cJZw5tPOwSgVkGlZ2m7WjNCvSOeRXXvqEw0jPnPNgcs+JUl1CQGnlFqSKCCvBaMArwXlKD3Iv76h7DauuzLJwITWlnZn+pOX9JITNCy5OwKHUItPvASuf11deasNVqVu5aVjbXKTg7Vioap2BNo03URnIZRHqHVCpe6mX3ewfzZOMpLIsn3yJbak6WCFsR2+L9NGPce5ofJRYrMUrrptA0Ya01By/JGPJkrlEvzbyfxYO7pvZyHdNGasDQVzkKy+n3VjEEbbDF4goat8zCbbMnM/k0x0wQXoegw43mLLwEaGcGH5Hv5ZSsBM5piQudaoZ8RJRp2dpVbd2M4SdCsts9NmOwvZXkvlMqQCg6CSO8OZEJ4KKUq6KH5PrIOHEaA66YiM+C4PVGabth/UtbNA+MM0vNSaXc8LN9Ntk00BxpLJVpNZYphoACoUqBd7kUg8/6lkIRQh1CBA==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif