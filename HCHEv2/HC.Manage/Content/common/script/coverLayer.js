(function ($) {
    $.fn.extend({
        "coverJia": function (options) {
            options = $.extend({
                backColor: "black", text: "", style: "height:100%;background:#000;position:fixed;width:100%;z-index:55555; opacity: 0.6;", ck: function () { $(".cover").hide(); $("#modifyCover2").hide(); location.reload(); }
            }, options);
            var coverDiv = "<div class='cover'></div>";          
            var contentDiv = "<div id=\"modifyCover2\" style=\"display: block;\"><p>" + options.text + "</p> <p><a id=\"confirmBut\" href=\"###\">确定</a></p></div>";
            $(this).prepend(contentDiv);
            $(this).prepend(coverDiv);

            $(".cover").attr("style", options.style);
                $("#confirmBut").click(options.ck);
        }
    });
})(jQuery);

//谁帮我养车消费验证
(function ($) {
    $.fn.extend({
        "innerCoverJia": function (options) {
            options = $.extend({
                backColor: "black", text: "", style: "height:85%;background:#000;position:absolute;width:97%;z-index:55555; opacity: 0.6;", ck: function () {
                    var innerCover = $(this).parent().parent();
                    var outerCover = $(innerCover).prev();
                    $(innerCover).remove(); $(outerCover).remove();
                }
            }, options);

            var coverDiv = "<div class='coverInner'></div> <div class=\"modifyCoverInner\" style=\"display: block; position:absolute; z-index: 55556;background-color: white;top: 29%;left: 29%;\"><p style=\"margin-top: 15px;\">" + options.text + "</p> <p><a class=\"confirmBut\" href=\"###\" style=\"text-align: center;\">确定</a></p></div>";

            var coverInDiv
            $(this).prepend(coverDiv);
            $(".coverInner").attr("style", options.style);
            $(".confirmBut").click(options.ck);
        }
    });
})(jQuery);
