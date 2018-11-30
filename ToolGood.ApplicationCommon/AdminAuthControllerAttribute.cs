using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using ToolGood.Infrastructure;

namespace ToolGood.ApplicationCommon
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AdminAuthControllerAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        private string loginUrl = KeywordString.AdminLoginUrl;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuth = false;

            if (context.HttpContext.Request.Cookies.TryGetValue(KeywordString.AdminCookie, out string bs)) {
                isAuth = true;
            }
            if (isAuth == false) {
                UnAuthorizationWork(context);
            }
        }
    


        private void UnAuthorizationWork(AuthorizationFilterContext filterContext)
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

        protected ActionResult JumpTopUrl(string url)
        {
            var content = new ContentResult();
            content.Content = string.Format("<script>top.location.href='{0}'</script>", url);
            content.ContentType = "text/html";
            return content;
        }

    }
}
