using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using ToolGood.Infrastructure.Utils;

namespace System
{
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// 获取枚举值的描述
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>枚举值的描述</returns>
        
        public static string Desc(this Enum enumValue)
        {
            bool flag = enumValue == null;
            string result;
            if (flag) {
                result = string.Empty;
            } else {
                result = EnumUtil.GetDesc(enumValue);
            }
            return result;
        }



    }
}
