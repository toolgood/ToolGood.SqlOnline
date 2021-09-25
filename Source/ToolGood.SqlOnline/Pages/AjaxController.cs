/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.Common;
using ToolGood.Common.Utils;
using ToolGood.RcxCrypto;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Configs;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;
using ToolGood.WebCommon.Controllers;
using ToolGood.WebCommon.Utils;

namespace ToolGood.SqlOnline.Pages
{
    public class AjaxController : ApiControllerCore
    {
        private readonly IAdminApplication _adminApplication;
        public AjaxController(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        [HttpGet("/admins/Account/VerifyCode")]
        public IActionResult VerifyCode()
        {
            #region 防止网页后退--禁止缓存
            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.Headers.Add("Pragma", "no-cache"); // HTTP 1.0.
            Response.Headers.Add("Expires", "-1"); // Proxies.
            #endregion

            Random r = new Random();
            string code = r.Next(10000, 100000).ToString();
            SetSession(SessionSetting.AdminLoginCode, code);
            VerifyCode vimg = new VerifyCode();
            vimg.FontSize = 30;
            var bytes = vimg.CreateImageBytes(code);
            var AcceptEncodings = Request.Headers["Accept-Encoding"].ToString().Replace(" ", "").Split(',');
            if (AcceptEncodings.Contains("br")) {
                Response.Headers["Content-Encoding"] = "br";
                bytes = CompressionUtil.BrCompress(bytes, true);
            } else if (AcceptEncodings.Contains("gzip")) {
                Response.Headers["Content-Encoding"] = "gzip";
                bytes = CompressionUtil.GzipCompress(bytes, true);
            }
            return File(bytes, "image/jpg");
        }

        [IgnoreAntiforgeryToken]
        [HttpPost("/admins/ajax/Account/Login")]
        public async Task<IActionResult> Login([FromBody] Req<AdminLoginDto> request)
        {
            var rsa = RsaHelper.Instance;
            if (request.CheckSign(rsa.PrivateKey, rsa.RsaModulus, rsa.RsaExponent, out string msg) == false) { return Error(msg); }
            if (request.DecryptData() == false) { return Error("数据错误!"); }

            if (CheckSession(SessionSetting.AdminLoginCode, request.Data.Vcode) == false) { return Error("验证码错误!"); }
            DeleteSession(SessionSetting.AdminLoginCode);

            var admin = await _adminApplication.AdminLogin(request);
            if (admin == null) { return Error(request.Message); }


            SetSession(SessionSetting.AdminSession, new AdminSessionDto(admin.Id, admin.Name, admin.TrueName, admin.JobNo));
            var setting = await _adminApplication.GetSettingValueByCode("CookieTimes");//cookie 保存时间
            int mins = 0;
            if (setting != null) { if (int.TryParse(setting.Value, out mins) == false) { mins = 180 * 60; } }
            if (mins < 600) { mins = 180 * 60; }
            string cookie = SetAdminCookieDto(CookieSetting.AdminCookie, CacheHelper.CreateAdminCookieDto(admin, mins));

            await _adminApplication.SetAdminCookie(admin.Id, cookie, request.PasswordString);
            CacheHelper.SetAdminSessionId(admin.Id, cookie, request.PasswordString);


            var rsaKey = RsaUtil.PrivateEncrypt(rsa.PrivateKey, request.PasswordString);
            return Success(new { RsaKey = rsaKey });
        }



        [HttpGet("/Ajax/GetCurrTime")]
        public IActionResult GetCurrTime(double st)
        {
            var st2 = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(st);
            return Success((int)st2.TotalMilliseconds);
        }

        [HttpGet("/admins/Logout")]
        public async Task<IActionResult> Logout()
        {
            var userDto = GetAdminCookieDto();
            if (userDto != null) {
                await _adminApplication.SetAdminCookie(userDto.UserId, "", null);
                CacheHelper.SetAdminSessionId(userDto.UserId, "", "");
            }

            DeleteCookie(CookieSetting.AdminCookie);
            DeleteCookie(CookieSetting.AdminCookieLogin);
            DeleteSession(SessionSetting.AdminSession);
            return Redirect(UrlSetting.AdminLoginUrl);
        }

        [HttpGet("/ajax/AddMachineCode")]
        public async Task<IActionResult> AddMachineCode(string code)
        {
            await _adminApplication.AddMachineCode(code);
            return Success();
        }


        private string SetAdminCookieDto(string key, AdminCookieDto dto)
        {
            var json = dto.ToJson(true);
            var bytes = ThreeRCX.Encrypt(json, RsaHelper.Instance.CookiePassword);
            var hash = HashUtil.GetMd5String(bytes);
            var cookieStr = Base64.ToBase64ForUrlString(bytes) + "." + hash;
            SetCookie(key, cookieStr, dto.ExpireTime);

            HttpContext.Response.Cookies.Append(CookieSetting.AdminCookieLogin, "1", new Microsoft.AspNetCore.Http.CookieOptions() {
                Path = "/",
                Expires = dto.ExpireTime,
                IsEssential = true,
                HttpOnly = false,
            });
            return cookieStr;
        }
        private AdminCookieDto GetAdminCookieDto()
        {
            var cookie = GetCookie(CookieSetting.AdminCookie);
            if (string.IsNullOrEmpty(cookie)) return null;
            var sp = cookie.Split(".");
            if (sp.Length != 2) return null;
            var bytes = Base64.FromBase64ForUrlString(sp[0]);
            var hash = HashUtil.GetMd5String(bytes);
            if (hash == sp[1]) {
                bytes = ThreeRCX.Encrypt(bytes, RsaHelper.Instance.CookiePassword);
                var json = Encoding.UTF8.GetString(bytes);
                try {
                    var userDto = json.ToObject<AdminCookieDto>();
                    if (userDto.ExpireTime < DateTime.Now || userDto.CreateTime > DateTime.Now) {
                        return null;
                    }
                    return userDto;

                } catch (Exception) { }
            }
            return null;
        }

    }

}
