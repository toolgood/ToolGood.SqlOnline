/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Application.Admins;
using ToolGood.SqlOnline.IPlugin;
using ToolGood.SqlOnline.Plugin.Mysql;
using ToolGood.SqlOnline.Plugin.PostgreSQL;
using ToolGood.SqlOnline.Plugin.Sqlite;
using ToolGood.SqlOnline.Plugin.SqlServer;

namespace ToolGood.SqlOnline.Commons
{
    public class Bootstrap
    {
        public static void Init(IServiceProvider serviceProvider, IWebHostEnvironment env)
        {
            IAdminSecurityApplication securityApplication = new AdminSecurityApplication();
            securityApplication.ResetIpFilter();
            securityApplication = null;

            var cache = SqlCache.Instance;

            ISqlProvider provider = new SqlServerProvider();
            provider.Init(cache);
            provider = new MySqlProvider();
            provider.Init(cache);
            provider = new SqliteProvider();
            provider.Init(cache);
            provider = new PostgreSQLProvider();
            provider.Init(cache);
            provider = null;

            //var assList = AppDomain.CurrentDomain.GetAssemblies().Where(q => q.FullName?.Contains("Plugin") ?? false).ToList();
            //foreach (var ass in assList) {
            //    SqlProviderInit(cache,  ass);
            //}

            //var dir = Path.Combine(env.ContentRootPath, "Plugins");
            //if (Directory.Exists(dir)) {
            //    var files = Directory.GetFiles(dir, "*.Plugin*.dll", SearchOption.AllDirectories);
            //    foreach (var file in files) {
            //        try {
            //            var ass = Assembly.LoadFrom(file);
            //            SqlProviderInit(cache, ass);
            //        } catch (Exception) { }
            //    }
            //}
        }

        //private static void SqlProviderInit(SqlCache cache, Assembly ass)
        //{
        //    var providerType = typeof(ISqlProvider);//isAssignableFrom
        //    var types = ass.GetTypes();
        //    foreach (var type in types) {
        //        if (type.IsClass == false) { continue; }
        //        if (type.IsAbstract) { continue; }
        //        if (type.IsGenericType) { continue; }

        //        if (providerType.IsAssignableFrom(type)) {
        //            var provider = Activator.CreateInstance(type) as ISqlProvider;
        //            provider.Init(cache);
        //            provider = null;
        //        }
        //    }
        //}
    }
}
