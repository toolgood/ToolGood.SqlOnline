using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace ToolGood.Infrastructure.MvcBase
{
    public static class ObjectExtensions
    {
        #region ToHtml
        public static HtmlString ToHtml(this string txt, bool replaceEnter = false)
        {
            if (replaceEnter) {
                txt = txt.Replace("\r\n", "<br />").Replace("\n", "<br />").Replace("\r", "<br />");
            }
            return new HtmlString(txt);
        }
        public static HtmlString ToHtml(this bool b, string trueString, string falseString = "")
        {
            var txt = b ? trueString : falseString;
            return new HtmlString(txt);
        }
        public static HtmlString ToHtmlWhenIs(this string txt, string equalString, string trueString, string falseString = "")
        {
            var t = txt == equalString ? trueString : falseString;
            return new HtmlString(t);
        }

        public static HtmlString ToHtml(this DateTime? dt, string fmt)
        {
            if (dt != null) {
                return new HtmlString(dt.Value.ToString(fmt));
            }
            return new HtmlString("");

        }
        public static HtmlString ToHtml(this DateTime? dt, string fmt, DateTime @default)
        {
            if (dt != null) {
                return new HtmlString(dt.Value.ToString(fmt));
            }
            return new HtmlString(@default.ToString(fmt));
        }

        public static HtmlString ToHtml(this bool? b)
        {
            if (b.HasValue) {
                return new HtmlString(b.Value.ToString());
            }
            return new HtmlString("");
        }
        public static HtmlString ToHtml(this bool? b, string TrueString, string FalseString = "")
        {
            if (b.HasValue) {
                if (b.Value) {
                    return new HtmlString(TrueString);
                }
                return new HtmlString(FalseString);
            }
            return new HtmlString("");
        }
        public static HtmlString ToHtml(this short? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return new HtmlString(num.Value.ToString());
                }
                return new HtmlString(num.Value.ToString(fmt));
            }
            return new HtmlString("");
        }
        public static HtmlString ToHtml(this int? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return new HtmlString(num.Value.ToString());
                }
                return new HtmlString(num.Value.ToString(fmt));
            }
            return new HtmlString("");
        }
        public static HtmlString ToHtml(this long? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return new HtmlString(num.Value.ToString());
                }
                return new HtmlString(num.Value.ToString(fmt));
            }
            return new HtmlString("");
        }
        public static HtmlString ToHtml(this ushort? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return new HtmlString(num.Value.ToString());
                }
                return new HtmlString(num.Value.ToString(fmt));
            }
            return new HtmlString("");
        }
        public static HtmlString ToHtml(this uint? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return new HtmlString(num.Value.ToString());
                }
                return new HtmlString(num.Value.ToString(fmt));
            }
            return new HtmlString("");
        }
        public static HtmlString ToHtml(this ulong? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return new HtmlString(num.Value.ToString());
                }
                return new HtmlString(num.Value.ToString(fmt));
            }
            return new HtmlString("");
        }
        public static HtmlString ToHtml(this float? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return new HtmlString(num.Value.ToString());
                }
                return new HtmlString(num.Value.ToString(fmt));
            }
            return new HtmlString("");
        }
        public static HtmlString ToHtml(this double? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return new HtmlString(num.Value.ToString());
                }
                return new HtmlString(num.Value.ToString(fmt));
            }
            return new HtmlString("");
        }
        public static HtmlString ToHtml(this decimal? num, string fmt)
        {
            if (num.HasValue) {
                if (string.IsNullOrEmpty(fmt)) {
                    return new HtmlString(num.Value.ToString());
                }
                return new HtmlString(num.Value.ToString(fmt));
            }
            return new HtmlString("");
        }
        #endregion

        #region ToOption
        public static HtmlString ToOption(this IEnumerable<int> list)
        {
            string html = "<option value=\"{0}\">{1}</option>";
            StringBuilder sb = new StringBuilder();
            foreach (var item in list) {
                sb.AppendFormat(html, item, item);
            }
            return new HtmlString(sb.ToString());
        }
        public static HtmlString ToOption(this IEnumerable<int> list, int value)
        {
            string html = "<option value=\"{0}\" {2}>{1}</option>";
            StringBuilder sb = new StringBuilder();
            foreach (var item in list) {
                var selected = item == value ? "selected" : "";
                sb.AppendFormat(html, item, item, selected);
            }
            return new HtmlString(sb.ToString());
        }

        public static HtmlString ToOption(this IEnumerable<string> list)
        {
            string html = "<option value=\"{0}\">{1}</option>";
            StringBuilder sb = new StringBuilder();
            foreach (var item in list) {
                sb.AppendFormat(html, item, item);
            }
            return new HtmlString(sb.ToString());
        }
        public static HtmlString ToOption(this IEnumerable<string> list, string value)
        {
            string html = "<option value=\"{0}\" {2}>{1}</option>";
            StringBuilder sb = new StringBuilder();
            foreach (var item in list) {
                var selected = item == value ? "selected" : "";
                sb.AppendFormat(html, item, item, selected);
            }
            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Value 为显示的值  Key为传送的值
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static HtmlString ToOption<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            string html = "<option value=\"{0}\">{1}</option>";
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<TKey, TValue> item in dict) {
                sb.AppendFormat(html, item.Key, item.Value);
            }
            return new HtmlString(sb.ToString());
        }
        /// <summary>
        /// Value 为显示的值  Key为传送的值
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static HtmlString ToOption<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey value)
        {
            string html = "<option value=\"{0}\" {2}>{1}</option>";
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<TKey, TValue> item in dict) {
                var selected = object.Equals(item.Key, value) ? "selected" : "";
                sb.AppendFormat(html, item.Key, item.Value, selected);
            }
            return new HtmlString(sb.ToString());
        }

        public static HtmlString ToOption<T>(this IEnumerable<T> list, Expression<Func<T, string>> show) where T : class
        {
            List<string> tlist = new List<string>();
            foreach (var item in list) {
                var k = show.Compile()(item);
                tlist.Add(k);
            }
            return tlist.ToOption();
        }
        public static HtmlString ToOption<T>(this IEnumerable<T> list, Expression<Func<T, int>> show) where T : class
        {
            List<string> tlist = new List<string>();
            foreach (var item in list) {
                var k = show.Compile()(item);
                tlist.Add(k.ToString());
            }
            return tlist.ToOption();
        }
        public static HtmlString ToOption<T>(this IEnumerable<T> list, Expression<Func<T, string>> show, string value) where T : class
        {
            List<string> tlist = new List<string>();
            foreach (var item in list) {
                var k = show.Compile()(item);
                tlist.Add(k);
            }
            return tlist.ToOption(value);
        }
        public static HtmlString ToOption<T>(this IEnumerable<T> list, Expression<Func<T, int>> show, int value) where T : class
        {
            List<string> tlist = new List<string>();
            foreach (var item in list) {
                var k = show.Compile()(item);
                tlist.Add(k.ToString());
            }
            return tlist.ToOption(value.ToString());
        }

        public static HtmlString ToOption<T, TKey, TValue>(this IEnumerable<T> list,
            Expression<Func<T, TValue>> showFun, Expression<Func<T, TKey>> valueFun) where T : class
        {
            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
            foreach (var item in list) {
                var s = showFun.Compile()(item);
                var v = valueFun.Compile()(item);
                dict[v] = s;
            }
            return dict.ToOption();
        }
        public static HtmlString ToOption<T, TKey, TValue>(this IEnumerable<T> list,
            Expression<Func<T, TValue>> showFun, Expression<Func<T, TKey>> valueFun, TKey value) where T : class
        {
            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
            foreach (var item in list) {
                var s = showFun.Compile()(item);
                var v = valueFun.Compile()(item);
                dict[v] = s;
            }
            return dict.ToOption(value);
        }


        public static HtmlString ToOptionWithGroup<T>(this IEnumerable<T> list, Expression<Func<T, string>> group, Expression<Func<T, string>> show)
            where T : class
        {
            return ToOptionWithGroup(list, group, show, show, new string[0]);
        }
        public static HtmlString ToOptionWithGroup<T>(this IEnumerable<T> list, Expression<Func<T, string>> group, Expression<Func<T, string>> show, string value)
            where T : class
        {
            return ToOptionWithGroup(list, group, show, show, new string[1] { value });
        }
        public static HtmlString ToOptionWithGroup<T>(this IEnumerable<T> list, Expression<Func<T, string>> group, Expression<Func<T, string>> show, string[] values)
            where T : class
        {
            return ToOptionWithGroup(list, group, show, show, values);
        }
        public static HtmlString ToOptionWithGroup<T>(this IEnumerable<T> list, Expression<Func<T, string>> group, Expression<Func<T, string>> key, Expression<Func<T, string>> show)
            where T : class
        {
            return ToOptionWithGroup(list, group, key, show, new string[0] { });
        }
        public static HtmlString ToOptionWithGroup<T>(this IEnumerable<T> list, Expression<Func<T, string>> group, Expression<Func<T, string>> key, Expression<Func<T, string>> show, string value)
            where T : class
        {
            return ToOptionWithGroup(list, group, key, show, new string[1] { value });
        }

        public static HtmlString ToOptionWithGroup<T>(this IEnumerable<T> list, Expression<Func<T, string>> group, Expression<Func<T, string>> key, Expression<Func<T, string>> show, string[] vvalues)
            where T : class
        {
            List<Tuple<string, string, string>> groups = new List<Tuple<string, string, string>>();
            foreach (var item in list) {
                var g = group.Compile()(item);
                if (g == null) g = "";
                var k = key.Compile()(item);
                var s = show.Compile()(item);
                groups.Add(Tuple.Create(g, k, s));
            }
            return toOption(groups, vvalues);
        }


        /// <summary>
        /// list 结构为  1 Group 2 Show 3 Value
        /// </summary>
        /// <param name="list"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private static HtmlString toOption(List<Tuple<string, string, string>> list, string[] values)
        {
            var gs = list.Select(q => q.Item1).OrderBy(q => q).Distinct().ToList();
            StringBuilder sb = new StringBuilder();
            string html = "<option value=\"{1}\">{0}</option>";
            if (values.Length > 0) html = "<option value=\"{1}\" {2}>{0}</option>";

            foreach (var g in gs) {
                if (g != "") sb.AppendFormat("<optgroup label=\"{0}\">", g);
                foreach (var item in list.Where(q => q.Item1 == g)) {
                    var selected = values.Contains(item.Item3) ? "selected" : "";
                    sb.AppendFormat(html, item.Item2, item.Item3, selected);
                }
                if (g != "") sb.Append("</optgroup>");
            }
            return new HtmlString(sb.ToString());
        }

        #endregion

        #region ToJson

        public static HtmlString ToJson<T>(this T obj, bool toUncode = false)
            where T : class
        {
            var json = JsonConvert.SerializeObject(obj);
            //var json = LitJson.JsonMapper.ToJson(obj);
            if (toUncode) {
                return new HtmlString(uncode(json));
            }
            return new HtmlString(json);
        }
        //private static void toJson()
        //{

        //}
        private static string uncode(string str)
        {
            StringBuilder outStr = new StringBuilder();
            for (int i = 0; i < str.Length; i++) {
                if (((int)str[i]) <= 256 && ((int)str[i]) >= 0) {
                    outStr.Append(str[i]);
                } else {
                    outStr.Append("\\u");
                    outStr.Append(((int)str[i]).ToString("x"));
                }
            }
            return outStr.ToString();
        }
        #endregion
    }

}
