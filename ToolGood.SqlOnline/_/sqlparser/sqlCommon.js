var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var EnumSqlType;
(function (EnumSqlType) {
    EnumSqlType[EnumSqlType["SqlServer"] = 1] = "SqlServer";
    EnumSqlType[EnumSqlType["MySql"] = 2] = "MySql";
})(EnumSqlType || (EnumSqlType = {}));
var EnumSqlToken;
(function (EnumSqlToken) {
    EnumSqlToken[EnumSqlToken["Unknow"] = 0] = "Unknow";
    EnumSqlToken[EnumSqlToken["Keyword"] = 1] = "Keyword";
    EnumSqlToken[EnumSqlToken["Number"] = 2] = "Number";
    EnumSqlToken[EnumSqlToken["String"] = 3] = "String";
    EnumSqlToken[EnumSqlToken["Params"] = 4] = "Params";
    EnumSqlToken[EnumSqlToken["Operator"] = 5] = "Operator";
    EnumSqlToken[EnumSqlToken["Commas"] = 6] = "Commas";
    EnumSqlToken[EnumSqlToken["LParen"] = 7] = "LParen";
    EnumSqlToken[EnumSqlToken["RParen"] = 8] = "RParen";
    EnumSqlToken[EnumSqlToken["LBrace"] = 9] = "LBrace";
    EnumSqlToken[EnumSqlToken["RBrace"] = 10] = "RBrace";
    EnumSqlToken[EnumSqlToken["Semi"] = 11] = "Semi";
    EnumSqlToken[EnumSqlToken["Comment"] = 12] = "Comment";
    EnumSqlToken[EnumSqlToken["Block"] = 13] = "Block";
    EnumSqlToken[EnumSqlToken["Blank"] = 14] = "Blank";
    EnumSqlToken[EnumSqlToken["Dot"] = 15] = "Dot";
})(EnumSqlToken || (EnumSqlToken = {}));
var EnumSqlPosition;
(function (EnumSqlPosition) {
    EnumSqlPosition[EnumSqlPosition["Root"] = 0] = "Root";
    EnumSqlPosition[EnumSqlPosition["Comment"] = 1] = "Comment";
    EnumSqlPosition[EnumSqlPosition["SelectFirstColumn"] = 2] = "SelectFirstColumn";
    EnumSqlPosition[EnumSqlPosition["SelectColumn"] = 3] = "SelectColumn";
    EnumSqlPosition[EnumSqlPosition["SubSelect"] = 4] = "SubSelect";
    EnumSqlPosition[EnumSqlPosition["From"] = 5] = "From";
    EnumSqlPosition[EnumSqlPosition["Join"] = 6] = "Join";
    EnumSqlPosition[EnumSqlPosition["JoinCondition"] = 7] = "JoinCondition";
    EnumSqlPosition[EnumSqlPosition["WhereCondition"] = 8] = "WhereCondition";
    EnumSqlPosition[EnumSqlPosition["Group"] = 9] = "Group";
    EnumSqlPosition[EnumSqlPosition["GroupBy"] = 10] = "GroupBy";
    EnumSqlPosition[EnumSqlPosition["Order"] = 11] = "Order";
    EnumSqlPosition[EnumSqlPosition["OrderBy"] = 12] = "OrderBy";
    EnumSqlPosition[EnumSqlPosition["Limit"] = 13] = "Limit";
    EnumSqlPosition[EnumSqlPosition["UpdateTable"] = 14] = "UpdateTable";
    EnumSqlPosition[EnumSqlPosition["UpdateSet"] = 15] = "UpdateSet";
    EnumSqlPosition[EnumSqlPosition["UpdateFrom"] = 16] = "UpdateFrom";
    EnumSqlPosition[EnumSqlPosition["UpdateJoin"] = 17] = "UpdateJoin";
    EnumSqlPosition[EnumSqlPosition["UpdateWhereCondition"] = 18] = "UpdateWhereCondition";
    EnumSqlPosition[EnumSqlPosition["DeleteTable"] = 19] = "DeleteTable";
    EnumSqlPosition[EnumSqlPosition["DeleteJoin"] = 20] = "DeleteJoin";
    EnumSqlPosition[EnumSqlPosition["DeleteWhereCondition"] = 21] = "DeleteWhereCondition";
    EnumSqlPosition[EnumSqlPosition["Insert"] = 22] = "Insert";
    EnumSqlPosition[EnumSqlPosition["InsertTable"] = 23] = "InsertTable";
    EnumSqlPosition[EnumSqlPosition["InsertColumn"] = 24] = "InsertColumn";
    EnumSqlPosition[EnumSqlPosition["InsertValues"] = 25] = "InsertValues";
})(EnumSqlPosition || (EnumSqlPosition = {}));
var SqlNode = /** @class */ (function () {
    function SqlNode() {
        this.Nodes = new Array();
    }
    SqlNode.prototype.SetValue1 = function (c, start, token) {
        this.Index = start;
        this.Length = 1;
        this.Sql = c;
        this.Token = token;
        this.Keyword = this.Sql.toUpperCase();
        return this;
    };
    ;
    SqlNode.prototype.SetValue2 = function (sql, start, end, token) {
        this.Index = start;
        this.Length = end - start;
        this.Sql = sql.substr(start, this.Length);
        this.Token = token;
        this.Keyword = this.Sql.toUpperCase();
        return this;
    };
    ;
    SqlNode.prototype.SetValue3 = function (sql, start, end, token, nodes) {
        this.Index = start;
        this.Length = end - start;
        this.Sql = sql.substr(start, this.Length);
        this.Token = token;
        this.Nodes = nodes;
        this.Keyword = this.Sql.toUpperCase();
        return this;
    };
    ;
    return SqlNode;
}());
var SqlSplitBase = /** @class */ (function () {
    function SqlSplitBase() {
    }
    SqlSplitBase.prototype.SplitSql = function (sql) {
        sql = sql.replace(/\r\n/g, "\n").replace(/\r/g, "\n").replace(/[\t\f\v]/g, " ").trim() + "\n";
        var root = new SqlNode();
        this.Split(root, root.Nodes, sql, 0, 0);
        return root.Nodes;
    };
    SqlSplitBase.prototype.MergeSql = function (nodes) {
        var list = new Array();
        var sqlNodes = new Array();
        list.push(sqlNodes);
        var jumpNextSelect = false;
        var jumpNextSelect_Insert = false;
        for (var i = 0; i < nodes.length; i++) {
            var node = nodes[i];
            if (node.Token == EnumSqlToken.Semi) {
                jumpNextSelect = false;
                jumpNextSelect_Insert = false;
                sqlNodes.push(node);
                sqlNodes = new Array();
                list.push(sqlNodes);
            }
            else if (sqlNodes.length == 0) {
                sqlNodes.push(node);
            }
            else if (node.Keyword == "INSERT") {
                jumpNextSelect_Insert = true;
                jumpNextSelect = false;
                sqlNodes = new Array();
                sqlNodes.push(node);
                list.push(sqlNodes);
            }
            else if (node.Keyword == "UNION" || node.Keyword == "INTERSECT" || node.Keyword == "EXCEPT") {
                jumpNextSelect_Insert = false;
                jumpNextSelect = true;
                sqlNodes.push(node);
            }
            else if (jumpNextSelect_Insert && node.Keyword == "VALUES") {
                jumpNextSelect_Insert = false;
                jumpNextSelect = false;
                sqlNodes.push(node);
            }
            else if (jumpNextSelect && node.Token == EnumSqlToken.Block) {
                jumpNextSelect_Insert = false;
                jumpNextSelect = false;
                sqlNodes.push(node);
            }
            else if ((jumpNextSelect_Insert || jumpNextSelect) && node.Keyword == "SELECT") {
                jumpNextSelect_Insert = false;
                jumpNextSelect = false;
                sqlNodes.push(node);
            }
            else if (this.IsFirstKeyword(node.Keyword)) {
                jumpNextSelect_Insert = false;
                jumpNextSelect = false;
                sqlNodes = new Array();
                sqlNodes.push(node);
                list.push(sqlNodes);
            }
            else {
                sqlNodes.push(node);
            }
        }
        return list;
    };
    SqlSplitBase.prototype.GetIndexByPosition = function (position, sql) {
        sql = sql.replace(/\r\n/g, "\n").replace(/\r/g, "\n").replace(/[\t\f\v]/g, " ").trim() + "\n";
        var col = position.column - 1;
        var line = position.lineNumber - 1;
        if (line == 0) {
            return col;
        }
        var index = 0, c = 0, l = 0;
        while (index < sql.length) {
            var ch = sql[index];
            if (col == c && l == line) {
                return index;
            }
            if (ch == "\n") {
                l++, c = 0;
            }
            else {
                c++;
            }
            index++;
        }
    };
    SqlSplitBase.prototype.GetCurrentSql = function (position, sql) {
        var index = this.GetIndexByPosition(position, sql);
        var nodes = this.SplitSql(sql);
        var lines = this.MergeSql(nodes);
        for (var i = lines.length - 1; i >= 0; i--) {
            var item = lines[i];
            if (item[0].Index < index) {
                return item;
            }
        }
        return lines[0];
    };
    SqlSplitBase.prototype.IsFirstKeyword = function (key) {
        var keys = ["SELECT", "INSERT", "UPDATE", "DELETE", "CREATE", "ALERT", "DROP", "USE", "TRUNCATE"];
        for (var i = 0; i < keys.length; i++) {
            if (keys[i] == key) {
                return true;
            }
        }
        return false;
    };
    SqlSplitBase.prototype.Split = function (root, nodes, sql, layer, index) {
        while (index < sql.length) {
            var start = index;
            var ch = sql[index++];
            var arr = this.customSplit(root, root.Nodes, ch, sql, 0, 0);
            if (arr[0]) {
                index = arr[1];
                continue;
            }
            if (ch == ';') {
                if (layer > 0) {
                    return;
                }
                nodes.push(new SqlNode().SetValue1(ch, start, EnumSqlToken.Semi));
            }
            else if (ch == '.') {
                nodes.push(new SqlNode().SetValue1(ch, start, EnumSqlToken.Dot));
            }
            else if (ch == ',') {
                nodes.push(new SqlNode().SetValue1(ch, start, EnumSqlToken.Commas));
            }
            else if (ch == ' ' || ch == '\n') {
                index = this.ReadBlankSpace(sql, index);
                nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.Blank));
            }
            else if (ch == '(' || ch == '{') {
                var list = new Array();
                list.push(new SqlNode().SetValue1(ch, start, ch == '(' ? EnumSqlToken.LParen : EnumSqlToken.LBrace));
                this.Split(root, list, sql, layer + 1, index);
                var lastNode = list[list.length - 1];
                index = lastNode.Index + lastNode.Length;
                nodes.push(new SqlNode().SetValue3(sql, start, index, EnumSqlToken.Block, list));
            }
            else if (ch == ')' || ch == '}') {
                nodes.push(new SqlNode().SetValue1(ch, start, ch == ')' ? EnumSqlToken.RParen : EnumSqlToken.RBrace));
                if (layer > 0) {
                    return;
                }
            }
            else if (ch == '[') {
                index = this.ReadToEnd(sql, index, "]\n") + 1;
                nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.Keyword));
            }
            else if (ch == '\'') {
                index = this.ReadToEnd2(sql, index + 1, "\n\'", "\\'") + 1;
                nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.String));
            }
            else if (ch == '"') {
                index = this.ReadToEnd2(sql, index + 1, "\n\"", "\\\"") + 1;
                nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.String));
            }
            else {
                var hasNext = index < sql.length;
                if (hasNext && ch == '-' && sql[index] == '-') {
                    index = this.ReadComment(sql, index + 1);
                    nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.Comment));
                }
                else if (hasNext && ch == '/' && sql[index] == '*') {
                    index = this.ReadMultiComment(sql, index + 1);
                    nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.Comment));
                }
                else if (hasNext && "XNxn".indexOf(ch) > -1 && sql[index] == '\'') {
                    index = this.ReadToEnd2(sql, index, "\n\'", "\\'");
                    nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.String));
                }
                else if (hasNext && "XNxn".indexOf(ch) > -1 && sql[index] == '\"') {
                    index = this.ReadToEnd2(sql, index, "\n\"", "\\\"");
                    nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.String));
                }
                else if ("<>=?+-*/%:!&~|#.".indexOf(ch) > -1) {
                    nodes.push(new SqlNode().SetValue1(ch, start, EnumSqlToken.Operator));
                }
                else if (ch == '@') {
                    index = this.ReadToEnd(sql, index, "<>=?+-*/%:!&~|#(){}[].\n;,@'\" ");
                    nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.Params));
                }
                else {
                    index = this.ReadToEnd(sql, index, "<>=?+-*/%:!&~|#(){}[].\n;,@'\" ");
                    var t = sql.substr(start, index - start);
                    var token = EnumSqlToken.Keyword;
                    if (/^-?([0](\.\d*)?|[123456789]\d*(\.\d*)?)$/.test(t)) {
                        token = EnumSqlToken.Number;
                    }
                    nodes.push(new SqlNode().SetValue2(sql, start, index, token));
                }
            }
        }
    };
    SqlSplitBase.prototype.customSplit = function (root, nodes, ch, sql, layer, index) {
        var outInfo;
        outInfo = [false, index];
        return outInfo;
    };
    SqlSplitBase.prototype.ReadBlankSpace = function (sql, start) {
        for (var i = start; i < sql.length; i++) {
            var ch = sql[i];
            if (" " != ch && "\n" != ch) {
                return i;
            }
        }
        return sql.length;
    };
    SqlSplitBase.prototype.ReadMultiComment = function (sql, start) {
        for (var i = start; i < sql.length; i++) {
            if (sql[i] == '*' && i + 1 < sql.length && sql[i + 1] == '/') {
                return i + 2;
            }
        }
        return sql.length;
    };
    SqlSplitBase.prototype.ReadComment = function (sql, start) {
        for (var i = start; i < sql.length; i++) {
            if (sql[i] == '\n') {
                return i;
            }
        }
        return sql.length;
    };
    SqlSplitBase.prototype.ReadToEnd = function (sql, start, end) {
        for (var i = start; i < sql.length; i++) {
            if (end.indexOf(sql[i]) > -1) {
                return i;
            }
        }
        return sql.length;
    };
    SqlSplitBase.prototype.ReadToEnd2 = function (sql, start, end, skip) {
        while (start < sql.length) {
            var ch = sql[start];
            if (ch == skip[0]) {
                var isRetrun = true;
                for (var i = 1; i < skip.length; i++) {
                    if (sql[start + i] != skip[i]) {
                        isRetrun = false;
                        break;
                    }
                }
                if (isRetrun) {
                    start += skip.length;
                    continue;
                }
            }
            if (end.indexOf(ch) > -1) {
                return start;
            }
            start++;
        }
        return sql.length;
    };
    return SqlSplitBase;
}());
var SqlServerSplit = /** @class */ (function (_super) {
    __extends(SqlServerSplit, _super);
    function SqlServerSplit() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    SqlServerSplit.prototype.customSplit = function (root, nodes, ch, sql, layer, index) {
        var outInfo;
        outInfo = [false, index];
        return outInfo;
    };
    return SqlServerSplit;
}(SqlSplitBase));
var MySqlSplit = /** @class */ (function (_super) {
    __extends(MySqlSplit, _super);
    function MySqlSplit() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return MySqlSplit;
}(SqlSplitBase));
var BaseStatement = /** @class */ (function () {
    function BaseStatement() {
    }
    return BaseStatement;
}());
var CommentStatement = /** @class */ (function (_super) {
    __extends(CommentStatement, _super);
    function CommentStatement() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return CommentStatement;
}(BaseStatement));
var SelectStatement = /** @class */ (function (_super) {
    __extends(SelectStatement, _super);
    function SelectStatement() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return SelectStatement;
}(BaseStatement));
var UpdateStatement = /** @class */ (function (_super) {
    __extends(UpdateStatement, _super);
    function UpdateStatement() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return UpdateStatement;
}(BaseStatement));
var InsertStatement = /** @class */ (function (_super) {
    __extends(InsertStatement, _super);
    function InsertStatement() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return InsertStatement;
}(BaseStatement));
var DeleteStatement = /** @class */ (function (_super) {
    __extends(DeleteStatement, _super);
    function DeleteStatement() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return DeleteStatement;
}(BaseStatement));
var OtherStatement = /** @class */ (function (_super) {
    __extends(OtherStatement, _super);
    function OtherStatement() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return OtherStatement;
}(BaseStatement));
var FromStatement = /** @class */ (function (_super) {
    __extends(FromStatement, _super);
    function FromStatement() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return FromStatement;
}(BaseStatement));
var JoinStatement = /** @class */ (function (_super) {
    __extends(JoinStatement, _super);
    function JoinStatement() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return JoinStatement;
}(BaseStatement));
var WhereStatement = /** @class */ (function (_super) {
    __extends(WhereStatement, _super);
    function WhereStatement() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return WhereStatement;
}(BaseStatement));
var GroupByStatement = /** @class */ (function (_super) {
    __extends(GroupByStatement, _super);
    function GroupByStatement() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return GroupByStatement;
}(BaseStatement));
var HavingStatement = /** @class */ (function (_super) {
    __extends(HavingStatement, _super);
    function HavingStatement() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return HavingStatement;
}(BaseStatement));
var OrderByStatement = /** @class */ (function (_super) {
    __extends(OrderByStatement, _super);
    function OrderByStatement() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return OrderByStatement;
}(BaseStatement));
//# sourceMappingURL=sqlCommon.js.map