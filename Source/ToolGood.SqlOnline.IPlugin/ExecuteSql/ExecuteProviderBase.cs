/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToolGood.SqlOnline.Dto;

namespace ToolGood.SqlOnline.IPlugin
{
    public abstract class ExecuteProviderBase
    {
        protected ISqlProvider _provider;
        protected static Regex selectRegex = new Regex(@"\b(delete|replace|insert|update|truncate|call|exec|execute|create|alter|drop|grant|change|backup|trigger|event|merge|rename|index|table|view|function|procedure|proc)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        protected static Regex insertUpdateRegex = new Regex(@"\b(delete|replace|truncate|call|exec|execute|create|alter|drop|grant|change|backup|trigger|event|merge|rename|index|table|view|function|procedure|proc)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        protected static Regex deleteRegex = new Regex(@"\b(truncate|call|exec|execute|create|alter|drop|grant|change|backup|trigger|event|merge|rename|index|table|view|function|procedure|proc)\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        protected static Regex updateDeleteRegex = new Regex(@"\b(update|delete)\b", RegexOptions.IgnoreCase);
        protected static Regex updateSqlRegex = new Regex(@"\bupdate +(.*?) +set +([\s\S]*?)(from +[\s\S]*?)?(where +[\s\S]+)$", RegexOptions.IgnoreCase);
        protected static Regex deleteSqlRegex = new Regex(@"\bdelete +from +([^ ]+) +(where [\s\S]+)$", RegexOptions.IgnoreCase);

        protected virtual Regex GetSelectRegex()
        {
            return selectRegex;
        }

        protected virtual Regex GetInsertUpdateRegex()
        {
            return insertUpdateRegex;
        }

        protected virtual Regex GetDeleteRegex()
        {
            return deleteRegex;
        }




        public virtual DbCommand CreateCommand(string connStr, string databaseName)
        {
            var factory = _provider.GetProviderFactory();
            var conn = factory.CreateConnection();
            conn.ConnectionString = connStr;
            conn.Open();
            conn.ChangeDatabase(databaseName);
            DbCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            return cmd;
        }

        #region ExecuteSql
        public virtual async Task<ExecuteResult> ExecuteSql_Select(DbCommand command, string sql, int readMaxRows)
        {
            var m = GetSelectRegex().Match(sql);
            ExecuteResult result = new ExecuteResult();
            if (m.Success) {
                result.IsException = true;
                result.Exception = "此SQL语句包含“" + m.Value + "”关键字！";
                return result;
            }
            var list = SqlStringList.SplitString(sql);
            var sqlList = list.SplitBySemi();

            try {
                result.Result = new List<ExecuteResultItem>();
                foreach (var item in sqlList) {
                    ExecuteResultItem sqlResult = new ExecuteResultItem();
                    result.Result.Add(sqlResult);
                    var b = await ExecuteSql(command, sqlResult, item, readMaxRows);
                    if (b == false) {
                        result.IsException = true;
                        result.Exception = sqlResult.Exception;
                        result.Result = null;
                        break;
                    }
                }
            } catch (Exception x) {
                result.IsException = true;
                result.Exception = x.Message;
                result.Result = null;
            }
            return result;
        }

        public virtual async Task<ExecuteResult> ExecuteSql_InsertUpdate(DbCommand command, string sql, int readMaxRows, int changeMaxRows)
        {
            var m = GetInsertUpdateRegex().Match(sql);
            ExecuteResult result = new ExecuteResult();
            if (m.Success) {
                result.IsException = true;
                result.Exception = "此SQL语句包含“" + m.Value + "”关键字！";
                return result;
            }
            var list = SqlStringList.SplitString(sql);
            var sqlList = list.SplitBySemi();

            try {
                result.Result = new List<ExecuteResultItem>();
                bool isRollback = false;
                using (var tran = await command.Connection.BeginTransactionAsync()) {
                    foreach (var item in sqlList) {
                        ExecuteResultItem sqlResult = new ExecuteResultItem();
                        result.Result.Add(sqlResult);
                        var b = await CheckUpdateDelete(command, sqlResult, item, changeMaxRows);
                        if (b) {
                            b = await ExecuteSql(command, sqlResult, item, readMaxRows);
                        }
                        if (b == false) {
                            result.IsException = true;
                            result.Exception = sqlResult.Exception;
                            result.Result = null;
                            isRollback = true;
                            tran.Rollback();
                            break;
                        }
                    }
                    if (isRollback == false) {
                        await tran.CommitAsync();
                    }
                }
            } catch (Exception x) {
                result.IsException = true;
                result.Exception = x.Message;
                result.Result = null;
            }
            return result;
        }

        public virtual async Task<ExecuteResult> ExecuteSql_Delete(DbCommand command, string sql, int readMaxRows, int changeMaxRows)
        {
            var m = GetDeleteRegex().Match(sql);
            ExecuteResult result = new ExecuteResult();
            if (m.Success) {
                result.IsException = true;
                result.Exception = "此SQL语句包含“" + m.Value + "”关键字！";
                return result;
            }
            var list = SqlStringList.SplitString(sql);
            var sqlList = list.SplitBySemi();

            try {
                result.Result = new List<ExecuteResultItem>();
                bool isRollback = false;
                using (var tran = await command.Connection.BeginTransactionAsync()) {
                    foreach (var item in sqlList) {
                        ExecuteResultItem sqlResult = new ExecuteResultItem();
                        result.Result.Add(sqlResult);
                        var b = await CheckUpdateDelete(command, sqlResult, item, changeMaxRows);
                        if (b) {
                            b = await ExecuteSql(command, sqlResult, item, readMaxRows);
                        }
                        if (b == false) {
                            result.IsException = true;
                            result.Exception = sqlResult.Exception;
                            result.Result = null;
                            isRollback = true;
                            tran.Rollback();
                            break;
                        }
                    }
                    if (isRollback == false) {
                        await tran.CommitAsync();
                    }
                }
            } catch (Exception x) {
                result.IsException = true;
                result.Exception = x.Message;
                result.Result = null;
            }
            return result;
        }

        public virtual async Task<ExecuteResult> ExecuteSql_AllPermissions(DbCommand command, string sql)
        {
            var list = SqlStringList.SplitString(sql);
            var sqlList = list.SplitBySemi();
            ExecuteResult result = new ExecuteResult();

            try {
                result.Result = new List<ExecuteResultItem>();
                bool isRollback = false;
                using (var tran = await command.Connection.BeginTransactionAsync()) {
                    foreach (var item in sqlList) {
                        ExecuteResultItem sqlResult = new ExecuteResultItem();
                        result.Result.Add(sqlResult);

                        var b = await ExecuteSql(command, sqlResult, item, int.MaxValue);
                        if (b == false) {
                            result.IsException = true;
                            result.Exception = sqlResult.Exception;
                            result.Result = null;
                            isRollback = true;
                            tran.Rollback();
                            break;
                        }
                    }
                    if (isRollback == false) {
                        await tran.CommitAsync();
                    }
                }
            } catch (Exception x) {
                result.IsException = true;
                result.Exception = x.Message;
                result.Result = null;
            }
            return result;
        }

        protected virtual async Task<bool> CheckUpdateDelete(DbCommand cmd, ExecuteResultItem sqlResult, string sql, int changeMaxRows)
        {
            if (updateDeleteRegex.IsMatch(sql)) {
                bool hasWhere = false;
                var m = updateSqlRegex.Match(sql);
                if (m.Success) {
                    hasWhere = true;
                    sqlResult.OldData = await GetOldDataJson(cmd, m.Groups[2].Value, m.Groups[1].Value, m.Groups[3].Value, m.Groups[4].Value, changeMaxRows);
                }
                m = deleteSqlRegex.Match(sql);
                if (m.Success) {
                    hasWhere = true;
                    sqlResult.OldData = await GetOldDataJson(cmd, "*", m.Groups[1].Value, null, m.Groups[2].Value, changeMaxRows);
                }
                if (hasWhere == false) {
                    sqlResult.Exception = "SQL statement has no where condition!";
                    return false;
                }
            }
            return true;
        }

        protected virtual async Task<string> GetOldDataJson(DbCommand cmd, string set, string table, string from, string where, int changeMaxRows)
        {
            string selectSql;
            string countSql;
            if (set == "*") {
                selectSql = $"Select * from {table} {where}";
                countSql = $"Select count(*) from {table} {where}";
            } else if (string.IsNullOrEmpty(from)) {
                selectSql = $"Select * from {table} {where}";
                countSql = $"Select count(*) from {table} {where}";
            } else {
                var sp = table.Trim().Split(' ');
                var t = sp.Length > 1 ? sp[1] : sp[0];
                selectSql = $"Select {t}.* {from} {where}";
                countSql = $"Select count(*) {from} {where}";
            }
            cmd.CommandText = countSql;
            var count = await cmd.ExecuteScalarAsync();
            if (int.Parse(count.ToString()) > changeMaxRows) {
                throw new Exception("Update/Delete 修改数量超过上限！");
            }

            cmd.CommandText = selectSql;
            var reader = await cmd.ExecuteReaderAsync();
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            try {
                int fieldCount = reader.FieldCount;
                var Columns = new List<string>();
                for (int i = 0; i < fieldCount; i++) { Columns.Add(reader.GetName(i)); }

                object[] vals = null; int rows = 0;
                while (await reader.ReadAsync()) {
                    vals = new object[fieldCount];
                    reader.GetValues(vals);

                    Dictionary<string, object> temp = new Dictionary<string, object>();
                    for (int j = 0; j < fieldCount; j++) {
                        temp[Columns[j]] = vals[j];
                    }
                    result.Add(temp);
                    rows++;
                }

            } finally {
                if (reader.IsClosed == false) {
                    reader.Close();
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }

        protected virtual async Task<bool> ExecuteSql(DbCommand cmd, ExecuteResultItem sqlResult, string sql, int readMaxRows)
        {
            cmd.CommandText = sql;
            sqlResult.Sql = sql;
            sqlResult.StartTime = DateTime.Now;
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            DbDataReader reader = null;
            try {
                reader = await cmd.ExecuteReaderAsync();
                if (reader.RecordsAffected > 0) { sqlResult.ChangeCount = reader.RecordsAffected; }
                int fieldCount = reader.FieldCount;
                if (fieldCount == 0) { return true; }

                sqlResult.Columns = new List<string>();
                sqlResult.Values = new List<object[]>();
                for (int i = 0; i < fieldCount; i++) { sqlResult.Columns.Add(reader.GetName(i)); }
                object[] vals = null; int rows = 0;
                while (await reader.ReadAsync()) {
                    if (rows >= readMaxRows) { sqlResult.IsOverflow = true; break; }
                    vals = new object[fieldCount];
                    reader.GetValues(vals);
                    sqlResult.Values.Add(vals);
                    rows++;
                }
                for (int i = 0; i < sqlResult.Columns.Count; i++) {
                    var name = sqlResult.Columns[i];
                    if (string.IsNullOrEmpty(name)) {
                        if (i == 0) {
                            sqlResult.Columns[i] = "Result";
                        } else {
                            sqlResult.Columns[i] = "Result_" + i.ToString();
                        }
                    }
                }
                return true;
            } catch (Exception ex) {
                stopwatch.Stop();
                sqlResult.RunTime = stopwatch.ElapsedMilliseconds;
                sqlResult.Exception = ex.Message;
            } finally {
                if (reader != null && reader.IsClosed == false) { reader.Close(); }
            }
            return false;
        }

        #endregion


    }
}
