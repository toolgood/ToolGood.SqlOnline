using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public static class UseInterceptConfigManageExtension
    {
        /// <summary>
        /// 使用时间监控消耗
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        
        public static ContainerManager UseTimeElapsedStatistic(this ContainerManager configuration)
        {
            configuration.RegisterType(typeof(TimeElapsedStatistic), null, LifeStyle.PerLifetimeScope);
            configuration.RegisterType(typeof(CacheStatisticIntercept), null, LifeStyle.Transient);
            configuration.RegisterType(typeof(HttpStatisticIntercept), null, LifeStyle.Transient);
            configuration.RegisterType(typeof(MQStatisticIntercept), null, LifeStyle.Transient);
            configuration.RegisterType(typeof(NoSqlStatisticIntercept), null, LifeStyle.Transient);
            configuration.RegisterType(typeof(OtherStatisticIntercept), null, LifeStyle.Transient);
            configuration.RegisterType(typeof(RpcStatisticIntercept), null, LifeStyle.Transient);
            configuration.RegisterType(typeof(SqlStatisticIntercept), null, LifeStyle.Transient);
            return configuration;
        }
    }
}
