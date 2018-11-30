using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;
using ToolGood.Infrastructure;
using ToolGood.ReadyGo3;

namespace ToolGood.Application
{
    public interface IDatabaseInfoApplication
    {
        /// <summary>
        /// 获取页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        OperateResult<Page<DbDatabaseInfo>> GetPageInfos(int page, int pageSize);

        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperateResult<DbDatabaseInfo> GetInfo(int id);

        /// <summary>
        /// 添加数据库信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        OperateResult AddInfo(DbDatabaseInfo info);

        /// <summary>
        /// 编辑数据库信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        OperateResult EditInfo(UpDatabaseInfo info);

        /// <summary>
        /// 删除数据库信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperateResult DeleteInfo(int id, int operatorId, string operatorName);

        /// <summary>
        /// 获取数据库 ID与名字
        /// </summary>
        /// <returns></returns>
        OperateResult<List<DbDatabaseInfo>> GetNameAndIdOfDatabaseInfo();


    }
}
