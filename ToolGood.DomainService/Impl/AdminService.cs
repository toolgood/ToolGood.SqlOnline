using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.TransDto;
using Newtonsoft.Json;
using ToolGood.Infrastructure;
using ToolGood.Repository;
using ToolGood.RcxCrypto;

namespace ToolGood.DomainService.Impl
{
    public class AdminService : IAdminService
    {
        private const string _category = "system";
        private const string _key = "token";
        private static string _saltValue;

        private readonly ISettingRepository _settingRepository;
        private readonly IAdminRepository _adminRepository;

        public AdminService(ISettingRepository settingRepository, IAdminRepository adminRepository)
        {
            _settingRepository = settingRepository;
            _adminRepository = adminRepository;
        }

        /// <summary>
        /// 创建Token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public string CreateToken(AdminDto dto)
        {
            var str = JsonConvert.SerializeObject(dto);
            var key = GetSaltValue();
            var t = ThreeRCX.Encrypt(str, key, Encoding.UTF8);
            var hash = Hash.GetHmacSha256String(t, key);
            return  Base64.ToBase64ForUrlString(t) + "." + hash;
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public AdminDto GetToken(string cookie)
        {
            var sp = cookie.Split('.');
            var key = GetSaltValue();
            var bs = Base64.FromBase64ForUrlString(sp[0]);
            var hash = Hash.GetHmacSha256String(bs, key);
            if (sp[1] != hash) return null;
            var t = ThreeRCX.Encrypt(bs, key, Encoding.UTF8);
            return JsonConvert.DeserializeObject<AdminDto>(Encoding.UTF8.GetString(t));
        }
        /// <summary>
        /// 核对管理员
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool CheckAdmin(AdminDto token)
        {
            var admin = _adminRepository.Where(q => q.Id == token.Id).Where(q => q.IsDelete == false).FirstOrDefault();
            if (admin == null) return false;
            var password = Hash.GetMd5String(admin.Password);
            if (password != token.PasswordMd5) return false;

            return true;
        }


        /// <summary>
        /// 更新盐值，全部重新登录
        /// </summary>
        public void ChangeSaltValue()
        {
            var salt = DateTime.Now.ToString("yyyyMMddHHmmss");
            _settingRepository.Update(new Datas.UpSetting { Value = salt, LastUpdateTime = DateTime.Now },
                new Datas.UpSetting { Category = _category, Key = _key });
            _saltValue = null;
        }


        private string GetSaltValue()
        {
            if (_saltValue == null) {
                _saltValue = _settingRepository.Where(q => q.Category == _category)
                    .Where(q => q.Key == _key)
                    .FirstOrDefault(q => q.Value);
            }
            return _saltValue;
        }

    }
}
