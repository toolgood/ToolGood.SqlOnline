/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Datas.JsonData
{
    public class CassandraJson
    {
        /// <summary>
        ///  Contact points(comma delimited) 
        /// </summary>
        public string ContactPoints { get;set;}
        /// <summary>
        /// Local data center
        /// </summary>
        public string LocalDataCenter { get;set;}
        /// <summary>
        /// Keyspace
        /// </summary>
        public string Keyspace { get;set;}
    }

}
