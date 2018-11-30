using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public abstract class BaseMonitorIntercept : IInterceptor
    {
        
        public BaseMonitorIntercept(TimeElapsedStatistic timeElapsedStatistic)
        {
            this._timeElapsedStatistic = timeElapsedStatistic;
        }

        
        // (get) Token: 0x0600003C RID: 60
        public abstract TimeElapsedType TimeElapsedType { get; }

        
        public void Intercept(IInvocation invocation)
        {
            BaseMonitorIntercept.MethodType delegateType = this.GetDelegateType(invocation);
            bool flag = delegateType == BaseMonitorIntercept.MethodType.Synchronous;
            if (flag) {
                this.Monitor(delegate {
                    invocation.Proceed();
                }, string.Format("{0}-{1}", invocation.TargetType.FullName, invocation.Method.Name));
            } else {
                bool flag2 = delegateType == BaseMonitorIntercept.MethodType.AsyncAction;
                if (flag2) {
                    invocation.Proceed();
                    invocation.ReturnValue = this.MonitorAsync((Task)invocation.ReturnValue, string.Format("{0}-{1}", invocation.TargetType.FullName, invocation.Method.Name));
                } else {
                    invocation.Proceed();
                    this.ExecuteHandleAsyncWithResultUsingReflection(invocation);
                }
            }
        }

        /// <summary>
        /// 获取方法类型
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        
        private BaseMonitorIntercept.MethodType GetDelegateType(IInvocation invocation)
        {
            Type returnType = invocation.Method.ReturnType;
            bool flag = returnType == BaseMonitorIntercept._AsyncActionType;
            BaseMonitorIntercept.MethodType result;
            if (flag) {
                result = BaseMonitorIntercept.MethodType.AsyncAction;
            } else {
                bool flag2 = returnType.IsGenericType && returnType.GetGenericTypeDefinition() == BaseMonitorIntercept._AsyncFunctionType;
                if (flag2) {
                    result = BaseMonitorIntercept.MethodType.AsyncFunction;
                } else {
                    result = BaseMonitorIntercept.MethodType.Synchronous;
                }
            }
            return result;
        }

        /// <summary>
        /// 执行异步方法
        /// </summary>
        /// <param name="invocation"></param>
        
        private void ExecuteHandleAsyncWithResultUsingReflection(IInvocation invocation)
        {
            Type resultType = invocation.Method.ReturnType.GetGenericArguments()[0];
            MethodInfo mi = BaseMonitorIntercept.handleAsyncMethodInfo.MakeGenericMethod(new Type[]
            {
                resultType
            });
            invocation.ReturnValue = mi.Invoke(this, new object[]
            {
                invocation.ReturnValue,
                this.TimeElapsedType,
                string.Format("{0}-{1}", invocation.TargetType.FullName, invocation.Method.Name)
            });
        }

        
        private void Monitor(Action executeAction, string memberName = null)
        {
            TimeElapsedInfo timeElapsedInfo = new TimeElapsedInfo();
            DateTime startTime = DateTime.Now;
            timeElapsedInfo.ExecuteTime = startTime;
            try {
                executeAction();
                timeElapsedInfo.IsSuccess = true;
            } catch (Exception ex) {
                timeElapsedInfo.IsSuccess = false;
                timeElapsedInfo.Remark = ex.Message;
                throw;
            } finally {
                timeElapsedInfo.TimeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
                timeElapsedInfo.CallMemberName = memberName;
                timeElapsedInfo.TimeElapsedType = this.TimeElapsedType;
                this._timeElapsedStatistic.AddTimeElapsedInfo(timeElapsedInfo);
            }
        }

        
        private async Task MonitorAsync(Task task, string memberName = null)
        {
            TimeElapsedInfo timeElapsedInfo = new TimeElapsedInfo();
            DateTime startTime = DateTime.Now;
            timeElapsedInfo.ExecuteTime = startTime;
            try {
                timeElapsedInfo.IsSuccess = true;
                await task;
            } catch (Exception ex) {
                timeElapsedInfo.IsSuccess = false;
                timeElapsedInfo.Remark = ex.Message;
                throw;
            } finally {
                timeElapsedInfo.TimeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
                timeElapsedInfo.CallMemberName = memberName;
                timeElapsedInfo.TimeElapsedType = this.TimeElapsedType;
                this._timeElapsedStatistic.AddTimeElapsedInfo(timeElapsedInfo);
            }
        }

        
        private async Task<T> MonitorAsyncFunction<T>(Task<T> task, TimeElapsedType elapsedType, string memberName = null)
        {
            TimeElapsedInfo timeElapsedInfo = new TimeElapsedInfo();
            DateTime startTime = DateTime.Now;
            timeElapsedInfo.ExecuteTime = startTime;
            T result;
            try {
                timeElapsedInfo.IsSuccess = true;
                T t = await task;
                result = t;
            } catch (Exception ex) {
                timeElapsedInfo.IsSuccess = false;
                timeElapsedInfo.Remark = ex.Message;
                throw;
            } finally {
                timeElapsedInfo.TimeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
                timeElapsedInfo.CallMemberName = memberName;
                timeElapsedInfo.TimeElapsedType = elapsedType;
                this._timeElapsedStatistic.AddTimeElapsedInfo(timeElapsedInfo);
            }
            return result;
        }

        /// <summary>
        /// 异步类型(无返回值)
        /// </summary>
        
        private static readonly Type _AsyncActionType = typeof(Task);

        /// <summary>
        /// 异步方法类型(有返回值)
        /// </summary>
        
        private static readonly Type _AsyncFunctionType = typeof(Task<>);

        /// <summary>
        /// 异步方法处理
        /// </summary>
        
        private static readonly MethodInfo handleAsyncMethodInfo = typeof(BaseMonitorIntercept).GetMethod("MonitorAsyncFunction", BindingFlags.Instance | BindingFlags.NonPublic);

        
        private readonly TimeElapsedStatistic _timeElapsedStatistic;

        /// <summary>
        /// 方法类型
        /// </summary>
        
        private enum MethodType
        {
            /// <summary>
            /// 同步方法
            /// </summary>
            
            Synchronous,
            /// <summary>
            /// 异步(无返回值)
            /// </summary>
            
            AsyncAction,
            /// <summary>
            /// 异步方法(有返回值)
            /// </summary>
            
            AsyncFunction
        }
    }
}
