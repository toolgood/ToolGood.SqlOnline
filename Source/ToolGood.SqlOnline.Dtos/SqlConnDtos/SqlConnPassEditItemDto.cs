/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Dtos
{
    public class SqlConnPassEditItemDto
    {
        public int Id { get; set; }

        public int ConnId { get; set; }

        public bool CanRead { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }

        public bool AllPermissions { get; set; }

        public bool CanDownload { get; set; }
        public int ReadMaxRows { get; set; }
        public int ChangeMaxRows { get; set; }

        
    }
}
