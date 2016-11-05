var valiateResult = true;
function validateInput(e) {
    var vaType = e.attr("vali-type");
    switch (vaType) {
        case "require":
            reQuireVa(e);
            break;
        case "num":
            vaNum(e);
            break;
        case "phone":
            vaPhone(e);
            break;
    }
}

function reQuireVa(e) {
    if (e.val().replace(/(^\s+)|(\s+$)/g, "").replace(/\s/g, "").length == 0) {
        valiateResult = false;
        e.next().show();
    }
}

function vaPhone(e) {
    if (e.val().replace(/(^\s+)|(\s+$)/g, "").replace(/\s/g, "").length > 0) {
        var myreg = /^(((13[0-9]{1})|(15[0-9]{1})|(18[0-9]{1}))+\d{8})$/;
        if (!myreg.test(e.val())) {
            valiateResult = false;
            e.next().show();
        }
    }
}

function vaNum(e) {
    if (e.val().replace(/(^\s+)|(\s+$)/g, "").replace(/\s/g, "").length > 0) {
        var reg = new RegExp("^[0-9]*$");
        if (!reg.test(e.val())) {
            valiateResult = false;
            e.next().show();
        }
    }
}


//function vaGroup(gs) {
//    var leng = gs.length;
//    var j = 0;
//    for (var i = 0; i <= leng - 1; i++) {
//        if ($(gs[i]).val().replace(/(^\s+)|(\s+$)/g, "").replace(/\s/g, "").length == 0) {
//            j++;
//        }
//    }

//    if (leng == j) {
//        valiateResult = false;
//        $(".errorsp").show();
//    }
//}


$(document).ready(function () {

    






    $("#submitMessage").click(function () {
        var _this = $(this);
        //if (_this.hasClass("leaveMessageIng"))//拒绝提交中的二次点击
        //    return false;

        reQuireVa($("#leaveName"));
        reQuireVa($("#leaveEmail"));
        reQuireVa($("#leavePhone"));
        vaPhone($("#leavePhone"));
        reQuireVa($("#leaveContent"));
       
        //验证结束
        if (valiateResult) {
            $("from").submit();
        } else {
            return false;
        }

      


    })


    $("form input,form textarea").not("input[vali-group]").click(function () {
        $(this).next().hide();
        valiateResult = true;
    })

    //$("form input[vali-group]").click(function () {
    //    $(".errorsp").hide();
    //    $(this).next().hide();
    //    valiateResult = true;
    //})

})

