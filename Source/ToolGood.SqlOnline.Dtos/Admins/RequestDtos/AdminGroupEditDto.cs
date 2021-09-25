/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using ToolGood.Attributes;

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminGroupEditDto
    {

        public int Id { get; set; }
        [VaNameLength]
        [VaRequired]
        public string Name { get; set; }
        [VaCommentLength]
        public string Comment { get; set; }
        public int OrderNum { get; set; }

        public List<AdminMenuDto> Menus { get; set; }

        /// <summary>
        /// 操作人密码
        /// </summary>

    }



}
