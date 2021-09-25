/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;

namespace ToolGood.Common.Utils
{
    public static class EnumUtil
    {

        /// <summary>
        /// 获取 枚举 键值对
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetDescriptions(Type type)
        {
            return ObjectExtension.GetDescriptions(type);
        }


    }
}
