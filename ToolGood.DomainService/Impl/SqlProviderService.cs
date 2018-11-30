using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using ToolGood.PluginBase;
using ToolGood.TransDto;
using ToolGood.Repository;
using System.Linq;
using System.Data;
using System.Data.Common;

namespace ToolGood.DomainService.Impl
{
    public class SqlProviderService : ISqlProviderService
    {
        private static bool _initialize = false;
        private static Dictionary<string, ISqlProvider> _sqlProviders = new Dictionary<string, ISqlProvider>();
        private readonly IAdminDatabasePassRepository _adminDatabasePassRepository;
        private readonly IDatabaseInfoRepository _databaseInfoRepository;

        public SqlProviderService(IAdminDatabasePassRepository adminDatabasePassRepository, IDatabaseInfoRepository databaseInfoRepository)
        {
            _adminDatabasePassRepository = adminDatabasePassRepository;
            _databaseInfoRepository = databaseInfoRepository;
        }

        /// <summary>
        /// 初始化SqlProvider
        /// </summary>
        public void InitSqlProvider()
        {
            if (_initialize == false) {
                var sqlProvider = new Dictionary<string, ISqlProvider>();
                var path = Path.GetDirectoryName(typeof(SqlProviderService).Assembly.Location);
                var files = Directory.GetFiles(path, "ToolGood.Plugin.*.dll");

                foreach (var file in files) {
                    var ass = Assembly.LoadFrom(file);
                    var types = ass.GetTypes();
                    foreach (var type in types) {
                        var interfaces = type.GetInterfaces();
                        foreach (var intf in interfaces) {
                            if (intf == typeof(ISqlProvider)) {
                                var provider = Activator.CreateInstance(type) as ISqlProvider;
                                sqlProvider[provider.GetServerName()] = provider;
                            }
                        }
                    }
                }
                _sqlProviders = sqlProvider;
                _initialize = true;
            }
        }

        /// <summary>
        /// 获取sql名
        /// </summary>
        /// <returns></returns>
        public List<string> GetSqlTypeNames()
        {
            InitSqlProvider();
            return _sqlProviders.Keys.ToList();
        }


        /// <summary>
        /// 获取服务器信息
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public List<ServerTreeDto> GetServerTrees(int gid)
        {
            InitSqlProvider();
            var dbs = _databaseInfoRepository.GetDatabaseInfos(gid);
            List<ServerTreeDto> list = new List<ServerTreeDto>();
            foreach (var db in dbs) {
                var provider = _sqlProviders[db.SqlServerType];
                var dto = provider.GetServerInfo(db.Name, db.ReadConnectionString);
                list.Add(new ServerTreeDto(dto));
            }
            return list;
        }

        /// <summary>
        /// 获取 数据库集
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public List<DatabaseInfoDto> GetDatabaseInfos(int gid, string serverName)
        {
            InitSqlProvider();

            List<DatabaseInfoDto> list = new List<DatabaseInfoDto>();

            var db = _databaseInfoRepository.GetDatabaseInfo(gid, serverName);
            if (db != null) {
                var provider = _sqlProviders[db.SqlServerType];

                var databases = provider.GetDatabases(db.ReadConnectionString);
                foreach (var item in databases) {
                    DatabaseInfoDto dto = new DatabaseInfoDto() {
                        DatabaseName = item.Name,
                        ServerName = serverName
                    };
                    list.Add(dto);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取 完整的表信息
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public List<TableInfoDto> GetFullTableInfos(int gid, string serverName, string databaseName)
        {
            InitSqlProvider();

            List<TableInfoDto> list = new List<TableInfoDto>();
            var db = _databaseInfoRepository.GetDatabaseInfo(gid, serverName);
            if (db != null) {
                var provider = _sqlProviders[db.Name];
                var tableInfos = provider.GetFullTableInfos(db.ConnectionString, databaseName);

                foreach (var item in tableInfos) {
                    TableInfoDto dto = new TableInfoDto(item);
                    list.Add(dto);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取基础名
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public List<DatabaseTreeDto> GetDatabaseTrees(int gid, string serverName)
        {
            InitSqlProvider();

            var dbs = _databaseInfoRepository.GetDatabaseInfos(gid);
            List<DatabaseTreeDto> list = new List<DatabaseTreeDto>();



            return list;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public List<TableInfoDto> GetTableTrees(int gid, string serverName, string databaseName)
        {
            InitSqlProvider();
            var db = _databaseInfoRepository.GetDatabaseInfo(gid, serverName);
            if (db == null) {
                return new List<TableInfoDto>();
            }
            List<TableInfoDto> list = new List<TableInfoDto>();
            if (_sqlProviders.TryGetValue(db.SqlServerType, out ISqlProvider provider)) {
                var tableInfos = provider.GetTableInfos(db.ReadConnectionString, databaseName);
                foreach (var table in tableInfos) {
                    list.Add(new TableInfoDto(table));
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public RunSqlResult RunSql(int gid, string serverName, string databaseName, string sql)
        {
            RunSqlResult result = new RunSqlResult();
            if (string.IsNullOrWhiteSpace(sql)) {
                result.StartTime = DateTime.Now;
                result.Exception = "请输入SQL语句！";
                return result;
            }

            result.Sql = sql;
            var db = _databaseInfoRepository.GetDatabaseInfo(gid, serverName);
            if (db == null) {
                result.StartTime = DateTime.Now;
                result.Exception = "权限出错！";
                return result;
            }
            if (_sqlProviders.TryGetValue(db.SqlServerType, out ISqlProvider provider)) {
                try {
                    #region OpenConnection
                    var factory = provider.GetProviderFactory();
                    var conn = factory.CreateConnection();
                    conn.ConnectionString = db.ConnectionString;
                    conn.Open();
                    conn.ChangeDatabase(databaseName);
                    #endregion
                    result.StartTime = DateTime.Now;
                    System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
                    try {
                        using (DbCommand cmd = conn.CreateCommand()) {
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = sql;

                            List<ISqlResult> sqlResults = new List<ISqlResult>();
                            var reader = cmd.ExecuteReader();
                            #region 读取更改数据
                            if (reader.RecordsAffected > -1) {
                                sqlResults.Add(new SqlCountResult() { Count = reader.RecordsAffected });
                            }
                            #endregion
                            #region 读取数据
                            do {
                                int fieldCount = reader.FieldCount;
                                if (fieldCount > 0) {
                                    SqlDataTableResult sr = new SqlDataTableResult();
                                    for (int i = 0; i < fieldCount; i++) {
                                        sr.Columns.Add(reader.GetName(i));
                                    }
                                    object[] vals = null;
                                    while (reader.Read()) {
                                        vals = new object[fieldCount];
                                        reader.GetValues(vals);
                                        sr.Values.Add(vals);
                                    }
                                    sqlResults.Add(sr);
                                }
                            } while (reader.NextResult());
                            #endregion
                            result.SqlResults = sqlResults;
                            result.Success = true;
                        }
                    } finally {
                        stopwatch.Stop();
                        result.EndTime = DateTime.Now;
                        result.RunTime = stopwatch.ElapsedMilliseconds;
                        conn.Close();
                    }
                } catch (Exception x) {
                    result.Exception = x.Message;
                }
            }
            return result;
        }



    }
}
