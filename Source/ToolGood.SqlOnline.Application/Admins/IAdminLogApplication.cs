/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Threading.Tasks;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Application
{
    public interface IAdminLogApplication
    {
        /// <summary>
        /// 写登录日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="isSuccess"></param>
        /// <param name="request"></param>
        Task WriteLoginLog(string message, string machineCode, bool isSuccess, IRequest request);

        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="request"></param>
        Task WriteOperationLog(string message, IRequest request);
        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="operatorId"></param>
        /// <param name="operatorName"></param>
        /// <returns></returns>
        Task WriteOperationLog(string message, int operatorId, string operatorName);

    }
}
