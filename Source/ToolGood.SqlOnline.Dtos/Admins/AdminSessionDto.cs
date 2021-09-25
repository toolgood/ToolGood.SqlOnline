/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminSessionDto
    {
        public AdminSessionDto() { }

        public AdminSessionDto(int id, string name, string trueName, string jobNo)
        {
            Id = id;
            TrueName = trueName;
            Name = name;
            JobNo = jobNo;
        }



        public int Id { get; set; }

        public string Name { get; set; }

        public string JobNo { get; set; }

        /// <summary>
        /// 真名
        /// </summary>
        public string TrueName { get; set; }


        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? AdminModeExpireTime { get; set; }


        public void SetAdminMode(DateTime dateTime)
        {
            AdminModeExpireTime = dateTime;
        }

        public bool IsAdminMode()
        {
            if (AdminModeExpireTime != null) {
                if (AdminModeExpireTime > DateTime.Now) {
                    return true;
                }
            }
            return false;
        }
    }

}
