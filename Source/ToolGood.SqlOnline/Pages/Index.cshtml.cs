/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Configs;
using ToolGood.WebCommon.Controllers;
using ToolGood.WebCommon.Utils;

namespace ToolGood.SqlOnline.Pages
{
    public class IndexModel : PageModelBaseCore
    {
        public string RsaExponent { get; private set; }
        public string RsaModulus { get; private set; }
        public bool UseDevelopment { get; private set; }
        private readonly IAdminApplication _adminApplication;



        public IndexModel(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        public async Task<IActionResult> OnGetAsync()
        {

            GetPrivateKey();

            #region 防止网页后退--禁止缓存
            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
            Response.Headers.Add("Pragma", "no-cache"); // HTTP 1.0.
            Response.Headers.Add("Expires", "-1"); // Proxies.
            #endregion

            DeleteCookie(CookieSetting.AdminCookie);
            DeleteCookie(CookieSetting.AdminCookieLogin);
            DeleteSession(SessionSetting.AdminSession);

            var settingValue =await _adminApplication.GetSettingValueByCode("UseDevelopment");
            if (settingValue != null && settingValue.Value=="1") {
                UseDevelopment = true;
            }

            var rsa = RsaHelper.Instance;
            RsaExponent = rsa.RsaExponent;
            RsaModulus = rsa.RsaModulus;
            return Page();
        }


        private void GetPrivateKey()
        {
            var rsa = RsaHelper.Instance;
            RsaExponent = rsa.RsaExponent;
            RsaModulus = rsa.RsaModulus;
        }

    }
}
