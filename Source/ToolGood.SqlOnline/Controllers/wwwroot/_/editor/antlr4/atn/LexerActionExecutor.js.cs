#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/LexerActionExecutor.js")]
        public IActionResult __editor_antlr4_atn_LexerActionExecutor_js()
        {
            if (SetResponseHeaders("5638F47A2D14D58FD630F98D27E7E3A7") == false) { return StatusCode(304); }
            const string s = "G/8EIIzCce8Tn9ZaxDYtsmRTZWH/h8sDlu8F22WrGlzHmJBNN+1dV3GSPh5IhDSZuJyW2AMnmNKPORv0l60xNFpUYJksplV5xYCLVHwj+L5EFRSJnMI3f80v71oE8EA23ne7dno3gggCCD17Chj8ZKKYa5Hy6D+AGoTmvyyzchJ210gtXpB/Tsd0plvluDFIDiEgT3NS57NARrmaq+zw7DYgAXkWkg5yOoRmzZ+B5KIL2lvzNqDQnuPhwN5kerUqrH+X4ZmzWb/ebAmNZeQZXV09gFa4x5huv4VZuXsIEiHw2kWhoUDZ1J2tr0/GIp5pd/D3E8bD3zouAiFNOvg4y6RUGTxWcGEpaoDm7mEEWBcUD9ww3UqyDTrpipW7LkBSBWD9Nm+GNX6uraQ0DorjubwR+3BY+cCUnAN3Lp+mQwMAilo5vFsuyOBvGtLs4qoo7xbhFjaJv4bkUZvC7GWZKINCNDruKzq4vEj/+SZ+tyyxmPOHgn9zgNgTmTQoU/qoo7k4v28kL/2Kv0jOc3SJVLqpq4FEkMCPgbM45kne0kMJK3oPmLTSaGBT90LeiEf9fq6DxAdy8Ibl2r1/lGmhGrByevazvNZLhwFbjW6weyW8KW0JHyj/b1v7+4XPtFUa4qMqQIbMAw==";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif