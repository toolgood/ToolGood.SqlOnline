using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public class MQStatisticIntercept : BaseMonitorIntercept
    {
        
        public MQStatisticIntercept(TimeElapsedStatistic timeElapsedStatistic) : base(timeElapsedStatistic)
        {
        }

        
        // (get) Token: 0x06000049 RID: 73 RVA: 0x000027E4 File Offset: 0x000009E4
        public override TimeElapsedType TimeElapsedType {
            get {
                return TimeElapsedType.MQ;
            }
        }
    }
}
