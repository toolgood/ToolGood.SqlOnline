using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;
using ToolGood.Repository;
using ToolGood.Infrastructure;
using ToolGood.TransDto.Admins;
using System.Linq;

namespace ToolGood.Application.Impl
{
    public class AdminMenuApplication : IAdminMenuApplication
    {
        private readonly IAdminMenuRepository _adminMenuRepository;
        private readonly IAdminMenuPassRepository _adminMenuPassRepository;

        public AdminMenuApplication(IAdminMenuRepository adminMenuRepository, IAdminMenuPassRepository adminMenuPassRepository)
        {
            _adminMenuRepository = adminMenuRepository;
            _adminMenuPassRepository = adminMenuPassRepository;

        }

        public OperateResult<List<TopAdminMenu>> GetTopAdminMenu(int roleId, int topId = 1)
        {
            return OperateResult.Execute(() => {
                var result = _adminMenuRepository.GetTopAdminMenu(roleId);
                List<TopAdminMenu> list = result.Select(q => new TopAdminMenu(q, topId)).ToList();
                if (list.Any(q => q.Activity) == false) {
                    if (list.Count > 0) {
                        list[0].Activity = true;
                    }
                }
                return OperateResult.Success(list);
            }, "AdminMenuApplication-GetTopAdminMenu");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="topId"></param>
        /// <returns></returns>
        public OperateResult<List<TopAdminMenu>> GetTopAdminMenu(int roleId, string code)
        {
            return OperateResult.Execute(() => {
                var result = _adminMenuRepository.GetTopAdminMenu(roleId);
                List<TopAdminMenu> list = result.Select(q => new TopAdminMenu(q, code)).ToList();
                //if (list.Any(q => q.Activity) == false) {
                //    if (list.Count > 0) {
                //        list[0].Activity = true;
                //    }
                //}
                return OperateResult.Success(list);
            }, "AdminMenuApplication-GetTopAdminMenu");
        }


        public OperateResult<List<TreeAdminMenu>> GetAdminMenu(int roleId, int topId = 1)
        {
            return OperateResult.Execute(() => {
                var result = _adminMenuRepository.GetAdminMenu(roleId, topId);
                List<TreeAdminMenu> list = new List<TreeAdminMenu>();
                for (int i = 0; i < result.Count; i++) {
                    var item = result[i];
                    if (item.ParentId != topId) continue;
                    TreeAdminMenu menu = new TreeAdminMenu(item);
                    list.Add(menu);
                    for (int j = 0; j < result.Count; j++) {
                        var item2 = result[j];
                        if (item2.ParentId != item.Id) continue;
                        menu.Items.Add(new TreeAdminMenuItem(item2));
                    }
                }
                return OperateResult.Success(list);
            }, "AdminMenuApplication-GetAdminMenu");
        }

        public OperateResult<DbAdminMenu> GetMenuById(int id)
        {
            return OperateResult.Execute(() => {
                var menu = _adminMenuRepository.SingleById(id);
                return OperateResult.Success(menu);
            }, "AdminMenuApplication-GetMenuById");

        }


        public OperateResult<List<DbAdminMenu>> GetParentAdminMenu()
        {
            return OperateResult.Execute(() => {
                var list = _adminMenuRepository.Where(q => q.IsDelete == false).Where(q => q.ParentId == 0)
                    .OrderBy(q => q.Sort, ReadyGo3.OrderType.Asc).Select();
                var list2 = _adminMenuRepository.Where(q => q.IsDelete == false).WhereIn(q => q.ParentId, list.Select(q => q.Id).ToList())
                    .OrderBy(q => q.Sort, ReadyGo3.OrderType.Asc).Select();
                list.AddRange(list2);
                return OperateResult.Success(list);
            }, "AdminMenuApplication-GetParentAdminMenu");
        }


        public OperateResult<List<DbAdminMenu>> GetAdminMenuAll()
        {
            return OperateResult.Execute(() => {
                var result = _adminMenuRepository.Where(q => q.IsDelete == false).Select();
                return OperateResult.Success(result);
            }, "AdminMenuApplication-GetAdminMenuAll");
        }

        public OperateResult AddMenu(int pid, string name, string code, string url, string actions, int sort)
        {
            return OperateResult.Execute(() => {
                var m = _adminMenuRepository.Where(q => q.Code == code).Where(q => q.IsDelete == false).SelectCount();
                if (m > 0) {
                    return OperateResult.Failed("CODE 已存在！");
                }

                var menu = new DbAdminMenu() {
                    ParentId = pid,
                    Name = name,
                    Code = code,
                    Url = url,
                    Actions = actions,
                    Sort = sort,
                    AddingTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now
                };
                _adminMenuRepository.Insert(menu);
                return OperateResult.Success();
            }, "AdminMenuApplication-AddMenu");

        }

        public OperateResult EditMenu(int id, int pid, string name, string code, string url, string actions, int sort)
        {
            return OperateResult.Execute(() => {
                var m = _adminMenuRepository.Where(q => q.Id != id).Where(q => q.Code == code).Where(q => q.IsDelete == false).SelectCount();
                if (m > 0) {
                    return OperateResult.Failed("CODE 已存在！");
                }

                var menu = _adminMenuRepository.SingleById(id);
                menu.ParentId = pid;
                menu.Name = name;
                menu.Code = code;
                menu.Url = url;
                menu.Actions = actions;
                menu.Sort = sort;
                _adminMenuRepository.Update(menu);
                return OperateResult.Success();
            }, "AdminMenuApplication-EditMenu");
        }


        public OperateResult DeleteMenu(int id)
        {
            return OperateResult.Execute(() => {
                _adminMenuRepository.Update(new UpAdminMenu { IsDelete = true }, new UpAdminMenu { Id = id, IsDelete = false });
                return OperateResult.Success();
            }, "AdminMenuApplication-EditMenu");
        }

        public OperateResult MenuChangeSort(int[] ids, int[] sorts)
        {
            return OperateResult.Execute(() => {
                for (int i = 0; i < ids.Length; i++) {
                    _adminMenuRepository.Update(new UpAdminMenu { Sort = sorts[i] }, new UpAdminMenu { Id = ids[i], IsDelete = false });
                }
                return OperateResult.Success();
            }, "AdminMenuApplication-EditMenu");
        }




    }
}
