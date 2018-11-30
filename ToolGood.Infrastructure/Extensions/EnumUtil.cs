using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace ToolGood.Infrastructure.Utils
{
    public static class EnumUtil
    {
        /// <summary>
        /// 根据枚举值数组获取对应的枚举列表
        /// </summary>
        /// <typeparam name="EnumType">枚举类型</typeparam>
        /// <param name="enumValues">枚举值</param>
        /// <returns>枚举列表</returns>
        public static EnumType[] GetEnumType<EnumType>(string[] enumValues)
        {
            bool flag = false;
            foreach (var item in enumValues) {
                if (string.IsNullOrEmpty(item)) {
                    flag = true;
                    break;
                }
            }
            EnumType[] result;
            if (flag) {
                result = null;
            } else {
                Type enumType = typeof(EnumType);
                List<EnumType> enumList = new List<EnumType>();
                foreach (string item in enumValues) {
                    int value;
                    if (int.TryParse(item,out value)==false) {
                        value = -100;
                    }
                    bool flag2 = Enum.IsDefined(enumType, value);
                    if (flag2) {
                        enumList.Add((EnumType)((object)Enum.Parse(enumType, item)));
                    }
                }
                result = enumList.ToArray();
            }
            return result;
        }

        /// <summary>
        /// 根据枚举值获取枚举描述
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>枚举值对应的描述</returns>
        public static string GetDesc(Enum enumValue)
        {
            Dictionary<string, string> dic = EnumUtil.GetDesc(enumValue.GetType());
            string key = Convert.ToInt32(enumValue).ToString();
            bool flag = dic != null && dic.ContainsKey(key);
            string result;
            if (flag) {
                result = dic[key];
            } else {
                result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// 根据枚举值获取枚举描述
        /// </summary>
        /// <typeparam name="EnumType">枚举类型</typeparam>
        /// <param name="value">枚举值（数字）</param>
        /// <returns>枚举值对应的描述</returns>
        public static string GetDesc<EnumType>(string value)
        {
            Dictionary<string, string> dic = EnumUtil.GetDesc<EnumType>();
            bool flag = dic != null && dic.ContainsKey(value);
            string result;
            if (flag) {
                result = dic[value];
            } else {
                result = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// 获取枚举类型的说明信息
        /// </summary>
        /// <typeparam name="EnumType">枚举类型</typeparam>
        /// <returns>枚举类型的说明信息</returns>
        public static Dictionary<string, string> GetDesc<EnumType>()
        {
            return EnumUtil.GetDesc(typeof(EnumType));
        }

        /// <summary>
        /// 获取枚举类型的说明信息
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>枚举类型的说明信息</returns>
        public static Dictionary<string, string> GetDesc(Type enumType)
        {
            bool flag = enumType == null;
            Dictionary<string, string> result;
            if (flag) {
                result = new Dictionary<string, string>();
            } else {
                RuntimeTypeHandle typeHandle = enumType.TypeHandle;
                result = GetValue(_enumDescCache, typeHandle, () => GetDescriptions(enumType));
            }
            return result;
        }
        /// <summary>
        /// ConcurrentDictionary 获取值的扩展方法
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TValue">值类型</typeparam>
        /// <param name="conDic">字典集合</param>
        /// <param name="key">键值</param>
        /// <param name="action">获取值的方法(使用时需要执行注意方法闭包)</param>
        /// <returns>指定键的值</returns>
        private static TValue GetValue<TKey, TValue>(ConcurrentDictionary<TKey, TValue> conDic, TKey key, Func<TValue> action)
        {
            TValue value;
            bool flag = conDic.TryGetValue(key, out value);
            TValue result;
            if (flag) {
                result = value;
            } else {
                value = action();
                conDic[key] = value;
                result = value;
            }
            return result;
        }


        /// <summary>
        /// 根据枚举类型获取枚举说明
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>枚举说明</returns>
        private static Dictionary<string, string> GetDescriptions(Type enumType)
        {
            Dictionary<string, string> enumDic = new Dictionary<string, string>();
            Array enumList = Enum.GetValues(enumType);
            foreach (object obj in enumList) {
                int item = (int)obj;
                FieldInfo fieldInfo = enumType.GetField(Enum.GetName(enumType, item));
                bool flag = fieldInfo != null;
                if (flag) {
                    DescriptionAttribute[] customAttributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                    bool flag2 = customAttributes != null && customAttributes.Length == 1;
                    if (flag2) {
                        enumDic.Add(item.ToString(), customAttributes[0].Description);
                    }
                }
            }
            return enumDic;
        }


        private static ConcurrentDictionary<RuntimeTypeHandle, Dictionary<string, string>> _enumDescCache = new ConcurrentDictionary<RuntimeTypeHandle, Dictionary<string, string>>();
    }
}
