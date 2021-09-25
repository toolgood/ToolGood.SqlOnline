/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    public class RedshiftJson
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
        /// Use SSL
        /// </summary>
        public bool Ssl { get; set; }
        /// <summary>
        /// Database Certificate Path 
        /// </summary>
        public string certPath { get; set; }
        /// <summary>
        /// Database Key Path
        /// </summary>
        public string keyPath { get; set; }
        /// <summary>
        /// Database CA Path
        /// </summary>
        public string caPath { get; set; }


    }
 

}
