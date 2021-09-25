/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Linq;
using System.Threading.Tasks;
using ToolGood.SqlOnline.Application;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline
{
    public static class IpAddressHelper
    {
        public static async Task Init()
        {
            IAdminSecurityApplication ipAddressApplication = MyIoc.Create<IAdminSecurityApplication>();
            var useIpType = await ipAddressApplication.GetUseIpType();

            var ipFirewall = IpFirewallHelper.GetIpFirewall();
            var list = await ipAddressApplication.GetIpBanAll();
            ipFirewall.SetIpBanList(list.Select(q => q.Ip).ToList());
            var list2 = await ipAddressApplication.GetIpAllowAll();
            ipFirewall.SetIpAllowList(list2.Select(q => q.Ip).ToList());

            if (useIpType == 1) {
                ipFirewall.UseIpBanList();
            } else if (useIpType == 2) {
                ipFirewall.UseIpAllowList();
            } else {
                ipFirewall.CloseFirewall();
            }
        }

        public static void SetFirewallType(int useIpType)
        {
            var ipFirewall = IpFirewallHelper.GetIpFirewall();
            if (useIpType == 1) {
                ipFirewall.UseIpBanList();
            } else if (useIpType == 2) {
                ipFirewall.UseIpAllowList();
            } else {
                ipFirewall.CloseFirewall();
            }
        }

        public static async Task InitIpBlack(IAdminSecurityApplication ipAddressApplication)
        {
            var ipFirewall = IpFirewallHelper.GetIpFirewall();
            var list = await ipAddressApplication.GetIpBanAll();
            ipFirewall.SetIpBanList(list.Select(q => q.Ip).ToList());
        }

        public static async Task InitIpWhite(IAdminSecurityApplication ipAddressApplication)
        {
            var ipFirewall = IpFirewallHelper.GetIpFirewall();
            var list2 = await ipAddressApplication.GetIpAllowAll();
            ipFirewall.SetIpAllowList(list2.Select(q => q.Ip).ToList());
        }



    }

}
