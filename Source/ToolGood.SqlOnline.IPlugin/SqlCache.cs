/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.SqlOnline.IPlugin
{
    public class SqlCache
    {
        public static SqlCache Instance;

        static SqlCache()
        {
            Instance = new SqlCache();
        }


        private List<string> SqlType = new List<string>();

        private Dictionary<string, Func<ISqlCommonProvider>> SqlCommonProviderDict = new Dictionary<string, Func<ISqlCommonProvider>>();

        private Dictionary<string, Func<ISqlFunctionProvider>> SqlFunctionProviderDict = new Dictionary<string, Func<ISqlFunctionProvider>>();

        private Dictionary<string, Func<ISqlProcedureProvider>> SqlProcedureProviderDict = new Dictionary<string, Func<ISqlProcedureProvider>>();

        private Dictionary<string, Func<ISqlViewProvider>> SqlViewProviderDict = new Dictionary<string, Func<ISqlViewProvider>>();

        public void AddSqlType(string name)
        {
            SqlType.Add(name);
        }
        public void AddProvider(string name, Func<ISqlCommonProvider> func)
        {
            SqlCommonProviderDict.Add(name, func);
        }
        public void AddProvider(string name, Func<ISqlFunctionProvider> func)
        {
            SqlFunctionProviderDict.Add(name, func);
        }
        public void AddProvider(string name, Func<ISqlProcedureProvider> func)
        {
            SqlProcedureProviderDict.Add(name, func);
        }
        public void AddProvider(string name, Func<ISqlViewProvider> func)
        {
            SqlViewProviderDict.Add(name, func);
        }

        public List<string> GetSqlTypes()
        {
            return SqlType;
        }

        public ISqlCommonProvider GetSqlCommonProvider(string name)
        {
            return SqlCommonProviderDict[name].Invoke();
        }
        public ISqlFunctionProvider GetSqlFunctionProvider(string name)
        {
            return SqlFunctionProviderDict[name].Invoke();
        }
        public ISqlProcedureProvider GetSqlProcedureProvider(string name)
        {
            return SqlProcedureProviderDict[name].Invoke();
        }
        public ISqlViewProvider GetSqlViewProvider(string name)
        {
            return SqlViewProviderDict[name].Invoke();
        }

    }
}
