#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/lay/modules/laytpl.js")]
        public IActionResult __layui_lay_modules_laytpl_js()
        {
            if (SetResponseHeaders("520D6947890F56B81A171636ECEA675D") == false) { return StatusCode(304); }
            const string s = "GxQHAIzUUQ3r2fxnqjmt0rsCP3zjssXkX9mzZXNrgI5DLtdlhnFOafL+tN+siMVIpib4J6jYO3yqiT7xkkRDX8l7CJt2ETlQ/25I6+/3aG0BkTBsHeNbS4dfN0bXgYmU01wpKA6Bt0/vv45jOpOvwIG8//zrlfu1Z3hQ4WAA/EJkz0HOHzUyxY2ezmaX2ezfnMwjGHq66G7KnH49Yj6N1+tgnrSRjdb+sRqAzNqqlzeNWduF3+gADFDhsEyd4sFB2IAo/PpeP9Y3j9N17+e5Nxn0fs+No75nuJhB1v+BHExvC+GDAi9QKKGZqQDY75xnUIjklCtR7A+Cv+vvt2P6Gc7F/8H1bDKQEiKCPAvi/v5x9A/f6AjGG8wyTqJI/acCGkr4ohvZ2+UFPsgaFYxP3o5JfHEHe3yTg00to0ssXAnsMkDS2yIC+AQSextsL8Ivzq+SRLZG2WgsELZu3IAB4gZEA2jH7WnNJUkeq5hdzHV2vuK96bLzVit3cuSUo5v4PoMGJWjLPoVSIWFgWGyz2VI7u5gu4b5aW/NINbWZTb4nU5cl07F2+mrQksFgGG0NgVteRZnbDF5pNQ9oS0vZ7dBcwnw+F8IrPgquChfK1O//MyIzmrMx6qhrDkTKIxWcMtmDOZBRrt5cYQSqSNs4NAmlyb+zKBitXDzolqXoEhARv1CfUD+8CFBaad1nB5pcor+Qfjtbb4k6UgRFmEgTw7JmpJZaFVceaVozbWQsLLokCr/9f/3l7Ry3b0hz7snWsFRQJhkFhyFIv1Pu7fzdvdgO4pMkFZL+N/dS2HGlQxicgtmQtOe9BRrpeWQgUPcilDCkBOrRn+BXKuPXYBsFGJ+SIIq2xjXhywJ1fHzI7UeHG0ClkSjL+Hh7nw56OcaxAth0LBt+DsRT6hua4nVDJWfjrtSocEk9K/haBK15CyhD36325/btI+DnQaXLDBy0p+J2tnEDMbwQDBL+FEfaUGW+klWhaj31cwk3dIX4lQ/B0I7sAG6rkWquD7wbUQ==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif