/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.IO;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas;

namespace ToolGood.DataCreate
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.GetFullPath(@".\..\..\..\..\ToolGood.SqlOnline\App_Data\setting.sav");
            if (File.Exists(path)) {
                File.Delete(path);
            }
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.Create(path).Close();

            var srcHelper = SqlHelperFactory.OpenSqliteFile(path, "", false, JournalMode.Truncate);
            AdminTypes.CreateTable(srcHelper);
            AdminMenus.CreateMenu(srcHelper);
            AdminDatas.CreateData(srcHelper);

            ProjectTypes.CreateTable(srcHelper);
            ProjectMenus.CreateMenu(srcHelper);
            ProjectDatas.CreateData(srcHelper);


            Console.WriteLine("Data create ok!");
        }
    }
}
