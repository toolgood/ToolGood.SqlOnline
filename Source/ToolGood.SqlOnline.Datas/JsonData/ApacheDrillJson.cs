/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    /// <summary>
    /// from https://getsqlpad.com/#/connections?id=cratedb
    /// </summary>
    public class ApacheDrillJson
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
        /// Default Schema
        /// </summary>
        public string DrillDefaultSchema { get; set; }

        /// <summary>
        /// Use SSL
        /// </summary>
        public bool Ssl { get; set; }

    }

}
