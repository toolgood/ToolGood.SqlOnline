using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public class CacheStatisticIntercept : BaseMonitorIntercept
    {
        
        public CacheStatisticIntercept(TimeElapsedStatistic timeElapsedStatistic) : base(timeElapsedStatistic)
        {
        }

        
        // (get) Token: 0x06000045 RID: 69 RVA: 0x000027BC File Offset: 0x000009BC
        public override TimeElapsedType TimeElapsedType {
            get {
                return TimeElapsedType.Cache;
            }
        }
    }
}
