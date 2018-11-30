using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public class SqlStatisticIntercept : BaseMonitorIntercept
    {
        
        public SqlStatisticIntercept(TimeElapsedStatistic timeElapsedStatistic) : base(timeElapsedStatistic)
        {
        }

        
        // (get) Token: 0x06000051 RID: 81 RVA: 0x00002834 File Offset: 0x00000A34
        public override TimeElapsedType TimeElapsedType {
            get {
                return TimeElapsedType.Sql;
            }
        }
    }
}
