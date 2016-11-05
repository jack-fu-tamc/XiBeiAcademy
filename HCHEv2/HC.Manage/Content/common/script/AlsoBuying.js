//文本框默认提示文字
function textFocus(el) {
    if (el.defaultValue == el.value) { el.value = ''; el.style.color = '#333'; }
}
function textBlur(el) {
    if (el.value == '') { el.value = el.defaultValue; el.style.color = '#999'; }
}

function initPicPlug(ele) {
    ele.uploadify({
        'uploader': '../../HC.Manage/Content/common/script/uploadify/jquery.uploadify-v2.1.0/uploadify.swf',
        'script': '/manage/Common/fileUpQg?x=palceHold',
        'cancelImg': '../../HC.Manage/Content/common/script/uploadify/jquery.uploadify-v2.1.0/cancel.png',
        'folder': 'LicenseImg',
        'simUploadLimit': 4,
        'queueSizeLimit': 4,
        'auto': true,//选择图片后自动上传
        'multi': true,//是否允许同时选择多个文件
        'wmode': 'transparent',
        'fileExt': '*.jpg;*.png;',
        'buttonImg': '../../HC.Manage/Content/common/script/uploadify/jquery.uploadify-v2.1.0/uploadImg_2.png',
        'width': '130',
        'onComplete': function (event, queueId, fileObj, response, data) {
            var x = response;
            if (x == '0|') alert('上传失败');
            else {
                var ss = event.data.who;
                var imgcontent = $(ss).parent().next();
                var upObj = $(ss).next();
                $(imgcontent).show();
                if (imgcontent.children().length == 3) {
                    imgcontent.append("<li><img src='" + response + "' /><div class='del_Liimg'>点击删除</div></li>");
                    upObj.css("visibility", "hidden");
                } else {
                    imgcontent.append("<li><img src='" + response + "' /><div class='del_Liimg'>点击删除</div></li>");
                }
            }
        }
    });
}

