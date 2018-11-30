/*
  作者:yixuan
  介绍：sql语句（delete语句）解析类
  邮箱:yixuan.zzq@taobao.com
*/

var Lexter = require('../lexter.js');
var Tool = require('../sqlParseTool.js');

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
function parseSource(tokens) {

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
exports.parseSource = parseSource;
/*}}}*/

/*{{{ parseWhere()*/
/**
 * 解析Where字段
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
    if (parts["where"]) { res.where = parseWhere(parts["where"]); }

    return res;
}
/*}}}*/
