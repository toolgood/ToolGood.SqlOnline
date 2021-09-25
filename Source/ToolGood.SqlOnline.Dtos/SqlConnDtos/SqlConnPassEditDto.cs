/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;

namespace ToolGood.SqlOnline.Dtos
{
    public class SqlConnPassEditDto
    {
        public int AdminGroupId { get; set; }

        public List<SqlConnPassEditItemDto> p { get; set; }

    }
}
