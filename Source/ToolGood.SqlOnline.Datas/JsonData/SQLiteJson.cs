/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    public class SQLiteJson
    {
        /// <summary>
        /// Reuse db connection across query executions
        /// </summary>
        public bool MultiStatementTransactionEnabled { get; set; }
        /// <summary>
        /// Seconds to allow connection to be idle before closing
        /// </summary>
        public int? IdleTimeoutSeconds { get; set; }
        /// <summary>
        /// Path to file
        /// </summary>
        public string Filename { get; set; }
        /// <summary>
        /// Open file in read only mode 
        /// </summary>
        public string Readonly { get; set; }
 
    }
}