//填写求购单
function Writeclick() {
    $(".body_alter").html("<div id='addlist' class='addlist'><div class='hche_alert'><div class='hche_alertone'>" +
           "<div class='hche_alerttit'>+&nbsp;&nbsp;填写新清单<div class='hc_couse' id='hc_couse_car'></div></div>" +
           "<div class='hche_alertcont'><div class='hc_item'><span class='hc_textlable'>配件分类</span>" +
           "<div class='hc_fl'>" +
           "<div class='date_time1'><div class='current_date1' id='date_time1'><span id='pjUnits1'>选择配件类别</span><em></em></div>" +
           "<ul class='time_show1' id='time_show1'><li><a href='javascript:void(0);'>宝马阿萨德</a></li><li><a href='javascript:void(0);'>上海打着那个</a></li></ul></div>" +
           "<div class='date_time2'><div class='current_date2' id='date_time2'><span id='pjUnits2'>选择配件类别</span><em></em></div>" +
           "<ul class='time_show2' id='time_show2'><li><a href='javascript:void(0);'>宝马阿萨德</a></li><li><a href='javascript:void(0);'>上海打着那个</a></li></ul></div>" +
           "<span id='onname_span' style='display:none;'></span><span id='twoname_span' style='display:none;'></span>" +
           "<span class='no_mycar'><a href='javascript:void(0);'onclick='no_myCar();'>没您要的分类？</a></span><span style='display:none;' class='hava_myCar'><a href='javascript:void(0);'onclick='hava_myCar();'>返回选择</a></span>" +
           "<input placeholder='请输入您要添加的分类' style='display:none;' id='pjName' class='hc_nameinput pjName' type='text' /></div>" +
           "<div class='try_sex'><span class='tip_text' id='postcode_error1' style='display:none'></span></div></div>" +
           "<div class='hc_item'><span class='hc_textlable'>规格型号</span><div class='hc_fl'>" +
           "<input id='pjType' class='hc_nameinput pjType' type='text' placeholder='请输入规格型号' /></div><div class='try_sex'>" +
           "<span class='tip_text'id='postcode_error2' style='display:none'></span></div></div><div class='hc_item'><span class='hc_textlable'>数量/单位</span>" +
           "<div class='hc_fl'><input class='hc_nameinput hc_nameinputs pjNumber' id='pjNumber' type='text' placeholder='请输入求购产品数量' />" +
           "<div class='date_time'><div class='current_date' id='date_time'><span id='pjUnits'>个</span><em></em></div>" +
           "<ul class='time_show' id='time_show'><li><a href='javascript:void(0);'>个</a></li><li><a href='javascript:void(0);'>套</a></li>" +
           "</ul></div></div><div class='try_sex'><span class='tip_text'id='postcode_error3' style='display:none'></span></div>" +
           "</div><div class='hc_item'><span class='hc_textlable'>实拍图片</span><div class='hche_Upload'><div class='hche_upbutt'>" +
           "<input type='button' id='upPic' autocomplete='off' value='上传图片' /></div>" +
           //图片DIV
           "<div class='hche_deposit' style='display:none;'></div>" +
           "</div><div class='try_sex' ><span class='tip_text' id='postcode_error4' style='display:none;'></span></div></div>" +
            "<div class='hc_item' style='float: left;'><span class='hc_textlable'>备注</span><div class='hc_fl'>" +
            "<textarea id='pjRemark' placeholder='备注可输入100字' maxlength='100' class='hc_nameinput'></textarea></div>" +
             "</div><div class='sub_product_btn'><input onclick='submitformOld();' value='提交' type='submit' id='butnpwds'/></div></div></div>" +
             "</div></div><div class='mask_layer' id='mask_layer_show' ></div>");
    initPicPlug($("#upPic"));
}
//上传求购附件
function Upclick() {
    //$(".hche_showimg").show();
}
//验证配件名称
function pjName() {
    if ($("#pjName").val() == "") {
        $("#postcode_error1").html("配件名称不能为空");
        $("#postcode_error1").show();
        return false;
    }
    else {
        $("#postcode_error1").hide();
        return true;
    }
}
function pjNameSpan() {
    if ($("#onname_span").text() == "" || $("#twoname_span").text() == "") {
        $("#postcode_error1").html("请选择配件分类");
        $("#postcode_error1").show();
        return false;
    }
    else {
        $("#postcode_error1").hide();
        return true;
    }
}
$(document).on("blur", "#pjName", pjName);

//规格型号
function pjType() {
    if ($("#pjType").val() == "") {
        $("#postcode_error2").html("规格型号不能为空");
        $("#postcode_error2").show();

    } else {
        $("#postcode_error2").hide();
        return true;
    }
}
$(document).on("blur", "#pjType", pjType);

