#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/layui/css/modules/laydate/default/laydate.css")]
        public IActionResult __layui_css_modules_laydate_default_laydate_css()
        {
            if (SetResponseHeaders("5DC3EB5E80E81012B4CEBEA9FB52C150") == false) { return StatusCode(304); }
            const string s = "GygdABwFbkzPp+KbItk0XEV7hCSdvql7p9XbL/iVQGp5GTsl7JCdsKuThXMqO4R7StLVjZQrgi045FCSdspO+dF+/yPeh2UiLeIhkyqzu3PtK2LWPL2ZM/c6th+1xk+IZSK1kBsB2RgNs35h0Ae69cmrLokDb8NcLIi9PT+yOrRodzFzuS7A3klJ/eFnp91QEckcl4WmkHI75Gg2z7ddERWw4D2Wgvy5/aEc9Av/feg3ILOy7LPuMBtPdI31hwEz/Q9Tkrt1Ju4PA5AbBT++hGRwvZueQr5ObyyDHkzchEaSFxdwmZIQwfSIRpORZ0nG9VhDOpo7aFop4CoB+57PnjU7i1owOucGDnkAG/4tUzvxK4MCLH32qpeH2iLG1IOzkaIQH56TjqmPt+irWi7e52c3t6C+bHFmD0LfjB4six/Zubn5eFpUPmqkXwtdTaU+/AckINl7l0YcrBwQwG18gCnvmtdoPxh7VADpI4HHS6K3nQidV8jwnf46qEqTb1VMIhIREiHEktcMXfTR3vzvqkTmdj9a5e5mLEEkvsDBwS2E4lDOilyatIpJIicRCO7SOUMj1dO0bikt6AE7lnjfV8g6hxKstkc0RkECG7GPs4zPtGOUNgLvQ7MPwnDFQlXVNEAnwTSh6mMuwT8WskGr+61UdvNScurjIbpf0w+pjdpSyIE07d6bFi2/m3SdC7ObOcm7DF/iD4LyNYwWSjTXrJg41ALQC2WeAPfS6/7E98b/Y52LCQWR822GKNQYb+YGW4ai0qZXahh+g1N5WH4+JkKiS1tna90XUj3CwKKY0G1iJAYUhiLKDzyZJxGVGwoE540l4qGN4pKZpLd1KBCRkrDqH4D2LnqVlIiFINJpOp7YaWlOYohe948tUxm9qRRVzzXsteRXFEBDPFGhNhBiRdmh24QNU7vHUCq2EJg4iWomkZqz0/Brs1mBpt+w/zv4sFx1/hTSZZiVGvhKkGIcHu6FYS1gtfCeDB3vx62idmJ1HczRpkp/5++OA3H5n6UQbKgw/oLZh6J5SCvE5EONNbeTgerT6gnEr6oguSbiRmIX1olFNuS8ATvOyYP9pWq1DWWd9AeAExtEuIKBeeByfq/tp9xJ/lKDeB4h5Gankcz5pWEPZwM04VKml57UHL2rgyYRnGRf13G0VCBowkL4eKPxPOQgIi25DVSOmtql0InI9W9UxC4TqOvkKlXPjhZTueLAxlE7a9izqYiOWIW/905sSpPlG0g1CKBggAXhCwPRcRqBaew5tcVxsxRoZPS2ib23q4tmTruxir5rOhWucQLAkbEEdO4s7o4aiUHrHgB2d+Z3xzOc+mO/82+E1V04jIbDjFWarBGXc7sqn7E5vJk5lsWBsq0DecdVafggBuphz3V27VEw0PZ8BMyZbxipBZjYrTyqgOSZsJZZQD2gY+oDvF82IKr+fODvvz/t7rVgG1StaN5j5mks2yg2GjuT5CgVASBvsXa3YF+M1Q+8oc0+HvalR2DykjYHS2xvEiF9MBGAqI3kMlbkshglILlj25min63vIwwYD82Ou8l5UQgWL0doalG0iQAAEQihIpD4av+bZ67JAVMgEIHDUyN90Mg2tcRNLJW2Y0E/WdaOBW1DHIfMdpELocUQZ1PwlAz8/2ys1Am5X3R1Q5kdR7T5vH+m9Qs+0DHdofmFLXPrPpRwFb3vtnQxiHN7ik8CHDfa7P2O0VNzUnkpQmzNmuZ7Haaw/XkBx+5RPB0xMXtOwySr5jn2SUzVnvgw1qagoCZDbPNGChXsYfQBeMMCYrztTlug1Xpj6KlvBFnAZikMsFjRWHQd6Y1SV8/1O9O7o4QRDGrVGcKZqfpQewbEnNi9oY4bLi9I/TmOnPb/K39jWn4nw7/ymKBT8ObRWknfWR/TvEKNeVqjFV54";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif