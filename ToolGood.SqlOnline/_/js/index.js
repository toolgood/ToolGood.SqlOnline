
layui.use(['element', 'layer'], function () {
    var element = layui.element, layer = layui.layer;
    $(document).bind("selectstart", function () { return false; });

    element.init();

    //菜单
    element.on('tab(docDemoTabBrief)', function (data) {
        $(".layui-side .layui-nav dd.layui-this").removeClass('layui-this');
        $(".layui-side .layui-nav dd a[data-index='" + $('.layui-tab .layui-this').attr('lay-id') + "']").parent().addClass('layui-this');
    });
    $(".layui-nav dd a").each(function (index) {
        if (!$(this).attr('data-index')) {
            $(this).attr('data-index', index);
        }
    });
    $('body').on('click', '.layui-side .layui-nav dd a', function (e) {
        var id = $(this).attr('data-index');
        var href = $(this).attr('href');
        var name = $(this).text();

        if ($(".layui-tab li[lay-id='" + id + "']").length > 0) {
            //选中
            element.tabChange('docDemoTabBrief', id);
        } else {
            setAddTab(id, name, href);
        }
        e.preventDefault();
    });
    //双击刷新
    $('body').on('dblclick', '.layui-tab .layui-this', function (data) {
        var target = $('.layui-tab-iframe[data-id="' + $(this).attr('lay-id') + '"]');
        var url = target.attr('src');
        target.attr('src', url).load(function () {
        });
    });

});
function setAddTab(id, name, href) {
    if (/[?&]_test\b/.test(top.location.search)) {
        href = href + (href.indexOf("?") > 0 ? "&" : "?") + "_test=";
    }
    layui.use(['element', 'layer'], function () {
        var element = layui.element, layer = layui.layer;
        if ($(".layui-tab li[lay-id='" + id + "']").length > 0) {
        } else {
            //新增
            element.tabAdd('docDemoTabBrief', {
                title: name
                , content: '<iframe class="layui-tab-iframe" width="100%" height="100%" src="' + href + '" frameborder="0" name="' + href + '" data-id="' + id + '"></iframe>'
                , id: id
            });
        }
        //选中
        element.tabChange('docDemoTabBrief', id);
    });
}
function insertAddTab(id, name, href) {
    if (/[?&]_test\b/.test(top.location.search)) {
        href = href + (href.indexOf("?") > 0 ? "&" : "?") + "_test=";
    }
    layui.use(['element', 'layer'], function () {
        var element = layui.element, layer = layui.layer;
        if ($(".layui-tab li[lay-id='" + id + "']").length > 0) {
        } else {
            element.tabInsert('docDemoTabBrief', {
                title: name
                , content: '<iframe class="layui-tab-iframe" width="100%" height="100%" src="' + href + '" frameborder="0" name="' + href + '" data-id="' + id + '"></iframe>'
                , id: id
            });
        }
        element.tabChange('docDemoTabBrief', id);
    });
}