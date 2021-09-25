/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */

using System.Collections.Generic;

namespace ToolGood.SqlOnline.Datas.Databases
{
    public class FunctionEntity
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
        public string FunctionName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 列类型
        /// </summary>
        public string ReturnType { get; set; }

        /// <summary>
        /// 列长度
        /// </summary>
        public string ReturnLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReturnPrecision { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public string ReturnScale { get; set; }


        public List<FunctionParamEntity> Params { get; set; }
    }

}
