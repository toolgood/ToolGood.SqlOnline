/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using ToolGood.Attributes;

namespace ToolGood.SqlOnline.Dtos
{
    public class GetAdminListDto
    {
        [VaShortNameLength]
        public string Name { get; set; }
        [VaShortNameLength]
        public string TrueName { get; set; }
        [VaPhoneLength]
        public string Phone { get; set; }
        public int? GroupId { get; set; }
        public int? IsFrozen { get; set; }
        public string Field { get; set; }
        public string Order { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }



    }


}
