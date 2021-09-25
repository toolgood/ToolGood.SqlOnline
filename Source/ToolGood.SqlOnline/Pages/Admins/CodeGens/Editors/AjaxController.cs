/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ToolGood.SqlOnline.Dtos.CodeGens;

namespace ToolGood.SqlOnline.Pages.Admins.CodeGens.Editors
{
    [Route("/admins/CodeGens/Editors/Ajax/{action}")]
    public class AjaxController : AdminApiController
    {

        [HttpGet()]
        public IActionResult GetTestTableData()
        {

            TableViewInfo tableInfo = new TableViewInfo() {
                AdminName = "管理员",
                OperationTime = DateTime.Now,
                Namespace = "ToolGood.SqlOnline",
                ServerName = "测试服务器",
                ServerType = "SqlServer",
                DatabaseName = "ToolGood",
                SchemaName = "dbo",
                TableName = "Member",
                Comment = "成员表",
                Columns = new List<TableColumnInfo>() {
                    new TableColumnInfo() {
                                Index=1,
                                ColumnName="Id",
                                Comment="主键自增",
                                IsPrimaryKey=true,
                                Type="bigint",
                               IsIdentity=true,
                    },
                    new TableColumnInfo() {
                                Index=2,
                                ColumnName="UserName",
                                Comment="用户名",
                                Type="varchar",
                                Length="20",
                            },
                    new TableColumnInfo() {
                                Index=3,
                                ColumnName="Password",
                                Comment="密码",
                                Type="char",
                                Length="32",
                            },
                    new TableColumnInfo() {
                                Index=4,
                                ColumnName="Email",
                                Comment="邮箱",
                                Type="char",
                                Length="100",
                            },
                    new TableColumnInfo() {
                                Index=5,
                                ColumnName="NickName",
                                Comment="昵称",
                                Type="nvarchar",
                                Length="50",
                            },
                    new TableColumnInfo() {
                                Index=6,
                                ColumnName="Phone",
                                Comment="手机号",
                                Type="varchar",
                                Length="20",
                            },
                    new TableColumnInfo() {
                                Index=7,
                                ColumnName="CreateTime",
                                Comment="创建日期",
                                Type="datetime",
                            },
                    new TableColumnInfo() {
                                Index=0,
                                ColumnName="IsDelete",
                                Comment="是否删除",
                                Type="bit",
                                DefaultValue="(0)"
                            },
                }

            };

            return Success(tableInfo, null);
        }

        [HttpGet()]
        public IActionResult GetTestProcedureData()
        {
            ProcedureInfo procedureInfo = new ProcedureInfo() {
                AdminName = "管理员",
                OperationTime = DateTime.Now,
                Namespace = "ToolGood.SqlOnline",
                ServerName = "测试服务器",
                ServerType = "SqlServer",
                DatabaseName = "ToolGood",
                SchemaName = "dbo",
                ProcedureName = "Pro_WriteLog",
                Comment = "成员表",
                Params = new List<ProcedureParameter>() {
                    new ProcedureParameter() {
                        Index=1,
                        ParamName="AdminName",
                        Type="nvarchar",
                        Length="200",
                    },
                    new ProcedureParameter() {
                        Index=2,
                        ParamName="AdminId",
                        Type="int",
                    },
                    new ProcedureParameter() {
                        Index=3,
                        ParamName="Log",
                        Type="nvarchar",
                        Length="2000",
                    },
                    new ProcedureParameter() {
                        Index=4,
                        ParamName="AddingTime",
                        Type="datatime",
                    }
                }
            };
            return Success(procedureInfo, null);
        }

    }
}
