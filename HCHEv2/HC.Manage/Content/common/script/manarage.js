//企业经营类目

$(function () {
    //图标居底部
    $(".apply_text").each(function () {
        var $apply = $(this).height();
        var $right_show = $(this).parent().find(".beauty_text").height();
        $(this).css({ "margin-top": ($right_show - 20) });
    });

    //展开收起
    $(".change_next").click(function () {

            if ($(this).text() == "展开") {
                $(this).parent().parent().parent().parent().find(".hide_next ").hide();
                $(this).parent().parent().parent().find(".hide_next ").show();
                $(this).parent().parent().parent().parent().find(".open_close").html("展开");
                $(this).find(".open_close").html("收起");
                $(this).parent().parent().parent().parent().find("em").removeClass("top_next").addClass("bto_next");
                $(this).find("em").removeClass("bto_next").addClass("top_next");
                //图标居底部
                $(".apply_text").each(function () {
                    var $apply = $(this).height();
                    var $right_show = $(this).parent().find(".beauty_text").height();
                    $(this).css({ "margin-top": ($right_show - 20) });
                });

                // $(this).parent().parent().parent().parent().find(".hide_next ").hide();

            } else if ($(this).text() == "收起") {
         
                $(this).parent().parent().parent().find(".hide_next ").hide();
         
                $(this).find(".open_close").html("展开");

                $(this).find("em").removeClass("top_next").addClass("bto_next");
               
                //图标居底部
                $(".apply_text").each(function () {
                    var $apply = $(this).height();
                    var $right_show = $(this).parent().find(".beauty_text").height();
                    $(this).css({ "margin-top": ($right_show - 20) });
                });
            }

    });
    

   
});
/*全选*/

//全选/取消全选
$(function () {
    $(".beauty_car").each(function () {
        var $firstlabel = $(this).find("label");
        var $firstcheckbox = $(this).find("input[type='checkbox']");
        $firstlabel.click(function () {
            var $hide_next = $(this).parent().parent().parent().find(".hide_next");
            if ($firstcheckbox.attr("checked") == undefined) {
                $firstcheckbox.attr("checked", true);
                $hide_next.find("input[name='abc']").attr("checked", true);
                $hide_next.find("input[name='abc']").attr("class", "checkbox");
                $hide_next.find(".lable_last").attr("class", "label_new");
                $(this).attr("class", "lable_show label_new");
            }
            else {
                $firstcheckbox.attr("checked", false);
                $hide_next.find("input[name='abc']").attr("checked", false);
                $hide_next.find("input[name='abc']").attr("class", "checkbox");
                $hide_next.find(".label_new").attr("class", "lable_last");
                $(this).attr("class", "lable_show");
            }
        });

        $(this).parent().find(".mouth_next").each(function () {
            var $secondlabel = $(this).find(".left_show").find("label");
            var $secondcheckbox = $(this).find(".left_show").find("input[type='checkbox']");
            $secondlabel.click(function () {
                var $right_show = $(this).parent().parent().find(".right_show");
                if ($secondcheckbox.attr("checked") == undefined) {
                    $secondcheckbox.attr("checked", true);
                    $right_show.find("input[name='abc']").attr("checked", true);
                    $right_show.find("input[name='abc']").attr("class", "checkbox");
                    $right_show.find(".lable_last").attr("class", "label_new");
                    $(this).attr("class", "label_new");
                }
                else {
                    $secondcheckbox.attr("checked", false);
                    $right_show.find("input[name='abc']").attr("checked", false);
                    $right_show.find("input[name='abc']").attr("class", "checkbox");
                    $right_show.find(".label_new").attr("class", "lable_last");
                    $(this).attr("class", "lable_last");
                }
            });

            $(this).find(".right_next_text").each(function () {
                var $thirdlabel = $(this).find("label");
                var $thirdcheckbox = $(this).find("input[type='checkbox']");
                $thirdlabel.click(function () {
                    if ($thirdcheckbox.attr("checked") == undefined) {
                        $thirdcheckbox.attr("checked", true);
                        $thirdcheckbox.attr("class", "checkbox");
                        $thirdlabel.attr("class", "label_new");
                    }
                    else {
                        $thirdcheckbox.attr("checked", false);
                        $thirdcheckbox.attr("class", "checkbox");
                        $thirdlabel.attr("class", "lable_last"); 
                    }
                });
            });
            
        });
    });
});