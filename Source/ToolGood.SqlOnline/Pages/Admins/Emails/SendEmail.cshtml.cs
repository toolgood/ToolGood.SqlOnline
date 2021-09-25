/*!
 *  ��Ȩ����(C) 2021 ToolGood(��֪��)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;

namespace ToolGood.SqlOnline.Pages.Admins.Emails
{
    [AdminMenu("SendEmail", "show")]
    public class SendEmailModel : AdminPageModel
    {
        private readonly IAdminApplication _projectApplication;
        public SendEmailModel(IAdminApplication projectApplication)
        {
            _projectApplication = projectApplication;
        }

        public EmailSendDto EmailSendDto { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            EmailSendDto = await _projectApplication.GetEmailSendInfo();
            return Page();
        }
    }
}
