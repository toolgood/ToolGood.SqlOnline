/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ToolGood.SqlOnline.Datas;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Application.Admins
{
    public class AdminLogApplication : ApplicationBase, IAdminLogApplication
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminLogApplication(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region 日志操作 写
        /// <summary>
        /// 写登录日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isSuccess"></param>
        /// <param name="request"></param>
        public Task WriteLoginLog(string message,string machineCode, bool isSuccess, IRequest request)
        {
            var sessionId = _httpContextAccessor.HttpContext.GetSessionId();
            var ip = _httpContextAccessor.HttpContext.GetRealIP();
            var userAgent = _httpContextAccessor.HttpContext.GetUserAgent();

            var helper = GetWriteHelper();
            return helper.Insert_Async(new DbSysAdminLoginLog() {
                Name = request.OperatorName,
                Message = message,
                SessionID = sessionId,
                AddingTime = DateTime.Now,
                Ip = ip,
                Success = isSuccess,
                UserAgent = userAgent,
                MachineCode= machineCode,
            });
        }

        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="operatorId"></param>
        /// <param name="operatorName"></param>
        public Task WriteOperationLog(string message, int operatorId, string operatorName)
        {
            return Task.CompletedTask;
            //var sessionId = _httpContextAccessor.HttpContext.GetSessionId();
            //var ip = _httpContextAccessor.HttpContext.GetRealIP();
            //var userAgent = _httpContextAccessor.HttpContext.GetUserAgent();

            //var helper = GetWriteHelper();
            //return helper.Insert_Async(new DbSysAdminOperationLog() {
            //    AdminId = operatorId,
            //    Name = operatorName,
            //    Message = message,
            //    AddingTime = DateTime.Now,
            //    UserAgent = userAgent,
            //    SessionID = sessionId,
            //    Ip = ip,
            //});
        }

        public Task WriteOperationLog(string message, IRequest request)
        {
            return Task.CompletedTask;

            //try {
            //    var sessionId = _httpContextAccessor.HttpContext.GetSessionId();
            //    var ip = _httpContextAccessor.HttpContext.GetRealIP();
            //    var userAgent = _httpContextAccessor.HttpContext.GetUserAgent();

            //    var helper = GetWriteHelper();
            //    await helper.Insert_Async(new DbSysAdminOperationLog() {
            //        AdminId = request.OperatorId,
            //        Name = request.OperatorName,
            //        Message = message,
            //        AddingTime = DateTime.Now,
            //        UserAgent = userAgent,
            //        SessionID = sessionId,
            //        Ip = ip,
            //    });
            //} catch (Exception) { }
        }


        #endregion
    }
}
