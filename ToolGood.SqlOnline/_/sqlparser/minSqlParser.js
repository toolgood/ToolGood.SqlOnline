var Lexter = (function () {
    var Types = {
        UNKNOWN: 0,				/**<    未知的 */
        KEYWORD: 1,				/**<    关键字 */
        NUMBER: 2,				/**<    数  字 */
        STRING: 3,				/**<    字符串 */
        FUNCTION: 4,				/**<    函数名 */
        VARIABLE: 5,				/**<    变  量 */
        PARAMS: 6,                /**<    绑定值 */
        OPERATOR: 7,				/**<    运算符 */
        COMMAS: 8,				/**<    标  点 */
        MEMORY: 9,

        COMMENT: 99,       /**<    注  释 */
    }

    var Parser = function (query) {
        var tks = [], pre = Types.UNKNOWN;
        var tmp = '', cur = '', sub = '', nxt = '';

        for (var i = 0; i < query.length; i++) {
            cur = query.charAt(i);

            /* {{{ 注释 */
            if ('/' == cur && '*' == query.charAt(i + 1)) {
                tmp = '';
                i++;
                while (++i < query.length) {
                    sub = query.charAt(i);
                    nxt = query.charAt(i + 1);
                    if ('*' == sub && '/' == nxt) {
                        tmp = tmp.substr(0, tmp.length - 1);
                        i++;
                        break;
                    }
                    if (tmp === '') {
                        tmp += (sub + nxt);
                    } else {
                        tmp += nxt;
                    }
                }
                tks[tks.length] = {
                    'text': tmp.replace(/^[\*\s]+/, '').replace(/[\s\*]+$/, ''),
                    'type': Types.COMMENT,
                };
            }
            /* }}} */

            /* {{{ 字符串 */
            else if ("'" == cur || '"' == cur || '`' == cur) {
                tmp = '';
                while (i < query.length && cur != (sub = query.charAt(++i))) {
                    tmp += ("\\" == sub) ? query.charAt(++i) : sub;
                }
                tks[tks.length] = {
                    'text': tmp,
                    'type': ('`' == cur) ? Types.VARIABLE : Types.STRING,
                };
            }
            /* }}} */

            /* {{{ 绑定变量 */
            else if (':' == cur) {
                tmp = cur;
                while (i < query.length) {
                    sub = query.charAt(++i);
                    if (!(/^\w+$/i.test(sub))) {
                        break;
                    }
                    tmp += sub;
                }
                i--;
                tks[tks.length] = {
                    'text': tmp,
                    'type': Types.PARAMS,
                };
            }
            /* }}} */

            /* {{{ 函数名 */
            else if (/^[a-z_]+$/i.test(cur)) {
                tmp = cur;
                while (i < query.length) {
                    sub = query.charAt(++i);
                    if (!(/^[\w\.:]+$/i.test(sub))) {
                        break;
                    }
                    tmp += sub;
                }
                i--;
                tks[tks.length] = {
                    'text': tmp,
                    'type': '(' == sub ? Types.FUNCTION : Types.KEYWORD,
                };
            }
            /* }}} */

            /* {{{ 数字 */

            else if (('-' == cur && Types.VARIABLE != pre) || /\d+/.test(cur)) {
                tmp = cur;
                while (i < query.length) {
                    sub = query.charAt(++i);
                    if (!(/^[\d\.]+$/.test(sub))) {
                        break;
                    }
                    tmp += sub;
                }
                i--;

                if ("-" == tmp) {
                    tks[tks.length] = {
                        'text': '-',  /**<    类型转换 */
                        'type': Types.OPERATOR
                    };
                } else {
                    tks[tks.length] = {
                        'text': tmp - 0,  /**<    类型转换 */
                        'type': Types.NUMBER
                    };
                }
            }
            /* }}} */

            /* {{{ 标点 */
            else if (/^[\,;\(\)]+$/.test(cur)) {
                tks[tks.length] = {
                    'text': cur,
                    'type': Types.COMMAS,
                };
            }
            /* }}} */

            /* {{{ 运算符 */
            else if (/^(\+|\-|\*|\/|>|<|=|!)$/.test(cur)) {
                tmp = cur;
                while (i < query.length) {
                    sub = query.charAt(++i);
                    if (!(/^(\+|\*|\/|>|<|=|!)+$/.test(sub))) {
                        break;
                    }
                    tmp += sub;
                }
                i--;

                tks[tks.length] = {
                    'text': tmp,
                    'type': Types.OPERATOR,
                };
            }
            /* }}} */

            pre = (tks.length === 0) ? pre : tks[tks.length - 1].type;
        }

        return tks;
    }

    /* {{{ public construct() */
    var Lexter = function (query) {
        this.tokens = (query instanceof Array) ? query : Parser(query.toString());
        this.blocks = [];

        var express = 0;
        var calcmap = {
            "(": 1,
            ")": -1,
        };

        for (var i = 0; i < this.tokens.length; ++i) {
            var tks = this.tokens[i];
            if (tks.type == Types.COMMAS && undefined !== calcmap[tks.text]) {
                express += calcmap[tks.text];
            } else if (!express) {
                this.blocks[this.blocks.length] = i;
            }
        }
    }
    /* }}} */

    /* {{{ public getAll() */
    getAll = function () {
        return this.tokens;
    }
    /* }}} */

    /* {{{ public indexOf() */
    indexOf = function (who, off) {
        var pos = 0;
        var tks = null;

        try {
            var exp = new RegExp(who.text, 'i');
        } catch (e) {
            var exp = who.text.toLowerCase();
        }
        var off = (off == undefined) ? 0 : off + 1;

        // xxx: 这里可以用二分法
        for (var i = 0; i < this.blocks.length; ++i) {
            pos = this.blocks[i];
            if (pos < off) {
                continue;
            }

            tks = this.tokens[pos];
            if (who.type == tks.type && ((exp instanceof RegExp && exp.test(tks.text)) || exp == tks.text.toLowerCase())) {
                return this.blocks[i];
            }
        }

        return -1;
    }
    /* }}} */

    /*{{{ static vars()*/
    vars = function (idx, tokens, isString) {
        var res;
        if (isString) {
            var lexter = new Lexter(tokens);
            tokens = lexter.getAll();
        }
        if (!tokens[idx]) { return null; }
        switch (tokens[idx]["type"]) {
            case Types.OPERATOR:
                res = [tokens[idx - 1], tokens[idx + 1]];
                break;
            case Types.FUNCTION:
                res = [];
                var temp = [];
                var expr = 0;
                for (var i = idx + 1, count = tokens.length; i < count; i++) {
                    var tk = tokens[i];
                    if (tk["type"] != Types.COMMAS) {
                        temp.push(tk);
                        continue;
                    }
                    switch (tk["text"]) {
                        case "(":
                            if (expr > 0) {
                                temp.push(tk);
                            }
                            expr++;
                            break;
                        case ")":
                            if ((--expr) == 0) {
                                res.push(temp);
                                temp = [];
                                i = count;
                                break;
                            } else { temp.push(tk); }
                            break;
                        case ",":
                            if (expr == 1) {
                                res.push(temp);
                                temp = [];
                            } else { temp.push(tk); }
                            break;
                        default:
                            break;
                    }
                }
                break;
            default:
                res = null;
                break;
        }
        return res;
    }
    /*}}}*/

    /*{{{ static text()*/
    text = function (stack, comma) {
        var res = [];
        for (var idx in stack) {
            var token = stack[idx];
            if (!token || !token.type || token.text == null) {
                res.push(null);
            } else {
                switch (token.type) {
                    case Types.STRING:
                        res.push("'" + token.text + "'");
                        break;
                    case Types.VARIABLE:
                        res.push(token.text);
                        break;
                    default:
                        res.push(token.text);
                        break;
                }
            }
            if (comma) {
                res.push(comma);
            }
        }
        if (comma) { res.pop(); }
        return res;
    }
    /*}}}*/

    types = Types;
    create = function (query) {
        return new Lexter(query);
    }
})();

