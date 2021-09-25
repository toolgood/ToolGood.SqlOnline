/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using Microsoft.AspNetCore.Mvc.Filters;
using ToolGood.WebCommon.Utils;

namespace ToolGood.WebCommon
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.HttpContext.Items.ContainsKey("ToolGood.Bedrock.QueryArgsBase")) {
                LogUtil.QueryArgs = context.HttpContext.Items["ToolGood.Bedrock.QueryArgsBase"] as QueryArgs;
                //ActionResultUtil.QueryArgs = LogUtil.QueryArgs;
            }
            LogUtil.Error(context.Exception);

            if (context.HttpContext.Request.Method.ToLower() == "post") {
                context.Result = ActionResultUtil.Error("系统出了个小差！");
            }

        }
    }
}
