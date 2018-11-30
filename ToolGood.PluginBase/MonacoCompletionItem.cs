using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel;

namespace ToolGood.PluginBase
{
    public class MonacoCompletionItem
    {

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("kind")]
        [JsonConverter(typeof(CompletionItemKindConvert))]
        public CompletionItemKind Kind { get; set; }

        [JsonProperty("detail", NullValueHandling = NullValueHandling.Ignore)]
        public string Detail { get; set; }

        [JsonProperty("documentation", NullValueHandling = NullValueHandling.Ignore)]
        public string Documentation { get; set; }

        [JsonProperty("insertText", NullValueHandling = NullValueHandling.Ignore)]
        public string InsertText { get; set; }

    }
    public enum CompletionItemKind
    {
        Text = 0,
        //Method = 1,
        Function = 2,
        //Constructor = 3,
        //Field = 4,
        //Variable = 5,
        //Class = 6,
        //Interface = 7,
        //Module = 8,
        //Property = 9,
        //Unit = 10,
        Value = 11,
        //Enum = 12,
        Keyword = 13,
        Snippet = 14,
        //Color = 15,
        //File = 16,
        //Reference = 17,
        //Folder = 18
    }
    public class CompletionItemKindConvert : JsonConverter
    {
        public override bool CanConvert(Type objectType) { throw new NotImplementedException(); }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) { throw new NotImplementedException(); }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var v = (CompletionItemKind)value;
            switch (v) {
                case CompletionItemKind.Function:
                    writer.WriteValue("monaco.languages.CompletionItemKind.Function");
                    break;
                case CompletionItemKind.Keyword:
                    writer.WriteValue("monaco.languages.CompletionItemKind.Keyword");
                    break;
                case CompletionItemKind.Snippet:
                    writer.WriteValue("monaco.languages.CompletionItemKind.Snippet");
                    break;
                case CompletionItemKind.Text:
                    writer.WriteValue("monaco.languages.CompletionItemKind.Text");
                    break;
                case CompletionItemKind.Value:
                    writer.WriteValue("monaco.languages.CompletionItemKind.Value");
                    break;
                default:
                    writer.WriteNull();
                    break;
            }

        }
    }
}
