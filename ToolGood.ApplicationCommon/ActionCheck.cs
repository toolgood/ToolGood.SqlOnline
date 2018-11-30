using System.Collections.Generic;
using ToolGood.Infrastructure.Dependency;
using ToolGood.Repository;


namespace ToolGood.ApplicationCommon
{
    public static class ActionCheck
    {
        private static SortedSet<string> _adminActions;

        static ActionCheck()
        {
            Update();
        }
        public static void Update()
        {
            using (IIocScopeResolve resolver = ContainerManager.BeginLeftScope()) {
                IAdminMenuPassRepository adminMenuPassRepository = resolver.Resolve<IAdminMenuPassRepository>();
                var passList = adminMenuPassRepository.Select(null);
                SortedSet<string> set = new SortedSet<string>();
                foreach (var item in passList) {
                    set.Add((item.ActionName + "." + item.Code + "." + item.AdminGroupId.ToString()).ToLower());
                }
                _adminActions = set;
            }
        }


        public static bool IsAdminPass(string menuCode, string actionName, int groupId)
        {
            //if (groupId == 1) { return true; }

            var txt = (actionName + "." + menuCode + "." + groupId.ToString()).ToLower();
            return _adminActions.Contains(txt);
        }

    }

}
