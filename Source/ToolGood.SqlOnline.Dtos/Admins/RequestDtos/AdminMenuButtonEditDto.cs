/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using ToolGood.Attributes;

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminMenuButtonEditDto
    {
        public int Id { get; set; }

        [VaShortNameLength]
        public string ButtonCode { get; set; }

        [VaRequired]
        [VaShortNameLength]
        public string ButtonName { get; set; }

        public int OrderNum { get; set; }

    }

}
