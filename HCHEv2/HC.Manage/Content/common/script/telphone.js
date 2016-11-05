var userNamestate = true;//手机错误
var fixationTel = true;//固话区号错误
var specialtleTel = true;//固话错误
var fixationExt = true;//分机号错误
var disfaxesTel = true;//传真区号错误
var disfaxesTelphon = true;//传真错误
var addteldiv;
var addtelephone;

//手机验证
function checkUserName(obj) {
    var format = new RegExp("^0?(1)[0-9]{10}$").test($(obj).val());
    var errorLable = $(obj).parent().find(".prompt_info label:eq(1)");
    var okLable = $(obj).parent().find(".prompt_info label:eq(0)");


    if ($(obj).val() != "") {
        if (!format) {
            $(errorLable).html("<em class=\"errorem\"></em>手机号码格式不正确，请重新输入！");
            $(okLable).hide();
            $(errorLable).show();
            userNamestate = false;
        } else {

            $(okLable).show();
            $(errorLable).hide();
            userNamestate = true;
        }
    } else {
        $(okLable).hide();
        $(errorLable).hide();
    }
}
//区号
function checkfixation(obj) {
    var disteelphone = new RegExp("^[0-9]{3,4}$").test($(obj).val());
    var errorLable = $(obj).parent().find(".prompt_info label:eq(1)");
    var okLable = $(obj).parent().find(".prompt_info label:eq(0)");
    var numfix = $(obj).parent().find("input")[1];
    var subfix = $(obj).parent().find("input")[2];
    if (($(obj).val() != "") || ($(numfix).val() != "") || ($(subfix).val() != "")) {//都为空的情况下不验证
        if ($(obj).val() == "") {
            $(errorLable).html("<em class=\"errorem\"></em>请输入区号");
            $(okLable).hide();
            $(errorLable).show();
            fixationTel = false;
            return false;
        }
        if (!disteelphone) {
            $(errorLable).html("<em class=\"errorem\"></em>区号格式不正确，请重新输入！");
            $(okLable).hide();
            $(errorLable).show();
            fixationTel = false;
            return false;
        } else {
            $(okLable).show();
            $(errorLable).hide();
            fixationTel = true;
            return true;
        }
    } else {
        $(okLable).hide();
        $(errorLable).hide();
        fixationTel = true;
    }
}
//固话
function checkspecialtle(obj) {
    var preall = $(obj).prevAll();
    var arearCodeInput = preall[1];

    if (checkfixation($(arearCodeInput))) {
        var telformat = new RegExp("^[0-9]{7,8}$").test($(obj).val());
        var errorLable = $(obj).parent().find(".prompt_info label:eq(1)");
        var okLable = $(obj).parent().find(".prompt_info label:eq(0)");
        if ($(obj).val() == "") {
            $(errorLable).html("<em class=\"errorem\"></em>请输入电话");
            $(okLable).hide();
            $(errorLable).show();
            specialtleTel = false;
            return false;
        }
        else if (!telformat) {
            $(errorLable).html("<em class=\"errorem\"></em>电话号码格式不正确，请重新输入！");
            $(okLable).hide();
            $(errorLable).show();
            specialtleTel = false;
            return false;
        } else {
            $(okLable).show();
            $(errorLable).hide();
            specialtleTel = true;
            return true;
        }
    }
}

//分机号
function checktxtExt(obj) {

    var areaNuminput = $(obj).prevAll()[3];
    var fixNuminput = $(obj).prevAll()[1];

    if (checkspecialtle($(fixNuminput)) && checkfixation($(areaNuminput))) {
        var disteelphoneS = new RegExp("^[0-9]{1,4}$").test($(obj).val());
        var errorLable = $(obj).parent().find(".prompt_info label:eq(1)");
        var okLable = $(obj).parent().find(".prompt_info label:eq(0)");
        if ($(obj).val().length > 0) {
            if (!disteelphoneS) {
                $(errorLable).html("<em class=\"errorem\"></em>分机号格式不正确，请重新输入！");
                $(okLable).hide();
                $(errorLable).show();
                fixationExt = false;
            } else {
                $(okLable).show();
                $(errorLable).hide();
                fixationExt = true;
            }
        }
    }
}
//传真区号
function checkdisfaxesTel(disfaxesphon) {

    var format = new RegExp("^[0-9]{3,4}$").test($(disfaxesphon).val());
    //if ($(disfaxesphon).val() == "" && $(disfaxestelphon).val() == "") {
    //    $("#mobile_disfaxesno").html("<em class=\"errorem\"></em>请输入区号");
    //    $("#mobile_disfaxes").hide();
    //    $("#mobile_disfaxesno").show();
    //    disfaxesTel = false;
    //} else
    if ($(disfaxesphon).val() != "" || $(disfaxestelphon).val() != "") {
        if (!format) {
            $("#mobile_disfaxesno").html("<em class=\"errorem\"></em>区号格式不正确，请重新输入！");
            $("#mobile_disfaxes").hide();
            $("#mobile_disfaxesno").show();
            disfaxesTel = false;
        } else {
            $("#mobile_disfaxes").show();
            $("#mobile_disfaxesno").hide();
            disfaxesTel = true;
            return true;
        }
    } else {
        $("#mobile_disfaxes").hide();
        $("#mobile_disfaxesno").hide();
        disfaxesTel = true;
        return true;
    }
}

