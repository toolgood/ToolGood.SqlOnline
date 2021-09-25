/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */

namespace ToolGood.WebCommon
{
    public interface IRequest
    {

        int OperatorId { get; set; }

        string OperatorName { get; set; }

        string Message { get; set; }

        /// <summary>
        /// 操作人密码
        /// </summary>
        string OperatorPassword { get; set; }

        int AdminModeTime { get; set; }


    }
}
