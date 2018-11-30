
function resize() {
    var bh = $('body').height() - $('.data-header').outerHeight(true) - $('.pager').outerHeight(true);
    $('.data-content').height(bh);
    $(".data-content>div").first().height(bh);
}
layui.use(['form', 'laydate', 'code', 'laypage', 'layer'], function () {
    var laypage = layui.laypage
        , layer = layui.layer
        , form = layui.form
        , layedit = layui.layedit
        , laydate = layui.laydate;

    //laydate.render({
    //	elem: '.daterange',range: true
    //});
    //laydate.render({
    //	elem: '.date'
    //});
    lay('.daterange').each(function () {
        laydate.render({
            elem: this
            , range: true
            , trigger: 'click'
        });
    });
    lay('.date').each(function () {
        laydate.render({
            elem: this
            , trigger: 'click'
        });
    });

    laypage.render({
        elem: 'pager'
        , curr: $('#searchForm #page').val()
        , count: $('#searchForm #count').val(), limit: $('#searchForm #limit').val()
        , limits: [10, 20, 30, 50, 100]
        , layout: ['count', 'prev', 'page', 'next', 'limit', 'skip', 'limits']
        , jump: function (obj, first) {
            if (!first) {
                console.log(obj);
                $("#searchForm #page").val(obj.curr);
                $("#searchForm #limit").val(obj.limit);
                $("#searchForm").submit();
            }
        }
    });
    resize();
    $(".gridTable").tableHeadFixer();
    $(window).resize(function () {
        resize();
    });


});