var Tool = (function () {
    /*{{{ RELATE*/
    RELATE = {
        "=": 1,
        ">": 2,
        ">=": 3,
        "<": 4,
        "<=": 5,
        "<>": 6,
        "!=": 6,
        "in": 7,
        "not in": 8,
        "like": 9,
        "not like": 10,
        "between": 11,
        "is null": 20,
        "not null": 21,
    };
    /*}}}*/

    /*{{{ ORDER*/
    ORDER = {
        'ASC': 1,
        'DESC': 2
    }
    /*}}}*/

    /*{{{ JOIN*/
    JOIN = {
        'INNER JOIN': 1,
        'OUTER JOIN': 2,
        'LEFT JOIN': 3,
        'RIGHT JOIN': 4,
    }
    /*}}}*/

    /*{{{ removeParenthese()*/
    /**
     * 过滤最外面的括号对
     * @param {Array} tokens
     * @return {Array}
     */
    removeParenthese = function (tokens) {
        if (tokens[0] === undefined || tokens[0].text !== "(") {
            return tokens;
        }
        var lev = 0;
        for (var i = 0; i < tokens.length; i++) {
            if (tokens[i].text === "(") { lev++; }
            if (tokens[i].text === ")") { lev--; }
        }
        if (lev === 0 && tokens[tokens.length - 1].text === ")") {
            tokens.pop();
            tokens.shift();
            return removeParenthese(tokens);
        }
        throw new Error("lack parenthese");
    }
    /*}}}*/

    /*{{{ merge()*/
    /**
     * 拼合一组token的text字段
     * @param {Array} parts token组成的数组
     * @param {String} sep 连接分隔符
     * @return {String}
     */
    merge = function (parts, sep) {
        sep = (sep === undefined) ? " " : sep;
        var res = "";
        for (var i = 0; i < parts.length; i++) {
            res += (parts[i].text + sep);
        }
        return res.substr(0, res.length - sep.length);
    }
    /*}}}*/

    /*{{{ pickUp()*/
    /**
     * 将str中的内容按照分隔符提取成数组，类似Array里的split
     * @param {String} str 待分割的字符串
     * @param {String} sep 分隔符
     * @return {Array}
     */
    pickUp = function (tokens, sep) {
        tokens.push({ text: sep });
        var res = [];
        var pre = 0;
        var lev = 0;
        for (var i = 0; i < tokens.length; i++) {
            if (tokens[i].text === "(") { lev++; }
            if (tokens[i].text === ")") { lev--; }
            if (new RegExp("^" + sep + "$", "i").test(tokens[i].text) && lev === 0) {
                var part = tokens.slice(pre, i);
                if (part.length === 0) {
                    pre = i + 1;
                    continue;
                }
                res.push(part);
                pre = i + 1;
            }
        }
        return res;
    }
    /*}}}*/

    /*{{{ getHint()*/
    /**
     * 获得hint信息 
     * @param {array} part 可能包含hint的部分
     * @param {int}   pos  hint出现的位置
     * @return {obj||undefined} 返回信息对象或者Undefined
     */
    getHint = function (part, pos) {
        var hint = undefined;
        if (part && part[pos] && part[pos].type === Lexter.types.COMMENT) {
            hint = part[pos];
            var tmp = part.slice(0, pos);
            for (var i = 0; i <= pos; i++) {
                part.shift();
            }
            while (tmp.length > 0) {
                part.unshift(tmp.pop());
            }
        }
        return hint;
    }
    /*}}}*/

    /*{{{ parseOneSource()*/
    /**
     * 解析一个源
     * @param {Object} part 由几个token表示的一个源
     * @return {Object} 这个源的解析结果
     */
    parseOneSource = function (part) {
        var name;
        var type;
        var source;
        var res;
        var idx = part[0].text.indexOf(".");
        //确定源种类
        type = part[0].text.substr(0, idx);

        /*{{{ 确定具体源*/
        //例如from字段跟的是(select * from table)这样的子表
        if (idx === -1) {
            if (part[0].text === "(") {
                var innerLev = 1;
                for (var j = 1; j < part.length; j++) {
                    if (part[j].text === "(") { innerLev++; }
                    if (part[j].text === ")") { innerLev--; }
                    if (innerLev === 0) {
                        source = merge(part.slice(1, j), " ");
                        part = part.slice(j + 1, part.length);
                        break;
                    }
                }
            } else {
                source = part[0].text;
                part.shift();
            }
            //例如sql.()这样的源
        } else if (idx === part[0].text.length - 1) {
            if (part[1].text !== "(") { throw new Error("something wrong in 'source' part"); }
            var innerLev = 0;
            var j;
            for (j = 1; j < part.length; j++) {
                if (part[j].text === "(") { innerLev++; }
                if (part[j].text === ")") { innerLev--; }
                if (innerLev === 0) {
                    source = merge(part.slice(2, j), " ");
                    part = part.slice(j + 1, part.length);
                    break;
                }
            }
            if (j === part.length) { throw new Error("lack parentheses in 'source' part"); }
            //列入db.table这样的源
        } else {
            source = part[0].text.substr(idx + 1);
            part.shift();
        }
        /*}}}*/

        /*{{{ 确定源名字，并设置返回对象*/
        if (part.length === 0) {
            res = {
                name: source,
                type: type,
                source: source
            }
        } else {
            if (part.length >= 3 || (part.length === 2 && !(/^as$/i.test(part[0].text))) || (part[part.length - 1].type !== Lexter.types.KEYWORD && part[part.length - 1].type !== Lexter.types.VARIABLE)) { throw new Error("something wrong in 'source' part"); }
            res = {
                name: part[part.length - 1].text,
                type: type,
                source: source
            }
        }
        /*}}}*/

        return res;

    }
    /*}}}*/

    /*{{{ parseOneWhere()*/
    /**
     * 解析一个条件
     * @param {Object} part 由几个token表示的一个条件
     * @return {Object} 这个条件的解析结果
     */
    parseOneWhere = function (part) {
        var res = {};
        var relate = part[1];
        res["column"] = part[0];
        //如果关系是操作符
        if (relate.type === Lexter.types.OPERATOR) {
            if (RELATE[relate.text] === undefined) {
                throw new Error("unrecognized operator");
            }
            res["relate"] = RELATE[relate.text];
            res["values"] = group(part.slice(2), ',');
            //如果关系是类似 in , like 这样的关键字
        } else {
            if (relate.text.toLowerCase() === "between") {
                res["relate"] = RELATE[relate.text.toLowerCase()];
                res["values"] = group(part.slice(3, part.length - 1), "and");
            } else if (relate.text.toLowerCase() === "in") {
                res["relate"] = RELATE[relate.text.toLowerCase()];
                res["values"] = group(part.slice(3, part.length - 1), ',');
            } else if (relate.text.toLowerCase() === "like") {
                res["relate"] = RELATE[relate.text.toLowerCase()];
                res["values"] = [[part[2]]];
            } else if (relate.text.toLowerCase() === "is") {
                if (part[2].text.toLowerCase() === "not") {
                    res["relate"] = RELATE["not null"];
                    res["values"] = null;
                } else if (part[2].text.toLowerCase() === "null") {
                    res["relate"] = RELATE["is null"];
                    res["values"] = null;
                } else {
                    throw new Error("wrong key word after \"is\"");
                }
            } else if (relate.text.toLowerCase() === "not") {
                if (part[2].text.toLowerCase() === "in") {
                    res["relate"] = RELATE["not in"];
                    res["values"] = group(part.slice(4, part.length - 1), ',');
                } else if (part[2].text.toLowerCase() === "like") {
                    res["relate"] = RELATE["not like"];
                    res["values"] = [[part[3]]];
                } else {
                    throw new Error("wrong key word after \"not\"");
                }
            } else {
                throw new Error("unrecognized relate");
            }
        }
        return res;
    }
    /*}}}*/

    /*{{{ group */
    /**
     * token分组
     * @param {array} part token组
     * @param {string} sep 分隔符
     * @param {}
     */
    group = function (part, sep) {
        sep = new RegExp(sep, 'i');

        var res = [];
        var pos = 0;
        var tmp = [];
        for (var i in part) {
            if (sep.test(part[i].text)) {
                res.push(tmp);
                tmp = [];
            } else {
                tmp.push(part[i]);
            }
        }

        if (tmp.length !== 0) {
            res.push(tmp);
        }
        return res;
    }
    /*}}}*/
})();

