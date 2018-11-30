using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using ToolGood.ReadyGo3;
using ToolGood.ReadyGo3.Gadget.Monitor;

namespace ToolGood.Infrastructure
{
    public class BaseController : Controller
    {
        protected int SuccessCode { get { return KeywordString.SuccessCode; } }
        protected int ErrorCode { get { return KeywordString.ErrorCode; } }
        private ISqlMonitor _monitor;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (Request.Method.ToUpper() == "POST") {
                if (ModelState.IsValid == false) context.Result = Error(ModelState);
            }

            if (Request.Query.ContainsKey("_test")) {
                _monitor = new ToolGood.ReadyGo3.Gadget.Monitor.SqlMonitor();
                Config.SetMonitor(_monitor);
            } else  {
                var urlReferrer = Request.Headers[HeaderNames.Referer].ToString();
                if (string.IsNullOrEmpty(urlReferrer)==false && urlReferrer.Contains("_test")) {
                    _monitor = new ToolGood.ReadyGo3.Gadget.Monitor.SqlMonitor();
                    Config.SetMonitor(_monitor);
                }
            }
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //Response.Headers.Add("Strict-Transport-Security", "max-age=63072000; includeSubdomains; preload");
            //Response.Headers.Add("X-Frame-Options", "DENY");
            base.OnActionExecuted(context);
            Config.Dispose();
        }


        #region JumpUrl
        protected IActionResult JumpTopUrl(string url)
        {
            var content = new ContentResult();
            content.Content = $"<script>top.location.href='{url}'</script>";
            content.ContentType = "text/html";
            return content;
        }
        protected IActionResult JumpUrl(string url)
        {
            var content = new ContentResult();
            content.Content = $"<script>location.href='{url}'</script>";
            content.ContentType = "text/html";
            return content;
        }
        #endregion

