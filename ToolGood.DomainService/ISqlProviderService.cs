using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.TransDto;

namespace ToolGood.DomainService
{
    public interface ISqlProviderService
    {
        /// <summary>
        /// 初始化SqlProvider
        /// </summary>
        void InitSqlProvider();

        /// <summary>
        /// 获取sql名
        /// </summary>
        /// <returns></returns>
        List<string> GetSqlTypeNames();

        /// <summary>
        /// 获取服务器信息
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        List<ServerTreeDto> GetServerTrees(int gid);

        /// <summary>
        /// 获取 数据库集
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        List<DatabaseInfoDto> GetDatabaseInfos(int gid, string name);

        /// <summary>
        /// 获取 完整的表信息
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        List<TableInfoDto> GetFullTableInfos(int gid, string serverName,string databaseName);

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        List<TableInfoDto> GetTableTrees(int gid, string serverName, string databaseName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="serverName"></param>
        /// <param name="databaseName"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        RunSqlResult RunSql(int gid, string serverName, string databaseName, string sql);

    }
}
