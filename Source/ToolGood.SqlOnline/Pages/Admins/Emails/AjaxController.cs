/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Application;
using ToolGood.SqlOnline.Dtos;
using ToolGood.WebCommon;

namespace ToolGood.SqlOnline.Pages.Admins.Emails
{
    [Route("/admins/Emails/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {
        private readonly IAdminApplication _adminApplication;

        public AjaxController(IAdminApplication adminApplication)
        {
            _adminApplication = adminApplication;
        }

        [AdminMenu("SendEmail", "edit")]
        [HttpPost]
        public async Task<IActionResult> EditEmailSend([FromBody] Req<EmailSendDto> request)
        {
            var b = await _adminApplication.EditEmailSend(request);
            if (b == false) return Error(request.Message);
            return Success();
        }



    }
}
