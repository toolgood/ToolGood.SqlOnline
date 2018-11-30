using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToolGood.Infrastructure
{
    public static class HostingEnvironment
    {

        public static string EnvironmentName { get;  set; }

        public static string ApplicationName { get;  set; }

        public static string WebRootPath { get;  set; }

        public static string ContentRootPath { get;  set; }

        public static string MapPath(string path)
        {
            return IsAbsolute(path) ? path : Path.Combine(ContentRootPath, path.TrimStart('~', '/').Replace("/", Path.DirectorySeparatorChar.ToString()));
        }
        public static string MapWebRootPath(string path)
        {
            return IsAbsolute(path) ? path : Path.Combine(WebRootPath, path.TrimStart('~', '/').Replace("/", Path.DirectorySeparatorChar.ToString()));
        }
        private static bool IsAbsolute(string path)
        {
            return Path.VolumeSeparatorChar == ':' ? path.IndexOf(Path.VolumeSeparatorChar) > 0 : path.IndexOf('\\') > 0;
        }

    }
}
