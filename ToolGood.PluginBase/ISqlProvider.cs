using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ToolGood.PluginBase
{
    public interface ISqlProvider
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        string GetServerName();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DbProviderFactory GetProviderFactory();

        /// <summary>
        /// 获取服务器信息
        /// </summary>
        /// <returns></returns>
        ServerTree GetServerInfo(string name, string connStr);

        /// <summary>
        /// 获取数据库名称列表
        /// </summary>
        /// <param name="connStr"></param>
        /// <returns></returns>
        List<DatabaseTreeNode> GetDatabases(string connStr);

        /// <summary>
        /// 获取完整表列表
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        List<SqlTableInfo> GetFullTableInfos(string connStr, string databaseName);

        /// <summary>
        /// 获取表列表
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        List<SqlTableInfo> GetTableInfos(string connStr, string databaseName);


        /// <summary>
        /// 获取列集
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        List<SqlColumnInfo> GetColumnInfos(string connStr, string databaseName, string tableName);

    }
}
