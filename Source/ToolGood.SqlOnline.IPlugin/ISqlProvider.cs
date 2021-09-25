/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.SqlOnline.IPlugin
{
    public interface ISqlProvider
    {
        string GetServerName();
        string GetJsMode();

        DbProviderFactory GetProviderFactory();

        void Init(SqlCache sqlCache);
    
    }

}
