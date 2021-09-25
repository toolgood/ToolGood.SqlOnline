/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    public class MySQL2Json
    {
 
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
        /// Use old/insecure pre 4.1 Auth System
        /// </summary>
        public bool MysqlInsecureAuth { get; set; }
        /// <summary>
        /// Minimum TLS version to allow.One of: TLSv1.3, TLSv1.2, TLSv1.1, or TLSv1.
        /// </summary>
        public string MinTlsVersion { get; set; }
        /// <summary>
        /// Maximum TLS version to allow. see above for options
        /// </summary>
        public string MaxTlsVersion { get; set; }
        /// <summary>
        /// Do not validate servier certificate. (Don't use this for production)
        /// </summary>
        public bool MysqlSkipValidateServerCert { get; set; }

    }

     

}
