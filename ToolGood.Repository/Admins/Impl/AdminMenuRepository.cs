using System.Collections.Generic;
using ToolGood.Datas;
using ToolGood.Infrastructure;
using System.Linq;

namespace ToolGood.Repository.Impl
{
    public partial class AdminMenuRepository  
    {
        public List<DbAdminMenu> GetTopAdminMenu(int groudID)
        {
            var sql = @"Select t0.* from AdminMenu as t0
inner Join AdminMenuPass as t1 on t0.Id=t1.MenuId 
inner join AdminGroup as t2 on t1.AdminGroupId=t2.Id
where t2.IsDelete=0 and t2.Id=@0 and t0.IsDelete=0 and t0.ParentId=0 and t1.ActionName='navbar'
Order by t0.Sort ASC";
            return CreateSqlHelper().Select<DbAdminMenu>(sql, groudID);
        }

        public List<DbAdminMenu> GetAdminMenu(int groudID,int topId)
        {
            var sql = @"Select t0.* from AdminMenu as t0
inner Join AdminMenuPass as t1 on t0.Id=t1.MenuId 
inner join AdminGroup as t2 on t1.AdminGroupId=t2.Id
where t2.IsDelete=0 and t2.Id=@0 and t0.IsDelete=0 and t0.ParentId=@1 and t1.ActionName='navbar'
Order by t0.Sort ASC";
            var list= CreateSqlHelper().Select<DbAdminMenu>(sql, groudID, topId);
            var ids = list.Select(q => q.Id).ToList();
            if (ids.Count>0) {
                sql = @"Select t0.* from AdminMenu as t0
inner Join AdminMenuPass as t1 on t0.Id=t1.MenuId 
inner join AdminGroup as t2 on t1.AdminGroupId=t2.Id
where t2.IsDelete=0 and t2.Id=@0 and t0.IsDelete=0 and t0.ParentId in ("+string.Join(",",ids) + @") and t1.ActionName='navbar'
Order by t0.Sort ASC";
                var list2 = CreateSqlHelper().Select<DbAdminMenu>(sql, groudID);
                list.AddRange(list2);
            }
            return list;
        }


    }
}
