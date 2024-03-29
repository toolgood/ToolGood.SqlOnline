﻿/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ToolGood.WebCommon.Internals
{
    public class TextLogger : LoggerBase
    {
        internal readonly static FileLoggerQueue fileLoggerQueue = new FileLoggerQueue();
        public string File_Name { get; set; }

        public TextLogger()
        {
            File_Name = "App_Data/{type}/{yyyy}{MM}{dd}_{type}.log";
        }

        public override void WriteLog(string type, string content)
        {
            var Time = DateTime.Now;
            var filePath = new StringBuilder(File_Name);
            filePath.Replace("{time}", (Time.Ticks / 1000).ToString());
            filePath.Replace("{yyyy}", Time.Year.ToString());
            filePath.Replace("{yy}", (Time.Year % 100).ToString("D2"));
            filePath.Replace("{MM}", Time.Month.ToString("D2"));
            filePath.Replace("{dd}", Time.Day.ToString("D2"));
            filePath.Replace("{HH}", Time.Hour.ToString("D2"));
            filePath.Replace("{hh}", Time.Hour.ToString("D2"));
            filePath.Replace("{mm}", Time.Minute.ToString("D2"));
            filePath.Replace("{ss}", Time.Second.ToString("D2"));
            filePath.Replace("{type}", type);

            var file = MyHostingEnvironment.MapPath(filePath.ToString());
            file = Path.GetFullPath(file);
            Directory.CreateDirectory(Path.GetDirectoryName(file));
            fileLoggerQueue.EnqueueMessage(file, content);
        }



        internal class FileLoggerQueue
        {
            private readonly ConcurrentQueue<Tuple<string, string>> _writeQueue = new ConcurrentQueue<Tuple<string, string>>();
            private int _isInProcessMessage = 0;
            private const int _maxWriteCount = 1000;

            public void EnqueueMessage(string filePath, string info)
            {
                _writeQueue.Enqueue(Tuple.Create(filePath, info));
                ProcessMessage();
                if (_writeQueue.Count >= _maxWriteCount) Thread.Sleep(1);
            }

            private void ProcessMessage()
            {
                bool flag = Interlocked.CompareExchange(ref _isInProcessMessage, 1, 0) == 0;
                if (flag == false) return;

                Task.Factory.StartNew(() => {
                    try {
                        if (_writeQueue.IsEmpty) return;
                        Dictionary<string, FileStream> dict = new Dictionary<string, FileStream>();

                        while (_writeQueue.TryDequeue(out Tuple<string, string> info)) {
                            if (dict.TryGetValue(info.Item1, out FileStream fs) == false) {
                                var filePath = GetFullPath(info.Item1, DateTime.Now);
                                if (File.Exists(filePath) == false) {
                                    File.Create(filePath).Close();
                                    if (Environment.OSVersion.Platform != PlatformID.Win32NT) {
                                        executeLinuxCmd("chmod", "777 " + filePath);
                                    }
                                }
                                fs = File.OpenWrite(filePath);
                                fs.Seek(0, SeekOrigin.End);
                                dict[info.Item1] = fs;
                            }
                            var bytes = Encoding.UTF8.GetBytes(info.Item2);
                            fs.Write(bytes, 0, bytes.Length);
                        }
                        foreach (var item in dict.Values) {
                            if (item != null) {
                                item.Close();
                            }
                        }
                    } finally {
                        Interlocked.Exchange(ref _isInProcessMessage, 0);
                        if (!_writeQueue.IsEmpty) ProcessMessage();
                    }
                });
            }

            private static void executeLinuxCmd(string cmd, string args)
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(cmd, args) {
                    RedirectStandardOutput = false,
                    RedirectStandardInput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                };
                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit();

            }

            private string GetFullPath(string filePath, DateTime dateTime)
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
                    filePath = file.ToString();
                }
                return Path.GetFullPath(filePath);
            }


        }


    }
}
