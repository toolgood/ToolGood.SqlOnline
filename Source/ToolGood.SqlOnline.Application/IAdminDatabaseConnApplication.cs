/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.Dtos.CodeGens;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Application
{
    public interface IAdminDatabaseConnApplication
    {
        List<String> GetSqlTypes();


        #region 数据库链接字符串
        Task<bool> AddDatabaseConn(Req<SqlConnDto> request);

        Task<bool> EditDatabaseConn(Req<SqlConnDto> request);

        Task<bool> DeleteDatabaseConn(Req<AdminIdDto> request);

        Task<List<SqlConnDto>> GetDatabaseConnAll(Req<SearchDto> request);

        Task<List<SqlConnDto>> GetDatabaseConnAllWithAdminId(Req<SearchDto> request);


        Task<SqlConnDto> GetDatabaseConnById(int id);

        #endregion

        #region 数据库链接字符串 PASS
        Task<bool> EditDatabaseConnPass(Req<SqlConnPassEditDto> request);

        Task<bool> DeleteDatabaseConnPass(Req<AdminIdDto> request);

        Task<SqlConnPassDto> GetDatabaseConnPassById(int id);

        Task<List<SqlConnPassDto>> GetConnPowerList(Req<SearchDto> request);


        Task<Page<SqlConnPassDto>> GetDatabaseConnPage(Req<SearchDto> requeste);

        Task<List<SqlConnPassDto>> GetDatabaseConnPassListByAdminGroupId(int adminGropId);

        #endregion

        #region SqlQueryLogSetting
        Task<bool> AddLogSetting(Req<SqlQueryLogSettingDto> request);
        Task<bool> EditLogSetting(Req<SqlQueryLogSettingDto> request);
        Task<bool> DeleteLogSetting(Req<AdminIdDto> request);
        Task<Page<SqlQueryLogSettingDto>> GetLogSettingList(Req<SearchDto> request);
        Task<SqlQueryLogSettingDto> GetLogSettingById(int id);
        #endregion

        #region SqlDoc
        Task<bool> DeleteSqlDoc(Req<AdminIdDto> request);
        Task<AdminSqlDocDto> GetSqlDocById(int id);
        Task<Page<AdminSqlDocDto>> GetSqlDocList(Req<AdminSqlDocSearchDto> request);

        #endregion

        #region SqlDocTemp
        Task<bool> DeleteSqlDocTemp(Req<AdminIdDto> request);
        Task<AdminSqlDocDto> GetSqlDocTempById(int id);
        Task<Page<AdminSqlDocDto>> GetSqlDocTempList(Req<AdminSqlDocSearchDto> request);
        #endregion


        #region CodeGens

        Task<bool> AddCodeGen(Req<SqlCodeGenDto> request);
        Task<bool> EditCodeGen(Req<SqlCodeGenDto> request);
        Task<bool> DeleteCodeGen(Req<AdminIdDto> request);

        Task<Page<SqlCodeGenDto>> GetSqlCodeGenList(Req<SqlCodeGenSearchDto> request);

        Task<SqlCodeGenDto> GetSqlCodeGenById(int id);

        Task<ProcedureInfo> GetProcedureInfo(Req<SqlCodeGenSearchDto> request);
        Task<TableViewInfo> GetTableInfo(Req<SqlCodeGenSearchDto> request);

        #endregion


    }
}
