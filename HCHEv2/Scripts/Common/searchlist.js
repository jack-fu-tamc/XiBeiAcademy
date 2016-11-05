$(document).ready(function () {
    if (document.location.href.indexOf("ShopSearchList") > -1) {
        $(".searchbox ul li").eq(0).find("a").attr("class", "");
        $(".searchbox ul li").eq(1).find("a").attr("class", "style2");
    }

    $(".tab_box_list .hcsy_nextshoplist_next").each(function () {
        var $left = $(this).parent().find(".chinhot_show_next");
        var curIndex = 1;
        var interval = 477;


        $(this).find("em").eq(1).click(function () {
            if (curIndex > 0) {
                $left.stop(false, true).animate({ "margin-left": -curIndex * interval + "px" }, 1000);
                curIndex--;
                $(this).attr("class", "hc_nextnoli");
                $(this).parent().find("em").eq(0).attr("class", "hc_lastnoli");
            }
        });
        $(this).find(".hc_lastli").eq(0).click(function () {
            if (curIndex < 1) {
                $left.stop(false, true).animate({ "margin-left": curIndex * interval + "px" }, 1000);
                curIndex++;
                $(this).attr("class", "hc_lastli");
                $(this).parent().find("em").eq(1).attr("class", "hc_nextli");
            }
        });

    });
    //默认用户选择的服务展示样式
    var $tab_li = $('#zone_sore ul li');

    if ($("#hidStyle").val() == "2") {

        $tab_li.eq(0).removeClass('tab_sore_one').addClass('select_tab_one');

        $tab_li.eq(1).removeClass('tab_sore_two').addClass('select_tab_two');

        $('#hide').addClass("hide").removeClass("list_cont_one");

    } else {

        $tab_li.eq(0).removeClass('select_tab_one').addClass('tab_sore_one');

        $tab_li.eq(1).removeClass('select_tab_two').addClass('tab_sore_two');

        $('#hide').addClass("list_cont_one").removeClass("hide");

    }



    //销量
    $("#zone_sore_text").click(function () {
        //$(this).css({ "background": "#c20201", "color": "#fff" });
        $("#price_zone").find("span").eq(1).attr("class", "");
        var $sp = $(this).find("span").eq(1);
        $sp.attr("class", "sore_img_num");
        var $span_sp = $(this).find("span").eq(0);
        $span_sp.attr("class", "sales_text_next");
        if ($span_sp.attr("class") == "sales_text_next") {
            $("#hidorder").val(1);
            AreaSearch(1);
        }
    });

   
    $(".shop_sore .float_sore").each(function () {
        $(this).find(".zone_sore").click(function () {
            $(this).parent().parent().find(".zone_sore").removeClass("zone_sore_shownext");
            $(this).addClass("zone_sore_shownext");
            $("#address_sore").removeClass("zone_sore_shownext");
            $(".price_text_one").parent().removeClass("zone_sore_shownext");
        });

    });
  

    //综合排序
    $("#zone_next").click(function () {
        $("#price_zone").find("span").eq(1).attr("class", "");
        $("#zone_sore_text").find("span").eq(1).attr("class", "");
    });
    //地区
    $("#address_sore").mouseover(function () {
        $("#drop_sore_address").show();
    });
    $("#zone_go").mouseleave(function () {

        $("#drop_sore_address").hide();
        $("#zone_go .zone_sore").removeClass("give_sore");


    });


    $("#drop_sore_address").mouseleave(function () {
        $(this).hide();
        $("#zone_go .zone_sore").removeClass("give_sore");
    });
    $("#zone_go").click(function () {
        $(this).attr("class", "float_sore");
    });
    //好评率
    $("#idear_good").mouseover(function () {
        $("#drop_idear").show();
    });
    $("#zone_idear").mouseleave(function () {

        $("#drop_idear").hide();
        $("#idear_good .zone_sore").removeClass("give_sore");
    });
    $("#drop_idear").mouseleave(function () {
        $(this).hide();
        $("#idear_good .zone_sore").removeClass("give_sore");
    });
    //店铺类型
    $("#car_next").mouseover(function () {
        $("#drop_car").show();
    });
    $("#zone_car").mouseleave(function () {

        $("#drop_car").hide();
        $("#car_next .zone_sore").removeClass("give_sore");
    });
    $("#drop_car").mouseleave(function () {
        $(this).hide();
        $("#car_next .zone_sore").removeClass("give_sore");
    });

})