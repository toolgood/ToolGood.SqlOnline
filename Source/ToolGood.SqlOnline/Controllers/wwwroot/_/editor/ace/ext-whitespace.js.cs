#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/ext-whitespace.js")]
        public IActionResult __editor_ace_ext_whitespace_js()
        {
            if (SetResponseHeaders("8AA1FF3F92F541699A9407DF13D2F236") == false) { return StatusCode(304); }
            const string s = "G7QLABwFbtPzEd3EjZDWsW26CidEJNNpTnOZ2t9oZ5oy0QgLq8hE5pyv/rtcna6END+P1mdFWZWTKO0K7Zjx1xn4vqnvV3XUvPU9vrJdglZUBUPlFH8K+E9rrR4aIikTCZEWd9/8+ec2i7jM4eksHYlmFppbJZESj+Fc9i+YiLxC5Zvmj4zhDGNv1vzmTFXo1S6ZpCMtSQhGsyFdzQpoLcOnj8xXG+TAlp+BAv2Tn98aMb7Pe5Zl9dfTxHoIaVo9rYoM6jtuLJ9r3YyZEDZorYyGT4fE0bIX3SUmYK3Y+CE1ryG06ik0d4z/PifzuEy8xzAKzgx7KhkH3c1ErnJXvyhu/+hpDNX0L+N4tyRgqtjWiojU0z3E2jH4VMadv1RhZBLjxhppUiIZ9x3pNJyC4eOGYURx9otk+tLr/cfX7/O4v1znFgYx7FOJMFOIbPYsW6jXbRzp866F0Ngu3rmg2S/137+flYPy+Qi4XoZruPkyRSG42/A153Nbsk2OgL5eMSWcFJZsNXLr5qZ0RGUQtS8e+cIVWyAwpXdepcETEt3/iKs2CU2NEyMm1VS9YBYcK7Z47Wu//OHjAAGn2NC14+YeR0s3ynud4Pu09/g0ScVXXRkdYyF8hj41a50K3z/SUFPJbn61W5gKbz9A5YnmnatcxrOsu2k/VrT9MQo1wDrF/767tXX5v8exbYfXVdvZwAjF3z5uhgabaZtHl7q/RtFUN4rKVeSUI9CsSMKGj482a3bu+Bu88xDFFRTHCQumTxbIVH152keicbo7vhvNXT/x5elBaaJOQUgR2AQJEHxXeFyaM6yJFFDcwzXdG3d4Ze0IaCsHc/oRuFE9zlX7bWj7sTdmYyEtxb0mn9pNikKIpxLzq4h3fY0O0wjgwSHFHX198zshDhJhnE+IKvieSFW23lXCXSLpVI9L2x/zuzsup9kIZv+n3NYRxai+0GdaI09P5x/fOuk5yvteSsAK7WqqNEuNh7vJ1pfhJswkiqMqRVjotiB2E82VKf3At1Y9IvXh2n3A7tLepfgYF0GmcCe6Drc3bYhn9xC19xuFuu5XWESG+lrVu/kqBILXSfmhcw2XQTgM1SO0pQiho0i9p7m+AS5IRori4EwRigoT0oduOAcLXO2/x9xFFScU+ysiWD+DA7ZxkUx46W9tB8P+jW7N+sPGzZ2WhmX/F6wHglbgofUON9tBfFgtbqt/4aJGTmMEdk3Z5gxq6ui578R37Odm0OzhZDW1rQ1pX76dFWbnPsZ/j8Ot7IRLfwypzqHXXuFQaHOwBFedU0eaOZY2cGoIVvQUrSSA6x+sESfQwVV3ztKMw6VNNeZ70CGk3YFMR84uvW8dpuOCH3uFk0QzTrlaVI25B4KtsXyciloIrWAqMat/jzZ1ZAoBIdf4tSyD5O804qUELKW1Dico3nYptE2dgj/ml80hJpKvwB4h+e6HEQTYCFW0/BeWYCk6paf/T0q1dWCv2RJZyYCpOMoSUuPqFBbOz6D8buymizJxewuakly7mhPT9QxXl+PmSC+ANggpTG9sCGn1Wi7Js2NpM8R+3SjvXbzM2RfCh3hnOnPitTR4t4eIySy10wRL+xgb6S+xX7Huzm65N4291shCfkv/2BbL4xGDAHijBdRHLpRy3T3HFBfTIDdLybggpjl3QCZEwyQ3mzldPrDkQopStF42ErwiwglfCpZQzW3ooAA=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif