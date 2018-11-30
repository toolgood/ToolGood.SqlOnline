using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace ToolGood.Infrastructure
{
    public class Logger
    {
        /// <summary>
        /// 日志等级，0.不输出日志；1.只输出错误信息;2.输出错误和警告信息，3.输出错误、警告和正常信息; 4.输出错误信息、警告、正常信息和调试信息
        /// </summary>
        public static int Log_Level { get; set; }
        public static string Log_Folder { get; set; }
        public static string File_Name { get; set; }
        public static string Log_Prefix { get; set; }
        private static object _lockObj = new object();

        static Logger()
        {
            Log_Level = 5;

            Log_Folder = Path.GetFullPath("logs");

            //if (HostingEnvironment.IsHosted) {
            //    Log_Folder = HostingEnvironment.MapPath("/App_Data/logs");
            //} else {
            //    Log_Folder = Path.GetFullPath("logs");
            //}
            if (Directory.Exists(Log_Folder) == false) {
                Directory.CreateDirectory(Log_Folder);
            }
            File_Name = "{type}_{yyyy}{MM}{dd}.log";
            Log_Prefix = "{yyyy}-{MM}-{dd} {HH}:{mm}:{ss} [{type}] {class}.{method} >>> \r\n";
        }


        public static void Debug(string content)
        {
            if (Log_Level >= 4) {
                WriteLog("DEBUG", content);
            }
        }
        public static void Debug(string type, string content)
        {
            if (Log_Level >= 4) {
                WriteLog("DEBUG-" + type, content);
            }
        }

        public static void Info(string content)
        {
            if (Log_Level >= 3) {
                WriteLog("INFO", content);
            }
        }
        public static void Info(string type, string content)
        {
            if (Log_Level >= 3) {
                WriteLog("INFO-" + type, content);
            }
        }

        public static void Warn(string content)
        {
            if (Log_Level >= 2) {
                WriteLog("WARN", content);
            }
        }
        public static void Warn(string type, string content)
        {
            if (Log_Level >= 2) {
                WriteLog("WARN-" + type, content);
            }
        }

        public static void Error(string content)
        {
            if (Log_Level >= 1) {
                WriteLog("ERROR", content);
            }
        }
        public static void Error(string type, string content)
        {
            if (Log_Level >= 1) {
                WriteLog("ERROR-" + type, content);
            }
        }
        public static void Error(Exception exp)
        {
            if (Log_Level >= 1) {
                var msg = string.Format("[{0}:{1}]\r\n{2}", exp.GetType().ToString(), exp.Message, exp.StackTrace);
                WriteLog("ERROR", msg);
            }
        }
        public static void Error(string type, Exception exp)
        {
            if (Log_Level >= 1) {
                var msg = string.Format("[{0}:{1}]\r\n{2}", exp.GetType().ToString(), exp.Message, exp.StackTrace);
                WriteLog("ERROR-" + type, msg);
            }
        }




        private static void WriteLog(string type, string content)
        {
            var Time = DateTime.Now;
            var filePath = Path.Combine(Log_Folder, GetFullPath(File_Name, type, Time));
            var log = new StringBuilder(Log_Prefix);
            var mi = GetMethodInfo();
            log.Replace("{time}", (Time.Ticks / 1000).ToString());
            log.Replace("{yyyy}", Time.Year.ToString());
            log.Replace("{yy}", (Time.Year % 100).ToString("D2"));
            log.Replace("{MM}", Time.Month.ToString("D2"));
            log.Replace("{dd}", Time.Day.ToString("D2"));
            log.Replace("{HH}", Time.Hour.ToString("D2"));
            log.Replace("{hh}", Time.Hour.ToString("D2"));
            log.Replace("{mm}", Time.Minute.ToString("D2"));
            log.Replace("{ss}", Time.Second.ToString("D2"));
            log.Replace("{type}", type);
            log.Replace("{class}", mi.DeclaringType.FullName);
            log.Replace("{method}", mi.Name);
            log.Replace("{threadId}", Thread.CurrentThread.ManagedThreadId.ToString());
            log.Append(content);
            log.Append("\r\n\r\n");

            lock (_lockObj) {
                try {
                    StreamWriter mySw = File.AppendText(filePath);
                    mySw.WriteLine(log.ToString());
                    mySw.Close();
                } catch { }
            }
        }



        private static string GetFullPath(string filePath, string type, DateTime dateTime)
        {
            if (filePath.Contains("{")) {
                var invalidPattern = new Regex(@"[\*\?\042\<\>\|]", RegexOptions.Compiled);
                filePath = invalidPattern.Replace(filePath, "");
                StringBuilder file = new StringBuilder(filePath);

                file.Replace("{time}", (dateTime.Ticks / 1000).ToString());
                file.Replace("{yyyy}", dateTime.Year.ToString());
                file.Replace("{yy}", (dateTime.Year % 100).ToString("D2"));
                file.Replace("{MM}", dateTime.Month.ToString("D2"));
                file.Replace("{dd}", dateTime.Day.ToString("D2"));
                file.Replace("{HH}", dateTime.Hour.ToString("D2"));
                file.Replace("{hh}", dateTime.Hour.ToString("D2"));
                file.Replace("{mm}", dateTime.Minute.ToString("D2"));
                file.Replace("{ss}", dateTime.Second.ToString("D2"));
                file.Replace("{type}", type);
                filePath = file.ToString();
            }
            return filePath;
        }

        private static MethodInfo GetMethodInfo()
        {
            var st = new StackTrace();
            int index = 1;
            var mi = ((MethodInfo)st.GetFrame(index++).GetMethod());
            var classType = mi.DeclaringType;
            while (classType == typeof(Logger)) {
                mi = ((MethodInfo)st.GetFrame(index++).GetMethod());
                classType = mi.DeclaringType;
            }
            return mi;

        }


        /// <summary>
        /// 获取错误异常信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="memberName">出现异常的方法名字</param>
        /// <returns>错误异常信息</returns>
        private static string ToErrMsg(Exception ex, string memberName = null)
        {
            StringBuilder errorBuilder = new StringBuilder();
            bool flag = !string.IsNullOrWhiteSpace(memberName);
            if (flag) {
                errorBuilder.AppendFormat("CallerMemberName：{0}", memberName).AppendLine();
            }
            errorBuilder.AppendFormat("Message：{0}", ex.Message).AppendLine();
            bool flag2 = ex.InnerException != null;
            if (flag2) {
                bool flag3 = !string.Equals(ex.Message, ex.InnerException.Message, StringComparison.OrdinalIgnoreCase);
                if (flag3) {
                    errorBuilder.AppendFormat("InnerException：{0}", ex.InnerException.Message).AppendLine();
                }
            }
            errorBuilder.AppendFormat("Source：{0}", ex.Source).AppendLine();
            errorBuilder.AppendFormat("StackTrace：{0}", ex.StackTrace).AppendLine();
            //bool flag4 = WebUtil.IsHaveHttpContext();
            //if (flag4) {
            //    errorBuilder.AppendFormat("RealIP：{0}", WebUtil.GetRealIP()).AppendLine();
            //    errorBuilder.AppendFormat("HttpRequestUrl：{0}", WebUtil.GetHttpRequestUrl()).AppendLine();
            //    errorBuilder.AppendFormat("UserAgent：{0}", WebUtil.GetUserAgent()).AppendLine();
            //}
            return errorBuilder.ToString();
        }

    }

}
