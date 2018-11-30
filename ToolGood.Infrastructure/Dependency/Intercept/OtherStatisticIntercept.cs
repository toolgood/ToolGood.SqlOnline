using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public sealed class OtherStatisticIntercept : BaseMonitorIntercept
    {
        
        public OtherStatisticIntercept(TimeElapsedStatistic timeElapsedStatistic) : base(timeElapsedStatistic)
        {
        }

        
        // (get) Token: 0x0600004D RID: 77 RVA: 0x0000280C File Offset: 0x00000A0C
        public override TimeElapsedType TimeElapsedType {
            get {
                return TimeElapsedType.Other;
            }
        }
    }
}
