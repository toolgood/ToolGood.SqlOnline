using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ToolGood.ReadyGo3;
using ToolGood.ReadyGo3.Gadget.Monitor;

namespace ToolGood.Infrastructure
{
    public static class Config
    {
        private static string _rootPath;

        [ThreadStatic]
        private static SqlHelper _dbHelper;

        [ThreadStatic]
        private static SqlHelper _logHelper;

        [ThreadStatic]
        private static ISqlMonitor _monitor;

        internal static void SetMonitor(ISqlMonitor monitor)
        {
            _monitor = monitor;
            if (_dbHelper!=null) {
                _dbHelper._Config.SqlMonitor = monitor;
            }
            if (_logHelper != null) {
                _logHelper._Config.SqlMonitor = monitor;
            }
        }

        /// <summary>
        /// 管理库
        /// </summary>
        public static SqlHelper DbHelper {
            get {
                if (_dbHelper == null || _dbHelper._IsDisposed) {
                    if (_rootPath == null) {
                        _rootPath = Path.GetDirectoryName(typeof(Config).Assembly.Location);
                    }
                    var file = Path.Combine(_rootPath, "ToolGood.sav");
                    _dbHelper = SqlHelperFactory.OpenSqliteFile(file);
                    if (_monitor!=null) {
                        _dbHelper._Config.SqlMonitor = _monitor;
                    }
                }
                return _dbHelper;
            }
        }

        /// <summary>
        /// 日志
        /// </summary>
        public static SqlHelper LogHelper {
            get {
                if (_logHelper == null || _logHelper._IsDisposed) {
                    if (_rootPath == null) {
                        _rootPath = Path.GetDirectoryName(typeof(Config).Assembly.Location);
                    }
                    var file = Path.Combine(_rootPath, "ToolGood.sav");
                    _logHelper = SqlHelperFactory.OpenSqliteFile(file);
                    if (_monitor != null) {
                        _logHelper._Config.SqlMonitor = _monitor;
                    }
                }
                return _logHelper;
            }
        }



        public static void Dispose()
        {
            if (_dbHelper != null) {
                if (_dbHelper._IsDisposed == false) {
                    _dbHelper.Dispose();
                }
                _dbHelper = null;
            }
 
        }

    }
}
