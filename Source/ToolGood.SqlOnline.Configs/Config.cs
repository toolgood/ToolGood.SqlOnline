/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Diagnostics;
using System.IO;
using ToolGood.ReadyGo3;

namespace ToolGood.SqlOnline.Configs
{
    public class Config
    {
        [ThreadStatic]
        private static SqlHelper _dbHelper;


        public static SqlHelper WriteHelper {
            get {
                if (_dbHelper == null || _dbHelper._IsDisposed) {
                    var path = Path.GetFullPath("App_Data/setting.sav");

                    _dbHelper = SqlHelperFactory.OpenSqliteFile(path);
                }
                return _dbHelper;
            }
        }


        [ThreadStatic]
        private static SqlHelper _readHelper;
        public static SqlHelper ReadHelper {
            get {
                if (_readHelper == null || _readHelper._IsDisposed) {
                    var path2 = Path.GetFullPath("App_Data/setting.sav");
                    var path = $"Data Source={path2};Mode=ReadOnly;";
                    _readHelper = SqlHelperFactory.OpenDatabase(path, SqlType.SQLite);
                }
                return _readHelper;
            }
        }

        [ThreadStatic]
        public static SqlHelper _logHelper;
        public static SqlHelper GetLogSqlHelper(DateTime date)
        {
            if (_logHelper == null || _logHelper._IsDisposed) {
                var path =Path.GetFullPath( $"App_Data/SqlQueryLog/{date.ToString("yyyyMM")}.sav");
                if (File.Exists(path) == false) {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    File.Create(path).Close();
                    if (Environment.OSVersion.Platform != PlatformID.Win32NT) {
                        executeLinuxCmd("chmod", "777 " + path);
                    }

                    var helper = SqlHelperFactory.OpenSqliteFile(path);
                    helper.Execute(@"CREATE TABLE [SqlQueryLog](
    [Id] INTEGER NOT NULL PRIMARY KEY AutoIncrement,
    [AdminId] INTEGER NOT NULL,
    [AdminName] Text NOT NULL,
    [AdminTrueName] Text NULL,
    [AdminJobNo] Text NULL,
    [AdminPhone] Text NULL,
    [SqlConnectionId] INTEGER NOT NULL,
    [SqlConnectionName] Text NOT NULL,
    [DatabaseName] Text NULL,
    [Sql] Text NOT NULL,
    [RunReadResult] int NOT NULL,
    [Exception] Text NULL,
    [RunTime] INTEGER NOT NULL,
    [AddingTime] dateTime NOT NULL
);
CREATE TABLE IF NOT EXISTS [SqlExecuteLog](
    [Id] INTEGER NOT NULL PRIMARY KEY AutoIncrement,
    [AdminId] INTEGER NOT NULL,
    [AdminName] Text NULL,
    [AdminTrueName] Text NULL,
    [AdminJobNo] Text NULL,
    [AdminPhone] Text NULL,
    [SqlConnectionId] INTEGER NOT NULL,
    [SqlConnectionName] Text NULL,
    [DatabaseName] Text NULL,
    [Sql] Text NULL,
    [Exception] Text NULL,
    [RunReadResult] int NOT NULL,
    [RunTime] INTEGER NOT NULL,
    [AddingTime] dateTime NOT NULL
);
CREATE TABLE IF NOT EXISTS [SqlExecuteLogItem](
    [Id] INTEGER NOT NULL PRIMARY KEY AutoIncrement,
    [SqlExecuteLogId] INTEGER NOT NULL,
    [Sql] Text NULL,
    [OldData] Text NULL
);
");
                    helper.Dispose();
                }
                _logHelper = SqlHelperFactory.OpenSqliteFile(path);
            }
            return _logHelper;
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

        public static void Dispose()
        {
            if (_dbHelper != null) {
                if (_dbHelper._IsDisposed == false) {
                    _dbHelper.Dispose();
                }
                _dbHelper = null;
            }
            if (_readHelper != null) {
                if (_readHelper._IsDisposed == false) {
                    _readHelper.Dispose();
                }
                _readHelper = null;
            }
            if (_logHelper != null) {
                if (_logHelper._IsDisposed == false) {
                    _logHelper.Dispose();
                }
                _logHelper = null;
            }
        }

    }
}
