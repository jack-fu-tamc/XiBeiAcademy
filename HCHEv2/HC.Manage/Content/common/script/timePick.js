/* 
 * Author: senthil
 * plugin: timepicker
 */
(function ($) {

    $.fn.timepicki = function (options) {

        var defaults = {

        };

        var settings = $.extend({}, defaults, options);

        return this.each(function () {

            var ele = $(this);
            var ele_hei = ele.outerHeight();
            var ele_lef = ele.position().left;
            ele_hei += 10;
            $(ele).wrap("<div class='time_pick'>");
            var ele_par = $(this).parents(".time_pick");
            //ele_par.append("<div class='timepicker_wrap'><div class='arrow_top'></div><div class='time'><div class='prev'></div><div class='ti_tx'></div><div class='next'></div></div><div class='mins'><div class='prev'></div><div class='mi_tx'></div><div class='next'></div></div><div class='meridian'><div class='prev'></div><div class='mer_tx'></div><div class='next'></div></div></div>");
            ele_par.append("<div class='timepicker_wrap'><div class='arrow_top'></div><div class='time'><div class='prev'></div><div class='ti_tx'></div><div class='next'></div></div><div class='hoursUnit'>时</div>   <div class='mins'><div class='prev'></div><div class='mi_tx'></div><div class='next'></div></div> <div class='minitUnit'>分</div>     </div>");
            var ele_next = $(this).next(".timepicker_wrap");
            var ele_next_all_child = ele_next.find("div");
            //ele_next.css({ "top": ele_hei+"px", "left": ele_lef+"px"});
            //$(document).on("click", function (event) {
            $(document).click(function (event) {
                //console.log(event.target);
                var ee = event.srcElement ? event.srcElement : event.target;
                if (!$(ee).is(ele_next)) {
                    if (!$(ee).is(ele)) {
                        var tim = ele_next.find(".ti_tx").html();
                        var mini = ele_next.find(".mi_tx").text();
                        var meri = ele_next.find(".mer_tx").text();
                        //if(tim.length !=0 && mini.length !=0 && meri.length !=0 )
                        //{
                        //    ele.val(tim+" : "+mini+"  "+meri);
                        //}
                        if (tim.length != 0 && mini.length != 0) {
                            ele.val(tim + " : " + mini);
                        }
                        if (!$(ee).is(ele_next) && !$(ee).is(ele_next_all_child)) {
                            ele_next.fadeOut();
                        }
                    }
                    else {
                        set_date(ele);
                        ele_next.fadeIn();
                    }
                }
            });
            function set_date(obj) {
                var timm, ti, mi; //console.log(obj);
                if (obj.val().length != 0) {
                    timm = obj.val().split(":");
                    ti = parseInt(timm[0]);
                    mi = parseInt(timm[1]);
                } else {
                    ti = 8;
                    mi = 0;
                }
                var d = new Date();
                //var ti = d.getHours();
                //var mi = d.getMinutes();

                var mer = "上午";
                //if (12 < ti) {
                //    ti -= 12;
                //    mer = "下午";
                //}
                //console.log(ele_next);
                //console.log(ti);
                //console.log(mi);
                if (ti < 10) {
                    ele_next.find(".ti_tx").text("0" + ti);
                }
                else {
                    ele_next.find(".ti_tx").text(ti);
                }
                if (mi < 10) {
                    ele_next.find(".mi_tx").text("0" + mi);
                }
                else {
                    ele_next.find(".mi_tx").text(mi);
                }
                //if(mer<10)
                //    {
                //        ele_next.find(".mer_tx").text("0"+mer);
                //    }
                //else{
                //        ele_next.find(".mer_tx").text(mer);
                //    }
            }


            var cur_next = ele_next.find(".next");
            var cur_prev = ele_next.find(".prev");


            $(cur_prev).add(cur_next).on("click", function () {
                //console.log("click");
                var cur_ele = $(this);
                var cur_cli = null;
                var ele_st = 0;
                var ele_en = 0;
                if (cur_ele.parent().attr("class") == "time") {//时
                    cur_cli = "time";
                    ele_en = 12;
                    var cur_time = null;
                    cur_time = ele_next.find("." + cur_cli + " .ti_tx").text(); //显示时
                    cur_time = cur_time * 1; 
                    if (cur_ele.attr("class") == "next") {//加
                        if (cur_time == 23) {//12
                            ele_next.find("." + cur_cli + " .ti_tx").text("00");
                        }
                        else {
                            cur_time++;
                            if (cur_time < 10) {
                                ele_next.find("." + cur_cli + " .ti_tx").text("0" + cur_time); //console.log("0" + cur_time);
                            }
                            else {
                                ele_next.find("." + cur_cli + " .ti_tx").text(cur_time);
                            }
                        }
                    }
                    else {//减
                        //alert("jian");
                        if (cur_time == 00) {
                            ele_next.find("." + cur_cli + " .ti_tx").text(23);//12
                        }
                        else {
                            cur_time--;
                            if (cur_time < 10) {
                                ele_next.find("." + cur_cli + " .ti_tx").text("0" + cur_time);
                            }
                            else {
                                ele_next.find("." + cur_cli + " .ti_tx").text(cur_time);
                            }
                        }
                    }
                }
                else if (cur_ele.parent().attr("class") == "mins") {//分
                    //alert("mins");
                    cur_cli = "mins";
                    ele_en = 59;
                    var cur_mins = null;
                    cur_mins = ele_next.find("." + cur_cli + " .mi_tx").text();
                    //cur_mins = parseInt(cur_mins);
                    cur_mins = cur_mins * 1;
                    if (cur_ele.attr("class") == "next") {
                        //alert("nex");
                        if (cur_mins == 59) {
                            ele_next.find("." + cur_cli + " .mi_tx").text("00");
                        } else {
                            cur_mins++;
                            if (cur_mins < 10) {
                                ele_next.find("." + cur_cli + " .mi_tx").text("0" + cur_mins);
                            }
                            else {
                                ele_next.find("." + cur_cli + " .mi_tx").text(cur_mins);
                            }
                        }
                    }
                    else {

                        if (cur_mins == 0) {
                            ele_next.find("." + cur_cli + " .mi_tx").text(59);
                        }
                        else {
                            cur_mins--;

                            if (cur_mins < 10) {
                                ele_next.find("." + cur_cli + " .mi_tx").text("0" + cur_mins);
                            }
                            else {
                                ele_next.find("." + cur_cli + " .mi_tx").text(cur_mins);
                            }

                        }

                    }
                }
                //else {//上下午
                //    //alert("merdian");
                //    ele_en = 1;
                //    cur_cli = "meridian";
                //    var cur_mer = null;
                //    cur_mer = ele_next.find("."+cur_cli+" .mer_tx").text();
                //    if (cur_ele.attr("class") == "next") {
                //        //alert(cur_mer);
                //        if(cur_mer=="上午"){
                //          ele_next.find("."+cur_cli+" .mer_tx").text("下午");
                //        }
                //        else{
                //         ele_next.find("."+cur_cli+" .mer_tx").text("上午");
                //        }
                //    } else {
                //        if(cur_mer=="上午"){
                //          ele_next.find("."+cur_cli+" .mer_tx").text("下午");
                //        }
                //        else{
                //         ele_next.find("."+cur_cli+" .mer_tx").text("上午");
                //        }
                //    }
                //}


            });

        });
    };

}(jQuery));
