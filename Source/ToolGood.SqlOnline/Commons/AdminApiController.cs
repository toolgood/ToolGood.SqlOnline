/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToolGood.Common;
using ToolGood.Common.Extensions;
using ToolGood.RcxCrypto;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Configs;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;
using ToolGood.WebCommon.Controllers;
using ToolGood.WebCommon.Utils;

namespace ToolGood.SqlOnline
{
    [AutoValidateAntiforgeryToken]
    public abstract class AdminApiController : ApiControllerCore
    {
        protected AdminSessionDto AdminDto { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var adminApplication = MyIoc.Create<IAdminApplication>();

            #region 检测登录，cookie登录
            AdminSessionDto adminSessionDto = context.GetSession<AdminSessionDto>(SessionSetting.AdminSession);
            //if (adminSessionDto == null) {
            //    var userDto = GetAdminCookieDto(context);
            //    if (userDto != null && userDto.ExpireTime > DateTime.Now) {
            //        if (CacheHelper.CheckAdminSessionId(userDto.UserId, context.GetCookie(CookieSetting.AdminCookie))) {
            //            var admin = adminApplication.GetAdminById(userDto.UserId).RunSync();
            //            if (null != admin && admin.IsFrozen == 0) {
            //                bool pwd = HashUtil.GetMd5String(admin.Password) == userDto.PasswordHash;
            //                if (pwd) {
            //                    adminSessionDto = new AdminSessionDto(admin.Id, admin.Name, admin.TrueName, admin.JobNo);
            //                    context.SetSession(SessionSetting.AdminSession, adminSessionDto);
            //                }
            //            }
            //        }
            //    }
            //}
            if (null == adminSessionDto) {
                if (context.HttpContext.Request.Method.ToUpper() == "GET") {
                    var url = UrlSetting.AdminLoginUrl;
                    context.Result = ActionResultUtil.JumpTopUrl(url, "cookie无效，请先登录！");
                } else {
                    context.Result = ActionResultUtil.Error();
                }
                return;
            }
            #endregion

            #region 检测菜单权限
            var menus = this.GetType().GetCustomAttributes<AdminMenuAttribute>(true);
            List<AdminMenuAttribute> adminMenus = new List<AdminMenuAttribute>();
            if (menus.Count() > 0) {
                foreach (var item in menus) {
                    var isPass = CacheHelper.AdminMenuButtonCache.GetOrAdd(adminSessionDto.Id + "-" + item.MenuCode + "-" + item.ButtonCode, () => {
                        return adminApplication.IsPass(adminSessionDto.Id, item.MenuCode, item.ButtonCode).RunSync();
                    });
                    if (isPass) {
                        adminMenus.Add(item);
                    }
                }
                if (adminMenus.Count == 0) {
                    context.Result = new RedirectResult(UrlSetting.AdminNoAccessUrl);
                    return;
                }
                ViewData["MenuCode"] = adminMenus[0].MenuCode;
                ViewData["ButtonCode"] = adminMenus[0].ButtonCode;
            }
            #endregion


            base.OnActionExecuting(context);

            if (context.Result != null) { return; }

            #region 检测参数是否正常
            AdminDto = adminSessionDto;
            var rsaHelper = RsaHelper.Instance;

            IRequest temp = null;
            foreach (var item in context.ActionArguments) {
                if (item.Value is EncryptedQueryArgs rsaData) {
                    rsaData.PasswordString = CacheHelper.GetBrowserPassword(AdminDto.Id);
                    if (rsaData.CheckSign(rsaHelper.RsaModulus, rsaHelper.RsaExponent, out string msg) == false) {
                        DeleteCookie(CookieSetting.AdminCookieLogin); 
                        context.Result = Error(msg); 
                        return;
                    }
                    if (rsaData.DecryptData() == false) { context.Result = Error("数据错误!"); return; }
                    if (rsaData.CheckData(out string msg2) == false) { context.Result = Error(msg2); return; }
                    if (rsaData is IRequest adminRequest) {
                        adminRequest.OperatorId = AdminDto.Id;
                        adminRequest.OperatorName = AdminDto.TrueName;
                        temp = adminRequest;
                    }
                }
            }
            #endregion

            #region 检测是否启用管理模式
            if (adminSessionDto.IsAdminMode() == false) {
                foreach (var item in adminMenus) {
                    var useCheck = CacheHelper.AdminMenuCheckCache.GetOrAdd(item.MenuCode + "-" + item.ButtonCode, () => {
                        return adminApplication.GetMenuCheck(item.MenuCode, item.ButtonCode).RunSync();
                    });
                    if (useCheck) {
                        if (context.HttpContext.Request.Method.ToUpper() == "GET") {
                            var urlP = System.Web.HttpUtility.UrlEncode(context.HttpContext.Request.Path.ToSafeString() + context.HttpContext.Request.QueryString.ToSafeString());
                            var url = UrlSetting.AdminModeUrl + urlP;// $"/admin/tools/AdminMode?url={urlP}";
                            context.Result = ActionResultUtil.JumpUrl(url);
                            return;
                        } else {
                            if (temp == null) {
                                context.Result = ActionResultUtil.Error("TryAdminMode");
                                return;
                            }
                            if (string.IsNullOrEmpty(temp.OperatorPassword)) {
                                context.Result = ActionResultUtil.Error("TryAdminMode");
                                return;
                            }
                            if (adminApplication.CheckPassword(temp.OperatorId, temp.OperatorPassword, temp).RunSync() == false) {
                                context.Result = ActionResultUtil.Error("密码不正确");
                                return;
                            }
                            AdminDto.SetAdminMode(DateTime.Now.AddMinutes(temp.AdminModeTime));
                            SetSession(SessionSetting.AdminSession, AdminDto);
                            break;
                        }
                    }
                }
            }

            #endregion


        }


        private AdminCookieDto GetAdminCookieDto(ActionExecutingContext context)
        {
            var cookie = context.GetCookie(CookieSetting.AdminCookie);
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

                } catch { }
            }
            return null;
        }


        protected string DecryptKey(string rsaKey)
        {
            var rsaHelper = RsaHelper.Instance;
            return RsaUtil.PrivateDecrypt(rsaHelper.PrivateKey, rsaKey);
        }


    }
}
