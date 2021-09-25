/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolGood.SqlOnline.Dtos
{
    /// <summary>
    /// 权限关联表
    /// </summary>
    public class RelationshipDto : IAdminButtonPass
    {
        public int AdminId { get; set; }

        //public string AdminName { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }


        public int MenuId { get; set; }

        public string MenuCode { get; set; }

        //public string MenuName { get; set; }

        public int ButtonId { get; set; }

        public string ButtonCode { get; set; }

        //public string ButtonName { get; set; }

        //public int GroupCount { get; set; }
        //public int Count { get; set; }

        //public int Count2 { get; set; }

        public List<RelationshipDto> Items { get; set; }


        #region IButtonPass
        [Newtonsoft.Json.JsonIgnore]
        private Dictionary<string, bool> buttonPass = new Dictionary<string, bool>();

        bool IAdminButtonPass.CanEdit => HasButtonCode("edit");
        bool IAdminButtonPass.CanDelete => HasButtonCode("delete");
        bool IAdminButtonPass.CanEditOrDelete => HasButtonCode("edit") || HasButtonCode("delete");

        private bool HasButtonCode(string buttonCode)
        {
            if (buttonPass.TryGetValue(buttonCode, out bool t2)) { return t2; }
            var val = Items.Any(q => q.ButtonCode.Equals(buttonCode, StringComparison.OrdinalIgnoreCase));
            buttonPass[buttonCode] = val;
            return val;
        }

        bool IAdminButtonPass.HasButtonCode(string buttonCode)
        {
            return HasButtonCode(buttonCode);
        }
        #endregion
    }



}
