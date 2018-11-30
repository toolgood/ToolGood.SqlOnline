using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolGood.ReadyGo3;
using ToolGood.ReadyGo3.Attributes;
using ToolGood.ReadyGo3.DataCentxt;
using ToolGood.Datas;
using ToolGood.Infrastructure;

namespace ToolGood.Repository
{
    public partial interface IAdminRepository : IRepositoryBase<DbAdmin, UpAdmin>, IRepositoryBase<DbAdmin>
    {
    }
    public partial interface IAdminDatabasePassRepository : IRepositoryBase<DbAdminDatabasePass, UpAdminDatabasePass>, IRepositoryBase<DbAdminDatabasePass>
    {
    }
    public partial interface IAdminGroupRepository : IRepositoryBase<DbAdminGroup, UpAdminGroup>, IRepositoryBase<DbAdminGroup>
    {
    }
    public partial interface IAdminLoginLogRepository : IRepositoryBase<DbAdminLoginLog, UpAdminLoginLog>, IRepositoryBase<DbAdminLoginLog>
    {
    }
    public partial interface IAdminMenuRepository : IRepositoryBase<DbAdminMenu, UpAdminMenu>, IRepositoryBase<DbAdminMenu>
    {
    }
    public partial interface IAdminMenuPassRepository : IRepositoryBase<DbAdminMenuPass, UpAdminMenuPass>, IRepositoryBase<DbAdminMenuPass>
    {
    }
    public partial interface IDatabaseInfoRepository : IRepositoryBase<DbDatabaseInfo, UpDatabaseInfo>, IRepositoryBase<DbDatabaseInfo>
    {
    }
    public partial interface IDatabaseInfoHistoryRepository : IRepositoryBase<DbDatabaseInfoHistory, UpDatabaseInfoHistory>, IRepositoryBase<DbDatabaseInfoHistory>
    {
    }
    public partial interface ISettingRepository : IRepositoryBase<DbSetting, UpSetting>, IRepositoryBase<DbSetting>
    {
    }
    public partial interface ISqlQueryLogRepository : IRepositoryBase<DbSqlQueryLog, UpSqlQueryLog>, IRepositoryBase<DbSqlQueryLog>
    {
    }
}
namespace ToolGood.Repository.Impl
{

    public partial class AdminRepository : RepositoryBase<ToolGood.Datas.DbAdmin, UpAdmin>, IAdminRepository
    {
    }

    public partial class AdminDatabasePassRepository : RepositoryBase<ToolGood.Datas.DbAdminDatabasePass, UpAdminDatabasePass>, IAdminDatabasePassRepository
    {
    }

    public partial class AdminGroupRepository : RepositoryBase<ToolGood.Datas.DbAdminGroup, UpAdminGroup>, IAdminGroupRepository
    {
    }

    public partial class AdminLoginLogRepository : RepositoryBase<ToolGood.Datas.DbAdminLoginLog, UpAdminLoginLog>, IAdminLoginLogRepository
    {
    }

    public partial class AdminMenuRepository : RepositoryBase<ToolGood.Datas.DbAdminMenu, UpAdminMenu>, IAdminMenuRepository
    {
    }

    public partial class AdminMenuPassRepository : RepositoryBase<ToolGood.Datas.DbAdminMenuPass, UpAdminMenuPass>, IAdminMenuPassRepository
    {
    }

    public partial class DatabaseInfoRepository : RepositoryBase<ToolGood.Datas.DbDatabaseInfo, UpDatabaseInfo>, IDatabaseInfoRepository
    {
    }

    public partial class DatabaseInfoHistoryRepository : RepositoryBase<ToolGood.Datas.DbDatabaseInfoHistory, UpDatabaseInfoHistory>, IDatabaseInfoHistoryRepository
    {
    }

    public partial class SettingRepository : RepositoryBase<ToolGood.Datas.DbSetting, UpSetting>, ISettingRepository
    {
    }

    public partial class SqlQueryLogRepository : RepositoryBase<ToolGood.Datas.DbSqlQueryLog, UpSqlQueryLog>, ISqlQueryLogRepository
    {
    }
}




