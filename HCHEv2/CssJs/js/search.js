//页面全局搜索代码
$(function () {
    $("#keywords").val(getQueryString("keywords"));

    if ($("#hidpage").val() == "index") {
        $(".topnavad").show();
    }
    else {
        $(".topnavad").hide();
        $('.hc_topNav').css('margin-top', '0');
        $('.hc_clear').css('height', '40' + 'px');
        $('.fixed_nav').css({
            'top': '40' + 'px',
            'height': '95' + '%'
        });
    }

    $(".searchbox ul li").click(function () {
        var index = $(this).index();
        if (index == 0) {
            $("li").find("a").removeClass("style2");
            $(this).find("a").addClass("style1");
            $("#hidSearchType").val(1);
        }
        if (index == 1) {
            $("li").find("a").removeClass("style1");
            $(this).find("a").addClass("style2");
            $("#hidSearchType").val(2);
        }
        var index = $(this).index();
        $(".bodys p").eq(index).show().siblings().hide();
    });

    $("#btnsearch").click(function () {
        var type = $("#hidSearchType").val();
        var province = $("#hidprovince").val();
        var keywords = "";
        if (type == "1") {
            keywords = $("#keywords").val() == "" ? "雨刮片" : $("#keywords").val()
            location.href = "/Area/SearchPage?province=" + province + "&keywords=" + keywords;
        } else {
            keywords = $("#keywords").val();
            location.href = "/Area/ShopSearchList?province=" + province + "&keywords=" + keywords;
        }
    });
});

// 首页幻灯js
$(function () {
    var numpic = $('#slides li').size() - 1;
    var nownow = 0;
    var inout = 0;
    var TT = 0;
    var SPEED = 5000;

    $('#slides li').eq(0).siblings('li').css({ 'display': 'none' });

    var ulstart = '<ul id="pagination">',
		ulcontent = '',
		ulend = '</ul>';
    ADDLI();
    var pagination = $('#pagination li');
    var paginationwidth = $('#pagination').width();
    $('#pagination').css('margin-left', (470 - paginationwidth))

    pagination.eq(0).addClass('current')

    function ADDLI() {
        //var lilicount = numpic + 1;
        for (var i = 0; i <= numpic; i++) {
            ulcontent += '<li>' + '<a href="#">' + (i + 1) + '</a>' + '</li>';
        }

        $('#slides').after(ulstart + ulcontent + ulend);
    }

    pagination.on('click', DOTCHANGE)

    function DOTCHANGE() {
        var changenow = $(this).index();

        $('#slides li').eq(nownow).css('z-index', '-1000');
        $('#slides li').eq(changenow).css({ 'z-index': '-1800' }).show();
        pagination.eq(changenow).addClass('current').siblings('li').removeClass('current');
        $('#slides li').eq(nownow).fadeOut(400, function () { $('#slides li').eq(changenow).fadeIn(500); });
        nownow = changenow;
    }

    pagination.mouseenter(function () {
        inout = 1;
    })

    pagination.mouseleave(function () {
        inout = 0;
    })

    function GOGO() {
        var NN = nownow + 1;

        if (inout == 1) {
        } else {
            if (nownow < numpic) {
                $('#slides li').eq(nownow).css('z-index', '-1000');
                $('#slides li').eq(NN).css({ 'z-index': '-1800' }).show();
                pagination.eq(NN).addClass('current').siblings('li').removeClass('current');
                $('#slides li').eq(nownow).fadeOut(400, function () { $('#slides li').eq(NN).fadeIn(500); });
                nownow += 1;
            } else {
                NN = 0;
                $('#slides li').eq(nownow).css('z-index', '-1000');
                $('#slides li').eq(NN).stop(true, true).css({ 'z-index': '-1800' }).show();
                $('#slides li').eq(nownow).fadeOut(400, function () { $('#slides li').eq(0).fadeIn(500); });
                pagination.eq(NN).addClass('current').siblings('li').removeClass('current');

                nownow = 0;
            }
        }
        TT = setTimeout(GOGO, SPEED);
    }

    TT = setTimeout(GOGO, SPEED);
});

