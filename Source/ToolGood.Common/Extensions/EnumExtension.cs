/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;

namespace System
{
    public static partial class ObjectExtension
    {
        private static ConcurrentDictionary<RuntimeTypeHandle, Dictionary<int, string>> _descriptionCache = new ConcurrentDictionary<RuntimeTypeHandle, Dictionary<int, string>>();

        /// <summary>
        /// 获取枚举对象描述信息
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum @enum)
        {
            if (@enum == null) return null;
            var type = @enum.GetType();
            var typeHandle = type.TypeHandle;
            Dictionary<int, string> dict;
            if (_descriptionCache.TryGetValue(typeHandle, out dict)) {
                return dict[Convert.ToInt32(@enum)];
            } else {
                dict = new Dictionary<int, string>();

                var enumList = Enum.GetValues(type);
                foreach (int item in enumList) {
                    var field = type.GetField(Enum.GetName(type, item));
                    if (field == null) continue;

                    var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                    if ((attr != null) && (attr.Length == 1)) {
                        dict.Add(item, attr[0].Description);
                        continue;
                    }

                }

                _descriptionCache.TryAdd(typeHandle, dict);
                return dict[Convert.ToInt32(@enum)];
            }
        }

        public static string GetDescriptionNull(this Enum @enum)
        {
            if (@enum == null) return null;
            var type = @enum.GetType();
            var typeHandle = type.TypeHandle;
            Dictionary<int, string> dict;
            if (_descriptionCache.TryGetValue(typeHandle, out dict)) {
                if (dict.TryGetValue(Convert.ToInt32(@enum), out string result)) {
                    return result;
                }
            } else {
                dict = new Dictionary<int, string>();

                var enumList = Enum.GetValues(type);
                foreach (int item in enumList) {
                    var field = type.GetField(Enum.GetName(type, item));
                    if (field == null) continue;

                    var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                    if ((attr != null) && (attr.Length == 1)) {
                        dict.Add(item, attr[0].Description);
                        continue;
                    }

                }
                _descriptionCache.TryAdd(typeHandle, dict);

                if (dict.TryGetValue(Convert.ToInt32(@enum), out string result)) {
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetDescriptions(Type type)
        {
            var typeHandle = type.TypeHandle;
            Dictionary<int, string> dict;
            if (_descriptionCache.TryGetValue(typeHandle, out dict)) {
                return dict;
            } else {
                dict = new Dictionary<int, string>();

                var enumList = Enum.GetValues(type);
                foreach (int item in enumList) {
                    var field = type.GetField(Enum.GetName(type, item));
                    if (field == null) continue;

                    var attr = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                    if ((attr != null) && (attr.Length == 1)) {
                        dict.Add(item, attr[0].Description);
                        continue;
                    }

                }
                _descriptionCache.TryAdd(typeHandle, dict);
                return dict;
            }
        }




    }
}
