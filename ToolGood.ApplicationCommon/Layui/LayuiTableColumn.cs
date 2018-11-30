using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace ToolGood.ApplicationCommon
{
    public class LayuiTableColumns : List<LayuiTableColumn>
    {
        public void SetColumn(string field, int columnIndex, string columnTitle, string width, bool hidden)
        {
            var obj = this.Where(q => q.Field == field).FirstOrDefault();
            if (obj==null) {
                obj.ColumnIndex = columnIndex;
                obj.Title = columnTitle;
                obj.Width = width;
                obj.IsHidden = hidden;

            }
        }
        public void SetColumn(string field, int columnIndex, bool hidden)
        {
            var obj = this.Where(q => q.Field == field).FirstOrDefault();
            if (obj == null) {
                obj.ColumnIndex = columnIndex;
                obj.IsHidden = hidden;

            }
        }

        public LayuiTableColumns FirstCharToLower()
        {
            foreach (var item in this) {
                if (item.Field.Length>1) {
                    item.Field = char.ToLower(item.Field[0]) + item.Field.Substring(1);
                } else {
                    item.Field = item.Field.ToLower();
                }
            }
            return this;
        }

    }


    public class LayuiTableColumn
    {
        /// <summary>
        /// 设定字段名。字段名的设定非常重要，且是表格数据列的唯一标识
        /// </summary>
        [JsonProperty("field", NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; set; }
        /// <summary>
        /// 设定标题名称
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// 设定列宽，若不填写，则自动分配；若填写，则支持值为：数字、百分比
        /// </summary>
        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public string Width { get; set; }

        /// <summary>
        /// 局部定义当前常规单元格的最小宽度（默认：60），一般用于列宽自动分配的情况。其优先级高于基础参数中的 cellMinWidth
        /// </summary>
        [JsonProperty("minWidth", NullValueHandling = NullValueHandling.Ignore)]
        public decimal? MinWidth { get; set; }
        /// <summary>
        /// 设定列类型。可选值有：
        /// normal（常规列，无需设定）
        /// checkbox（复选框列）
        /// radio（单选框列，layui 2.4.0 新增）
        /// numbers（序号列）
        /// space（空列）
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(JsEnumConvert))]
        public LayuiTableColumnType? Type { get; set; }

        /// <summary>
        /// 是否全选状态（默认：false）。必须复选框列开启后才有效，如果设置 true，则表示复选框默认全部选中。
        /// </summary>
        [JsonProperty("LAY_CHECKED", NullValueHandling = NullValueHandling.Ignore)]
        public bool? LAY_CHECKED { get; set; }

        /// <summary>
        /// 固定列。可选值有：left（固定在左）、right（固定在右）。一旦设定，对应的列将会被固定在左或右，不随滚动条而滚动。
        /// </summary>
        [JsonProperty("fixed", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(JsEnumConvert))]
        public LayuiTableColumnFixedType? Fixed { get; set; }
        /// <summary>
        /// 是否初始隐藏列，默认：false。layui 2.4.0 新增
        /// </summary>
        [JsonProperty("hide", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Hide { get; set; }
        /// <summary>
        /// 是否开启该列的自动合计功能，默认：false。layui 2.4.0 新增
        /// </summary>
        [JsonProperty("totalRow", NullValueHandling = NullValueHandling.Ignore)]
        public bool? TotalRow { get; set; }
        /// <summary>
        /// 用于显示自定义的合计文本。layui 2.4.0 新增
        /// </summary>
        [JsonProperty("totalRowText", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalRowText { get; set; }
        /// <summary>
        /// 是否允许排序（默认：false）。如果设置 true，则在对应的表头显示排序icon，从而对列开启排序功能。
        /// 注意：不推荐对值同时存在“数字和普通字符”的列开启排序，因为会进入字典序比对。比如：'贤心' > '2' > '100'，
        /// 这可能并不是你想要的结果，但字典序排列算法（ASCII码比对）就是如此。
        /// </summary>
        [JsonProperty("sort", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Sort { get; set; }
        /// <summary>
        /// 是否禁用拖拽列宽（默认：false）。默认情况下会根据列类型（type）来决定是否禁用，如复选框列，会自动禁用。
        /// 而其它普通列，默认允许拖拽列宽，当然你也可以设置 true 来禁用该功能。
        /// </summary>
        [JsonProperty("unresize", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Unresize { get; set; }
        /// <summary>
        /// 单元格编辑类型（默认不开启）目前只支持：text（输入框）
        /// </summary>
        [JsonProperty("edit", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(JsEnumConvert))]
        public LayuiTableColumnEditType? Edit { get; set; }
        /// <summary>
        /// 自定义单元格点击事件名，以便在 tool 事件中完成对该单元格的业务处理
        /// </summary>
        [JsonProperty("event", NullValueHandling = NullValueHandling.Ignore)]
        public string Event { get; set; }
        /// <summary>
        /// 自定义单元格样式。即传入 CSS 样式
        /// </summary>
        [JsonProperty("style", NullValueHandling = NullValueHandling.Ignore)]
        public string Style { get; set; }
        /// <summary>
        /// 单元格排列方式。可选值有：left（默认）、center（居中）、right（居右）
        /// </summary>
        [JsonProperty("align", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(JsEnumConvert))]
        public LayuiAlign? Align { get; set; }
        /// <summary>
        /// 单元格所占列数（默认：1）。一般用于多级表头
        /// </summary>
        [JsonProperty("colspan", NullValueHandling = NullValueHandling.Ignore)]
        public int? Colspan { get; set; }
        /// <summary>
        /// 单元格所占行数（默认：1）。一般用于多级表头
        /// </summary>
        [JsonProperty("rowspan", NullValueHandling = NullValueHandling.Ignore)]
        public int? Rowspan { get; set; }
        /// <summary>
        /// 自定义列模板，模板遵循 laytpl 语法。这是一个非常实用的功能，你可借助它实现逻辑处理，以及将原始数据转化成其它格式，如时间戳转化为日期字符等
        /// </summary>
        [JsonProperty("templet", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(JsFunctionConvert))]
        public string Templet { get; set; }
        /// <summary>
        /// 绑定列工具条。设定后，可在每行列中出现一些自定义的操作性按钮
        /// </summary>
        [JsonProperty("toolbar", NullValueHandling = NullValueHandling.Ignore)]
        public string Toolbar { get; set; }

        [JsonIgnore]
        public int ColumnIndex { get; set; }

        [JsonIgnore]
        public bool IsHidden { get; set; }
    }
}
