﻿/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Dtos
{
    public class AdminChangePwdForceDto
    {
        public int AdminId { get; set; }

        public string NewPassword { get; set; }

        public string NewManagerPassword { get; set; }
    }

}
