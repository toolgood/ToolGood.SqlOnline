/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;

namespace ToolGood.SqlOnline.Configs
{
    public static class SystemSetting
    {
        /// <summary>
        /// 成功编码
        /// </summary>
        public readonly static int SuccessCode = 1;
        /// <summary>
        /// 失败编码
        /// </summary>
        public readonly static int ErrorCode = 0;

        public readonly static string SuccessStr = "SUCCESS";

        public readonly static string ErrorStr = "ERROR";

        #region EnvironmentType
        public readonly static EnvironmentType EnvironmentType =
#if DEBUG
            EnvironmentType.Debug
#else
            EnvironmentType.Release
#endif
            ;
        #endregion
    }
    [Flags]
    public enum EnvironmentType
    {
        None = 0,

        /// <summary>
        /// Debug
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Release
        /// </summary>
        Release = 2,
    }
}
