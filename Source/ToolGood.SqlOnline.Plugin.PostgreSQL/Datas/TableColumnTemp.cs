/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
namespace ToolGood.SqlOnline.Plugin.PostgreSQL
{
    public class TableColumnTemp
    {
        public string SchemaName { get; set; }
        public string TableName { get; set; }

        public string TableType { get; set; }
        public int Index { get; set; }
        public string ColumnName { get; set; }
        public string Length { get; set; }
        public bool IsNullAble { get; set; }
        public string Precision { get; set; }
        public string Scale { get; set; }
        public string Comment { get; set; }
        public string DefaultValue { get; set; }
        public bool IsIdentity { get; set; }
        public string col_type { get; set; }
        public string elem_name { get; set; }
        public bool IsPrimaryKey { get; set; }

    }

}
