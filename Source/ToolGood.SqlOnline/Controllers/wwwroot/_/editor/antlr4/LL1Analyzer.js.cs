#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/LL1Analyzer.js")]
        public IActionResult __editor_antlr4_LL1Analyzer_js()
        {
            if (SetResponseHeaders("D01DFD685AB8C5277C0A9931DE2DC975") == false) { return StatusCode(304); }
            const string s = "G4YIACwKbHBNcScmntvWfTTJWwlRvpNZadOqZY1uvHOx25c3AFAZWW+44Ecmbao0VdRVGtgZx0h+dyEqZ9xkOchEjrB+EXKaEnR+uV/OeUkFpSvcvU1ywKhKuPlVyA5wPHlg7VgIWR3ivHU3o0pqFzZ3GOgUb1KkmUEdi4wJh1gih5/rH8eHYb7ZoQ9QNdNOjmZGklomer0BfhresXhw8v643EUk6PsNCEaVkFjqjRpF2Rn4hPVWyL6SPJR9BxksSHcRbVOoaSY1JfeMuH1s53A5GPxq+wUpOKQevrd9UXJezedE/Ynt/jYwgzAEPbm4SAwUGie6hNAY2JSkEMigsQMhfBvRLazDLY9G3g1Fr2/eW/96cU7Tm8fhaXa3BoTZrutIo3UdDUwyDgJwVHD5mRLRIPG9neEAFFvA0BvJcsknWRnv7wqcr31m1LTtaNPmDThrA8ByUc2hCL83r6HBOPjeYujroQkbtytJ8e7WbHjUHKt6iGYaN7zLgHRmIOKShmIzuWJDV4GeLv0eQrK83dSYijJYKiLO2RDs+ILa6AkghIcDUkUBQPERD3633AEn//9BrDon2yCVj7685eAyIBlNZ3IoChwECCQfoa78dAhQOBZpqY+AhApGQ8Aba5AiMJVx/y9cZmbAMrfslbmSk64J6u8EOwTJUAwnqsEsYOi7L5RaMJGC3mra6fcpQwUguV77a+SHCzbx9JORN714fru5f3pE0Z3MZnSymV57xzyd69pIPF1eofz4//jf5cOt7BM2uW0y4YSlSKxjd5a7V2vLf6P3UkkKYYHr1XohXKv5Q3Sojoqlu/wujZCMS8TCohUHCYGOlFDWv0X4fG+gy+O+IkhwXCyEdut3iU9fqlAGFcmcW/KkuxVPCT0wEiqmjhSrIt8ptkLF1NXJ0J80MqZWrIFz2EWG5GuSY+g5Z74zy3Hv/VLXdkM1/i7XHtWKU+s2AkmAJ+pjBQn4YG7OD4syAnSWMEQ4sMq0jgu+ESw409JplqPvIz/4zp8lGq1kCGdnyrrmkCfzPLpjiGdkfuQyiQkP9qYPN4+tYQP3tIaQnArgAJJaZtH5qYrx2WSoZL0ZZ0MUkoNZKXLQOc9denPObCGa9hPl/BaDaF5FBGF89I28mYikFAYGtXB/Xxlk4fbfYE0D";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif