/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Reflection;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas;

namespace ToolGood.DataCreate
{
    class ProjectTypes : List<Type>
    {
        public ProjectTypes()
        {
            Add(typeof(DbSqlConn));
            Add(typeof(DbSqlConnPass));
            
            Add(typeof(DbSqlDoc));
            Add(typeof(DbSqlDocTemp));

            Add(typeof(DbSqlQueryLog));
            Add(typeof(DbSqlQueryLogSetting));

            Add(typeof(DbSqlCodeGen));

        }



        public static void CreateTable(SqlHelper sqlHelper)
        {
            Console.WriteLine("CreateTable");
            var table = sqlHelper._TableHelper;
            foreach (var type in new ProjectTypes()) {
                Console.WriteLine("CreateTable: " + type.FullName);
                table.CreateTable(type);
            }
        }

        public static void CopyTable(SqlHelper srcHelper, SqlHelper helper)
        {
            var t = typeof(MemberTypes);
            var mi = t.GetMethod("CopyTableDatas", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);

            using (var tran = helper.UseTransaction()) {
                foreach (var type in new ProjectTypes()) {
                    var genMethod = mi.MakeGenericMethod(type);
                    genMethod.Invoke(null, new object[] { srcHelper, helper });
                }
                tran.Complete();
            }
        }

        private static void CopyTableDatas<T>(SqlHelper srcHelper, SqlHelper helper) where T : class, new()
        {
            Console.WriteLine("CopyTable: " + typeof(T).FullName);
            helper._TableHelper.DropTable(typeof(T));
            helper._TableHelper.CreateTable(typeof(T), false);
            var dbs = srcHelper.Select<T>();
            if (dbs.Count > 0) {
                helper.InsertList<T>(dbs);
            }
            var t = helper._TableHelper.GetCreateTableIndex(typeof(T));

            helper._TableHelper.CreateTableIndex(typeof(T));
        }
    }
}
