﻿/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Dtos
{
    public interface IAdminButtonPass
    {
        bool HasButtonCode(string buttonCode);
        bool CanEdit { get; }
        bool CanDelete { get; }
        bool CanEditOrDelete { get; }
    }



}
