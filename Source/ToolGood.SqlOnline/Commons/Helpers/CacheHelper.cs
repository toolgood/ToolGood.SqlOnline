/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Concurrent;
using ToolGood.AntiDuplication;
using ToolGood.Common;
using ToolGood.Common.Extensions;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Datas;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline
{
    public class CacheHelper
    {
        private static readonly ConcurrentDictionary<int, string> AdminSessionIdCaches = new ConcurrentDictionary<int, string>();
        private static readonly ConcurrentDictionary<int, string> AdminBrowserPasswordCaches = new ConcurrentDictionary<int, string>();
        public static readonly AntiDupCache<string, bool> AdminMenuButtonCache = new AntiDupCache<string, bool>(10000, 5);
        public static readonly AntiDupCache<string, bool> AdminMenuCheckCache = new AntiDupCache<string, bool>(10000, 5);
        private static string WatermarkText;
        private static bool UseWatermark;


        static CacheHelper()
        {
            IAdminApplication adminApplication = MyIoc.Create<IAdminApplication>();
            var task = adminApplication.GetAdminCookieList().RunSync();
            //adminApplication.GetAdminSessionId().RunSynchronously()
            foreach (var item in task) {
                AdminSessionIdCaches[item.Id] = item.LastSessionID;
                AdminBrowserPasswordCaches[item.Id] = item.LastBrowserPassword;
            }
            var settingValue = adminApplication.GetSettingValueByCode("UseWatermark").RunSync();
            if (settingValue != null) {
                UseWatermark = settingValue.Value == "1";
            }

            var settingValue2 = adminApplication.GetSettingValueByCode("WatermarkText").RunSync();
            if (settingValue2 != null) {
                WatermarkText = settingValue2.Value;
            } else {
                WatermarkText = "{trueName} {yyyy}-{MM}-{dd}";
            }
        }

        public static void SetAdminSessionId(int userId, string sessionId, string password)
        {
            AdminSessionIdCaches[userId] = sessionId;
            AdminBrowserPasswordCaches[userId] = password;
        }

        public static bool CheckAdminSessionId(int userId, string cookie)
        {
            if (AdminSessionIdCaches.TryGetValue(userId, out string value)) {
                return (value == cookie);
            }
            return false;
        }
        public static string GetBrowserPassword(int userId)
        {
            if (AdminBrowserPasswordCaches.TryGetValue(userId, out string value)) {
                return value;
            }
            return null;
        }

        public static void UpdateWatermarkText()
        {
            IAdminApplication adminApplication = MyIoc.Create<IAdminApplication>();
            var settingValue = adminApplication.GetSettingValueByCode("UseWatermark").RunSync();
            if (settingValue != null) {
                UseWatermark = settingValue.Value == "1";
            }

            var settingValue2 = adminApplication.GetSettingValueByCode("WatermarkText").RunSync();
            if (settingValue2 != null) {
                WatermarkText = settingValue2.Value;
            } else {
                WatermarkText = "{trueName} {yyyy}-{MM}-{dd}";
            }
        }

        public static string GetWatermarkText(AdminSessionDto dto)
        {
            if (UseWatermark == false) {
                return "";
            }
            if (string.IsNullOrEmpty(WatermarkText)) {
                return "";
            }
            if (dto == null) {
                return "";
            }
            var hidden = dto.TrueName;
            if (hidden.Length == 2) {
                hidden = hidden[0].ToString() + "*";
            } else if (hidden.Length == 3) {
                hidden = hidden[0].ToString() + "*" + hidden[2].ToString();
            } else if (hidden.Length == 4) {
                hidden = hidden[0].ToString() + "**" + hidden[3].ToString();
            } else {
                hidden = hidden[0].ToString() + "***" + hidden[hidden.Length - 1].ToString();
            }

            return WatermarkText//.Replace("{name}", dto.Name)
                   .Replace("{trueName}", dto.TrueName)
                   .Replace("{hideName}", hidden)
                   .Replace("{jobNo}", dto.JobNo)
                   .Replace("{yyyy}", DateTime.Now.Year.ToString())
                   .Replace("{MM}", DateTime.Now.Month.ToString("D2"))
                   .Replace("{dd}", DateTime.Now.Day.ToString("D2"))
                   .Replace("{HH}", DateTime.Now.Hour.ToString("D2"))
                   .Replace("{mm}", DateTime.Now.Minute.ToString("D2"))
                   .Replace("{ss}", DateTime.Now.Second.ToString("D2"));
        }

        public static AdminCookieDto CreateAdminCookieDto(DbSysAdmin admin, int mins)
        {
            var dt = DateTime.Now;
            var exp = dt.AddMinutes(mins);
            AdminCookieDto userDto = new AdminCookieDto() {
                CreateTime = dt,
                ExpireTime = exp,
                UserId = admin.Id,
                UserName = admin.Name,
                PasswordHash = HashUtil.GetMd5String(admin.Password),
            };
            return userDto;
        }




    }

}
