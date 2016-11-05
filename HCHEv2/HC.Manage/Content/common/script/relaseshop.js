
//企业经营类目
if ((navigator.userAgent.indexOf('MSIE') >= 0) && (navigator.userAgent.indexOf('Opera') < 0)) {
    var browser = navigator.appName
    var b_version = navigator.appVersion
    var version = b_version.split(";");
    var trim_Version = version[1].replace(/[ ]/g, "");
}


$(function () {
    //图标居底部
    $(".apply_text").each(function () {
        var $apply = $(this).height();
        var $right_show = $(this).parent().find(".beauty_text").height();
        $(this).css({ "margin-top": ($right_show - 20) });
    });

    //展开收起
    $(".change_next").click(function () {

        if ($(this).find(".open_close").text() == "展开") {
            $(this).parent().parent().parent().parent().find(".hide_next").hide();
            $(this).parent().parent().parent().find(".hide_next").show();
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

        } else if ($(this).find(".open_close").text() == "收起") {

            $(this).parent().parent().parent().find(".hide_next").hide();

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
            var flag = false;
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
                    $(this).parent().parent().parent().find(".apply_text").show();
                }
                else {
                    $secondcheckbox.attr("checked", false);
                    $right_show.find("input[name='abc']").attr("checked", false);
                    $right_show.find("input[name='abc']").attr("class", "checkbox");
                    $right_show.find(".label_new").attr("class", "lable_last");
                    $(this).attr("class", "lable_last");
                    $(this).parent().parent().parent().find(".apply_text").hide();
                }
            });

            $(this).find(".right_next_text").each(function () {
                var $thirdlabel = $(this).find("label");
                var $thirdcheckbox = $(this).find("input[type='checkbox']");
                $thirdlabel.click(function () {
                    var flag = false;
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
                    var $right_show = $(this).parent().parent();
                    $right_show.find(".right_next_text").each(function () {
                        if ($(this).find("input[type='checkbox']").attr("checked") == "checked") {
                            flag = true;
                            return false;
                        }
                    });
                    if (flag) $(this).parent().parent().parent().parent().find(".apply_text").show();
                    else $(this).parent().parent().parent().parent().find(".apply_text").hide();
                });
            });
        });
    });


});
$(function () {
    $(".select_type").each(function () {
        var $firstlast = $(this).find("label");
        var $firstnext = $(this).find("input[type='checkbox']");

        $firstlast.click(function () {
            var $hide_year = $(this).find(".selected");
            if ($firstnext.attr("checked") == undefined) {
                $firstnext.attr("checked", true);
                $hide_year.find("input[name='abc']").attr("checked", true);
                //$hide_year.find("input[name='abc']").attr("class", "checkbox");
                //$hide_year.find(".agree_show ").attr("class", "lable_last");
                $(this).attr("class", "agree_show_next ");
            }
            else {
                $firstnext.attr("checked", false);
                $hide_year.find("input[name='abc']").attr("checked", false);
                ////$hide_year.find("input[name='abc']").attr("class", "checkbox");
                //$hide_year.find(".label_new").attr("class", "lable_last");
                $(this).attr("class", "agree_show ");
            }
        });
    });
});

//tab切换
$(function () {
    var $tab_li = $('#tab .select_type .color_next');
    $tab_li.hover(function () {
        $(this).addClass('type_show').siblings().removeClass('type_show')
        var index = $tab_li.index(this);
        $('div.moth_giv>.unit_year').eq(index).show().siblings().hide();
    });
})

//给动态加载的数据绑定jquery(搜索分类用)jia+
function bindOnevent() {
    $(".matching").show();
    $(".release_product").show();

    $(".matching").each(function () {

        $(this).find("p").mousemove(function () {
            $(this).parent().find("p").removeClass("add_pax");
            $(this).addClass("add_pax");
            $(this).find(".matching_slect").show();
        });

        $(this).find("p").mouseleave(function () {
            $(this).parent().find("p").removeClass("add_pax");
            $(this).removeClass("add_pax");
            if ($(this).find(".matching_slect").attr("value") == "be") {
                $(this).find(".matching_slect").show();
            } else {
                $(this).find(".matching_slect").hide();
            }
        });

        $(this).find("p").click(function () {
            $(this).parent().find("p").removeClass("add_pax");
            $(this).removeClass("add_pax");
            $(".matching_slect").hide();
            $(this).find(".matching_slect").show();
            $(".matching_slect").attr("value", "");
            $(this).find(".matching_slect").attr("value", "be");

            //根据搜索 然后去发布商品 给cateIDs赋值
            var cateID = $(this).find(".matching_type").attr("cat");
            var allPathStr = $(this).find(".matching_type").text().replace(/\s/g, "");
            var subIndex = allPathStr.lastIndexOf(">"); var cateName = allPathStr.substr(subIndex + 1);
            //组合cateid+catename
            var combinationStr = cateID + "-" + cateName;
            $("#selectedCate").val(combinationStr);
            //赋值结束
        });
    });
    $(".matching_left p").click(function () {
        $(this).parent().find("p").attr("class", "");
        $(this).attr("class", "add_pax_next");
    });



}


