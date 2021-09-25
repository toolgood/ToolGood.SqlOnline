#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/theme-eclipse.js")]
        public IActionResult __editor_ace_theme_eclipse_js()
        {
            if (SetResponseHeaders("14F16E26C217E4EE6C4815CD406F24DB") == false) { return StatusCode(304); }
            const string s = "G9AIAIzDdFs8F9llE8dD/+83/+uXrQ1k26tKymsksuSo06n8ppbwnBa6QfyNurr/z9/P1+F9tAlOZwU4pJOL7B+g0Y0f6GMFxSKeA5AyGH5XKZd/ZIzQqPXV2DJE+shJ2XS/mmasNd2mK0LGQDNNd693kpARkafNH1ePf+t/rnCZZYQXVsdUxTuNoQmfj79A2KRHucSqhNGCx/9grLVUyFhYWrKhMHs5opv5qIWpClgfakNlfD2yhdea2Bqes7Zqdck2o9hR5dQgeqb5NDIXjPWMFUZOB6oNsuF5Vb7PzxMRk77Hths6mX8kUcZLKWTUEQ+3jrtWiLAvZXSrjEUanofdUKlQT00L9LEms2E4VRk6vCRzp+KRPdFCUvKJ9TMOeO6tMjbJpGD2biYJ5fmUKkHz2VEdCP0dODym+TNEzvVpC6t16Mb+NH2OpuBA7P8P2ej1xRTsIUdqhURprp3nVy5M7aZQlX9sJJ/z+x6Ynr++c5YmeDfjSRMLMRK/lEpGYRrKSwP0czb1YIimUCPCZRdfT2eK8lhqZB7qFb4oTmgwLFxDFjM3GTJN7Reg2JlSU6tdfJILM7IdsKt7pxBia30pjci8Ic5Bc3PfuBPGXELhzONCi7ftc7QsLNpBiSbW94Z5UBgzoPkAWMFwRusLW4UzbARD4jMhY5toHh8Ml8zblKXAovF6xl0X74HoYXiVTGOmO4fx8LlB8fM1EV/+NSky2GK0kqvLFr4e3j1OB1dnkB8MXG6fXoKTFziwTg7aR0cHH5Pjn8A+sjVPDv6Th9eHi/TtfvmO/ucvs+tkTsjh9CB9BTEk0+HwhvHj4eXjy8qj4ksA2NkZ1hE+SxMTeL5V93iwPHWq+8fNrQlViO/Q/vA8dl61tviXK1E7qqrDsgATnwnullVbWwIbChQiM+zTf9vZYkLPMqF/vpLr6s7OEJ1qzW53pYgds2rePmuZ6bi3VYfWtSUUAw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif