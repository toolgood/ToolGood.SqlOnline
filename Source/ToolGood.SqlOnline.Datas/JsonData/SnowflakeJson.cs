/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    public class SnowflakeJson
    {
        public string Account { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Warehouse { get; set; }
        public string Database { get; set; }
        public string Schema { get; set; }
        public string Role { get; set; }
        /// <summary>
        /// Pre-query statements
        /// </summary>
        public string PreQueryStatements { get; set; }

    }
}
