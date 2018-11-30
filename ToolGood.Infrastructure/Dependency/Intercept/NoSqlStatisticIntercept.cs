using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public class NoSqlStatisticIntercept : BaseMonitorIntercept
    {
        
        public NoSqlStatisticIntercept(TimeElapsedStatistic timeElapsedStatistic) : base(timeElapsedStatistic)
        {
        }

        
        // (get) Token: 0x0600004B RID: 75 RVA: 0x000027F8 File Offset: 0x000009F8
        public override TimeElapsedType TimeElapsedType {
            get {
                return TimeElapsedType.NoSql;
            }
        }
    }
}
