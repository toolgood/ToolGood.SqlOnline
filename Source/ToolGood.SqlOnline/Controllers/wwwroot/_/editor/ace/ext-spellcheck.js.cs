#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/ace/ext-spellcheck.js")]
        public IActionResult __editor_ace_ext_spellcheck_js()
        {
            if (SetResponseHeaders("C5B270880822B5B86F9C9E612875D58B") == false) { return StatusCode(304); }
            const string s = "G2UFAGRV1eu12UUiDCPntEDk6vH0HVtG2CFDq1tXYwu1kGkNajpbRCWS3KPJm0biT/8kGturhNf2RnVIJ+Qen4OExI1wLezCd90tbxw1Ig98DYGMA0My4gOT6gVmy77b004l/MZCCicWrQm45QD8BSNX+oNWwlBqtucKPspPm0YEEYFlxNlXCSfpAwrGIDWjJ5Z8uapRAom4JEpOtOenYZ2mv0HiwI1XbwtWifeoWFnPczbaeIsy0l4QHXn1yGmJj+ou5UBoSG9VdMXMtRTJ6xcXiW5ErGchfVwJrh3J4980/+5fBZmK4hnq+Kyp4Q1ciM7PMSjcfLvp/R3gpssGBtgEzpKmbm3fkiXU/smYXvtScXPw5ezNRMTncuz+NLC/SrzqOLHJ48vLSLoaQ8bM8BZ+xiNnTOLQ8Iw7u9V1DKf+xzP5VUfXidPLV8IXSGgdbLEFo2ojEEcA/I+9a6CktbEjVXVE5h3iGhhxtCAwtp75ep3DnAtyl/GFxPfzHjreng3/ubd7DlvIMPOmpXVPqjLplIABRO+vBwP4MsdGPJvR1OTNTZQhjL6r88hGDEDd2vJO1LvP0AHcRUkuv9j8PwBAO6EcqolJzOIy/Azcc/yH/JPzXwl1TgUI833q0QrE9ryJjGIdx+jjgQWXNqjgAJqlHyuBwe2lmWtA9pmQBG9nQLw8j6536sTVVeLowJWtwut7v4tCB6g9Zo4upgoOPQ7n53+CL+y7Mnr02YZ7h5CFIvVXDxI=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif