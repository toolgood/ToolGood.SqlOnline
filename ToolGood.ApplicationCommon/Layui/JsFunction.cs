using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.ApplicationCommon
{
    public class JsFunctionConvert : JsonConverter
    {
        public JsFunctionConvert() { }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null) {
                writer.WriteNull();
                return;
            }
            //writer.WriteRawValue(value.ToString());
            writer.WriteRawValue(value.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }

    public class JsEnumConvert : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null) {
                writer.WriteNull();
                return;
            }
            writer.WriteValue(value.ToString());
        }
    }
}
