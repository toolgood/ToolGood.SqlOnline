/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using ToolGood.Attributes;

namespace ToolGood.SqlOnline.Dtos
{
    public class AdminMenuEditDto
    {
        public int Id { get; set; }

        public int Pid { get; set; }

        [VaRequired]
        [VaShortNameLength]
        public string Name { get; set; }

        [VaShortNameLength]
        public string Code { get; set; }

        [VaUrl]
        public string Url { get; set; }

        public int OrderNum { get; set; }



        public string[] Buttons { get; set; }

        public string[] ButtonChecks { get; set; }

    }



}
