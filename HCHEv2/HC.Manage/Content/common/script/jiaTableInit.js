/*
插件： 表格数据绑定更新
作者： FuJia
日期:  2015-12-22
*/
(function ($) {
    $.fn.extend({
        "jiaTableBind": function (bindOption,action) {
            finallyOption = $.extend({
                data: {},
                trTemplate: "",
                noTrTemplate:"",
                getArray: "",
                initTr: "",
                updataTr: "",
                ele: jQuery(this)
            }, bindOption);

            finallyOption.getArray = function (ar, ind) {
                return ar[ind];
            };
            finallyOption.trTemplate = finallyOption.ele.find("tr:eq(0)").prop("outerHTML");
            finallyOption.noTrTemplate = finallyOption.ele.find("tr:eq(0)").html();
            finallyOption.initTr = function (data, trTemp) {
                var strs = "";
                var pars = trTemp.match(/\{([^\}]+)\}/ig);
                var noinclude = [];
                $.each(pars, function (j, v) {
                    noinclude.push(v.replace('{', '').replace('}', ''));
                })
                
                for (var i in data.tr) {
                    if (!isNaN(i)) {// i包含了值indexof 不解  jia 12-25
                        for (var o in pars) {
                            if (!isNaN(o)) {
                                curenProName = (function (x) { return noinclude[x]; })(o);
                                if (o == 5) {
                                    var imgsHtml = "";
                                    for (var im in data.tr[i][curenProName]) {
                                        if (!isNaN(im)) {
                                            imgsHtml += "<li><img src=" + data.tr[i][curenProName][im].tar + "></li>";
                                        }
                                    }
                                    trTemp = trTemp.replace(pars[o], imgsHtml);
                                } else {
                                    trTemp = trTemp.replace(pars[o], data.tr[i][curenProName]);
                                }
                            }
                        }
                        strs += trTemp;
                    }
                }
                return strs;
            }
            finallyOption.updataTr = function (trindex, data) {
               var updataStrs= finallyOption.initTr(data.data, finallyOption.noTrTemplate);
               finallyOption.ele.find("tr:eq(" + trindex + ")").html(updataStrs);
            }
            var finalyHtml = finallyOption.initTr(finallyOption.data, finallyOption.trTemplate);
            action == undefined ? finallyOption.ele.append(finalyHtml) : "";
            return finallyOption;
        }
    });
})(jQuery)


