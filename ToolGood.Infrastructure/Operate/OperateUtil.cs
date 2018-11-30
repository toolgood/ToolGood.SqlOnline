using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ToolGood.Infrastructure
{
    public partial class OperateResult
    {
        /// <summary>
        /// 异常操作结果
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult Error(Exception ex)
        {
            return new OperateResult(ex);
        }

        /// <summary>
        /// 异常操作结果
        /// </summary>
        /// <typeparam name="T">结果值类型</typeparam>
        /// <param name="ex">异常信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult<T> Error<T>(Exception ex)
        {
            return new OperateResult<T>(ex);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg">失败信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult Failed(string msg)
        {
            return new OperateResult(false, msg);
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="msg">失败信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult<T> Failed<T>(string msg)
        {
            return new OperateResult<T>(false, msg);
        }

        /// <summary>
        /// 创建成功的操作结果
        /// </summary>
        /// <returns>操作结果</returns>
        public static OperateResult Success()
        {
            return new OperateResult();
        }

        /// <summary>
        /// 创建成功的操作结果
        /// </summary>
        /// <param name="msg">信息</param>
        /// <returns>操作结果</returns>
        public static OperateResult Success(string msg)
        {
            return new OperateResult(true, msg);
        }

        /// <summary>
        /// 创建成功的操作结果
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="value">值类型</param>
        /// <returns>操作结果</returns>

        public static OperateResult<T> Success<T>(T value)
        {
            return Success<T>(value, null);
        }

        /// <summary>
        /// 创建成功的操作结果
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="value">值类型</param>
        /// <param name="msg">信息</param>
        /// <returns>操作结果</returns>

        public static OperateResult<T> Success<T>(T value, string msg)
        {
            return new OperateResult<T>(true, msg) {
                Value = value
            };
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="executeActionAsync">方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static OperateResult Execute(Func<OperateResult> executeAction, string callMemberName = null)
        {
            OperateResult result;
            try {
                result = executeAction();
            } catch (ArgumentException ex) {
                Logger.Info(callMemberName, ex.Message);
                result = Error(ex);
            } catch (Exception ex2) {
                Logger.Error(callMemberName, ex2);
                result = Failed("操作失败");
            }
            return result;
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="executeAction">方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns></returns>
        public static OperateResult Execute(Action executeAction, string callMemberName = null)
        {
            OperateResult result;
            try {
                executeAction();
                return OperateResult.Success();
            } catch (ArgumentException ex) {
                Logger.Info(callMemberName, ex.Message);
                result = Error(ex);
            } catch (Exception ex2) {
                Logger.Error(callMemberName, ex2);
                result = Failed("操作失败");
            }
            return result;
        }


        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="executeAction">方法</param>
        /// <param name="successMessage">成功提示信息</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static OperateResult Execute(Action executeAction, string successMessage = null, string callMemberName = null)
        {
            OperateResult result;
            try {
                executeAction();
                result = Success(successMessage);
            } catch (ArgumentException ex) {
                Logger.Info(callMemberName, ex.Message);
                result = Error(ex);
            } catch (Exception ex2) {
                Logger.Error(callMemberName, ex2);
                result = Failed("操作失败");
            }
            return result;
        }

        /// <summary>
        /// 执行异步方法
        /// </summary>
        /// <param name="executeActionAsync">异步方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static async Task<OperateResult> ExecuteAsync(Func<Task<OperateResult>> executeActionAsync, string callMemberName = null)
        {
            OperateResult result;
            try {
                OperateResult operateResult = await executeActionAsync();
                result = operateResult;
            } catch (ArgumentException ex) {
                Logger.Info(callMemberName, ex.Message);
                result = Error(ex);
            } catch (Exception ex2) {
                Logger.Error(callMemberName, ex2);
                result = Failed("操作失败");
            }
            return result;
        }

        /// <summary>
        /// 执行异步方法
        /// </summary>
        /// <param name="executeActionAsync">异步方法</param>
        /// <param name="successMessage">成功提示信息</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static async Task<OperateResult> ExecuteAsync(Func<Task> executeActionAsync, string successMessage = null, string callMemberName = null)
        {
            OperateResult result;
            try {
                await executeActionAsync();
                result = Success(successMessage);
            } catch (ArgumentException ex) {
                Logger.Info(callMemberName, ex.Message);
                result = Error(ex);
            } catch (Exception ex2) {
                Logger.Error(callMemberName, ex2);
                result = Failed("操作失败");
            }
            return result;
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="executeAction">执行方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static OperateResult<T> Execute<T>(Func<OperateResult<T>> executeAction, string callMemberName = null)
        {
            OperateResult<T> result;
            try {
                result = executeAction();
            } catch (ArgumentException ex) {
                Logger.Info(callMemberName, ex.Message);
                result = Error<T>(ex);
            } catch (Exception ex2) {
                Logger.Error(callMemberName, ex2);
                result = Failed<T>("操作失败");
            }
            return result;
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="executeAction">执行方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static OperateResult<T> Execute<T>(Func<T> executeAction, string callMemberName = null)
        {
            OperateResult<T> result;
            try {
                T value = executeAction();
                result = Success<T>(value);
            } catch (ArgumentException ex) {
                Logger.Info(callMemberName, ex.Message);
                result = Error<T>(ex);
            } catch (Exception ex2) {
                Logger.Error(callMemberName, ex2);
                result = Failed<T>("操作失败");
            }
            return result;
        }

        /// <summary>
        /// 执行异步方法
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="executeAction">异步方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static async Task<OperateResult<T>> ExecuteAsync<T>(Func<Task<OperateResult<T>>> executeActionAsync, string callMemberName = null)
        {
            OperateResult<T> result;
            try {
                result = await executeActionAsync();
            } catch (ArgumentException ex) {
                Logger.Info(callMemberName, ex.Message);
                result = Error<T>(ex);
            } catch (Exception ex2) {
                Logger.Error(callMemberName, ex2);
                result = Failed<T>("操作失败");
            }
            return result;
        }

        /// <summary>
        /// 执行异步方法
        /// </summary>
        /// <typeparam name="T">返回结果类型</typeparam>
        /// <param name="executeAction">异步方法</param>
        /// <param name="callMemberName">调用方法名字</param>
        /// <returns>操作结果</returns>
        public static async Task<OperateResult<T>> ExecuteAsync<T>(Func<Task<T>> executeActionAsync, string callMemberName = null)
        {
            OperateResult<T> result;
            try {
                T t = await executeActionAsync();
                result = Success<T>(t);
            } catch (ArgumentException ex) {
                Logger.Info(callMemberName, ex.Message);
                result = Error<T>(ex);
            } catch (Exception ex2) {
                Logger.Error(callMemberName, ex2);
                result = Failed<T>("操作失败");
            }
            return result;
        }

    }
}
