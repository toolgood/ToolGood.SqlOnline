using System.Data.Common;
using Npgsql;
using ToolGood.SqlOnline.IPlugin;

namespace ToolGood.SqlOnline.Plugin.PostgreSQL
{
    public class PostgreSQLProvider : ISqlProvider
    {
        public string GetJsMode()
        {
            return "ace/mode/pgsql";
        }

        public DbProviderFactory GetProviderFactory()
        {
            return NpgsqlFactory.Instance;
        }

        public string GetServerName()
        {
            return "PostgreSQL";
        }

        public void Init(SqlCache sqlCache)
        {
            var name = GetServerName();

            sqlCache.AddSqlType(name);
            sqlCache.AddProvider(name, () => new PostgreSQLCommonProvider());
            sqlCache.AddProvider(name, () => new PostgreSQLFunctionProvider());
            sqlCache.AddProvider(name, () => new PostgreSQLProcedureProvider());
            sqlCache.AddProvider(name, () => new PostgreSQLViewProvider());
        }
    }



}
