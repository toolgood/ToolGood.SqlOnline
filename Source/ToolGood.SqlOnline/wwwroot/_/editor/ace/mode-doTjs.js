ace.define("ace/mode/dotjs_highlight_rules", ["require", "exports", "module", "ace/lib/oop", "ace/mode/text_highlight_rules", "ace/mode/javascript_highlight_rules"], function (require, exports, module) {
    "use strict";
    var oop = require("../lib/oop");
    var TextHighlightRules = require("./text_highlight_rules").TextHighlightRules;

    var JavaScriptHighlightRules = require("./javascript_highlight_rules").JavaScriptHighlightRules;

    var JavaScriptHighlightRules2 = function () {
        this.$rules = new TextHighlightRules().getRules();

        for (var i in this.$rules) {
            this.$rules[i].unshift({
                token: "keyword",
                regex: "{{",
                next: "javascript-start"
            });
        }
        this.embedRules(JavaScriptHighlightRules, "javascript-", [
            {
                token: "keyword",
                regex: "}}",
                next: "start"
            }
        ]);
    };
    oop.inherits(JavaScriptHighlightRules2, TextHighlightRules);

    exports.JavaScriptHighlightRules2 = JavaScriptHighlightRules2;
});
ace.define("ace/mode/dotjs", ["require", "exports", "module", "ace/lib/oop", "ace/mode/text", "ace/mode/dotjs_highlight_rules"], function (require, exports, module) {
    "use strict";

    var oop = require("../lib/oop");
    var TextMode = require("./text").Mode;
    var JavaScriptHighlightRules = require("./dotjs_highlight_rules").JavaScriptHighlightRules2;

    var Mode = function () {
        this.HighlightRules = JavaScriptHighlightRules;
        this.$behaviour = this.$defaultBehaviour;
    };
    oop.inherits(Mode, TextMode);

    (function () {
        this.lineCommentStart = ";";
        this.blockComment = null;
        this.$id = "ace/mode/dotjs";
    }).call(Mode.prototype);

    exports.Mode = Mode;
});

(function () {
    ace.require(["ace/mode/dotjs"], function (m) {
        if (typeof module == "object" && typeof exports == "object" && module) {
            module.exports = m;
        }
    });
})();
