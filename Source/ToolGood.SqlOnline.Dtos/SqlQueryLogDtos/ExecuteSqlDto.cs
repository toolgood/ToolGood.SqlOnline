/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Dto
{
    public class ExecuteSqlDto
    {
        public int AdminId { get; set; }

        public int SqlConnId { get; set; }

        public string Database { get; set; }

        public string Sql { get; set; }
        public string Key { get; set; }
        /// <summary>
        /// 0）select、1）insert/update、2）delete、3)all  4)download  5)download+all
        /// </summary>
        public int Authority { get; set; }

        /// <summary>
        /// 0)txt 1)Excel  2)csv 3）Json 4)xml
        /// </summary>
        public int ExportType { get; set; }

        public string Password { get; set; }

    }


}
