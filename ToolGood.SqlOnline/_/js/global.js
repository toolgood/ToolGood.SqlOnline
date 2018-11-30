
function getParameter(val) {
    var re = new RegExp("[?&]" + val + "=([^&?]*)", "ig");
    var url = location.search;
    return ((url.match(re)) ? (decodeURI(url.match(re)[0].substr(val.length + 2))) : '');
}

/********************************************  
*  ajax 操作
********************************************/

function PostUrl(url, data, success, error) {
    url = url + (url.indexOf("?") > 0 ? "&" : "?") + "_time=" + new Date().valueOf();
    if (/[?&]_test\b/.test(top.location.search)) { url = url + "&_test=" }
    ShowLoading();

    jQuery.ajax({
        url: url,
        data: data,
        type: "POST",
        success: function (data, textStatus, jqXHR) {
            CloseLoading();
            if (success) {
                success(data, textStatus, jqXHR);
            } else {
                TopShowAlert("提交成功！");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            CloseLoading();
            if (error) {
                error(XMLHttpRequest, textStatus, errorThrown);
            } else {
                TopShowAlert("无法连接！");
            }
        }
    });
}
function GetUrl(url, data, success, error) {
    url = url + (url.indexOf("?") > 0 ? "&" : "?") + "_time=" + new Date().valueOf();
    if (/[?&]_test\b/.test(top.location.search)) { url = url + "&_test=" }
    ShowLoading();

    jQuery.ajax({
        url: url,
        data: data,
        type: "GET",
        success: function (data, textStatus, jqXHR) {
            CloseLoading();
            if (success) {
                success(data, textStatus, jqXHR);
            } else {
                TopShowAlert("提交成功！");
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            CloseLoading();
            if (error) {
                error(XMLHttpRequest, textStatus, errorThrown);
            } else {
                TopShowAlert("无法连接！");
            }
        }
    });
}



/********************************************  
*  Layer弹层 
********************************************/

/**
提示内容 两秒后消失
 * @param {String} msg 提示内容
 * @param {Function} callBack 返回事件
*/
function ShowMsg(msg, callBack) {
    if (msg == null) { msg = "[null]"; }
    layer.msg(msg, { time: 2000 }, function (index) { callBack && callBack(); layer.close(index); });
}
/**
提示内容 两秒后消失
 * @param {String} msg 提示内容
 * @param {Function} callBack 返回事件
*/
function TopShowMsg(msg, callBack) {
    if (window != top) { top.ShowMsg(msg, callBack); } else { ShowMsg(msg, callBack); }
}

/**
 * 显示系统提示,需要点击确认按钮才能关闭
 * @param {String} msg 提示内容
 * @param {String} title [可选]提示标题
 * @param {Function} callBack 返回事件
 * */
function ShowAlert(msg, title, callBack) {
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
function TopShowAlert(msg, title, callBack) {
    if (window != top) {
        top.ShowAlert(msg, title, callBack);
    } else {
        ShowAlert(msg, title, callBack);
    }
}

/**
 * 弹出确认提示框
 * @param {String} [msg] 确认内容
 * @param {String} [title] 确认内容
 * @param {Function} [confirmCallback] 确认后要执行的方法
 * @param {Function} [noConfirmCallback] 取消后要执行的方法
*/
function ShowConfirm(msg, title, confirmCallback, noConfirmCallback) {
    if (msg == null) { msg = "[null]"; }
    if (typeof title === "function") {
        noConfirmCallback = confirmCallback;
        confirmCallback = title;
        title = "信息";
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
 * @param {String} [title] 确认内容
 * @param {Function} [confirmCallback] 确认后要执行的方法
 * @param {Function} [noConfirmCallback] 取消后要执行的方法
*/
function TopShowConfirm(msg, title, confirmCallback, noConfirmCallback) {
    if (window != top) {
        top.ShowConfirm(msg, title, confirmCallback, noConfirmCallback);
    } else {
        ShowConfirm(msg, title, confirmCallback, noConfirmCallback);
    }
}

/**
 * 关闭窗口并刷新上一级页面
*/
function CloseWindowAndReload(index) {
    if (window != top) {
        var index = parent.layer.getFrameIndex(window.name);
        top.CloseWindowAndReload(index);
    } else {
        var $ = jQuery;
        //判断是否有上级的弹层，如果有，就刷新上级弹层
        if ($("#layui-layer" + index).prevAll('.layui-layer').length > 0) {
            var id = $($("#layui-layer" + index).prevAll('.layui-layer')[0]).attr('id').replace('layui-layer', '');
            layer.iframeSrc(id, $('#layui-layer-iframe' + id).attr('src'));
        } else {
            var target = $('.layui-tab-item.layui-show iframe');
            var url = target.attr('src');
            target.attr('src', url).load(function () {
            });
        }
        parent.layer.close(index);
    }
}
/**
 * 关闭窗口
*/
function CloseWindow() {
    var index = parent.layer.getFrameIndex(window.name);
    parent.layer.close(index);
}
/**
 * 关闭窗口
*/
function CloseAllWindows() {
    top.layer.closeAll();
}
function ShowLoading() {
    layer.load(2);
}
function CloseLoading() {
    layer.closeAll("loading");
}

function OpenWindow(title, url, success, end, obj) {
    if (obj != undefined && obj != null) { $(obj).prop("disenable", true); }
    function QueryString(val, url) {
        var re = new RegExp("" + val + "=([^&?]*)", "ig");
        return ((url.match(re)) ? (decodeURI(url.match(re)[0].substr(val.length + 1))) : '');
    }

    url = url + (url.indexOf("?") > 0 ? "&" : "?") + "_time=" + new Date().valueOf();
    if (/[?&]_test\b/.test(top.location.search)) { url = url + "&_test" }

    var w = QueryString('w', url), h = QueryString('h', url);
    w = ((w == null || w == '')) ? ($('body').width() - 100) : w;
    h = ((h == null || h == '')) ? ($('body').height() - 100) : h;
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
        success: function (layero, index) { success && success(layero, index); },
        end: function () { end && end(); if (obj && obj != null) { $(obj).prop("disenable", false); } }
    });
}
function TopOpenWindow(title, url, success, end, obj) {
    if (window != top) {
        top.OpenWindow(title, url, success, end, obj);
    } else {
        OpenWindow(title, url, success, end, obj);
    }
    this.blur();
    return false;
}

function Tpl(template, data) {
    return doT.template($(template).html())(data)
}

/**
 * 初始 openwin 
*/
function Init() {
    var $ = jQuery;
    $('.openwin').unbind("click");
    $('.openwin').click(function (event) {
        event && event.stopPropagation();
        var t = this.title || this.name || null;
        var a = this.href || this.alt;
        TopOpenWindow(t, a, null, null, this)
        return false;
    });
    $('.btn-reset').unbind("click");
    $(".btn-reset").on("click", function () {
        $(':input', '#searchForm')
            .not(':button,:submit,:reset,:hidden')
            .val('')
            .removeAttr('checked')
            .removeAttr('selected');
    });
}

layui.use(['element', 'form', 'laydate', 'code', 'laypage', 'layer'], function () {
    var laypage = layui.laypage
        , element = layui.element
        , layer = layui.layer
        , form = layui.form
        , layedit = layui.layedit
        , laydate = layui.laydate;
});
jQuery(function () { Init(); })
