/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ToolGood.WebCommon.Utils
{
    internal class HttpContextHelper
    {
        public static QueryArgs BuildQueryArgs(HttpContext context, IDictionary<string, object> args)
        {
            foreach (var item in args) {
                var queryArgsBase = item.Value as QueryArgs;
                if (queryArgsBase != null) {
                    queryArgsBase.SetHttpContext(context);
                    LogUtil.QueryArgs = queryArgsBase;
                    //ActionResultUtil.QueryArgs = queryArgsBase;
                    return queryArgsBase;
                }
            }

            QueryArgs queryArgs = new QueryArgs();
            queryArgs.SetHttpContext(context);
            LogUtil.QueryArgs = queryArgs;
            //ActionResultUtil.QueryArgs = queryArgs;
            return queryArgs;
        }

    }
}
