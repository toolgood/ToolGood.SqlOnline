

/**
 * 获取参数
 * @param {any} val
 */
function getParameter(val) {
    var re = new RegExp("[?&]" + val + "=([^&?]*)", "ig");
    var url = location.search;
    return ((url.match(re)) ? (decodeURI(url.match(re)[0].substr(val.length + 2))) : '');
}
/*******************************************************************
 *          Layer 弹窗  
 *******************************************************************/

/**
 * 提示内容 两秒后消失
 * @param {String} msg 提示内容
 * @param {Function} callBack 返回事件
*/
function openMsg(msg, callBack) {
    if (msg == null) { msg = "[null]"; }
    layer.msg(msg, { time: 2000 }, function (index) { callBack && callBack(); layer.close(index); });
}
/**
 * 提示内容 两秒后消失
 * @param {String} msg 提示内容
 * @param {Function} callBack 返回事件
*/
function openTopMsg(msg, callBack) {
    if (window != top) { top.openMsg(msg, callBack); } else { openMsg(msg, callBack); }
}

/**
 * 显示系统提示,需要点击确认按钮才能关闭
 * @param {String} msg 提示内容
 * @param {String} title [可选]提示标题
 * @param {Function} callBack 返回事件
 * */
function openAlert(msg, title, callBack) {
    if (msg == null) { msg = "[null]"; }
    if (typeof title === "function") {
        layer.alert(msg, function (index) { callBack && callBack(); layer.close(index); });
    } else {
        layer.alert(msg, { title: title }, function (index) { callBack && callBack(); layer.close(index); });
    }
}
/**
 * 显示系统提示,需要点击确认按钮才能关闭
 * @param {String} msg 提示内容
 * @param {String} title [可选]提示标题
 * @param {Function} callBack 返回事件
 * */
function openTopAlert(msg, title, callBack) {
    if (window != top) {
        top.openAlert(msg, title, callBack);
    } else {
        openAlert(msg, title, callBack);
    }
}
/**
 * 警告提示
 * @param {any} msg 提示内容
 * @param {any} callBack
 */
function openWarn(msg, callBack) {
    openAlert(msg, '警告', 0, callBack);
}
/**
 * 警告提示
 * @param {any} msg 提示内容
 * @param {any} callBack
 */
function openTopWarn(msg, callBack) {
    if (window != top) {
        top.openWarn(msg, callBack);
    } else {
        openWarn(msg, callBack);
    }
}

/**
 * 弹出确认提示框
 * @param {String} [msg] 确认内容
 * @param {String} [title] [可选]标题
 * @param {Function} [confirmCallback] 确认后要执行的方法
 * @param {Function} [noConfirmCallback] 取消后要执行的方法
*/
function openConfirm(msg, title, confirmCallback, noConfirmCallback) {
    if (msg == null) { msg = "[null]"; }
    if (typeof title === "function") {
        noConfirmCallback = confirmCallback;
        confirmCallback = title;
        title = "提示";
    }
    layer.confirm(msg, { title: title }, function (index) {
        confirmCallback && confirmCallback();
        layer.close(index);
    }, function () {
        noConfirmCallback && noConfirmCallback();
    });
}
/**
 * 弹出确认提示框
 * @param {String} [msg] 确认内容
 * @param {String} [title] [可选]标题
 * @param {Function} [confirmCallback] 确认后要执行的方法
 * @param {Function} [noConfirmCallback] 取消后要执行的方法
*/
function openTopConfirm(msg, title, confirmCallback, noConfirmCallback) {
    if (window != top) {
        top.openConfirm(msg, title, confirmCallback, noConfirmCallback);
    } else {
        openConfirm(msg, title, confirmCallback, noConfirmCallback);
    }
}
function getParentWindow(index) {
    if (window != top) {
        var index = parent.layer.getFrameIndex(window.name);
        return top.getParentWindow(index);
    } else {
        var $ = jQuery;
        //判断是否有上级的弹层，如果有，就刷新上级弹层
        if ($("#layui-layer" + index).prevAll('.layui-layer').length > 0) {
            var id = $($("#layui-layer" + index).prevAll('.layui-layer')[0]).attr('id').replace('layui-layer', '');
            return $("#layui-layer-iframe" + id)[0].contentWindow;
        } else {
            $(".sub-window-container").each(function () {
                if ($(this).css("display") != "none") {
                    return $(this).children("iframe")[0].contentWindow;
                }
            })
            return parent;
        }
    }
}

