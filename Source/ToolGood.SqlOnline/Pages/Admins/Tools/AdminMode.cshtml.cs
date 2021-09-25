/*!
 *  ��Ȩ����(C) 2021 ToolGood(��֪��)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Pages.Admins.Tools
{
    public class AdminModeModel : AdminPageModel
    {
        public string RequestUrl { get; private set; }

        public void OnGet(string url)
        {
            RequestUrl = url;
            ViewData["Title"] = "����";
        }
    }
}
