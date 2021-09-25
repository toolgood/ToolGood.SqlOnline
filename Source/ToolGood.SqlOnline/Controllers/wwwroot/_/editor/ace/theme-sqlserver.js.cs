#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/theme-sqlserver.js")]
        public IActionResult __editor_ace_theme_sqlserver_js()
        {
            if (SetResponseHeaders("3F6C31E1D4538475B1A18C7FE8E2D791") == false) { return StatusCode(304); }
            const string s = "G9cLACwK7IazEyJUJIRX/WmzWSOGILbZVq8v5zCaFF9xx67AmjdM2l4fWx5xb0kg3VsgQRSOBot2W4Kk0TWIGF2f+/Zr8Q7RdqERqXTRUGlESvjsGOISou98wTzh9R6RKGI2gnPEy9VN6LKX/0fGKP/c86uxDcnX8y654U+hUcvuTwH9WLrCZJ0FxHtiFzqyd9x/LoVS/oU9CHFdyWvBvmyTgvFHPgzm2OQiMRf0Nj9OWF6rYJzS1vR+u1kwrePLd9SaeNeWKYvaOCMnqljxFKERq8S+pGsEzRhSsqgwVT2VlWq7ypLCpYGm9ZC7fu8Nk5bPGMQvZUMf624XpdKanyl/z6GbNqHR0Lkj5oxcsSBgZ0JfUQhctXuJC3fvQl4qTiOm8i77pUwNJqgepp1X/2aWQa43kUpiQp0eUgVp479TmsMDEcl69oU6LFooMKyCi83EDw78mBw7oTrTXNQZZFFkoupMuzLt+LIM1W904hZ2CdLpAtRgN6guGsMDa9QpgJshWVPKQavhCAG4SyaI84BdeNOi6CkZwGdsrleAdQHwJRkuhsHmeQpYTKZn0QuGVaAZFhewnPdAscXM+8g1HxmqhlcEaXXj1S5OWAaSfFGKXRjgdgmUKXC57fasV61lqkJWYy2jvfY78UQQyaCCAXa+Glde9EQuIUhVzcrTlOS8kvfFEIEhIJq2MiDajjVhagK9ioVRjSDt9C8iYgdXkdYWMXCSYewkqJQjoOM+N/+UXoRoJFnT1gtW+9judhlBXU0m1tEg4DrFudT5t7U84nGKiKcXA4NlSRzcABAjXhgmbAgmOwQpFSKL1mvQZrOwIG1/CePIAGE1YmIiqBs8Vw2wtzt8TxSArr0UC1XQ426NAgFjIQ5WBdNO/2ZbjuBmU2GPOBaOUS4+9V/sc9cAMQMIz6/Ks+jN/MCm3wfwlHiw97KnBJaqgtUQHjBPpYhLlXY0T59rPoIWQNdfy6cDjWtffV0VGuXavSfUy/SYVTK0+sOgWDSbUswO5PPZqXFyRp3pwedW2BVz1K3uxx1VVv73rzqN3VuWcz97RtRD+Ykop/FurtYbasXLtkYpTRJ6JRgC7u/Fd/w/f/aFkcN+mgRrh7j4li5ndOev1ysoBLMuEo4nuTnhgpxB1gSdd2TnRxsvOyu6QgaRdVeSeAuRzb+/ngS+rT2Gd8xV7LJGCtY23GoDqw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif