var corpcompstate = false;
var addressrangestate = false;
var addressstate = false;
var contactwaystate = false;
var exptypestate = false;
var liceidstate = false;
var liceimgstate = false;
var imguploadstate = false;
var shopclassstate = false;
var shopclass = "";

//验证公司名称
function checkCorpcomp(corpcomp) {
    var format = new RegExp("^[A-Za-z0-9_()（）\\-\\u4e00-\\u9fa5]+$").test($(corpcomp).val());
    if ($(corpcomp).val() == "请输入公司全称" || $(corpcomp).val() == "") {
        $("#corpcomp_error").attr("class", "spanmsci");
        $("#corpcomp_error").html("请输入公司全称");
        $("#corpcomp_succeed").hide();
        corpcompstate = false;
    } else if ($(corpcomp).val().length < 4 || $(corpcomp).val().length > 40) {
        $("#corpcomp_error").attr("class", "spanmsci")
        $("#corpcomp_error").html("公司名称长度只能在4-40位字符之间");
        $("#corpcomp_succeed").hide();
        corpcompstate = false;
    } else if (!format) {
        $("#corpcomp_error").attr("class", "spanmsci")
        $("#corpcomp_error").html("公司名称只能由中文、英文、数字及“_”、“-”、()、（）组成！");
        $("#mobile_succeed").hide();
        corpcompstate = false;
    } else {
        $.getJSON("/Account/CheckComcorpAndLiced/?p_str=" + $(corpcomp).val(), function (data) {
            if (data.success == 0) {
                $("#corpcomp_succeed").show();
                $("#corpcomp_error").html("&nbsp;");
                corpcompstate = true;
            }
            else if (data.success == 1) {
                $("#corpcomp_error").attr("class", "spanmsci")
                $("#corpcomp_error").html("您输入的公司名称已被注册，重新输入");
                $("#mobile_succeed").hide();
                corpcompstate = false;
            }
            else {
                $("#corpcomp_error").attr("class", "spanmsci")
                $("#corpcomp_error").html("网络繁忙,请刷新页面重试！");
                $("#mobile_succeed").hide();
                corpcompstate = false;
            }
        });
    }
}

//验证所在区域
function checkAddressRange(province, city, area, town) {
    if ($(province).val() == "") {
        $("#addressrange_error").attr("class", "spanmsci");
        $("#addressrange_error").html("地址信息不完整");
        $("#addressrange_succeed").hide();
        addressrangestate = false;
    } else if ($(city).is(":visible")) {
        if ($(city).val() == "") {
            $("#addressrange_error").attr("class", "spanmsci");
            $("#addressrange_error").html("地址信息不完整");
            $("#addressrange_succeed").hide();
            addressrangestate = false;
        } else addressrangestate = true;
    } else if ($(area).is(":visible")) {
        if ($(area).val() == "") {
            $("#addressrange_error").attr("class", "spanmsci");
            $("#addressrange_error").html("地址信息不完整");
            $("#addressrange_succeed").hide();
            addressrangestate = false;
        } else addressrangestate = true;
    } else if ($(town).is(":visible")) {
        if ($(town).val() == "") {
            $("#addressrange_error").attr("class", "spanmsci");
            $("#addressrange_error").html("地址信息不完整");
            $("#addressrange_succeed").hide();
            addressrangestate = false;
        } else addressrangestate = true;
    } else {
        $("#addressrange_succeed").show();
        $("#addressrange_error").html("&nbsp;");
        addressrangestate = true;
    }
}

//验证详细地址
function checkAddress(address) {
    var format = new RegExp("^[A-Za-z0-9_()（）\\#\\-\\u4e00-\\u9fa5]+$").test($(address).val());
    if ($(address).val() == "请输入详细地址" || $(address).val() == "") {
        $("#address_error").attr("class", "spanmsci");
        $("#address_error").html("请输入详细地址");
        $("#address_succeed").hide();
        addressstate = false;
    } else if ($(address).val().length < 4 || $(address).val().length > 50) {
        $("#address_error").attr("class", "spanmsci")
        $("#address_error").html("详细地址长度只能在4-50位字符之间");
        $("#address_succeed").hide();
        addressstate = false;
    } else if (!format) {
        $("#address_error").attr("class", "spanmsci")
        $("#address_error").html("详细地址只能由中文、英文、数字及“_”、“-”、()、（）、#组成");
        $("#address_succeed").hide();
        addressstate = false;
    } else {
        $("#address_succeed").show();
        $("#address_error").html("&nbsp;");
        addressstate = true;
    }
}

//验证联系方式
function checkContactWay(phoneno, phone1, phone2, phone3) {
    var format1 = new RegExp("^0?(1)[0-9]{10}$").test($(phoneno).val());
    var fixphone = ($(phone3).val() == "分机" ? ($(phone1).val() + "-" + $(phone2).val()) : ($(phone1).val() + "-" + $(phone2).val() + "-" + $(phone3).val()));
    var format2 = new RegExp("^([0-9]{2,4}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,6})?$").test(fixphone);
    if (($(phoneno).val() == "联系人手机" || $(phoneno).val() == "") && (fixphone == "区号-固话号码" || fixphone == "-固话号码" || fixphone == "区号-" || fixphone == "区号-固话号码-")) {
        $("#cntactway_error").attr("class", "spanmsci");
        $("#cntactway_error").html("请输入联系人手机或固定电话");
        $("#cntactway_succeed").hide();
        contactwaystate = false;
    } else {
        if ($(phoneno).val() != "联系人手机") {
            if (!format1) {
                $("#cntactway_error").attr("class", "spanmsci");
                $("#cntactway_error").html("联系人手机格式错误，请重新输入");
                $("#cntactway_succeed").hide();
                contactwaystate = false;
            }
            else {
                $("#cntactway_succeed").show();
                $("#cntactway_error").html("&nbsp;");
                $("#moblie").val($(phoneno).val());
                contactwaystate = true;
            }
        }
        if (fixphone != "区号-固话号码" && fixphone != "-固话号码" && fixphone != "区号-" && fixphone != "区号-固话号码-") {
            if (!format2) {
                $("#cntactway_error").attr("class", "spanmsci");
                $("#cntactway_error").html("固定电话格式错误，请重新输入");
                $("#cntactway_succeed").hide();
                contactwaystate = false;
            }
            else {
                $("#cntactway_succeed").show();
                $("#cntactway_error").html("&nbsp;");
                $("#fixphone").val(fixphone);
                contactwaystate = true;
            }
        }
    }
}

//验证企业类型
function checkExptype(exptype) {
    if ($(exptype).val() == "=请选择=" || $(exptype).val() == "") {
        $("#exptype_error").attr("class", "spanmsci");
        $("#exptype_error").html("请选择企业类型");
        $("#exptype_succeed").hide();
        exptypestate = false;
    } else {
        $("#exptype_succeed").show();
        $("#exptype_error").html("&nbsp;");
        exptypestate = true;
    }
}

//验证企业营业执照ID
function checkLiceid(liceid) {
    var format = new RegExp(/^\d{15}$/).test($(liceid).val());
    if ($(liceid).val() == "") {
        $("#liceid_error").attr("class", "spanmsci");
        $("#liceid_error").html("请输入企业营业执照ID");
        $("#liceid_succeed").hide();
        liceidstate = false;
    } else if (!format) {
        $("#liceid_error").attr("class", "spanmsci");
        $("#liceid_error").html("企业营业执照ID由15位数字组成");
        $("#liceid_succeed").hide();
        liceidstate = false;
    } else {
        $.getJSON("/Account/CheckComcorpAndLiced/?p_str=" + $(liceid).val(), function (data) {
            if (data.success == 0) {
                $("#liceid_succeed").show();
                $("#liceid_error").html("&nbsp;");
                liceidstate = true;
            }
            else if (data.success == 1) {
                $("#liceid_error").attr("class", "spanmsci");
                $("#liceid_error").html("您输入的企业营业执照ID已被注册，重新输入");
                $("#liceid_succeed").hide();
                liceidstate = false;
            }
            else {
                $("#liceid_error").attr("class", "spanmsci");
                $("#liceid_error").html("网络繁忙,请刷新页面重试！");
                $("#liceid_succeed").hide();
                liceidstate = false;
            }
        });
    }
}

