using System;
using System.IO;
using ToolGood.Datas;
using ToolGood.ReadyGo3;

namespace ToolGood.DatabaseCreate
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.GetFullPath("data.sav");

            if (File.Exists(path)) {
                File.Delete(path);
            }
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Create(path).Close();
            var helper2 = SqlHelperFactory.OpenSqliteFile(path);
            Create(helper2);


            File.Copy(path, @"..\..\..\..\ToolGood.SqlOnline\bin\Debug\netcoreapp2.1\ToolGood.sav", true);
            //File.Copy(path, @"F:\somain\ToolGood.BoredPile\bin\Service\ToolGood.sav", true);
        }
        private static void Create(SqlHelper helper2)
        {
            AdminCreate.CreateAdminTable(helper2);
            AdminCreate.CreateAdminData(helper2);
            CreateMenu(helper2);
            //CreateTable(helper2);
            CreateData(helper2);
            AdminCreate.CreateMenuPass(helper2);
            helper2.Dispose();
        }
        private static void CreateMenu(SqlHelper helper)
        {
            var menuIndex = 1;
            {
                var topDesktop = new DbAdminMenu() { Name = "Sql查询", Code = "SqlDesktop", Url = "/SqlQuery/Main", Actions = "navbar", ParentId = 0, Sort = menuIndex++, };
                helper.Insert(topDesktop);
                var index = 1;

            }
        }
        private static void CreateData(SqlHelper helper)
        {
            helper.Insert(new DbDatabaseInfo() {
                ReadConnectionString = "Server=localhost;Database=baobei; User=toolgood;Password=toolgood;Charset=utf8;SslMode=none;Allow User Variables=True",
                RoleName= "somain",
                ConnectionString = "Server=localhost;Database=baobei; User=toolgood;Password=toolgood;Charset=utf8;SslMode=none;Allow User Variables=True",
                SqlServerType = "Mysql",
                Name = "测试",
                AddingTime = DateTime.Now,
                Sort = 1,
                LastUpdateTime = DateTime.Now,
                CreateAdminId = 0,
                CreateAdminName = "系统"
            });
            helper.Insert(new DbAdminDatabasePass() {
                DatabaseInfoId = 1,
                AdminGroupId = 1
            });
        }

    }
}
