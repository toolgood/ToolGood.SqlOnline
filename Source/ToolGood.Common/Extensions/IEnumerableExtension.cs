/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System
{
    public static partial class ObjectExtension
    {
        /// <summary>
        /// 去重
        /// </summary>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IList<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }


        /// <summary>
        /// 转成字典 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="list"></param>
        /// <param name="keyFun"></param>
        /// <param name="valueFun"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<T1, T2> ToDictionary<T1, T2, T3>(this IList<T3> list, Func<T3, T1> keyFun, Func<T3, T2> valueFun, T1 key, T2 value)
        {
            Dictionary<T1, T2> dict = new Dictionary<T1, T2>();
            dict.Add(key, value);
            if (list == null) return dict;
            foreach (var item in list) {
                dict.Add(keyFun(item), valueFun(item));
            }
            return dict;
        }

        /// <summary>
        /// 遍历执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action">要执行的方法</param>
        public static IEnumerable<T> ForEach<T>(this IList<T> enumerable, Action<T> action)
        {
            foreach (var element in enumerable) {
                action(element);
            }
            return enumerable;
        }

        /// <summary>
        /// 转成集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static List<TResult> ToList<T, TResult>(this IList<T> source, Func<T, TResult> fun)
        {
            List<TResult> result = new List<TResult>();
            source.ForEach(m => result.Add(fun(m)));
            return result;
        }


        /// <summary>
        /// 遍历异步执行方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action">要执行的方法</param>
        /// <returns></returns>
        public static Task ForEachAsync<T>(this IList<T> enumerable, Func<T, Task> action)
        {
            return Task.WhenAll(from item in enumerable select Task.Run(() => action(item)));
        }

        /// <summary>
        /// 不为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsNotEmpty<T>(this IList<T> enumerable)
        {
            if (enumerable == null) return false;
            if (enumerable.Any()) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IList<T> enumerable)
        {
            return !(enumerable.IsNotEmpty());
        }


        /// <summary>
        ///     Checks whatever given collection object is null or has no item.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IList<T> source)
        {
            return source == null || source.Count <= 0;
        }




        /// <summary>
        ///     Concatenates the members of a constructed <see cref="IEnumerable{T}" /> collection of type System.String, using the
        ///     specified separator between each member.
        ///     This is a shortcut for string.Join(...)
        /// </summary>
        /// <param name="source">A collection that contains the strings to concatenate.</param>
        /// <param name="separator">
        ///     The string to use as a separator. separator is included in the returned string only if values
        ///     has more than one element.
        /// </param>
        /// <returns>
        ///     A string that consists of the members of values delimited by the separator string. If values has no members,
        ///     the method returns System.String.Empty.
        /// </returns>
        public static string JoinAsString(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        ///     Concatenates the members of a collection, using the specified separator between each member.
        ///     This is a shortcut for string.Join(...)
        /// </summary>
        /// <param name="source">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">
        ///     The string to use as a separator. separator is included in the returned string only if values
        ///     has more than one element.
        /// </param>
        /// <typeparam name="T">The type of the members of values.</typeparam>
        /// <returns>
        ///     A string that consists of the members of values delimited by the separator string. If values has no members,
        ///     the method returns System.String.Empty.
        /// </returns>
        public static string JoinAsString<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        ///     Filters a <see cref="IEnumerable{T}" /> by given predicate if given condition is true.
        /// </summary>
        /// <param name="source">Enumerable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the enumerable</param>
        /// <returns>Filtered or not filtered enumerable based on <paramref name="condition" /></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition
                ? source.Where(predicate)
                : source;
        }

        /// <summary>
        ///     Filters a <see cref="IEnumerable{T}" /> by given predicate if given condition is true.
        /// </summary>
        /// <param name="source">Enumerable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the enumerable</param>
        /// <returns>Filtered or not filtered enumerable based on <paramref name="condition" /></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
        {
            return condition
                ? source.Where(predicate)
                : source;
        }



        /// <summary>
        /// 忽略大小写的字符串相等比较，判断是否以任意一个待比较字符串相等
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="strs">待比较字符串数组</param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this IEnumerable<string> strs, string value)
        {
            foreach (var item in strs) {
                if (String.Equals(value, item, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }


        /// <summary>忽略大小写的字符串开始比较，判断是否以任意一个待比较字符串开始</summary>
        /// <param name="value">字符串</param>
        /// <param name="strs">待比较字符串数组</param>
        /// <returns></returns>
        public static Boolean StartsWithIgnoreCase(this IEnumerable<string> strs, string value)
        {
            if (String.IsNullOrEmpty(value)) return false;

            foreach (var item in strs) {
                if (value.StartsWith(item, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }

        /// <summary>忽略大小写的字符串结束比较，判断是否以任意一个待比较字符串结束</summary>
        /// <param name="value">字符串</param>
        /// <param name="strs">待比较字符串数组</param>
        /// <returns></returns>
        public static Boolean EndsWithIgnoreCase(this IEnumerable<string> strs, string value)
        {
            if (String.IsNullOrEmpty(value)) return false;

            foreach (var item in strs) {
                if (value.EndsWith(item, StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }


        /// <summary>忽略大小写的字符串开始比较，判断是否以任意一个待比较字符串开始</summary>
        /// <param name="value">字符串</param>
        /// <param name="strs">待比较字符串数组</param>
        /// <returns></returns>
        public static Boolean StartsWith(this IEnumerable<string> strs, string value)
        {
            if (String.IsNullOrEmpty(value)) return false;

            foreach (var item in strs) {
                if (value.StartsWith(item)) return true;
            }
            return false;
        }

        /// <summary>忽略大小写的字符串结束比较，判断是否以任意一个待比较字符串结束</summary>
        /// <param name="value">字符串</param>
        /// <param name="strs">待比较字符串数组</param>
        /// <returns></returns>
        public static Boolean EndsWith(this IEnumerable<string> strs, string value)
        {
            if (String.IsNullOrEmpty(value)) return false;

            foreach (var item in strs) {
                if (value.EndsWith(item)) return true;
            }
            return false;
        }
    }

}
