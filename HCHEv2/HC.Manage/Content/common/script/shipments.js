
//验证
var userNamestate = false;
var Numberstate = false;
var nichengpstate = false;
var nickteelphont = false;
function checkUserName(sticssinput) {
    if ($("#sticssinput").val() == "") {
        $("#labeltwo").html("<em class=\"errorem\"></em>请输入货运公司名称");
        $("#labelone").hide();
        $("#labeltwo").show();
        userNamestate = false;
    } else if ($("#sticssinput").val().length < 2) {
        $("#labeltwo").html("<em class=\"errorem\"></em>货运公司名称最少两个字符");
        $("#labelone").hide();
        $("#labeltwo").show();
        userNamestate = false;
    } else {
        $("#labelone").show();
        $("#labeltwo").hide();
        userNamestate = true;
    }

}
function checkNumber(sticnumber) {
    var format = new RegExp("[0-9a-zA-Z]{3,20}$").test($(sticnumber).val());
    if ($("#sticnumber").val() == "") {
        $("#labelfour").html("<em class=\"errorem\"></em>请输入货运单号");
        $("#labelthere").hide();
        $("#labelfour").show();
        Numberstate = false;
    } else if ($("#sticnumber").val().length < 5) {
        $("#labelfour").html("<em class=\"errorem\"></em>货运单号最少五位");
        $("#labelthere").hide();
        $("#labelfour").show();
        Numberstate = false;
    } else if (!format) {
        $("#labelfour").html("<em class=\"errorem\"></em>货运单号由数字、字母组成");
        $("#labelfour").show();
        $("#labelthere").hide();
        Numberstate = false;
    } else {
        $("#labelthere").show();
        $("#labelfour").hide();
        Numberstate = true;
    }

}
function nicknamelik(sticName) {
    var format = new RegExp("^[A-Za-z0-9_()（）\\-\\u4e00-\\u9fa5]+$").test($(sticName).val());
    if ($(sticName).val() == "") {
        $("#labelnametwo").html("<em class=\"errorem\"></em>请输入送货人姓名");
        $("#labelnameone").hide();
        $("#labelnametwo").show();
        nichengpstate = false;
    } else if ($(sticName).val().length < 2 || $(sticName).val().length > 8) {
        $("#labelnametwo").html("<em class=\"errorem\"></em> 姓名长度只能在2-8位字符之间");
        $("#labelnametwo").show();
        $("#labelnameone").hide();
        nichengpstate = false;
    } else if (!format) {
        $("#labelnametwo").html("<em class=\"errorem\"></em>姓名只能由中文、英文组成！");
        $("#labelnametwo").show();
        $("#labelnameone").hide();
        nichengpstate = false;
    } else {
        $("#labelnameone").show();
        $("#labelnametwo").hide();
        nichengpstate = true;
    }
}
function nickteelphonlik(teelphoneinpt) {
    var format = new RegExp("^[0-9]{7,11}$").test($(teelphoneinpt).val());
    if ($(teelphoneinpt).val() == "") {
        $("#labelteltwo").html("<em class=\"errorem\"></em>请输入电话号码");
        $("#labeltelone").hide();
        $("#labelteltwo").show();
        nickteelphont = false;
    } else if (!format) {
        $("#labelteltwo").html("<em class=\"errorem\"></em>电话号码格式不正确，请重新输入！");
        $("#labelteltwo").show();
        $("#labeltelone").hide();
        nickteelphont = false;
    } else {
        $("#labeltelone").show();
        $("#labelteltwo").hide();
        nickteelphont = true;
    }
}
$(function () {
    $("#sticssinput").val("");
    $("#sticssinput").change(function () { checkUserName($("#sticssinput")); });
    $("#sticnumber").val("");
    $("#sticnumber").change(function () { checkNumber($("#sticnumber")); });
    $("#sticName").val("");
    $("#sticName").change(function () { nicknamelik($("#sticName")); });
    $("#teelphoneinpt").val("");
    $("#teelphoneinpt").change(function () { nickteelphonlik($("#teelphoneinpt")); });
    $(".xq_susbut .fabuton").click(function () {
        checkUserName($("#sticssinput"));
        checkNumber($("#sticnumber"));
        nicknamelik($("#sticName"));
        nickteelphonlik($("#teelphoneinpt"));
        if ($("#redioman").attr("class").indexOf("rabuit") > -1 || $("#radiowoman").attr("class").indexOf("rabuit") > -1) {
            if (userNamestate && Numberstate) {

            } else {
                return false;
            }
        } else {
            if (nichengpstate && nickteelphont) {
                
            } else {
                return false;
            }
        }
    });

    $("#redioman").click(function () {
        $("#redioman").addClass("rabuit");
        $("#radiowoman").removeClass("rabuit");
        $("#secrecy").removeClass("rabuit");
        $("#logistics").show();
        $("#deliveryman").hide();
        $("#sticssinput").val("");
        $("#teelphoneinpt").val("");
        $("#sticnumber").val("");
        $("#sticName").val("");
        $("#labeltwo").hide();
        $("#labelone").hide();
        $("#labelfour").hide();
        $("#labelthere").hide();
    });
    $("#radiowoman").click(function () {
        $("#radiowoman").addClass("rabuit");
        $("#redioman").removeClass("rabuit");
        $("#secrecy").removeClass("rabuit");
        $("#logistics").show();
        $("#deliveryman").hide();
        $("#sticssinput").val("");
        $("#teelphoneinpt").val("");
        $("#sticnumber").val("");
        $("#sticName").val("");
        $("#labeltwo").hide();
        $("#labelone").hide();
        $("#labelfour").hide();
        $("#labelthere").hide();
    });
    $("#secrecy").click(function () {
        $("#secrecy").addClass("rabuit");
        $("#redioman").removeClass("rabuit");
        $("#radiowoman").removeClass("rabuit");
        $("#deliveryman").show();
        $("#logistics").hide();
        $("#sticssinput").val("");
        $("#teelphoneinpt").val("");
        $("#sticnumber").val("");
        $("#sticName").val("");
        $("#labelnametwo").hide();
        $("#labelnameone").hide();
        $("#labeltelone").hide();
        $("#labelteltwo").hide();
    });

});

$(document).ready(function () {
    $(".pay_list_c1").click(function () {
        var seleV = $(this).attr("checkvalue");
        $("#deliveryType").val(seleV);
    })
})