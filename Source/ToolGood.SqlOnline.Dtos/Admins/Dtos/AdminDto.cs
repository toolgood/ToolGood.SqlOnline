/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 真名
        /// </summary>
        public string TrueName { get; set; }

        public string JobNo { get; set; }


        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool IsFrozen { get; set; }

        public DateTime AddingTime { get; set; }


        public List<String> GroupNames { get; set; }

    }
}
