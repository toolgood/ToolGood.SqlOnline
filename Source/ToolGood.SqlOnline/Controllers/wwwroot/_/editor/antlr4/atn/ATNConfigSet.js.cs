#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/ATNConfigSet.js")]
        public IActionResult __editor_antlr4_atn_ATNConfigSet_js()
        {
            if (SetResponseHeaders("565588583B54A3221020364FFA83DC8B") == false) { return StatusCode(304); }
            const string s = "G6kNAIzCce8Tg6W15RMhFFlNzaqVKBhudGb4VngdQqg+RGTjgH7E2VnQ/1Q2zc1kFjtODg6JfVCv8HMFwc2l0g8q5aUoL+utjj3Rmpb8ycXuGhOQ0paiFCDWh0ztfxDkG3tS1xFCyzKk0AqsaeFNQk1HVzWMNOF+aHpfIjjWsOtpAGS7dPWdWenj7e3/X7BMQoJBobWGmwlmugyglXJbNAa8QeqXjzVS+CpiFh/ECCBcx74/dJDMpThAHFARvMcpD6dMptiVMXw9hnob2j2FW2qN18f0BCnMzCso6GLO/fjOk8Rli0tE+W9XcZ1Psb2+DR2Ccpfq1Ykmq7ABMqReW0bhYFrl1Fsh1gBSANesnnC6b0yz++7Dler21joW9JN97o+St6yIB8RykgUHYu+DfCNL4h2kyD66EV+0o5bK9sQ2lTP/HsdWj22Hsme+WkFcdBZgP4tw8fRpjMbr1NqodA9ZsJHk+cNDKurbV5DiJoATbZXy6guMJ/M8UAIfdvSggXIsGw47Ymx/R1xWbLnSVhPTcePAUCB0hQH8hUCibeMG5JCYfbKBeHNUPwqle8/hidbELg1Ryr0vKpY4Arj4Yuervc+/DVpM6UEx9HUQXSEtQ0thxls4w+VSg4JnzWoFGsnc3i2anoI0f8ZSl054xXr6m8B5NlVF2XOQqiJRNcPoHDK22Y0ysd8uo9fG63oFJzWksvOhw6LrObwuiub1ikh8BsExusQw0L/fnsl9Jbd2ICfw/xOkXlgz5fnc9yT4rHYt8T3+hnxLR5VooxNEYnoijgpwt0cdDZSAI4U+LpxXiwlziWsdO0xBslThfsBE2jLcf0Pqc8ve3fPcGsZEHhw6DCuMzvCgLbBBOSCLfSdj6kbN4wHE///4poPRbVOgtZRUbxV2s1PWcO7tJ617WC1APAKU94ngSI/gUewtAZ74OGLb0hycbSA8i8mNjnYgVxjVDHyozBQSw7XD99WwlsHVAzkGa6M+Y1ksQMmSWZj7aYzcmo/15lqD4XHi+5zpxMyJJZIoObVYOPWgRe9LdU/hIQZvZczvOl8+CNwH4Pq1rh9zLVrjN6qesbjH/y3BWVopvtxOpwP4xZ22pHx1jdg4goH4+8dH7yU8Eisyn6r+zAyQvkQp1TaX4OsBS/GVPIGb7hzkjnjMkOhE8ji/RBT1J0z18J3VvdQYzxQ/YAlxu6SqJnXItmLAUaCqIi11U/Qo2Y0sgcaeXo12/dB09+Jh+vNkt2wrRiOi9CDRkpNkcTnIsZFioKYjHZBavKQYHqygAlV0T+9LvcXIDjjoxbKCQ6g0+j7DbYfODxBD9Z2GimqRQFEbtByjoAM=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif