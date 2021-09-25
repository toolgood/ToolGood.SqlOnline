#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("_/zTree/css/zTreeStyle.css")]
        public IActionResult __zTree_css_zTreeStyle_css()
        {
            if (SetResponseHeaders("9443319C29D3B54123D36375465E6029") == false) { return StatusCode(304); }
            const string s = "G8EVAIzCtu0xivotTlGEoUs3pxXopMG2pGpruwbzD6J/Vd1UFovbujJKUIeAMheGo8vk0ikgAXzwNL1902Rnb+9bp7eUjvGfvZkvrQCnUESCFopyACOah7g9zaTHDtoJMtUI0YWdZLSpa0SKFBK6wxd/fuXZhb06VrGdrR5+1vlN7PbfFCLz79BEsave0B9TeUQvdWgXGV8c2DedKGUxRVISSilBapeIfCiARo6ByeBaJiGqxjDTYxPV0E2Q2oy2hFYOpcHhGwcIgHKYkJWFJKBe+8BdKAqjCc63SpnN6xFM1GbuXn+DZB43axKbJo1oBku7pCrM4C1XmRWZYssyhU8GwdwfIlb5n8poOPJe6PY3xG2rJ6PJJw+WoGFW0Gh+Obu3k8v4Amt0+I1mYQYxXdz8co3mj/ilGPibqSkRCf+KX724oSPb5OKNtmDmfOFc3ugRnt9uYLG0Vt3c3IZmiQ7S3z9qX7eylVX64J8Asj/YJ4qlxvGpUHHwXAAmyaOfzb1Di2txwZLutxRL6VpiNzTOeMOdQsTrofsimoBiBQwBdrVmo9Urp6zZCGqPa5tKnOMf6IUM3UAV8iJaPeSucjx/iHrcKlh2DhxISBkD09Ny9ZMyYaJhTpUt7nQQxgFBoM/EjVKU3k7AeUe62hlpqcgajQhRf1mRIK/zAryuqYi1zRy1okEK8gqMsgmsrTZPMGWlSYImFGJQJ4zQr84jLQW0nLapWvmDxPohacB4KoAZUgMXLKyoAEVCTBrFEAxdwJ6nD5joAs2SN/sQHxRzYw3CQGFDBicOYtInguBoQQjEKg69Eyv+rj/EirPrD7Hi7/pCrLg7K4QKLHTlclREpj6jAmg4CL/AuzlqIAwIXQij1c6aaiqq1gAwdtS1CiJ5EgAJkqEwiOCsVazLI6SXyJ7Rcvp3KyCLC7/tpaPntNAzzGnZA7cdqqfah3sQVtZaMiCwNceAwhZ7fnjOxi6oyDjJXgWrLhNu45q7BpuVF5PjVPSg2/ytG67hBhHlkW1vB2SaK4+ovU2pLFAe538hqWY7iUWd4/SKuofc5Ga2VWgF2RYUo+WJxqY2i9whpqoKMwX8qFsyKoECOPl6vo/G7aqCHkNXcyQeMqIz+SRnQwTrkr37fT3dKHZlvzy2TbXZPEhu8KpLZYffT9rOTSpK/Id224A6b0yIuqg+8QeHOO3xo5gtxS0eNI2fhgFruLHrFPOvcd0OiDgdyGew88GF0cAf";
            var bytes = UseCompressBytes(s);
            return File(bytes, "text/css");
        }
    }
}
#endif