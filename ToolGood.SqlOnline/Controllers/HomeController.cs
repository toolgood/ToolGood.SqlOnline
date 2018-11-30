using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using ToolGood.Application;
using ToolGood.DomainService;
using ToolGood.Infrastructure;
using ToolGood.Infrastructure.MvcBase;
using ToolGood.SqlOnline.Models;
using ToolGood.TransDto;

namespace ToolGood.SqlOnline.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IAdminApplication _adminApplication;
        private readonly IAdminService _adminService;

        public HomeController(IAdminApplication adminApplication, IAdminService adminService)
        {
            _adminApplication = adminApplication;
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            #region 防止网页后退--禁止缓存
            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.Headers.Add("Pragma", "no-cache"); // HTTP 1.0.
            Response.Headers.Add("Expires", "-1"); // Proxies.
            #endregion

            DeleteCookie(KeywordString.AdminCookie);
            HttpContext.Session.Remove(KeywordString.AdminSession);

            string m, e;
            var r = _adminApplication.GetRsaInfo(out e, out m);
            if (r.IsSuccess) {
                ViewData["m"] = m;
                ViewData["e"] = e;
            }
            return View();
        }


        public IActionResult Healthy()
        {
            var db = _adminApplication.GetAdminAll();
            return Success();
        }

        [HttpPost]
        public IActionResult Login(string data)
        {
            var r = _adminApplication.RsaDecryptToDict(data);
            if (r.IsFailed) {
                return Error(r.Message);
            }
            var json = r.Value;
            var sessionCode = GetSession(KeywordString.AdminLoginCode);
            if (string.IsNullOrEmpty(sessionCode) || sessionCode != json["vcode"]) {
                return Error("验证码错误！");
            }

            var user = json["username"];
            var ip = GetRealIP().ToLower();
            if (user == "admin" && (ip != "::1" && ip != "127.0.0.1" && ip != "::ffff:127.0.0.1")) {
                return Error("无管理员权限，请尝试其他账号！" + ip);
            }
            var result = _adminApplication.Login(user, json["password"], GetSessionId(), ip);
            if (result.IsFailed) {
                return Error(r.Message);
            }
            AdminDto dto = new AdminDto(result.Value);
            dto.UpdateTime = DateTime.Now.AddMinutes(5);
            dto.ExpiryTime = DateTime.Now.AddDays(1);

            SetCookie(KeywordString.AdminCookie, _adminService.CreateToken(dto), dto.ExpiryTime);
            DeleteSession(KeywordString.AdminLoginCode);
            return Success();
        }


        [HttpGet]
        public IActionResult VerifyCode()
        {
            #region 防止网页后退--禁止缓存
            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.Headers.Add("Pragma", "no-cache"); // HTTP 1.0.
            Response.Headers.Add("Expires", "-1"); // Proxies.
            #endregion

            Random r = new Random();
            string code = r.Next(1000, 10000).ToString();
            SetSession(KeywordString.AdminLoginCode, code);
            VerifyCode vimg = new VerifyCode();
            vimg.FontSize = 23;
            return File(vimg.CreateImageBytes(code), "image/jpg");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ToolGood([FromServices]IHostingEnvironment env)
        {
            return Content(env.ContentRootPath);
        }

    }
}
