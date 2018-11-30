using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.Infrastructure.Dependency.Intercept
{
    public class TimeElapsedStatistic
    {
        /// <summary>
        /// 时间消耗类型
        /// </summary>
        
        // (get) Token: 0x0600005F RID: 95 RVA: 0x000028B0 File Offset: 0x00000AB0
        public IEnumerable<TimeElapsedInfo> TimeElapsedList {
            get {
                return this._timeConsumerQueue.ToArray();
            }
        }

        /// <summary>
        /// 添加消耗时间信息
        /// </summary>
        /// <param name="timeElapsedInfo">消耗时间信息</param>
        
        public void AddTimeElapsedInfo(TimeElapsedInfo timeElapsedInfo)
        {
            bool flag = timeElapsedInfo != null;
            if (flag) {
                this._timeConsumerQueue.Enqueue(timeElapsedInfo);
            }
        }

        /// <summary>
        /// 添加消耗信息
        /// </summary>
        /// <param name="executeAction">执行的方法</param>
        /// <param name="timeElapsedType">消耗类型</param>
        /// <param name="memberName">调用的方法名字</param>
        
        public void AddTimeElapsedInfo(Action executeAction, TimeElapsedType timeElapsedType, string memberName = null)
        {
            TimeElapsedInfo timeElapsedInfo = new TimeElapsedInfo();
            DateTime startTime = DateTime.Now;
            timeElapsedInfo.ExecuteTime = startTime;
            try {
                executeAction();
                timeElapsedInfo.IsSuccess = true;
            } catch (Exception ex) {
                timeElapsedInfo.IsSuccess = false;
                timeElapsedInfo.Remark = ex.Message;
                throw;
            } finally {
                timeElapsedInfo.TimeElapsed = (DateTime.Now - startTime).TotalMilliseconds;
                timeElapsedInfo.CallMemberName = memberName;
                timeElapsedInfo.TimeElapsedType = timeElapsedType;
                this.AddTimeElapsedInfo(timeElapsedInfo);
            }
        }

        
        private ConcurrentQueue<TimeElapsedInfo> _timeConsumerQueue = new ConcurrentQueue<TimeElapsedInfo>();
    }
}
