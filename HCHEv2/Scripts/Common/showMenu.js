var passportUrl;
$(function () {
    //加入收藏夹
    $("#setfavorite").click(function () {
        var ctrl = (navigator.userAgent.toLowerCase()).indexOf('mac') != -1 ? 'Command/Cmd' : 'CTRL';
        try {
            window.external.addFavorite(window.location.href, '烩车网');
        }
        catch (e) {
            try {
                window.sidebar.addPanel('烩车网', window.location.href, "");
            }
            catch (e) {
                alert("抱歉，您所使用的浏览器无法完成此操作。\n\n加入收藏失败，请使用Ctrl+D进行添加");
            }
        }
        return false;
    });
    //获取passport地址
    $.getJSON("/Common/GetAppsetingValue?paraName=ssoAddress", function (data) {
        passportUrl = data;
    });

    //获取市信息
    $.getJSON("/Common/GetBaseCity?nodetype=1&unid=0", function (data) {
        $(".hc_addrs").html("");
        $.each(data, function (i, item) {
            $(".hc_addrs").append("<p value=\"" + item["unid"] + "\">" + item["city"] + "</p>");
        });
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
        $('.hc_addrs p').click(function () {
            if ($(this).addClass('hc_hot').siblings().removeClass('hc_hot'),
                $('.hc_ddr').text($(this).html())) {
                $('.hc_addrs').fadeOut('5000')
            };
            var province = $(this).attr("value");
            var param = getQueryString("province");
            $("#hidprovince").val(province);
            var currentURL = location.href;
            if (param == "") {
                location.href = "/Home/Index?province=" + province;
            } else {
                if (currentURL.indexOf("AreaPage") == -1) {
                    location.href = currentURL.replace(/(province=).*/, '$1' + province);
                }
                else {
                    location.href = currentURL.replace(/(province=).*?(&)/, '$1' + province + '$2');
                }
            }
        });

        //获取中文地址
        var province = getQueryString("province") == "" ? $("#hidprovince").val() : getQueryString("province");
        $('.hc_addrs p').each(function () {
            var value = $(this).attr("value");
            if (province == value) {
                $(".hc_ddr").html($(this).html());
                $(this).attr("class", "hc_hot");
            }
        });
    });
})

$(function () {
    $("#alisearch-type").each(function () {
        var s = $(this);
        var z = parseInt(s.css("z-index"));
        var dl = $(this).children("dl");
        var dt = $(this).children("dt");
        var dd = $(this).children("dd");

        dt.bind("mouseover", function () { dd.show(); })
        $(this).bind("mouseleave", function () { dd.hide(); })

        dd.find("a").click(function () {
            var value = $(this).html();
            switch (value) {
                case "商家":
                    paramname = "SortId=0&keyword";
                    break;
                case "商品":
                    paramname = "SortId=1&keyword";
                    break;
                case "服务":
                    paramname = "SortId=2&keyword";
                    break;
            }
            dt.html($(this).html()); dd.hide();
        });     //选择效果（如需要传值，可自定义参数，在此处返回对应的“value”值 ）
        $("body").click(function (i) { !$(i.target).parents("#alisearch-type").first().is(s) ? dd.hide() : ""; });
    })
})

//获取url参数
function getQueryString(name) {
    var url = window.location.search.substr(1).toString();
    var param = url.split('&');
    var value = "";
    for (var i = 0; i < param.length; i++) {
        var id = param[i].split('=')[0];
        if (id == name) {
            value = param[i].split('=')[1];
        }
    }
    return decodeURI(value);
}

function gotoRegest() {
    location.href = "/Regest/loginRmz.aspx";
}

function gotoLogin() {
    location.href = "/Login/Login.aspx?mark=2";
}

function chatmsg(corpcomp, memberid) {
    $.ajax({
        async: false,
        url: '/Client/MemberVerify',
        data: {
            'uin': $('[name=uin]').val(),
            'ptsig': $('[name=ptsig]').val(),
            'name': $('[name=name]').val(),
            'tarUin': memberid,
            'tarName': corpcomp
        },
        type: 'post',
        cache: false,
        dataType: 'json',
        success: function (remsg) {
            /* 用户验证后*/
            if (remsg == 1205) {
                window.open("/Client/MemberChat", 'blueidea', 'toolbar=no,location=no, width=600,height=400,menubar=no,scrollbars =no,status=no');
            } else {
                location.href = remsg.url;
            }
        },
        error: function (remsg) {
            alert('网络异常！');
        }
    });
};

//获得选定的类目
$(function () {
    var typeid = getQueryString("typeid");
    var value;
    $("#typelist a").each(function () {
        var url = $(this).attr("href").toString();
        if (url.indexOf(typeid) >= 0) value = $(this).html();
    });

    if (typeid != "") $(".select-no").html(value);
    else $(".select-no").html("暂时没有选择过滤条件");
});

//消息划出特效
$(function () {
    $(".tidings").mouseover(function () {
        $(this).css({
            "border-left": "solid 1px #ccc",
            "border-right": "solid 1px #ccc"
        });
        $("#xiaoxi").show();
    });
    $("#xiaoxi").mouseleave(function () {
        $(this).hide();
        $(".tidings").css({
            "border": "none"
        });
    });
})
//消息划出特效
$(function () {
    $(".tidings").mouseover(function () {
        $(this).css({
            "border-left": "solid 1px #ccc",
            "border-right": "solid 1px #ccc"
        });
        $("#xiaoxi").show();
    });
    $("#xiaoxi").mouseleave(function () {
        $(this).hide();
        $(".tidings").css({
            "border": "none"
        });
    });
})

