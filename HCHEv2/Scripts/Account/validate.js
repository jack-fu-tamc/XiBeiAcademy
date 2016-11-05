var userNamestate = false;
var authCodestate = false;
var pwdstate = false;
var rePwdstate = false;

//提交注册的验证
function RegistSubmit() {
    checkUserName($("#userName"));
    checkAuthCode($("#authCode"));
    checkPwd($("#pwd"));
    checkRePwd($("#rePwd"));
    if (userNamestate && authCodestate && pwdstate && rePwdstate) {
        if ($("#chkAgreement").attr("checked") == "checked") return true;
        else return false;
    }
    else return false;
}

//验证手机号
function checkUserName(userName) {
    var format = new RegExp("^0?(1)[0-9]{10}$").test($(userName).val());
    if ($(userName).val() == "") {
        $("#mobile_error").attr("class", "error");
        $("#mobile_error").html("<em class=\"errorem\"></em> 请输入自己正在使用的手机号码");
        $("#mobile_succeed").hide();
        $("#mobile_error").show();
        userNamestate = false;
    } else if (!format) {
        $("#authCode_error").attr("class", "error");
        $("#mobile_error").html("<em class=\"errorem\"></em>  手机号码格式不正确，请重新输入！");
        $("#mobile_succeed").hide();
        $("#mobile_error").show();
        userNamestate = false;
    } else {
        $.getJSON("/Account/CheckUserName/?userName=" + $(userName).val(), function (data) {
            if (data.success == 0) {
                $("#mobile_succeed").attr("class", "succeed");
                $("#mobile_succeed").show();
                $("#mobile_error").hide();
                userNamestate = true;
            }
            else if (data.success == 1) {
                $("#mobile_error").attr("class", "error");
                $("#mobile_error").html('<em class=\"errorem\"></em>  您输入的手机号已被注册，重新输入或<a href=\"/Home/logOn\">登录</a>');
                $("#mobile_succeed").hide();
                $("#mobile_error").show();
                userNamestate = false;
            }
            else {
                $("#mobile_error").attr("class", "error");
                $("#mobile_error").html('<em class=\"errorem\"></em>  网络繁忙');
                $("#mobile_succeed").hide();
                $("#mobile_error").show();
            }
        });
    }
}

//验证手机校验码
function checkAuthCode(authCode) {
    if ($(authCode).val() == "") {
        $("#authCode_error").attr("class", "error");
        $("#authCode_error").html("<em class=\"errorem\"></em>  请输入验证码");
        $("#authCode_succeed").hide();
        $("#authCode_error").show();
        authCodestate = false;
    } else {
        $.getJSON("/Account/CheckAuthCode/?authCode=" + $(authCode).val(), function (data) {
            if (data.success == 0) {
                $("#authCode_succeed").attr("class", "succeed");
                $("#authCode_succeed").show();
                $("#authCode_error").hide();
                authCodestate = true;
            }
            else if (data.success == 1) {
                $("#authCode_error").attr("class", "error")
                $("#authCode_error").html('<em class=\"errorem\"></em>  验证码不正确！');
                $("#authCode_succeed").hide();
                $("#authCode_error").show();
                authCodestate = false;
            }
            else {
                $("#authCode_error").attr("class", "error")
                $("#authCode_error").html('<em class=\"errorem\"></em>  网络繁忙,请刷新页面重试！');
                $("#authCode_succeed").hide();
                $("#authCode_error").show();
                authCodestate = false;
            }
        });
    }
}

//验证密码
function checkPwd(pwd) {
    var format = new RegExp("^[\sA-Za-z09*!@(.)#$\\w_-]+$").test($(pwd).val());
    if ($(pwd).val() == "") {
        $("#pwd_error").attr("class", "error");
        $("#pwd_error").html("<em class=\"errorem\"></em>  请输入密码");
        $("#pwd_succeed").hide();
        $("#pwd_error").show();
        pwdstate = false;
    } else if ($(pwd).val().length < 6 || $(pwd).val().length > 16) {
        $("#pwd_error").attr("class", "error")
        $("#pwd_error").html("<em class=\"errorem\"></em>  密码长度只能在6-16位字符之间");
        $("#pwd_succeed").hide();
        $("#pwd_error").show();
        pwdstate = false;
    } else if (!format) {
        $("#userName").attr("class", "error")
        $("#pwd_error").html("<em class=\"errorem\"></em>  密码只能由英文、数字及特殊符号组成");
        $("#pwd_succeed").hide();
        $("#pwd_error").show();
        pwdstate = false;
    } else {
        $("#pwd_succeed").attr("class", "succeed");
        $("#pwd_succeed").show();
        $("#pwd_error").hide();
        pwdstate = true;
    }
}

