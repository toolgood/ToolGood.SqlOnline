using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Collections.Generic
{
    public static class DictionaryExtension
    {

        /// <summary>
        /// 添加或者修改
        /// </summary>
        /// <typeparam name="T">值的类型</typeparam>
        /// <param name="data"></param>
        /// <param name="key">key</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        
        public static Dictionary<string, T> AddOrUpdate<T>(this Dictionary<string, T> data, string key, T value)
        {
            bool flag = data == null;
            if (flag) {
                data = new Dictionary<string, T>(StringComparer.InvariantCultureIgnoreCase);
            }
            if (string.IsNullOrWhiteSpace(key)==false) {
                bool flag3 = data.ContainsKey(key);
                if (flag3) {
                    data[key] = value;
                } else {
                    data.Add(key, value);
                }
            }
            return data;
        }

        /// <summary>
        /// 对字典按键进行排序并进行拼接成字符串
        /// </summary>
        /// <param name="data">字典集合</param>
        /// <returns>拼接后的字符串</returns>
        
        public static string GetSortedContent<T>(this Dictionary<string, T> data)
        {
            bool flag = data.Count > 0;
            string result;
            if (flag) {
                List<string> keys = data.Keys.ToList<string>();
                keys.Sort();
                StringBuilder context = new StringBuilder();
                int currentIndex = 0;
                keys.ForEach(delegate (string key) {
                    bool flag2 = currentIndex != 0;
                    if (flag2) {
                        context.Append("&");
                    }
                    context.Append(key).Append("=").Append(data[key]);
                    currentIndex++;
                });
                result = context.ToString();
            } else {
                result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// 将字典内容以key=value的形式进行拼接
        /// </summary>
        /// <param name="requestData">字典内容</param>
        /// <param name="charset">转换格式，默认为UTF-8</param>
        /// <returns>key=value的形式进行拼接的文本</returns>
        
        public static string ToRequestContent<T>(this Dictionary<string, T> requestData, string charset = null)
        {
            bool flag = requestData == null || requestData.Count <= 0;
            string result;
            if (flag) {
                result = string.Empty;
            } else {
                charset = (charset ?? "UTF-8");
                StringBuilder query = new StringBuilder();
                bool hasParam = false;
                IOrderedEnumerable<KeyValuePair<string, T>> orderedData = from m in requestData
                                                                          orderby m.Key
                                                                          select m;
                foreach (KeyValuePair<string, T> item in orderedData) {
                    bool flag2 = hasParam;
                    if (flag2) {
                        query.Append("&");
                    } else {
                        hasParam = true;
                    }
                    query.Append(item.Key).Append("=").Append((item.Value?.ToString()??"").UrlEncode(charset));
                }
                result = query.ToString();
            }
            return result;
        }

        /// <summary>
        /// 将对象转为字典，NULL时直接返回空字典
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns></returns>
        
        public static Dictionary<string, object> ToDictionary(this object obj)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();
            bool flag = obj == null;
            Dictionary<string, object> result;
            if (flag) {
                result = map;
            } else {
                var propertyList = obj.GetType().GetProperties();
                foreach (PropertyInfo p in propertyList) {
                    MethodInfo mi = p.GetGetMethod();
                    bool flag2 = mi != null && mi.IsPublic;
                    if (flag2) {
                        map.Add(p.Name, mi.Invoke(obj, new object[0]));
                    }
                }
                result = map;
            }
            return result;
        }

        /// <summary>
        /// 根据键移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key">键值</param>
        /// <returns></returns>
        
        public static Dictionary<string, T> RemoveKey<T>(this Dictionary<string, T> data, string key)
        {
            bool flag = data == null;
            Dictionary<string, T> result;
            if (flag) {
                result = data;
            } else {
                bool flag2 = data.ContainsKey(key);
                if (flag2) {
                    data.Remove(key);
                }
                result = data;
            }
            return result;
        }
    }
}
