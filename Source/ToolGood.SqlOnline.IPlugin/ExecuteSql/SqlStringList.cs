/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ToolGood.SqlOnline.IPlugin
{
    public class SqlStringList : List<SqlString>
    {
        public void Add(string field, SqlToken token)
        {
            this.Add(new SqlString(field, token));
        }

        public void SetLayer()
        {
            var layer = 0;
            foreach (var item in this) {
                item.Layer = layer;
                if (item.SqlToken == SqlToken.BracketOpen) {
                    layer++;
                } else if (item.SqlToken == SqlToken.BracketClose) {
                    layer--;
                } else if (item.SqlToken == SqlToken.Field) {
                    var field = item.SqlField;
                    if (field.Equals("begin", StringComparison.OrdinalIgnoreCase)) {
                        layer++;
                    } else if (field.Equals("end", StringComparison.OrdinalIgnoreCase)) {
                        layer--;
                    }
                }
            }
        }

        public string FindStrings(List<string> sqlStrings)
        {
            foreach (var item in this) {
                if (item.SqlToken == SqlToken.Field) {
                    var field = item.SqlField.ToLower();
                    if (sqlStrings.Contains(field)) {
                        return field;
                    }
                }
            }
            return null;
        }

        public string FindStrings(params string[] sqlStrings)
        {
            foreach (var item in this) {
                if (item.SqlToken == SqlToken.Field) {
                    var field = item.SqlField.ToLower();
                    if (sqlStrings.Contains(field)) {
                        return field;
                    }
                }
            }
            return null;
        }

        private static Regex whitespaceRegex = new Regex("[ \r\n\t]+");

        public List<string> SplitBySemi()
        {
            List<string> list = new List<string>();
            StringBuilder sql = new StringBuilder();

            foreach (var item in this) {
                if (item.SqlToken == SqlToken.Comment) {
                    continue;
                }
                sql.Append(item.SqlField);
                if (item.SqlToken == SqlToken.Semi && item.Layer == 0) {
                    var str = whitespaceRegex.Replace(sql.ToString().Trim(), " ");
                    list.Add(str);
                    sql.Clear();
                }
            }
            if (sql.Length > 0) {
                var str = whitespaceRegex.Replace(sql.ToString().Trim(), " ");
                list.Add(str);
            }
            return list;
        }

        public static SqlStringList SplitString(string sql)
        {
            SqlStringList list = new SqlStringList();

            bool isInText = false;
            bool isInComment = false;
            bool isInLineComment = false;
            var c = 'a';
            var index = 0;
            var text = "";

            while (index < sql.Length) {
                var ch = sql[index++];
                if (isInLineComment) {
                    if (ch == '\r' && index < sql.Length && sql[index] == '\n') {
                        isInLineComment = false;
                        list.Add(text, SqlToken.Comment);
                        list.Add("\r\n", SqlToken.Whitespace);
                        text = "";
                    } else if (ch == '\r' || ch == '\n') {
                        isInLineComment = false;
                        list.Add(text, SqlToken.Comment);
                        list.Add(ch.ToString(), SqlToken.Whitespace);
                        text = "";
                    } else {
                        text += ch;
                    }
                } else if (isInComment) {
                    text += ch;
                    if (ch == '*' && index < sql.Length && sql[index] == '/') {
                        isInComment = false; list.Add(text + "/", SqlToken.Comment); text = ""; index++;
                    }
                } else if (isInText) {
                    text += ch;
                    if (c == '\\') {
                        text += sql[index++];
                    } else if (c == '[') {
                        if (ch == ']') { isInText = false; list.Add(text, SqlToken.Field); text = ""; }
                    } else if (c == '\'') {
                        if (ch == '\'' && index < sql.Length && sql[index] == '\'') { text += sql[index++]; break; }
                        if (ch == '\'') { isInText = false; list.Add(text, SqlToken.Field); text = ""; }
                    } else if (c == '"') {
                        if (ch == '"') { isInText = false; list.Add(text, SqlToken.Field); text = ""; }
                    } else if (c == '`') {
                        if (ch == '`') { isInText = false; list.Add(text, SqlToken.Field); text = ""; }
                    }
                } else if (ch == '#') {
                    isInLineComment = true; list.Add(text, SqlToken.Field); text = "#";
                } else if (ch == '-' && index < sql.Length && sql[index] == '-') {
                    isInLineComment = true; list.Add(text, SqlToken.Field); text = "--"; index++;
                } else if (ch == '/' && index < sql.Length && sql[index] == '*') {
                    isInComment = true; list.Add(text, SqlToken.Field); text = "/*"; index++;
                } else if (ch == '[' || ch == '\'' || ch == '"' || ch == '`') {
                    c = ch; isInText = true; list.Add(text, SqlToken.Field); text = ch.ToString();

                } else if (ch == '(' || ch == '{') {
                    list.Add(text, SqlToken.BracketOpen); text = ch.ToString();
                } else if (ch == ')' || ch == '}') {
                    list.Add(text, SqlToken.BracketClose); text = ch.ToString();
                } else if (ch == ',' || ch == '.' || ch == ':') {
                    list.Add(text, SqlToken.Field); list.Add(ch.ToString(), SqlToken.Other); text = "";
                } else if (ch == ';') {
                    list.Add(text, SqlToken.Field); list.Add(ch.ToString(), SqlToken.Semi); text = "";
                } else if (ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '%' || ch == '=' || ch == ':' || ch == '~' || ch == '|' || ch == '&' || ch == '^' || ch == '<' || ch == '>' || ch == '!') {
                    list.Add(text, SqlToken.Field); list.Add(ch.ToString(), SqlToken.Operator); text = "";
                } else if (ch == ' ' || ch == '\t' || ch == '\r' || ch == '\n') {
                    list.Add(text, SqlToken.Field); list.Add(ch.ToString(), SqlToken.Whitespace); text = "";

                } else {
                    text += ch;
                }
            }
            if (isInComment || isInLineComment) {
                list.Add(text, SqlToken.Comment);
            } else {
                list.Add(text, SqlToken.Field);
            }

            list.RemoveAll(q => string.IsNullOrEmpty(q.SqlField));
            list.SetLayer();
            return list;
        }


    }


}
