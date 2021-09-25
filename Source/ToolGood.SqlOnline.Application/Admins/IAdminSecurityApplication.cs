/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Application
{
    public interface IAdminSecurityApplication
    {
        #region 黑名单/白名单
        Task<bool> SetUseIpType(int ipType);

        Task<int> GetUseIpType();

        Task ResetIpFilter();
        #endregion

        #region 黑名单

        Task<bool> AddIpBan(Req<IpAddressEditDto> request);

        Task<bool> EditIpBan(Req<IpAddressEditDto> request);

        Task<bool> DeleteIpBan(Req<AdminIdDto> request);

        Task<DbSysIpBanlist> GetIpBanById(int id);

        Task<List<DbSysIpBanlist>> GetIpBanAll();

        Task<Page<IpAddressDto>> GetIpBanPage(Req<GetIpAddressDto> request);


        #endregion

        #region 白名单

        Task<bool> AddIpAllow(Req<IpAddressEditDto> request);

        Task<bool> EditIpAllow(Req<IpAddressEditDto> request);

        Task<bool> DeleteIpAllow(Req<AdminIdDto> request);

        Task<DbSysIpAllowlist> GetIpWhiteById(int id);

        Task<List<DbSysIpAllowlist>> GetIpAllowAll();

        Task<Page<IpAddressDto>> GetIpAllowPage(Req<GetIpAddressDto> request);


        #endregion

        Task<Page<AdminLoginLogDto>> GetAdminLoginLogPage(string name, bool? success, string ip, string field, string order, int page, int pageSize);

        Task<Page<AdminOperationLogDto>> GetAdminOperationLogPage(string name, string message, string ip, string field, string order, int page, int pageSize);


        Task<SecuritySettingDto> GetSecuritySetting();

        Task<bool> SetSecuritySetting(Req<SecuritySettingDto> request);

    }

}
