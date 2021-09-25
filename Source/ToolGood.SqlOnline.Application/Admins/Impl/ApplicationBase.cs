/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Configs;

namespace ToolGood.SqlOnline.Application.Admins
{
    public abstract class ApplicationBase
    {
        protected SqlHelper GetWriteHelper()
        {
            return Config.WriteHelper;
        }
        protected SqlHelper GetReadHelper()
        {
            return Config.ReadHelper;
        }



    }
}