/**
 * 关闭窗口并刷新上一级页面
*/
function closeWindowAndReload(funName, index) {
    if (window != top) {
        index = parent.layer.getFrameIndex(window.name);
        top.closeWindowAndReload(funName, index);
        parent.layer.close(index);
    } else {
        var $ = jQuery;
        //判断是否有上级的弹层，如果有，就刷新上级弹层
        if ($("#layui-layer" + index).prevAll('.layui-layer').length > 0) {
            var id = $($("#layui-layer" + index).prevAll('.layui-layer')[0]).attr('id').replace('layui-layer', '');
            var target = $('#layui-layer-iframe' + id);
            if (funName && target[0].contentWindow[funName]) {
                target[0].contentWindow[funName]();
            } else if (target[0].contentWindow.reload) {
                target[0].contentWindow.reload();
            } else {
                target[0].contentWindow.location.reload();
            }
        } else {
            $(".sub-window-container").each(function () {
                if ($(this).css("display") != "none") {
                    var target = $(this).children("iframe");
                    if (funName && target[0].contentWindow[funName]) {
                        target[0].contentWindow[funName]();
                    } else if (target[0].contentWindow.reload) {
                        target[0].contentWindow.reload();
                    } else {
                        target[0].contentWindow.location.reload();
                    }
                }
            })
        }
        parent.layer.close(index);
    }
}
/**
 * 关闭窗口
*/
function closeWindow() {
    var index = parent.layer.getFrameIndex(window.name);
    parent.layer.close(index);
}

/**
 * */
function closeTabContent() {
    var id = top.$("ul.layui-tab-title li.layui-this").attr("lay-id");
    var element = top.layui.element;
    element.tabDelete('myTab', id);
}


/**
 * 关闭窗口
*/
function closeAllWindows() {
    top.layer.closeAll();
}
/** 显示 loading 图标 */
function openLoading() {
    layer.load(2);
}
/** 隐藏 loading 图标 */
function closeLoading() {
    layer.closeAll("loading");
}
/**
 * 打开窗口
 * @param {string} title
 * @param {string} url
 * @param {function} success
 * @param {function} end
 */
function openWindow(title, url, success, end) {
    function QueryString(val, url) {
        var re = new RegExp("" + val + "=([^&?#]*)", "ig");
        return ((url.match(re)) ? (decodeURI(url.match(re)[0].substr(val.length + 1))) : '');
    }

    var w = QueryString('w', url), h = QueryString('h', url);
    w = ((w == null || w == '')) ? ($('body').width() - 100) : w;
    h = ((h == null || h == '')) ? ($('body').height() - 100) : h;
    if ($('body').width() * 0.95 < w) { w = $('body').width() * 0.95; }
    if ($('body').height() * 0.95 < h) { h = $('body').height() * 0.95; }

    layer.open({
        type: 2,
        area: [w + 'px', h + 'px'],
        fix: true,
        shade: 0.4,
        title: title,
        anim: -1,
        content: [url],
        scrollbar: true,
        shadeClose: false,
        maxmin: true,
        success: function (layero, index) {
            success && success(layero, index, window[layero.find('iframe')[0]['name']]);
        },
        end: function () {
            end && end();
        }
    });
}
/**
 * 顶部窗口 打开窗口
 * @param {string} title
 * @param {string} url
 * @param {function} success
 * @param {function} end
 */