//验证营业执照
function checkLiceImg(liceimg) {

    if ($(liceimg).val() == "") {
        $("#liceimg_error").attr("class", "spanmsci");
        $("#liceimg_error").html("请上传营业执照");
        $("#liceimg_succeed").hide();
        liceimgstate = false;
    } else {

        //获取验证图片的类型
        var msg = "";
        var strTemp = $(liceimg).val().split(".");
        var strCheck = strTemp[strTemp.length - 1];

        //验证图片类型库
        var imagetype = "jpg|bmp|jpeg|png";
        var typelst = imagetype.split("|");

        var format = false;
        for (var i = 0; i < typelst.length; i++) {
            var type = typelst[i];
            if (type == strCheck.toLowerCase()) {
                format = true;
            }
        }

        if (format) {
            var fileSize = 0;
            try {
                fileSize = $(liceimg).get(0).files[0].size;
            } catch (e) {
                fileSize = 1024;
            }
            if ((fileSize / 1024) > 4096) {
                $("#liceimg_error").attr("class", "spanmsci");
                $("#liceimg_error").html("上传的营业执照图片不能超过4m");
                $("#liceimg_succeed").hide();
                liceimgstate = false;
            }
            else {
                $("#liceimg_succeed").show();
                $("#liceimg_error").html("&nbsp;");
                liceimgstate = true;
            }
        }
        else {
            $("#liceimg_error").attr("class", "spanmsci");
            $("#liceimg_error").html("上传的营业执照图片图片的格式不正确");
            $("#liceimg_succeed").hide();
            liceimgstate = false;
        }
    }
}

//验证主营大类
function checkShopClass(type) {
    if (type == "1") {
        var server = false;
        $(".hc_chk input[name=product]").each(function () { if ($(this).attr("checked")) { server = true; return false; } });
        if (server) {
            $("#shopclass_succeed").show();
            $("#shopclass_error").html("&nbsp;");
            shopclassstate = true;
        } else {
            $("#shopclass_error").attr("class", "spanmsci");
            $("#shopclass_error").html("请选择主营大类");
            $("#shopclass_succeed").hide();
            shopclassstate = false;
        }
    } else {
        var shop = false;
        $(".hc_chk input[type=checkbox]").each(function () { if ($(this).attr("checked")) { shop = true; return false; } });
        if (shop) {
            $("#shopclass_succeed").show();
            $("#shopclass_error").html("&nbsp;");
            shopclassstate = true;
        } else {
            $("#shopclass_error").attr("class", "spanmsci");
            $("#shopclass_error").html("请选择主营大类");
            $("#shopclass_succeed").hide();
            shopclassstate = false;
        }
    }
}

//图片上传预览    IE是用了滤镜。
function previewImage(file) {
    $("#imghead").attr('src', "");
    $("#imghead").attr('style', "");

    //验证营业执照
    checkLiceImg(file);
    if (!liceimgstate) return;

    var MAXWIDTH = 150;
    var MAXHEIGHT = 150;
    var div = document.getElementById('preview');
    if (file.files && file.files[0]) {
        div.innerHTML = '<img id=imghead>';
        var img = document.getElementById('imghead');
        img.onload = function () {
            var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
            $(img).attr("style", "width:" + rect.width + "px;height:" + rect.height + "px;margin-Top:" + rect.top + "px;");
        }
        var reader = new FileReader();
        reader.onload = function (evt) { img.src = evt.target.result; }
        reader.readAsDataURL(file.files[0]);
    }
    else //兼容IE
    {
        var img = document.getElementById('imghead');
        img.src = file.value;
        rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
        div.innerHTML = "<img id='imghead' style='max-width:150px;max-height:150px;width:" + rect.width + "px;height:" + rect.height + "px;margin-top:" + rect.top + "px;' src='" + file.value + "'>";
    }
}

function clacImgZoomParam(maxWidth, maxHeight, width, height) {
    var param = { top: 0, left: 0, width: width, height: height };
    if (width > maxWidth || height > maxHeight) {
        rateWidth = width / maxWidth;
        rateHeight = height / maxHeight;

        if (rateWidth > rateHeight) {
            param.width = maxWidth;
            param.height = Math.round(height / rateWidth);
        } else {
            param.width = Math.round(width / rateHeight);
            param.height = maxHeight;
        }
    }

    param.left = Math.round((maxWidth - param.width) / 2);
    param.top = Math.round((maxHeight - param.height) / 2);
    return param;
}

