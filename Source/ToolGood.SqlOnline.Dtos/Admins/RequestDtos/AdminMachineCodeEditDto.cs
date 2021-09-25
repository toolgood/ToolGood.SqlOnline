/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminMachineCodeEditDto
    {
        public int AdminId { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        public string MachineCode { get; set; }
    }
}
