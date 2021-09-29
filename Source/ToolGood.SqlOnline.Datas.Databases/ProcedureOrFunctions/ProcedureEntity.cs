/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */

using System.Collections.Generic;

namespace ToolGood.SqlOnline.Datas.Databases
{
    public class ProcedureEntity
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
        /// 存储过程名
        /// </summary>
        public string ProcedureName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        public string Language { get; set; }

        public List<ProcedureParamEntity> Params { get; set; }
        public override string ToString()
        {
            return $"Procedure:[{DatabaseName}].[{SchemaName}].[{ProcedureName}]";
        }
    }

}
