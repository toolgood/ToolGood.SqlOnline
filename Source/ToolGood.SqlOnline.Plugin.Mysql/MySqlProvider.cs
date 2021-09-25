/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Data.Common;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.Mysql
{
    public class MySqlProvider : ISqlProvider
    {
        public string GetJsMode()
        {
            return "ace/mode/mysql";
        }

        public DbProviderFactory GetProviderFactory()
        {
            return MySql.Data.MySqlClient.MySqlClientFactory.Instance;
        }

        public string GetServerName()
        {
            return "Mysql";
        }

        public void Init(SqlCache sqlCache)
        {
            var name = GetServerName();

            sqlCache.AddSqlType(name);
            sqlCache.AddProvider(name,()=>new MySqlCommonProvider());
            sqlCache.AddProvider(name,()=>new MySqlFunctionProvider());
            sqlCache.AddProvider(name,()=>new MySqlProcedureProvider());
            sqlCache.AddProvider(name,()=>new MySqlViewProvider());
        }
    }


}
