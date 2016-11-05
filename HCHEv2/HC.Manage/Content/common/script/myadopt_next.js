//顶部导航右侧下拉列表
//$(function () {
//    $('.he_jy').mouseover(function () {
//        $('.he_jy').css({
//            'background': '#5b5b5b'
//        });
//        $('.hc_jy').show();
//    })
//    $('.hc_jy').mouseleave(function () {
//        $('.hc_jy').hide();
//        $('.he_jy').css('background', '#000')
//    });
//    $('.he_jy').mouseleave(function () {
//        $('.hc_jy').hide();
//        $('.he_jy').css('background', '#000')
//    });
//});




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

/*点击显示*/
$(document).ready(function () {
    $("#hc_GJseek").click(function () {
        $("#hc_GJseek i").toggleClass("seekico_lis");
    });
});
function domeseek() {
    var seek1 = document.getElementById("seek1");
    seek1.style.display = (seek1.style.display == "none" ? "" : "none");
    var seek2 = document.getElementById("seek2");
    seek2.style.display = (seek2.style.display == "none" ? "" : "none");
}
function indent(ele) {
    var val = $(ele).text();
    var newInfo = getCarCategoryDate(val.toLowerCase());
    //颜色
    var lis = $("a[name=indent]");
    var isli = $("a[name=indent_fa]");
    for (var i = 0; i < lis.length; i++) {
        $(lis[i]).css({ "color": "#000" });
        $(isli[i]).css({ "color": "#5E5E5E" });
    }
    var obj = $(ele);
    obj.css({ "color": "#c02021" });

}
//左侧导航点击样式变化
$(function () {
    $(".hche_leftnav ul li a").click(function () {
        $(this).addClass("hche_onclickchange");
        $(this).parent().find("i").addClass("hche_onclickmenuimg");
        $(this).parent().siblings().find("a").removeClass("hche_onclickchange");
        $(this).parent().siblings().find("i").removeClass("hche_onclickmenuimg");
    });
})

/*全选*/

//全选/取消全选
$(function () {
    $('#quanxuan').click(function () {
        if ($(this).attr("checked") == "checked") {
            $("input[name='abc']").attr("checked", 'true');
            $("input[name='abc']").parent().parent().remove("title_list").addClass("title_naxt");
            $(".number_one").addClass("number_two");
        } else {
            $("input[name='abc']").removeAttr("checked");
            $("input[name='abc']").parent().parent().removeClass("title_naxt");
            $(".number_one").removeClass("number_two");
        }

    });
    $("input[name='abc']").click(function () {
        if ($(this).attr("checked") == "checked") {

            $(this).parent().parent().addClass("title_naxt");
            $(".number_one").addClass("number_two");
        } else {

            $(this).parent().parent().removeClass("title_naxt");
            $(".number_one").removeClass("number_two");
        }

    });

    /*订单列表右侧分页加减*/
    $(document).ready(function () {
        $("#add").click(function () {
            var n = $("#num").html();
            var num = parseInt(n) + 1;
            var count = $("#count").html();
            if (num - 1 == count) { return false; }
            $("#num").html(num);
        });

        $("#min").click(function () {
            var n = $("#num").html();
            var num = parseInt(n) - 1;
            if (num == 0) { return false; }
            $("#num").html(num);
        });

    });
});

$(function () {
    $.fn.manhuaDate = function (options) {
        var defaults = {
            Event: "click",		//插件绑定的响应事件
            Left: 0,				//弹出时间停靠的左边位置
            Top: 22,				//弹出时间停靠的上边位置
            fuhao: "-",			//日期之间的连接符号
            isTime: false,			//是否开启时间值默认为false
            beginY: 1949,			//年份的开始默认为1949
            endY: 2049				//年份的结束默认为2049
        };
        var options = $.extend(defaults, options);
        var stc;
        if ($("#calender").length <= 0) {
            $("body").prepend("<div class='calender'><div class='calenderContent'><div class='calenderTable'><div class='getyear'><a class='preMonth' id='preMonth'>上一月</a><select id='year'></select><select id='month'></select><a class='nextMonth' id='nextMonth'>下一月</a></div><div class='tablebg'><table id='calender' class='calendertb' cellpadding='0' cellspacing='1'><tr bgcolor='#D6D6D6'><th class='weekend'>日</th><th>一</th><th>二</th><th>三</th><th>四</th><th>五</th><th class='weekend noborder'>六</th></tr><tr><td class='weekend2'></td><td></td><td></td><td></td><td></td><td></td><td class='weekend2 noborder'></td></tr><tr><td class='weekend2'></td><td></td><td></td><td></td><td></td><td></td><td class='weekend2 noborder'></td></tr><tr><td class='weekend2'></td><td></td><td></td><td></td><td></td><td></td><td class='weekend2 noborder'></td></tr><tr><td class='weekend2'></td><td></td><td></td><td></td><td></td><td></td><td class='weekend2 noborder'></td></tr><tr><td class='weekend2'></td><td></td><td></td><td></td><td></td><td></td><td class='weekend2'></td></tr><tr><td class='weekend2'></td><td></td><td></td><td></td><td></td><td></td><td class='weekend2'></td></tr></table></div></div></div></div>");
        }
        var $mhInput = $(this);
        var isToday = true;//是否为今天默认为是
        var date = new Date();//获得时间对象
        var nowYear = date.getFullYear();//获得当前年份
        var nowMonth = date.getMonth() + 1;//获得当前月份
        var today = date.getDate();//获得当前天数
        var nowWeek = new Date(nowYear, nowMonth - 1, 1).getDay();//获得当前星期
        var nowLastday = getMonthNum(nowMonth, nowYear);//获得最后一天
        //年、月下拉框的初始化
        for (var i = options.beginY; i <= options.endY; i++) {
            $("<option value='" + i + "'>" + i + "年</option>").appendTo($("#year"));
        }
        for (var i = 1; i <= 12; i++) {
            $("<option value='" + i + "'>" + i + "月</option>").appendTo($("#month"));
        }
        ManhuaDate(nowYear, nowMonth, nowWeek, nowLastday);//初始化为当前日期
        //上一月绑定点击事件
        $("#preMonth").click(function () {
            isToday = false;
            var year = parseInt($("#year").val());
            var month = parseInt($("#month").val());
            month = month - 1;
            if (month < 1) {
                month = 12;
                year = year - 1;
            }
            if (nowYear == year && nowMonth == month) {
                isToday = true;
            }
            var week = new Date(year, month - 1, 1).getDay();
            var lastday = getMonthNum(month, year);
            ManhuaDate(year, month, week, lastday);
        });
        //年下拉框的改变事件
        $("#year").change(function () {
            isToday = false;
            var year = parseInt($(this).val());
            var month = parseInt($("#month").val());
            if (nowYear == year && nowMonth == month) {
                isToday = true;
            }
            var week = new Date(year, month - 1, 1).getDay();
            var lastday = getMonthNum(month, year);
            ManhuaDate(year, month, week, lastday);
        });
        //月下拉框的改变事件
        $("#month").change(function () {
            isToday = false;
            var year = parseInt($("#year").val());
            var month = parseInt($(this).val());
            if (nowYear == year && nowMonth == month) {
                isToday = true;
            }
            var week = new Date(year, month - 1, 1).getDay();
            var lastday = getMonthNum(month, year);
            ManhuaDate(year, month, week, lastday);
        });
        //下一个月的点击事件
        $("#nextMonth").click(function () {
            isToday = false;
            var year = parseInt($("#year").val());
            var month = parseInt($("#month").val());

            month = parseInt(month) + 1;
            if (parseInt(month) > 12) {
                month = 1;
                year = parseInt(year) + 1;
            }
            if (nowYear == year && nowMonth == month) {
                isToday = true;
            }
            var week = new Date(year, month - 1, 1).getDay();
            var lastday = getMonthNum(month, year);
            ManhuaDate(year, month, week, lastday);
        });

        //初始化日历
        function ManhuaDate(year, month, week, lastday) {
            $("#year").val(year);
            $("#month").val(month)
            var table = document.getElementById("calender");
            var n = 1;
            for (var j = 0; j < week; j++) {
                table.rows[1].cells[j].innerHTML = "&nbsp;"
            }
            for (var j = week; j < 7; j++) {
                if (n == today && isToday) {
                    table.rows[1].cells[j].className = "tdtoday";
                } else {
                    table.rows[1].cells[j].className = "";
                }
                table.rows[1].cells[j].innerHTML = n;
                n++;
            }
            for (var i = 2; i < 7; i++) {
                for (j = 0; j < 7; j++) {
                    if (n > lastday) {
                        table.rows[i].cells[j].innerHTML = "&nbsp"
                    }
                    else {
                        if (n == today && isToday) {
                            table.rows[i].cells[j].className = "tdtoday";
                        } else {
                            table.rows[i].cells[j].className = "";
                        }
                        table.rows[i].cells[j].innerHTML = n;
                        n++;
                    }
                }
            }
        }
        //获得月份的天数
        function getMonthNum(month, year) {
            month = month - 1;
            var LeapYear = ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0) ? true : false;
            var monthNum;
            switch (parseInt(month)) {
                case 0:
                case 2:
                case 4:
                case 6:
                case 7:
                case 9:
                case 11:
                    monthNum = 31;
                    break;
                case 3:
                case 5:
                case 8:
                case 10:
                    monthNum = 30;
                    break;
                case 1:
                    monthNum = LeapYear ? 29 : 28;
            }
            return monthNum;
        }
        //每一列的悬挂事件改变当前样式
        $("#calender td:not(.tdtoday)").hover(function () {
            $(this).addClass("hover")
        }, function () {
            $(this).removeClass("hover");
        });
        //点击时间列表事件
        $("#calender td").die().live("click", function () {
            var dv = $(this).html();
            if (dv != "&nbsp;") {
                var str = "";
                if (options.isTime) {
                    var nd = new Date();
                    str = $("#year").val() + options.fuhao + $("#month").val() + options.fuhao + dv + " " + nd.getHours() + ":" + nd.getMinutes() + ":" + nd.getSeconds();
                } else {
                    str = $("#year").val() + options.fuhao + $("#month").val() + options.fuhao + dv;
                }
                $("input.dateVisited").val(str);
                $("input.dateVisited").removeClass('dateVisited')
                $(".calender").hide();
            }
        });
        //文本框绑定事件
        $mhInput.live(options.Event, function (e) {
            $(this).addClass("dateVisited");
            if (stc) {
                clearTimeout(stc);//清除定时器
            }
            var iof = $(this).offset();
            $(".calender").css({ "left": iof.left + options.Left, "top": iof.top + options.Top });
            $(".calender").show();
        });
        //当鼠标离开控件上面的时候延迟3秒关闭
        $(".calender").live("mouseleave", function () {
            stc = setTimeout(function () {
                $(".calender").hide();
                clearTimeout(stc);
            }, 3000);
        });
        //当鼠标移到控件上面的时候显示
        $(".calender").live("mousemove", function () {
            if (stc) {
                clearTimeout(stc);//清除定时器
            }
            $(this).show();
        });
        //点击年选择下拉框的时候清除定时器阻止控件层关闭
        $("#year").die().live("click", function () {
            if (stc) {
                clearTimeout(stc);//清除定时器
            }
        });
        //点击月选择下拉框的时候清除定时器阻止控件层关闭
        $("#month").die().live("click", function () {
            if (stc) {
                clearTimeout(stc);//清除定时器
            }
        });
    };
});


