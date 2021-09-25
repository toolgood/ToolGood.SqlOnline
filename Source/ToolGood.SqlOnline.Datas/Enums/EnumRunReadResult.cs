/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.ComponentModel;

namespace ToolGood.SqlOnline.Datas.Enums
{
    /// <summary>
    /// 读操作结果类型
    /// </summary>
    public enum EnumRunReadResult
    {
        [Description("执行成功")]
        Success,

        [Description("执行失败")]
        Fail,

        [Description("无权")]
        NoRight
    }
}