function openTopWindow(title, url, success, end) {
    if (url.charAt(0) == '.' && url.charAt(1) == '.' && url.charAt(1) == '/') {
        url = location.pathname + '/../' + url;
    } else if (url.charAt(0) == '.' && url.charAt(1) == '/') {
        url = location.pathname + '/.' + url;
    }
    url = url.replace("//", "/");
    if (window != top) {
        top.openWindow(title, url, success, end);
    } else {
        openWindow(title, url, success, end);
    }
    return false;
}
/**
 * 触发打开顶部窗口
 * @param {any} id
 * @param {any} data
 * @param {any} success
 * @param {any} end
 */
function trrigerOpenTopWindow(id, data, success, end) {
    var t = $(obj).attr("title") || $(obj).attr("name") || $(obj).text();
    var a = $(obj).attr("href") || $(obj).attr("src") || $(obj).attr("alt");
    if (data) {
        for (var item in data) {
            if (data[item]) {
                a += a.indexOf("?") >= 0 ? "&" : "?";
                a += item + "=" + encodeURI(data[item]);
            }
        }
    }
    openTopWindow(t, a, success, end);
}
/**
 * 触发打开窗口
 * @param {any} id
 * @param {any} data
 * @param {any} success
 * @param {any} end
 */
function trrigerOpenWindow(id, data, success, end) {
    var t = $(obj).attr("title") || $(obj).attr("name") || $(obj).text();
    var a = $(obj).attr("href") || $(obj).attr("src") || $(obj).attr("alt");
    if (data) {
        for (var item in data) {
            if (data[item]) {
                a += a.indexOf("?") >= 0 ? "&" : "?";
                a += item + "=" + encodeURI(data[item]);
            }
        }
    }
    openWindow(t, a, success, end);
}


/*******************************************************************
 *          input 弹窗
 *******************************************************************/

function standardSearchText(str) {
    // \u202D\u202c 从excel表格直接复制到input框
    // \u0085 代表下一行的字符
    // \u00A0 不换行空格 相当与  看上去和空格一样，但是在HTML中不自动换行， 主要用在office中,让一个单词在结尾处不会换行显示,快捷键ctrl+shift+space ;
    // \u2028 行分隔符
    // \u2029 段落分隔符
    // \uFEFF 字节顺序标记(零宽非连接符)
    // \u200E 从左至右书写标记
    // \u200F 从右至左书写标记
    // \u200D 零宽连接符
    // \u2006 另一种空格符
    // \u3000 全角空格(中文符号)
    str = str.replace(/[\x00-\x1F\x7f]/ig, '');// ASCII码 不可见字符
    str = str.replace(/[\u00A0\u1680\u2000-\u200a\u202F\u205F\u3000]/ig, ' ');// 换成普通空格符
    return str.replace(/[\u180E\u200b-\u200f\u2028-\u202e\u2060\uFEFF]/ig, '').trim(); //清空两端空格
}
function standardText(str) {
    str = str.replace(/[\x00-\x08\x0B\x0C\x0E-\x1F\x7f\u0085]/ig, '');// ASCII码 排除 \t \r \n
    str = str.replace(/[\u00A0\u1680\u2000-\u200a\u202F\u205F\u3000]/ig, ' ');// 换成普通空格符
    return str.replace(/[\u180E\u200b-\u200f\u2028-\u202e\u2060\uFEFF]/ig, '').trim(); //清空两端空格
}
/**
 * 标准化处理 整数
 * @param {any} newValue
 * @param {any} min
 * @param {any} max
 */
function standardInt(newValue, min, max) {
    if (min && min > 0) {
        newValue = newValue.replace(/[^\d]/g, '').replace(/^0+(\d.*)$/g, '$1');
    } else {
        newValue = newValue.replace(/[^-\d]/g, '').replace(/^(-?)0+(\d.*)$/g, '$1$2').replace(/^(.+)-/g, '$1');
    }
    if (min && min != '' && newValue != "") {
        if (parseInt(newValue) < min) { newValue = min }
    }
    if (max && max != '' && newValue != "") {
        if (parseInt(newValue) > max) { newValue = max }
    }
    return newValue;
}
/**
 * 标准化处理 数字带小数
 * @param {any} newValue
 * @param {any} min
 * @param {any} max
 */
function standardNum(newValue, min, max) {
    if (min && min > 0) {
        newValue = newValue.replace(/[^\.\d]/g, '').replace(/^0+(\d.*)$/g, '$1').replace(/(\..*?)\./g, '$1').replace(/^(\.)(\d*)$/g, '0$1$2');
    } else {
        newValue = newValue.replace(/[^-\.\d]/g, '').replace(/^(-?)0+(\d.*)$/g, '$1$2').replace(/^(.+)-/g, '$1').replace(/(\..*?)\./g, '$1').replace(/^(-?)(\.)(\d*)$/g, '$10$2$3')
    }
    if (min && min != '' && newValue != "") {
        if (parseFloat(newValue) < min) { newValue = min }
    }
    if (max && max != '' && newValue != "") {
        if (parseFloat(newValue) > max) { newValue = max }
    }
    return newValue;
}
/**
 * 时间格式化
 * @param {any} date 时间
 * @param {any} fmt 格式 默认：yyyy-MM-dd HH:mm:ss
 */
function dateFormat(date, fmt) {
    if (fmt == undefined) {
        fmt = "yyyy-MM-dd HH:mm:ss";
    }
    var o = {
        "M+": date.getMonth() + 1, //月份
        "d+": date.getDate(), //日
        "h+": date.getHours() % 12 == 0 ? 12 : date.getHours() % 12, //小时
        "H+": date.getHours(), //小时
        "m+": date.getMinutes(), //分
        "s+": date.getSeconds(), //秒
        "q+": Math.floor((date.getMonth() + 3) / 3), //季度
        "S": date.getMilliseconds() //毫秒
    };
    var week = { "0": "/u65e5", "1": "/u4e00", "2": "/u4e8c", "3": "/u4e09", "4": "/u56db", "5": "/u4e94", "6": "/u516d" };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "/u661f/u671f" : "/u5468") : "") + week[date.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}
/**
 * 标准化处理 时间
 * @param {any} newValue
 * @param {any} min
 * @param {any} max
 */
