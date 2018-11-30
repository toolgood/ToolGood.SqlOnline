using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public class HttpStatisticIntercept : BaseMonitorIntercept
    {
        
        public HttpStatisticIntercept(TimeElapsedStatistic timeElapsedStatistic) : base(timeElapsedStatistic)
        {
        }

        
        // (get) Token: 0x06000047 RID: 71 RVA: 0x000027D0 File Offset: 0x000009D0
        public override TimeElapsedType TimeElapsedType {
            get {
                return TimeElapsedType.HTTP;
            }
        }
    }
}
