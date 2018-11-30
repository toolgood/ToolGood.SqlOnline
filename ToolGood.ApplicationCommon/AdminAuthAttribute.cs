using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;
using ToolGood.DomainService;
using ToolGood.Infrastructure;
using ToolGood.Infrastructure.Dependency;

namespace ToolGood.ApplicationCommon
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AdminAuthAttribute : ActionFilterAttribute, IAuthorizationFilter, IActionFilter
    {
        public string Code { get; private set; }
        public string ActionName { get; private set; }
        public List<string> Conditions { get; private set; }

        #region 构造方法
        public AdminAuthAttribute(string code, string action)
        {
            Code = code;
            ActionName = action.ToLower();
        }

        public AdminAuthAttribute(string code, ActionPermissionType action)
        {
            Code = code;
            ActionName = action.ToString();
        }

        private List<string> SplitCondition(string condition)
        {
            List<string> list = new List<string>();
            bool isInText = false;
            bool isOperator = false;
            bool isEscape = false;
            char textChar = '\"';
            var txt = "";

            foreach (var item in condition) {
                if (isEscape) {
                    if (item == 't') {
                        txt += '\t';
                    } else if (item == 'r') {
                        txt += '\r';
                    } else if (item == 'n') {
                        txt += '\n';
                    } else if (item == 'f') {
                        txt += '\f';
                    } else if (item == 'v') {
                        txt += '\v';
                    } else if (item == 'b') {
                        txt += '\b';
                    } else if (item == 'a') {
                        txt += '\a';
                    } else {
                        txt += item;
                    }
                    isEscape = false;
                    continue;
                } else if (isInText) {
                    if (item == textChar) {
                        list.Add(txt + item);
                        txt = "";
                        isInText = false;
                        continue;
                    }
                    if (item == '\\') {
                        isEscape = true;
                        continue;
                    }
                } else if (isOperator) {
                    if ("<>!=".Contains(item) == false) {
                        list.Add(txt);
                        txt = item.ToString();
                        isOperator = false;
                        continue;
                    }
                } else if ("<>!=".Contains(item)) {
                    isOperator = true;
                    list.Add(txt);
                    txt = item.ToString();
                    continue;
                } else if (item == '\'' || item == '"') {
                    textChar = item;
                    list.Add(txt);
                    txt = item.ToString();
                    isInText = true;
                    continue;
                } else if (item == ',' || item == ';') {
                    if (string.IsNullOrEmpty(txt) == false) list.Add(txt);
                    txt = "";
                    continue;
                }
                txt += item;
            }
            if (string.IsNullOrEmpty(txt) == false) {
                list.Add(txt);
            }
            return list;
        }
        #endregion

        #region UsedAuthorization
        private bool UsedAuthorization(FilterContext filterContext)
        {
            if (Conditions == null) return true;

            var Request = filterContext.HttpContext.Request;
            for (int i = 0; i < Conditions.Count; i += 3) {
                var key = Conditions[i];
                var op = Conditions[i + 1];
                var value = Conditions[i + 2];
                var v = Request.Query[key];
                if (string.IsNullOrEmpty(v)) {
                    v = Request.Form[key];
                }
                if (Contrast(v, op, value) == false) return false;
            }
            return true;
        }
        public bool Contrast(string v, string op, string value)
        {
            if (string.IsNullOrEmpty(value) || value.ToLower() == "null") {
                if (op == "=" || op == "==") return string.IsNullOrEmpty(v);
                return string.IsNullOrEmpty(v) == false;
            }
            if (op == "=" || op == "==") return v == value;
            if (op == "!=" || op == "<>") return v != value;
            decimal a, b;

            if (decimal.TryParse(v, out a) && decimal.TryParse(value, out b)) {
                if (op == ">") return a > b;
                if (op == ">=") return a >= b;
                if (op == "<") return a < b;
                if (op == "<=") return a <= b;
            }
            throw new InvalidOperationException();
        }
        #endregion


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (UsedAuthorization(context) == false) return;

            var request = context.HttpContext.Request;
            using (IIocScopeResolve resolver = ContainerManager.BeginLeftScope()) {
                IAdminService adminService = resolver.Resolve<IAdminService>();
                if (request.Cookies.TryGetValue(KeywordString.AdminCookie, out string cookie)) {
                    var admin = adminService.GetToken(cookie);

                    if (admin == null || admin.Id == 0) {
                        UnAuthorizationWork(context);
                        return;
                    }
                    if (ActionCheck.IsAdminPass(Code, ActionName, admin.GroupId) == false) {
                        UnAuthorizationWork(context);
                    } else {
                        context.RouteData.DataTokens.Add("DataTokens_Admin_Auth", "pass");
                    }

                } else {
                    UnAuthorizationWork(context);
                    return;
                }
            }
        }


        private void UnAuthorizationWork(FilterContext filterContext)
        {
            ActionResult result;
            if (filterContext.HttpContext.Request.Method.ToUpper() == "GET") {
                var url = string.Format(KeywordString.AdminErrorUrl, System.Web.HttpUtility.UrlDecode("您没有权限！"));
                result = new ContentResult() {
                    Content = @"<!DOCTYPE html>
<html>
<head>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta charset=""utf-8"">
    <title>401</title>
    <style type=""text/css"">
        .page-404 { color: #afb5bf; padding-top: 60px; padding-bottom: 90px; text-align: center; padding-right: 15px; padding-left: 15px; margin-right: auto; margin-left: auto; }
        .error-title { font-size: 80px; }
        .error-description { font-size: 24px; }
        p { margin-bottom: 10px; }
    </style>
</head>
<body>
    <div class=""page-404"">
        <p class=""error-title"">
            <span class=""va-m""> 401</span>
        </p>
        <p class=""error-description"">不好意思，您访问的页面暂无权限~</p>
    </div>
</body>
</html>
",
                    //Content = $"<script>top.location.href='{url}'</script>",
                    ContentType = "text/html"
                };
            } else {
                result = new JsonResult(new { code = KeywordString.ErrorCode, msg = "您没有权限！" });

            }
            if (filterContext is AuthorizationFilterContext) {
                ((AuthorizationFilterContext)filterContext).Result = result;
            } else {
                ((ActionExecutingContext)filterContext).Result = result;
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RouteData.DataTokens.ContainsKey("DataTokens_Admin_Auth") == false) {
                UnAuthorizationWork(filterContext);
            } else {
                ((Controller)filterContext.Controller).ViewData["DataTokens_Admin_Code"] = Code;
                ((Controller)filterContext.Controller).ViewData["DataTokens_Admin_ActionName"] = ActionName;
            }
        }



    }
}