function standardDate(newValue, min, max) {
    function isValidDate(date) {
        return date instanceof Date && !isNaN(date.getTime())
    }
    newValue = newValue.replace(/[年月]/g, '-');
    newValue = newValue.replace(/日/g, ' ');
    newValue = newValue.replace(/[时分]/g, ':');
    newValue = newValue.replace(/秒/g, '');
    newValue = newValue.replace(/\//g, '-');
    newValue = newValue.replace(/T/g, ' ');
    newValue = newValue.replace(/ +/g, ' ');
    //newValue = newValue.trim();
    newValue = standardSearchText(newValue);

    if (/^\d{1}-\d{1,2}-\d{1,2}( |$)/.test(newValue)) {
        var y = new Date().getFullYear().toString().substr(0, 3);
        newValue = y + newValue;
    } else if (/^\d{2}-\d{1,2}-\d{1,2}( |$)/.test(newValue)) {
        var y = new Date().getFullYear().toString().substr(0, 2);
        newValue = y + newValue;
    } else if (/^\d{1,2}-\d{1,2}( |$)/.test(newValue)) {
        var y = new Date().getFullYear().toString();
        newValue = y + "-" + newValue;
    }
    if (/^\d{4}-\d{1,2}-\d{1,2} \d{1,2}$/.test(newValue)) {
        newValue = newValue + ":0:0";
    }
    if (/^\d{4}-\d{1,2}-\d{1,2}( \d{1,2}:\d{1,2}(:\d{1,2})?)?$/.test(newValue)) {
        newValue = newValue.replace(/-/g, '/');//除去8小时时区差
        dt = new Date(newValue);
        if (isValidDate(dt)) {
            if (min && min != '') {
                minDt = new Date(min.replace(/-/g, '/'));
                if (isValidDate(minDt) && dt < minDt) { dt = minDt }
            }
            if (max && max != '') {
                maxDt = new Date(max.replace(/-/g, '/'));
                if (isValidDate(maxDt) && dt > maxDt) { dt = maxDt }
            }
            return dateFormat(dt)
        }
    }
    return '';
}

(function (factory) {
    if (typeof define === 'function' && define.amd) { // AMD. Register as an anonymous module.
        define(['jquery'], factory);
    } else if (typeof exports === 'object') { // Node/CommonJS
        var jQuery = require('jquery');
        module.exports = factory(jQuery);
    } else { // Browser globals (zepto supported)
        factory(window.jQuery || window.Zepto || window.$); // Zepto supported on browsers as well
    }
}(function ($) {
    /**
     * 序列化 
     * @param {any} type 0、直接获取文本，1、获取文本格式化，2、获取文本格式化 去除 \t\r\n
     */
    $.fn.serializeJson = function (type) {
        var serializeObj = {};
        var map = {};
        var array = this.serializeArray();
        var txtFunc = standardText;
        if (type) {
            if (type == 0) {
                txtFunc = getText;
            } else if (type == 1) {
                txtFunc = standardText;
            } else if (type == 2) {
                txtFunc = standardSearchText;
            }
        }
        $(array).each(function () {
            var names = splitName(this.name);
            setData(serializeObj, map, names, 0, this.value, txtFunc);
        })
        return serializeObj;
    };
    /**
     * 去除禁用
     * */
    $.fn.removeDisabled = function () {
        $(this).prop("disabled", false);
        $(this).removeClass("layui-disabled");
        $(this).removeClass("disabled");

    }
    /**
     * 禁用
     * */
    $.fn.addDisabled = function () {
        $(this).prop("disabled", true);
        $(this).addClass("layui-disabled");
        $(this).addClass("disabled");
    }

    function setData(tarData, map, names, index, value, txtFunc) {
        var name = names[index];
        if (index == names.length - 1) {
            if (name.isArray) {
                if (tarData[name.name] == null) { tarData[name.name] = new Array(); }
                setValue(tarData, name.name, value, txtFunc);
            } else {
                setValue(tarData, name.name, value, txtFunc);
            }
        } else if (name.isArray && name.hasTag) {
            if (tarData[name.name] == null) { tarData[name.name] = new Array(); }
            if (map[name.allPath] == null) {
                var obj = {};
                tarData[name.name].push(obj);
                map[name.allPath] = obj;
            }
            setData(map[name.allPath], map, names, index + 1, value, txtFunc);
        } else if (name.isArray) {
            console.log("input name is error, no tag, set the input name like 'a[tag].b' ");
            if (tarData[name.name] == null) { tarData[name.name] = new Array(); }
            setValue(tarData, name.name, value, txtFunc);
        } else {
            if (tarData[name.name] == null) { tarData[name.name] = {}; }
            setData(tarData[name.name], map, names, index + 1, value, txtFunc);
        }
    }
    function setValue(tarData, name, value, txtFunc) {
        if (tarData[name] == null || tarData[name] == undefined) {
            tarData[name] = txtFunc(value);
        } else if ($.isArray(tarData[name])) {
            tarData[name].push(txtFunc(value));
        } else {
            tarData[name] = [tarData[name], txtFunc(value)];
        }
    }
    function splitName(txt) {
        var ts = txt.split(".");
        var names = [];
        var path = "";
        for (var i = 0; i < ts.length; i++) {
            names.push(getName(ts[i], path));
            path += ts[i] + ".";
        }
        return names;
    }
    function getName(txt, path) {
        var m = /^(.*)\[([^\]]+)\]$/.exec(txt);
        if (m != null) {
            var dataName = m[1];
            return { isArray: true, hasTag: true, name: dataName, allPath: path + "." + txt };
        }
        var m2 = /^(.*)\[\]$/.exec(txt);
        if (m2 != null) {
            var dataName = m2[1];
            return { isArray: true, hasTag: false, name: dataName };
        }
        return { isArray: false, name: txt };
    }

    function getText(str) {
        return str;
    }
}));

