/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.WebCommon.Internals
{
    public interface ILogger
    {
        bool UseDebug { get; set; }
        bool UseInfo { get; set; }
        bool UseWarn { get; set; }
        bool UseError { get; set; }
        bool UseFatal { get; set; }
        bool UseSql { get; set; }

        void WriteLog(string type, string msg);
        void WriteLog(QueryArgs queryArgs, LogType type, string msg);

    }


}
