/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3.Attributes;

namespace ToolGood.SqlOnline.Datas
{
    [Table("SysMachineCode")]
    [Index("MachineCode")]
    public class DbSysMachineCode
    {
        public int Id { get; set; }

        /// <summary>
        /// MAC地址
        /// </summary>
        [FieldLength(64)]
        public string MachineCode { get; set; }

        public DateTime AddingTime { get; set; }
     
    }


}
