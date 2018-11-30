/*
  作者:yixuan
  介绍：sql语句（update语句）解析类
  邮箱:yixuan.zzq@taobao.com
*/

var Lexter = require('../lexter.js');
var Tool = require('../sqlParseTool.js');

/*{{{ divideTokens()*/
/**
 * 分解Tokens
 * @param {Array} tokens 需要分解的tokens
 * @return {Object} 分解结果
 */
function divideTokens(tokens) {

    var parts = {};
    var lev = 0;
    var pos = 0;

    for (var i = 0; i < tokens.length; i++) {
        var txt = tokens[i].text;

        if (txt === "(") { lev++; continue; }
        if (txt === ")") { lev--; continue; }
        if (lev !== 0 || tokens[i].type !== Lexter.types.KEYWORD) { continue; }

        if (/^SET$/i.test(txt)) {
            if (i === tokens.length - 1) {
                throw new Error("empty after keyword 'set'");
            }
            parts["source"] = tokens.slice(0, i);
            pos = i;
        } else if (/^WHERE$/i.test(txt)) {
            if (i === tokens.length - 1) {
                throw new Error("empty after keyword 'where'");
            }
            parts["column"] = tokens.slice(pos, i);
            pos = i;
        }
    }

    parts["where"] = tokens.slice(pos);
    return parts;
}
/*}}}*/

/*{{{ parseSource()*/
/**
 * 把tokens当做sql中的table字段解析
 * @param {Array} tokens 需要解析的tokens
 * @return {Object} 解析结果
 */
function parseSource(tokens) {

    var res = {};

    if (!(/^update$/i.test(tokens.shift().text))) {
        i
        throw new Error("no keyword 'update'");
    }

    var get = Tool.parseOneSource(tokens);

    return {
        name: get["name"],
        type: get["type"],
        source: get["source"],
    }

}
exports.parseSource = parseSource;
/*}}}*/

/*{{{ parseColumn()*/
/**
 * 把tokens当做sql中的set字段解析
 * @param {Array} tokens 需要解析的tokens
 * @return {Object} 解析结果
 */
function parseColumn(tokens) {

    var res = [];

    var parts = Tool.pickUp(tokens, ",");
    parts[0].shift();

    parts.forEach(function (part) {
        res.push(Tool.parseOneWhere(part))
    });

    return res;
}
exports.parseColumn = parseColumn;
/*}}}*/

/*{{{ parseWhere()*/
/**
 * 把tokens当做sql中的where字段解析
 * @param {Array} tokens 需要解析的tokens
 * @return {Object} 解析结果
 */
function parseWhere(tokens) {

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
    if (parts["column"]) { res.column = parseColumn(parts["column"]); }
    if (parts["where"]) { res.where = parseWhere(parts["where"]); }

    return res;

}
/*}}}*/
