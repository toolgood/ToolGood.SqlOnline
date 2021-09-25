/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.IPlugin
{
    public interface ISqlViewProvider
    {
        List<ViewEntity> GetViews(string connStr, string databaseName);

        ViewEntity GetView(string connStr, string databaseName, string schemaName, string viewName);

        List<ViewColumnEntity> GetViewColumns(string connStr, string databaseName, string schemaName, string viewName);

        SqlEditorDto GetSelect100(string connStr, string databaseName, string schemaName, string viewName);

        SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string viewName);
        SqlEditorDto GetAlterSql(string connStr, string databaseName, string schemaName, string viewName);

    }


}
