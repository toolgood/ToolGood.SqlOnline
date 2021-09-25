/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Data.Common;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.SqlServer
{
    public class SqlServerProvider : ISqlProvider
    {
        public DbProviderFactory GetProviderFactory()
        {
            return System.Data.SqlClient.SqlClientFactory.Instance;
        }
        public string GetJsMode()
        {
            return "ace/mode/sqlserver";
        }
        public string GetServerName()
        {
            return "SqlServer";
        }

        public void Init(SqlCache sqlCache)
        {
            var name = GetServerName();

            sqlCache.AddSqlType(name);
            sqlCache.AddProvider(name, () => new SqlServerCommonProvider());
            sqlCache.AddProvider(name, () => new SqlServerFunctionProvider());
            sqlCache.AddProvider(name, () => new SqlServerProcedureProvider());
            sqlCache.AddProvider(name, () => new SqlServerViewProvider());
        }
    }

}
