/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    public class MySQLJson
    {
        /// <summary>
        /// Reuse db connection across query executions
        /// </summary>
        public bool MultiStatementTransactionEnabled { get; set; }
        /// <summary>
        /// Host/Server/IP Address
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Database Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Database text
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Use SSL
        /// </summary>
        public bool MysqlSsl { get; set; }
        /// <summary>
        /// Use old/insecure pre 4.1 Auth System
        /// </summary>
        public bool MysqlInsecureAuth { get; set; }


    }

     

}
