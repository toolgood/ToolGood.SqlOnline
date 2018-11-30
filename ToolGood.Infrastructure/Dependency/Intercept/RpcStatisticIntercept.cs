using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public class RpcStatisticIntercept : BaseMonitorIntercept
    {
        
        public RpcStatisticIntercept(TimeElapsedStatistic timeElapsedStatistic) : base(timeElapsedStatistic)
        {
        }

        
        // (get) Token: 0x0600004F RID: 79 RVA: 0x00002820 File Offset: 0x00000A20
        public override TimeElapsedType TimeElapsedType {
            get {
                return TimeElapsedType.RPC;
            }
        }
    }
}
