using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.TransDto
{
    public class PageQueryModel
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order { get; set; }

    }
}
