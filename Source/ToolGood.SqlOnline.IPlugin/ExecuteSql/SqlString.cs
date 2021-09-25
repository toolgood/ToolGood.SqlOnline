/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.IPlugin
{
    public class SqlString
    {
        public SqlString(string field, SqlToken token)
        {
            SqlToken = token;
            SqlField = field;
        }

        public SqlToken SqlToken { get; set; }
        public string SqlField { get; set; }

        public int Layer { get; set; }

        public bool IsBreak { get; set; }




    }


}