/*添加*/
/*切换*/
$(function () {
    var $tab_li = $('#tab .select_type .color_next');
    $tab_li.click(function () {
        $(this).addClass('type_show').siblings().removeClass('type_show')
        var index = $tab_li.index(this);
        $('div.moth_giv>.unit_year').eq(index).show().siblings().hide();
        if (index == 0) {
            $(".change_slect").show();
        } else {
            $(".change_slect").hide();
        }
    });

    //消费验证弹出框js
    $(document).on('click', '.list_contain .show_lass', function () {
        $(this).parent().parent().parent().find(".alert_big_show").hide();
        $(this).parent().parent().find(".alert_big_show").show();
    });
    $(document).on('click', '.hc_closdiv', function () {
        $(".consumer_code_input").val("");
        $(this).parent().parent().parent().hide();
    });
    //消费验证结束

    //订单取消
    $(document).on('click', '.CancelOrder', function () {
        var orderNo = $(this).attr("orderNum");

        $.ajax({
            url: "/manage/Order/CanceOrderInfo",
            data: { orderno: orderNo },
            type: 'GET',
            dataType: 'json',
            error: function (xhr, status) {
                alert("取消订单出错");
            },
            success: function (Data) {
                if (Data.result == "sucess") {
                    alert("取消订单成功");
                    //刷新进三个月和关闭的交易的grid
                    refreshGrid("#LastMonth");
                    refreshGrid("#closed");
                    refreshGrid("#waitPay");

                } else {
                    alert("取消订单出错");
                }
            }
        });
    });

    //订单删除
    $(document).on('click', '.jia .delOrder', function () {

        var orderNo = $(this).attr("ordernum");
        $.ajax({
            url: "/manage/Order/DelOrderInfo",
            data: { Orderid: orderNo },
            type: 'GET',
            dataType: 'json',
            error: function (xhr, status) {
                alert("发生删除异常");
            },
            success: function (Data) {
                if (Data.result == "sucess") {

                    //删除后刷新近三个月已关闭和已完成grid
                    refreshGrid("#LastMonth");
                    refreshGrid("#closed");
                    refreshGrid("#complete");
                    alert("删除订单成功");
                } else {
                    alert("删除订单失败");
                }
            }
        });
    });



    //确认发货 消费验证
    $(document).on('click', '.jia  .relase_btn', function (e) {

        var Consumpno = $(this).parent().parent().find(".consumer_code_input").val();//消费码
        var mstid = $(this).attr("mstID");
        var orderNo = $(this).attr("orderID");
        var priceT = $(this).attr("Price");
        if (Consumpno.length == 0) {
            $(this).parent().prev().show();
            return false;
        }
        var tadeNo = $(this).next().val();
        $.ajax({
            url: "/manage/Order/CheckAndDelivery",
            data: { ConsumptionCode: Consumpno, tradeno: tadeNo, mstID: mstid, orderNo: orderNo, price: priceT },
            type: 'GET',
            dataType: 'json',
            error: function (xhr, status) {
                alert("消费验证出错");
            },
            success: function (Data) {
                //if (Data.result == "sucess") {
                //    refreshGrid("#LastMonth");
                //    refreshGrid("#waitConsumption");
                //    alert("消费验证成功");
                //} else {
                //    alert(Data.result);
                //}
                //$(".hc_closdiv").click();
                //var options = { text: Data, ck: function () { $(".cover").hide(); $("#modifyCover2").hide(); } };
                //$("body").coverJia(options);
                //refreshGrid("#LastMonth");
                //refreshGrid("#waitConsumption");
                //return false;
                //$(".relase_btn").off("click");
            }
        });
        e.preventDefault();
        e.stopPropagation();
    });





    //验证订单输入框是否有值
    $(document).on('input', '.consumer_code_input', function () {
        $(".try_sex").hide();
    });


    //退款同意时跳转支付宝提示
    $(document).on("mouseover mouseout", '.top_center .agree', function (event) {
        if (event.type == "mouseover") {
            //$(this).next().show();
            $(this).nextAll().show();
        } else if (event.type == "mouseout") {
            $(this).nextAll().hide();
            //$(this).next().hide();
        }
    })


});

