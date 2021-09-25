﻿/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */

namespace ToolGood.SqlOnline.Dtos
{
    public class SearchDto
    {
        public int  Id { get; set; }
        public string Search { get; set; }


        public string Field { get; set; }
        public string Order { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

}
