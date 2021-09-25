/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.WwwRoot;

namespace ToolGood.SqlOnline.WwwRootHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            WwwRootSetting setting = new WwwRootSetting();
            setting.NameSpace = "ToolGood.SqlOnline.Controllers";
            setting.InFolderPath = @".\..\..\..\..\ToolGood.SqlOnline\wwwroot";
            setting.OutFolderPath = @".\..\..\..\..\ToolGood.SqlOnline\Controllers\wwwroot";
            setting.ExcludeFileSuffixs = new System.Collections.Generic.List<string>() { ".map", ".old.js", ".src.js" };
            setting.BuildControllers();

            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("完成！");
        }
    }
}
