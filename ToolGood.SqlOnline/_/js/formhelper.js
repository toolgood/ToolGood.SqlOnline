/**
 * 填充表单内容：将json数据形式数据填充到表单内，最多解析两层json结构
 * 
 * @param {$()} [frm] jQuery表单对象 可空
 * @param {Object} json 序列化好的json数据对象，最多只包含两层嵌套
 */
function SetForm(frm, json) {
    var $ = jQuery;
    function _copyObject(src, tar, pre) {
        if (src == null || src == undefined) { return; }
        if (typeof src === "function") { return; }
        if (src.constructor === Array) {
            for (var i = 0; i < src.length; i++) {
                _copyObject(src[i], tar, pre + "[" + i + "]");
            }
            return;
        }
        if (typeof src === "object") {
            for (var key in src) {
                _copyObject(src[key], tar, pre != null ? pre + "." + key : key);
            }
            return;
        }
        tar[pre] = src;
    }
    function _deserializeInputs(inputs, value) {
        if (!inputs && value == null || inputs == null) { return; }

        for (var i = inputs.length - 1; i >= 0; i--) {
            var input = $(inputs[i]);
            // 判断输入框是否已经序列化过
            if (input.hasClass('_isSerialized')) { continue; }

            var type = input.attr('type');
            if (type) {
                type = type.toLowerCase();

                switch (type) {
                    case 'checkbox': {
                        var tempvalue = input.val();
                        value = value + '';
                        var valueArrs = value.split(',');
                        for (var j = 0; j < valueArrs.length; j++) {
                            if (valueArrs[j].toLowerCase() == tempvalue.toLowerCase()) {
                                input.prop('checked', true);
                            } else if (input.attr("name").toLowerCase() == valueArrs[j].toLowerCase()) {
                                input.prop('checked', true);
                            }
                        }
                    }
                        break;

                    case 'radio': {
                        input.each(function (i) {
                            var thiz = $(this);
                            if (thiz.prop('value').toLowerCase() == value.toLowerCase()) {
                                thiz.prop('checked', true);
                            }
                        });
                    }
                        break;
                    default: {
                        input.val(value);
                    }
                }
            } else {
                input.val(value);
            }
            input.addClass('_isSerialized');
        }
        return;
    }
    function _filterInputs(container) {
        var inputs = $(container.find('input[type!=button][type!=reset][type!=submit][type!=image][type!=file],select,textarea'));
        return inputs;
    }
    function _findInputs(inputs, key) {
        var json = [];
        inputs.each(function () {
            var name = $(this).attr("name");
            var id = $(this).attr("id");
            if ((name && (name.toLowerCase() == key.toLowerCase())) || (id && (id.toLowerCase() == key.toLowerCase()))) {
                json.push(this);
            }
        });
        if (json.length > 0) { return json; }

        var input = $(inputs.filter('input[name="' + key + '"],input[id="' + key + '"],textarea[name="' + key + '"],textarea[id="' + key + '"],select[name="' + key + '"],select[id="' + key + '"]'));
        return input;
    }

    if (json == undefined) {
        json = frm;
        frm = $('body');
    }

    var temp = {};
    _copyObject(json, temp, null);
    json = temp;

    var inputs = _filterInputs(frm);
    for (var key in json) {
        var input = _findInputs(inputs, key);
        _deserializeInputs(input, json[key]);
    }
    inputs.filter('._isSerialized').removeClass('_isSerialized');
    return this;
}
