using System;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Extras.DynamicProxy;
using Autofac.Features.Scanning;

namespace ToolGood.Infrastructure.Dependency.AutofacContainer
{
    public sealed class AutofacObjectContainer : IObjectContainer, IIocRegister, IIocResolve
    {
        
        public AutofacObjectContainer()
        {
            this._builder = new ContainerBuilder();
            this._container = null;
        }

        
        public AutofacObjectContainer(ContainerBuilder builder)
        {
            this._builder = (builder ?? new ContainerBuilder());
            this._container = null;
        }
        
        public IContainer Container {
            get {
                bool flag = this._container == null;
                if (flag) {
                    lock (this) {
                        bool flag3 = this._container == null;
                        if (flag3) {
                            this._container = this._builder.Build(ContainerBuildOptions.None);
                        }
                    }
                }
                return this._container;
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="implementationType">实例类型</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public void RegisterType(Type implementationType, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            ContainerBuilder builder = this._builder;
            IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder = builder.RegisterType(implementationType).AsSelf<object, ConcreteReflectionActivatorData>();
            bool flag = !string.IsNullOrWhiteSpace(serviceName);
            if (flag) {
                registrationBuilder.Named(serviceName, implementationType);
            }
            registrationBuilder.SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="implementationType">实例类型</param>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public void RegisterType(Type implementationType, Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            ContainerBuilder builder = this._builder;
            IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder = builder.RegisterType(implementationType).AsSelf<object, ConcreteReflectionActivatorData>();
            bool flag = !string.IsNullOrWhiteSpace(serviceName);
            if (flag) {
                registrationBuilder.Named(serviceName, implementationType);
            }
            registrationBuilder.InterceptedBy(interceptTypeList).EnableInterfaceInterceptors<object, ConcreteReflectionActivatorData, SingleRegistrationStyle>().SetLifeStyle(lifeStyle);
        }

        
        public void RegisterType<T>(string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            ContainerBuilder builder = this._builder;
            IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder = builder.RegisterType<T>().AsSelf<T, ConcreteReflectionActivatorData>();
            bool flag = !string.IsNullOrWhiteSpace(serviceName);
            if (flag) {
                registrationBuilder.Named<T>(serviceName);
            }
            registrationBuilder.SetLifeStyle(lifeStyle);
        }

        
        public void RegisterType<T>(Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            ContainerBuilder builder = this._builder;
            IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder = builder.RegisterType<T>().AsSelf<T, ConcreteReflectionActivatorData>();
            bool flag = !string.IsNullOrWhiteSpace(serviceName);
            if (flag) {
                registrationBuilder.Named<T>(serviceName);
            }
            registrationBuilder.InterceptedBy(interceptTypeList).EnableInterfaceInterceptors<T, ConcreteReflectionActivatorData, SingleRegistrationStyle>().SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">实例类型</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="life">生命周期</param>
        
        public void RegisterType(Type serviceType, Type implementationType, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            ContainerBuilder builder = this._builder;
            IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder = builder.RegisterType(implementationType).As(new Type[]
            {
                serviceType
            });
            bool flag = !string.IsNullOrWhiteSpace(serviceName);
            if (flag) {
                registrationBuilder.Named(serviceName, implementationType);
            }
            registrationBuilder.SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="implementationType">实例类型</param>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public void RegisterType(Type serviceType, Type implementationType, Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton)
        {
            ContainerBuilder builder = this._builder;
            IRegistrationBuilder<object, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder = builder.RegisterType(implementationType).As(new Type[]
            {
                serviceType
            });
            bool flag = !string.IsNullOrWhiteSpace(serviceName);
            if (flag) {
                registrationBuilder.Named(serviceName, implementationType);
            }
            registrationBuilder.InterceptedBy(interceptTypeList).EnableInterfaceInterceptors<object, ConcreteReflectionActivatorData, SingleRegistrationStyle>().SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实例类型</typeparam>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public void RegisterType<TService, TImplementer>(string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton) where TService : class where TImplementer : class, TService
        {
            ContainerBuilder builder = this._builder;
            IRegistrationBuilder<TImplementer, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder = builder.RegisterType<TImplementer>().As<TService>();
            bool flag = !string.IsNullOrWhiteSpace(serviceName);
            if (flag) {
                registrationBuilder.Named<TService>(serviceName);
            }
            registrationBuilder.SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实例类型</typeparam>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public void RegisterType<TService, TImplementer>(Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton) where TService : class where TImplementer : class, TService
        {
            ContainerBuilder builder = this._builder;
            IRegistrationBuilder<TImplementer, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder = builder.RegisterType<TImplementer>().As<TService>();
            bool flag = !string.IsNullOrWhiteSpace(serviceName);
            if (flag) {
                registrationBuilder.Named<TService>(serviceName);
            }
            registrationBuilder.InterceptedBy(interceptTypeList).EnableInterfaceInterceptors<TImplementer, ConcreteReflectionActivatorData, SingleRegistrationStyle>().SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <typeparam name="TImplementer">实例类型</typeparam>
        /// <param name="instance">实例值</param>
        /// <param name="serviceName">服务名字</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton) where TService : class where TImplementer : class, TService
        {
            ContainerBuilder builder = this._builder;
            IRegistrationBuilder<TImplementer, SimpleActivatorData, SingleRegistrationStyle> registrationBuilder = builder.RegisterInstance(instance).As<TService>();
            bool flag = serviceName != null;
            if (flag) {
                registrationBuilder.Named<TService>(serviceName);
            }
            registrationBuilder.SetLifeStyle(lifeStyle);
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
        
        public void RegisterInstance<TService, TImplementer>(TImplementer instance, Type[] interceptTypeList, string serviceName = null, LifeStyle lifeStyle = LifeStyle.Singleton) where TService : class where TImplementer : class, TService
        {
            ContainerBuilder builder = this._builder;
            IRegistrationBuilder<TImplementer, SimpleActivatorData, SingleRegistrationStyle> registrationBuilder = builder.RegisterInstance(instance).As<TService>();
            bool flag = serviceName != null;
            if (flag) {
                registrationBuilder.Named<TService>(serviceName);
            }
            registrationBuilder.InterceptedBy(interceptTypeList).EnableInterfaceInterceptors<TImplementer, SimpleActivatorData, SingleRegistrationStyle>().SetLifeStyle(lifeStyle);
        }

        /// <summary>
        /// 根据程序集注册
        /// </summary>
        /// <param name="assemblies">程序集</param>
        /// <param name="predicate">筛选条件</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public void RegisterAssemblyTypes(Assembly assemblies, Func<Type, bool> predicate = null, LifeStyle lifeStyle = LifeStyle.PerLifetimeScope)
        {
            bool flag = assemblies != null;
            if (flag) {
                ContainerBuilder builder = this._builder;
                IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registrationBuilder = builder.RegisterAssemblyTypes(new Assembly[]
                {
                    assemblies
                });
                bool flag2 = predicate != null;
                if (flag2) {
                    registrationBuilder.Where(predicate);
                }
                registrationBuilder.AsImplementedInterfaces<object>().SetLifeStyle(lifeStyle);
            }
        }

        /// <summary>
        /// 根据程序集注册
        /// </summary>
        /// <param name="assemblies">程序集</param>
        /// <param name="interceptTypeList">Aop类型</param>
        /// <param name="predicate">筛选条件</param>
        /// <param name="lifeStyle">生命周期</param>
        
        public void RegisterAssemblyTypes(Assembly assemblies, Type[] interceptTypeList, Func<Type, bool> predicate = null, LifeStyle lifeStyle = LifeStyle.PerLifetimeScope)
        {
            bool flag = assemblies != null;
            if (flag) {
                ContainerBuilder builder = this._builder;
                IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registrationBuilder = builder.RegisterAssemblyTypes(new Assembly[]
                {
                    assemblies
                });
                bool flag2 = predicate != null;
                if (flag2) {
                    registrationBuilder.Where(predicate);
                }
                registrationBuilder.AsImplementedInterfaces<object>().InterceptedBy(interceptTypeList).EnableInterfaceInterceptors<object, ScanningActivatorData, DynamicRegistrationStyle>().SetLifeStyle(lifeStyle);
            }
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>注册的服务类型</returns>
        
        public TService Resolve<TService>() where TService : class
        {
            return this.Container.Resolve<TService>();
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns>注册的服务类型</returns>
        
        public object Resolve(Type serviceType)
        {
            return this.Container.Resolve(serviceType);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="instance">服务类型默认实例</param>
        /// <returns>成功 则返回true</returns>
        
        public bool TryResolve<TService>(out TService instance) where TService : class
        {
            return this.Container.TryResolve(out instance);
        }

        /// <summary>
        /// 尝试取出注册的服务类型
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <param name="instance"></param>
        /// <returns>成功 则返回true</returns>
        
        public bool TryResolve(Type serviceType, out object instance)
        {
            return this.Container.TryResolve(serviceType, out instance);
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="serviceName">服务名字</param>
        /// <returns>服务类型</returns>
        
        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            return Container.ResolveNamed<TService>(serviceName);
        }

        /// <summary>
        /// 取出注册的服务类型
        /// </summary>
        /// <param name="serviceName">服务名字</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns>服务类型</returns>
        
        public object ResolveNamed(string serviceName, Type serviceType)
        {
            return this.Container.ResolveNamed(serviceName, serviceType);
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
            return this.Container.TryResolveNamed(serviceName, serviceType, out instance);
        }

        /// <summary>
        /// 判断是否已注册该服务
        /// </summary>
        /// <param name="serviceType">服务类型</param>
        /// <returns>true表示已注册</returns>
        
        public bool IsRegistered(Type serviceType)
        {
            return this.Container.IsRegistered(serviceType);
        }

        /// <summary>
        /// 判断是否已注册该服务
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <returns>true表示已注册</returns>
        
        public bool IsRegistered<TService>()
        {
            return this.Container.IsRegistered<TService>();
        }

        /// <summary>
        /// 判断是否已注册该服务
        /// </summary>
        /// <param name="serviceName">注册的服务名字</param>
        /// <param name="serviceType">服务类型</param>
        /// <returns>true表示已注册</returns>
        
        public bool IsRegisteredWithName(string serviceName, Type serviceType)
        {
            return this.Container.IsRegisteredWithName(serviceName, serviceType);
        }

        /// <summary>
        /// 判断是否已注册该服务
        /// </summary>
        /// <typeparam name="TService">服务类型</typeparam>
        /// <param name="serviceName">注册的服务名字</param>
        /// <returns>true表示已注册</returns>
        
        public bool IsRegisteredWithName<TService>(string serviceName)
        {
            return this.Container.IsRegisteredWithName<TService>(serviceName);
        }

        /// <summary>
        /// 开始一个作用域请求，与其它请求相互独立
        /// </summary>
        /// <returns>IIocScopeResolve</returns>
        
        public IIocScopeResolve BeginLeftScope()
        {
            return new AutofacLeftScope(this.Container.BeginLifetimeScope());
        }

        
        private IContainer _container;

        
        private ContainerBuilder _builder;
    }
}
