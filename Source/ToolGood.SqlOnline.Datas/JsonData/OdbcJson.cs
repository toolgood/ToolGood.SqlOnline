/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    public class OdbcJson
    {
        /// <summary>
        /// Reuse db connection across query executions
        /// </summary>
        public bool multiStatementTransactionEnabled { get; set; }
        /// <summary>
        /// Seconds to allow connection to be idle before closing 
        /// </summary>
        public int? idleTimeoutSeconds { get; set; }
        /// <summary>
        /// ODBC connection string
        /// </summary>
        public string connection_string { get; set; }
        /// <summary>
        /// Database SQL to lookup schema(optional, if omitted default to checking INFORMATION_SCHEMA)
        /// </summary>
        public string schema_sql { get; set; }

        /// <summary>
        /// Username(optional). Will be added to connect_string as Uid key
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password(optional). Will be added to connect_string as Pwd key
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Comma separated list of limit strategies used to restrict queries.
        /// These strategies will be used to enforce and inject LIMIT and FETCH FIRST use in SELECT queries.
        /// Allowed strategies are limit, fetch, first, and top.
        /// Example: limit, fetch
        /// </summary>
        public string limit_strategies { get; set; }

 
    }
}
