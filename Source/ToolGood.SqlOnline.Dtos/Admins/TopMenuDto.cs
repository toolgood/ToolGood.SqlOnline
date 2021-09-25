/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using ToolGood.SqlOnline.Datas;

namespace ToolGood.SqlOnline.Dtos
{
    public class TopMenuDto
    {
        public int Id { get; set; }

        public string Ids { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool Activity { get; set; }

        public TopMenuDto() { }

        public TopMenuDto(DbSysAdminMenu adminMenu, string code)
        {
            Id = adminMenu.Id;
            Ids = adminMenu.Ids;
            Url = adminMenu.Url;
            Name = adminMenu.MenuName;
            Code = adminMenu.MenuCode;
            Activity = adminMenu.MenuCode == code;
        }


    }
}