//传真号码
function checkdisfaxestelphon(disfaxestelphon) {
    var disfformat = new RegExp("^[0-9]{7,8}$").test($(disfaxestelphon).val());
    //if ($(disfaxestelphon).val() == "") {
    //    $("#mobile_disfaxesno").html("<em class=\"errorem\"></em>请输入电话");
    //    $("#mobile_disfaxes").hide();
    //    $("#mobile_disfaxesno").show();
    //    disfaxesTelphon = false;
    //}
    //else
    if ($(disfaxesphon).val() != "" || $(disfaxestelphon).val() != "") {
        if (!disfformat) {
            $("#mobile_disfaxesno").html("<em class=\"errorem\"></em>传真号码格式不正确，请重新输入！");
            $("#mobile_disfaxes").hide();
            $("#mobile_disfaxesno").show();
            disfaxesTelphon = false;
        } else {
            $("#mobile_disfaxes").show();
            $("#mobile_disfaxesno").hide();
            disfaxesTelphon = true;
        }
    } else {
        $("#mobile_disfaxes").hide();
        $("#mobile_disfaxesno").hide();
        disfaxesTelphon = true;
    }
}
$(function () {
    //固话验证事件绑定
    $(document).on("change", "#hctelephone .samaddtelphon .areacode", function () {
        checkfixation($(this));
    })
    $(document).on("change", "#hctelephone .samaddtelphon .hc_telint", function () {
        checkspecialtle($(this));
    })
    $(document).on("change", "#hctelephone .samaddtelphon .subcode", function () {
        checktxtExt($(this));
    })


    //传真验证事件绑定
    $("#disfaxesphon").change(function () { checkdisfaxesTel($("#disfaxesphon")); });
    $("#disfaxestelphon").change(function () { checkdisfaxestelphon($("#disfaxestelphon")); });
   
    
    //手机验证事件绑定
    $(document).on("change", "#hc_telephone input", function () {
        checkUserName($(this));
    });
});

//增加固话
function addphone() {
    $("#hctelephone").append("<div class='samaddtelphon' style='width:590px;'><input type='text' name='txtkbn' placeholder='区号' class='areacode' /><span class='spangapa'>-</span><input type='text' name='txtphoneno' placeholder='座机号码' class='hc_telint' /><span class='spangapa'>-</span><input type='text' name='txtExt' placeholder='分机号' class='subcode' /><span class='closetel' onclick='dropElement(this)' title='关闭'>X</span><span class='prompt_info' style='width:240px;'><label style='display:none' class='blank labelblank'><img src='/HC.Manage/images/publicimg/Input_ok.png' /></label><label class='textlabel'></label></span></div>");
}

//增加手机号
//var i=1
function addcellphone() {
    $("#hc_telephone").append("<div class='samaddtelphon'><input type='text' placeholder='手机号' class='hc_cellphone' /><span class='closetel' onclick='dropElement(this)' title='关闭'>X</span><span class='prompt_info'><label style='display:none' class='blank'><img src='/HC.Manage/images/publicimg/Input_ok.png' /></label><label></label></span></div>");
    //$("#hc_telephone .samaddtelphon").id = 'Elem' + i;
    //i++;
}

//修改联系方式
$(document).ready(function () {
    $("#altertelphon").click(function () {
        $("#maxalterphone").show();
    });
    
    $(".buttonsus").click(function () {
        //触发change事件做提交前的验证
        $("#hc_telephone input").change();//手机触发
        //固话触发
        $("input[name='txtkbn']").change();
        $("input[name='txtphoneno']").change();
        $("input[name='txtExt']").change();
        //传真触发
        $("#disfaxesphon").change();
        $("#disfaxestelphon").change();

        if (userNamestate && fixationTel && specialtleTel && fixationExt && disfaxesTel && disfaxesTelphon) {
            var fixnums = "", mobilPhone = "", faxNum = "";
            //固话post
            $("#hctelephone .samaddtelphon").each(function (i, v) {
                var areaNum = $(v).find("input[name='txtkbn']").val();
                var Num = $(v).find("input[name='txtphoneno']").val();
                var subNum = $(v).find("input[name='txtExt']").val();

                if (areaNum.length > 0 && Num.length > 0) {
                    if (subNum.length > 0) {
                        fixnums = fixnums + areaNum + "-" + Num + "-" + subNum + ",";
                    } else {
                        fixnums = fixnums + areaNum + "-" + Num + ",";
                    }
                }
            })

            //手机号post
            $("#hc_telephone .samaddtelphon").each(function (i, v) {
                var MobileNum = $(v).find("input").val();
                if (MobileNum.length > 0) {
                    mobilPhone = mobilPhone + MobileNum + ",";
                }
            })

            //传真post
            if ($("#disfaxesphon").val().length > 0 && $("#disfaxestelphon").val().length > 0) {
                faxNum = $("#disfaxesphon").val() + "-" + $("#disfaxestelphon").val();
            }

            $("#fixPhone").val(fixnums); $("#MobilePhone").val(mobilPhone); $("#faxNum").val(faxNum);
            console.log(fixnums); console.log(mobilPhone); console.log(faxNum);
            $("#contactForm").submit();
        }
    })
});


function dropElement(e)
{
    var parent = $(e).parent();
    $(parent).remove();
}
