/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    public class TrinoJson
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

        public string Catalog { get; set; }
        public string Schema { get; set; }


    }

}