var deleteSQL=(function () {

    /*{{{ divideTokens()*/
    /**
     * 分解sql语句
     * @param {Array} tokens 需要分解的tokens
     * @return {Object} 分解结果
     */
    function divideTokens(tokens) {

        var parts = {};
        var lev = 0;

        for (var i = 0; i < tokens.length; i++) {
            var txt = tokens[i].text;
            if (txt === "(") { lev++; continue; }
            if (txt === ")") { lev--; continue; }
            if (lev !== 0 || tokens[i].type !== Lexter.types.KEYWORD) { continue; }
            if (/^WHERE$/i.test(txt)) {
                parts.source = tokens.slice(0, i);
                parts.where = tokens.slice(i);
            }
        }

        if (parts.source === undefined) {
            parts.source = tokens;
        }

        return parts;

    }
    /*}}}*/

    /*{{{ parseSource()*/
    /**
     * 解析Source字段
     * @param {Array} tokens 需要解析的tokens
     * @return {Object} 解析结果
     */
    parseSource= function(tokens) {

        var res = {};

        if (!(/^delete$/i.test(tokens.shift().text))) {
            throw new Error("no keyword 'delete'");
        }
        if (!(/^from$/i.test(tokens.shift().text))) {
            throw new Error("no keyword 'from'");
        }

        var get = Tool.parseOneSource(tokens);

        return {
            name: get["name"],
            type: get["type"],
            source: get["source"],
        }

    }
    /*}}}*/

    /*{{{ parseWhere()*/
    /**
     * 解析Where字段
     * @param {Array} tokens 需要解析的tokens
     * @return {Object} 解析结果
     */
    parseWhere=  function(tokens) {

        var res = [];
        var parts = Tool.pickUp(tokens, "and");

        parts[0].shift();
        parts.forEach(function (part) {
            res.push(Tool.parseOneWhere(part));
        });

        return res;

    }
    exports.parseWhere = parseWhere;
    /*}}}*/

    /*{{{ createObj()*/
    /**
     * 创建sql对应的sql对象，可以选择解析部分
     * @param {String} sql 需要分析的sql
     * @return {Object} 结果对象
     */
    exports.createObj = function (sql) {

        var res = {};

        var tokens = Lexter.create(sql).getAll();
        var parts = divideTokens(tokens);

        var hint = Tool.getHint(parts['source'], 1);
        if (hint) { res.hint = hint; }
        if (parts["source"]) { res.source = parseSource(parts["source"]); }
        if (parts["where"]) { res.where = parseWhere(parts["where"]); }

        return res;
    }
    /*}}}*/
})();

var sqlParser = (function () {
    function init() {
        require('fs').readdirSync(__dirname + '/parsers').forEach(function (file) {
            var match = file.match(/^(\w+)\.js$/);
            if (!match) {
                return;
            }
            parsers[match[1].trim().toLowerCase()] = require(__dirname + '/parsers/' + file);
        });
    }
    init();

    parse = function (sql) {
        sql = sql.trim();
        var who = sql.substr(0, sql.indexOf(' ')).toLowerCase();
        if (parsers[who] === undefined) {
            throw new Error("Unsupport sentence");
        }
        return parsers[who].createObj(sql);
    }

    exports.RELATE = Tool.RELATE;
    exports.JOIN = Tool.JOIN;
    exports.ORDER = Tool.ORDER;
    exports.types = Lexter.types;




})();