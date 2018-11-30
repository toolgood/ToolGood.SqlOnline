using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using ToolGood.Infrastructure;

namespace ToolGood.SqlOnline.Controllers
{
    public class StaticFilesController : Controller
    {
        private static Dictionary<string, string> Etags;

        public IActionResult Get(string url)
        {
            if (HasCache(url)) return new StatusCodeResult(304);

            var istr = GetFileStream(url);
            if (istr == null) {
                return new StatusCodeResult(404);
            }
            var etag = GetETag(istr);
            Etags[url] = etag;
            Response.Headers.Add("Cache-Control", "public, max-age=604800");
            Response.Headers.Add("Etag", etag);
            Response.Headers.Add("Expires", DateTime.Now.AddDays(7).ToString("R"));
            Response.Headers.Add("Last-Modified", DateTime.Now.ToString("R"));

            var contentType = GetContentType(url);
            return File(istr, contentType);
        }

        private bool HasCache(string url)
        {
            if (Etags == null) { Etags = new Dictionary<string, string>(); return false; }
            if (Etags.ContainsKey(url) == false) { return false; }

            foreach (var item in HttpContext.Request.Headers) {
                if (item.Key.ToLower() == "if-none-match") {
                    if (Etags[url] == item.Value.ToString()) {
                        return true;
                    }
                }
            }
            return false;
        }

        private string GetETag(Stream stream)
        {
            var hash= Hash.GetMd5String(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return hash;
        }

        private string GetContentType(string url)
        {
            var fileName = Path.GetFileName(url.Split('?')[0].Split('#')[0]);
            var fileExt = Path.GetExtension(fileName).ToLower();

            var contentType = "application/octet-stream";
            if (fileExt == ".js") {
                contentType = "application/x-javascript";
            } else if (fileExt == ".css") {
                contentType = "text/css";
            } else if (fileExt == ".jpeg" || fileExt == ".jpg" | fileExt == ".jpe") {
                contentType = "image/jpeg";
            } else if (fileExt == ".gif") {
                contentType = "image/gif";
            } else if (fileExt == ".png") {
                contentType = "image/png";
            } else if (fileExt == ".svg") {
                contentType = "image/svg+xml";
            } else if (fileExt == ".woff") {
                contentType = "application/font-woff";
            } else if (fileExt == ".woff2") {
                contentType = "application/font-woff2";
            } else if (fileExt == ".eot") {
                contentType = "application/vnd.ms-fontobject";
            } else if (fileExt == ".otf" || fileExt == ".ttf") {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        private Stream GetFileStream(string url)
        {
            var dirPath = Path.GetDirectoryName(url);
            var fileName = Path.GetFileName(url.Split('?')[0].Split('#')[0]);
            var filePath = "ToolGood.SqlOnline.__." + dirPath.Replace("\\", ".").Replace("-", "_") + "." + fileName;

            Assembly myAssem = typeof(StaticFilesController).Assembly;
            return myAssem.GetManifestResourceStream(filePath);
        }



    }
}
