using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 遍历执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action">要执行的方法</param>

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T element in enumerable) {
                action(element);
            }
        }

        /// <summary>
        /// 遍历异步执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action">要执行的方法</param>
        /// <returns></returns>

        public static Task ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
        {
            return Task.WhenAll(
                from item in enumerable
                select Task.Run(() => action(item))
                                );
        }

        /// <summary>
        /// 不为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>

        public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable)
        {
            bool flag = enumerable == null;
            bool result;
            if (flag) {
                result = false;
            } else {
                bool flag2 = enumerable.Any();
                result = flag2;
            }
            return result;
        }

        /// <summary>
        /// 为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.IsNotEmpty<T>();
        }
    }
}
