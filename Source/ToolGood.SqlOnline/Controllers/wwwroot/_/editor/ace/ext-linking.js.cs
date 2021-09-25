#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/ext-linking.js")]
        public IActionResult __editor_ace_ext_linking_js()
        {
            if (SetResponseHeaders("77E458CFECBB77023E98A7B31D4C7A90") == false) { return StatusCode(304); }
            const string s = "GwYEAMRUU6f5OQgkcKDt9R90SZkq9tKplM5Q6Ro4py5/1QS52SqCjvu5qAIHdD1QqcAuCDsSqstr1znRmeLFzL9zJmY14ZjmMCYQDXGPGqRJ6IeaLTqnMEzoFFO7wi9I+ZuI6J5fEK/u7UX71lGTn7eem+trf9rzH+bP/bVTJBiWY/5dXEfgdq/IFWjmDnl+2L9Ncmi8fiEUjUamVIrQdxIjTwF7opR23mAt/HCv5/58n0xPDhm8cdSSZLJTwp+2MAMs2mtsfcSc5cSiZafJOoZHfgwG2Q4L6VgPYm8AbKE1pcSAAhyLX9u8lIB9thytc24VzmGuzwrJ+2tYoQm40fskAK1hUmWtUHS3Rpra0DwmC9fSOj7lpiLggJhS+hMiGqil8e/DcwSJ8MyX1X1E8SYWXU9bGoLGpNIjpLJDuBcZu39Qq3u3Vtfs+NSensOi8wDewbVA9xPCSEvcXT5O18UK8+j1N0oYIzPSODGV0pKW17WY6j4qhWsU7cx7kV40WAA=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif