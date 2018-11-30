using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;

namespace ToolGood.Repository.Impl
{
    public partial class DatabaseInfoRepository
    {
        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<DbDatabaseInfo> GetDatabaseInfos(int groupId)
        {
            const string sql = @"select * from DatabaseInfo t 
inner join AdminDatabasePass as t1 on t1.DatabaseInfoId=t.Id 
where t.IsDelete=0 and t1.AdminGroupId=@0";
            return CreateSqlHelper().Select<DbDatabaseInfo>(sql, groupId);
        }

        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="serverName"></param>
        /// <returns></returns>
        public DbDatabaseInfo GetDatabaseInfo(int groupId, string serverName)
        {
            const string sql = @"select * from DatabaseInfo t 
inner join AdminDatabasePass as t1 on t1.DatabaseInfoId=t.Id 
where t.IsDelete=0 and t1.AdminGroupId=@0 and t.Name=@1";
            return CreateSqlHelper().FirstOrDefault<DbDatabaseInfo>(sql, groupId, serverName);
        }



    }

}
