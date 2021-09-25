/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    public class SqlServerJson
    {

        /// <summary>
        /// Host/Server/IP Address
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port(e.g. 39015)
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Database Username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Database Password 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Database
        /// </summary>
        public string Database { get; set; }
        /// <summary>
        /// Domain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Encrypt(necessary for Azure)
        /// </summary>

        public bool SqlserverEncrypt { get; set; }
        /// <summary>
        /// MultiSubnetFailover
        /// </summary>
        public bool SqlserverMultiSubnetFailover { get; set; }
        /// <summary>
        /// ReadOnly Application Intent
        /// </summary>
        public bool ReadOnlyIntent { get; set; }

    }
}
