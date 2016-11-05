


//适配车型验证 jia+
$(function () {
    $(".select_radio_next").each(function (index) {
        var $input_show_one = $(this).find(".input_show_one");
        $input_show_one.each(function () {
            var $radio_li = $(this).find("label");
            $radio_li.each(function (index) {
                $(this).click(function () {

                    var $radio = $(this).parent().find("input[type='radio']");
                    $radio.click();

                    if (index == 0) {
                        $(this).parent().parent().find("label").eq(0).attr("class", "agree_arg_next");
                        $(this).parent().parent().find("label").eq(1).attr("class", "agree_arg");
                        //隐藏选择车型并更改该   商品匹配车型的值为0jia+
                        $(".ready_carSelect").hide();
                        $("#adapterCar").val(0); $(".append_adpapt").empty();
                    }
                    if (index == 1) {
                        $(this).parent().parent().find("label").eq(0).attr("class", "agree_arg");
                        $(this).parent().parent().find("label").eq(1).attr("class", "agree_arg_next");
                        //显示选择车型 jia+
                        $(".ready_carSelect").show();
                        $("#adapterCar").val("");
                    }
                });
            });
        });
    });

    //添加适配车型 jia+ telerik版
    //$("#addAdaptCar").click(function () {

    //    var dropdownListOfPinPai = $("#CarPinPaiSelect").data("tDropDownList");
    //    var dropdownListOfSeries = $('#CarSeriesSelect').data("tDropDownList");
    //    var dropdownListOfVersion = $('#CarVersionSelect').data("tDropDownList");

    //    //显示用户选择的车型车系车款  大众 > 斯柯达 > 所有车型
    //    var pinpaiV = dropdownListOfPinPai.value();
    //    var SeriesV = dropdownListOfSeries.value();
    //    var VersionV = dropdownListOfVersion.value();


    //    var pinpai = pinpaiV.split(',')[1];
    //    var Series = SeriesV == "-1" ? "" : SeriesV.split(',')[1];
    //    var Version = VersionV == "-1" ? "" : VersionV.split(',')[1];
    //    var adapterCarV = $("#adapterCar").val();

    //    //给adapterCar字段赋值
    //    if (dropdownListOfPinPai.value() != "") {
    //        if (dropdownListOfSeries.value() == "-1") {

    //            var addPValue = dropdownListOfPinPai.value().split(',')[0];
    //            if ($("#adapterCar").val().indexOf(addPValue) > -1) {
    //                alert("你已添加了该车型");
    //            } else {
    //                $("#adapterCar").val(adapterCarV + dropdownListOfPinPai.value().split(',')[0] + ",");
    //                var pinpaiSV = dropdownListOfPinPai.value().split(',')[0] + ",";//用于删除选择的适配车型
    //                var adaptAll = pinpai + " > 所有车系 > 所有车型";
    //                $(".append_adpapt").append("<div class=\"append_adpapt_single\"><i class=\"ture_img\"></i><span class=\"car_tip\">" + adaptAll + "</span><i class=\"colse_img\" sel=\"" + pinpaiSV + "\"></i></div>");
    //            }
    //        }
    //        else if (dropdownListOfVersion.value() == "-1") {

    //            var addSvalue = dropdownListOfSeries.value().split(',')[0];
    //            if ($("#adapterCar").val().indexOf(addSvalue) > -1) {
    //                alert("你已添加了该车型");
    //            } else {

    //                $("#adapterCar").val(adapterCarV + dropdownListOfSeries.value().split(',')[0] + ",");
    //                var seriesSV = dropdownListOfSeries.value().split(',')[0] + ",";//用于删除选择的适配车型
    //                var adaptAll = pinpai + " > " + Series + " > " + "所有车型";
    //                $(".append_adpapt").append("<div class=\"append_adpapt_single\"><i class=\"ture_img\"></i><span class=\"car_tip\">" + adaptAll + "</span><i class=\"colse_img\" sel=\"" + seriesSV + "\"></i></div>");
    //            }
    //        } else {

    //            var addVvalue = dropdownListOfVersion.value().split(',')[0];
    //            if ($("#adapterCar").val().indexOf(addVvalue) > -1) {
    //                alert("你已添加了该车型");
    //            } else {
    //                $("#adapterCar").val(adapterCarV + dropdownListOfVersion.value().split(',')[0] + ",")
    //                var VersionSV = dropdownListOfVersion.value().split(',')[0] + ",";//用于删除选择的适配车型
    //                var adaptAll = pinpai + " > " + Series + " > " + Version;
    //                $(".append_adpapt").append("<div class=\"append_adpapt_single\"><i class=\"ture_img\"></i><span class=\"car_tip\">" + adaptAll + "</span><i class=\"colse_img\" sel=\"" + VersionSV + "\"></i></div>");
    //            }
    //        }
    //    } else {
    //        alert("请选择，再添加！");
    //        return false;
    //    }

    //})



    //添加适配车型 jia插件版
    $("#addAdaptCar").click(function () {

        var dropdownListOfPinPai = $("#CarPinPaiSelect");
        var dropdownListOfSeries = $("#CarSeriesSelect");
        var dropdownListOfVersion = $("#CarVersionSelect");


        //显示用户选择的车型车系车款  大众 > 斯柯达 > 所有车型
        var pinpaiV = dropdownListOfPinPai.val();
        var SeriesV = dropdownListOfSeries.val();
        var VersionV = dropdownListOfVersion.val();


        var pinpai = pinpaiV.split(',')[1];
        var Series = SeriesV == "-1" ? "" : SeriesV.split(',')[1];
        var Version = VersionV == "-1" ? "" : VersionV.split(',')[1];
        var adapterCarV = $("#adapterCar").val();

        //给adapterCar字段赋值
        if (dropdownListOfPinPai.val() != "") {
            if (dropdownListOfSeries.val() == "-1") {

                var addPValue = dropdownListOfPinPai.val().split(',')[0];
                if ($("#adapterCar").val().indexOf(addPValue) > -1) {
                    alert("你已添加了该车型");
                } else {
                    $("#adapterCar").val(adapterCarV + dropdownListOfPinPai.val().split(',')[0] + ",");
                    var pinpaiSV = dropdownListOfPinPai.val().split(',')[0] + ",";//用于删除选择的适配车型
                    var adaptAll = pinpai + " > 所有车系 > 所有车型";
                    $(".append_adpapt").append("<div class=\"append_adpapt_single\"><i class=\"ture_img\"></i><span class=\"car_tip\">" + adaptAll + "</span><i class=\"colse_img\" sel=\"" + pinpaiSV + "\"></i></div>");
                }
            }
            else if (dropdownListOfVersion.val() == "-1") {

                var addSvalue = dropdownListOfSeries.val().split(',')[0];
                if ($("#adapterCar").val().indexOf(addSvalue) > -1) {
                    alert("你已添加了该车型");
                } else {

                    $("#adapterCar").val(adapterCarV + dropdownListOfSeries.val().split(',')[0] + ",");
                    var seriesSV = dropdownListOfSeries.val().split(',')[0] + ",";//用于删除选择的适配车型
                    var adaptAll = pinpai + " > " + Series + " > " + "所有车型";
                    $(".append_adpapt").append("<div class=\"append_adpapt_single\"><i class=\"ture_img\"></i><span class=\"car_tip\">" + adaptAll + "</span><i class=\"colse_img\" sel=\"" + seriesSV + "\"></i></div>");
                }
            } else {

                var addVvalue = dropdownListOfVersion.val().split(',')[0];
                if ($("#adapterCar").val().indexOf(addVvalue) > -1) {
                    alert("你已添加了该车型");
                } else {
                    $("#adapterCar").val(adapterCarV + dropdownListOfVersion.val().split(',')[0] + ",")
                    var VersionSV = dropdownListOfVersion.val().split(',')[0] + ",";//用于删除选择的适配车型
                    var adaptAll = pinpai + " > " + Series + " > " + Version;
                    $(".append_adpapt").append("<div class=\"append_adpapt_single\"><i class=\"ture_img\"></i><span class=\"car_tip\">" + adaptAll + "</span><i class=\"colse_img\" sel=\"" + VersionSV + "\"></i></div>");
                }
            }
        } else {
            alert("请选择，再添加！");
            return false;
        }

    })


    //删除已添加的车型 jia+
    $(document).on("click", ".colse_img", function () {
        var seletV = $(this).attr("sel");
        var adapterCarVa = $("#adapterCar").val();
        adapterCarVa = adapterCarVa.replace(seletV, "");
        $("#adapterCar").val(adapterCarVa);
        $(this).parent().remove();
    })

});


//设为主图 多图版 jia+
function changImg(obj) {

    var rePlaceImgSrc = $("#imghead1").attr("src");
    $("#imgReplace").val(rePlaceImgSrc);
    var pre = $(obj).prev();
    var thisImg = $(pre).find("img");
    var imgSrc = $(thisImg).attr("src");

    if (imgSrc == undefined || imgSrc == "") {
        alert("请先选择图片，再进行主图设置操作");
    } else {
        //Input中值的替换
        var ImgIndex = $(obj).parent().index() + 1;
        //$("#MasterImg").val(MstImgIndex);//设置主图
        $("#fileImg1").val(imgSrc);
        $("#fileImg" + ImgIndex).val(rePlaceImgSrc);

        //Img src的替换
        $("#imghead1").attr("src", imgSrc);
        $("#imghead1").show();
        $(thisImg).attr("src", $("#imgReplace").val());
        $("#imgReplace").val("");
    }





}

//显设为主图
$(function () {
    $(".hc_addimg").each(function (index) {
        if (index % 2 == 0) {
            $(this).attr("class", "hc_imgshow");
        }
        else {
            $(this).attr("class", "hc_imgshow_right")
        };

    });
    $("[name='tax']").hover(function () {
        $(this).parent().parent().find(".ser_axa").hide();//先隐藏其他的
        $(this).parent().find(".set_main").show();//再显示自己
    });



    $(".upload_img").hover(function () {
    }, function () {
        $(this).find(".hc_patImg a").hide();
    });


    //删除添加的图片 多图版
    $(".hc_patImg img").click(function () {

        $(this).attr("src", "");
        $(this).hide();
        var thisIndex = $(this).parent().parent().index() + 1;
        $("#fileImg" + thisIndex).val("");//删除清空对应text的val
        $("#btnprevieww").attr("disabled", false);
    });

});

/*类别选择和赋值 */
$(function () {
    $(".replace_tire_show").each(function () {
        var $nite_next = $(this);
        $(this).mouseover(function () {
            $(this).find(".show_year").show();
            $(this).find("em").attr("class", "mas_show_new");
        });
        $(this).find(".show_year").mouseout(function () {
            $(this).hide();
            $(this).find("em").attr("class", "mas_show");

        });
        $(this).find(".replace_tire").mouseout(function () {
            $nite_next.find(".show_year").hide();
            $nite_next.find("em").attr("class", "mas_show");
        });
    });

    $(".show_year").each(function () {
        var $show_year = $(".show_year")
        $(this).find("li").each(function () {
            $(this).click(function () {
                $show_year.hide();
                $(this).parent().parent().find("i").html($(this).text());
                $(this).parent().parent().find("em").attr("class", "mas_show");
                //给商品所属分类赋值
                $("#ServicerTypeID").val($(this).attr("cateID"));

            });
        });
        $(this).mouseleave(function () {
            $show_year.hide();
            $(this).parent().find("em").attr("class", "mas_show");
        });
    });
});

//配件来源radio模拟
$(function () {
    var lables = $(".select_radio .input_show_one").find("label");
    $(lables).click(function () {
        var zhi = $(this).attr("checkvalue");
        $("#accInfoASdd_PartOrigin").val(zhi);
        $(this).addClass("agree_arg_next").removeClass("agree_arg");
        $(this).parent().siblings().find("label").removeClass("agree_arg_next").addClass("agree_arg");
    });



    var Postagelables = $(".postageU .input_show_one").find("label");
    $(Postagelables).click(function () {
        var zhi = $(this).attr("checkvalue");
        $("#accInfoAcc_IsPostage").val(zhi);
        $(this).addClass("agree_arg_next").removeClass("agree_arg");
        $(this).parent().siblings().find("label").removeClass("agree_arg_next").addClass("agree_arg");
    });
    
})


//是否禁用上传图片按钮
$(function () {
    var ems = $("#preview img");

    var fullFlag = true;
    $(ems).each(function (imgI, v) {
        if ($(v).attr("src") == undefined || $(v).attr("src") == "") {
            fullFlag = false;
            return false;
        }
    })
    if (fullFlag) {
        $("#btnprevieww").attr("disabled", 'disabled');
    } else {
        $("#btnprevieww").attr("disabled", false);
    }
})
