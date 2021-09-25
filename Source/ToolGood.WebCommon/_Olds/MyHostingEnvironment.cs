/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.IO;

namespace ToolGood.WebCommon
{
    /// <summary>
    /// 对应asp.net的 HostingEnvironment
    /// </summary>
    public static class MyHostingEnvironment
    {
        public static bool IsDevelopment { get; set; }

        public static string EnvironmentName { get; set; }

        public static string ApplicationName { get; set; }

        public static string WebRootPath { get; set; }

        public static string ContentRootPath { get; set; }
        /// <summary>
        /// 获取文件物理路径  
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            return IsAbsolute(path) ? path : Path.Combine(ContentRootPath, path.TrimStart('~', '/').Replace("/", Path.DirectorySeparatorChar.ToString()));
        }
        /// <summary>
        /// 以WebRoot为根目录获取文件物理路径  
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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
