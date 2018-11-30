using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using ToolGood.Infrastructure.Dependency.AutofacContainer;

namespace ToolGood.Infrastructure.Dependency
{
    public class ContainerManager
    {
        
        private ContainerManager()
        {
        }
        public IObjectContainer Container { get; private set; }

        
        public static ContainerManager Instance { get; private set; } = new ContainerManager();

        
        public static ContainerManager UseAutofacContainer(ContainerBuilder builder)
        {
            return ContainerManager.SetContainer(new AutofacObjectContainer(builder));
        }

        
        private static ContainerManager SetContainer(IObjectContainer container)
        {
            ContainerManager.Instance.Container = container;
            return ContainerManager.Instance;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="implementationType">实例类型</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public ContainerManager RegisterType(Type implementationType, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            this.Container.RegisterType(implementationType, serviceName, lifeStyle);
            return this;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="implementationType">实例类型</param>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public ContainerManager RegisterType(Type implementationType, Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            this.Container.RegisterType(implementationType, interceptTypeList, serviceName, lifeStyle);
            return this;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="serviceName">服务名称</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public ContainerManager RegisterType<T>(string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            this.Container.RegisterType<T>(serviceName, lifeStyle);
            return this;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public ContainerManager RegisterType<T>(Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            this.Container.RegisterType<T>(interceptTypeList, serviceName, lifeStyle);
            return this;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">实例类型</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="life">生命周期</param>
        
        public ContainerManager RegisterType(Type serviceType, Type implementationType, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            this.Container.RegisterType(serviceType, implementationType, serviceName, lifeStyle);
            return this;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">实例类型</param>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public ContainerManager RegisterType(Type serviceType, Type implementationType, Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            this.Container.RegisterType(serviceType, implementationType, interceptTypeList, serviceName, lifeStyle);
            return this;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实例类型</typeparam>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public ContainerManager RegisterType<TService, TImplementer>(string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton) where TService : class where TImplementer : class, TService
        {
            this.Container.RegisterType<TService, TImplementer>(serviceName, lifeStyle);
            return this;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实例类型</typeparam>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public ContainerManager RegisterType<TService, TImplementer>(Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton) where TService : class where TImplementer : class, TService
        {
            this.Container.RegisterType<TService, TImplementer>(interceptTypeList, serviceName, lifeStyle);
            return this;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实例类型</typeparam>
        /// <param name="instance">实例值</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public ContainerManager RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton) where TService : class where TImplementer : class, TService
        {
            this.Container.RegisterInstance<TService, TImplementer>(instance, serviceName, lifeStyle);
            return this;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实例类型</typeparam>
        /// <param name="instance">实例值</param>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public ContainerManager RegisterInstance<TService, TImplementer>(TImplementer instance, Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton) where TService : class where TImplementer : class, TService
        {
            this.Container.RegisterInstance<TService, TImplementer>(instance, interceptTypeList, serviceName, lifeStyle);
            return this;
        }

        /// <summary>
        /// 根据程序集注册
        /// </summary>
        /// <param name="assemblies">程序集</param>
        /// <param name="predicate">筛选条件</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public ContainerManager RegisterAssemblyTypes(Assembly assemblies, Func<Type, bool> predicate = null, LifeStyle lifeStyle = LifeStyle.PerLifetimeScope)
        {
            this.Container.RegisterAssemblyTypes(assemblies, predicate, lifeStyle);
            return this;
        }

        /// <summary>
        /// 根据程序集注册
        /// </summary>
        /// <param name="assemblies">程序集</param>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="predicate">筛选条件</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public ContainerManager RegisterAssemblyTypes(Assembly assemblies, Type[] interceptTypeList, Func<Type, bool> predicate = null, LifeStyle lifeStyle = LifeStyle.PerLifetimeScope)
        {
            this.Container.RegisterAssemblyTypes(assemblies, interceptTypeList, predicate, lifeStyle);
            return this;
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>注册的服务类型</returns>
        
        public static TService Resolve<TService>() where TService : class
        {
            return ContainerManager.Instance.Container.Resolve<TService>();
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns>注册的服务类型</returns>
        
        public static object Resolve(Type serviceType)
        {
            return ContainerManager.Instance.Container.Resolve(serviceType);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="instance">服务类型默认实例</param>
        /// <returns>成功 则返回true</returns>
        
        public static bool TryResolve<TService>(out TService instance) where TService : class
        {
            return ContainerManager.Instance.Container.TryResolve<TService>(out instance);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance"></param>
        /// <returns>成功 则返回true</returns>
        
        public static bool TryResolve(Type serviceType, out object instance)
        {
            return ContainerManager.Instance.Container.TryResolve(serviceType, out instance);
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="serviceName">服务名字</param>
        /// <returns>服务类型</returns>
        
        public static TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return ContainerManager.Instance.Container.ResolveNamed<TService>(serviceName);
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <param name="serviceName">服务名字</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns>服务类型</returns>
        
        public static object ResolveNamed(string serviceName, Type serviceType)
        {
            return ContainerManager.Instance.Container.ResolveNamed(serviceName, serviceType);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <param name="serviceName">服务名字</param>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance">默认实例</param>
        /// <returns>成功 则返回true</returns>
        
        public static bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            return ContainerManager.Instance.Container.TryResolveNamed(serviceName, serviceType, out instance);
        }

        /// <summary>
        /// 判断是否已注册该服务
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns>true表示已注册</returns>
        
        public static bool IsRegistered(Type serviceType)
        {
            return ContainerManager.Instance.Container.IsRegistered(serviceType);
        }

        /// <summary>
        /// 判断是否已注册该服务
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>true表示已注册</returns>
        
        public static bool IsRegistered<TService>()
        {
            return ContainerManager.Instance.Container.IsRegistered<TService>();
        }

        /// <summary>
        /// 判断是否已注册该服务
        /// </summary>
        /// <param name="serviceName">注册的服务名字</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns>true表示已注册</returns>
        
        public static bool IsRegisteredWithName(string serviceName, Type serviceType)
        {
            return ContainerManager.Instance.Container.IsRegisteredWithName(serviceName, serviceType);
        }

        /// <summary>
        /// 判断是否已注册该服务
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="serviceName">注册的服务名字</param>
        /// <returns>true表示已注册</returns>
        
        public static bool IsRegisteredWithName<TService>(string serviceName)
        {
            return ContainerManager.Instance.Container.IsRegisteredWithName<TService>(serviceName);
        }

        /// <summary>
        /// 开始一个作用域请求，与其它请求相互独立
        /// </summary>
        /// <returns>IIocScopeResolve</returns>
        
        public static IIocScopeResolve BeginLeftScope()
        {
            return ContainerManager.Instance.Container.BeginLeftScope();
        }
    }
}
