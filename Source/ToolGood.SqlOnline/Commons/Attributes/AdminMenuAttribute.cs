/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;

namespace ToolGood.SqlOnline
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AdminMenuAttribute : Attribute
    {
        public string MenuCode { get; private set; }
        public string ButtonCode { get; private set; }


        public AdminMenuAttribute(string menuCode, string buttonCode)
        {
            MenuCode = menuCode.Trim();
            ButtonCode = buttonCode.ToLower().Trim();
        }
    }
}
