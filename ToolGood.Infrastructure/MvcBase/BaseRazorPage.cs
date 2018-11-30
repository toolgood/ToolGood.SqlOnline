using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Threading.Tasks;

namespace ToolGood.Infrastructure
{
    public class BaseRazorPage<TModel> : RazorPage<TModel>
    {
        public override Task ExecuteAsync()
        {
            return new Task(() => { });

        }

        /// <summary>
        /// json请求成功码
        /// </summary>
        public static int SuccessCode {
            get { return KeywordString.SuccessCode; }
        }

        /// <summary>
        /// json请求错误码
        /// </summary>
        public static int ErrorCode {
            get { return KeywordString.ErrorCode; }
        }


        public void WriteHtml(string html)
        {
            Write(new HtmlString(html));
        }


        public HtmlString ToJson(object obj)
        {
            if (Object.Equals(null, obj)) return new HtmlString("");

            var jsonSerizlizerSetting = new JsonSerializerSettings();
            //设置取消循环引用
            jsonSerizlizerSetting.MissingMemberHandling = MissingMemberHandling.Ignore;
            //设置首字母小写
            jsonSerizlizerSetting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSerizlizerSetting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            jsonSerizlizerSetting.NullValueHandling = NullValueHandling.Ignore;
            jsonSerizlizerSetting.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;

            var json = JsonConvert.SerializeObject(obj, Formatting.None, jsonSerizlizerSetting);
            return new HtmlString(json);
        }
    }

}
