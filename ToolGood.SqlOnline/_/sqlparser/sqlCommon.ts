enum EnumSqlType {
    SqlServer = 1,
    MySql
}
enum EnumSqlToken {
    Unknow = 0,
    Keyword,
    Number,
    String,
    Params,
    Operator,
    Commas,
    LParen,
    RParen,
    LBrace,
    RBrace,
    Semi,
    Comment,
    Block,
    Blank,
    Dot,
}
enum EnumSqlPosition {
    Root = 0,
    Comment,
    SelectFirstColumn,
    SelectColumn,
    SubSelect,
    From,
    Join,
    JoinCondition,
    WhereCondition,
    Group,
    GroupBy,
    Order,
    OrderBy,
    Limit,

    UpdateTable,
    UpdateSet,
    UpdateFrom,
    UpdateJoin,
    UpdateWhereCondition,

    DeleteTable,
    DeleteJoin,
    DeleteWhereCondition,

    Insert,
    InsertTable,
    InsertColumn,
    InsertValues,
}

class SqlNode {
    Sql: string;
    Index: number;
    Length: number;
    Token: EnumSqlToken;
    Nodes: Array<SqlNode>;
    Keyword: string;

    constructor() {
        this.Nodes = new Array<SqlNode>();
    }

    SetValue1(c: string, start: number, token: EnumSqlToken) {
        this.Index = start;
        this.Length = 1;
        this.Sql = c;
        this.Token = token;
        this.Keyword = this.Sql.toUpperCase();
        return this;
    };
    SetValue2(sql: string, start: number, end: number, token: EnumSqlToken) {
        this.Index = start;
        this.Length = end - start;
        this.Sql = sql.substr(start, this.Length);
        this.Token = token;
        this.Keyword = this.Sql.toUpperCase();
        return this;
    };
    SetValue3(sql: string, start: number, end: number, token: EnumSqlToken, nodes: Array<SqlNode>) {
        this.Index = start;
        this.Length = end - start;
        this.Sql = sql.substr(start, this.Length);
        this.Token = token;
        this.Nodes = nodes;
        this.Keyword = this.Sql.toUpperCase();
        return this;
    };
}
class SqlSplitBase {
    public SplitSql(sql: string) {
        sql = sql.replace(/\r\n/g, "\n").replace(/\r/g, "\n").replace(/[\t\f\v]/g, " ").trim() + "\n";
        let root = new SqlNode();
        this.Split(root, root.Nodes, sql, 0, 0);
        return root.Nodes;
    }
    public MergeSql(nodes: Array<SqlNode>) {
        var list = new Array<Array<SqlNode>>();
        var sqlNodes = new Array<SqlNode>();
        list.push(sqlNodes);

        var jumpNextSelect = false;
        var jumpNextSelect_Insert = false;
        for (var i = 0; i < nodes.length; i++) {
            var node = nodes[i];
            if (node.Token == EnumSqlToken.Semi) {
                jumpNextSelect = false;
                jumpNextSelect_Insert = false;
                sqlNodes.push(node);
                sqlNodes = new Array<SqlNode>();
                list.push(sqlNodes);
            } else if (sqlNodes.length == 0) {
                sqlNodes.push(node);
            } else if (node.Keyword == "INSERT") {
                jumpNextSelect_Insert = true
                jumpNextSelect = false;
                sqlNodes = new Array<SqlNode>();
                sqlNodes.push(node);
                list.push(sqlNodes);
            } else if (node.Keyword == "UNION" || node.Keyword == "INTERSECT" || node.Keyword == "EXCEPT") {
                jumpNextSelect_Insert = false
                jumpNextSelect = true;
                sqlNodes.push(node);
            } else if (jumpNextSelect_Insert && node.Keyword == "VALUES") {
                jumpNextSelect_Insert = false
                jumpNextSelect = false;
                sqlNodes.push(node);
            } else if (jumpNextSelect && node.Token == EnumSqlToken.Block) {
                jumpNextSelect_Insert = false
                jumpNextSelect = false;
                sqlNodes.push(node);
            } else if ((jumpNextSelect_Insert || jumpNextSelect) && node.Keyword == "SELECT") {
                jumpNextSelect_Insert = false
                jumpNextSelect = false;
                sqlNodes.push(node);
            } else if (this.IsFirstKeyword(node.Keyword)) {
                jumpNextSelect_Insert = false;
                jumpNextSelect = false;
                sqlNodes = new Array<SqlNode>();
                sqlNodes.push(node);
                list.push(sqlNodes);
            } else {
                sqlNodes.push(node);
            }
        }
        return list;
    }
    public GetIndexByPosition(position, sql) {
        sql = sql.replace(/\r\n/g, "\n").replace(/\r/g, "\n").replace(/[\t\f\v]/g, " ").trim() + "\n";
        var col = position.column - 1;
        var line = position.lineNumber - 1;
        if (line == 0) { return col; }

        var index = 0, c = 0, l = 0;
        while (index < sql.length) {
            var ch = sql[index];
            if (col == c && l == line) { return index; }
            if (ch == "\n") { l++ , c = 0; } else { c++; }
            index++;
        }
    }
    public GetCurrentSql(position, sql) {
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
    }



