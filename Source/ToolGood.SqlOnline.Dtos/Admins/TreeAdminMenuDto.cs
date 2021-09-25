/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;

namespace ToolGood.SqlOnline.Dtos
{
    public class TreeAdminMenuDto
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Name { get; set; }

        public bool IsOpen { get; set; }

        public bool Activity { get; set; }



        public List<TreeAdminMenuDto> Items { get; set; }

        public TreeAdminMenuDto()
        {
            Items = new List<TreeAdminMenuDto>();
        }

    }


}
