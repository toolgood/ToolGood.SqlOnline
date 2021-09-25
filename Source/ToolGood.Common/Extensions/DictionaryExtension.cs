/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;

namespace System
{
    public static partial class ObjectExtension
    {
        /// <summary>
        /// 尝试更新，注：字典为空，默认new 一个字典对象
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="data">为空时，默认new 一个字典对象</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<T1, T2> TryUpdateOnly<T1, T2>(this Dictionary<T1, T2> data, T1 key, T2 value)
        {
            if (data == null) { data = new Dictionary<T1, T2>(); }
            if (null != key) {
                if (data.ContainsKey(key)) {
                    data[key] = value;
                }
            }
            return data;
        }

        /// <summary>
        /// 尝试更新，注：字典为空，默认new 一个字典对象
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="data">为空时，默认new 一个字典对象</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Dictionary<T1, T2> TryUpdateOnly<T1, T2>(this Dictionary<T1, T2> data, T1 key, Func<T2> value)
        {
            if (data == null) { data = new Dictionary<T1, T2>(); }
            if (null != key) {
                if (data.ContainsKey(key)) {
                    data[key] = value();
                }
            }
            return data;
        }

        /// <summary>
        /// 批量添加，注：字典为空，默认new 一个字典对象
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="data">为空时，默认new 一个字典对象</param>
        /// <param name="list"></param>
        /// <param name="keyFun"></param>
        /// <param name="valueFun"></param>
        /// <returns></returns>
        public static IDictionary<T1, T2> AddRange<T1, T2, T3>(this IDictionary<T1, T2> data, IEnumerable<T3> list, Func<T3, T1> keyFun, Func<T3, T2> valueFun)
        {
            if (data == null) { data = new Dictionary<T1, T2>(); }

            foreach (var item in list) {
                data[keyFun(item)] = valueFun(item);
            }
            return data;
        }

        /// <summary>
        /// 尝试获取，不存在则添加，注：字典为空，默认new 一个字典对象
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="data">为空时，默认new 一个字典对象</param>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T2 TryGetOrAdd<T1, T2>(this Dictionary<T1, T2> data, T1 key, Func<T2> func)
        {
            if (data == null) { data = new Dictionary<T1, T2>(); }

            if (data.TryGetValue(key, out T2 t2)) { return t2; }
            var val = func();
            data[key] = val;
            return val;
        }


        /// <summary>
        /// 尝试获取，不存在则添加，注：字典为空，默认new 一个字典对象
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="data">为空时，默认new 一个字典对象</param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T2 TryGetOrAdd<T1, T2>(this Dictionary<T1, T2> data, T1 key, T2 defaultValue)
        {
            if (data == null) { data = new Dictionary<T1, T2>(); }

            if (data.TryGetValue(key, out T2 t2)) { return t2; }
            data[key] = defaultValue;
            return defaultValue;
        }


 
    }
}
