var Tool = require(__dirname + '/sqlParseTool.js');
var Lexter = require(__dirname + '/lexter.js');

var parsers = {};

function init(){
  require('fs').readdirSync(__dirname + '/parsers').forEach(function(file) {
    var match   = file.match(/^(\w+)\.js$/);
      if (!match) {
        return;
      }
      parsers[match[1].trim().toLowerCase()] = require(__dirname + '/parsers/' + file);
  });
}
init();

exports.parse = function(sql) {
  sql = sql.trim();
  var who = sql.substr(0,sql.indexOf(' ')).toLowerCase();
  if(parsers[who] === undefined){
    throw new Error("Unsupport sentence");
  }
  return  parsers[who].createObj(sql);
}

exports.RELATE = Tool.RELATE;
exports.JOIN = Tool.JOIN;
exports.ORDER = Tool.ORDER;
exports.types = Lexter.types;
