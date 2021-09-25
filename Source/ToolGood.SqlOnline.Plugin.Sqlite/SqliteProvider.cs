/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Data.Common;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.Sqlite
{
    public class SqliteProvider : ISqlProvider
    {
        public DbProviderFactory GetProviderFactory()
        {
            return Microsoft.Data.Sqlite.SqliteFactory.Instance;
        }

        public string GetServerName()
        {
            return "Sqlite";
        }

        public string GetJsMode()
        {
            return "ace/mode/sql";
        }

        public void Init(SqlCache sqlCache)
        {
            var name = GetServerName();

            sqlCache.AddSqlType(name);
            sqlCache.AddProvider(name, () => new SqliteCommonProvider());
            sqlCache.AddProvider(name, () => new SqliteFunctionProvider());
            sqlCache.AddProvider(name, () => new SqliteProcedureProvider());
            sqlCache.AddProvider(name, () => new SqliteViewProvider());
        }
    }
}