/****************************************************************
 *    右键菜单
 ****************************************************************/

/**
 * 显示菜单
 * @param {any} obj $("XXX")
 * @param {any} win window
 * @param {any} e 点击事件
 */
function showContextmenu(obj, win, e) {
    function getOffset(win, x, y) {
        if (win == top) { return { x: x, y: y }; }
        var fs = win.parent.document.getElementsByTagName("iframe");
        for (var i = 0; i < fs.length; i++) {
            var f = fs[i];
            if (f.contentWindow.window == win) {
                var offset = $(f).offset();
                var xx = offset.left + x;
                var yy = offset.top + y;
                return getOffset(win.parent, xx, yy)
            }
        }
    }
    if (window != top) { top.showContextmenu(obj, win, e); return; }
    var v = getOffset(win, e.clientX, e.clientY);
    var mouseX = v.x;
    var mouseY = v.y;
    var winWidth = $(document).width();
    var winHeight = $(document).height();
    var menuWidth = $("#contextmenu-div").width();
    var menuHeight = $("#contextmenu-div").height();
    var minEdgeMargin = 10;
    if (mouseX + menuWidth + minEdgeMargin >= winWidth && mouseY + menuHeight + minEdgeMargin >= winHeight) {
        menuLeft = mouseX - menuWidth - minEdgeMargin;
        menuTop = mouseY - menuHeight;// - minEdgeMargin;
    } else if (mouseX + menuWidth + minEdgeMargin >= winWidth) {
        menuLeft = mouseX - menuWidth - minEdgeMargin;
        menuTop = mouseY;//+ minEdgeMargin;
    } else if (mouseY + menuHeight + minEdgeMargin >= winHeight) {
        menuLeft = mouseX + minEdgeMargin;
        menuTop = mouseY - menuHeight;//- minEdgeMargin;
    } else {
        menuLeft = mouseX + minEdgeMargin;
        menuTop = mouseY;//+ minEdgeMargin;
    };
    if ($("#contextmenu-div").length == 0) {
        $(document.body).append("<ul id='contextmenu-div' class='contextmenu'></ul>");
    }
    $("#contextmenu-div").css({ "left": menuLeft + "px", "top": menuTop + "px" }).html("").append(obj);
    setTimeout(function () { $("#contextmenu-div").show(); }, 100);
}
function hideContextmenu() { if (window != top) { top.hideContextmenu(); return; } $("#contextmenu-div").hide(); }

$(document).on("mouseleave", "#contextmenu-div", function (event) {
    event && event.stopPropagation();
    setTimeout(function () { hideContextmenu(); }, 100);
    return false;
});

/****************************************************************
  *    水印
  ****************************************************************/
