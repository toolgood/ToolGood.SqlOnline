/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ToolGood.Common.Internals
{
    public class JsonCustomDoubleNullConvert : CustomCreationConverter<double?>
    {
        public override bool CanWrite { get { return true; } }
        public override double? Create(Type objectType) { return null; }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) { return reader.Value; }
        /// <summary>
        /// 重载序列化方法
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null || ((double?)value).HasValue == false) {
                writer.WriteNull();
            } else {
                var d = Math.Round(((double?)value).Value, 10);
                writer.WriteValue(d);
            }
        }
    }
}
