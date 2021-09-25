/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Configs;
using ToolGood.WebCommon.Utils;

namespace ToolGood.WebCommon.Controllers
{
    public abstract class PageModelBaseCore : PageModel
    {
        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            HttpContextHelper.BuildQueryArgs(context.HttpContext, context.HandlerArguments);
            base.OnPageHandlerExecuting(context);
        }
        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            base.OnPageHandlerExecuted(context);
            Config.Dispose();
        }


        #region Success
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        protected IActionResult Success(object obj)
        {
            return ActionResultUtil.Success(obj);
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objs"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        protected IActionResult Success<T>(List<T> objs)
        {
            return ActionResultUtil.Success(objs);
        }


        /// <summary>
        /// 返回成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        protected IActionResult Success<T>(Page<T> page)
        {
            return ActionResultUtil.Success(page);
        }


        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        protected IActionResult Success(string msg = "SUCCESS")
        {
            return ActionResultUtil.Success(msg);
        }

        #endregion

        #region Error

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        protected IActionResult Error(string msg = "ERROR")
        {
            return ActionResultUtil.Error(msg);
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        protected IActionResult Error(int code, string msg)
        {
            return ActionResultUtil.Error(code, msg);
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="usePassword"></param>
        /// <returns></returns>
        protected IActionResult Error(object obj)
        {
            return ActionResultUtil.Error(obj);
        }



        #endregion

        #region Other CreatePassword GetUserAgent MapPath
        /// <summary>
        /// 获取 UserAgent
        /// </summary>
        /// <returns></returns>
        protected string GetUserAgent()
        {
            return HttpContext.Request.Headers[HeaderNames.UserAgent].ToString();
        }

        /// <summary>
        /// 获取文件绝对路径
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        protected string MapPath(string path)
        {
            return MyHostingEnvironment.MapPath(path);
        }
        /// <summary>
        /// 获取文件绝对路径 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected string MapWebRootPath(string path)
        {
            return MyHostingEnvironment.MapWebRootPath(path);
        }
        #endregion

        #region Session
        /// <summary>
        /// 获取 SessionId
        /// </summary>
        /// <returns></returns>
        protected string GetSessionId()
        {
            return HttpContext.GetSessionId();
        }
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        protected void SetSession(string key, string val)
        {
            HttpContext.SetSession(key, val);
        }
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void SetSession(string key, object value)
        {
            HttpContext.SetSession(key, value);
        }
        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetSession(string key)
        {
            return HttpContext.GetSession(key);
        }
        /// <summary>
        /// 判断session是否存在key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected bool HasSession(string key)
        {
            return HttpContext.HasSession(key);
        }
        /// <summary>
        /// 获取Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        protected T GetSession<T>(string key)
        {
            return HttpContext.GetSession<T>(key);
        }
        /// <summary>
        /// 依据key删除Session
        /// </summary>
        /// <param name="key"></param>
        protected void DeleteSession(string key)
        {
            HttpContext.DeleteSession(key);
        }

        /// <summary>
        /// 核对Session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        protected bool CheckSession(string key, string val)
        {
            return HttpContext.CheckSession(key, val);
        }
        #endregion

        #region Cookie 操作
        /// <summary>
        /// 获取 Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetCookie(string key)
        {
            return HttpContext.GetCookie(key);
        }
        /// <summary>
        /// 设置 Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        protected void SetCookie(string key, string val)
        {
            HttpContext.SetCookie(key, val);
        }
        /// <summary>
        /// 设置 Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="minutes"></param>
        protected void SetCookie(string key, string val, int minutes)
        {
            HttpContext.SetCookie(key, val, minutes);
        }
        /// <summary>
        /// 设置 Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="dateTime"></param>
        protected void SetCookie(string key, string val, DateTime dateTime)
        {
            HttpContext.SetCookie(key, val, dateTime);
        }
        /// <summary>
        /// 依据cookieName 删除 cookie
        /// </summary>
        /// <param name="cookieName"></param>
        protected void DeleteCookie(string cookieName)
        {
            HttpContext.DeleteCookie(cookieName);

        }
        /// <summary>
        /// 判断  Cookie是否包含cookieName 
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>
        protected bool HasCookie(string cookieName)
        {
            return HttpContext.HasCookie(cookieName);
        }
        #endregion

        #region JumpUrl
        /// <summary>
        /// 跳转Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected IActionResult JumpTopUrl(string url)
        {
            return ActionResultUtil.JumpTopUrl(url);
        }
        /// <summary>
        /// 跳转Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected IActionResult JumpUrl(string url)
        {
            return ActionResultUtil.JumpUrl(url);
        }
        #endregion


        #region 获取真ip
        /// <summary>
        /// 获取真ip
        /// </summary>
        /// <returns></returns>
        protected string GetRealIP()
        {
            return HttpContext.GetRealIP();
        }
        #endregion



    }

}
