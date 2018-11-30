using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public class TimeElapsedInfo
    {
        /// <summary>
        /// 消耗类型
        /// </summary>
        
        // (get) Token: 0x06000052 RID: 82 RVA: 0x00002847 File Offset: 0x00000A47
        // (set) Token: 0x06000053 RID: 83 RVA: 0x0000284F File Offset: 0x00000A4F
        public TimeElapsedType TimeElapsedType { get; set; }

        /// <summary>
        /// 调用方法
        /// </summary>
        
        // (get) Token: 0x06000054 RID: 84 RVA: 0x00002858 File Offset: 0x00000A58
        // (set) Token: 0x06000055 RID: 85 RVA: 0x00002860 File Offset: 0x00000A60
        public string CallMemberName { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        
        // (get) Token: 0x06000056 RID: 86 RVA: 0x00002869 File Offset: 0x00000A69
        // (set) Token: 0x06000057 RID: 87 RVA: 0x00002871 File Offset: 0x00000A71
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        
        // (get) Token: 0x06000058 RID: 88 RVA: 0x0000287A File Offset: 0x00000A7A
        // (set) Token: 0x06000059 RID: 89 RVA: 0x00002882 File Offset: 0x00000A82
        public string Remark { get; set; }

        /// <summary>
        /// 消耗时间
        /// </summary>
        
        // (get) Token: 0x0600005A RID: 90 RVA: 0x0000288B File Offset: 0x00000A8B
        // (set) Token: 0x0600005B RID: 91 RVA: 0x00002893 File Offset: 0x00000A93
        public double TimeElapsed { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        
        // (get) Token: 0x0600005C RID: 92 RVA: 0x0000289C File Offset: 0x00000A9C
        // (set) Token: 0x0600005D RID: 93 RVA: 0x000028A4 File Offset: 0x00000AA4
        public DateTime ExecuteTime { get; set; }
    }
}
