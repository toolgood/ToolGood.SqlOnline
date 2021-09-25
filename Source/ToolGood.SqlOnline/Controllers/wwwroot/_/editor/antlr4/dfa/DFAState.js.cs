#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/dfa/DFAState.js")]
        public IActionResult __editor_antlr4_dfa_DFAState_js()
        {
            if (SetResponseHeaders("FE242264F9C48B611B37315F2BD5CB24") == false) { return StatusCode(304); }
            const string s = "G/cDQBQhSaffq9NpdS4F6elMq91foBFoApb7FPh2kivQ/51qCXNU7//M3Wl70JrYhvoOrHjY4lbNK1BDYpuNGqMI6unQXXruU+xTcnMnOaoNoUCdoaeZwXQZGpbxFaz2v5KQfyAAvs9mGxuOyGuvJpoyMH5mS28O4KATA/BE31urDklY9ZU6SoK0mgdKu7wuMKph6QlMgEo2NItfcblX5cnoT/ulHcn36vEqzp60ztrZk3Ws/TMhXEKx5qQoC6NPxnLv6mSyKMZlNPGFIElN4+G1pdlKvL7Dis/l8aWDFZfR2lSEzSqybyWq6NiqOAkQy0E0KymfZhbEy39LQz96i7Y3s8to4BpG4Fan1NSmzjndGIUpre/6pSOqllvtMncZDMEntD264Z1a8fT66hIcORNMU2oAqHZsJzQGE/LyaLWvod0kAPv7s4d8zL7LuLEBCk1EzkSW72GzN2Uv5Ak5Vhl4FaSRsMk3sp54wOx7GATbogRtJD/SdhiaaA0DWyleCZkMouR1PI6HsFnvmAIWmbsav4JX/uAdRf/fjrKl4vB4L1dsCtWzcnSWMVoC";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif