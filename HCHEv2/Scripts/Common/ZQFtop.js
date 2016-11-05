$(function () {
    $('.tabPanel ul li').click(function () {
        $(this).addClass('hit').siblings().removeClass('hit');
        $('.panes>div:eq(' + $(this).index() + ')').show().siblings().hide();
    })
})
//返回顶部
//$('#backTop').click(function (event) {
//    //event.preventDefault();
//    $('html, body').animate({ scrollTop: 0 }, 300);

//});

//$(window).scroll(function () {
//    if ($(window).scrollTop() > 0) {
//        $('#backTop').slideDown();
//    } else {
//        $('#backTop').slideUp();
//    }
//});

//列表切换
$(function () {
    var url;
    var currstyle = $("#shop-list").attr("class");
    var style = getQueryString("style");
    var page = getQueryString("no") == "" ? "1" : getQueryString("no");
    var sortId = getQueryString("SortId");
    
    if (sortId == "0") {
       $("#prices").hide();
       $("#sortSale").hide();
       $("#xianshi").hide();
       $(".prices").hide();
       $("#box1").hide();
       $("#box2").hide();
       $("#shop-list").removeClass("shop-cross").addClass("Merchant-right-list");
    }
    else {
        if (style == "shop-right-list") {
            $("#shop-list").removeClass("shop-cross").addClass("shop-right-list");
            $("#box1").removeClass("cures");
            $("#box1").addClass("curesimg");
            $("#box2").removeClass("hc_imgq_cure");
            $("#box2").addClass("hc_img_cures");
            $("#box1").show();
            $("#box2").show();
        }
        if (style == "shop-cross") {
            $("#shop-list").removeClass("shop-cross").addClass("shop-cross");
            $("#box1").addClass("cures");
            $("#box1").removeClass("curesimg");
            $("#box1").show();
            $("#box2").show();
        }
        $("#sortSale").show();
        $("#xianshi").show();
    }

    $("#box1").click(function () {
        $("#shop-list").removeClass("shop-right-list").addClass("shop-cross");
        var url = window.location.href.split("?")[0];
        var no = window.location.href.split("?")[1];
        var style = $("#shop-list").attr("class");
        if (no == undefined) {
            location.href = url + "?no=" + page + "&style=shop-cross";
        }
        else {
            url = window.location.href.split("no=")[0];
            no = window.location.href.split("no=")[1];
            if (no == undefined)
                location.href = url + "&no=" + page + "&style=shop-cross";
            else
                location.href = url + "no=" + page + "&style=shop-cross";
        }
    });
    $("#box2").click(function () {
        $("#shop-list").removeClass("shop-cross").addClass("shop-right-list");
        var url = window.location.href.split("?")[0];
        var no = window.location.href.split("?")[1];
        var style = $("#shop-list").attr("class");
        if (no == undefined) {
            location.href = url + "?no=" + page + "&style=shop-right-list";
        }
        else {
            url = window.location.href.split("no=")[0];
            no = window.location.href.split("no=")[1];
            if (no == undefined)
                location.href = url + "&no=" + page + "&style=shop-right-list";
            else
                location.href = url + "no=" + page + "&style=shop-right-list";
        }
        
    });
});
//end
$(function () {
    $("#toggleLoginL").toggle(function () {
        document.getElementById("loginR").style.display = "none";
        document.getElementById("loginL").style.display = "";
        $("#loginL").parent("div").animate({ height: 105 }, 520);
        $("#loginL").animate({ marginTop: 0 }, 500);
        $(this).blur();
    }, function () {
        document.getElementById("loginR").style.display = "none";
        document.getElementById("loginL").style.display = "";
        $("#loginL").parent("div").animate({ height: 0 }, 500);
        $("#loginL").animate({ marginTop: -105 }, 520);
        $(this).blur();
    });
    $("#closeLoginL").click(function () {
        document.getElementById("loginR").style.display = "none";
        document.getElementById("loginL").style.display = "";
        $("#loginL").parent("div").animate({ height: 0 }, 500);
        $("#loginL").animate({ marginTop: -105 }, 520);
    });
})
$(function () {

    $("#toggleLoginR").toggle(function () {
        document.getElementById("loginL").style.display = "none";
        document.getElementById("loginR").style.display = "";
        $("#loginR").parent("div").animate({ height: 105 }, 520);
        $("#loginR").animate({ marginTop: 0 }, 500);
        $(this).blur();
    }, function () {
        document.getElementById("loginL").style.display = "none";
        document.getElementById("loginR").style.display = "";
        $("#loginR").parent("div").animate({ height: 0 }, 500);
        $("#loginR").animate({ marginTop: -105 }, 520);
        $(this).blur();
    });
    $("#closeLoginR").click(function () {
        document.getElementById("loginL").style.display = "none";
        document.getElementById("loginR").style.display = "";
        $("#loginR").parent("div").animate({ height: 0 }, 500);
        $("#loginR").animate({ marginTop: -105 }, 520);
    });
})
$(function () {
    $(".select").each(function () {
        var s = $(this);
        var z = parseInt(s.css("z-index"));
        var dt = $(this).children("dt");
        var dd = $(this).children("dd");
        var _show = function () { dd.slideDown(200); dt.addClass("cur"); s.css("z-index", z + 1); };   //展开效果
        var _hide = function () { dd.slideUp(200); dt.removeClass("cur"); s.css("z-index", z); };    //关闭效果
        dt.click(function () { dd.is(":hidden") ? _show() : _hide(); });
        dd.find("a").click(function () { dt.html($(this).html()); _hide(); });     //选择效果（如需要传值，可自定义参数，在此处返回对应的“value”值 ）
        $("body").click(function (i) { !$(i.target).parents(".select").first().is(s) ? _hide() : ""; });
    })
})

//加入购物车方法
function addSoldCart(product_no) {
    var url = "/PublicAjax/ForCollectionMnage.ashx?action=addsoldcart&product_no=" + product_no + "&random=" + Math.random();
    $.ajax({
        type: "POST",
        url: url,
        dataType: "html",
        success: function (result) {
            if (result == "0") {
                alert("加入购物车成功！");
                location.reload();
            }
            if (result == "1") {
                alert("商家库存不足！");
            }
            if (result == "2") {
                window.location.href = "/Login/Login.aspx?BackURL=" + encodeURIComponent(location.href);
            }
        }
    });
}

//收藏店铺方法
function addMyCollect(memberid) {
    var url = "/PublicAjax/ForCollectionMnage.ashx?action=addcollect&memberid=" + memberid + "&&collect_type=2&random=" + Math.random();
    $.ajax({
        type: "POST",
        url: url,
        dataType: "html",
        success: function (result) {
            if (result == "0")
                alert('店铺已收藏成功！');
            else if (result == "1") {
                alert('店铺已在您收藏夹中！');
            }
            else{
                window.location.href = "/Login/Login.aspx?BackURL=" + encodeURIComponent(location.href);
            }
        }
    });
}