/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.SqlOnline.Dtos
{
    public class SqlEditorDto
    {
        public string JsMode { get; set; }
        public string SqlType { get; set; }

        public string SqlString { get; set; }
    }
}
