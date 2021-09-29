/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToolGood.SqlOnline.Datas.Databases;

namespace ToolGood.SqlOnline.Dtos
{
    public class StructureItemModel
    {
        /// <summary>
        /// f key(k) 标识(++)
        /// </summary>
        [JsonProperty("k")]
        public string KeyType { get; set; }

        [JsonProperty("n", NullValueHandling = NullValueHandling.Ignore)]
        public string ColumnName { get; set; }

        [JsonProperty("t")]
        public string ColumnType { get; set; }

        [JsonProperty("nu", NullValueHandling = NullValueHandling.Ignore)]
        public string Nullable { get; set; }

        [JsonProperty("c", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        [JsonProperty("v", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultValue { get; set; }

        public StructureItemModel() { }
        public StructureItemModel(TableColumnEntity columnEntity)
        {
            ColumnName = columnEntity.ColumnName;
            Nullable = columnEntity.IsNullAble ? "?" : null;
            if (string.IsNullOrEmpty(columnEntity.DefaultValue) == false) {
                DefaultValue = columnEntity.DefaultValue;
            }
            if (string.IsNullOrEmpty(columnEntity.Comment) == false) {
                Comment = columnEntity.Comment;
            }

            if (columnEntity.IsPrimaryKey && columnEntity.IsIdentity) {
                KeyType = "fka";
            } else if (columnEntity.IsPrimaryKey) {
                KeyType = "fk";
            } else if (columnEntity.IsIdentity) {
                KeyType = "fa";
            } else {
                KeyType = "f";
            }
            ColumnType = columnEntity.Type.ToLower();
            if (ColumnType == "varchar" || ColumnType == "nvarchar" || ColumnType == "char" || ColumnType == "nchar") {
                ColumnType = ColumnType + "(" + columnEntity.Length + ")";
            } else if (ColumnType == "decimal" || ColumnType == "numeric") {
                ColumnType = ColumnType + "(" + columnEntity.Precision + "," + columnEntity.Scale + ")";
            }
        }
        public StructureItemModel(ViewColumnEntity columnEntity)
        {
            ColumnName = columnEntity.ColumnName;
            Nullable = columnEntity.IsNullAble ? "?" : null;

            if (string.IsNullOrEmpty(columnEntity.Comment) == false) {
                Comment = columnEntity.Comment;
            }
            KeyType = "f";

            ColumnType = columnEntity.Type.ToLower();
            if (ColumnType == "varchar" || ColumnType == "nvarchar" || ColumnType == "char" || ColumnType == "nchar") {
                ColumnType = ColumnType + "(" + columnEntity.Length + ")";
            } else if (ColumnType == "decimal" || ColumnType == "numeric") {
                ColumnType = ColumnType + "(" + columnEntity.Precision + "," + columnEntity.Scale + ")";
            }
        }

        public StructureItemModel(TableShowEntity columnEntity)
        {
            ColumnName = columnEntity.ColumnName;
            Nullable = columnEntity.IsNullAble ? "?" : null;
            if (string.IsNullOrEmpty(columnEntity.DefaultValue) == false) {
                DefaultValue = columnEntity.DefaultValue;
            }
            if (string.IsNullOrEmpty(columnEntity.ColumnComment) == false) {
                Comment = columnEntity.ColumnComment;
            }

            if (columnEntity.IsPrimaryKey && columnEntity.IsIdentity) {
                KeyType = "fka";
            } else if (columnEntity.IsPrimaryKey) {
                KeyType = "fk";
            } else if (columnEntity.IsIdentity) {
                KeyType = "fa";
            } else {
                KeyType = "f";
            }
            ColumnType = columnEntity.Type.ToLower();
            if (ColumnType == "varchar" || ColumnType == "nvarchar" || ColumnType == "char" || ColumnType == "nchar") {
                ColumnType = ColumnType + "(" + columnEntity.Length + ")";
            } else if (ColumnType == "decimal" || ColumnType == "numeric") {
                ColumnType = ColumnType + "(" + columnEntity.Precision + "," + columnEntity.Scale + ")";
            }
        }
 

    }

}
