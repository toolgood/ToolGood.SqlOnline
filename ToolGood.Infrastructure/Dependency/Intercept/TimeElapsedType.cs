using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public enum TimeElapsedType
    {
        /// <summary>
        /// NoSql
        /// </summary>
        
        [Description("NoSql")]
        NoSql = 1,
        /// <summary>
        /// Cache
        /// </summary>
        
        [Description("Cache")]
        Cache,
        /// <summary>
        /// Sql
        /// </summary>
        
        [Description("Sql")]
        Sql,
        /// <summary>
        /// MQ
        /// </summary>
        
        [Description("MQ")]
        MQ,
        /// <summary>
        /// HTTP
        /// </summary>
        
        [Description("HTTP")]
        HTTP,
        /// <summary>
        /// RPC
        /// </summary>
        
        [Description("RPC")]
        RPC,
        /// <summary>
        /// 其它
        /// </summary>
        
        [Description("其它")]
        Other
    }
}
