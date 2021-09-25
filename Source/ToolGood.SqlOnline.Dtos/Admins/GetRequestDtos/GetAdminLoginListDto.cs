/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Dtos
{
    public class GetAdminLoginListDto
    {
        public string Name { get; set; }

        public bool? Success { get; set; }
        public string Ip { get; set; }

        public string Field { get; set; }
        public string Order { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

}