//订单删除
function delOrder(e) {
    var orderNo = $(e).attr("ordernum");
    $.ajax({
        url: "/manage/Order/DelOrderInfo",
        data: { Orderid: orderNo },
        type: 'GET',
        dataType: 'json',
        error: function (xhr, status) {
            alert("发生删除异常");
        },
        success: function (Data) {
            if (Data.result == "sucess") {

                //删除后刷新近三个月已关闭和已完成grid
                refreshGrid("#LastMonth");
                refreshGrid("#closed");
                refreshGrid("#complete");
                alert("删除订单成功");
            } else {
                alert("删除订单失败");
            }
        }
    });
}

//取消订单
function CancelOrder(e) {
    var orderNo = $(e).attr("orderNum");

    $.ajax({
        url: "/manage/Order/CanceOrderInfo",
        data: { orderno: orderNo },
        type: 'GET',
        dataType: 'json',
        error: function (xhr, status) {
            alert("取消订单出错");
        },
        success: function (Data) {
            if (Data.result == "sucess") {
                alert("取消订单成功");
                //刷新进三个月和关闭的交易的grid
                refreshGrid("#LastMonth");
                refreshGrid("#closed");
                refreshGrid("#waitPay");

            } else {
                alert("取消订单出错");
            }
        }
    });
}


//验证发货
function deliveryService(e) {
  
    var alertDiv = $(e).parent().parent();
    var Consumpno = $(e).parent().parent().find(".consumer_code_input").val();//消费码
    var mstid = $(e).attr("mstID");
    var orderNo = $(e).attr("orderID");
    var priceT = $(e).attr("Price");
    if (Consumpno.length == 0) {
        $(e).parent().prev().show();
        return false;
    }
    var tadeNo = $(e).next().val();
    $.ajax({
        url: "/manage/Order/CheckAndDelivery",
        data: { ConsumptionCode: Consumpno, tradeno: tadeNo, mstID: mstid, orderNo: orderNo, price: priceT },
        type: 'GET',
        dataType: 'json',
        error: function (xhr, status) {
            alert("消费验证出错");
        },
        success: function (Data) {
            var options = { text: Data };
            $(alertDiv).innerCoverJia(options);
            if (Data == "success") {
                var optionsuccess = { text: "验证成功！" };
                $(alertDiv).innerCoverJia(options);
                refreshGrid("#LastMonth");
                refreshGrid("#waitConsumption");
            }
        }
    });
    
}


function refreshGrid(id) {
    var grid = $(id).data("tGrid");
    grid.currentPage = 1;
    grid.ajaxRequest();
}