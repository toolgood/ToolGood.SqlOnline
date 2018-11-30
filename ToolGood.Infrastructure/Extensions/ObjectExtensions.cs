using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace System
{
    public static partial class ObjectExtensions
    {
        #region ToString
        public static string ToString(this DateTime? dt, string fmt)
        {
            if (dt != null) {
                return ((DateTime)dt).ToString(fmt);
            }
            return "";
        }
        public static string ToString(this DateTime? dt, string fmt, DateTime @default)
        {
            if (dt != null) {
                return ((DateTime)dt).ToString(fmt);
            }
            return @default.ToString(fmt);
        }
        public static string ToString(this bool b, string trueString, string falseString = "")
        {
            return b ? trueString : falseString;
        }
        public static string ToString(this bool? b)
        {
            if (b.HasValue) {
                return b.Value.ToString();
            }
            return "";
        }
        public static string ToString(this bool? b, string TrueString, string FalseString = "")
        {
            if (b.HasValue) {
                if (b.Value) {
                    return TrueString;
                }
                return FalseString;
            }
            return "";
        }
        public static string ToString(this short? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return num.Value.ToString();
                }
                return num.Value.ToString(fmt);
            }
            return "";
        }
        public static string ToString(this int? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return num.Value.ToString();
                }
                return num.Value.ToString(fmt);
            }
            return "";
        }
        public static string ToString(this long? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return num.Value.ToString();
                }
                return num.Value.ToString(fmt);
            }
            return "";
        }
        public static string ToString(this ushort? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return num.Value.ToString();
                }
                return num.Value.ToString(fmt);
            }
            return "";
        }
        public static string ToString(this uint? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return num.Value.ToString();
                }
                return num.Value.ToString(fmt);
            }
            return "";
        }
        public static string ToString(this ulong? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return num.Value.ToString();
                }
                return num.Value.ToString(fmt);
            }
            return "";
        }
        public static string ToString(this float? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return num.Value.ToString();
                }
                return num.Value.ToString(fmt);
            }
            return "";
        }
        public static string ToString(this double? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return num.Value.ToString();
                }
                return num.Value.ToString(fmt);
            }
            return "";
        }
        public static string ToString(this decimal? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return num.Value.ToString();
                }
                return num.Value.ToString(fmt);
            }
            return "";
        }
        #endregion

        #region DeepClone
        /// <summary>
        /// 对象拷贝
        /// </summary>
        /// <param name="source">被复制对象</param>
        /// <returns>新对象</returns>
        public static T DeepClone<T>(this T source)
        {
            if (source is ICloneable) {
                return (T)(source as ICloneable).Clone();
            }
            return (T)Copy(source);
        }

        private static object Copy(object obj)
        {
            if (obj == null) return null;

            Object targetDeepCopyObj;
            Type targetType = obj.GetType();
            //值类型
            if (obj.GetType().IsValueType == true) {
                targetDeepCopyObj = obj;
            } else {
                targetDeepCopyObj = System.Activator.CreateInstance(targetType);   //创建引用对象 
                System.Reflection.MemberInfo[] memberCollection = obj.GetType().GetMembers();

                foreach (System.Reflection.MemberInfo member in memberCollection) {
                    if (member.MemberType == System.Reflection.MemberTypes.Field) {
                        System.Reflection.FieldInfo field = (System.Reflection.FieldInfo)member;
                        Object fieldValue = field.GetValue(obj);
                        if (fieldValue is ICloneable) {
                            field.SetValue(targetDeepCopyObj, (fieldValue as ICloneable).Clone());
                        } else {
                            field.SetValue(targetDeepCopyObj, Copy(fieldValue));
                        }

                    } else if (member.MemberType == System.Reflection.MemberTypes.Property) {
                        System.Reflection.PropertyInfo myProperty = (System.Reflection.PropertyInfo)member;
                        if (myProperty.CanWrite && myProperty.CanRead) {
                            object propertyValue = myProperty.GetValue(obj, null);
                            if (propertyValue is ICloneable) {
                                myProperty.SetValue(targetDeepCopyObj, (propertyValue as ICloneable).Clone(), null);
                            } else {
                                myProperty.SetValue(targetDeepCopyObj, Copy(propertyValue), null);
                            }
                        }
                    }
                }
            }
            return targetDeepCopyObj;
        }

        #endregion


        #region ToFormatedString
        /// <summary>
        /// object 转换为自定义格式的字符串
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="format">自定义格式</param>
        /// <param name="defaultValue">object为null时的默认输出</param>
        /// <returns></returns>
        public static string ToFormatedString(this object obj, string format, string defaultValue = "")
        {
            bool flag = obj == null || string.IsNullOrWhiteSpace(obj.ToString());
            string result;
            if (flag) {
                result = defaultValue;
            } else {
                bool flag2 = string.IsNullOrWhiteSpace(format);
                if (flag2) {
                    result = obj.ToString();
                } else {
                    decimal tmpDecimal = 0m;
                    bool flag3 = decimal.TryParse(obj.ToString(), out tmpDecimal);
                    if (flag3) {
                        result = tmpDecimal.ToString(format, null);
                    } else {
                        IFormattable formattableObj = obj as IFormattable;
                        bool flag4 = formattableObj == null;
                        if (flag4) {
                            result = obj.ToString();
                        } else {
                            result = formattableObj.ToString(format, null);
                        }
                    }
                }
            }
            return result;
        }
        #endregion


        #region ToHideString
        /// <summary>
        /// object 转换为指定的隐藏字符串
        /// </summary>
        /// <param name="obj">目标对象</param>
        /// <param name="frontLen">前面保留显示位数</param>
        /// <param name="endLen">后面保留显示位数</param> 
        /// <param name="spaceModNum">每隔多少个字符增加一个空格，默认为4个字符</param> 
        /// <param name="hideFormatStr">隐藏符，默认为“*”号</param> 
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string ToHideString(this string obj, int frontLen, int endLen, int spaceModNum = 4, string hideFormatStr = "*", string defaultValue = "")
        {
            bool flag = obj == null || string.IsNullOrWhiteSpace(obj.ToString());
            string result;
            if (flag) {
                result = defaultValue;
            } else {
                string targetStr = obj.ToString().Trim();
                bool flag2 = targetStr.Length <= frontLen + endLen;
                if (flag2) {
                    result = targetStr;
                } else {
                    StringBuilder strBuilder = new StringBuilder();
                    string hideStr = string.IsNullOrWhiteSpace(hideFormatStr) ? "*" : hideFormatStr;
                    for (int i = 0; i < targetStr.Length; i++) {
                        bool flag3 = i < frontLen || i >= targetStr.Length - endLen;
                        if (flag3) {
                            bool flag4 = i != 0 && i % spaceModNum == 0;
                            if (flag4) {
                                strBuilder.Append(" ");
                                strBuilder.Append(targetStr[i]);
                            } else {
                                strBuilder.Append(targetStr[i]);
                            }
                        } else {
                            bool flag5 = i != 0 && i % spaceModNum == 0;
                            if (flag5) {
                                strBuilder.Append(" ");
                                strBuilder.Append(hideStr);
                            } else {
                                strBuilder.Append(hideStr);
                            }
                        }
                    }
                    result = strBuilder.ToString();
                }
            }
            return result;
        }
        #endregion

        #region UrlEncode
        /// <summary>
        /// 对字符串进行Url编码
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        /// <param name="encodeName">编码格式</param>
        /// <returns>Url编码后的字符</returns>
        public static string UrlEncode(this string str, string encodeName)
        {
            return HttpUtility.UrlEncode(str, Encoding.GetEncoding(encodeName));
        }

        /// <summary>
        /// 对字符串进行Url编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }
        #endregion

        #region UrlDecode
        /// <summary>
        /// 对字符串进行Url解码
        /// </summary>
        /// <param name="str">需要解码的字符串</param>
        /// <param name="decodeName">编码格式</param>
        /// <returns>Url解码后的字符串</returns>
        public static string UrlDecode(this string str, string decodeName)
        {
            return HttpUtility.UrlDecode(str, Encoding.GetEncoding(decodeName));
        }

        /// <summary>
        /// 对字符串进行Url解码
        /// </summary>
        /// <param name="str">需要解码的字符串</param>
        /// <returns>Url解码后的字符串</returns>
        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        } 
        #endregion
    }
}