//验证数量单位
function pjNumber() {
    var z = /^[0-9]*$/;
    if ($("#pjNumber").val() == "") {
        $("#postcode_error3").html("数量单位不能为空");
        $("#postcode_error3").show();
        return false;
    } else if (!z.test($("#pjNumber").val())) {
        $("#postcode_error3").html("输入格式只能为数字 ");
        $("#postcode_error3").show();
        return false;
    }
    else {
        $("#postcode_error3").hide();
        return true;
    }
}
$(document).on("blur", "#pjNumber", pjNumber);
//图片验证
function imgdivBUt() {
    if ($(".hche_deposit").html() == "") {
        $("#postcode_error4").html("请上传图片");
        $("#postcode_error4").show();
        return false;
    }
    else {
        $("#postcode_error4").hide();
        return true;
    }
}
//没有我的名称
function no_myCar() {
    $(".date_time1").hide();
    $(".date_time2").hide();
    $("#onname_span").text("");
    $("#twoname_span").text("");
    $("#pjName").show();
    $(".no_mycar").hide();
    $(".hava_myCar").show();
}
function hava_myCar() {
    $(".date_time1").show();
    $(".date_time2").show();
    $("#pjName").val("");
    $("#pjName").hide();
    $(".no_mycar").show();
    $(".hava_myCar").hide();
    $("#pjUnits1").html("请选择");
    $("#pjUnits2").html("请选择");
}
function submitformOld() {
    if (!pjNameSpan() && !pjName() || !pjType() || !pjNumber() || !imgdivBUt()) {

    } else {
        $(".qg_table").show();
        $(".qg_tbody").append("<tr class='submTR'><td>" + $('#twoname_span').text() + $("#pjName").val() +
                              "<span class='one_appendspan' style='display:none;'>" +
                              $("#onname_span").text() + "</span><span class='two_appendspan' style='display:none;'>" +
                              $("#twoname_span").text() + "</span><span class='three_appendspan' style='display:none;'>" +
                              $("#pjName").val() + "</span></td><td>" +
                              $('#pjType').val() + "</td>" +
                              "<td>" + "<span class='onenumb'>" + $('#pjNumber').val() + "</span>" + "/" +
                              "<span class='twonumb'>" + $('#pjUnits').text() + "</span>" + "</td><td>" +
                              "<div class='scroll'><a class='prev' href='javascript:;'></a>" +
                              "<div class='box'><div class='scroll_list'><ul>" +
                              $(".hche_deposit").html() + "</ul>" +
                              "</div></div><a class='next' href='javascript:;'></a></div></td>" +
                              "<td>" + $('#pjRemark').val() + "</td><td><span class='td_span'>" +
                              "<a href='javascript:;' onclick='RedactInput(this);'>编辑</a></span>" +
                              "<span class='td_span'><a href='javascript:;' onclick='dropElement(this)'>删除</a></span></td></tr>");


        $("#addlist").hide();
        $("#mask_layer_show").hide();
        //实拍图片滚动
        var page = 1;
        var i = 3;
        $(".next").click(function () {
            var v_wrap = $(this).parents(".scroll");
            var v_show = v_wrap.find(".scroll_list");
            var v_cont = v_wrap.find(".box");
            var v_width = v_cont.width();
            var len = v_show.find("li").length;
            var page_count = Math.ceil(len / i);
            if (!v_show.is(":animated")) {
                if (page == page_count) {
                    v_show.animate({ left: '0px' }, "slow");
                    page = 1;
                } else {
                    v_show.animate({ left: '-=' + v_width }, "slow");
                    page++;
                }
            }
        });
        $(".prev").click(function () {
            var v_wrap = $(this).parents(".scroll");
            var v_show = v_wrap.find(".scroll_list");
            var v_cont = v_wrap.find(".box");
            var v_width = v_cont.width();
            var len = v_show.find("li").length;
            var page_count = Math.ceil(len / i);
            if (!v_show.is(":animated")) {
                if (page == 1) {
                    v_show.animate({ left: '-=' + v_width * (page_count - 1) }, "slow");
                    page = page_count;
                } else {
                    v_show.animate({ left: '+=' + v_width }, "slow");
                    page--;
                }
            }
        });
    }
}


//提交
function submitform(ele) {
    var modifyTr = $(".qg_tbody").find("tr")[ele];
    if (!pjNameSpan() && !pjName() || !pjType() || !pjNumber()) {

    } else {

        $(".qg_table").show();
        $(modifyTr).html("<td>" + $('#twoname_span').text() + $("#pjName").val() +
                         "<span class='one_appendspan' style='display:none;'>" +
                         $("#onname_span").text() + "</span>" +
                         "<span class='two_appendspan' style='display:none;'>" +
                         $("#twoname_span").text() + "</span><span class='three_appendspan' style='display:none;'>" +
                         $("#pjName").val() + "</span></td><td>" +
                         $('#pjType').val() + "</td><td><span class='onenumb'>" +
                         $('#pjNumber').val() + "</span>" + "/" + "<span class='twonumb'>" +
                         $('#pjUnits').text() + "</span>" + "</td><td>" +
                          "<div class='scroll'><a class='prev' href='javascript:;'></a>" +
                          "<div class='box'><div class='scroll_list'><ul>" +
                          $(".hche_deposit").html() + "</ul>" +
                          "</div></div><a class='next' href='javascript:;'></a></div></td>" +
                          "<td>" + $('#pjRemark').val() + "</td><td><span class='td_span'>" +
                          "<a href='javascript:;' onclick='RedactInput(this);'>编辑</a></span>" +
                          "<span class='td_span'><a href='javascript:;' onclick='dropElement(this)'>删除</a></span></td>");
        $("#addlist").hide();
        $("#mask_layer_show").hide();
        //实拍图片滚动
        var page = 1;
        var i = 3;
        $(".next").click(function () {
            var v_wrap = $(this).parents(".scroll");
            var v_show = v_wrap.find(".scroll_list");
            var v_cont = v_wrap.find(".box");
            var v_width = v_cont.width();
            var len = v_show.find("li").length;
            var page_count = Math.ceil(len / i);
            if (!v_show.is(":animated")) {
                if (page == page_count) {
                    v_show.animate({ left: '0px' }, "slow");
                    page = 1;
                } else {
                    v_show.animate({ left: '-=' + v_width }, "slow");
                    page++;
                }
            }
        });
        $(".prev").click(function () {
            var v_wrap = $(this).parents(".scroll");
            var v_show = v_wrap.find(".scroll_list");
            var v_cont = v_wrap.find(".box");
            var v_width = v_cont.width();
            var len = v_show.find("li").length;
            var page_count = Math.ceil(len / i);
            if (!v_show.is(":animated")) {
                if (page == 1) {
                    v_show.animate({ left: '-=' + v_width * (page_count - 1) }, "slow");
                    page = page_count;
                } else {
                    v_show.animate({ left: '+=' + v_width }, "slow");
                    page--;
                }
            }
        });
    }
}


