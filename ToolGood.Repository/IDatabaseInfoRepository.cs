using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;

namespace ToolGood.Repository
{
    public partial interface IDatabaseInfoRepository
    {
        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        List<DbDatabaseInfo> GetDatabaseInfos(int groupId);


        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        DbDatabaseInfo GetDatabaseInfo(int groupId, string serverName);
    }
}
