using ToolGood.Datas;
using ToolGood.Infrastructure;
using ToolGood.ReadyGo3;

namespace ToolGood.Repository.Impl
{
    public partial class AdminLoginLogRepository
    {
        protected override SqlHelper CreateSqlHelper()
        {
            if (_sqlHelper == null) {
                _sqlHelper = Config.LogHelper;
            }
            return _sqlHelper;
        }



    }
}
