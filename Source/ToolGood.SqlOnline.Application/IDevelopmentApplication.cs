/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Threading.Tasks;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;
using ToolGood.SqlOnline.Dtos.CodeGens;

namespace ToolGood.SqlOnline.Application
{
    public interface IDevelopmentApplication
    {
        Task<bool> UseDevelopment();

        Task<List<SqlConnDto>> GetConnList();

        Task<List<DatabaseEntity>> GetDatabaseNames(int connId);

        Task<List<TableShowEntity>> GetTableShowColumns(int connId, string databaseName);


        Task<List<TableEntity>> GetTables(int connId, string databaseName);

        Task<List<ViewEntity>> GetViews(int connId, string databaseName);

        Task<List<ProcedureEntity>> GetProcedures(int connId, string databaseName);

        Task<List<SqlCodeGenDto>> GetSqlCodeGens();

        Task<string> GetSqlCodeGenTpl(int id);

        #region GetProcedureInfo GetTableInfo GetViewInfo

        Task<ProcedureInfo> GetProcedureInfo(int connId, string databaseName, string schemaName, string name);

        Task<TableViewInfo> GetTableInfo(int connId, string databaseName, string schemaName, string name);

        Task<TableViewInfo> GetViewInfo(int connId, string databaseName, string schemaName, string name);

        #endregion

    }
}
