/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.ComponentModel;

namespace ToolGood.Attributes
{
    public class ShowNameAttribute : DisplayNameAttribute
    {
 

        public ShowNameAttribute(string displayName) : base(displayName)
        {
        }
    }


}
