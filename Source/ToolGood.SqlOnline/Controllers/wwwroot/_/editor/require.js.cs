#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/editor/require.js")]
        public IActionResult __editor_require_js()
        {
            if (SetResponseHeaders("43040A5AF3FB45CC411C57CBA454181D") == false) { return StatusCode(304); }
            const string s = "GzwNIByFcZvzc16KlE6TFisTZN9S39PVIn/kt2aN7CvkDSn1cMakSvpdymA5TcY31f00503QlQAjvy8bdJ/1I4qciyXo2fRkZeCa2qKQE2aqM5Kl3l/yKeKA+dMRo0Z0c479HsO5dxjokFqkBPkvKlZczsMmL+X+52JCzIa1CJ2M1fGsRrqrjBIcGcfpfHqigB9qtDocsNyV+c9Ghz/TIm4g+DTr8M9bP5B818I+Oemt2QgsDMFBskefSynWwNBumZxixkpQBe76vUygqbGDUg4Yer8WwdsWGQkNacANQyVE4wISxZ4syrEuOP4bAN7pk0HlRGNL8K1tOW0euLfmNFWDemnbi8B11Ho478Ir7zwxMtSBRbpgOedxYtMerCN+L6uLFa8qGqk870rh/tJlbbhbi/LKpWG9ULC+vo7zti3L8y/JsmoY7v/onHL04gWVDFSIpHvmnXJdl/q7orByo3PqfzSOfseTJhEwKm6waWSdmWNjJhxidQ7mijdtix0SsD0Rz7E1Q1AsCaGTfSconfhpVwsYaRJSWE0SjvlcOLmicZnEeFB4cZO1AcM67jBAAp3Pk/Nyz6K5gDnH+C5u7fV6ZFinH9Qrcb+nn92/JFPWSu6xSZQ+4kxVon9s/EU+fWex9ssuW/hulKU7uYh46qL42ka7YxQiAtlPbo/jvW6HeNOBUTttohy/j87d9x4GcAHJfsUxusu76C+wWqpyvE8xlTnKDzhkwlP2fsFNbclMzLTncCeFndoBFDXa4ZfB/g1qTWc8QzYRAsim+HZ6yJpcAyaNLbeyGTbZHu7h7Cudb9k0ITKxLg2NMu8zq8t/ryRtSiOyyMKQ01ZDpYaca1mJb01rPDwOLthssSxoj5fyKb3P9HLvEwtqtnQa2RQo1yo5c8BJbagbjpAhjv0LVadAnPY15DSIPKc6L46tKXTn/VjYE2JVydynGxAkQrA67R4/F3EWxbSBkAgdMz8IdKqEXAi6mZIoC1KQl+8/Hymrelqfpld590p+g7qil739Mnby1Gyc1QlevKiG2QmEkZliI0lFGwQTWYIOLV53vVFLJRIAqQkIqFkriyNCq7IUsAPLv9UVM+nNq0gzzPGuBP0htnhhtkL0XzqY0yUDgmlIWMxYtBXVSl1yUy35NVDzlGv6fIeZ+KQilq2yv5pGjPNs+FTOTo/hNNtz5iywNE+5xaEitHzYZXM8z3VCmmWMaSt9hM2i3n3+BELT500MRdmEpNmnbf+anT2EfpV5fdKEUGQtufwVgl/AdSZzB5rN//Fy7p+GAp6FNHobNeGkC0qgNlp7UoZEiPE+0hUc8w/oDOwROfBchJUC62Y5M+n6bkmcE2Wdw5n4lMrsT4kyrJeB36IwJaC2EzcybXDpKtFsjcaS1HDSLBGXoWYNHZ9iXpsvPdJNDH2Hw+gN51m7ETaQIgjEvLnCaZoecG+3Eczb7mLirLeIDdZkDLyUp5vAPUjaTRy0TDSFuoLb+Ky6ieYwbdSISLDDhXPG/7B4bcBaPViFqvvWKnE4IOO0Di5ml5UBNPkgpXYKkbP/mMFKkEDkyxRklItMxYavpuU7zSqzH0uhoqnWCjZElEhVdpdp0YqnoGBq2ggQ2CAj/XRuYrp8EvtNskJg3S6zBtINy6nrXgzATOZC32kndYjFXOZiuEG7FAlbm40BLGAU2uqnyKQi+SsHISyl+/mapGckHhnkGOnuqAA=";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/javascript");
        }
    }
}
#endif