/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */


namespace ToolGood.SqlOnline.Dtos
{
    public class SqlCodeGenDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 1) 表、视图，2）存储过程
        /// </summary>
        public int TplType { get; set; }

        public string Title { get; set; }

        public string Namespace { get; set; }

        /// <summary>
        /// 参考 <see cref="ToolGood.Oviki.Datas.Enums.EnumTplLanguageType"/>
        /// 编程语言
        /// </summary>
        public string Language { get; set; }

        public string Content { get; set; }

        public string Comment { get; set; }
    }
}
