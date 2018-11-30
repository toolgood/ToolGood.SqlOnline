using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.ApplicationCommon
{
    public enum LayuiTableColumnType
    {
        /// <summary>
        /// 常规列，无需设定
        /// </summary>
        normal,
        /// <summary>
        /// 复选框列
        /// </summary>
        checkbox,
        /// <summary>
        /// 单选框列
        /// </summary>
        radio,
        /// <summary>
        /// 序号列
        /// </summary>
        numbers,
        /// <summary>
        /// 空列
        /// </summary>
        space
    }
    public enum LayuiTableColumnFixedType
    {
        /// <summary>
        /// 固定在左
        /// </summary>
        left,
        /// <summary>
        /// 固定在右
        /// </summary>
        right
    }
    public enum LayuiTableColumnEditType
    {
        /// <summary>
        /// 输入框
        /// </summary>
        text,
    }
    public enum LayuiAlign
    {
        /// <summary>
        /// 默认
        /// </summary>
        left,
        /// <summary>
        /// 居中
        /// </summary>
        center,
        /// <summary>
        /// 居右
        /// </summary>
        right
    }



}
