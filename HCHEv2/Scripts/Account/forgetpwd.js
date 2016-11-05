var userNamestate = false;
var authCodestate = false;
//修改密码
$(function () {
    $("#txtUserName").change(function () { checkUserName($("#txtUserName")); });

    //获取短信验证码
    $("#btnvalidate").attr("disabled", false);
    $("#btnvalidate").click(function () {
        var CurrentUrl = location.href;
        checkUserName($("#txtUserName"));
        if (userNamestate) {
            start_sms_button();
            $.ajax({
                async: false,
                url: '/Account/GetValidate?username=' + $("#txtUserName").val(),
                type: 'post',
                cache: false,
                dataType: 'json',
                success: function (data) {
                    if (data.success == 0) {
                        $(".yantext").attr("readonly", false);
                        $(".yantext").focus();
                    } else {
                        location.href = passportUrl + "/PassPort/UserLogin?returnUrl=" + encodeURI(CurrentUrl);
                    }
                }
            });
        }
    });

    //判断验证码是否正确
    $(".yantext").val("");
    $(".tijiaobut").click(function () {
        var CurrentUrl = location.href;
        checkUserName($("#txtUserName"));
        checkAuthCode($(".yantext"));
        if (!userNamestate || !authCodestate) {
            return;
        }
        $.getJSON("/Account/CheckUpdPwdAuthCode/?authCode=" + $(".yantext").val(), function (data) {
            if (data.result == 0) {
                $("#wrutepwd").html(data.wrutepwd);
                $("#alterpwddiv").hide();
                $("#wrutepwd").show();

                //修改密码
                var pwdstate = false;
                var repwdstate = false;
                $("#butnpwds").click(function () {
                    checkPwd($("#txtpwd"));
                    checkRePwd($("#txtpwd"), $("#txtrepwd"));
                    if (pwdstate && repwdstate) {
                        $.getJSON("/Account/UpdOldPwd?username=" + $("#txtUserName").val() + "&newpwd=" + $("#txtpwd").val(), function (data) {
                            if (data.result == 0) {
                                $("#userphone").html($("#txtUserName").val());
                                $("#alterpwddiv").hide();
                                $("#wrutepwd").hide();
                                $("#okpaswd").show();
                            } else {
                                location.href = passportUrl + "/PassPort/UserLogin?returnUrl=" + encodeURI(CurrentUrl);
                            }
                        });
                    }
                });

                //触法验证新密码
                $("#txtpwd").change(function () { checkPwd($("#txtpwd")); });
                //触法验证重复密码
                $("#txtrepwd").change(function () { checkRePwd($("#txtpwd"), $("#txtrepwd")); });

                //验证新密码
                function checkPwd(newpwd) {
                    var format = new RegExp("^[\sA-Za-z09*!@(.)#$\\w_-]+$").test($(newpwd).val());
                    if ($(newpwd).val() == "") {
                        $("#pwd_error").html("新密码不能为空");
                        $("#pwd_error").show();
                        $("#pwd_success").hide();
                        pwdstate = false;
                    } else if ($(newpwd).val().length < 6 || $(newpwd).val().length > 16) {
                        $("#pwd_error").html("新密码长度只能在6-16位字符之间");
                        $("#pwd_error").show();
                        $("#pwd_success").hide();
                        pwdstate = false;
                    } else if (!format) {
                        $("#pwd_error").html("新密码只能由英文、数字及特殊符号组成");
                        $("#pwd_error").show();
                        $("#pwd_success").hide();
                        pwdstate = false;
                    } else {
                        $("#pwd_error").hide();
                        $("#pwd_success").show();
                        pwdstate = true;
                    }
                }

                //验证确认新密码
                function checkRePwd(newpwd, renewpwd) {
                    var format = new RegExp("^[\sA-Za-z09*!@(.)#$\\w_-]+$").test($(renewpwd).val());
                    if ($(renewpwd).val() == "") {
                        $("#repwd_error").html("确认密码不能为空");
                        $("#repwd_error").show();
                        $("#repwd_success").hide();
                        repwdstate = false;
                    } else if ($(renewpwd).val().length < 6 || $(renewpwd).val().length > 16) {
                        $("#repwd_error").html("确认密码长度只能在6-16位字符之间");
                        $("#repwd_error").show();
                        $("#repwd_success").hide();
                        repwdstate = false;
                    } else if (!format) {
                        $("#repwd_error").html("确认密码只能由英文、数字及特殊符号组成");
                        $("#repwd_error").show();
                        $("#repwd_success").hide();
                        repwdstate = false;
                    } else if ($(newpwd).val() != $(renewpwd).val()) {
                        $("#repwd_error").html("新密码和确认密码不一致");
                        $("#repwd_error").show();
                        $("#repwd_success").hide();
                        repwdstate = false;
                    } else {
                        $("#repwd_error").hide();
                        $("#repwd_success").show();
                        repwdstate = true;
                    }
                }
            }
            else {
                $("#telpwd_error").html("输入得验证码不正确");
                $("#telpwd_error").show();
                $("#telpwd_success").hide();
            }
        });
    });

});


