/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */

namespace ToolGood.SqlOnline.Dtos
{
    public class SqlSearchDto
    {
        public string Id { get; set; }

        public int AdminId { get; set; }

        public int SqlDocType { get; set; }


        public int SqlConnId { get; set; }
        public string SqlType { get; set; }

        public string Database { get; set; }

        public string Schema { get; set; }

        public string SearchType { get; set; }

        public string Search { get; set; }


    }

}
