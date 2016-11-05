/// <reference path="authentication.js" />
//顶部公共导航js
//顶部导航右侧下拉列表
$(function () {

    $(".hc_topright>li").hover(function () {
        $(this).css("background", "#5b5b5b");
    }, function () {
        $(this).css('background', '#000');
    })
    $(".buyerNav").hover(function () {
        $(this).find("div").show();
    }, function () {
        $(this).find("div").hide();
    })
});

$(function () {
    $('.hc_topwidth').mouseover(function () {
        $('.hc_kff').show();
        $('.hc_topwidth').css({
            'background': '#5b5b5b'
        });
    });
    $('.hc_kff').mouseleave(function () {
        $('.hc_kff').hide();
        $('.hc_topwidth').css({
            'background': '#000'
        });
    });
    $('.hc_topwidth').mouseleave(function () {
        $('.hc_kff').hide();
        $('.hc_topwidth').css({
            'background': '#000'
        });
    });
});
//顶部导航下拉js结束

//地区导航
$(function () {
    $('.hc_topLi').mouseover(function () {
        $(this).css({
            'background': '#5b5b5b'
        });
        $('.hc_addrs').show();
    });

    $('.hc_addrs').mouseleave(function () {
        $('.hc_topLi').css({
            'background': '#000',
            'color': '#000'
        });
        $('.hc_addrs').hide();
        $('.hc_topLi a').css('color', '#fff')
    });

    $('.hc_topLi').mouseleave(function () {
        $('.hc_topLi').css({
            'background': '#000',
            'color': '#000'
        });
        $('.hc_addrs').hide();
        $('.hc_topLi a').css('color', '#fff')
    })
});
$(function () {
    $('.hc_addrs p').click(function () {
        if ($(this).addClass('hc_hot').siblings().removeClass('hc_hot'),
            $('.hc_ddr').text($(this).html())) {
            $('.hc_addrs').fadeOut('5000')
        }
    })
});
//地区导航结束

//消息
$(function () {
    $("#show_name").mouseover(function () {
        $("#show_fouces").show();
        $("#show_name").addClass("change_name");
    });

    $("#show_fouces").mouseleave(function () {
        $(this).hide();
        $("#show_name").removeClass("change_name");
    });

    $(".infornation").mouseleave(function () {
        $('#show_fouces').hide();
        $("#show_name").removeClass("change_name");
    });
});
//消息结束

//导航搜索下拉js
$(function () {
    $(".select").each(function () {
        var s = $(this);
        var z = parseInt(s.css("z-index"));
        var dt = $(this).children("dt");
        var dd = $(this).children("dd");
        var _show = function () {
            dd.slideDown(200);
            dt.addClass("cur");
            s.css("z-index", z + 1);
        }; //展开效果
        var _hide = function () {
            dd.slideUp(200);
            dt.removeClass("cur");
            s.css("z-index", z);
        }; //关闭效果
        dt.click(function () {
            dd.is(":hidden") ? _show() : _hide();
        });
        dd.find("a").click(function () {
            dt.html($(this).html());
            _hide();
        }); //选择效果（如需要传值，可自定义参数，在此处返回对应的“value”值 ）
        $("body").click(function (i) {
            !$(i.target).parents(".select").first().is(s) ? _hide() : "";
        });
    })
});
//导航搜索下拉js结束




$(function () {

    $(".hc_nav p").click(function () {

        var _this = this;
        var ul = $(_this).next();
        if (ul.css("display") == "none") {
            ul.slideDown("fast");
            ul.parent().find(".hc_set").addClass("hc_setlisrf");
        } else {
            ul.slideUp("fast");
            ul.parent().find(".hc_set").removeClass("hc_setlisrf");
        }
    });

    $(".hc_nav li").click(function () {
        var _this = this;
        var Curentp = $(_this).parent().prev();
        var text = $(_this).text();
        Curentp.text(text);
        $(".hc_new").hide();
        $("p").removeClass("hc_setlisrf");
        checkExptype($("#type"));
    });

});
