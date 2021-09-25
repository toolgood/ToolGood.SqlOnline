/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using ToolGood.Attributes;

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminLoginDto
    {
        [VaUserName]
        [VaRequired]
        public string TName { get; set; }

        [VaPasswordLength]
        [VaRequired]
        public string TPwd { get; set; }

        [VaRequired]
        public string Vcode { get; set; }

        public string MachineCode { get; set; }

    }



}
