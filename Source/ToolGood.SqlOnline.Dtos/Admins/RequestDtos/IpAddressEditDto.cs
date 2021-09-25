/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using ToolGood.Attributes;

namespace ToolGood.SqlOnline.Dtos
{
    public class IpAddressEditDto
    {
        public int Id { get; set; }

        [ShowName("IP地址")]
        [VaRequired]
        [VaIpAddressEx]
        public string Ip { get; set; }

        [ShowName("名称")]
        [VaRequired]
        [VaShortNameLength]
        public string Name { get; set; }

        public bool IsDisable { get; set; }
    }

}
