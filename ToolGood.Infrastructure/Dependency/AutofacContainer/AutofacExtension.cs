using Autofac.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.AutofacContainer
{
    public static class AutofacExtension
    {
        /// <summary>
        /// 设置生命周期
        /// </summary>
        /// <typeparam name="TImplementer"></typeparam>
        /// <typeparam name="TActivatorData"></typeparam>
        /// <typeparam name="TRegistrationStyle"></typeparam>
        /// <param name="registrationBuilder"></param>
        /// <param name="lifeStyle"></param>
        
        public static void SetLifeStyle<TImplementer, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TImplementer, TActivatorData, TRegistrationStyle> registrationBuilder, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            switch (lifeStyle) {
                case LifeStyle.Transient:
                    registrationBuilder.InstancePerDependency();
                    break;
                case LifeStyle.Singleton:
                    registrationBuilder.SingleInstance();
                    break;
                case LifeStyle.PerLifetimeScope:
                    registrationBuilder.InstancePerLifetimeScope();
                    break;
            }
        }
    }
}
