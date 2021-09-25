/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.SqlOnline.Dtos
{
    public class EmailSendDto
    {
        public string EmailSendHost { get; set; }
        public string EmailSendPort { get; set; }
        public int EmailSendSSL { get; set; }
        public string EmailSendUser { get; set; }
        public string EmailSendPassword { get; set; }
        public int EmailSendCount { get; set; }
        public int EmailSendInterval { get; set; }
    }
}
