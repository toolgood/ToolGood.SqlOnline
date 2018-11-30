using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;
using ToolGood.Infrastructure;
using ToolGood.ReadyGo3;
using ToolGood.Repository;

namespace ToolGood.Application.Impl
{
    public class DatabaseInfoApplication : IDatabaseInfoApplication
    {
        private readonly IDatabaseInfoHistoryRepository _databaseInfoHistoryRepository;
        private readonly IDatabaseInfoRepository _databaseInfoRepository;

        public DatabaseInfoApplication(IDatabaseInfoHistoryRepository databaseInfoHistoryRepository, IDatabaseInfoRepository databaseInfoRepository)
        {
            _databaseInfoHistoryRepository = databaseInfoHistoryRepository;
            _databaseInfoRepository = databaseInfoRepository;
        }

        /// <summary>
        /// 获取页面
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public OperateResult<Page<DbDatabaseInfo>> GetPageInfos(int page, int pageSize)
        {
            return OperateResult.Execute(() => {
                return _databaseInfoRepository.Where(q => q.IsDelete == false)
                    .OrderBy(q => q.Sort, ReadyGo3.OrderType.Desc)
                        .Page(page, pageSize);
            }, "DatabaseInfoApplication-GegPageList");

        }

        /// <summary>
        /// 获取数据库信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OperateResult<DbDatabaseInfo> GetInfo(int id)
        {
            return OperateResult.Execute(() => {
                return _databaseInfoRepository.Where(q => q.IsDelete == false)
                            .Where(q => q.Id == id).FirstOrDefault();
            }, "DatabaseInfoApplication-GetInfo");

        }

        /// <summary>
        /// 添加数据库信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public OperateResult AddInfo(DbDatabaseInfo info)
        {
            return OperateResult.Execute(() => {
                _databaseInfoRepository.Insert(info);
                var hist = new DbDatabaseInfoHistory(info);
                _databaseInfoHistoryRepository.Insert(hist);
            }, "DatabaseInfoApplication-AddInfo");
        }

        /// <summary>
        /// 编辑数据库信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public OperateResult EditInfo(UpDatabaseInfo info)
        {
            return OperateResult.Execute(() => {
                _databaseInfoRepository.Update(info, new UpDatabaseInfo { Id = info.Id });
                var db = _databaseInfoRepository.Where(q=>q.IsDelete==false).Where(q => q.Id == info.Id).FirstOrDefault();
                if (db != null) {
                    var hist = new DbDatabaseInfoHistory(db);
                    _databaseInfoHistoryRepository.Insert(hist);
                }
            }, "DatabaseInfoApplication-EditInfo");
        }

        /// <summary>
        /// 删除数据库信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OperateResult DeleteInfo(int id, int operatorId, string operatorName)
        {
            return OperateResult.Execute(() => {
                _databaseInfoRepository.Update(
                    new UpDatabaseInfo { IsDelete = true, ChangeAdminId = operatorId, ChangeAdminName = operatorName }
                    , new UpDatabaseInfo { Id = id });

            }, "DatabaseInfoApplication-DeleteInfo");
        }


        /// <summary>
        /// 获取数据库 ID与名字
        /// </summary>
        /// <returns></returns>
        public OperateResult<List<DbDatabaseInfo>> GetNameAndIdOfDatabaseInfo()
        {
            return OperateResult.Execute(() => {
              return  _databaseInfoRepository.Where(q => q.IsDelete)
                    .OrderBy(q => q.Sort, ReadyGo3.OrderType.Asc)
                    .Select("Id,Name");
            }, "DatabaseInfoApplication-GetNameAndIdOfDatabaseInfo");
        }


    }
}
