/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */


namespace ToolGood.SqlOnline.Dtos
{
    public class SqlCodeGenSearchDto
    {
        public int Id { get; set; }

        public int AdminId { get; set; }

        public int TplType { get; set; }

        public string Language { get; set; }

        public string Namespace { get; set; }


        public int SqlConnId { get; set; }
        public string Database { get; set; }
        public string Schema { get; set; }
        public string SearchType { get; set; }

        public string Search { get; set; }



        public string Field { get; set; }
        public string Order { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }


    }
}