    protected IsFirstKeyword(key: string) {
        let keys = ["SELECT", "INSERT", "UPDATE", "DELETE", "CREATE", "ALERT", "DROP", "USE", "TRUNCATE"];
        for (var i = 0; i < keys.length; i++) {
            if (keys[i] == key) {
                return true;
            }
        }
        return false;
    }
    protected Split(root: SqlNode, nodes: Array<SqlNode>, sql: string, layer: number, index: number) {
        while (index < sql.length) {
            var start = index;
            var ch = sql[index++];
            var arr = this.customSplit(root, root.Nodes, ch, sql, 0, 0);
            if (arr[0]) {
                index = arr[1];
                continue;
            }
            if (ch == ';') {
                if (layer > 0) { return; }
                nodes.push(new SqlNode().SetValue1(ch, start, EnumSqlToken.Semi));
            } else if (ch == '.') {
                nodes.push(new SqlNode().SetValue1(ch, start, EnumSqlToken.Dot));
            } else if (ch == ',') {
                nodes.push(new SqlNode().SetValue1(ch, start, EnumSqlToken.Commas));
            } else if (ch == ' ' || ch == '\n') {
                index = this.ReadBlankSpace(sql, index);
                nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.Blank));
            } else if (ch == '(' || ch == '{') {
                var list = new Array<SqlNode>();
                list.push(new SqlNode().SetValue1(ch, start, ch == '(' ? EnumSqlToken.LParen : EnumSqlToken.LBrace));
                this.Split(root, list, sql, layer + 1, index);
                var lastNode = list[list.length - 1];
                index = lastNode.Index + lastNode.Length;
                nodes.push(new SqlNode().SetValue3(sql, start, index, EnumSqlToken.Block, list));
            } else if (ch == ')' || ch == '}') {
                nodes.push(new SqlNode().SetValue1(ch, start, ch == ')' ? EnumSqlToken.RParen : EnumSqlToken.RBrace));
                if (layer > 0) { return; }
            } else if (ch == '[') {
                index = this.ReadToEnd(sql, index, "]\n") + 1;
                nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.Keyword));
            } else if (ch == '\'') {
                index = this.ReadToEnd2(sql, index + 1, "\n\'", "\\'") + 1;
                nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.String));
            } else if (ch == '"') {
                index = this.ReadToEnd2(sql, index + 1, "\n\"", "\\\"") + 1;
                nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.String));
            } else {
                var hasNext = index < sql.length;
                if (hasNext && ch == '-' && sql[index] == '-') {
                    index = this.ReadComment(sql, index + 1);
                    nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.Comment));
                } else if (hasNext && ch == '/' && sql[index] == '*') {
                    index = this.ReadMultiComment(sql, index + 1);
                    nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.Comment));
                } else if (hasNext && "XNxn".indexOf(ch) > -1 && sql[index] == '\'') {
                    index = this.ReadToEnd2(sql, index, "\n\'", "\\'");
                    nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.String));
                } else if (hasNext && "XNxn".indexOf(ch) > -1 && sql[index] == '\"') {
                    index = this.ReadToEnd2(sql, index, "\n\"", "\\\"");
                    nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.String));
                } else if ("<>=?+-*/%:!&~|#.".indexOf(ch) > -1) {
                    nodes.push(new SqlNode().SetValue1(ch, start, EnumSqlToken.Operator));
                } else if (ch == '@') {
                    index = this.ReadToEnd(sql, index, "<>=?+-*/%:!&~|#(){}[].\n;,@'\" ");
                    nodes.push(new SqlNode().SetValue2(sql, start, index, EnumSqlToken.Params));
                } else {
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
    }
    protected customSplit(root: SqlNode, nodes: Array<SqlNode>, ch: string, sql: string, layer: number, index: number) {
        let outInfo: [boolean, number];
        outInfo = [false, index];
        return outInfo;
    }

    protected ReadBlankSpace(sql: string, start: number) {
        for (var i = start; i < sql.length; i++) {
            var ch = sql[i];
            if (" " != ch && "\n" != ch) {
                return i;
            }
        }
        return sql.length;
    }
    protected ReadMultiComment(sql: string, start: number) {
        for (var i = start; i < sql.length; i++) {
            if (sql[i] == '*' && i + 1 < sql.length && sql[i + 1] == '/') {
                return i + 2;
            }
        }
        return sql.length;
    }
    protected ReadComment(sql: string, start: number) {
        for (var i = start; i < sql.length; i++) {
            if (sql[i] == '\n') { return i; }
        }
        return sql.length;
    }
    protected ReadToEnd(sql: string, start: number, end: string) {
        for (var i = start; i < sql.length; i++) {
            if (end.indexOf(sql[i]) > -1) { return i; }
        }
        return sql.length;
    }
    protected ReadToEnd2(sql: string, start: number, end: string, skip: string) {
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
            if (end.indexOf(ch) > -1) { return start; }
            start++;
        }
        return sql.length;
    }
}

class SqlServerSplit extends SqlSplitBase {
    protected customSplit(root: SqlNode, nodes: Array<SqlNode>, ch: string, sql: string, layer: number, index: number) {
        let outInfo: [boolean, number];
        outInfo = [false, index];
        return outInfo;
    }

}
class MySqlSplit extends SqlSplitBase {


}

class BaseStatement {
    public Index: number;
    public Length: number;
    protected _sql: string;
}
class CommentStatement extends BaseStatement {

}
class SelectStatement extends BaseStatement {

}
class UpdateStatement extends BaseStatement {

}
class InsertStatement extends BaseStatement {

}
class DeleteStatement extends BaseStatement {

}
class OtherStatement extends BaseStatement {

}
class FromStatement extends BaseStatement {

}
class JoinStatement extends BaseStatement {

}
class WhereStatement extends BaseStatement {

}
class GroupByStatement extends BaseStatement {

}
class HavingStatement extends BaseStatement {

}
class OrderByStatement extends BaseStatement {

}