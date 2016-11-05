$(function () {
    
    //回复框
    $('.hc_closdiv').click(function () {
        $("#hc_replydiv").hide();
    });
    //$("input[name='revertbut']").mousedown(function () {
    //    var X = $(this).offset().top;
    //    var Y = $(this).offset().left;
    //    $("#hc_replydiv").css({ "top": (X + 20), "left": (Y - 720) });
    //    $("#hc_replydiv").show();
    //});




    //回复框
    $(document).on("click", "a[name='revertbut']", function () {
        var X = $(this).offset().top;
        var Y = $(this).offset().left;
        var reConetent = $(this).parent().prev().text();
        var mst = $(this).attr("mst");
        $("#hc_replydiv").css({ "top": (X + 1), "left": (Y - 830) });
        $("#hc_replydiv").find(".hc_textarebut").attr("mst", mst);
        $("#hc_textreply").val(reConetent);//清空回复框内容
        $("#hc_replydiv").show();
    })
    //删除
    $(document).on("click", ".hc_delevalue", function () {
        var mst = $(this).attr("mst");
        $.ajax({
            url: "/manage/evaluate/delMessage",
            data: { evaK: mst},
            type: 'GET',
            dataType: 'json',
            error: function (xhr, status) {
                alert(status);
            },
            success: function (Data) {
                if (Data == "success") {
                    var index = $(".tab_menus").find("li[class=selects]").index();
                    if (index == 0) {
                        var grid0 = $("#AllEvaGrid").data('tGrid');
                        grid0.ajaxRequest();
                    } else {
                        var grid1 = $("#beenEvaGrid").data('tGrid');
                        grid1.ajaxRequest();
                    }
                } else {
                    alert("请稍后再试");
                }
            }
        });
    })


    $(document).on("click", ".hc_textarebut", function () {
        var reConten = $("#hc_textreply").val();
        if (reConten.length > 0) {
            var mst = $(this).attr("mst");
            $.ajax({
                url: "/manage/LeaveMessage/reMessage",
                data: { evaK: mst, content: reConten },
                type: 'GET',
                dataType: 'json',
                error: function (xhr, status) {
                    alert(status);
                },
                success: function (Data) {
                    if (Data == "success") {
                        $("#hc_replydiv").hide();
                        var grid0 = $("#onsalesM").data('tGrid');
                        grid0.ajaxRequest();
                    } else {
                        alert("请稍后再试");
                    }
                }
            });
        } else {
            alert("请填写回复内容");
        }
    })
});

//字数限制
//document.getElementById("hc_textreply").onfocus = document.getElementById("hc_textreply").onkeydown = document.getElementById("hc_textreply").onkeyup = function () {
//    textCounter(this, 'hc_textNmb', 500);
//}


var textarea = $("#hc_textreply");
$(textarea).change(function () {
    textCounter(this, 'hc_textNmb', 500);
})

var textCounter = function (field, counter, maxlimit) {
    var charcnt = field.value.length;
    if (charcnt > maxlimit) {
        field.value = field.value.substring(0, maxlimit);
    } else {
        document.getElementById(counter).innerHTML = charcnt;
    }
}
