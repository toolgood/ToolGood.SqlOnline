/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */

namespace ToolGood.SqlOnline.Plugin.Mysql
{
    public class SchemaInfo
    {
        public string Schema { get; set; }

        public string TName { get; set; }

        public string CName { get; set; }

        public string CType { get; set; }

        public string CComment { get; set; }

        public string CDefault { get; set; }

        public string CIsNull { get; set; }

        public string EXTRA { get; set; }
        public string CHARACTER_SET_NAME { get; set; }
        public string COLLATION_NAME { get; set; }


    }

}
