#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/frame/watermark.js")]
        public IActionResult __frame_watermark_js()
        {
            if (SetResponseHeaders("1C4433BEEFC742D0F7F900760861E7C0") == false) { return StatusCode(304); }
            const string s = "G0QKACwKY2PltMvDzT+7SSDdiOKftgVlOrrcujM7HX44HG5woB9bK2weB17zVpnm9napzmklT9OrUbqdz5gtl9u7iwQGInGyDH3YxmApZ9z8gQ519+6nlexH0vxWuDrVwCUjcMB8l4wo+2NWfVUDI6YqRacSnU8N7UFrJiHfr3d4swe/BdF2ZNmG7KYN1/2Ln3iOCYA88v45GQ/LOBfu+4Y+WeH3decfmzvJqI9dPDPh4vQJtLMsg1aE//1E86zjAc+9KLMERvMsQE3NOf3//TvWcCeHTDPOeGg0xDxnOex63XpvkurvXiVruZGNtDKmlWV+D8JlChGNeiBzNcOkj+HXEcSVD+BWUQdvJC976wPbmUvLt6gsqz525vLV67n98rhGIqorMXqWpjJLqShndTnltfgI012NrlRKbn/N1/s/MpSBnib4kb/szKKVjEDiG+swUPGpblYoCwpc00gwUFCFNNRJR+X4M90YQK515tJLmNNRZIRoeG9JhHA8dhZOh9e2XAdgo/Fre27Ob9QIvQTs9WygTb8mC6oH3S/HAbOWndcPJWWV5u1ROdYfBy5zUXbXDf7kqZ4LRHY6Jgn+lPBSFPzJY4BKcptuF4VRFFVQe9Ex8SX+xaT5XpVz/XHgth+ibGiYNs/tf6SNEGh9YBvyWbtjLXb4HnkjLQ1Tq3te4fche0RNcXA9otuzuJURQcwrqyOQUYvPQLDmR7atEbbXYgaT3aLQMGnNb1XKbzFMbdcezxGNEeJnpj6ZfFfF76ppZfAxi4YpcIA+UZ0poDRrBPV38HbtzuCvrg7gvdr/WKEPIwhR1AfzoxAFonO5nYZqAYfz9En0B9pU6rMWkhgDUcuuXyFU5WqDRji7K/8bSBxmYV0l1ruMnBa9mouLVnEbeRipJKarhJHmVk9C0lMWcaweOZIR30eVRGfIqhLWWlIK9SE4W2pJkcBqeA1V4rFV3XxauBgxFYGUIeh7Ogx0OppAh2PwiLObQ1gKQsKTa+XywEfq9N5nJUEzKTszI4r+0OsltdYnAiCAeL1Ee6cBgnUjzOFDckrUTlqTjSVIbGaYwsJa5myY6kGQg0U4Xqy/PRA=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif