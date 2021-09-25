/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */

namespace ToolGood.SqlOnline.Dtos
{
    public class SqlConnDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 连接字符串名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

        public string SqlType { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        public bool UseDevelopment { get; set; }


        public int  ReadMaxRows { get; set; }
        public int ChangeMaxRows { get; set; }

        
    }
}
