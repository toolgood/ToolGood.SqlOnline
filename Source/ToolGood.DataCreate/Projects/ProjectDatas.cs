/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using ToolGood.ReadyGo3;
using ToolGood.SqlOnline.Datas;
using ToolGood.SqlOnline.Datas.Enums;

namespace ToolGood.DataCreate
{
    class ProjectDatas
    {
        public static void CreateData(SqlHelper helper)
        {
            helper.Insert(new DbSqlCodeGen() {
                AddingAdminId = 0,
                AddingTime = DateTime.Now,
                ModifyAdminId = 0,
                ModifyTime = DateTime.Now,
                Language = "cs",
                Title = "实体类生成器",
                TplType = 1,
                Namespace= "ToolGood.SqlOnline",
                Content = @"using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace {{=it.namespace }}.Datas
{
    /// <summary>
    /// 描述：{{! it.comment }} 实体类
    /// 对应表：{{! it.tableName }}
    /// Copyright (C) {{= it.operationTime.split('-')[0] }} your siteweb 版权所有。
    /// 创建标识：{{= it.adminName}} {{= it.operationTime }}
    /// </summary>
    public class {{= standardName(it.tableName,true) }}Info
    {
    {{ for(var j=0;j<it.columns.length;j++) { var col=it.columns[j]; }}
        /// <summary>
        /// {{! col.comment }} 
        /// </summary>
        {{ if(/int/i.test(col.type)){ }}
        public int{{= (col.isNullAble?'?':'')}} {{= col.columnName }} { get; set; }
        {{ }else if(/n?(var)?char|text/i.test(col.type)){ }}
        public string {{= col.columnName }} { get; set; }
        {{ }else if(/(date|time)/i.test(col.type)){ }}
        public DateTime{{= (col.isNullAble?'?':'')}} {{= col.columnName }} { get; set; }
        {{ }else if(/decimal/i.test(col.type)){ }}
        public decimal{{= (col.isNullAble?'?':'')}} {{= col.columnName }} { get; set; }
        {{ }else if(/bit/i.test(col.type)){ }}
        public bool{{= (col.isNullAble?'?':'')}} {{= col.columnName }} { get; set; }
        {{ }else { }}
        public {{= col.type }}{{= (col.isNullAble?'?':'')}} {{= col.columnName }} { get; set; }
        {{ } }}
    {{ } }}
    }
}"
            });
            helper.Insert(new DbSqlCodeGen() {
                AddingAdminId = 0,
                AddingTime = DateTime.Now,
                ModifyAdminId = 0,
                ModifyTime = DateTime.Now,
                Language = "cs",
                Title = "仓储接口生成器",
                TplType = 1,
                Namespace = "ToolGood.SqlOnline",
                Content = @"using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace {{=it.namespace }}.IRepository
{
    /// <summary>
    /// 描述：{{! it.comment }} 接口类
    /// 对应表：{{! it.tableName }}
    /// Copyright (C) {{= it.operationTime.split('-')[0] }} your siteweb 版权所有。
    /// 创建标识：{{= it.adminName}} {{= it.operationTime }}
    /// </summary>
    public interface I{{= standardName(it.tableName,true) }}Repository
    {
 
    }
}"
            });
            helper.Insert(new DbSqlCodeGen() {
                AddingAdminId = 0,
                AddingTime = DateTime.Now,
                ModifyAdminId = 0,
                ModifyTime = DateTime.Now,
                Language = "cs",
                Title = "仓储类生成器",
                TplType = 1,
                Namespace = "ToolGood.SqlOnline",
                Content = @"using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace {{=it.namespace }}.Repository
{
    /// <summary>
    /// 描述：{{! it.comment }} 接口类
    /// 对应表：{{! it.tableName }}
    /// Copyright (C) {{= it.operationTime.split('-')[0] }} your siteweb 版权所有。
    /// 创建标识：{{= it.adminName}} {{= it.operationTime }}
    /// </summary>
    public class {{= standardName(it.tableName,true) }}Repository 
        : BaseRepository, I{{= standardName(it.tableName,true) }}Repository
    {
 
    }
}"
            });

            helper.Insert(new DbSqlCodeGen() {
                AddingAdminId = 0,
                AddingTime = DateTime.Now,
                ModifyAdminId = 0,
                ModifyTime = DateTime.Now,
                Language = "java",
                Title = "实体类生成器",
                TplType = 1,
                Namespace = "toolgood.sqlOnline",
                Content = @"package {{=it.namespace }}.Datas;

import java.util.Date;

/**
 * 描述：{{! it.comment }} 实体类
 * 对应表：{{! it.tableName }}
 * Copyright (C) {{= it.operationTime.split('-')[0] }} your siteweb 版权所有。
 * 创建标识：{{= it.adminName}} {{= it.operationTime }}
 */
public class KeywordItem {
{{ for(var j=0;j<it.columns.length;j++) { var col=it.columns[j]; }}
    /**
     * {{! col.comment }} 
     */
    {{ if(col.isNullAble){ }}
        {{ if(/int/i.test(col.type)){ }}
    public Integer {{= col.columnName }};
        {{ }else if(/n?(var)?char|text/i.test(col.type)){ }}
    public String {{= col.columnName }};
        {{ }else if(/(date|time)/i.test(col.type)){ }}
    public Date {{= col.columnName }};
        {{ }else if(/decimal/i.test(col.type)){ }}
    public Float {{= col.columnName }};
        {{ }else if(/bit/i.test(col.type)){ }}
    public Boolean {{= col.columnName }};
        {{ }else { }}
    public {{= col.type }} {{= col.columnName }};
        {{ } }}
    {{ } else { }}
        {{ if(/int/i.test(col.type)){ }}
    public int {{= col.columnName }};
        {{ }else if(/n?(var)?char|text/i.test(col.type)){ }}
    public String {{= col.columnName }};
        {{ }else if(/(date|time)/i.test(col.type)){ }}
    public Date {{= col.columnName }};
        {{ }else if(/decimal/i.test(col.type)){ }}
    public float {{= col.columnName }};
        {{ }else if(/bit/i.test(col.type)){ }}
    public boolean {{= col.columnName }};
        {{ }else { }}
    public {{= col.type }} {{= col.columnName }};
        {{ } }}
    {{ } }}
{{ } }}
}"
            });

            helper.Insert(new DbSqlQueryLogSetting() {
                LogType = 1,
                Name = "默认使用SQLite保存",
                Data = "",
                AddingAdminId = 1,
                AddingTime = System.DateTime.Now,
                ModifyAdminId = 1,
                ModifyTime = System.DateTime.Now,
            });

              

        }

    }
}
