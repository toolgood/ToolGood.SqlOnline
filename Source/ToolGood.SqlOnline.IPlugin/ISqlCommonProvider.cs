/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using ToolGood.SqlOnline.Datas.Databases;
using ToolGood.SqlOnline.Dto;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.IPlugin
{
    public interface ISqlCommonProvider
    {
        List<DatabaseEntity> GetDatabases(string connStr);

        List<TableEntity> GetTables(string connStr, string databaseName);

        List<TableColumnEntity> GetTableColumns(string connStr, string databaseName, string schemaName, string tableName);

        List<TableShowEntity> GetTableShowList(string connStr, string databaseName);

        TableEntity GetTable(string connStr, string databaseName, string schemaName, string tableName);

        SqlEditorDto GetNew();

        SqlEditorDto GetSelect100(string connStr, string databaseName, string schemaName, string tableName);

        SqlEditorDto GetInsertSql(string connStr, string databaseName, string schemaName, string tableName);
        SqlEditorDto GetUpdateSql(string connStr, string databaseName, string schemaName, string tableName);
        SqlEditorDto GetDeleteSql(string connStr, string databaseName, string schemaName, string tableName);
        SqlEditorDto GetCreateSql(string connStr, string databaseName, string schemaName, string tableName);


        bool ChangeComment(string connStr, string databaseName, string schemaName, string tableName, string comment);

        bool ChangeComment(string connStr, string databaseName, string schemaName, string tableName, string columnName, string comment);

        DbCommand CreateCommand(string connStr, string databaseName);

        Task<ExecuteResult> ExecuteSql_Select(DbCommand command, string sql, int readMaxRows);

        Task<ExecuteResult> ExecuteSql_InsertUpdate(DbCommand command, string sql, int readMaxRows, int changeMaxRows);

        Task<ExecuteResult> ExecuteSql_Delete(DbCommand command, string sql, int readMaxRows, int changeMaxRows);

        Task<ExecuteResult> ExecuteSql_AllPermissions(DbCommand command, string sql);
    }



}
