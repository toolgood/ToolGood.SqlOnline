/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ToolGood.Common.Extensions;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dto;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Pages.Sqls.Editors
{

    [Route("/Sqls/Editors/Ajax/{action}")]

    [AdminMenu("SqlOnlineDesktop", "show")]
    public class AjaxController : AdminApiController
    {
        private readonly ISqlOnlineApplication _sqlOnlineApplication;

        public AjaxController(ISqlOnlineApplication sqlOnlineApplication)
        {
            _sqlOnlineApplication = sqlOnlineApplication;
        }


        [HttpPost]
        public async Task<IActionResult> CreateExecute([FromBody] Req<ExecuteSqlDto> request)
        {
            request.Data.AdminId = AdminDto.Id;
            var executeResult = await _sqlOnlineApplication.CreateCommand(request);
            if (executeResult.StartsWith("ERROR:")) {
                return Error(executeResult);
            }
            return Success(executeResult, request.PasswordString);
        }

        [HttpPost]
        public IActionResult StopExecute([FromBody] Req<ExecuteSqlDto> request)
        {
            request.Data.AdminId = AdminDto.Id;
            var executeResult = _sqlOnlineApplication.StopCommand(request);
            return Success(executeResult, request.PasswordString);
        }

        [HttpPost]
        public async Task<IActionResult> DoExecuteSelectSql([FromBody] Req<ExecuteSqlDto> request)
        {
            request.Data.AdminId = AdminDto.Id;
            var executeResult = await _sqlOnlineApplication.ExecuteSql_Select(request);
            return Success(executeResult, request.PasswordString);
        }

        [HttpPost]
        public async Task<IActionResult> DoExecuteSql([FromBody] Req<ExecuteSqlDto> request)
        {
            request.Data.AdminId = AdminDto.Id;
            ExecuteResult executeResult;
            if (request.Data.Authority == 1) {
                executeResult = await _sqlOnlineApplication.ExecuteSql_InsertUpdate(request);
            } else if (request.Data.Authority == 2) {
                executeResult = await _sqlOnlineApplication.ExecuteSql_InsertUpdateDelete(request);
            } else if (request.Data.Authority == 3) {
                executeResult = await _sqlOnlineApplication.ExecuteSql_AllPermissions(request);
            } else {
                executeResult = await _sqlOnlineApplication.ExecuteSql_Select(request);
            }
            return Success(executeResult, request.PasswordString);
        }
        #region DoExecuteSql_Select_Export
        [HttpPost]
        public async Task<IActionResult> DoExecuteSql_Select_Export([FromForm] ExecuteSqlDto request)
        {
            ExecuteResult executeResult = await _sqlOnlineApplication.ExecuteSql_Select_Export(request, AdminDto.Id);
            if (executeResult.IsException) {
                return Error(executeResult);
            }
            if (request.ExportType == 0) { //txt
                var txt = BuildText(executeResult);
                return File(Encoding.UTF8.GetBytes(txt), "text/plain", $"Export_{DateTime.Now.ToString("yyyyMMddHHmmss")}.txt");
            } else if (request.ExportType == 1) { //excel
                var bytes = BuildExcel(executeResult);
                return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Export_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
            } else if (request.ExportType == 2) { //csv
                var txt = BuildCsv(executeResult);
                return File(Encoding.UTF8.GetBytes(txt), "text/csv", $"Export_{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv");
            } else if (request.ExportType == 3) { //json
                var txt = BuildJson(executeResult);
                return File(Encoding.UTF8.GetBytes(txt), "text/json", $"Export_{DateTime.Now.ToString("yyyyMMddHHmmss")}.json");
            } else if (request.ExportType == 4) { //xml
                var bytes = BuildXml(executeResult);
                return File(bytes, "application/xHTML+XML", $"Export_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xml");
            }
            return Success(executeResult);
        }
        private string BuildText(ExecuteResult result)
        {
            if (result.Result == null) { return ""; }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var resultItem in result.Result) {
                stringBuilder.AppendLine(resultItem.Sql);

                for (int i = 0; i < resultItem.Columns.Count; i++) {
                    var col = resultItem.Columns[i];
                    stringBuilder.Append(col);
                    if (i < resultItem.Columns.Count - 1) {
                        stringBuilder.Append('\t');
                    }
                }
                stringBuilder.AppendLine();

                foreach (var values in resultItem.Values) {
                    for (int i = 0; i < values.Length; i++) {
                        var col = values[i];
                        stringBuilder.Append(col);
                        if (i < resultItem.Columns.Count - 1) {
                            stringBuilder.Append('\t');
                        }
                    }
                    stringBuilder.AppendLine();
                }
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }

        private byte[] BuildExcel(ExecuteResult result)
        {
            if (result.Result == null) { return new byte[0]; }

            var workbook = new XSSFWorkbook();
            for (int i = 0; i < result.Result.Count; i++) {
                var resultItem = result.Result[i];

                ISheet sheet = workbook.CreateSheet("result_" + (i + 1).ToString());
                ICellStyle style = sheet.Workbook.CreateCellStyle();
                style.Alignment = HorizontalAlignment.Center;
                style.VerticalAlignment = VerticalAlignment.Center;
                int rowIndex = 0;
                #region 创建头部
                XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                int colNum = -1;
                for (int j = 0; j < resultItem.Columns.Count; j++) {
                    SetCellValue(dataRow, ++colNum, resultItem.Columns[j], style);
                }
                #endregion
                sheet.CreateFreezePane(0, 1, 0, 1);//冻结首行

                var doubleCellStyle = sheet.CreateDoubleCellStyle();

                rowIndex++;
                foreach (var values in resultItem.Values) {
                    var row = sheet.CreateRow(rowIndex);
                    for (int j = 0; j < values.Length; j++) {
                        var col = values[j];
                        if (null == col) {
                        } else if (col is int num_int) {
                            row.WriteDouble(j, num_int);
                        } else if (col is double num_double) {
                            row.WriteDouble(j, num_double, doubleCellStyle);
                        } else if (col is DateTime num_dt) {
                            row.WriteDateTime(j, num_dt);
                        } else if (col is string num_str) {
                            row.WriteString(j, num_str);
                        } else {
                            row.WriteString(j, col.ToString());
                        }
                    }
                    rowIndex++;
                }
            }
            var sqlsheet = workbook.CreateSheet("sql");
            for (int i = 0; i < result.Result.Count; i++) {
                var resultItem = result.Result[i];
                var hrow = sqlsheet.CreateRow(i);
                var cell = hrow.CreateCell(0);
                cell.SetCellValue("result_" + (i + 1).ToString());
                var cell2 = hrow.CreateCell(1);
                cell2.SetCellValue(resultItem.Sql);
            }

            using (var ms = new MemoryStream()) {
                workbook.Write(ms);
                return ms.ToArray();
            }
        }

        private void SetCellValue(IRow row, int index, string value, ICellStyle style)
        {
            var cell0 = row.CreateCell(index);
            cell0.CellStyle = style;
            if (value != null) {
                cell0.SetCellValue(value);
                var sheet = row.Sheet;
                var width = sheet.GetColumnWidth(index);
                if (width < (value.ToString().Length + 10) * 256) {
                    sheet.SetColumnWidth(index, (value.ToString().Length + 10) * 256);
                }
            }
        }

        private string BuildCsv(ExecuteResult result)
        {
            if (result.Result == null) { return ""; }


            StringBuilder stringBuilder = new StringBuilder();
            foreach (var resultItem in result.Result) {
                for (int i = 0; i < resultItem.Columns.Count; i++) {
                    var col = resultItem.Columns[i];
                    stringBuilder.Append(col);
                    if (i < resultItem.Columns.Count - 1) {
                        stringBuilder.Append(',');
                    }
                }
                stringBuilder.AppendLine();

                foreach (var values in resultItem.Values) {
                    for (int i = 0; i < values.Length; i++) {
                        var col = values[i];
                        stringBuilder.Append(col);
                        if (i < resultItem.Columns.Count - 1) {
                            stringBuilder.Append(',');
                        }
                    }
                    stringBuilder.AppendLine();
                }
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
                return stringBuilder.ToString();
            }
            return stringBuilder.ToString();
        }

        private string BuildJson(ExecuteResult result)
        {
            if (result.Result == null) { return "[]"; }

            foreach (var resultItem in result.Result) {
                List<Dictionary<string, object>> temps = new List<Dictionary<string, object>>();
                foreach (var values in resultItem.Values) {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    for (int i = 0; i < values.Length; i++) {
                        var col = values[i];
                        var name = resultItem.Columns[i];
                        dict[name] = col;
                    }
                    temps.Add(dict);
                }
                return Newtonsoft.Json.JsonConvert.SerializeObject(temps);
            }
            return "[]";
        }

        private byte[] BuildXml(ExecuteResult result)
        {
            if (result.Result == null) { return new byte[0]; }

            foreach (var resultItem in result.Result) {
                XmlDocument xmlDoc = new XmlDocument();
                XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                xmlDoc.AppendChild(xmlSM);
                XmlElement xmls = xmlDoc.CreateElement("list");
                xmlDoc.AppendChild(xmls);
                foreach (var values in resultItem.Values) {
                    XmlElement xml = xmlDoc.CreateElement("object");

                    for (int j = 0; j < values.Length; j++) {
                        var name = resultItem.Columns[j];
                        XmlElement obj = xmlDoc.CreateElement(name);
                        var col = values[j];
                        if (null == col) {
                        } else if (col is int num_int) {
                            obj.InnerText = num_int.ToString();
                        } else if (col is double num_double) {
                            obj.InnerText = num_double.ToString();
                        } else if (col is DateTime num_dt) {
                            obj.InnerText = num_dt.ToString("yyyy-MM-dd HH:mm:ss");
                        } else if (col is string num_str) {
                            obj.InnerText = num_str.ToString();
                        } else {
                            obj.InnerText = col.ToString();
                        }
                        xml.AppendChild(obj);
                    }
                    xmls.AppendChild(xml);
                }
                using (var tran=new MemoryStream()) {
                    xmlDoc.Save(tran);
                    return tran.ToArray();
                }
            }
            return new byte[0];
        }


        #endregion





    }
}
