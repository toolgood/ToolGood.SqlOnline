#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/LexerAction.js")]
        public IActionResult __editor_antlr4_atn_LexerAction_js()
        {
            if (SetResponseHeaders("AB2A38CB0558E5402A362A0ED2F2CE3F") == false) { return StatusCode(304); }
            const string s = "Gx4PABwHdoz4jRgfQlMaL1mlW9WC38Wmz87iVjQRTD6YudlK+VP4lpxOimxwwDbYvKBV9m1vtqrFDfOGE+oLFWNyb1lIK0XGcqSQXoWLzmM41wkI5t8+AsDRE/3+58WTk4cKi+RwVKw5nnN0cnt9AFF4dNgcP89HtKZaitHzRIDhzTrPi0XyVYrwE8i21OgUNyBmDlcBWyaNnnJvEFNjklBDM/JFlGJyPCmqkUTuQjDPbhYGz8TbH1a2jlp+3c+koNYJDw0jGRqZO3xT9SZuNvzyiyVmS2uWbjsvuTm4CGtEQs4iP+Jj+5UEx4GhRQLNmKfQ03kGRvGBhSF+5MA+zHNhC+d5sGBHfJw8h6QRm+itm0rK1e4eb/l82/NLcCDOUQ10EPflpM798Toe6584oVNKwMUNnACukjFFBR7b5St2zM4yf55wSrqfnof7Z3DE/SUFDdHLWDEX9lkfz2oIfbnBGdKlBNBgQSUubR+sRJlnuDeD0p0p7gDcSvA7AMyBfvA+oGw2I/E+EQzHHg7LafzTHdhHZpVCmaDCYGfW/6R9do3UfEkyNqH/YGc084f0VaJigjTQ16+/L/hfvap2bs319DKmLWKPFDtILYgyQoM5MrVACb8g6LyKEZxNTMKQzhZIrMpswRJ/YZjnYobnjubSWPDs0VwaC5Hkl0GYS1rb8qehjBHFQq4/WlkLK9nfLVZjB6L9r+6HwwWSdNWrTOXV9TOpktEsOjb0ChgdeVwoqOph4mMwtzsYrZdMscEMBhfDqi6TQfXMekyKvVrm09COYY+B+9Sqs1tvovmZh9xsoSRVpFCxJnmYyikonlezeSk4rkA9o763VNRrx91q6xvbXJgGY+h7bLB8LS9VTp/gO6vP/XEQT7BTXslnaxh3gwQLw8LhPF4uwAy1MTSX5VmDlU+mBgxMQnFCmPTQJIWBcd4b";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif