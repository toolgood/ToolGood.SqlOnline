/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminGroupDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 管理组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述 200
        /// </summary>
        public string Comment { get; set; }

        public decimal OrderNum { get; set; }

        public bool IsSystem { get; set; }

        public DateTime AddingTime { get; set; }

        /// <summary>
        /// 管理员人数
        /// </summary>
        public int AdminCount { get; set; }

    }
}
