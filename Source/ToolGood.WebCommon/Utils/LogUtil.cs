/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.WebCommon.Internals;

namespace ToolGood.WebCommon.Utils
{
    /// <summary>
    /// 日志操作
    /// </summary>
    public class LogUtil
    {
        [ThreadStatic]
        public static QueryArgs QueryArgs;
        private static List<ILogger> allLoggers = new List<ILogger>();

        public static bool UseDebug { get; set; } = true;
        public static bool UseInfo { get; set; } = true;
        public static bool UseWarn { get; set; } = true;
        public static bool UseError { get; set; } = true;
        public static bool UseFatal { get; set; } = true;
        public static bool UseSql { get; set; } = true;

        static LogUtil()
        {
            allLoggers.Add(new TextLogger());
        }

        private static void WriteLog(LogType type, string msg)
        {
            var queryArgs = QueryArgs;
            if (queryArgs == null /*|| queryArgs.UseLog == null || queryArgs.UseLog == false*/) {
                if (type == LogType.Debug && UseDebug == false) return;
                if (type == LogType.Error && UseError == false) return;
                if (type == LogType.Fatal && UseFatal == false) return;
                if (type == LogType.Info && UseInfo == false) return;
                if (type == LogType.Warn && UseWarn == false) return;
            }
            //if (QueryArgs != null) {
            //    QueryArgs.Logs.Add(new DebugLog() {
            //        LogType = type.ToString(),
            //        Message = msg,
            //        StartTime = DateTime.Now
            //    });
            //}
            foreach (var logger in allLoggers) {
                logger.WriteLog(QueryArgs, type, msg);
            }
        }

        public static void Debug(string msg)
        {
            WriteLog(LogType.Debug, msg);
        }
        public static void Info(string msg)
        {
            WriteLog(LogType.Info, msg);
        }
        public static void Warn(string msg)
        {
            WriteLog(LogType.Warn, msg);
        }
        public static void Fatal(string msg)
        {
            WriteLog(LogType.Fatal, msg);
        }
        public static void Error(string msg)
        {
            WriteLog(LogType.Error, msg);
        }

        public static void Error(string msg, Exception ex)
        {
            var dotNetVersion = Environment.Version.Major + "." + Environment.Version.Minor + "." + Environment.Version.Build + "." + Environment.Version.Revision;
            var OSVersion = Environment.OSVersion.ToString();
            msg = msg + ex.ToErrMsg(null, OSVersion + " " + dotNetVersion);
            WriteLog(LogType.Error, msg);
        }
        public static void Error(Exception ex, string memberName = null)
        {
            var dotNetVersion = Environment.Version.Major + "." + Environment.Version.Minor + "." + Environment.Version.Build + "." + Environment.Version.Revision;
            var OSVersion = Environment.OSVersion.ToString();
            var msg = ex.ToErrMsg(memberName, OSVersion + " " + dotNetVersion);
            WriteLog(LogType.Error, msg);
        }

        public static void Sql(DateTime startTime, DateTime endTime, string sql, object[] args, string errorMessage)
        {
            var queryArgs = QueryArgs;
            if (queryArgs == null /*|| queryArgs.UseLog == null || queryArgs.UseLog == false*/) {
                if (UseSql == false) return;
            }
            //if (QueryArgs != null) {
            //    QueryArgs.SqlTimes.Add(new DebugSqlTime() {
            //        Sql = sql,
            //        Args = args,
            //        StartTime = startTime,
            //        EndTime = endTime,
            //        ErrorMessage = errorMessage,
            //        QueryTimes = (endTime - startTime).Milliseconds
            //    });
            //}
            StringBuilder sb = new StringBuilder();
            sb.Append(startTime.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append("[");
            sb.Append((endTime - startTime).Milliseconds);
            sb.Append("ms]");
            sb.AppendLine(sql);
            if (args != null && args.Length > 0) {
                for (int i = 0; i < args.Length; i++) {
                    var arg = args[i];
                    if (arg is System.Data.IDataParameter) {
                        var dataParam = arg as System.Data.IDataParameter;
                        sb.AppendLine($"@{dataParam.ParameterName}={dataParam.Value}");
                    } else {
                        sb.AppendLine($"@{i}={arg}");
                    }
                }
            }
            if (string.IsNullOrEmpty(errorMessage) == false) {
                sb.Append("ERROR MESSAGE:");
                sb.AppendLine(errorMessage);
            }
            sb.AppendLine();

            foreach (var logger in allLoggers) {
                logger.WriteLog("Sql", sb.ToString());
            }
        }
        public static void Request(string msg)
        {
            foreach (var logger in allLoggers) {
                logger.WriteLog(QueryArgs, LogType.Request, msg);
            }
        }
        public static void Response(string msg)
        {
            foreach (var logger in allLoggers) {
                logger.WriteLog(QueryArgs, LogType.Response, msg);
            }
        }

    }
}
