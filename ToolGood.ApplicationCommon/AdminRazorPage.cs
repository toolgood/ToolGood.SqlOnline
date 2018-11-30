using Newtonsoft.Json;
using System.Linq;
using System.Text;
using ToolGood.Datas;
using ToolGood.Infrastructure;
using ToolGood.Infrastructure.Dependency;
using ToolGood.Repository;
using ToolGood.TransDto;

namespace ToolGood.ApplicationCommon
{
    public class AdminRazorPage<TModel> : BaseRazorPage<TModel>
    {
 
        #region ParentCode MenuCode ActionName Title Description
        //private string _parentCode;
        //public string ParentCode {
        //    get {
        //        if (_parentCode == null) {
        //            if (ViewData.TryGetValue("DataTokens_Admin_ParentCode", out object value)) {
        //                _parentCode = value.ToString();
        //            }
        //        }
        //        return _parentCode;
        //    }
        //}

        private string _menuCode;
        public string MenuCode {
            get {
                if (_menuCode == null) {
                    if (ViewData.TryGetValue("DataTokens_Admin_Code", out object value)) {
                        _menuCode = value.ToString();
                    }
                }
                return _menuCode;
            }
        }

        private string _actionName;
        public string ActionName {
            get {
                if (_actionName == null) {
                    if (ViewData.TryGetValue("DataTokens_Admin_ActionName", out object value)) {
                        _actionName = value.ToString();
                    }
                }
                return _actionName;
            }
        }

        private string _title;
        public string Title {
            get {
                if (_title == null) {
                    if (ViewData.TryGetValue("DataTokens_Admin_Title", out object value)) {
                        _title = value.ToString();
                    }
                }
                return _title;
            }
        }

        private string _description;
        public string Description {
            get {
                if (_description == null) {
                    if (ViewData.TryGetValue("DataTokens_Admin_Description", out object value)) {
                        _description = value.ToString();
                    }
                }
                return _description;
            }
        }
        #endregion

        #region Admin Permission
        private AdminDto _admin;

        public AdminDto Admin {
            get {
                if (_admin == null) {
                    _admin = ViewContext.ViewData["admin"] as AdminDto;

                    //if (ViewContext.ViewData[].view.TryGetValue(KeywordString.AdminSession, out byte[] bs)) {
                    //    _admin = JsonConvert.DeserializeObject<DbAdmin>(Encoding.UTF8.GetString(bs));
                    //}
                }
                return _admin;
            }
        }

        private ActionPermission _permission;

        public ActionPermission Permission {
            get {
                if (_permission == null) {
                    using (IIocScopeResolve resolver = ContainerManager.BeginLeftScope()) {
                        IAdminMenuPassRepository adminMenuPassRepository = resolver.Resolve<IAdminMenuPassRepository>();
                        var ps = adminMenuPassRepository.Where(q => q.Code == MenuCode).Where(q => q.AdminGroupId == Admin.GroupId).Select();
                        _permission = new ActionPermission(ps.Select(q => q.ActionName).ToList());
                    }
                }
                return _permission;
            }
        }
        #endregion

    }
}
