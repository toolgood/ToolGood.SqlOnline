using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.AutofacContainer
{
    public sealed class AutofacLeftScope : IIocScopeResolve, IIocResolve, IDisposable
    {
        
        public AutofacLeftScope(ILifetimeScope lifeTimeScope)
        {
            this._lifeTimeScope = lifeTimeScope;
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>注册的服务类型</returns>
        
        public TService Resolve<TService>() where TService : class
        {
            return this._lifeTimeScope.Resolve<TService>();
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns>注册的服务类型</returns>
        
        public object Resolve(Type serviceType)
        {
            return this._lifeTimeScope.Resolve(serviceType);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="instance">服务类型默认实例</param>
        /// <returns>成功 则返回true</returns>
        
        public bool TryResolve<TService>(out TService instance) where TService : class
        {
            return this._lifeTimeScope.TryResolve(out instance);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance"></param>
        /// <returns>成功 则返回true</returns>
        
        public bool TryResolve(Type serviceType, out object instance)
        {
            return this._lifeTimeScope.TryResolve(serviceType, out instance);
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="serviceName">服务名字</param>
        /// <returns>服务类型</returns>
        
        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return this._lifeTimeScope.ResolveNamed<TService>(serviceName);
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <param name="serviceName">服务名字</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns>服务类型</returns>
        
        public object ResolveNamed(string serviceName, Type serviceType)
        {
            return this._lifeTimeScope.ResolveNamed(serviceName, serviceType);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <param name="serviceName">服务名字</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance">默认实例</param>
        /// <returns>成功 则返回true</returns>
        
        public bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            return this._lifeTimeScope.TryResolveNamed(serviceName, serviceType, out instance);
        }

        
        public void Dispose()
        {
            ILifetimeScope lifeTimeScope = this._lifeTimeScope;
            if (lifeTimeScope != null) {
                lifeTimeScope.Dispose();
            }
        }

        
        private readonly ILifetimeScope _lifeTimeScope;
    }
}
