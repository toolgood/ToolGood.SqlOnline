/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminSimpleDto
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

        public string Phone { get; set; }

    }
}
