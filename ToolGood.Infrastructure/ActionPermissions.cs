using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolGood.Infrastructure
{
    public class ActionPermissionDictionary : Dictionary<string, ActionPermission>
    {
        private Dictionary<string, ActionPermission> _dictionary = new Dictionary<string, ActionPermission>();

        public void SetAllow(string code, string action, bool pass = true)
        {
            ActionPermission permission;
            if (_dictionary.TryGetValue(code, out permission) == false) {
                permission = new ActionPermission();
                _dictionary[code] = permission;
            }
            permission.SetAllow(action, pass);
        }

        public bool IsAllow(string code, string action)
        {
            ActionPermission permission;
            if (_dictionary.TryGetValue(code, out permission) == false) {
                return false;
            }
            return permission.IsAllow(action);
        }
    }

    /// <summary>
    /// 操作许可
    /// </summary>
    public class ActionPermission
    {
        private static Dictionary<string, string> _actionDictionary = new Dictionary<string, string>();
        private List<string> _allowList = new List<string>();

        static ActionPermission()
        {
            var t = "navbar=导航栏&Show=显示&add=添加&edit=编辑&delete=删除&Online=上线&Offline=下线" +
                    "&Start=启动&ReStart=重启&Pause=暂停&Stop=停止&Close=关闭" +
                    "&Audit=审核&Revoke=撤销&Invalid=作废&Lock=锁定&UnLock=解锁&redo=重做&Pass=通过&notPass=不通过" +
                    "&Message=短信&search=搜索" +
                    "&Copy=复制&Replace=替换&Build=生成&Print=打印" +
                    "&Instal=安装&UnLoad=卸载&Backup=备份&Restore=还原&export=导出&Import=导入" +
                    "&Authorize=授权&changePassword=修改密码&Code=Code";
            var ts = t.ToLower().Split('&');
            foreach (var s in ts) {
                var sp = s.Split('=');
                _actionDictionary[sp[0]] = sp[1];
            }
        }


        public static string GetActionName(string name)
        {
            return _actionDictionary[name.ToLower()];
        }

        public static Dictionary<string, string> GetAllActions()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var item in _actionDictionary) {
                dictionary[item.Key] = item.Value;
            }
            return dictionary;
        }

        public ActionPermission() { }

        public ActionPermission(string actions)
        {
            var acts = actions.Split(new char[] { '|', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var act in acts) {
                SetAllow(act);
            }
        }

        public ActionPermission(List<string> actions)
        {
            foreach (var action in actions) {
                SetAllow(action);
            }
        }

        /// <summary>
        /// 设置是否许可
        /// </summary>
        /// <param name="action"></param>
        /// <param name="pass"></param>
        public void SetAllow(string action, bool pass = true)
        {
            if (pass) {
                _allowList.Add(action.ToLower());
            } else {
                _allowList.RemoveAll(q => q == action.ToLower());
            }
        }

        /// <summary>
        /// 判断是否许可
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool IsAllow(string action)
        {
            action = action.ToLower();
            return _allowList.Any(q => q == action);
        }

        public override string ToString()
        {
            return string.Join(",", _allowList.OrderBy(q => q).Distinct());
        }

        /// <summary>
        /// Code
        /// </summary>
        public bool Code { get { return IsAllow("Code"); } set { SetAllow("Code", value); } }
        /// <summary>
        /// 导航栏
        /// </summary>
        public bool Navbar { get { return IsAllow("Navbar"); } set { SetAllow("Navbar", value); } }

        /// <summary>
        /// 导入
        /// </summary>
        public bool Import { get { return IsAllow("Import"); } set { SetAllow("Import", value); } }
        /// <summary>
        /// 导出
        /// </summary>
        public bool Export { get { return IsAllow("Export"); } set { SetAllow("Export", value); } }

        /// <summary>
        /// 搜索 
        /// </summary>
        public bool Search { get { return IsAllow("Search"); } set { SetAllow("Search", value); } }
        /// <summary>
        /// 通过 
        /// </summary>
        public bool Pass { get { return IsAllow("Pass"); } set { SetAllow("Pass", value); } }

        /// <summary>
        /// 不通过 
        /// </summary>
        public bool NotPass { get { return IsAllow("NotPass"); } set { SetAllow("NotPass", value); } }
        /// <summary>
        /// 重做 
        /// </summary>
        public bool ReDo { get { return IsAllow("ReDo"); } set { SetAllow("ReDo", value); } }


        /// <summary>
        /// 上线 
        /// </summary>
        public bool OnLine { get { return IsAllow("OnLine"); } set { SetAllow("OnLine", value); } }
        /// <summary>
        /// 下线 
        /// </summary>
        public bool OffLine { get { return IsAllow("OffLine"); } set { SetAllow("OffLine", value); } }
        /// <summary>
        /// 显示 
        /// </summary>
        public bool Show { get { return IsAllow("Show"); } set { SetAllow("Show", value); } }
        /// <summary>
        /// 添加 
        /// </summary>
        public bool Add { get { return IsAllow("Add"); } set { SetAllow("Add", value); } }
        /// <summary>
        /// 编辑
        /// </summary>
        public bool Edit { get { return IsAllow("Edit"); } set { SetAllow("Edit", value); } }
        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete { get { return IsAllow("Delete"); } set { SetAllow("Delete", value); } }
        /// <summary>
        /// 审核
        /// </summary>
        public bool Audit { get { return IsAllow("Audit"); } set { SetAllow("Audit", value); } }

        /// <summary>
        /// 撤销
        /// </summary>
        public bool Revoke { get { return IsAllow("Revoke"); } set { SetAllow("Revoke", value); } }



        /// <summary>
        /// 作废
        /// </summary>
        public bool Invalid { get { return IsAllow("Invalid"); } set { SetAllow("Invalid", value); } }
        /// <summary>
        /// 生成
        /// </summary>
        public bool Build { get { return IsAllow("Build"); } set { SetAllow("Build", value); } }
        /// <summary>
        /// 安装
        /// </summary>
        public bool Instal { get { return IsAllow("Instal"); } set { SetAllow("Instal", value); } }
        /// <summary>
        /// 卸载
        /// </summary>
        public bool UnLoad { get { return IsAllow("UnLoad"); } set { SetAllow("UnLoad", value); } }
        /// <summary>
        /// 备份
        /// </summary>
        public bool Backup { get { return IsAllow("Backup"); } set { SetAllow("Backup", value); } }
        /// <summary>
        /// 还原
        /// </summary>
        public bool Restore { get { return IsAllow("Restore"); } set { SetAllow("Restore", value); } }
        /// <summary>
        /// 替换
        /// </summary>
        public bool Replace { get { return IsAllow("Replace"); } set { SetAllow("Replace", value); } }
        /// <summary>
        /// 复制
        /// </summary>
        public bool Copy { get { return IsAllow("Copy"); } set { SetAllow("Copy", value); } }
        /// <summary>
        /// 授权
        /// </summary>
        public bool Authorize { get { return IsAllow("Authorize"); } set { SetAllow("Authorize", value); } }
        /// <summary>
        /// 打印
        /// </summary>
        public bool Print { get { return IsAllow("Print"); } set { SetAllow("Print", value); } }
        /// <summary>
        /// 锁定
        /// </summary>
        public bool Lock { get { return IsAllow("Lock"); } set { SetAllow("Lock", value); } }
        /// <summary>
        /// 解锁
        /// </summary>
        public bool UnLock { get { return IsAllow("UnLock"); } set { SetAllow("UnLock", value); } }
        /// <summary>
        /// 启动
        /// </summary>
        public bool Start { get { return IsAllow("Start"); } set { SetAllow("Start", value); } }
        /// <summary>
        /// 重启
        /// </summary>
        public bool ReStart { get { return IsAllow("ReStart"); } set { SetAllow("ReStart", value); } }
        /// <summary>
        /// 暂停
        /// </summary>
        public bool Pause { get { return IsAllow("Pause"); } set { SetAllow("Pause", value); } }
        /// <summary>
        /// 停止
        /// </summary>
        public bool Stop { get { return IsAllow("Stop"); } set { SetAllow("Stop", value); } }
        /// <summary>
        /// 关闭
        /// </summary>
        public bool Close { get { return IsAllow("Close"); } set { SetAllow("Close", value); } }

        /// <summary>
        /// 发送信息
        /// </summary>
        public bool Message { get { return IsAllow("Message"); } set { SetAllow("Message", value); } }
        /// <summary>
        /// 修改密码
        /// </summary>
        public bool ChangePassword { get { return IsAllow("ChangePassword"); } set { SetAllow("ChangePassword", value); } }
    }
    public enum ActionPermissionType
    {
        /// <summary>
        /// Code
        /// </summary>
        Code,
        /// <summary>
        /// 导航栏
        /// </summary>
        Navbar,

        /// <summary>
        /// 导入
        /// </summary>
        Import,
        /// <summary>
        /// 导出
        /// </summary>
        Export,

        /// <summary>
        /// 搜索 
        /// </summary>
        Search,
        /// <summary>
        /// 通过 
        /// </summary>
        Pass,

        /// <summary>
        /// 不通过 
        /// </summary>
        NotPass,
        /// <summary>
        /// 重做 
        /// </summary>
        ReDo,


        /// <summary>
        /// 上线 
        /// </summary>
        OnLine,
        /// <summary>
        /// 下线 
        /// </summary>
        OffLine,
        /// <summary>
        /// 显示 
        /// </summary>
        Show,
        /// <summary>
        /// 添加 
        /// </summary>
        Add,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit,
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        /// <summary>
        /// 审核
        /// </summary>
        Audit,

        /// <summary>
        /// 撤销
        /// </summary>
        Revoke,



        /// <summary>
        /// 作废
        /// </summary>
        Invalid,
        /// <summary>
        /// 生成
        /// </summary>
        Build,
        /// <summary>
        /// 安装
        /// </summary>
        Instal,
        /// <summary>
        /// 卸载
        /// </summary>
        UnLoad,
        /// <summary>
        /// 备份
        /// </summary>
        Backup,
        /// <summary>
        /// 还原
        /// </summary>
        Restore,
        /// <summary>
        /// 替换
        /// </summary>
        Replace,
        /// <summary>
        /// 复制
        /// </summary>
        Copy,
        /// <summary>
        /// 授权
        /// </summary>
        Authorize,
        /// <summary>
        /// 打印
        /// </summary>
        Print,
        /// <summary>
        /// 锁定
        /// </summary>
        Lock,
        /// <summary>
        /// 解锁
        /// </summary>
        UnLock,
        /// <summary>
        /// 启动
        /// </summary>
        Start,
        /// <summary>
        /// 重启
        /// </summary>
        ReStart,
        /// <summary>
        /// 暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 停止
        /// </summary>
        Stop,
        /// <summary>
        /// 关闭
        /// </summary>
        Close,

        /// <summary>
        /// 发送信息
        /// </summary>
        Message,
        /// <summary>
        /// 修改密码
        /// </summary>
        ChangePassword,
    }

 }