(function (watermark) {
    loadMark = function (settings) {
        //默认设置
        var defaultSettings = {
            watermark_txt: "text",
            watermark_x: 20,//水印起始位置x轴坐标
            watermark_y: 20,//水印起始位置Y轴坐标
            watermark_rows: 20,//水印行数
            watermark_cols: 20,//水印列数
            watermark_x_space: 10,//水印x轴间隔
            watermark_y_space: 50,//水印y轴间隔
            watermark_color: '#aaa',//水印字体颜色
            watermark_alpha: 0.3,//水印透明度
            watermark_fontsize: '16px',//水印字体大小
            watermark_font: '微软雅黑',//水印字体
            watermark_width: 200,//水印宽度
            watermark_height: 80,//水印长度
            watermark_angle: 15//水印倾斜度数
        };
        //采用配置项替换默认值，作用类似jquery.extend
        if (arguments.length === 1 && typeof arguments[0] === "object") {
            var src = arguments[0] || {};
            for (key in src) {
                if (src[key] && defaultSettings[key] && src[key] === defaultSettings[key])
                    continue;
                else if (src[key])
                    defaultSettings[key] = src[key];
            }
        }

        var oTemp = document.createDocumentFragment();

        //获取页面最大宽度
        var page_width = Math.max(document.body.scrollWidth, document.body.clientWidth);
        var cutWidth = page_width * 0.0150;
        var page_width = page_width - cutWidth;
        //获取页面最大高度
        var page_height = $(window).height() * 0.95;
        //如果将水印列数设置为0，或水印列数设置过大，超过页面最大宽度，则重新计算水印列数和水印x轴间隔
        if (defaultSettings.watermark_cols == 0 || (parseInt(defaultSettings.watermark_x + defaultSettings.watermark_width * defaultSettings.watermark_cols + defaultSettings.watermark_x_space * (defaultSettings.watermark_cols - 1)) > page_width)) {
            defaultSettings.watermark_cols = parseInt((page_width - defaultSettings.watermark_x + defaultSettings.watermark_x_space) / (defaultSettings.watermark_width + defaultSettings.watermark_x_space));
            defaultSettings.watermark_x_space = parseInt((page_width - defaultSettings.watermark_x - defaultSettings.watermark_width * defaultSettings.watermark_cols) / (defaultSettings.watermark_cols - 1));
        }
        //如果将水印行数设置为0，或水印行数设置过大，超过页面最大长度，则重新计算水印行数和水印y轴间隔
        if (defaultSettings.watermark_rows == 0 || (parseInt(defaultSettings.watermark_y + defaultSettings.watermark_height * defaultSettings.watermark_rows + defaultSettings.watermark_y_space * (defaultSettings.watermark_rows - 1)) > page_height)) {
            defaultSettings.watermark_rows = parseInt((defaultSettings.watermark_y_space + page_height - defaultSettings.watermark_y) / (defaultSettings.watermark_height + defaultSettings.watermark_y_space));
            defaultSettings.watermark_y_space = parseInt(((page_height - defaultSettings.watermark_y) - defaultSettings.watermark_height * defaultSettings.watermark_rows) / (defaultSettings.watermark_rows - 1));
        }
        var x;
        var y;
        for (var i = 0; i < defaultSettings.watermark_rows; i++) {
            y = defaultSettings.watermark_y + (defaultSettings.watermark_y_space + defaultSettings.watermark_height) * i;
            for (var j = 0; j < defaultSettings.watermark_cols; j++) {
                x = defaultSettings.watermark_x + (defaultSettings.watermark_width + defaultSettings.watermark_x_space) * j;

                var mask_div = document.createElement('div');
                mask_div.id = 'mask_div' + i + j;
                mask_div.className = 'mask_div';
                mask_div.appendChild(document.createTextNode(defaultSettings.watermark_txt));
                //设置水印div倾斜显示
                mask_div.style.webkitTransform = "rotate(-" + defaultSettings.watermark_angle + "deg)";
                mask_div.style.MozTransform = "rotate(-" + defaultSettings.watermark_angle + "deg)";
                mask_div.style.msTransform = "rotate(-" + defaultSettings.watermark_angle + "deg)";
                mask_div.style.OTransform = "rotate(-" + defaultSettings.watermark_angle + "deg)";
                mask_div.style.transform = "rotate(-" + defaultSettings.watermark_angle + "deg)";
                mask_div.style.visibility = "";
                mask_div.style.position = "absolute";
                mask_div.style.left = x + 'px';
                mask_div.style.top = y + 'px';
                mask_div.style.overflow = "hidden";
                mask_div.style.zIndex = "999999999";
                mask_div.style.pointerEvents = 'none';//pointer-events:none  让水印不遮挡页面的点击事件
                //mask_div.style.border="solid #eee 1px";
                mask_div.style.opacity = defaultSettings.watermark_alpha;
                mask_div.style.fontSize = defaultSettings.watermark_fontsize;
                mask_div.style.fontFamily = defaultSettings.watermark_font;
                mask_div.style.color = defaultSettings.watermark_color;
                mask_div.style.textAlign = "center";
                mask_div.style.width = defaultSettings.watermark_width + 'px';
                mask_div.style.height = defaultSettings.watermark_height + 'px';
                mask_div.style.display = "block";
                oTemp.appendChild(mask_div);
            };
        };
        document.body.appendChild(oTemp);
        $('.mask_div').on('hover', function () {
            $(this).hide();
        });

    }
    watermark.load = function (settings) {
        if (window == top) {
            window.onload = function () {
                if (settings && settings.watermark_txt && settings.watermark_txt.length > 0) {
                    loadMark(settings);
                }
            };
            window.onresize = function () {
                $(".mask_div").remove();
                loadMark(settings);
            };
        }
    };
    //watermark.load({ watermark_txt: "测试水印，1021002301，测试水印，SDAHJDBJJdjsfsc" });
})(window.watermark = {});
/****************************************************************
 *    其它
 ****************************************************************/


