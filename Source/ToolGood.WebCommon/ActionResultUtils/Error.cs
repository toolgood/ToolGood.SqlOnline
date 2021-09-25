/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToolGood.WebCommon
{
    public static partial class ActionResultUtil
    {

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static IActionResult Error(ModelStateDictionary ms)
        {
            List<string> sb = new List<string>();
            //获取所有错误的Key
            List<string> Keys = ms.Keys.ToList();
            //获取每一个key对应的ModelStateDictionary
            foreach (var key in Keys) {
                var errors = ms[key].Errors.ToList();
                //将错误描述添加到sb中
                foreach (var error in errors) {
                    sb.Add(error.ErrorMessage);
                }
            }
            QueryResult result = new QueryResult() {
                Code = ErrorCode,
                Message = string.Join(",", sb),
                State = "ERROR",
            };

            return CamelCaseJson(result);
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        public static IActionResult Error(string msg = "ERROR")
        {
            QueryResult result = new QueryResult() {
                Code = ErrorCode,
                Message = msg,
                State = "ERROR",
            };

            return CamelCaseJson(result);
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        public static IActionResult Error(int code, string msg)
        {
            QueryResult result = new QueryResult() {
                Code = code,
                Message = msg,
                State = "ERROR",
            };

            return CamelCaseJson(result);
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        public static IActionResult Error(object obj)
        {
            QueryResult result = new QueryResult() {
                Code = ErrorCode,
                Data = obj,
                State = "ERROR",
            };
            return CamelCaseJson(result);
        }




    }
}
