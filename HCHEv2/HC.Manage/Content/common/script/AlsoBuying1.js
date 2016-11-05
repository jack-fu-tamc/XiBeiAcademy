/*jia*/
if (navigator.userAgent.indexOf('MSIE') >= 0) {
    var browser = navigator.appName
    var b_version = navigator.appVersion
    var version = b_version.split(";");
    var trim_Version = version[1].replace(/[ ]/g, "");
}

//单位下拉
$(function () {
    var unitSelect = $("#time_show");
    $(".current_date").click(function (e) {
        unitSelect.show();
    });
    $(".date_time a").click(function (e) {
        var selText = $(this).text().trim();
        $("#pjUnits").text(selText);
        unitSelect.hide();
    });
})

//删除图片执行函数
function closeyy(e) {
    $(e).prev("img").remove();
    $(e).remove();
    $(".hche_upbutt object").removeClass("jiaObj");//删除后打开上传开关
    $("#hche_deposit").css("margin-top", "-5px");
}
//清单图片上传初始化
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
        'buttonImg': '../../HC.Manage/Content/common/script/uploadify/jquery.uploadify-v2.1.0/uploadpic_btn.png',
        'width': '130',
        'onComplete': function (event, queueId, fileObj, response, data) {
            var x = response;
            if (x == '0|') alert('上传失败');
            else {
                var ss = event.data.who;
                var imgcontent = $(ss).parent().parent().next();
                $(imgcontent).show();
                if (imgcontent.children().length == 3) {
                    imgcontent.append("<img src=" + response + " />");
                    $(".hche_upbutt object").addClass("jiaObj");
                    $("#hche_deposit").css("margin-top", "-50px");
                } else {
                    imgcontent.append("<img src=" + response + " />");
                }
            }
        }
    });
}
//附件上传初始化
function initAttachemntPlu() {
   
    $("#attachmentFile").uploadify({
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
        'buttonImg': '../../HC.Manage/Content/common/script/uploadify/jquery.uploadify-v2.1.0/Attachment_btn.png',
        'width': '140',
        'height':'40',
        'onComplete': function (event, queueId, fileObj, response, data) {
            var x = response;
            if (x == '0|') alert('上传失败');
            else {   
                var ulTar = $(".hche_img ul");
                var fillHtml = "<li><span><em></em><img src=\"" + x + "\" class=\"imgshow\" /></span></li>";
                ulTar.append(fillHtml);
            }
        }
    });
};
//单选按钮
$(function () {
   
    //单选
    $("#redioman").click(function () {
        $("#redioman").addClass("rabuit");
        $("#radiowoman").removeClass("rabuit");
        //$("#attachmentFile").hide();
        $(".qg_button object").addClass("jiaObj");
        $("#add_butWrite").show();
        $(".hche_showimg").hide();
        if ($(".qg_tbody tr").text() == "") {
            $(".qg_table").hide();
        } else {
            $(".qg_table").show();
        }
    });
    $("#radiowoman").click(function () {
        $("#radiowoman").addClass("rabuit");
        $("#redioman").removeClass("rabuit");

        //table隐藏
        $(".qg_table").hide();

        $(".hche_showimg").show();
        $("#add_butWrite").hide();
        //$("#attachmentFile").show();
        $(".qg_button object").removeClass("jiaObj");
    });
});
//附件删除和查看大图
$(function () {
    //大图关闭
    $("#divimg").find("p").click(function () {
        $("#divimg").hide();
        $("#main").hide();
    })
    //大图展示
    $(document).on("click", ".hche_img ul li span img", function (e) {
        var imgsrc = $(this).attr("src");
        $("#divimg").find("img").eq(1).attr("src", imgsrc);
        $("#divimg").show();
        $("#main").show();
    })
    //滑过显示删除
    $(document).on("mouseover mouseout", ".hche_img ul li span", function (event) {
        if (event.type == "mouseover") {
            //鼠标悬浮
            $(this).find("em").addClass("deleteForEm");
        } else if (event.type == "mouseout") {
            $(this).find("em").removeClass("deleteForEm");
        }
    })
    //删除附件
    $(document).on("click", ".deleteForEm", function (e) {
        $(this).parent().parent().remove();
    })

});
/*表格 清单遮罩层*/
$(function () {
    //图片滚动
    var page = 1;
    var i = 3;
    var nextSrcoll = function (ele) {
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
    }
    var preSrcoll = function (ele) {
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
    }
    //清单图片删除  
    $(document).on("mouseover mouseout", ".hche_deposit>img", function (event) {
        if (event.type == "mouseover") {
            var totalImg = $("#hche_deposit>img").length;
            console.log(curenPos);
            //鼠标悬浮
            $(this).siblings("img").next("a").remove();
            if($(this).next().is($(".closeyy"))){
            } else {
                var curenPos = $(this).index();
                $(this).after("<a class=\"closeyy\" onclick=\"closeyy(this)\"><img src=\"/HC.Manage/Content/common/script/uploadify/jquery.uploadify-v2.1.0/ji03S.png\"></a>");
                var mleft = 0;
                if ((browser == "Microsoft Internet Explorer") && (trim_Version == "MSIE7.0")) {//兼容IE7
                    mleft = (totalImg - curenPos - 1) * 100 + 15 * 1 + (totalImg - curenPos - 1) * 10 * 1;
                    $(this).next().css("margin-left", "-" + mleft + "px");  
                }
                // || ()
                if (navigator.userAgent.indexOf("Firefox") > 0) {//兼容火狐
                    mleft = (totalImg - curenPos - 1) * 100 + 15 * 1 + (totalImg - curenPos - 1) * 10 * 1 + 10 * 1;
                    $(this).next().css("margin-left", "-" + mleft + "px");
                }

            }
        } else if (event.type == "mouseout") {
        }
    })
    $(".hche_deposit").hover(function () { }, function () {
        $(this).find("a").remove();
    })

    //初始附件上传
    initAttachemntPlu();
    globalTableDta = { data: { tr: [] } };
    //打开清单填写遮罩层并初始化数据
    function openLayer(trd, trindex) {
        if (trd) {
            $("#pjName").val(trd.category);
            $("#pjType").val(trd.Version);
            $("#pjNumber").val(trd.number);
            $("#pjUnits").text(trd.unit);
            $("#pjRemark").val(trd.Remark);
            var imgstrs = "";
            for (var im in trd.pics) {
                if(!isNaN(im))
                imgstrs += "<img src=\"" + trd.pics[im].tar + "\">";
            }
            $("#butnpwds").attr("action", "update");
            $("#butnpwds").attr("index", trindex);
            //$(".hche_deposit").html(imgstrs);
            $(".hche_deposit").html(imgstrs).show();//jia 12-30

            //处理选择或输入
            if ((trd.towName != undefined && trd.categoryId != undefined) && trd.towName != "undefined") {
                $("#pjName").attr("two", trd.towName);
                $("#pjName").attr("three", trd.categoryId);
                ////显示选择
                $(".adaptCarLayOut").show();
                $(".goWrite").parent().show();
                $($(".selectText")[0]).text(trd.towName.split(',')[0]);
                $($(".selectText")[1]).text(trd.category);
                ///隐藏输入
                $(".goChoose").parent().addClass("inputCategory");
                ///根据父类初始化子类选择
                $.ajax({
                    url: "/Manage/Purchase/_ajaxCatSelectPlus",
                    data: { id: trd.towName.split(',')[1] },
                    type: 'GET',
                    dataType: 'json',
                    error: function (xhr, status) {
                        alert(status);
                    },
                    success: function (Data) {
                        var letters = [];
                        $.each(Data, function (i, v) {
                            if (letters.indexOf(v.firstLetter) == -1 && isNaN(v.firstLetter)) {
                                letters.push(v.firstLetter);
                            }
                        })
                        var newCatThreeSerise = { O: "" };
                        carThreeObj.dropDonwNav({ 'letterObj': letters, 'selectObj': Data, 'valueChange': catThreeChange }, newCatThreeSerise);
                    }
                })
            } else {
                //显示输入
                $(".goChoose").parent().removeClass("inputCategory");
                //隐藏选择
                $(".adaptCarLayOut").hide();
                $(".goWrite").parent().hide();
            }
            trd.pics.length == 4 ? "" : $(".hche_upbutt object").removeClass("jiaObj");//控制编辑时 是否开启 图片上传
        } else {//控制编辑时 是否开启 图片上传
            $(".hche_upbutt object").removeClass("jiaObj");
        }
        $(".body_alter").css("visibility", "visible");
        $(".mask_layer").css("visibility", "visible");
        $(".field-validation-error span").remove();
    }
    //关闭清单填写遮罩层并清空数据
    function closeLayer() {
        $(".body_alter").css("visibility", "hidden");
        $(".mask_layer").css("visibility", "hidden");
        $(".hche_upbutt object").addClass("jiaObj");
        $(".hche_alertone input,textarea").val("");
        $(".hche_alertone .hche_deposit").empty();
        $("#butnpwds").removeAttr("action").removeAttr("index");
        $("#pjName").removeAttr("two").removeAttr("three");
        $($(".selectText")[0]).text("==请选择分类==");
        $($(".selectText")[1]).text("==请选择子类==");
        $("#movePar_selectThreeLevel").remove();
        $(".field-validation-error span").remove();
    }
    $("#add_butWrite").click(function (event) { openLayer(); })
    $("#hc_couse_car").click(function (event) { closeLayer(); })
    //初始化上传控件
    initPicPlug($("#upPic"));
    //初始化 table数据
    function initData(action, trindex) {
        var trdata = { category: "", Version: "", number: "", unit: "", pics: [], Remark: "", towName: "", categoryId: "" };
        var pic = { tar: "" };
        curentData = { data: { tr: [] } };
        var trdataObj = Object.create(trdata);
        trdataObj.category = $("#pjName").val();
        trdataObj.number = $("#pjNumber").val();
        trdataObj.unit = $("#pjUnits").text();
        trdataObj.Version = $("#pjType").val();
        trdataObj.Remark = $("#pjRemark").val();

        //处理是选择还是填写数据
        trdataObj.towName = $("#pjName").attr("two");
        trdataObj.categoryId = $("#pjName").attr("three");


        
        for (var l = 0; l < $(".hche_deposit").children().length; l++) {
            var picObj = Object.create(pic);
            picObj.tar = $($(".hche_deposit").children()[l]).attr("src");
            trdataObj.pics.push(picObj);
        }
        curentData.data.tr.push(trdataObj);
        //维持globalTableDta
        if (action == undefined)
            globalTableDta.data.tr.push(trdataObj);
        else
            globalTableDta.data.tr[trindex] = trdataObj;
        return curentData;
    }
    //增加或更新一个清单
    $("#butnpwds").click(function (event) {
        if ($("#purchaseForm").valid()) {
            var action = $(this).attr("action");
            var trind = $(this).attr("index");
            tableModel = initData(action, trind);
            action == undefined ? $(".qg_tbody").jiaTableBind(tableModel) : $(".qg_tbody").jiaTableBind(tableModel, "update").updataTr(trind * 1 + 1, tableModel);
            closeLayer();
            $(".qg_tabletext").show();
            //$(document).on("click", ".prev", preSrcoll);
            //$(document).on("click", ".next", nextSrcoll);
        }
        //return false;
    })
    //编辑tr
    $(document).on("click", ".editorTr", function (event) {
        var trIndex = $(this).parent().parent().index();
        openLayer(globalTableDta.data.tr[trIndex - 1], trIndex - 1);
    })
    //删除tr
    $(document).on("click", ".deleteTr", function (event) {
        var trofdelete = $(this).parent().parent();
        var trIndex = trofdelete.index();
        globalTableDta.data.tr.splice(trIndex - 1, 1);
        trofdelete.remove();
        if ($(".qg_tbody tr").length == 1) {
            $(".qg_tabletext").hide();
        }
    })

    //初始图片滚动效果
    $(document).on("click", ".prev", preSrcoll);
    $(document).on("click", ".next", nextSrcoll);
});



