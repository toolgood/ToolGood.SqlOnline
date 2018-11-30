using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ToolGood.PluginBase;

namespace ToolGood.TransDto
{

    public class DatabaseInfoDto
    {
        /// 数据库名
        /// </summary>
        [JsonProperty("serverName")]
        public string ServerName { get; set; }

        /// 数据库名
        /// </summary>
        [JsonProperty("databaseName")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// 表格
        /// </summary>
        [JsonProperty("tables", NullValueHandling = NullValueHandling.Include)]
        public List<TableInfoDto> Tables { get; set; }


    }

    public class TableInfoDto
    {
        /// <summary>
        /// 数据库名
        /// </summary>
        [JsonProperty("databaseName", NullValueHandling = NullValueHandling.Include)]
        public string DatabaseName { get; set; }

        /// <summary>
        /// 模式名;
        /// </summary>
        [JsonProperty("schemaName", NullValueHandling = NullValueHandling.Include)]
        public string SchemaName { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        [JsonProperty("tableName")]
        public string TableName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("useSchema")]
        public bool UseSchema { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("comment", NullValueHandling = NullValueHandling.Include)]
        public string Comment { get; set; }


        [JsonProperty("columns", NullValueHandling = NullValueHandling.Include)]
        public List<ColumnInfoDto> Columns { get; set; }

        public TableInfoDto() { }
        public TableInfoDto(SqlTableInfo tableInfo)
        {
            DatabaseName = tableInfo.DatabaseName;
            SchemaName = tableInfo.SchemaName;
            TableName = tableInfo.TableName;
            UseSchema = tableInfo.UseSchema;
            Comment = tableInfo.Comment;
            if (tableInfo.Columns!=null && tableInfo.Columns.Count>0) {
                Columns = new List<ColumnInfoDto>();

                foreach (var item in tableInfo.Columns) {
                    ColumnInfoDto dto = new ColumnInfoDto(item);
                    Columns.Add(dto);
                }
            }
        }

    }

    public class ColumnInfoDto
    {
        /// <summary>
        /// 列名
        /// </summary>
        [JsonProperty("columnName")]
        public string ColumnName { get; set; }

        /// <summary>
        /// 属性名
        /// </summary>
        [JsonProperty("columnType", NullValueHandling = NullValueHandling.Include)]
        public string ColumnType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("comment", NullValueHandling = NullValueHandling.Include)]
        public string Comment { get; set; }

        /// <summary>
        /// 允许空
        /// </summary>
        [JsonProperty("allowNull", NullValueHandling = NullValueHandling.Include)]
        public bool? AllowNull { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [JsonProperty("defaultString", NullValueHandling = NullValueHandling.Include)]
        public string DefaultString { get; set; }

        public ColumnInfoDto() { }
        public ColumnInfoDto(SqlColumnInfo columnInfo)
        {
            ColumnName = columnInfo.ColumnName;
            ColumnType = columnInfo.ColumnType;
            Comment = columnInfo.Comment;
            AllowNull = columnInfo.AllowNull;
            DefaultString = columnInfo.DefaultString;
        }

    }
}
