/*
插件：table数据绑定和主控
日期：2015-12-26
作者：FuJia
*/
(function ($) {
    $.fn.extend({
        "jiaTableSyn": function (bindOption) {
            finallyOption = $.extend({
                data: {},
                trTemplate: "",
                getArray: "",
                initTr: "",
                ele: jQuery(this),
                ontrDataBinded: "",
                initPager: "",//分页用
                totalRcord: "",//分页用
                pSize: "",//分页用
                pagerEle: "",//分页用
                afterPagerChange: "",//分页用
                beenPager: ""
            }, bindOption);

            finallyOption.getArray = function (ar, ind) {
                return ar[ind];
            };
            finallyOption.trTemplate = finallyOption.ele.find("tr:eq(0)").prop("outerHTML");
            finallyOption.initPager = function () {
                if (Math.ceil(finallyOption.totalRcord / finallyOption.pSize) > 1&&!finallyOption.beenPager) {
                    $("#" + finallyOption.pagerEle).jiaPager({ afterChange: finallyOption.afterPagerChange, pageSize: finallyOption.pSize, totalCount: finallyOption.totalRcord });
                }
            }
            finallyOption.initTr = function (data, trTemp, afterTrBinded) {
                var pars = trTemp.match(/\{([^\}]+)\}/ig);
                var noinclude = [];
                $.each(pars, function (j, v) {
                    noinclude.push(v.replace('{', '').replace('}', ''));
                })
                for (var i in data.tr) {
                    var trTempIndependent = trTemp;
                    //if (!isNaN(i)) {// i包含了值indexof 不解  jia 12-25  //如果被遍历的对象是数组的话 则i不会出现除了 小标数字的其他信息，如果被遍历的是一个对象则会出现除了数字下标以外的信息   这里是数组所以 这句可以屏蔽   外面传值的方式是 var tableModel={tr:[]}; 
                    for (var o in pars) {
                        if (!isNaN(o)) {
                            curenProName = (function (x) { return noinclude[x]; })(o);
                            if (o == 4) {
                                var imgsHtml = "";
                                for (var im in data.tr[i][curenProName]) {
                                    if (!isNaN(im)) {
                                        imgsHtml += "<li><img src=" + data.tr[i][curenProName][im].tar + "></li>";
                                    }
                                }
                                trTempIndependent = trTempIndependent.replace(pars[o], imgsHtml);
                            } else {
                                trTempIndependent = trTempIndependent.replace(pars[o], data.tr[i][curenProName]);
                            }
                        }
                    }
                    finallyOption.ele.append(trTempIndependent);
                    afterTrBinded({ trData: data.tr[i], trIndex: i * 1 + 1 * 1, jqObjOftr: $(finallyOption.ele.children()[i * 1 + 1 * 1]) });
                    //}
                }
                finallyOption.initPager();//初始分页
            }
            finallyOption.initTr(finallyOption.data, finallyOption.trTemplate, finallyOption.ontrDataBinded);
        }
    });
})(jQuery);


/*
插件：table分页
日期：2015-12-30
作者：FuJia
*/
(function ($) {
    $.fn.extend({
        "jiaPager": function (pagerOption) {
            finallyPagerSet = $.extend({
                ele:jQuery(this),
                pageSize: "",
                totalCount: "",
                initPager: "",
                preC: "",
                nextC: "",
                changPage: "",
                afterChange:"",
                curentPage:1
            }, pagerOption);

            var preOrNext = function (e, action) {
                var totalPage = Math.ceil(finallyPagerSet.totalCount / finallyPagerSet.pageSize);
                if (action == "P") {
                    finallyPagerSet.changPage($(e.parent().next().children()[finallyPagerSet.curentPage-2]), finallyPagerSet.afterChange);
                } else if (action == "N") {
                    finallyPagerSet.changPage($(e.parent().prev().children()[finallyPagerSet.curentPage]), finallyPagerSet.afterChange);
                } else {
                    var pr = $(finallyPagerSet.ele.find(".one_page").children()[0]);
                    var ne = $(finallyPagerSet.ele.find(".one_page").children()[1]);
                    if(finallyPagerSet.curentPage == 1){
                        pr.removeClass("activePreOrNext").addClass("destroyPreOrNext");
                        ne.removeClass("destroyPreOrNext").addClass("activePreOrNext");
                    }
                   else if(finallyPagerSet.curentPage == totalPage){
                       ne.removeClass("activePreOrNext").addClass("destroyPreOrNext");
                       pr.removeClass("destroyPreOrNext").addClass("activePreOrNext");
                   } else {
                       pr.removeClass("destroyPreOrNext").addClass("activePreOrNext");
                       ne.removeClass("destroyPreOrNext").addClass("activePreOrNext");
                   }
                }
            }
            finallyPagerSet.initPager = function (tCount, psize) {
                var totalPage = Math.ceil(tCount / psize);
                var origText = finallyPagerSet.ele.find(".totle_page").text();
                finallyPagerSet.ele.find(".totle_page").text(origText.replace(origText.match(/[1-9][0-9]*/g), totalPage));
                var pageStr = "";
                for (var p = 1; p <= totalPage; p++) {
                    if (p == 1) {
                        pageStr += "<a class=\"beenSelect\" href=\"javascript:void(0);\">" + p + "</a>";
                    //} else if(p<=5) {
                       // pageStr += "<a href=\"javascript:void(0);\">" + p + "</a>";
                    } else {
                        pageStr += "<a href=\"javascript:void(0);\">" + p + "</a>";
                    }
                }
                //if (totalPage > 5) {
                //    pageStr += "<a href=\"javascript:void(0);\">...</a>";
                //}
                $(".page_contain").html(pageStr);
                finallyPagerSet.ele.show();
            }
            finallyPagerSet.changPage = function (jqOfchangeTar, changed) {
                if (finallyPagerSet.curentPage == jqOfchangeTar.text().trim()) {
                    return false;
                } else {
                jqOfchangeTar.addClass("beenSelect").siblings().removeClass("beenSelect");
                finallyPagerSet.curentPage = jqOfchangeTar.text().trim();
                changed(jqOfchangeTar.text());//回调
                preOrNext("", "C");
                }
            }
            finallyPagerSet.initPager(finallyPagerSet.totalCount, finallyPagerSet.pageSize);
            finallyPagerSet.preC = function (e) {
                if (e.hasClass("destroyPreOrNext")) {
                    return false;
                } else {
                    preOrNext(e, "P");
                }
            }
            finallyPagerSet.nextC = function (e) {
                if (e.hasClass("destroyPreOrNext")) {
                    return false;
                } else {
                    preOrNext(e, "N");
                }
            }
            $(".page_contain").find("a").click(function () { finallyPagerSet.changPage($(this), finallyPagerSet.afterChange) });
            $(finallyPagerSet.ele.find(".one_page").children()[0]).click(function () { finallyPagerSet.preC($(this)) });
            $(finallyPagerSet.ele.find(".one_page").children()[1]).click(function () { finallyPagerSet.nextC($(this)) });
        }
    })
})(jQuery)