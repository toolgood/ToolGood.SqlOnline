using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency
{
    public enum LifeStyle
    {
        /// <summary>
        /// 默认
        /// </summary>
        
        Transient = 1,
        /// <summary>
        /// 单例
        /// </summary>
        
        Singleton,
        /// <summary>
        /// 在一个生命周期域中，每一个依赖或调用创建一个单一的共享的实例，且每一个不同的生命周期域，实例是唯一的，不共享的。
        /// </summary>
        
        PerLifetimeScope
    }
}
