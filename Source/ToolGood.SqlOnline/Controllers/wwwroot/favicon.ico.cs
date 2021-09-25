#if RELEASE
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace ToolGood.SqlOnline.Controllers
{
    public partial class WwwRootController : Controller
    {
        [HttpGet("favicon.ico")]
        public IActionResult favicon_ico()
        {
            if (SetResponseHeaders("8FE19626541E32EA72C132654B373BD2") == false) { return StatusCode(304); }
            const string s = "G70QUC4ObDp0xwqRIWM5hs14xXD8A2tHcOqlqSikKzxRD5Zub5ycVITvdWxZCMYQAaBMB3sn2KQVhKpthBopzfH53QOT3Rgl+H6ttNOX+qlZFQKVkedOyKBkJaIPaGrzQlge2CYqs0cLeDZhhRbYqFh6UWYGpMnm+aHwsFBYeCgUFgaKB/csAAYwwM1dn/+BEShGKBCGAm4o0IbvmXH01HDjltXpNkAh+wDsf4oI6wpgNtMm2acQTJRaeCkxZs+NC1v6ZH/AESBNmsr6lYMFFcInKlC3I4998tv/hPz3y3t3rSlhdpbxX8SyuQfEo6nHs3aIvnnhQbrutic++JeQn87EYNoLNsuqKIkh1buEvLKulBctwhlHDeaMqSYrXnLsFDnwPSG4P6RICrtesPMDclEcyReSmuYCHsR8t043TJun0q6QWFaFJMg67so5kyGV9XzRpju/dRc5IUobpLwC2X7uQ936VqRk2bfh7rma7/qM/FI812OxpEVMNlAEXBVMyNag7wZiXq1ZGzxnquSXHCPaOZwl1p4ZMdiUQm4Xaiqgw0cgBEGoEy1kPgnuYhRzEyxwULxr3I8ycgowK0v3IJduqctMsVdexKvwtlN0hYEyCDpYAMGQKaKcfHaKI/rkhhkXIdcIpEavXHbFtQ6BrFqWAOgquitvXTDy3hWeTT/6HQc2yBpzFKEeultopInEgLSh/HCqnDAAwW/FvPf+K2jOXGsGRZA0pqYiLQoVNITWZpr+AAkXIV9sCy7JsDFDTsli/4Ze0x3KDOB8pQLIFuU6hYENCbvjbATrL/TmIeH9qq/Is0FIaK/weZeqDWLjFBvZZi4jDjQETq7g7Vej8gu/FS/3Di6k563/Hsc3qSuxIGXGR/99sUkDqPbrSBPnw6d68b1sASxH2+7WT+5qG9Bm2RMkI8eUEqoCfgFyPr+iusKh3BNHviALwvwSSqwF9dmDMbjJGUA17j9d4NIngGkkmXmlBrYUP0JBfvUi49Y3D/bsMxBZ7oLOiipasrinAQ/leXZAnDIt+vRoUiSIeR2SGIZNk32Ic1iDiFIZC7323YvX9+u733757pMXblhVzTXjq0FoDOo1lrFKH5pR+OmdJ+645ro7nnjn1/7IQy39OqmYh6+O1AukTwrPh6wK0xOGhzRDwRqcpO+9IqYUf0zSL2jm80FjHJbjFd7vB7QMTfT6q4OUGjevDcz6+lqnYAXGC+/dMYkjri10m1DB0Be/dT1UDZI33Ti2YZfrFAVNfX770K2WEESyIB/hFIeLC3wYSxrWKWOSaDDho8JVSfO5pA9fGhfEVwEClolJ75qRfPbUVSf27DtxzVNf9kfeGcv1CezaMl4hzG4KEFMQZcCpZz51+vOG+eOTp471iyzlgAiArmG8/04Dp89ugwY/RYaswjFYMaSAN6NeBzwzu4XOKr7EDwFEq6zNhFdWGHd+DoRjnpWNC9UscwxumewfFXIt3CaALPseF26AVgE=";
            var bytes = UseCompressBytes(s);
            return File(bytes, " image/vnd.microsoft.icon");
        }
        private bool SetResponseHeaders(string etag)
        {
            if (Request.Headers["If-None-Match"] == etag) { return false; }
            Response.Headers["Cache-Control"] = "max-age=315360000";
            Response.Headers["Etag"] = etag;
            Response.Headers["Date"] = DateTime.Now.ToString("r");
            Response.Headers["Expires"] = DateTime.Now.AddYears(100).ToString("r");
            return true;
        }
        private byte[] UseCompressBytes(string s)
        {
            var bytes = Convert.FromBase64String(s);
            var sp = Request.Headers["Accept-Encoding"].ToString().Replace(" ", "").ToLower().Split(',');
            if (sp.Contains("br")) {
                Response.Headers["Content-Encoding"] = "br";
            } else  {
                using (MemoryStream stream = new MemoryStream(bytes)) {
                    using (BrotliStream zStream = new BrotliStream(stream, CompressionMode.Decompress)) {
                        using (var resultStream = new MemoryStream()) {
                            zStream.CopyTo(resultStream);
                            bytes = resultStream.ToArray();
                        }
                    }
                }
                if (sp.Contains("gzip")) {
                    Response.Headers["Content-Encoding"] = "gzip";
                    using (MemoryStream stream = new MemoryStream()) {
                        using (GZipStream zStream = new GZipStream(stream, CompressionMode.Compress)) {
                            zStream.Write(bytes, 0, bytes.Length);
                            zStream.Close();
                        }
                        bytes = stream.ToArray();
                    }
                }
            }
            return bytes;
        }
    }
}
#endif