/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ToolGood.Common.Extensions
{
    /// <summary>
    /// 任务设置
    /// </summary>
    public static partial class ObjectExtension
    {
        /// <summary>
        /// 转成任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Task<T> ToTask<T>(this T source)
        {
            return Task.FromResult(source);
        }
        /// <summary>
        /// 运行task
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public static T RunSync<T>(this Task<T> task)
        {
            return RunSync(() => task);
        }
        /// <summary>
        /// 运行task
        /// </summary>
        /// <param name="task"></param>
        public static void RunSync(this Task task)
        {
            RunSync(() => task);
        }


        #region Methods

        /// <summary>
        ///    Execute's an async Task<T /> method which has a void return value synchronously
        /// </summary>
        /// <param name="task">Task<T /> method to execute</param>
        private static void RunSync(Func<Task> task)
        {
            var oldContext = SynchronizationContext.Current;
            var synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            synch.Post(
               async _ => {
                   try {
                       await task();
                   } catch (Exception e) {
                       synch.InnerException = e;
                       throw;
                   } finally {
                       synch.EndMessageLoop();
                   }
               },
               null);
            synch.BeginMessageLoop();

            SynchronizationContext.SetSynchronizationContext(oldContext);
        }

        /// <summary>
        ///    Execute's an async Task<T /> method which has a T return type synchronously
        /// </summary>
        /// <typeparam name="T">Return Type</typeparam>
        /// <param name="task">Task<T /> method to execute</param>
        /// <returns></returns>
        private static T RunSync<T>(Func<Task<T>> task)
        {
            var oldContext = SynchronizationContext.Current;
            var synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            var ret = default(T);
            synch.Post(
               async _ => {
                   try {
                       ret = await task();
                   } catch (Exception e) {
                       synch.InnerException = e;
                       throw;
                   } finally {
                       synch.EndMessageLoop();
                   }
               },
               null);
            synch.BeginMessageLoop();
            SynchronizationContext.SetSynchronizationContext(oldContext);
            return ret;
        }

        #endregion

        private class ExclusiveSynchronizationContext : SynchronizationContext
        {
            #region Fields

            private readonly Queue<Tuple<SendOrPostCallback, object>> _items =
               new Queue<Tuple<SendOrPostCallback, object>>();

            private readonly AutoResetEvent _workItemsWaiting = new AutoResetEvent(false);
            private bool _done;

            #endregion

            #region Properties

            // ReSharper disable once MemberCanBePrivate.Local
            internal Exception InnerException { get; set; }

            #endregion

            #region Public Methods and Operators

            public override SynchronizationContext CreateCopy()
            {
                return this;
            }

            public override void Post(SendOrPostCallback d, object state)
            {
                lock (_items) {
                    _items.Enqueue(Tuple.Create(d, state));
                }

                _workItemsWaiting.Set();
            }

            public override void Send(SendOrPostCallback d, object state)
            {
                throw new NotSupportedException("We cannot send to our same thread");
            }

            #endregion

            #region Methods

            internal void BeginMessageLoop()
            {
                while (!_done) {
                    Tuple<SendOrPostCallback, object> task = null;
                    lock (_items) {
                        if (_items.Count > 0) task = _items.Dequeue();
                    }

                    if (task != null) {
                        task.Item1(task.Item2);
                        if (InnerException != null) // the method threw an exeption
                            throw new AggregateException("AsyncHelper.Run method threw an exception.", InnerException);
                    } else {
                        _workItemsWaiting.WaitOne();
                    }
                }
            }

            internal void EndMessageLoop()
            {
                Post(_ => _done = true, null);
            }

            #endregion
        }

    }
}
