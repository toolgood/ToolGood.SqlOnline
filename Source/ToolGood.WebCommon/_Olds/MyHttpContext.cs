/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ToolGood.WebCommon
{
    public class MyHttpContext
    {
        internal static IServiceCollection _serviceCollection;

        public static HttpContext Current {
            get {
                object factory = _serviceCollection.BuildServiceProvider().GetService(typeof(IHttpContextAccessor));
                HttpContext context = ((HttpContextAccessor)factory).HttpContext;
                return context;
            }
        }

        public static void SetServiceCollection(IServiceCollection serviceDescriptors)
        {
            _serviceCollection = serviceDescriptors;
        }

    }
}
