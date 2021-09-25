 

//左侧菜单收起展开
(function () {
    var isPaceUp = false;
    var jApp = $('#app')
    if (isPaceUp) {
        onPackHandler(true)
        setTimeout(function () {
            jApp.addClass('inited');
        }, 200)
    } else {
        jApp.addClass('inited');
    }

    $('.pack').click(function () {
        onPackHandler(!$('#app').is('.pack-up'))
    });
    //左侧
    function onPackHandler(isPaceUp) {
        var jThis = $('.pack');

        if (isPaceUp) {
            layui.data('T_global', {
                key: 'isPaceUp',
                value: true
            });
            jThis.removeClass('pack-left').addClass('pack-right')
            jApp.addClass('pack-up')
        } else {
            layui.data('T_global', {
                key: 'isPaceUp',
                value: false
            });
            jThis.removeClass('pack-right').addClass('pack-left')
            jApp.removeClass('pack-up')
        }
        if (window.onresize) {
            window.onresize();
        }
    }
})()


$(function () {
    setInterval(function () {
        var cookie = $.cookie();
        if (cookie == undefined) {
            console.log("Cookie invalidation！");
            top.location.href = "/"; return;
        }
        var data = cookie.a;
        if (data == undefined) {
            console.log("Cookie invalidation！");
            top.location.href = "/"; return;
        }
    }, 1000);
})
