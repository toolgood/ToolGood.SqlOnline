/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SysAdmin_MachineCode")]
    [Index("AdminId")]
    [Index("MachineCode")]
    public class DbSysAdmin_MachineCode
    {
        public int Id { get; set; }

        public int AdminId { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        [FieldLength(64)]
        public string MachineCode { get; set; }
    }


}
