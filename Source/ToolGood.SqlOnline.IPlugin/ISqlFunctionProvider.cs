/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.IPlugin
{
    public interface ISqlFunctionProvider
    {
        List<FunctionEntity> GetFunctions(string connStr, string databaseName);

        FunctionEntity GetFunction(string connStr, string databaseName, string schemaName, string functionName);

        List<FunctionParamEntity> GetFunctionParams(string connStr, string databaseName, string schemaName, string functionName);

        SqlEditorDto GetSelectSql(string connStr, string databaseName, string schemaName, string functionName);

        SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string functionName);

        SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string functionName);


    }


}
