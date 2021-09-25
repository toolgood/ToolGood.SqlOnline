/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using ToolGood.Common;
using ToolGood.RcxCrypto;
using ToolGood.ReadyGo3;

namespace ToolGood.WebCommon
{
    public static partial class ActionResultUtil
    {

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static IActionResult Success(object obj, string password)
        {
            QueryResult result = new QueryResult() {
                Code = SuccessCode,
                Data = obj,
                State = "SUCCESS",
            };
            if (string.IsNullOrEmpty(password)==false) {
                result.EncryptData(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return CamelCaseJson(result);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="objs"></param>
        /// <param name="ignoreNames"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        public static IActionResult Success(object objs)
        {
            QueryResult result = new QueryResult() {
                Code = SuccessCode,
                Data = objs,
                State = "SUCCESS",
            };

            //if (usePassword && QueryArgs != null && QueryArgs is EncryptedQueryArgs args) {
            //    result.EncryptData(args.Password);
            //}
            return CamelCaseJson(result);
        }


        /// <summary>
        /// 返回成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <param name="ignoreNames"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        public static IActionResult Success<T>(List<T> objs)
        {
            QueryResult result = new QueryResult() {
                Code = SuccessCode,
                Data = objs,
                State = "SUCCESS",
            };

            return CamelCaseJson(result);
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="ignoreNames"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        public static IActionResult Success<T>(Page<T> page)
        {
            QueryResult result = new QueryResult() {
                Code = SuccessCode,
                Data = page,
                State = "SUCCESS",
            };


            return CamelCaseJson(result);
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        public static IActionResult Success(string msg = "SUCCESS")
        {
            QueryResult result = new QueryResult() {
                Code = SuccessCode,
                State = "SUCCESS",
                Message = msg,
            };


            return CamelCaseJson(result);
        }
        public static IActionResult LayuiSuccess<T>(List<T> list)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = SuccessCode;
            data["message"] = "SUCCESS";
            data["state"] = "SUCCESS";
            data["data"] = list;


            return CamelCaseJson(data);
        }
        public static IActionResult LayuiSuccess(IDictionary dictionary, string password)
        {
            List<KeyValue> list = new List<KeyValue>();
            foreach (var key in dictionary.Keys) {
                list.Add(new KeyValue() {
                    Key = key.ToSafeString(),
                    Value = dictionary[key].ToSafeString()
                });
            }
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = SuccessCode;
            data["message"] = "SUCCESS";
            data["state"] = "SUCCESS";
            data["data"] = list;

            if (password != null && password.Length > 0) {
                var json = data["data"].ToJson();
                var bytes = Encoding.UTF8.GetBytes(json);

                var bs = RCX.Encrypt(bytes, password);//解密
                data["Ciphertext"] = Base64.ToBase64String(bs);
                data.Remove("data");
            }
            return CamelCaseJson(data);
        }
        public class KeyValue
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        public static IActionResult LayuiSuccess<T>(List<T> list, string password)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = SuccessCode;
            data["message"] = "SUCCESS";
            data["state"] = "SUCCESS";
            data["data"] = list;

            if (password != null && password.Length > 0) {
                var json = data["data"].ToJson();
                var bytes = Encoding.UTF8.GetBytes(json);

                var bs = RCX.Encrypt(bytes, password);//解密
                data["Ciphertext"] = Base64.ToBase64String(bs);
                data.Remove("data");
            }
            return CamelCaseJson(data);
        }
        public static IActionResult LayuiSuccess<T>(Page<T> page)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = SuccessCode;
            data["message"] = "SUCCESS";
            data["state"] = "SUCCESS";
            data["data"] = page.Items;
            data["count"] = page.TotalItems;


            return CamelCaseJson(data);
        }

        public static IActionResult LayuiSuccess<T>(List<T> page, int total, string password)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = SuccessCode;
            data["message"] = "SUCCESS";
            data["state"] = "SUCCESS";
            data["data"] = page;
            data["count"] = total;

            if (password != null && password.Length > 0) {
                var json = data["data"].ToJson();
                var bytes = Encoding.UTF8.GetBytes(json);

                var bs = RCX.Encrypt(bytes, password);//解密
                data["Ciphertext"] = Base64.ToBase64String(bs);
                data.Remove("data");
            }
            return CamelCaseJson(data);
        }

        public static IActionResult LayuiSuccess<T>(Page<T> page, string password)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = SuccessCode;
            data["message"] = "SUCCESS";
            data["state"] = "SUCCESS";
            data["data"] = page.Items;
            data["count"] = page.TotalItems;


            if (password != null && password.Length > 0) {
                var json = data["data"].ToJson();
                var bytes = Encoding.UTF8.GetBytes(json);

                var bs = RCX.Encrypt(bytes, password);//解密
                data["Ciphertext"] = Base64.ToBase64String(bs);
                data.Remove("data");
            }
            return CamelCaseJson(data);
        }


        public static IActionResult LayuiError(string msg)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["code"] = ErrorCode;
            data["state"] = "ERROR";
            data["message"] = msg;
            return CamelCaseJson(data);
        }


    }
}