$(function () {
    $(".img_show_next").each(function (index) {
        if (index % 2 == 0) {

            $(this).attr("class", "img_show_next");
        }
        else {
            $(this).attr("class", "img_show_next_two");

        };

    });


});



/*类别选择*/
$(function () {
    $(".replace_tire_show").each(function () {
        var $nite_next = $(this);
        $(this).mouseover(function () {
            $(this).find(".show_year").show();
            $(this).find("em").attr("class", "mas_show_new");
        });
        $(this).find(".show_year").mouseout(function () {
            $(this).hide();
            $(this).find("em").attr("class", "mas_show");
          
        });
        $(this).find(".replace_tire").mouseout(function () {
            $nite_next.find(".show_year").hide();
            $nite_next.find("em").attr("class", "mas_show");
        });
    });

    $(".show_year").each(function () {
        var $show_year = $(".show_year")
        $(this).find("li").each(function () {
            $(this).click(function () {
                $show_year.hide();
                $(this).parent().parent().find("i").html($(this).text());
                $(this).parent().parent().find("em").attr("class", "mas_show");
                //给商品所属分类赋值
                $("#ServicerTypeID").val($(this).attr("cateID"));

            });
        });
        $(this).mouseleave(function () {
            $show_year.hide();
            $(this).parent().find("em").attr("class", "mas_show");
        });
    });
});


/*商家中心 服务类型验证和模拟radio*/
$(function () {
    $(".select_type_next").each(function () {
        var $first_li = $(this).find("label");
        $first_li.click(function () {
            var $first_ul = $(this).parent().find("input[type='radio']");
            //点击服务类型真radio按钮
            $first_ul.click();
            $(this).attr("class", "agree_arg_next");
            //点击某个模拟radio其他的模拟radio重置为 未选中
            $(this).parent().siblings().find("label").attr("class", "agree_arg");

        });
    });
    $("#goService").click(function () {
        $("#goServiceFee").show();
    })
    $("#comeService").click(function () {
        $("#goServiceFee").hide();
    })
});

//是否需要预约验证和模拟radio jia xg+
$(function () {
    $(".select_radio").each(function (index) {
        var $input_show_one = $(this).find(".input_show_one");
        $input_show_one.each(function () {
            var $radio_li = $(this).find("label");
            $radio_li.each(function (index) {
                $(this).click(function () {
                    var $radio = $(this).parent().find("input[type='radio']");
                    $radio.click();


                    if (index == 0) {
                        $(this).parent().parent().find("label").eq(0).attr("class", "agree_arg_next");
                        $(this).parent().parent().find("label").eq(1).attr("class", "agree_arg");
                    }
                    if (index == 1) {
                        $(this).parent().parent().find("label").eq(0).attr("class", "agree_arg");
                        $(this).parent().parent().find("label").eq(1).attr("class", "agree_arg_next");
                    }
                });
            });
        });
    });
});

//适配车型验证 jia+
$(function () {
    $(".select_radio_next").each(function (index) {
        var $input_show_one = $(this).find(".input_show_one");
        $input_show_one.each(function () {
            var $radio_li = $(this).find("label");
            $radio_li.each(function (index) {
                $(this).click(function () {

                    var $radio = $(this).parent().find("input[type='radio']");
                    $radio.click();

                    if (index == 0) {
                        $(this).parent().parent().find("label").eq(0).attr("class", "agree_arg_next");
                        $(this).parent().parent().find("label").eq(1).attr("class", "agree_arg");
                        //隐藏选择车型并更改该   商品匹配车型的值为0jia+
                        $(".ready_carSelect").hide();
                        $("#adapterCar").val(0); $(".append_adpapt").empty();
                    }
                    if (index == 1) {
                        $(this).parent().parent().find("label").eq(0).attr("class", "agree_arg");
                        $(this).parent().parent().find("label").eq(1).attr("class", "agree_arg_next");
                        //显示选择车型 jia+
                        $(".ready_carSelect").show();
                        $("#adapterCar").val("");
                    }
                });
            });
        });
    });

    //添加适配车型 jia+
    $("#addAdaptCar").click(function () {
        var dropdownListOfPinPai = $("#CarPinPaiSelect").data("tDropDownList");
        var dropdownListOfSeries = $('#CarSeriesSelect').data("tDropDownList");
        var dropdownListOfVersion = $('#CarVersionSelect').data("tDropDownList");

        //显示用户选择的车型车系车款  大众 > 斯柯达 > 所有车型
        var pinpaiV = dropdownListOfPinPai.value();
        var SeriesV = dropdownListOfSeries.value();
        var VersionV = dropdownListOfVersion.value();

        var pinpai = pinpaiV.split(',')[1];
        var Series = SeriesV == "-1" ? "" : SeriesV.split(',')[1];
        var Version = VersionV == "-1" ? "" : VersionV.split(',')[1];
        var adapterCarV = $("#adapterCar").val();

        //给adapterCar字段赋值
        if (dropdownListOfPinPai.value() != "") {
            if (dropdownListOfSeries.value() == "-1") {

                var addPValue = dropdownListOfPinPai.value().split(',')[0];
                if ($("#adapterCar").val().indexOf(addPValue) > -1) {
                    alert("你已添加了该车型");
                } else {
                    $("#adapterCar").val(adapterCarV + dropdownListOfPinPai.value().split(',')[0] + ",");
                    var pinpaiSV = dropdownListOfPinPai.value().split(',')[0] + ",";//用于删除选择的适配车型
                    var adaptAll = pinpai + " > 所有车系 > 所有车型";
                    $(".append_adpapt").append("<div class=\"append_adpapt_single\"><i class=\"ture_img\"></i><span class=\"car_tip\">" + adaptAll + "</span><i class=\"colse_img\" sel=\"" + pinpaiSV + "\"></i></div>");
                }
            }
            else if (dropdownListOfVersion.value() == "-1") {

                var addSvalue = dropdownListOfSeries.value().split(',')[0];
                if ($("#adapterCar").val().indexOf(addSvalue) > -1) {
                    alert("你已添加了该车型");
                } else {

                    $("#adapterCar").val(adapterCarV + dropdownListOfSeries.value().split(',')[0] + ",");
                    var seriesSV = dropdownListOfSeries.value().split(',')[0] + ",";//用于删除选择的适配车型
                    var adaptAll = pinpai + " > " + Series + " > " + "所有车型";
                    $(".append_adpapt").append("<div class=\"append_adpapt_single\"><i class=\"ture_img\"></i><span class=\"car_tip\">" + adaptAll + "</span><i class=\"colse_img\" sel=\"" + seriesSV + "\"></i></div>");
                }
            } else {

                var addVvalue = dropdownListOfVersion.value().split(',')[0];
                if ($("#adapterCar").val().indexOf(addVvalue) > -1) {
                    alert("你已添加了该车型");
                } else {
                    $("#adapterCar").val(adapterCarV + dropdownListOfVersion.value().split(',')[0] + ",")
                    var VersionSV = dropdownListOfVersion.value().split(',')[0] + ",";//用于删除选择的适配车型
                    var adaptAll = pinpai + " > " + Series + " > " + Version;
                    $(".append_adpapt").append("<div class=\"append_adpapt_single\"><i class=\"ture_img\"></i><span class=\"car_tip\">" + adaptAll + "</span><i class=\"colse_img\" sel=\"" + VersionSV + "\"></i></div>");
                }
            }
        } else {
            alert("请选择，再添加！");
            return false;
        }

    })



    //删除已添加的车型 jia+
    $(document).on("click", ".colse_img", function () {
        var seletV = $(this).attr("sel");
        var adapterCarVa = $("#adapterCar").val();
        adapterCarVa = adapterCarVa.replace(seletV, "");
        $("#adapterCar").val(adapterCarVa);
        $(this).parent().remove();
    })




});


$(function () {
    $(".hc_addimg").each(function (index) {
        if (index % 2 == 0) {
            $(this).attr("class", "hc_imgshow");
        }
        else {
            $(this).attr("class", "hc_imgshow_right")
        };

    });
    $("[name='tax']").hover(function () {
        $(this).parent().parent().find(".ser_axa").hide();//先隐藏其他的
        $(this).parent().find(".set_main").show();//再显示自己
    });



    $(".upload_img").hover(function () {
    }, function () {
        $(this).find(".hc_patImg a").hide();
    })
});



//添加用材用料主函数jia+
var proUserage = {
    validataFla: false,
    editAndDel: "<a href=\"javascript:void(0);\" class=\"cons_add\" onclick=\"testss(this)\" action=\"E\">编辑</a><a href=\"javascript:void(0);\" class=\"cons_add\" onclick=\"delTr(this)\">删除</a>",

    saveAndDel: "<a href=\"javascript:void(0);\" class=\"cons_add\" onclick=\"testss(this)\" action=\"S\">保存</a><a href=\"javascript:void(0);\" class=\"cons_add\" onclick=\"delTr(this)\">删除</a>",

    apendHtml: "<tr><td class=\"consumable_name\"><input type=\"text\" class=\"consumable_name_text\" placeholder=\"请输入\"id=\"consumable_name_input\" /><div class=\"try_sex_next\" id=\"consumable_name_error\" style=\"display:none\"><em class=\"tip_img\"></em><span class=\"tip_text\">请填写名称</span></div></td><td class=\"consumable_num\"><input type=\"text\" class=\"consumable_num_text\" id=\"consumable_num_input\" /><div class=\"try_sex_next\" id=\"consumable_num_error\"><em class=\"tip_img\"></em><span class=\"tip_text\">请填写数量</span></div></td><td class=\"consumable_unit\"><input type=\"text\" class=\"consumable_num_text\" id=\"consumable_pas_input\" /><div class=\"try_sex_next\" id=\"consumable_pas_error\"><em class=\"tip_img\"></em><span class=\"tip_text\">请填写单位</span></div></td><td class=\"reference_price\"><input type=\"text\" class=\"consumable_num_text\" id=\"refence_price\" /><div class=\"try_sex_next\" id=\"refence_price_error\"><em class=\"tip_img\"></em><span class=\"tip_text\">请填写参考价格</span></div></td><td><a href=\"javascript:void(0);\" class=\"cons_add\" onclick=\"testss(this)\">添加</a></td></tr>",

    add: function (ele, fnapend) {
        proUserage.validataFla = true;

        //错误提示显示或者隐藏开始
        ele.each(function (i, e) {
            if ($(e).find("input").val().length == 0) {
                proUserage.validataFla = false;
                $(e).find("div").show();
            }
            else {
                if (i == 0) {
                    if (validDouble($(e).find("input").val())) {//验证参考价是否为数字或者小树
                        $(e).find("div").hide();
                    } else {
                        proUserage.validataFla = false;
                        $(e).find("div").show();
                    }
                } else if (i == 2) {
                    if (validInt($(e).find("input").val())) {//验证数量只能为数字
                        $(e).find("div").hide();
                    } else {
                        proUserage.validataFla = false;
                        $(e).find("div").show();
                    }
                } else {
                    $(e).find("div").hide();
                }
            }
        })
        //错误提示显示或者隐藏开始END


        if (!proUserage.validataFla) { return false; } else {
            //操作当行input处于readonly="readonly"
            ele.each(function (i, e) {
                $(e).find("input").attr("readonly", "readonly")
                $(e).find("input").addClass("consumable_change");
            })
            //操作当行input处于readonly="readonly" END

            var tdAdd = $(ele[0]).nextAll("td:last");
            tdAdd.html(proUserage.editAndDel);//变更添加为编辑和删除
            fnapend(ele[0]);//添加新的一行
        }
    },
    append: function (parEle) {
        var tbody = $(parEle).parent().parent();
        $(tbody).append(proUserage.apendHtml);//这里用this.apendHtml 为什么不行？  测试 console.log(this.apendHtml);//为undefind 改为$(tbody).append
    },
    del: function (ele) {
        $(ele).parent().parent().remove();
    },
    edit: function (ele) {

        //解禁其他td下的input
        var tds = $(ele).parent().prevAll();
        tds.each(function (i, e) {
            $(e).find("input").removeAttr("readonly");
            $(e).find("input").removeClass("consumable_change");
        })
        $(ele).parent().html(proUserage.saveAndDel);//不能放在edit函数体的第一行，因为这样的话 ele已经发生了变化 下面就拿不到tds了，也就不会执行解禁了
        ///解禁其他td下的input END
    },
    save: function (ele) {
        proUserage.validataFla = true;

        //错误提示显示或者隐藏开始
        ele.each(function (i, e) {
            if ($(e).find("input").val().length == 0) {
                proUserage.validataFla = false;
                $(e).find("div").show();
            }
            else {
                if (i == 0) {
                    if (validDouble($(e).find("input").val())) {//验证参考价是否为数字或者小树
                        $(e).find("div").hide();
                    } else {
                        proUserage.validataFla = false;
                        $(e).find("div").show();
                    }
                } else if (i == 2) {
                    if (validInt($(e).find("input").val())) {//验证数量只能为数字
                        $(e).find("div").hide();
                    } else {
                        proUserage.validataFla = false;
                        $(e).find("div").show();
                    }
                } else {
                    $(e).find("div").hide();
                }
            }
        })
        //错误提示显示或者隐藏开始END

        if (!proUserage.validataFla) { return false; } else {
            //操作当行input处于readonly="readonly"
            ele.each(function (i, e) {
                $(e).find("input").attr("readonly", "readonly")
                $(e).find("input").addClass("consumable_change");
            })
            //操作当行input处于readonly="readonly" END

            var tdAdd = $(ele[0]).nextAll("td:last");
            tdAdd.html(proUserage.editAndDel);//变更添加为编辑和删除
        }
    }
};

