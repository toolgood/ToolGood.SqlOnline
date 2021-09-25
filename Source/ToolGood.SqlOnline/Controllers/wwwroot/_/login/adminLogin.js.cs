#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/login/adminLogin.js")]
        public IActionResult __login_adminLogin_js()
        {
            if (SetResponseHeaders("766BF442D89EFC081A490C46EC443DBE") == false) { return StatusCode(304); }
            const string s = "G6ALQJyFKZx5xPDyJ1VVx8wlUPHII6xb9p66O4/7XQ5SUzto7ZNSDqIcilFjTaWXx7pH5UagHmqyXS8DsmrZlP4papnm+rPb+eEkKiT3OcEd+WqFMyj8r6WzORepcSnC4cyV9+8upW1ore1ualNEgepFIrEKLINR6DDXdqY/6oyGnIkVPEq1qnaBRtikCilU8Tb5Wq9ppdaLg6Gk6IwcVh/LtZLfGvREsYk90VnWhtgs28S0P1GiihBCzYp28Ojaf8+foSomCDA5AWtWTxn1SfaZeGVwNF+pgfmlmZDgJIF8R6+pJcx9DP/zoBn65Z1SAJbeUjOsJ5xyP/b1a7HwqgUaYYO0Ce38j4kinHnVW7CHLdLL9/Py6nLpztzdzow70fniwbCUQP+NSyUYt+NyWDvG5Sf04RWNkFMfvhZRR4zruuWObWxPtNX+m6WeyUq/V9rlIZRXDzurRjUk034GXWwVaarNed36K+O50HARjt6ysTfhfsnGLLMic/xBwaJlxonw3o/nCWRETxoVpIvv7SOOJjqxstmvQ010CjSfUrCCsvmgG2t3GM9xb/TLfnf8IXe/zMvwpeKjOAgY7ljMQmgIlaAF9N0Vjgj9oa+KH/xw27I5O78L+gL0YQO/ObwgAwa6F8IXekKyGiEsRxrl8v0rznsfZyHaAkOYp/IiAbFUiolI9wlINZptuzcQpxaoZKaG0VSAjbT0h+em06f0+bxwySArQnOarG+P67p8sCzmwgCN7RwESJpIshWKuVgQrhwEcYfu/ijV3idvE5xlCb1d3lorDuOx0j8gKsrtOy2/Pjz4P6TQ8y83yZiJl4dzfroyzX+pCbEVUO4IiI4efTe6/VqDxETc1cVVs8xI7q5XqN8MEaWBhMRVlrpw3sWMqvipnJPY5vxjBx0aG17o4p4oXuC5o2TXkrpbzyOvYk23mxNVHqze4KE8UFv6oTfdOa56Od9uyzXWggiDCIIv7lXacX6cBODda6q4r/J1RMdFFVwqwEbvjAx2YSKxajRUQUVhVyQcIne6p8+hy2SEbuoqxz8g9zo38yKTKEtRUhAG8RaEqEVpZZz1OqFNEk2jaPyRpJIjBSV7FGdsJU++kZKzuRA39IgRVv2Th7xnv/z5eNXM/ZQajuxOr7shlAudtTMF0dOfqfUSLmqQP1QgwU/SZchoAvKAPOl9jjERgEpIJ4FDpSX66TyXRz3PLMBaQkgm9HHyAON7cqk0Y8gi/wBizgTf0cuIJfYXuILUABe9ua+Urph6kAfrLdYttl3BGSoXi3B//uUcrciIlAA=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif