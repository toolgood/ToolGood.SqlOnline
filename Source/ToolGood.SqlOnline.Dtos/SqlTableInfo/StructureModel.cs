/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System.Collections.Generic;
using Newtonsoft.Json;
using ToolGood.SqlOnline.Datas.Databases;

namespace ToolGood.SqlOnline.Dtos
{
    public class StructureModel
    {
        /// <summary>
        /// table(t),view(v),proc(p)
        /// </summary>
        [JsonProperty("t")]
        public string Type { get; set; }

        [JsonProperty("n")]
        public string Name { get; set; }

        [JsonProperty("c", NullValueHandling = NullValueHandling.Ignore)]
        public string Comment { get; set; }

        [JsonProperty("items")]
        public List<StructureItemModel> Items { get; set; }


        public StructureModel()
        {
            Items = new List<StructureItemModel>();
        }
        public StructureModel(TableEntity tableEntity)
        {
            Type = "t";
            Name = tableEntity.TableName;
            if (string.IsNullOrEmpty(tableEntity.Comment) == false) {
                Comment = tableEntity.Comment;
            }
            Items = new List<StructureItemModel>();
        }

        public StructureModel(ViewEntity viewEntity)
        {
            Type = "v";
            Name = viewEntity.ViewName;
            if (string.IsNullOrEmpty(viewEntity.Comment) == false) {
                Comment = viewEntity.Comment;
            }
            Items = new List<StructureItemModel>();
        }

        public StructureModel(ProcedureEntity procedureEntity)
        {
            Type = "v";
            Name = procedureEntity.ProcedureName;
            if (string.IsNullOrEmpty(procedureEntity.Comment) == false) {
                Comment = procedureEntity.Comment;
            }
            Items = new List<StructureItemModel>();
        }


        public StructureModel(ViewColumnEntity columnEntity)
        {
            var t = columnEntity.Type.ToLower();
            if (t == "v" || t == "view") {
                Type = "v";
            } else {
                Type = "t";
            }

            Name = columnEntity.ColumnName;
            if (string.IsNullOrEmpty(columnEntity.Comment) == false) {
                Comment = columnEntity.Comment;
            }
            Items = new List<StructureItemModel>();
        }
    }

}