//用材用料添加事件//jia+
function testss(ele) {
    //输入用材用料时隐藏
    $(".usage_show").find(".field-validation-error").find("span").hide();

    var tds = $(ele).parent().prevAll();//从后往前因为是preall() 之前的所有
    if ($(ele).attr("action") == "E") {//编辑
        proUserage.edit(ele);
    }
    else if ($(ele).attr("action") == "S") {//保存
        proUserage.save(tds);
    }
    else {//添加
        proUserage.add(tds, proUserage.append);
    }
}
//删除tr jia+
function delTr(ele) {
    proUserage.del(ele);
}

//添加用材用料 jia+
function addProUse() {

    //if (proUserage.validataFla) {//这里的值是 当点击添加时 如果该行通过验证则将validataFla设置为true
    var uses = [];
    //if ($("#ProductUse").val().length > 0) {//这块用于修改商品
    //    uses.push($("#ProductUse").val());
    //}
    $("#ProductUse").val("");
    var tab = $(".usage_table");
    var trs = tab.find("tr:not(tr:eq(0))");
    trs.each(function (i, e) {
        var signInputs = $(e).find("input");
        //if (($(signInputs[0]).val().length == 0) || (true)) { return false; } else {
        if ($($(e).find("td:last").find("a:eq(0)")).attr("action") != "E") { return false; } else {//条件是该行是已保存状态
            uses.push($(signInputs[0]).val() + '-' + $(signInputs[1]).val() + '-' + $(signInputs[2]).val() + '-' + $(signInputs[3]).val());
        }
    })
    var usesStr = uses.join(',');
    $("#ProductUse").val(usesStr);
    //}
}




//设为主图 滤镜版 jia+
function changImgBack(obj) {
    if (browser == "Microsoft Internet Explorer" && (trim_Version == "MSIE8.0" || trim_Version == "MSIE7.0" || trim_Version == "MSIE9.0")) {
        var rePlaceImgSrc = $("#imghead1").attr("style"); console.log(rePlaceImgSrc);
        $("#imgReplace").val(rePlaceImgSrc);
        var pre = $(obj).prev();
        var thisImg = $(pre).find("img");
        var imgSrc = $(thisImg).attr("style"); console.log(imgSrc);
        if (trim_Version == "MSIE7.0" || trim_Version == "MSIE8.0" || trim_Version == "MSIE9.0") {
            var fIndex = imgSrc.indexOf("src"); var sub1 = imgSrc.substr(fIndex + 5);
            var sIndex = sub1.indexOf(";"); var sub2 = sub1.substr(0, sIndex - 2);
            var ss = 'FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod="scale",src="' + sub2 + '");';
            document.getElementById("imghead1").style.filter = ss;//替换主图的filter



            var fIndexS = rePlaceImgSrc.indexOf("src"); var sub1S = rePlaceImgSrc.substr(fIndexS + 5);
            var sIndexS = sub1.indexOf(";"); var sub2S = sub1S.substr(0, sIndexS - 2);
            var bb = 'FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod="scale",src="' + sub2S + '");';
            var thisID = $(thisImg).attr("id");
            document.getElementById(thisID).style.filter = bb;

        } else {
            $("#imghead1").attr("style", imgSrc.replace("100px", "210px").replace("100px", "210px").replace("100px", "210px").replace("100px", "210px"));
            $(thisImg).attr("style", $("#imgReplace").val().replace("210px", "100px").replace("210px", "100px").replace("210px", "100px").replace("210px", "100px"));
        }
        $("#imgReplace").val("");
    } else {
        var rePlaceImgSrc = $("#imghead1").attr("src"); console.log(rePlaceImgSrc);
        $("#imgReplace").val(rePlaceImgSrc);
        var pre = $(obj).prev();
        var thisImg = $(pre).find("img");
        var imgSrc = $(thisImg).attr("src");
        $("#imghead1").attr("src", imgSrc);
        $(thisImg).attr("src", $("#imgReplace").val());
        $("#imgReplace").val("");
    }
}


//设为主图 多图版 jia+
function changImg(obj) {

    var rePlaceImgSrc = $("#imghead1").attr("src");
    $("#imgReplace").val(rePlaceImgSrc);
    var pre = $(obj).prev();
    var thisImg = $(pre).find("img");
    var imgSrc = $(thisImg).attr("src");

    if (imgSrc == undefined || imgSrc == "") {
        alert("请先选择图片，再进行主图设置操作");
    } else {
        //Input中值的替换
        var ImgIndex = $(obj).parent().index() + 1;
        //$("#MasterImg").val(MstImgIndex);//设置主图
        $("#fileImg1").val(imgSrc);
        $("#fileImg" + ImgIndex).val(rePlaceImgSrc);

        //Img src的替换
        $("#imghead1").attr("src", imgSrc);
        $("#imghead1").show();
        $(thisImg).attr("src", $("#imgReplace").val());
        $("#imgReplace").val("");
    }





}



//上门服务费用验证
function checkServiceFee()
{
    if ($("#IsDTDY").is(":checked")) {
        if ($("#excludeAttDtd").val().length > 0 && validPriceFormart($("#excludeAttDtd").val())) {
            return true;
        } else {
            $("#valexcludeAttDtd").text("请正确填写上门服务费用").show();
            return false;
        }
    } else {
        return true;
    }
}

//价格 预付金额  抵扣金额 关系检查
function checkPriceAdDis()
{
    if (($("#productPrice").val() * 1 > $("#acc_Info_DiscountsMoney").val() * 1) && ($("#acc_Info_DiscountsMoney").val() * 1 > $("#acc_Info_AdvanceMoney").val() * 1) && ($("#productPrice").val() * 1 > $("#acc_Info_AdvanceMoney").val() * 1)) {
        return true;
    } else {
        
        alert("请注意：商品价格需大于抵扣金额，抵扣金额需大于在线预付金额,请修改");
        return false;
    }
}

$(function () {
    $("#excludeAttDtd").change(function () {
        if (!validPriceFormart($("#excludeAttDtd").val())) {
            $("#valexcludeAttDtd").text("请正确填写上门服务费用").show();
        } else {
            $("#valexcludeAttDtd").text("").hide();
        }
    })

    $("#IsDTDN").change(function () {
        $("#excludeAttDtd").val("");
        $("#valexcludeAttDtd").text("").hide();
    })
})

//验证是否是双精度 jia+
function validDouble(val) {
    //reg = /^[-\+]?\d+(\.\d+)?$/;  ^\d+(\.\d+)?$ 
    ret = /^\d+(\.\d+)?$/;
    if (!ret.test(val)) {
        return false;
    } else {
        return true;
    }
}

//验证数字 jia+
function validInt(val) {
    var reg = new RegExp("^[0-9]*$");
    if (!reg.test(val)) {
        return false;
    } else {
        return true;
    }
}

//验证价格
function validPriceFormart(val) {
    var reg = new RegExp("^(0|[1-9][0-9]{0,9})(\.[0-9]{1,2})?$");
    if (!reg.test(val)) {
        return false;
    } else {
        return true;
    }
}
