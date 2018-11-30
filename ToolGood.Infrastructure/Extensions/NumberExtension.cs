using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// 将double数字四舍五入保留两位小数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double RoundWith2Digits(this double input)
        {
            return (double)Math.Round((decimal)input, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 将decimal数字四舍五入保留两位小数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static decimal RoundWith2Digits(this decimal input)
        {
            return Math.Round(input, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 将double数字四舍五入保留两位小数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double Round(this double input,int point)
        {
            return (double)Math.Round((decimal)input, point, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 将decimal数字四舍五入保留两位小数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static decimal Round(this decimal input, int point)
        {
            return Math.Round(input, point, MidpointRounding.AwayFromZero);
        }


    }
}