//顶部公共导航js
//顶部导航右侧下拉列表
$(function () {
    $('.he_jy').mouseover(function () {
        $('.he_jy').css({
            'background': '#5b5b5b'
        });
        $('.hc_jy').show();
    })
    $('.he_jy').mouseleave(function () {
        $('.hc_jy').hide();
        $('.he_jy').css('background', '#000')
    });
    $('.hc_topleftphone').mouseover(function () {
        $('.hc_topleftphone').css({
            'background': '#5b5b5b'
        });
        $('.hc_slidePhone').show();
    })
    $('.hc_slidePhone').mouseleave(function () {
        $('.hc_slidePhone').hide();
        $('.hc_topleftphone').css('background', '#000')
    });
    $('.hc_topleftphone').mouseleave(function () {
        $('.hc_slidePhone').hide();
        $('.hc_topleftphone').css('background', '#000')
    });
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

//车型切换js
$(function () {
    $('.hc_tabhead > ul >li').click(function () {
        $(this).addClass('curs').siblings().removeClass('curs');
        $('.box').hide().eq($('.hc_tabhead > ul > li').index(this)).show();
    });
    $('.pintit > ul > li').click(function () {
        $(this).addClass('clor').siblings().removeClass('clor');
        $('.pinmain').hide().eq($('.pintit > ul > li').index(this)).show();
    });
    $(".pinlist .pintit li").each(function (index) {
        if (index > 0) {
            $(this).click(function () {
                var e = $(this).html();
                showCheckCar(e);
                $(".pinmain").show();
            });
        }
    });
    //获取市信息
    $.getJSON("/Common/GetBaseCity?nodetype=1&unid=0", function (data) {
        $(".hc_addrs").html("");
        $.each(data, function (i, item) {
            $(".hc_addrs").append("<p value=\"" + item["unid"] + "\">" + item["city"] + "</p>");
        });
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

        $('.hc_addrs p').click(function () {
            if ($(this).addClass('hc_hot').siblings().removeClass('hc_hot'),
                $('.hc_ddr').text($(this).html())) {
                $('.hc_addrs').fadeOut('5000')
            };
            var province = $(this).attr("value");
            var param = getQueryString("province");
            var currentURL = location.href;
            if (param == "") {
                location.href = "/Home/Index?province=" + province;
            } else {
                if (currentURL.indexOf("AreaPage") == -1) {
                    location.href = currentURL.replace(/(province=).*/, '$1' + province);
                }
                else {
                    location.href = currentURL.replace(/(province=).*?(&)/, '$1' + province + '$2');
                }
            }
        });

        //获取中文地址
        var province = getQueryString("province") == "" ? $("#hidprovince").val() : getQueryString("province");
        $('.hc_addrs p').each(function () {
            var value = $(this).attr("value");
            if (province == value) {
                $(".hc_ddr").html($(this).html());
                $(this).attr("class", "hc_hot");
            }
        });
    });
})

//设置已读消息
$(function () {
    $(".readordernews").each(function () {
        var CurrentUrl = location.href;
        $(this).click(function () {
            var id = $(this).find("input[name='msg']").val();
            var orderNo = $(this).find("input[name='order']").val();
            $.getJSON("/Common/IsReadOrderNews?id=" + id, function (data) {
                //if (data.result == 0) {
                //    location.href = "/ManageCenter/OrderDetail?OrderNo=" + orderNo;
                //} else {
                //    location.href = passportUrl + "/PassPort/UserLogin?returnUrl=" + encodeURI(CurrentUrl);
                //}
                if (data == "")//出现异常或订单找不到
                {
                    location.href = passportUrl + "/PassPort/UserLogin?returnUrl=" + encodeURI(CurrentUrl);
                }
                else {
                    location.href = data + orderNo;
                }
            });
        });
    });

    $("#ServiceToJoin").click(function () {
        var type = $(this).find("input[type='hidden']").val();
        if (type == 1) {
            alert("您已注册为个人车主用户，加盟服务商需重新注册为企业用户！");
        } else {
            location.href = "http://test.autobobo.com/Manage/Home/shopperCertificationO";
        }
    });
});

//显示选中的品牌
function showCheckCar(e) {
    var str = "";
    $(".pinmain li").hide();
    $(".pinmain li").each(function () {
        var store = $(this).find("input[type='hidden']").val();
        if (e.indexOf(store) != -1) {
            $(this).show();
        }
    });
}

//获取url参数
function getQueryString(name) {
    var url = window.location.search.substr(1).toString();
    var param = url.split('&');
    var value = "";
    for (var i = 0; i < param.length; i++) {
        var id = param[i].split('=')[0];
        if (id == name) {
            value = param[i].split('=')[1];
        }
    }
    return decodeURI(value);
}
//顶部控制
window.onload = function () {
    if ($(window).width() <= 1100) {
        $('.hc_topWelcome').css('width', '1040px');
        $('.adclouse').removeClass('adclouse').css({ 'margin-left': '970px', 'margin-top': '-100px' });
        $('.stores_sets').find('img').addClass('container');
    } else {
        $('.hc_topWelcome').css('width', '1170px');
    }
}
//图片预加载
$(function () {
    $('img').on('error', function () {
        $(this).prop('src', 'http://pic.autobobo.com/Default.png')
    })
})