//验证手机号
function checkUserName(userName) {
    var format = new RegExp("^0?(1)[0-9]{10}$").test($(userName).val());
    if ($(userName).val() == "") {
        $("#mobile_error").attr("class", "error");
        $("#mobile_error").html("<em class=\"errorem\"></em> 请输入用户名");
        $("#mobile_succeed").hide();
        $("#mobile_error").show();
        userNamestate = false;
    } else if (!format) {
        $("#mobile_error").attr("class", "error");
        $("#mobile_error").html("<em class=\"errorem\"></em>  手机号码格式不正确，请重新输入！");
        $("#mobile_succeed").hide();
        $("#mobile_error").show();
        userNamestate = false;
    } else {
        $.getJSON("/Account/CheckUserName/?userName=" + $(userName).val(), function (data) {
            if (data.success == 1) {
                $("#mobile_succeed").attr("class", "succeed");
                $("#mobile_succeed").show();
                $("#mobile_error").hide();
                userNamestate = true;
            }
            else if (data.success == 0) {
                $("#mobile_error").attr("class", "error");
                $("#mobile_error").html('<em class=\"errorem\"></em>  您输入的用户名不存在，请重新输入');
                $("#mobile_succeed").hide();
                $("#mobile_error").show();
                userNamestate = false;
            }
            else {
                $("#mobile_error").attr("class", "error");
                $("#mobile_error").html('<em class=\"errorem\"></em>  网络繁忙');
                $("#mobile_succeed").hide();
                $("#mobile_error").show();
                userNamestate = false;
            }
        });
    }
}


//验证手机号
function checkAuthCode(authCode) {
    if ($(authCode).val() == "") {
        $("#telpwd_error").attr("class", "error");
        $("#telpwd_error").html("<em></em>" + "请输入验证码");
        $("#telpwd_error").show();
        $("#telpwd_success").hide();
        $(authCode).focus();
        authCodestate = false;
    } else {
        $("#telpwd_success").attr("class", "succeed");
        $("#telpwd_error").hide();
        $("#telpwd_success").show();
        authCodestate = true;
    }
}

//导航搜索下拉js
$(function () {
    $(".select").each(function () {
        var s = $(this);
        var z = parseInt(s.css("z-index"));
        var dt = $(this).children("dt");
        var dd = $(this).children("dd");
        var _show = function () {
            dd.slideDown(200);
            dt.addClass("cur");
            s.css("z-index", z + 1);
        }; //展开效果
        var _hide = function () {
            dd.slideUp(200);
            dt.removeClass("cur");
            s.css("z-index", z);
        }; //关闭效果
        dt.click(function () {
            dd.is(":hidden") ? _show() : _hide();
        });
        dd.find("a").click(function () {
            dt.html($(this).html());
            _hide();
        }); //选择效果（如需要传值，可自定义参数，在此处返回对应的“value”值 ）
        $("body").click(function (i) {
            !$(i.target).parents(".select").first().is(s) ? _hide() : "";
        });
    })
});
//导航搜索下拉js结束

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
            $("#yantext").attr("readonly", true);
            obj.attr('class', "valida_code");
            obj.val('免费获取验证码');
            clearInterval(i);
        } else {
            obj.val('剩余' + parseInt(sum - count) + '秒');
            obj.attr('disabled', true);
        }
        count++;
    }, 1000);
}