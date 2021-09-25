/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ToolGood.WebCommon
{
    public static partial class ActionResultUtil
    {
        /// <summary>
        /// 跳转Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static IActionResult JumpTopUrl(string url)
        {
            var content = new ContentResult();
            content.Content = $"<script>top.location.href='{url}'</script>";
            content.ContentType = "text/html";
            return content;
        }

        public static IActionResult JumpTopUrl(string url, string msg)
        {
            var content = new ContentResult();
            if (msg == null) { msg = ""; }
            msg = JsonConvert.SerializeObject(msg);

            content.Content = $"<script>var msg={msg}; console.log(msg);top.location.href='{url}'</script>";
            content.ContentType = "text/html";
            return content;
        }

        /// <summary>
        /// 跳转Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static IActionResult JumpUrl(string url)
        {
            var content = new ContentResult();
            content.Content = $"<script>location.href='{url}'</script>";
            content.ContentType = "text/html";
            return content;
        }


        public static IActionResult JumpUrl(string url, string msg)
        {
            var content = new ContentResult();
            if (msg == null) { msg = ""; }
            msg = JsonConvert.SerializeObject(msg);

            content.Content = $"<script>var msg={msg}; console.log(msg);location.href='{url}'</script>";
            content.ContentType = "text/html";
            return content;
        }

    }
}
