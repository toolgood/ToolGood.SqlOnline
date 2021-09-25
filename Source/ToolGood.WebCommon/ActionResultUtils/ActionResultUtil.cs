/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Configs;

namespace ToolGood.WebCommon
{
    public static partial class ActionResultUtil
    {
        private static int SuccessCode { get { return SystemSetting.SuccessCode; } }
        private static int ErrorCode { get { return SystemSetting.ErrorCode; } }


        /// <summary>
        /// 首字母小写json
        /// </summary>
        /// <param name="data">原数据</param>
        /// <param name="ignoreNames">忽略的字段名</param>
        /// <returns></returns>
        private static IActionResult CamelCaseJson(object data)
        {
            var json = data.ToJson();
            return new ContentResult() {
                Content = json,
                StatusCode = 200,
                ContentType = "application/json"
            };
        }

    }
}
