#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/antlr4/atn/Transition.js")]
        public IActionResult __editor_antlr4_atn_Transition_js()
        {
            if (SetResponseHeaders("9A92BC48DF146EFF0FD9155A62705BDE") == false) { return StatusCode(304); }
            const string s = "G58SAByFjYP+8bQ0JsZRY3dVWeT36nRa40tBer4q9Wr1lxykFcggY3BqVJspm2Y7uSydJqcGIZDgfQNHtRFBN5RmIYzzqjVba2nLK2ydpJ33ReUv2ds8clnVlcfnqIj3XwBUtahVn8Ose37afUotIFCSuMV7COFPCMi/iASwQwjpx2MLStEnb8lnOVRhFOZAfl6RTqHOmeaaAV+/NdfWqU1elXh4qcgGFoyQsGSMlSCHH09DsYyV7WfK2Ss2MIXhWxAH/nrkTOyxa3gxO43A5nxpqM4s2y22Li/OxbNoqC1seaUv9L35M8LyEKJSlcixqdU2MWRAv4/tZbPYO2ErWrJwLv5jWtNFbzgZX8qquqvFaHW9TemPzO+bIUiom93lOmFfldUGtU4LDCxoR6hq47uacEsAR64xqL8C22k1gt7PTuetZi/4WbYqWVT0mNgw4+Ya8NcMcmewilx5a5ezqmIns91on7Jdk16Lud7/9ea9y2bhoNY4K7szsPrUwhcXrSUbZ4Hpl5cksPb5XxrgD/+ZonuDO/9QkC/V7niclCou1Rt3yTq86Uv1orL6N7hQ05aEPJfjyHiy3NBqV+LRiiDH0Hlt6PS13VSju86lk8HgdH5rHLScjHONwZoqd2EdNF1sqqFU4zfjkZHl9Wb8xdANbksLJZ5O2Skfd9xs4PvevAg/u5w5fBWdcfgv04sezWF3C/k8Eos0W0J7sQEkh9KPxm360dltPwaHPojJZPqwKOEQtor24aiqClyivgNZldWBLWkH8Np0b4B4I8p4hwBggHHVAO8BYIdgjAYYEwK+02DcGJxDK1CGgSv+Fc2a7M7zvTs2Bua52u1Wzu85qTUNSZdnW5wfNO3mG3MVqb4bkE/kk/yLkFdl8dXGFTdkZNC2G2IijsDtxkkJOLKlsoas6uBIFKGQ+j8HmYfcBRKVOstyurqgNrTmWQwBWRTeKGE0gccelTJAkfl7yGDFJWD8fNZqAA463GYHx80RWMm9TB44dRKs90/geN9amszqnaWXd1lkKbSFIE0BGJk5ltGjAmhz80RQw46oDQEYHz5RzPjLS/mxF4VnzxLpfyU3H3r38lrgxyVYb/KS0sXbgVKXdHXaXwFZIVWZOFhoK6RVyJgwTxpjHYfi8OYwnKwH6nCTW0ROdgGd4zEhF+SpIqCoKhtylzLgkp9MSzoCq1vIMn0krVtGq9Dkq8jR7aiQzDEAlOGRUtNP6l/tzZNCWnSXhBkSJSzPOWhFGe++a8cYk9t3xJh18DIt5XDOI0CcGUn0Xmvn/ZN7w2aFXoG0pK5k5BCihdsO5Ki/XYGxWth4AkWWegsZ7RXVGkCtP3msfUXSUt3jM//kUu0Ttc2JrH4us3CmAGRQzKUyRfkXIfeCOsCiu3AV9RGI1CEc7URTdEk3sfRGgxgWytFdCAma34Q5x3mEK1R/bM3xGA4=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif