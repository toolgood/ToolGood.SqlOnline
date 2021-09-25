/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon.Controllers;

namespace ToolGood.SqlOnline.Pages.Developments
{
    public class CodeGenInfoModel : PageModelBaseCore
    {
        private readonly IDevelopmentApplication _sqlOnlineApplication;

        public CodeGenInfoModel(IDevelopmentApplication sqlOnlineApplication)
        {
            _sqlOnlineApplication = sqlOnlineApplication;
        }
        public int SqlConnId { get; set; }
        public string Database { get; set; }
        public string Json { get; set; }


        public async Task<IActionResult> OnGetAsync(int sqlConnId, string database)
        {
            if (sqlConnId <= 0) { return Redirect("/NotFound"); }
            var use = await _sqlOnlineApplication.UseDevelopment();
            if (use == false) { return Redirect("/Developments/Prompt"); }
            SqlConnId = sqlConnId;
            Database = database;
            var languageTypeDict = ToolGood.Common.Utils.EnumUtil.GetDescriptions(typeof(ToolGood.SqlOnline.Datas.Enums.EnumTplLanguageType));
            var Languages = new Dictionary<string, string>();
            foreach (var item in languageTypeDict) {
                Languages[((ToolGood.SqlOnline.Datas.Enums.EnumTplLanguageType)item.Key).ToString()] = item.Value;
            }

            var list = await _sqlOnlineApplication.GetSqlCodeGens();
            var infos = Info_1.Analysis(list, Languages);
            Json = infos.ToJson();

            return Page();
        }

        #region Info
        public class Info_1
        {
            public int TplTypeId { get; set; }
            public string TplTypeName { get; set; }

            public List<Info_2> Items { get; set; }

            public static List<Info_1> Analysis(List<SqlCodeGenDto> list, Dictionary<string, string> languages)
            {
                List<Info_1> result = new List<Info_1>();
                var tplTypes = list.Select(q => q.TplType).Distinct().ToList();
                foreach (var tplType in tplTypes) {
                    Info_1 info = new Info_1();
                    info.TplTypeId = tplType;
                    if (info.TplTypeId == 1) {
                        info.TplTypeName = "表模板";
                    } else if (info.TplTypeId == 2) {
                        info.TplTypeName = "存储过程模板";
                    }
                    result.Add(info);

                    var tempList = list.Where(q => q.TplType == tplType).ToList();
                    info.Items = Info_2.Analysis(tempList, languages);
                }
                return result;
            }
        }
        public class Info_2
        {
            public string Language { get; set; }
            public string LanguageName { get; set; }
            public List<Info_3> Items { get; set; }

            public static List<Info_2> Analysis(List<SqlCodeGenDto> list, Dictionary<string, string> languages)
            {
                List<Info_2> result = new List<Info_2>();
                var Languages = list.Select(q => q.Language).Distinct().ToList();
                foreach (var Language in Languages) {
                    Info_2 info = new Info_2();
                    info.Language = Language;
                    info.LanguageName = languages[Language];
                    result.Add(info);
                    var tempList = list.Where(q => q.Language == Language).ToList();
                    info.Items = Info_3.Analysis(tempList);
                }
                return result;
            }

        }
        public class Info_3
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Namespace { get; set; }

            public static List<Info_3> Analysis(List<SqlCodeGenDto> list)
            {
                List<Info_3> result = new List<Info_3>();
                foreach (var dto in list) {
                    Info_3 info = new Info_3() {
                        Id = dto.Id,
                        Name = dto.Title,
                        Namespace = dto.Namespace
                    };
                    result.Add(info);
                }
                return result;
            }
        }

        #endregion




    }
}
