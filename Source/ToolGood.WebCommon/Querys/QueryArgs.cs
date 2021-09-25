/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using Microsoft.AspNetCore.Http;

namespace ToolGood.WebCommon
{
    /// <summary>
    /// 查询参数
    /// </summary>
    public class QueryArgs
    {
        public QueryArgs() { }

        internal HttpContext HttpContext;

        public void SetHttpContext(HttpContext value) { HttpContext = value; }

    }




}