//验证重复密码
function checkRePwd(rePwd) {
    var format = new RegExp("^[\sA-Za-z09*!@(.)#$\\w_-]+$").test($(rePwd).val());
    if ($(rePwd).val() == "") {
        $("#rePwd_error").attr("class", "error");
        $("#rePwd_error").html("<em class=\"errorem\"></em>  请再次输入密码");
        $("#rePwd_succeed").hide();
        $("#rePwd_error").show();
        rePwdstate = false;
    } else if ($(rePwd).val().length < 6 || $(rePwd).val().length > 16) {
        $("#rePwd_error").attr("class", "error")
        $("#rePwd_error").html("<em class=\"errorem\"></em>  密码长度只能在6-16位字符之间");
        $("#rePwd_succeed").attr("class", "");
        $("#rePwd_succeed").hide();
        $("#rePwd_error").show();
        pwdstate = false;
    } else if (!format) {
        $("#rePwd_error").attr("class", "error");
        $("#rePwd_succeed").html("<em class=\"errorem\"></em>  密码只能由英文、数字及特殊符号组成");
        $("#pwd_succeed").hide();
        $("#rePwd_error").show();
        pwdstate = false;
    } else {
        if ($(rePwd).val() != $("#pwd").val()) {
            $("#rePwd_error").attr("class", "error")
            $("#rePwd_error").html("<em class=\"errorem\"></em>  前后密码不一致");
            $("#rePwd_succeed").hide();
            $("#rePwd_error").show();
            rePwdstate = false;
        } else {
            $("#rePwd_succeed").attr("class", "succeed");
            $("#rePwd_succeed").show();
            $("#rePwd_error").hide();
            rePwdstate = true;
        }
    }
}

$(function () {

    //页面数据清空
    $("#userName").val("");
    $("#authCode").val("");
    $("#authCode").attr('disabled', 'disabled');
    $("#pwd").val("");
    $("#rePwd").val("");

    $("#userName").change(function () { checkUserName($("#userName")); });
    $("#authCode").change(function () { checkAuthCode($("#authCode")); });
    $("#pwd").change(function () { checkPwd($("#pwd")); });
    $("#rePwd").change(function () { checkRePwd($("#rePwd")); });

    //切换个人车主和企业商家
    $("#rdoPersonal").click(function () { $("#UserType").val(1); });
    $("#rdoCarOwner").click(function () { $("#UserType").val(7); });

    //获取短信验证码
    $("#btnvalidate").click(function () {
        checkUserName($("#userName"));
        if (userNamestate) {
            start_sms_button();
            $.getJSON("/Account/GetValidate/?userName=" + $("#userName").val(), function (data) {
                if (data.success == 0) {
                    $("#validate").attr('disabled', false);
                    $("#authCode").focus();
                }
                else {
                    alert('网络繁忙，请稍后重新获取验证码');
                    $(this).attr('disabled', false);
                    $(this).attr('class', "valida_code");
                }
            });
        }
    });

    //验证码有效期处理
    function start_sms_button() {
        var obj = $("#btnvalidate");
        var count = 1;
        var sum = 120;
        beoverdue = false;
        obj.attr('class', "valida_disable");
        var i = setInterval(function () {
            if (count > sum) {
                beoverdue = true;
                obj.attr('disabled', false);
                obj.attr('class', "valida_code");
                obj.html('发送验证码');
                clearInterval(i);
            } else {
                obj.html('剩余' + parseInt(sum - count) + '秒');
                $("#authCode").attr("disabled", false);
            }
            count++;
        }, 1000);
    }
});