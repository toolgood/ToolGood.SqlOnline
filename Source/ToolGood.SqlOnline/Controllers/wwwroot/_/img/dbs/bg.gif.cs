#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/img/dbs/bg.gif")]
        public IActionResult __img_dbs_bg_gif()
        {
            if (SetResponseHeaders("B2F2C77EFEEE77FD32C6542CA8A52107") == false) { return StatusCode(304); }
            const string s = "izeDR0lGODlh/QCcAsQAAP//////AKbe5cfR5cDAwIfG6aKxzqCgpJmZzGG2DFqyAVmyAP9/AFuZzG+MxYaGhoCAAG1tbQCAgFtbmShbzP8MDP8BAf8AACgoWwwM/wEB/wAA/////wAAAAAAAAAAACH5BAEHABwALAAAAAD9AJwCAAX/ICeOZGmeaKqubOu+cCzPdG2X0a3vfO//wKBwSCwaj8hYLslsOp/QqHRKrdKW1qx2yx1Rvt+ueExOYVONdLrMbrtN4PB7TneeUYdRnrPn1/+ATH15BxCEB32BiosudycHCJGFEAGGlIaMmZo0gwGelZ+Jm6OLOaYcp0udn6yipK+wkJIQtLW0BLC5f44mg7q/wCK+wcR0vCWIycXLzM3OVMfP0tPU1ULR1tna29wm2N3g4eLj5OXm5+jp6uvs7e7v8PHy8/T19vf4+fr7/P3+/wADChxIsKDBgwgTKlzIsKHDhxAjHvwmsaLFIhQvaty4IyNHanEofBxFUc2akctC/6LURPHBCJcricHkMDNmoJaRENS0mWvmTp7GUAlNJcIn0F8Pcv48uskoU1hOn5J6gKEqBqlYs2rdyrWr169gw4odS7as2bNo06pdy7at27dw48qdS7eu3bt48+rdy7ev37+ASXkMTLjO4MKI2xxOPEQlYyUsTDZ4DMQx5UYsol7eoXmzCpyRlnqe0Xn0CaJEab40rSNpaNafM6+GXaM0bREtrV69zRvI4t7Ae/wOTtzG8OLIkytfzry58+fQo0ufTr269evYs2vfzr279+/gw4sfT748w5Do5WgpwL69+/ZPJMifT3/+OwqS84vcUkCA//8AClBAfAAUaOCBAEhwX/96lmXx3oPsEYggggq6M8EICGA4wm5aGCDCBSByAOIFIjhAIH0Jyldghe1cKEKGL27IhYcfjkBiiSceKMGKCb4zQU5AAslhFgYMYOQFRg6ApJEmOrGjijwm2KOFGsYowpBW0MiBkiIOMEKTTey44pNQssiOixzAmKaMW2jJJZJf5iilmCv6GOSdWFZRZJJK8jkAmEyIOZ+UBpq5DppqqpknFW6KuGWcTk5IoY9VrnnljCN4KYKmHACaBJ2STtmibqRaNaMDqKaqKqrx1eeqoebFKuustNZq66245qrrrrz26uuvwAYr7LDEFmvsscgmq+yy7RzHbFvOasdAb85O+5z/Ywxkm+1tJZlEgrXONQgubRQNMwxzvhRyiCublZvTJJUUEi+6erBCiSfsUobaUMLUy0oo6L5riy24sFauHtWdG1y5yeT77FnRPpxWxBJDXPHFGGes8cYcd+zxxyCHLPLIJJds8skop6wyPxSvHFPL1o0LW7XQYautzKN1q8a3NcfBM28tzTadbdxmplR1RJPLb2pJM+eaTsgFXRTSQi+cmW4uPwVz1h9tzfVGXn8t9thkl2322WinrfbabLft9ttwxy333HTX/UsCCuSt994KJNCcAqEaqMDfBi5w4AKGD84c4Ig33niBii8HOACGV35g5Mox7rjjAGCenOacG945/+EFim756IuXHjrkhJ9+uufITU757LTDXpzsodpOHN588+233cAHL/zwxBdv/PHIJ6/88t1loMHz0EevQQbNaRB4gRpUj+AGCGbPnPUbhC9++NhrbyD33Bvo/XLgow8A+eV/f6D76a+vnPXvj58+APYnh//+++Of+fIXQAHKT3wFQqAB2Xe9BSrHedKTHvWYR8EKWvCCGMygBjfIwQ568IMgDGE5KmCBEprwhBaoQHMs0EALrBAAIwKRgS4AABcyh4U0HBEMd2jD5eAQhjLMYQ1fmMMg8nCFMYzhEW+4Qx0KsYfK+aETl+jDJsqwQDSEYnJwqEQdahE5LLzeF4tDQu0UolCFIkyjGtfIxja68Y1wjKMc50jHOtrxjnjMox73yMc++vGPgAykIAdJyEIa8pCITKQiF8nIRjrykZCMpCQnSclKWvKSmMykJjfJyU568pOgDKUoR0nKUprylKhMpSpXycpWuvKVsIylLGdJy1ra8pa4zKUud8nLXvryl8AMpjCHScxiGvOYyEymMpfJzGY685nQjKY0p0nNalrzmtjMpja3yc1uevOb4AynOMdJznKa85zoTKc618nOdrrznfCMpzznSc962vOe+MynPvfJz376858ADahAB0rQghr0oAhNqEIXytCGOnQOIQAAOwM=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "image/gif");
        }
    }
}
#endif