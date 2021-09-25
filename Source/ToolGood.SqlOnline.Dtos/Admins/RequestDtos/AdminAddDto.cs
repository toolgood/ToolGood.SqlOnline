/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using ToolGood.Attributes;

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminAddDto
    {
        [VaRequired]
        [VaMinLength(2)]
        public string Name { get; set; }
        [VaRequired]
        [VaMinLength(2)]
        public string TrueName { get; set; }
        [VaPasswordLength]
        public string Password { get; set; }
        [VaPasswordLength]
        public string ManagerPassword { get; set; }

        public string JobNo { get; set; }


        [VaEmail]
        public string Email { get; set; }
        [VaPhone]
        public string Phone { get; set; }

        public List<int> Groups { get; set; }
    }

}
