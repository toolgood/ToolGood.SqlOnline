/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using ToolGood.Attributes;

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminModeDto
    {
        [ShowName("密码")]
        [VaPasswordLength]
        public string OperatorPassword { get; set; }

        public int AdminModeTime { get; set; }
    }


}
