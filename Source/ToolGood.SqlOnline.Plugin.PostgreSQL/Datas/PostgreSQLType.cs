﻿/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Plugin.PostgreSQL
{
    public class PostgreSQLType
    {
        public int oid { get; set; }
        public string nspname { get; set; }
        public string typname { get; set; }
    }
}
