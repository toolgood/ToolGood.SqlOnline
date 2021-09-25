/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;

namespace ToolGood.SqlOnline.Dtos
{
    public class IpAddressDto
    {
        public int Id { get; set; }

        public string Ip { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool IsDisable { get; set; }

        public string IsDisableString {
            get {
                return IsDisable ? "禁用" : "启用";
            }
        }


        public DateTime ModifyTime { get; set; }
        public string ModifyAdminName { get; set; }


    }
}
