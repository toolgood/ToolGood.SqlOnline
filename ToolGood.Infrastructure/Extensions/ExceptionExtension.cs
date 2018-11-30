using System.Text;

namespace System
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// 获取错误异常信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="memberName">出现异常的方法名字</param>
        /// <returns>错误异常信息</returns>
        public static string ToErrMsg(this Exception ex, string memberName = null)
        {
            StringBuilder errorBuilder = new StringBuilder();
            bool flag = !string.IsNullOrWhiteSpace(memberName);
            if (flag) {
                errorBuilder.AppendFormat("CallerMemberName：{0}", memberName).AppendLine();
            }
            errorBuilder.AppendFormat("Message：{0}", ex.Message).AppendLine();
            bool flag2 = ex.InnerException != null;
            if (flag2) {
                bool flag3 = !string.Equals(ex.Message, ex.InnerException.Message, StringComparison.OrdinalIgnoreCase);
                if (flag3) {
                    errorBuilder.AppendFormat("InnerException：{0}", ex.InnerException.Message).AppendLine();
                }
            }
            errorBuilder.AppendFormat("Source：{0}", ex.Source).AppendLine();
            errorBuilder.AppendFormat("StackTrace：{0}", ex.StackTrace).AppendLine();
            //bool flag4 = WebUtil.IsHaveHttpContext();
            //if (flag4) {
            //    errorBuilder.AppendFormat("RealIP：{0}", WebUtil.GetRealIP()).AppendLine();
            //    errorBuilder.AppendFormat("HttpRequestUrl：{0}", WebUtil.GetHttpRequestUrl()).AppendLine();
            //    errorBuilder.AppendFormat("UserAgent：{0}", WebUtil.GetUserAgent()).AppendLine();
            //}
            return errorBuilder.ToString();
        }
    }
}