//绑定选择的所在区域
function showAddressDetail(type) {
    var address;
    switch (type) {
        case "province":
            address = $("#selprovince").find("option:selected").text();
            if (address == "--请选择--") address="";
            $("#power").html(address);
            $("#power1").html("");
            $("#power2").html("");
            $("#power3").html("");
            $("#selcity").empty();
            $("#selarea").empty();
            $("#seltown").empty();
            break;
        case "city":
            address = $("#selcity").find("option:selected").text();
            if (address == "--请选择--") address = "";
            else address = (" " + address);
            $("#power1").html(address);
            $("#power2").html("");
            $("#power3").html("");
            $("#selarea").empty();
            $("#seltown").empty();
            break
        case "area":
            address = $("#selarea").find("option:selected").text();
            if (address == "--请选择--") address = "";
            else address = (" " + address);
            $("#power2").html(address);
            $("#power3").html("");
            $("#seltown").empty();
            break;
        case "town":
            address = $("#seltown").find("option:selected").text();
            if (address == "--请选择--") address = "";
            else address = (" " + address);
            $("#power3").html(address);
            break;
        default:
            break;
    } 
};

//页面加载处理
$(function () {

    //页面初始化
    if ($("#province").val() != "") {
        $("#selcity").trigger("change");
    }
    //$("#selcity").empty();
    //$("#selarea").empty();
    //$("#seltown").empty();
    //$("#corpcomp").val("请输入公司全称");
    //$("#address").val("请输入详细地址");
    //$("#phoneno").val("联系人手机");
    //$("#phone1").val("区号");
    //$("#phone2").val("固话号码");
    //$("#phone3").val("分机");
    //$("#type").val(0);
    //$("#liceid").val("请输入企业营业执照ID");
    //$("#fileImg").val("");
    //$(".hc_chk input[type=checkbox]").each(function () { $(this).attr("checked", false) });
    //$("#ShopDesc").val("");
    if ($("#msg").val() != "") {
        $("#liceimg_error").attr("class", "spanmsci");
        $("#liceimg_error").html($("#msg").val());
        $("#liceimg_succeed").hide();
        liceimgstate = false;
    }

    //绑定页面上的验证事件
    $("#corpcomp").change(function () { checkCorpcomp($("#corpcomp")); });
    $("#address").change(function () { checkAddress($("#address")); });
    $("#phoneno").change(function () { checkContactWay($("#phoneno"), $("#phone1"), $("#phone2"), $("#phone3")); });
    $("#phone1").change(function () { checkContactWay($("#phoneno"), $("#phone1"), $("#phone2"), $("#phone3")); });
    $("#phone2").change(function () { checkContactWay($("#phoneno"), $("#phone1"), $("#phone2"), $("#phone3")); });
    $("#phone3").change(function () { checkContactWay($("#phoneno"), $("#phone1"), $("#phone2"), $("#phone3")); });
    $("#liceid").change(function () { checkLiceid($("#liceid")); });
    $("#corpcomp").focus(function () { if ($(this).val() == "请输入公司全称") { $(this).val("") }; checkCorpcomp($("#corpcomp")); });
    $("#address").focus(function () { if ($(this).val() == "请输入详细地址") { $(this).val("") }; checkAddress($("#address")); });
    $("#phoneno").focus(function () { if ($(this).val() == "联系人手机") { $(this).val("") }; checkContactWay($("#phoneno"), $("#phone1"), $("#phone2"), $("#phone3")); });
    $("#phone1").focus(function () { if ($(this).val() == "区号") { $(this).val("") }; checkContactWay($("#phoneno"), $("#phone1"), $("#phone2"), $("#phone3")); });
    $("#phone2").focus(function () { if ($(this).val() == "固话号码") { $(this).val("") }; checkContactWay($("#phoneno"), $("#phone1"), $("#phone2"), $("#phone3")); });
    $("#phone3").focus(function () { if ($(this).val() == "分机") { $(this).val("") }; checkContactWay($("#phoneno"), $("#phone1"), $("#phone2"), $("#phone3")); });
    $("#liceid").focus(function () { if ($(this).val() == "请输入企业营业执照ID") { $(this).val("") }; checkLiceid($("#liceid")); });
    $("#corpcomp").blur(function () { if ($(this).val() == "") { $(this).val("请输入公司全称");} });
    $("#address").blur(function () { if ($(this).val() == "") { $(this).val("请输入详细地址");} });
    $("#phoneno").blur(function () { if ($(this).val() == "") { $(this).val("联系人手机");}})
    $("#phone1").blur(function () { if ($(this).val() == "") { $(this).val("区号");}  });
    $("#phone2").blur(function () { if ($(this).val() == "") { $(this).val("固话号码"); }});
    $("#phone3").blur(function () { if ($(this).val() == "") { $(this).val("分机");} });
    $("#liceid").blur(function () { if ($(this).val() == "") { $(this).val("请输入企业营业执照ID"); } });

    $.getJSON("/Account/GetAddress?unid=0", function (data) {
        if (data != "") {
            $("#selprovince").empty();
            $("#selprovince").append("<option value=\"\">--请选择--</option>");

            $.each(data, function (i, item) {
                $("#selprovince").append("<option value=\"" + item["unid"] + "\">" + item["city"] + "</option>");
            });
        } else {
            $("#selprovince").empty();
            $("#selprovince").append("<option value=\"\">--请选择--</option>");
        }
    });
    $("#selprovince").find("option[text='" + $("#province").val() + "']").attr("selected", true);

    $("#selprovince").change(function () {
        var province = $(this).find("option:selected").text();
        var provincecode = $(this).val();
        $("#province").val(province);
        $("#provincecode").val(provincecode);
        showAddressDetail("province");

        $.getJSON("/Account/GetAddress?unid=" + provincecode, function (data) {
            if (data != "") {

                //验证区域地质显示
                $("#addressrange_error").attr("class", "spanmsci");
                $("#addressrange_error").html("地址信息不完整");
                $("#addressrange_succeed").hide();
                addressrangestate = false;

                $("#selcity").empty();
                $("#selcity").append("<option value=\"\">--请选择--</option>");
                $("#selarea").empty();
                $("#seltown").empty();
                $("#seldivcity").show();
                $("#divarea").show();

                $.each(data, function (i, item) {
                    $("#selcity").append("<option value=\"" + item["unid"] + "\">" + item["city"] + "</option>");
                });
            } else {

                //验证区域地质显示
                $("#addressrange_succeed").show();
                $("#addressrange_error").html("&nbsp;");
                addressrangestate = true;

                $("#selcity").empty();
                $("#selcity").append("<option value=\"\">--请选择--</option>");
                $("#divcity").hide();
                $("#divarea").hide();
                $("#divtown").hide();
            }
        });
    });

    $("#selcity").change(function () {
        var city = $(this).find("option:selected").text();
        var citycode = $(this).val();
        $("#city").val(city);
        $("#citycode").val(citycode);
        showAddressDetail("city");
        $.getJSON("/Account/GetAddress?unid=" + citycode, function (data) {
            if (data != "") {

                //验证区域地质显示
                $("#addressrange_error").attr("class", "spanmsci");
                $("#addressrange_error").html("地址信息不完整");
                $("#addressrange_succeed").hide();
                addressrangestate = false;

                $("#selarea").empty();
                $("#selarea").append("<option value=\"\">--请选择--</option>");
                $("#seltown").empty();

                $.each(data, function (i, item) {
                    $("#selarea").append("<option value=\"" + item["unid"] + "\">" + item["city"] + "</option>");
                });
            } else {

                //验证区域地质显示
                $("#addressrange_succeed").show();
                $("#addressrange_error").html("&nbsp;");
                addressrangestate = true;

                $("#selarea").empty();
                $("#selarea").append("<option value=\"\">--请选择--</option>");
                $("#selarea").hide();
            }
        });

    });

    $("#selarea").change(function () {
        var area = $(this).find("option:selected").text();
        var areacode = $(this).val();
        $("#area").val(area);
        $("#areacode").val(areacode);
        showAddressDetail("area");
        $.getJSON("/Account/GetAddress?unid=" + areacode, function (data) {
            if (data != "") {

                //验证区域地质显示
                $("#addressrange_error").attr("class", "spanmsci");
                $("#addressrange_error").html("地址信息不完整");
                $("#addressrange_succeed").hide();
                addressrangestate = false;

                $("#seltown").empty();
                $("#seltown").append("<option value=\"\">--请选择--</option>");

                $.each(data, function (i, item) {
                    $("#seltown").append("<option value=\"" + item["unid"] + "\">" + item["city"] + "</option>");
                });
                $("#divtown").show();
            } else {

                //验证区域地质显示
                $("#addressrange_succeed").show();
                $("#addressrange_error").html("&nbsp;");
                addressrangestate = true;

                $("#seltown").empty();
                $("#seltown").append("<option value=\"\">--请选择--</option>");
                $("#divtown").hide();
            }
        });
    });

    $("#btnpreview").click(function () { $("#fileImg").trigger("click"); });
    $("#fileImg").change(function () { previewImage(this); });

    $("#seltown").change(function () {

        //验证区域地质显示
        $("#addressrange_succeed").show();
        $("#addressrange_error").html("&nbsp;");
        addressrangestate = true;

        var town = $(this).find("option:selected").text();
        var towncode = $(this).val();
        $("#town").val(town);
        $("#towncode").val(towncode);
        showAddressDetail("town");
    });

    $("#type").change(function () {
        shopclass = "";
        if ($(this).val() == "=请选择=" || $(this).val() == "") {
            $("#exptype_error").attr("class", "spanmsci");
            $("#exptype_error").html("请选择企业类型");
            $("#exptype_succeed").hide();
            exptypestate = false;

            $("#rdocBeauty").show();
            $("#rdocRepair").show();
            $("#rdocRefit").show();
            $("#rdocShop").show();
            $("#cBeauty").show();
            $("#cRepair").show();
            $("#cRefit").show();
            $("#cShop").show();
            $("#shopclass_error").attr("style", "margin:60px");

            $("#shopclass").val(shopclass);
        } else {
            $("#exptype_succeed").show();
            $("#exptype_error").html("&nbsp;");
            exptypestate = true;

            if ($(this).val() == "1") {
                $("#rdocBeauty").hide();
                $("#rdocRepair").hide();
                $("#rdocRefit").hide();
                $("#rdocShop").hide();
                $("#cBeauty").hide();
                $("#cRepair").hide();
                $("#cRefit").hide();
                $("#cShop").hide();
                $("#shopclass_error").attr("style", "margin:0px");

                $(".hc_chk input[name=product]").each(function () {
                    $(this).click(function () {
                        $("#shopclass_error").html("&nbsp;");
                        shopclassstate = true;
                    });
                });
            }
            else {
                $("#rdocBeauty").show();
                $("#rdocRepair").show();
                $("#rdocRefit").show();
                $("#rdocShop").show();
                $("#cBeauty").show();
                $("#cRepair").show();
                $("#cRefit").show();
                $("#cShop").show();
                $("#shopclass_error").attr("style", "margin:60px");

                $(".hc_chk input[type=checkbox]").each(function () {
                    $(this).click(function () {
                        $("#shopclass_error").html("&nbsp;");
                        shopclassstate = true;
                    });
                });
            }
        }

        $("#exptype").val($(this).val());
    });

    $(".hc_chk input[type=checkbox]").each(function () {
        $(this).click(function () {
            $("#shopclass_error").html("&nbsp;");
            shopclassstate = true;
        });
    });

    //提交注册的验证
    $("#btnAttestation").click(function () {
        checkCorpcomp($("#corpcomp"));
        checkAddressRange($("#selprovince"), $("#selcity"), $("#selarea"), $("#seltown"));
        checkAddress($("#address"));
        checkContactWay($("#phoneno"), $("#phone1"), $("#phone2"), $("#phone3"));
        checkExptype($("#type"));
        checkLiceid($("#liceid"));
        checkLiceImg($("#fileImg"));
        checkShopClass($("#type").val());
        if (addressrangestate && addressrangestate && addressstate && contactwaystate && exptypestate && liceidstate && liceimgstate && shopclassstate) {
            if ($(this).val() == "1") {
                $(".hc_chk input[name=product]").each(function () {
                    shopclass = (shopclass == "" ? $(this).val() : (shopclass + "," + $(this).val()));
                });
            }
            else {
                $(".hc_chk input[type=checkbox]").each(function () {
                    shopclass = (shopclass == "" ? $(this).val() : (shopclass + "," + $(this).val()));
                });
            }
            $("#shopclass").val(shopclass);
            return true;
        }
        else return false;
    });
});