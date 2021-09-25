/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.IPlugin
{
    public interface ISqlProcedureProvider
    {
        List<ProcedureEntity> GetProcedures(string connStr, string databaseName);

        ProcedureEntity GetProcedure(string connStr, string databaseName, string schemaName, string procedureName);

        List<ProcedureParamEntity> GetProcedureParams(string connStr, string databaseName, string schemaName, string procedureName);

        SqlEditorDto GetExecuteSql(string connStr, string databaseName, string schemaName, string procedureName);

        SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string procedureName);

        SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string procedureName);


    }


}