        protected IActionResult LayuiSuccess<T>(Page<T> page)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = 0;
            data["msg"] = "Success";
            data["data"] = page.Items;
            data["count"] = page.TotalItems;
            if (_monitor != null) {
                data["_test_monitor"] = _monitor.ToText();
            }
            return Json(data);
        }
        protected IActionResult LayuiError(string msg)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = 1;
            data["msg"] = msg;
            if (_monitor != null) {
                data["_test_monitor"] = _monitor.ToText();
            }
            return Json(data);
        }


        #region 重写 View
        public override ViewResult View(string viewName, object model)
        {
            if (_monitor != null) {
                ViewData["_test_monitor"] = _monitor.ToText();
            }
            return base.View(viewName, model);
        }
 

        #endregion


        #region Success
        protected IActionResult Success(object obj)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = SuccessCode;
            data["msg"] = "Success";
            data["result"] = obj;
            if (_monitor != null) {
                data["_test_monitor"] = _monitor.ToText();
            }
            return Json(data);
        }
        protected IActionResult Success<T>(List<T> objs)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = SuccessCode;
            data["msg"] = "Success";
            data["result"] = objs;
            if (_monitor != null) {
                data["_test_monitor"] = _monitor.ToText();
            }
            return Json(data);
        }
        protected IActionResult Success<T>(Page<T> page)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = SuccessCode;
            data["msg"] = "Success";
            data["items"] = (object)page.Items;
            data["currentPage"] = page.CurrentPage;
            data["totalPages"] = page.TotalPages;
            data["totalItems"] = page.TotalItems;
            data["itemsPerPage"] = page.PageSize;
            data["pageStart"] = page.PageStart;
            data["pageEnd"] = page.PageEnd;
            data["context"] = page.Context;
            if (_monitor != null) {
                data["_test_monitor"] = _monitor.ToText();
            }
            return Json(data);
        }
        protected IActionResult Success(string msg = "Success")
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = SuccessCode;
            data["msg"] = msg;
            if (_monitor != null) {
                data["_test_monitor"] = _monitor.ToText();
            }
            return Json(data);
        }
        #endregion

        #region Error
        protected IActionResult Error(ModelStateDictionary ms)
        {
            List<string> sb = new List<string>();
            //获取所有错误的Key
            List<string> Keys = ModelState.Keys.ToList();
            //获取每一个key对应的ModelStateDictionary
            foreach (var key in Keys) {
                var errors = ModelState[key].Errors.ToList();
                //将错误描述添加到sb中
                foreach (var error in errors) {
                    sb.Add(error.ErrorMessage);
                    //break;
                }
                //break;
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = ErrorCode;
            data["msg"] = string.Join(",", sb);
            //data["msg"] = ModelState[key].Errors[0].ErrorMessage;
            if (_monitor != null) {
                data["_test_monitor"] = _monitor.ToText();
            }
            return Json(data);
        }

        protected IActionResult Error(string msg)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = ErrorCode;
            data["msg"] = msg;
            if (_monitor != null) {
                data["_test_monitor"] = _monitor.ToText();
            }
            return Json(data);
        }
        protected IActionResult Error(int code, string msg)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = code;
            data["msg"] = msg;
            if (_monitor != null) {
                data["_test_monitor"] = _monitor.ToText();
            }
            return Json(data);
        }
        protected IActionResult Error500()
        {
            return new StatusCodeResult(500);
        }
        protected IActionResult Error404()
        {
            return new StatusCodeResult(404);
        }
        #endregion

        #region Other CreatePassword GetUserAgent MapPath

        protected string CreatePassword(string user, string password, bool isMd5 = false)
        {
            if (isMd5) {
                return Hash.GetMd5String(user + "|ToolGood|" + password);
            }
            return Hash.GetMd5String(user + "|ToolGood|" + Hash.GetMd5String(password));
        }

        protected string GetUserAgent()
        {
            return HttpContext.Request.Headers[HeaderNames.UserAgent].ToString();
        }

        /// <summary>
        /// 获取文件绝对路径
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        protected string MapPath(string path)
        {
            return HostingEnvironment.MapPath(path);
        }

        protected string MapWebRootPath(string path)
        {
            return HostingEnvironment.MapWebRootPath(path);
        }
        #endregion

        #region Session

        protected string GetSessionId()
        {
            return HttpContext.Session.Id;
        }

        protected void SetSession(string key, string val)
        {
            HttpContext.Session.Set(key, Encoding.UTF8.GetBytes(val));
        }
        protected void SetSession(string key, object value)
        {
            HttpContext.Session.SetString(key, JsonConvert.SerializeObject(value));
        }

        protected string GetSession(string key)
        {
            return HttpContext.Session.GetString(key);
        }

        protected bool HasSession(string key)
        {
            return HttpContext.Session.Keys.Any(q => q == key);
        }

        protected T GetSession<T>(string key)
        {
            var value = HttpContext.Session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        protected void DeleteSession(string key)
        {
            HttpContext.Session.Remove(key);
        }

        #endregion

        #region Cookie 操作

        protected string GetCookie(string key)
        {
            return HttpContext.Request.Cookies[key];
        }

        protected void SetCookie(string key, string val)
        {
            HttpContext.Response.Cookies.Append(key, val);
        }

        protected void SetCookie(string key, string val, int minutes)
        {
            HttpContext.Response.Cookies.Append(key, val, new Microsoft.AspNetCore.Http.CookieOptions() {
                Path = "/",
                Expires = DateTime.Now.AddMinutes(minutes),
                HttpOnly = true,
                IsEssential = true
            });
        }

        protected void SetCookie(string key, string val, DateTime dateTime)
        {
            HttpContext.Response.Cookies.Append(key, val, new Microsoft.AspNetCore.Http.CookieOptions() {
                Path = "/",
                Expires = dateTime,
                HttpOnly = true,
                IsEssential = true
            });
        }

        protected void DeleteCookie(string cookieName)
        {
            var val = Request.Cookies[cookieName];
            if (val != null) {
                SetCookie(cookieName, "", DateTime.Now.AddYears(-1));
            }
        }

        protected bool HasCookie(string cookieName)
        {
            return Request.Cookies.ContainsKey(cookieName);
        }
        #endregion

        #region 获取真ip
        /// <summary>
        /// 获取真ip
        /// </summary>
        /// <returns></returns>
        protected string GetRealIP()
        {
            string result = String.Empty;
            result = Request.Headers["HTTP_X_FORWARDED_FOR"];
            //可能有代理 
            if (!string.IsNullOrWhiteSpace(result)) {
                //没有"." 肯定是非IP格式
                if (result.IndexOf(".") == -1) {
                    result = null;
                } else {
                    //有","，估计多个代理。取第一个不是内网的IP。
                    if (result.IndexOf(",") != -1) {
                        result = result.Replace(" ", string.Empty).Replace("\"", string.Empty);

                        string[] temparyip = result.Split(",;".ToCharArray());

                        if (temparyip != null && temparyip.Length > 0) {
                            for (int i = 0; i < temparyip.Length; i++) {
                                //找到不是内网的地址
                                if (IsIPAddress(temparyip[i]) && temparyip[i].Substring(0, 3) != "10." && temparyip[i].Substring(0, 7) != "192.168" && temparyip[i].Substring(0, 7) != "172.16.") {
                                    return temparyip[i];
                                }
                            }
                        }
                    }
                    //代理即是IP格式
                    else if (IsIPAddress(result)) {
                        return result;
                    }
                    //代理中的内容非IP
                    else {
                        result = null;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(result)) {
                result = Request.Headers["REMOTE_ADDR"];
            }

            if (string.IsNullOrWhiteSpace(result)) {
                result = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            }
            return result;
        }
        private bool IsIPAddress(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length < 7 || str.Length > 15)
                return false;

            string regformat = @"^(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);

            return regex.IsMatch(str);
        }


        #endregion

        #region MachineCode
        private string _machineCode;
        public string MachineCode {
            get {
                if (_machineCode == null) {
                    string s = "ToolGood|LinZhijun|310036871|" + BitConverter.ToString(typeof(BaseController).Assembly.GetName().GetPublicKeyToken()).ToLower().Replace("-", "");
                    ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher();
                    managementObjectSearcher.Query = new ObjectQuery("select * from Win32_Processor WHERE (ProcessorId IS NOT NULL)");
                    foreach (ManagementObject managementObject in managementObjectSearcher.Get())
                        s += "|" + managementObject.GetPropertyValue("ProcessorId").ToString().Trim();

                    managementObjectSearcher.Query = new ObjectQuery("select * from Win32_BaseBoard WHERE (Product IS NOT NULL) AND (SerialNumber IS NOT NULL)");
                    foreach (ManagementObject managementObject in managementObjectSearcher.Get())
                        s += "|" + managementObject.GetPropertyValue("SerialNumber").ToString().Trim();

                    managementObjectSearcher.Query = new ObjectQuery("SELECT * FROM Win32_DiskDrive WHERE (SerialNumber IS NOT NULL) AND (MediaType LIKE 'Fixed hard disk%')");
                    foreach (ManagementObject managementObject in managementObjectSearcher.Get())
                        s += "|" + managementObject.GetPropertyValue("SerialNumber").ToString().Trim();

                    managementObjectSearcher.Query = new ObjectQuery("select * from Win32_BIOS WHERE (SerialNumber IS NOT NULL)");
                    foreach (ManagementObject managementObject in managementObjectSearcher.Get())
                        s += "|" + managementObject.GetPropertyValue("SerialNumber").ToString().Trim();

                    _machineCode = Base58.Encode(Hash.GetMd5Bytes(s));
                }
                return _machineCode;
            }
        }
        #endregion
    }

}