$(function () {
    //单选
    $("#redioman").click(function () {
        $("#redioman").addClass("rabuit");
        $("#radiowoman").removeClass("rabuit");
        $("#attachmentFile").hide();
        $("#add_butWrite").show();
        $(".hche_showimg").hide();
        if ($(".qg_tbody tr").text() == "") {
            $(".qg_table").hide();
            $(".qg_tbody tr").hide();
        } else {
            $(".qg_table").show();
            $(".qg_tbody tr").show();
        }
    });
    $("#radiowoman").click(function () {
        $("#radiowoman").addClass("rabuit");
        $("#redioman").removeClass("rabuit");
        $(".qg_table").hide();
        $(".qg_tbody tr").hide();
        $(".hche_showimg").show();
        $("#attachmentFile").show();
        $("#add_butWrite").hide();
        $(".body_alter").html("");






    });

    //验证
    $(".qg_titleText").blur(function () {
        if ($(".qg_titleText").val() == "" || $(".qg_titleText").val() == "请输入求购配件的标题，标题描述可输入30字") {
            $(".qg_titleText").addClass("errorC");
            $(".qg_error0").html("请输入求购配件的标题最多可输入30字！");
            $(".qg_error0").css("display", "block");
            return false;
        } else if ($(".qg_titleText").val().length > 30) {
            $(".qg_titleText").addClass("errorC");
            $(".qg_error0").html("标题长度不正确最多可输入30字！");
            $(".qg_error0").css("display", "block");
            return false;
        }
        else {
            $(".qg_titleText").removeClass("errorC");
            $(".qg_error0").css("display", "none");
            return true;
        }
    });
});


//删除
function dropElement(e) {
    var parent = $(e).parent().parent().parent();
    $(parent).remove();
    if ($(".qg_tbody").html().replace(/(^\s*)|(\s*$)/g, "") == "") {
        $(".qg_table").hide();
    }
}

