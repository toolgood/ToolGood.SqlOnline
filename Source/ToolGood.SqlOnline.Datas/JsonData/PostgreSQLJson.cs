/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    public class PostgreSQLJson
    {
        /// <summary>
        /// Host/Server/IP Address
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port(optional)
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
        /// Reuse db connection across query executions 
        /// </summary>
        public bool MultiStatementTransactionEnabled { get; set; }
        /// <summary>
        /// Seconds to allow connection to be idle before closing
        /// </summary>
        public int? IdleTimeoutSeconds { get; set; }

        /// <summary>
        /// Use SSL boolean
        /// </summary>
        public bool PostgresSsl { get; set; }

        /// <summary>
        ///  Allow self-signed SSL certificate
        /// </summary>
        public bool PostgresSslSelfSigned { get; set; }
        /// <summary>
        /// Database Certificate Path
        /// </summary>
        public string postgresCert { get; set; }

        /// <summary>
        /// Database Key Path
        /// </summary>
        public string postgresKey { get; set; }
        /// <summary>
        /// Database CA Path 
        /// </summary>
        public string postgresCA { get; set; }

        ///// <summary>
        ///// Connect through SOCKS proxy
        ///// </summary>
        //public bool useSocks { get; set; }
        ///// <summary>
        ///// Proxy hostname 
        ///// </summary>
        //public string socksHost { get; set; }
        ///// <summary>
        ///// Proxy port
        ///// </summary>
        //public string socksPort { get; set; }
        ///// <summary>
        ///// Username for socks proxy 
        ///// </summary>
        //public string socksUsername { get; set; }
        ///// <summary>
        ///// Password for socks proxy
        ///// </summary>
        //public string socksPassword { get; set; }


    }
 

}
