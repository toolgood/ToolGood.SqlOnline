using System;
using System.Collections.Generic;
using System.Text;
using ToolGood.Datas;
using ToolGood.Infrastructure;

namespace ToolGood.Repository.Impl
{
    public partial class AdminRepository  
    {
        public bool ChangePassword(string name, string oldPassword, string newPassword)
        {
            var admin = Single(new { Name = name, IsDelete = 0 });
            if (admin.Password == oldPassword) {
                admin.Password = newPassword;
                Update(admin);
                return true;
            }
            return false;
        }

        public string CreatePassword(string user, string password, bool isMd5 = false)
        {
            if (isMd5) {
                return Hash.GetMd5String(user + "|ToolGood|" + password);
            }
            return Hash.GetMd5String(user + "|ToolGood|" + Hash.GetMd5String(password));
        }
    }
}