/*点击查看大图js*/
$(function () {
    $(".imgshow").click(function () {
        var aa = $(this).attr("src");
        $("#divimg").find("img").eq(1).attr("src", aa);
        $("#divimg").show();
        $("#main").show();
    });
    $("#divimg").find("p").click(function () {
        $("#divimg").hide();
        $("#main").hide();
    })
    //滑过显示删除样式
    $(".hche_img ul li img").mouseover(function () {
        $(this).addClass("hche_imgborder");
        $(this).parent().find("em").addClass("addDelsty");
    });
    $(".hche_img ul li img").mouseout(function () {
        $(this).removeClass("hche_imgborder");
        $(this).parent().find("em").removeClass("addDelsty");
    });
    $(".hche_img ul li em").click(function () {
        $(this).parent().parent().remove();
    })
});
$(function () {
    //弹出层
    $(document).on("click", "#hc_couse_car", function () {
        $("#addlist").hide();
        $("#mask_layer_show").hide();
    })
    /*配件分类*/
    $(document).on("click", "#time_show1 li", function () {
        var li = $(this).text();
        $(".date_time1 span").text(li);
        $(".date_time1 em").removeClass("sore_tas_three1");
        $("#time_show1").hide();
        $("#onname_span").html($("#pjUnits1").text());
    });
    $(document).on("click", "#date_time1", function () {
        var ul = $("#time_show1");
        if ($("#time_show1").is(":hidden")) {
            ul.show();
            $(".date_time1 em").addClass("sore_tas_three1");
        } else {
            ul.hide();
            $(".date_time1 em").removeClass("sore_tas_three1");
        }
    });
    $(document).on("click", "#time_show1", function () {
        $(this).hide();
        $(".date_time1 em").removeClass("sore_tas_three1");
    });
    $(document).on("mouseover", ".hche_deposit1 li", function () {
        $(this).find(".del_Liimg1").show();
    });
    $(document).on("mouseout", ".hche_deposit1 li", function () {
        $(this).find(".del_Liimg1").hide();
    });
    $(document).on("click", ".del_Liimg1", function () {
        $(this).parent().remove();
    });
    /*配件分类2*/
    $(document).on("click", "#time_show2 li", function () {
        var li = $(this).text();
        $(".date_time2 span").text(li);
        $(".date_time2 em").removeClass("sore_tas_three2");
        $("#time_show2").hide();
        $("#twoname_span").html($("#pjUnits2").text());
    });
    $(document).on("click", "#date_time2", function () {
        var ul = $("#time_show2");
        if ($("#time_show2").is(":hidden")) {
            ul.show();
            $(".date_time2 em").addClass("sore_tas_three2");
        } else {
            ul.hide();
            $(".date_time2 em").removeClass("sore_tas_three2");
        }
    });
    $(document).on("click", "#time_show2", function () {
        $(this).hide();
        $(".date_time2 em").removeClass("sore_tas_three2");
    });
    $(document).on("mouseover", ".hche_deposit2 li", function () {
        $(this).find(".del_Liimg2").show();
    });
    $(document).on("mouseout", ".hche_deposit2 li", function () {
        $(this).find(".del_Liimg2").hide();
    });
    $(document).on("click", ".del_Liimg2", function () {
        $(this).parent().remove();
    });

    /*下拉显示*/
    $(document).on("click", "#time_show li", function () {
        var li = $(this).text();
        $(".date_time span").text(li);
        $(".date_time em").removeClass("sore_tas_three");
        $("#time_show").hide();
    });
    $(document).on("click", "#date_time", function () {
        var ul = $("#time_show");
        if ($("#time_show").is(":hidden")) {
            ul.show();
            $(".date_time em").addClass("sore_tas_three");
        } else {
            ul.hide();
            $(".date_time em").removeClass("sore_tas_three");
        }
    });
    $(document).on("click", "#time_show", function () {
        $(this).hide();
        $(".date_time em").removeClass("sore_tas_three");
    });
    $(document).on("mouseover", ".hche_deposit li", function () {
        $(this).find(".del_Liimg").show();
    });
    $(document).on("mouseout", ".hche_deposit li", function () {
        $(this).find(".del_Liimg").hide();
    });
    $(document).on("click", ".del_Liimg", function () {
        $(this).parent().remove();
    });
});
/*编辑*/
var tr, td1, trIndex;
function returnTR(e) {
    tr = $(e).parent().parent().parent();
    td1 = $(tr).children()[0];
    td2 = $(tr).children()[1];
    td3 = $(tr).children()[2];
    td4 = $(tr).children()[3];
    td5 = $(tr).children()[4];
    trIndex = tr.index();
}
function RedactInput(self) {
    returnTR(self);
    $(".body_alter").html("<div id='addlist' class='addlist'><div class='hche_alert'><div class='hche_alertone'>" +
           "<div class='hche_alerttit'>+&nbsp;&nbsp;填写新清单<div class='hc_couse' id='hc_couse_car'></div></div>" +
           "<div class='hche_alertcont'><div class='hc_item'><span class='hc_textlable'>配件分类</span>" +
           "<div class='hc_fl'>" +
           "<div class='date_time1'><div class='current_date1' id='date_time1'><span id='pjUnits1'>" + $(td1).find(".one_appendspan").text() + "</span><em></em></div>" +
           "<ul class='time_show1' id='time_show1'><li><a href='javascript:void(0);'>宝马阿萨德</a></li><li><a href='javascript:void(0);'>上海打着那个</a></li></ul></div>" +
           "<div class='date_time2'><div class='current_date2' id='date_time2'><span id='pjUnits2'>" + $(td1).find(".two_appendspan").text() + "</span><em></em></div>" +
           "<ul class='time_show2' id='time_show2'><li><a href='javascript:void(0);'>宝马阿萨德</a></li><li><a href='javascript:void(0);'>上海打着那个</a></li></ul></div>" +
           "<span id='onname_span' style='display:none;'>" + $(td1).find(".one_appendspan").text() + "</span><span id='twoname_span' style='display:none;'>" + $(td1).find(".two_appendspan").text() + "</span>" +
           "<span class='no_mycar' style='display:none;'><a href='javascript:void(0);'onclick='no_myCar();'>没找到您要的分类？</a></span><span class='hava_myCar'><a href='javascript:void(0);'onclick='hava_myCar();'>返回选择</a></span>" +
           "<input placeholder='请输入您要添加的分类' value='" + $(td1).find(".three_appendspan").text() + "' style='display:none;' id='pjName' class='hc_nameinput pjName' type='text' /></div>" +
           "<div class='try_sex'><span class='tip_text' id='postcode_error1' style='display:none'></span></div></div>" +
           "<div class='hc_item'><span class='hc_textlable'>规格型号</span><div class='hc_fl'>" +
           "<input id='pjType' value='" + $(td2).text() + "' class='hc_nameinput pjType' type='text' placeholder='请输入规格型号' /></div><div class='try_sex'>" +
           "<span class='tip_text' id='postcode_error2' style='display:none'></span></div></div><div class='hc_item'><span class='hc_textlable'>数量/单位</span>" +
           "<div class='hc_fl'><input class='hc_nameinput hc_nameinputs pjNumber' id='pjNumber' value='" + $(td3).find(".onenumb").text() + "' type='text' placeholder='请输入求购产品数量' />" +
           "<div class='date_time'><div class='current_date' id='date_time'><span id='pjUnits'>" + $(td3).find(".twonumb").text() + "</span><em></em></div>" +
           "<ul class='time_show' id='time_show'><li><a href='javascript:void(0);'>个</a></li><li><a href='javascript:void(0);'>套</a></li>" +
           "</ul></div></div><div class='try_sex'><span class='tip_text' id='postcode_error3' style='display:none'></span></div>" +
           "</div><div class='hc_item'><span class='hc_textlable'>实拍图片</span><div class='hche_Upload'><div class='hche_upbutt'>" +
           "<input type='button' id='upPic' autocomplete='off' value='上传图片' /></div>" +
           //图片DIV
           "<div class='hche_deposit'>" + $(".scroll_list ul").html() + "</div>" +
           "</div><div class='try_sex'><span class='tip_text' id='postcode_error4' style='display:none;'></span></div></div>" +
            "<div class='hc_item' style='float: left;'><span class='hc_textlable'>备注</span><div class='hc_fl'>" +
            "<textarea id='pjRemark' placeholder='备注可输入100字' maxlength='100' class='hc_nameinput'>" + $(td5).text() + "</textarea></div>" +
             "</div><div class='sub_product_btn'><input onclick='submitform(" + trIndex + ");' value='提交' type='submit' /></div></div></div>" +
             "</div></div><div class='mask_layer' id='mask_layer_show' ></div>");

    initPicPlug($("#upPic"));

    if ($(td1).find(".three_appendspan").text() == "") {
        $(".date_time1").show();
        $(".date_time2").show();
        $("#pjName").hide();
        $(".no_mycar").show();
        $(".hava_myCar").hide();
    } else {
        $(".date_time1").hide();
        $(".date_time2").hide();
        $("#pjName").show();
        $(".no_mycar").hide();
        $(".hava_myCar").show();
    }

}
