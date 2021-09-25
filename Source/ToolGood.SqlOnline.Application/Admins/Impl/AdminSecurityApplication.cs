/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Application.Admins
{
    public class AdminSecurityApplication : ApplicationBase, IAdminSecurityApplication
    {

        #region 黑名单/白名单


        public async Task<bool> SetUseIpType(int ipType)
        {
            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSysSettingValue>("set Value=@0 where Code='IpFilterType'", ipType);
            return count > 0;
        }

        public async Task<int> GetUseIpType()
        {
            var helper = GetReadHelper();
            var read = await helper.FirstOrDefault_Async<DbSysSettingValue>("where Code='IpFilterType'");
            if (read == null) {
                return 0;
            }
            return read.Value.ToSafeInt32(0);
        }

        public async Task ResetIpFilter()
        {
            var type = await GetUseIpType();
            IpFirewallHelper ipFirewallHelper = IpFirewallHelper.GetIpFirewall();
            if (type == 0) {
                ipFirewallHelper.CloseFirewall();
            } else if (type == 1) {
                ipFirewallHelper.UseIpAllowList();
                var all = await GetIpAllowAll();
                ipFirewallHelper.SetIpAllowList(all.Select(q => q.Ip).ToList());
                all = null;
            } else if (type == 2) {
                ipFirewallHelper.UseIpBanList();
                var all = await GetIpBanAll();
                ipFirewallHelper.SetIpBanList(all.Select(q => q.Ip).ToList());
                all = null;
            }
        }

        #endregion

        #region 黑名单

        public async Task<bool> AddIpBan(Req<IpAddressEditDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }

            var db = new DbSysIpBanlist() {
                AddingTime = DateTime.Now,
                AddingAdminId = request.OperatorId,
                ModifyTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
                Name = request.Data.Name,
                Ip = request.Data.Ip,
                IsDisable = request.Data.IsDisable,
            };
            var helper = GetWriteHelper();
            await helper.Insert_Async(db);
            await ResetIpFilter();
            return true;
        }

        public async Task<bool> EditIpBan(Req<IpAddressEditDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }

            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSysIpBanlist>(new {
                ModifyTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
                Name = request.Data.Name,
                Ip = request.Data.Ip,
                IsDisable = request.Data.IsDisable,
            }, new { Id = request.Data.Id });

            await ResetIpFilter();

            return count > 0;
        }

        public async Task<bool> DeleteIpBan(Req<AdminIdDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Id == null) { throw new ArgumentException("Data.Id is null"); }

            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSysIpBanlist>(new {
                IsDelete = true,
                ModifyAdminId = request.OperatorId,
                ModifyTime = DateTime.Now,
            }, new { Id = request.Data.Id, IsDelete = false });

            await ResetIpFilter();

            return count > 0;
        }

        public Task<DbSysIpBanlist> GetIpBanById(int id)
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysIpBanlist>(q => q.IsDelete == false)
                    .Where(q => q.Id == id)
                    .FirstOrDefault_Async();
        }

        public Task<List<DbSysIpBanlist>> GetIpBanAll()
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysIpBanlist>(q => q.IsDelete == false)
                    .Where(q => q.IsDisable == false)
                    .OrderBy(q => q.Id, OrderType.Desc)
                    .Select_Async();

        }

        public Task<Page<IpAddressDto>> GetIpBanPage(Req<GetIpAddressDto> request)
        {
            if (request == null) { throw new ArgumentNullException(nameof(request)); }
            if (request.Data == null) { throw new ArgumentNullException("Data"); }

            var helper = GetReadHelper();
            return helper.Where<DbSysIpBanlist>()
                  .Where(q => q.IsDelete == false)
                  .IfSet(request.Data.Name).Where(q => q.Name.Contains(request.Data.Name))
                  .IfSet(request.Data.IP).Where(q => q.Ip.Contains(request.Data.IP))

                  .IfSet(request.Data.Field).OrderBy(request.Data.Field, request.Data.Order)
                  .IfNotSet(request.Data.Field).OrderBy(q => q.Id, OrderType.Desc)
                  .Page_Async<IpAddressDto>(request.Data.PageIndex, request.Data.PageSize);
        }


        #endregion

        #region 白名单

        public async Task<bool> AddIpAllow(Req<IpAddressEditDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }

            var db = new DbSysIpAllowlist() {
                AddingTime = DateTime.Now,
                AddingAdminId = request.OperatorId,
                ModifyTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
                Name = request.Data.Name,
                Ip = request.Data.Ip,
                IsDisable = request.Data.IsDisable,
            };
            var helper = GetWriteHelper();
            await helper.Insert_Async(db);
            await ResetIpFilter();

            return true;
        }

        public async Task<bool> EditIpAllow(Req<IpAddressEditDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }

            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSysIpAllowlist>(new {
                ModifyTime = DateTime.Now,
                ModifyAdminId = request.OperatorId,
                Name = request.Data.Name,
                Ip = request.Data.Ip,
                IsDisable = request.Data.IsDisable,
            }, new { Id = request.Data.Id });
            await ResetIpFilter();

            return count > 0;
        }

        public async Task<bool> DeleteIpAllow(Req<AdminIdDto> request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }
            if (request.Data == null) { throw new ArgumentException("Data is null"); }
            if (request.Data.Id == null) { throw new ArgumentException("Data.Id is null"); }

            var helper = GetWriteHelper();
            var count = await helper.Update_Async<DbSysIpAllowlist>(new {
                IsDelete = true,
                ModifyAdminId = request.OperatorId,
                ModifyTime = DateTime.Now,
            }, new { Id = request.Data.Id, IsDelete = false });
            await ResetIpFilter();

            return count > 0;
        }

        public Task<DbSysIpAllowlist> GetIpWhiteById(int id)
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysIpAllowlist>(q => q.IsDelete == false)
                    .Where(q => q.Id == id)
                    .FirstOrDefault_Async();
        }

        public Task<List<DbSysIpAllowlist>> GetIpAllowAll()
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysIpAllowlist>(q => q.IsDelete == false)
                    .Where(q => q.IsDisable == false)
                    .OrderBy(q => q.Id, OrderType.Desc)
                    .Select_Async();
        }

        public Task<Page<IpAddressDto>> GetIpAllowPage(Req<GetIpAddressDto> request)
        {
            if (request == null) { throw new ArgumentNullException(nameof(request)); }
            if (request.Data == null) { throw new ArgumentNullException("Data"); }

            var helper = GetReadHelper();
            return helper.Where<DbSysIpAllowlist>()
                  .Where(q => q.IsDelete == false)
                  .IfSet(request.Data.Name).Where(q => q.Name.Contains(request.Data.Name))
                  .IfSet(request.Data.IP).Where(q => q.Ip.Contains(request.Data.IP))

                  .IfSet(request.Data.Field).OrderBy(request.Data.Field, request.Data.Order)
                  .IfNotSet(request.Data.Field).OrderBy(q => q.Id, OrderType.Desc)
                  .Page_Async<IpAddressDto>(request.Data.PageIndex, request.Data.PageSize);
        }

        #endregion

        public Task<Page<AdminLoginLogDto>> GetAdminLoginLogPage(string name, bool? success, string ip, string field, string order, int page, int pageSize)
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysAdminLoginLog>()
                    .IfSet(name).Where(q => q.Name.Contains(name))
                    .IfNotNull(success).Where(q => q.Success == success.Value)
                    .IfSet(ip).Where(q => q.Ip == ip)
                    .IfSet(field).OrderBy(field, order)
                    .IfNotSet(field).OrderBy(q => q.Id, OrderType.Desc)
                    .Page_Async<AdminLoginLogDto>(page, pageSize);

        }


        public Task<Page<AdminOperationLogDto>> GetAdminOperationLogPage(string name, string message, string ip, string field, string order, int page, int pageSize)
        {
            var helper = GetReadHelper();
            return helper.Where<DbSysAdminOperationLog>()
                    .IfSet(name).Where(q => q.Name.Contains(name))
                    .IfNotNull(message).Where(q => q.Message.Contains(message))
                    .IfSet(field).OrderBy(field, order)
                    .IfNotSet(field).IfSet(ip).Where(q => q.Ip == ip)
                    .OrderBy(q => q.Id, OrderType.Desc)
                    .Page_Async<AdminOperationLogDto>(page, pageSize);
        }


        public async Task<SecuritySettingDto> GetSecuritySetting()
        {
            var helper = GetReadHelper();
            const string sql = @"select Code,Value from SysSettingValue where Code in 
('UseDevelopment','IpFilterType','Logo','UseRecordPassword','LoginPassword','ManagerPassword','UseMachineCode','FirstLoginUseMachineCode'
,'CookieTimes','UseWatermark','WatermarkText')";

            var list = await helper.Select_Async<DbSysSettingValue>(sql);
            SecuritySettingDto dto = new SecuritySettingDto();
            dto.UseDevelopment = list.Where(q => q.Code == "UseDevelopment").FirstOrDefault().SelectNull(q => q.Value).ToSafeInt32(0);
            dto.IpFilterType = list.Where(q => q.Code == "IpFilterType").FirstOrDefault().SelectNull(q => q.Value).ToSafeInt32(0);
            dto.Logo = list.Where(q => q.Code == "Logo").FirstOrDefault().SelectNull(q => q.Value);
            dto.UseRecordPassword = list.Where(q => q.Code == "UseRecordPassword").FirstOrDefault().SelectNull(q => q.Value).ToSafeInt32(0);
            dto.LoginPassword = list.Where(q => q.Code == "LoginPassword").FirstOrDefault().SelectNull(q => q.Value);
            dto.ManagerPassword = list.Where(q => q.Code == "ManagerPassword").FirstOrDefault().SelectNull(q => q.Value);
            dto.UseMachineCode = list.Where(q => q.Code == "UseMachineCode").FirstOrDefault().SelectNull(q => q.Value).ToSafeInt32(0);
            dto.FirstLoginUseMachineCode = list.Where(q => q.Code == "FirstLoginUseMachineCode").FirstOrDefault().SelectNull(q => q.Value).ToSafeInt32(0);
            dto.CookieTimes = list.Where(q => q.Code == "CookieTimes").FirstOrDefault().SelectNull(q => q.Value).ToSafeInt32(0);
            dto.UseWatermark = list.Where(q => q.Code == "UseWatermark").FirstOrDefault().SelectNull(q => q.Value).ToSafeInt32(0);
            dto.WatermarkText = list.Where(q => q.Code == "WatermarkText").FirstOrDefault().SelectNull(q => q.Value);
            return dto;
        }

        public async Task<bool> SetSecuritySetting(Req<SecuritySettingDto> request)
        {
            var helper = GetWriteHelper();

            using (var tran = helper.UseTransaction()) {
                await helper.Update_Async<DbSysSettingValue>("set Value=@0 where Code='UseDevelopment'", request.Data.UseDevelopment);
                await helper.Update_Async<DbSysSettingValue>("set Value=@0 where Code='IpFilterType'", request.Data.IpFilterType);
                await helper.Update_Async<DbSysSettingValue>("set Value=@0 where Code='Logo'", request.Data.Logo);
                await helper.Update_Async<DbSysSettingValue>("set Value=@0 where Code='UseRecordPassword'", request.Data.UseRecordPassword);
                await helper.Update_Async<DbSysSettingValue>("set Value=@0 where Code='LoginPassword'", request.Data.LoginPassword);
                await helper.Update_Async<DbSysSettingValue>("set Value=@0 where Code='ManagerPassword'", request.Data.ManagerPassword);
                await helper.Update_Async<DbSysSettingValue>("set Value=@0 where Code='UseMachineCode'", request.Data.UseMachineCode);
                await helper.Update_Async<DbSysSettingValue>("set Value=@0 where Code='CookieTimes'", request.Data.CookieTimes);
                await helper.Update_Async<DbSysSettingValue>("set Value=@0 where Code='UseWatermark'", request.Data.UseWatermark);
                await helper.Update_Async<DbSysSettingValue>("set Value=@0 where Code='WatermarkText'", request.Data.WatermarkText);
                await helper.Update_Async<DbSysSettingValue>("set Value=@0 where Code='FirstLoginUseMachineCode'", request.Data.FirstLoginUseMachineCode);
                tran.Complete();
            }
            await ResetIpFilter();
            return true;
        }




    }

}
