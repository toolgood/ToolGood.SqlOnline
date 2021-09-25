#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/snippets/javascript.js")]
        public IActionResult __editor_ace_snippets_javascript_js()
        {
            if (SetResponseHeaders("D3BA235B84A1A03FC72F805396440E02") == false) { return StatusCode(304); }
            const string s = "G4kPAOSypb75/HYUTawlICF6i1K59/HzJimHcOmk2Fy9ZPnV2qeFsEykSn3rtvv19NLtJ7jjDRD0zIYOUV2EQxcdIeN8hIyNof7/Yd4aqeIJiohJu69UjZOSNAV7v6XmoT92gC8UxVWW7JikMyxhbRk9AXqWXSa8/UFUSZPU6BuJRI84WP3sAoq7NKS24Bj9OA0R99rUCjd/P0hh7kjucyMy3v0dFiX3twz7xXUlvRAQPA1R9K+8bZS6u3yaMvy7Fwoazi4+WbkgwOjDwY7BrLYmBqWmgo2KQmreoqDbWUxpfzG5vfsLSogSieb4JMz6/yQSeCKqUXVKECFaxZt9usj9e+JPA/WO8VDPeXomM/zb++trg5xeX81jZi0CGOW2WL10yyZTty3yq9vjznXiVd4kErb7eI4GN7TfEjajZTIRy5eiPbbowM+yfhkZFfubU7EKzCFbhm2zqe/AL/Qd9WySmRTZvmllRzQjKeVuOS/36txyKudMFYjvLW5a+mr6t9OsX91AEwXUFGIIFckyBRYUZDGAAa3IakR0N8XQuDw6AA5NV3Z80ujdog0WHRwLqUkSfeb6y0Ikm4u8kCO78ym92OU5m6uQBKpMSYwp9HIdxtCPKIzfIkiMPI8LKSWC7wOKn/4RVmiqqs22p1UK5XVumf4FRCTR2odl46nobeEQPMbt9iCl5MBANdtD6LWHUDv/UI6hobC8uOHVBnyT6Tr4K7vE7y7xdPVyVZlOKY1nvZaYsH1ujtEVVnFSgiyRyJ52a1vo/lsC5eQNolrNy2lJKk5Ut+xqF8tV9ViR+EKr7Ec7JEanq7OKmH3plouVN+HhvVsWGqJVl9568i/r1Ldqb6amBG36X2eDGuRj+cEPCDu9bjHkOoT++81hD+JFP7l63aN+JzdPlo4tDQJGf0OeTQUjheXF2Eglse9ViLPUETd6CsEbH6P1RJ70n5yL3Ab6pNxpeKHWxrn9+Rbyfr7j+lLzn/lRzR//DEfgVXRKi94CFJAJ+p43Do0JQxXHf7HWhpgb6wRkIYVX7GBzfXusJi1iHZ4iilW5tRXeaGl/WdSgbDtXTT0yDc/tECRk/WYL6BVny0BV59slCBHKmJ7d50vslhoY8nuT42LSucNe/TkvU/NyKmrkmL77l/vsrDSMgEaVT3K+8vShHKnqcGk7MLkWdjRhr+SD3QVx7WB+YDeZYqnGkDFdLT/N8NQc90KhNnBGlyx8UnTInemZr6Q7B+CvrdQ12i3aI9VQKsnr4uJikQztye351rnI9odJmMPA9as0v0KqOHkEzZsY1aHVqaxxZ1HMFzI5LY8So+xxZk8Id1gc86+4FijLsbNbnuR8HjyEJZKmLIFrHG1Yv0ErhUWLsXurrSVRfr9LVfw3fZbkQSkbEweVrJ4p19XbGc/c26WRQ6baN3U3MxNA2gRUlsCY3UnxXoQxpZiqnUQ8ua62PicaAYfaN4M107h/WVIGnfOC4GSaFmAVLXG+I+pfFjaSsascDkqsLvpXZAVwDQ9ZOKwvLnqnmOPd8KzAHoh9yrLlbfHFYUpPjrMUeVmPqQ5KSEFzOPmI1V2mKUfscWGSZVWo1TqDJFM2bXe5FHq5mH1kRgm32Do7MIInmoUVWvaKkjGRUupZQJqCpfUCU4awc8BKtKQYScdGuXG4GWjzpylgK8+AVr8uvyskKqKVa8QVJakrptwkjbwjn+yE9kjAYhsDzYEd8Kp2n/489yr6vtvRhvfpsmTX+bN7fp4ozeG9S8Pkfr2QVGUVAm7NAQ==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif