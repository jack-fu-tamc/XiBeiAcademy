/*
插件： 字母导航下拉菜单
作者： FuJia
日期:  2015-12-11
*/
(function ($) {
    $.fn.extend({
        "dropDonwNav": function (ddNavOptions, nameSpace) {

            nameSpace.O = $.extend({
                closeMethod: "",
                openMethod: "",
                choseMethod: "",
                scrollMethod: "",
                letterNavMethod: "",
                id: jQuery(this).attr('id'),
                ele: jQuery(this),
                letterObj: [],
                selectObj: [],
                letterItem: "",
                selectItem: "",
                letterNav: "",
                moveLay: "",
                movePar: "",
                dropDownSpan: "",
                letterP: "",
                scrollPoint: [],
                valueChange: "",
                selectItemLength: ""
            }, ddNavOptions);


            
            if (!Array.indexOf) {
                Array.prototype.indexOf = function (obj) {
                    for (var i = 0; i < this.length; i++) {
                        if (this[i] == obj) {
                            return i;
                        }
                    }
                    return -1;
                }
            }


            nameSpace.O.init = function () {
                for (var i in nameSpace.O.letterObj) {
                    if (!isNaN(i)) {
                        nameSpace.O.letterItem += "<p>" + nameSpace.O.letterObj[i] + "</p>";
                    }
                }
                for (var j in nameSpace.O.selectObj) {
                    if (!isNaN(j)) {
                        if (j == 0) {
                            if (nameSpace.O.selectObj[j].Text.indexOf("==") == -1)
                                if (nameSpace.O.selectObj[j].firstLetter != "none")
                                    nameSpace.O.selectItem += "<li class=\"t separationLine\"> " + nameSpace.O.selectObj[j].firstLetter + " </li>";
                        } else {
                            if (nameSpace.O.selectObj[j].firstLetter != nameSpace.O.selectObj[j - 1].firstLetter && isNaN(nameSpace.O.selectObj[j].firstLetter)) {
                                nameSpace.O.selectItem += "<li class=\"t separationLine\"> " + nameSpace.O.selectObj[j].firstLetter + " </li>";
                            }
                        }
                        nameSpace.O.selectItem += "<li class=\"t\" k=\"" + nameSpace.O.selectObj[j].Value + "\"> " + nameSpace.O.selectObj[j].Text + "  </li>";
                    }
                }
                var wholeContainer = "<div id=\"movePar_" + nameSpace.O.id + "\" class=\"movePar\" style=\"display: none; \"> <div id=\"moveLay_" + nameSpace.O.id + "\" style=\"margin-top: -310px;\" class=\"moveLay\"><div class=\"letterNav_" + nameSpace.O.id + " letterNav\"> " + nameSpace.O.letterItem + " </div> <ul class=\"t-resetssss\">" + nameSpace.O.selectItem + " </ul></div></div>";
                nameSpace.O.ele.after(wholeContainer);

               
                nameSpace.O.selectItemLength = $("#moveLay_" + nameSpace.O.id + " .t-resetssss").children().length;
                if (nameSpace.O.selectItemLength * 20 < 309) {
                    $("#moveLay_" + nameSpace.O.id).css("height", nameSpace.O.selectItemLength * 20);
                }
            }();
            
            nameSpace.O.letterNav = $(".letterNav_" + nameSpace.O.id);
            nameSpace.O.moveLay = $("#moveLay_" + nameSpace.O.id);
            nameSpace.O.movePar = $("#movePar_" + nameSpace.O.id);
            nameSpace.O.dropDownSpan = nameSpace.O.ele.find("span");
            nameSpace.O.letterP = nameSpace.O.letterNav.find("p");
            separationLine = $(".separationLine");

            
            nameSpace.O.openMethod = function (event) {
                nameSpace.O.movePar.css({ "overflow": "hidden", "display": "block" });
                nameSpace.O.moveLay.animate(
                    { marginTop: '0' },
                    200,
                    "linear",
                    function () { nameSpace.O.movePar.css("overflow", "") });
                //如果是IE7 同步 字母导航
                if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE7.0") {
                    nameSpace.O.letterNav.animate({ marginTop: '0px' },
                     200,
                    "linear");
                }
                return false;
            };
            nameSpace.O.closeMethod = function (event) {
                nameSpace.O.movePar.css("overflow", "hidden");
                nameSpace.O.moveLay.animate({ marginTop: '-310px' },
                     200,
                    "linear",
                    function () { nameSpace.O.movePar.css({ "overflow": "", "display": "none" }) });
                
                if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE7.0") {
                    nameSpace.O.letterNav.animate({ marginTop: '-310px' },
                         200,
                        "linear");
                }
            };
            nameSpace.O.scrollMethod = function (event) {
                if (nameSpace.O.scrollPoint.length == 0) {
                    $("#moveLay_" + nameSpace.O.id + " .separationLine").each(function (i, v) {
                        var absoultPosUo = $("#moveLay_" + nameSpace.O.id + " .t-resetssss").offset().top;
                        var absoultPosLo = $(v).offset().top;
                        nameSpace.O.scrollPoint.push(Math.abs(absoultPosLo - absoultPosUo));
                    })
                }
                var moveLayScrollTop = nameSpace.O.moveLay.scrollTop();
                $.each(nameSpace.O.scrollPoint, function (i, v) {
                    if (moveLayScrollTop >= v && moveLayScrollTop <= nameSpace.O.scrollPoint[i + 1]) {
                        $("#moveLay_" + nameSpace.O.id + " .letterNav p:eq(" + i + ")").addClass("navMark").siblings().removeClass("navMark");
                    }
                })
            };
            nameSpace.O.choseMethod = function (ob, callBack) {
                if (ob.attr("class").indexOf("separationLine") == -1) {
                    nameSpace.O.ele.find("span:eq(0)").text(ob.text());
                    if (ob.attr("k")) {
                        var keyV = ob.attr("k").split(',');
                        callBack({ 'id': keyV[0], 'text': keyV[1] });
                    } else {
                        callBack({ 'id': '', 'text': '' });
                    }
                } else {

                }
            };
            nameSpace.O.letterNavMethod = function (ob) {
                var absoultPosU = $("#moveLay_" + nameSpace.O.id + " .t-resetssss").offset().top;
                var targetLetter = ob.text().trim();
                $("#moveLay_" + nameSpace.O.id + " .separationLine").each(function (i, v) {
                    if ($(v).text().trim() == targetLetter) {
                        var absoultPosL = $(v).offset().top;
                        document.getElementById('moveLay_' + nameSpace.O.id).scrollTop = Math.abs(absoultPosL - absoultPosU);
                        return false;
                    }
                })
            }
            $(document).click(function (event) {
                var e = event.srcElement ? event.srcElement : event.target;
                if ($(e).is(nameSpace.O.ele) || $(e).is(nameSpace.O.dropDownSpan) || $(e).is(separationLine) || $(e).is(nameSpace.O.letterP)) {
                } else {
                    nameSpace.O.closeMethod();
                }
            });
            nameSpace.O.ele.click(function () {
                if (nameSpace.O.moveLay.css("margin-top") != "-310px") {
                    nameSpace.O.closeMethod();
                } else {
                    nameSpace.O.openMethod();
                }
            });

            nameSpace.O.moveLay.find("li").click(function () { nameSpace.O.choseMethod($(this), nameSpace.O.valueChange); });
            nameSpace.O.moveLay.find("p").click(function () { nameSpace.O.letterNavMethod($(this)); });
            nameSpace.O.moveLay.on('scroll', nameSpace.O.scrollMethod);
        }
    });
})(jQuery);