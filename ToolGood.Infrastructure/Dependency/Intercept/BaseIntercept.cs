using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public abstract class BaseIntercept : IInterceptor
    {
        
        public abstract void Intercept(IInvocation invocation);
    }
}
