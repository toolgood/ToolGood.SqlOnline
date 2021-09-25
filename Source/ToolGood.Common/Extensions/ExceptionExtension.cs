/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Text;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class ObjectExtension
    {
        #region 获取最底层异常
        /// <summary>
        /// 获取最底层异常
        /// </summary>
        public static Exception GetDeepestException(this Exception ex)
        {
            var innerException = ex.InnerException;
            var resultExcpetion = ex;
            while (innerException != null)
            {
                resultExcpetion = innerException;
                innerException = innerException.InnerException;
            }
            return resultExcpetion;
        }
        #endregion

        /// <summary>
        /// 获取错误异常信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="memberName">出现异常的方法名字</param>
        /// <param name="customMsg">自定义错误消息</param> 
        /// <returns>错误异常信息</returns>
        public static string ToErrMsg(this Exception ex, string memberName = null, string customMsg = null)
        {
            StringBuilder errorBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(memberName)) {
                errorBuilder.AppendFormat("CallerMemberName：{0}", memberName).AppendLine();
            }
            if (!string.IsNullOrWhiteSpace(customMsg)) {
                errorBuilder.AppendFormat("CustomMsg: {0}", customMsg ?? "").AppendLine();
            }
            errorBuilder.AppendFormat("Message：{0}", ex.Message).AppendLine();
            if (ex.InnerException != null) {
                if (!string.Equals(ex.Message, ex.InnerException.Message, StringComparison.OrdinalIgnoreCase)) {
                    errorBuilder.AppendFormat("InnerException：{0}", ex.InnerException.Message).AppendLine();
                }
            }
            errorBuilder.AppendFormat("Source：{0}", ex.Source).AppendLine();
            errorBuilder.AppendFormat("StackTrace：{0}", ex.StackTrace).AppendLine();
            //if (WebUtil.IsHaveHttpContext()) {
            //    errorBuilder.AppendFormat("RealIP：{0}", WebUtil.GetRealIP()).AppendLine();
            //    errorBuilder.AppendFormat("HttpRequestUrl：{0}", WebUtil.GetHttpRequestUrl()).AppendLine();
            //    errorBuilder.AppendFormat("UserAgent：{0}", WebUtil.GetUserAgent()).AppendLine();
            //}
            return errorBuilder.ToString();
        }

    }
}
