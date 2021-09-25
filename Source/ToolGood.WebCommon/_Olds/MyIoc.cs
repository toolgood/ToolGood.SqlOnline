/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using Microsoft.Extensions.DependencyInjection;

namespace ToolGood.WebCommon
{
    public static class MyIoc
    {
        internal static IServiceCollection _serviceCollection;
        public static void SetServiceCollection(IServiceCollection serviceDescriptors)
        {
            _serviceCollection = serviceDescriptors;
        }

        public static T Create<T>()
        {
            return _serviceCollection.BuildServiceProvider().GetService<T>();
        }

    }
}