//侧边导航定位脚本
$(window).scroll(function () {
    var scrollTop = $(document).scrollTop();
    var documentHeight = $(document).height();
    var windowHeight = $(window).height();
    var contentItems = $("#content").find(".item");
    var currentItem = "";

    if (scrollTop + windowHeight == documentHeight) {
        currentItem = "#" + contentItems.last().attr("id");
    } else {
        contentItems.each(function () {
            var contentItem = $(this);
            var offsetTop = contentItem.offset().top;
            if (scrollTop > offsetTop - 100) {//此处的200视具体情况自行设定，因为如果不减去一个数值，在刚好滚动到一个div的边缘时，菜单的选中状态会出错，比如，页面刚好滚动到第一个div的底部的时候，页面已经显示出第二个div，而菜单中还是第一个选项处于选中状态
                currentItem = "#" + contentItem.attr("id");
            }
        });
    }
    if (currentItem != $("#menu").find(".current").attr("href")) {
        $("#menu").find(".current").removeClass("current");
        $("#menu").find("[href=" + currentItem + "]").addClass("current");
    }
});

//定位楼层
function gotofloor(thiz) {
    var pos = $("#" + thiz).offset().top; // 获取该点到头部的距离
    $("html,body").animate({
        scrollTop: pos - 60
    }, 400);
}
//顶部搜索
$(window).scroll(function () {
    var currentTOP = $(window).scrollTop();
    if (currentTOP > 300) {
        $("#hc_divSearch").show();
    } else {
        $("#hc_divSearch").hide();
    }
});
//搜索框JS
(function ($) {
    var selects = $('select');//获取select

    for (var i = 0; i < selects.length; i++) {
        createSelect(selects[i], i);
    }

    function createSelect(select_container, index) {
        //创建select容器，class为select_box，插入到select标签前
        var tag_select = $('<div></div>');//div相当于select标签
        tag_select.attr('class', 'select_box');
        tag_select.insertBefore(select_container);

        //显示框class为select_showbox,插入到创建的tag_select中
        var select_showbox = $('<div></div>');//显示框
        select_showbox.css('cursor', 'pointer').attr('class', 'select_showbox').appendTo(tag_select);

        //创建option容器，class为select_option，插入到创建的tag_select中
        var ul_option = $('<ul></ul>');//创建option列表
        ul_option.attr('class', 'select_option');
        ul_option.appendTo(tag_select);
        createOptions(index, ul_option);//创建option

        //点击显示框
        tag_select.toggle(function () {
            ul_option.show();
            ul_option.parent().find(".select_showbox").addClass("active");
        }, function () {
            ul_option.hide();
            ul_option.parent().find(".select_showbox").removeClass("active");
        });

        var li_option = ul_option.find('li');
        li_option.on('click', function () {
            $(this).addClass('selected').siblings().removeClass('selected');
            var value = $(this).text();
            select_showbox.text(value);
            ul_option.hide();
        });

        li_option.hover(function () {
            $(this).addClass('hover').siblings().removeClass('hover');
        }, function () {
            li_option.removeClass('hover');
        });
    }

    function createOptions(index, ul_list) {
        //获取被选中的元素并将其值赋值到显示框中
        var options = selects.eq(index).find('option'),
			selected_option = options.filter(':selected'),
			selected_index = selected_option.index(),
			showbox = ul_list.prev();
        showbox.text(selected_option.text());

        //为每个option建立个li并赋值
        for (var n = 0; n < options.length; n++) {
            var tag_option = $('<li></li>'),//li相当于option
				txt_option = options.eq(n).text();
            tag_option.text(txt_option).appendTo(ul_list);

            //为被选中的元素添加class为selected
            if (n == selected_index) {
                tag_option.attr('class', 'selected');
            }
        }
    }
})(jQuery);

//返回顶部
$(function () {
    //$('#backTop').click(function (event) {
    //    event.preventDefault();
    //    $('html, body').animate({ scrollTop: 0 }, 300);
    //});
    $(window).scroll(function () {
        if ($(window).scrollTop() > 0) {
            $('#backTop').slideDown("500");
        } else {
            $('#backTop').slideUp("500");
        }
    });
})