/**
 * 使用模板
 * @param {any} template
 * @param {any} data
 */
function tpl(template, data) {
    return doT.template($(template).html())(data)
}


$(function () {
    window.layer = layui.layer;
    window.element = layui.element;
    window.treeSelect = layui.treeSelect;
    window.table = layui.table;
    window.treeTable = layui.treeTable;


    $(document).on("click", ".openwin", function (event) {
        event && event.stopPropagation();
        $(this).prop("disenable", true);
        var t = $(this).attr("title") || $(this).attr("name") || $(this).text();
        var a = $(this).attr("href") || $(this).attr("src") || $(this).attr("alt");

        $(this).each(function () {
            $.each(this.attributes, function () {
                // this.attributes is not a plain object, but an array of attribute nodes, which contain both the name and value
                if (this.specified && this.name.indexOf("data-") == 0) {
                    a += a.indexOf("?") >= 0 ? "&" : "?";
                    a += this.name.replace("data-", "") + "=" + encodeURI(this.value);
                }
            });
        });
        openTopWindow(t, a, null, null, this)
        $(this).prop("disenable", false);
        return false;
    });
    $(document).on("click", ".reset", function (event) {
        $(':input', '#searchForm')
            .not(':button,:submit,:reset,:hidden')
            .val('')
            .removeAttr('checked')
            .removeAttr('selected');
    });

    $(document).click(function () { hideContextmenu(); });
    $(document).contextmenu(function () { hideContextmenu(); return false; });

    //测试是否支持本地存储
    function _testIsSupportStorage() {
        try {
            localStorage.setItem('_test_', "1");
            _supportStorageState = !!localStorage.getItem('_test_') ? 1 : 0;
        } catch (ex) {//ios 的safari浏览器无痕模式会禁用本地存储，而且会报错
            _supportStorageState = -2;
        }

        if (_supportStorageState == -2) {
            new openTopMsg('请关闭当前浏览器无痕浏览模式，以保证当前页面正常浏览。');
        } else if (_supportStorageState == 0) {//不支持方法的调用模式，重写常用的几个方法
            try {
                localStorage.setItem = function (key, value) {
                    localStorage[key] = value;
                    localStorage.length++;
                }
                localStorage.getItem = function (key) {
                    return localStorage[key];
                }
                localStorage.removeItem = function (key) {
                    delete localStorage[key];
                    localStorage.length--;
                }

                sessionStorage.setItem = function (key, value) {
                    sessionStorage[key] = value;
                    sessionStorage.length++;
                }
                sessionStorage.getItem = function (key) {
                    return sessionStorage[key];
                }
                sessionStorage.removeItem = function (key) {
                    delete sessionStorage[key];
                    sessionStorage.length--;
                }
            } catch (ex) {
            }
        }
    }
    _testIsSupportStorage();
});

