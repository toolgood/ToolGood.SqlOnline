using Microsoft.AspNetCore.Mvc.Filters;
using ToolGood.Datas;
using ToolGood.DomainService;
using ToolGood.Infrastructure;
using ToolGood.Infrastructure.Dependency;
using ToolGood.TransDto;
using System;
using Microsoft.AspNetCore.Mvc;

namespace ToolGood.ApplicationCommon
{
    [AdminAuthController]
    public class AdminBaseController : BaseController
    {
        private string loginUrl = KeywordString.AdminLoginUrl;
        private AdminDto _admin;
        protected AdminDto Admin {
            get {
                return _admin;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var request = context.HttpContext.Request;

            using (IIocScopeResolve resolver = ContainerManager.BeginLeftScope()) {
                IAdminService adminService = resolver.Resolve<IAdminService>();

                if (request.Cookies.TryGetValue(KeywordString.AdminCookie, out string cookie)) {
                    var token = adminService.GetToken(cookie);
                    if (token != null) {
                        if (token.ExpiryTime > DateTime.Now) {
                            if (token.UpdateTime < DateTime.Now) {
                                if (adminService.CheckAdmin(token)) {
                                    token.UpdateTime = DateTime.Now.AddMinutes(5);
                                    SetCookie(KeywordString.AdminCookie, adminService.CreateToken(token), token.ExpiryTime);
                                }
                            }  
                        } else {
                            token = null;
                        }
                    }
                    if (token==null) {
                        UnAuthorizationWork(context);
                    } else {
                        _admin = token;
                        ViewData["admin"]=_admin;
                    }
                }

            }
        }




        private void UnAuthorizationWork(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (request.Method.ToUpper() == "GET") {
                var url = string.Format(loginUrl, request.Path);
                filterContext.Result = JumpTopUrl(url);
            } else {
                var json = new JsonResult(new { code = KeywordString.ErrorCode, msg = "请重新登入后台。" });
                filterContext.Result = json;
            }
        }

        //private IActionResult JumpTopUrl(string url)
        //{
        //    var content = new ContentResult();
        //    content.Content = string.Format("<script>top.location.href='{0}'</script>", url);
        //    content.ContentType = "text/html";
        //    return content;
        //}
    }
}
