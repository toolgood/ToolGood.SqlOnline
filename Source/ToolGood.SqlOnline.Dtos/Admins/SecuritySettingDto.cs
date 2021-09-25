/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using ToolGood.Attributes;

namespace ToolGood.SqlOnline.Dtos
{
    public class SecuritySettingDto
    {
        public int UseDevelopment { get; set; }
        public int IpFilterType { get; set; }
        public string Logo { get; set; }

        public int UseRecordPassword { get; set; }

        [VaRequired]
        [VaMinLength(6)]
        public string LoginPassword { get; set; }
        [VaRequired]
        [VaMinLength(6)]
        public string ManagerPassword { get; set; }

        public int CookieTimes { get; set; }
        public int UseMachineCode { get; set; }
        public int FirstLoginUseMachineCode { get; set; }


        public int UseWatermark { get; set; }

        public string WatermarkText { get; set; }
    }
}
