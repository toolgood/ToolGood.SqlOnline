﻿/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;

namespace ToolGood.SqlOnline.Datas.Databases
{
    public class TableEntity
    {

        /// <summary>
        /// 数据库名
        /// </summary>
        public string DatabaseName { get; set; }


        /// <summary>
        /// Schema名
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }


        public List<TableColumnEntity> Columns { get; set; }


        public override string ToString()
        {
            return $"[{DatabaseName}].[{SchemaName}].[{TableName}]";
        }


    }

}
