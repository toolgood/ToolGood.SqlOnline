/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Configs;

namespace ToolGood.WebCommon.Controllers
{
    public abstract class ApiControllerCore : WebControllerBaseCore
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!this.ModelState.IsValid) {
                context.Result = Error(ModelState);
                return;
            }
            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            Config.Dispose();
        }

        protected IActionResult LayuiSuccess<T>(Page<T> page, string password)
        {
            return ActionResultUtil.LayuiSuccess(page, password);
        }

        protected IActionResult LayuiSuccess<T>(List<T> page, string password)
        {
            return ActionResultUtil.LayuiSuccess(page, password);
        }

        protected IActionResult LayuiSuccess(IDictionary dictionary, string password)
        {
            return ActionResultUtil.LayuiSuccess(dictionary, password);
        }

        protected IActionResult LayuiError(string msg)
        {
            return ActionResultUtil.LayuiError(msg);
        }
    }

}
