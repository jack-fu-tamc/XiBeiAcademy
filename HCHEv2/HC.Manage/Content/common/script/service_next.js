var imglength = 0;
var beDelete = [];
var flag = 1;
var ieFlag = 0;
var cancelFlag = true;

if ((navigator.userAgent.indexOf('MSIE') >= 0) && (navigator.userAgent.indexOf('Opera') < 0)) {
    var browser = navigator.appName
    var b_version = navigator.appVersion
    var version = b_version.split(";");
    var trim_Version = version[1].replace(/[ ]/g, "");
}
$(function () {

    $(".hc_nav p").click(function () {

        var _this = this;
        var ul = $(_this).next();
        if (ul.css("display") == "none") {
            ul.slideDown("fast");
            ul.parent().find(".hc_set").addClass("hc_setlisrf");
        } else {
            ul.slideUp("fast");
            ul.parent().find(".hc_set").removeClass("hc_setlisrf");
        }
    });

    $(".hc_nav li").click(function () {
        var _this = this;
        var Curentp = $(_this).parent().prev();
        var text = $(_this).text();
        Curentp.text(text);
        $(".hc_new").hide();
        $("p").removeClass("hc_setlisrf");
    });



    //添加图片滤镜版
    $("#btnpreview").click(function () {
        flag = 1;
        ieFlag = 0;
        cancelFlag = true;
        var tempArrayLength;
        var tempArrayZhi;
        imglength = imgLength();
        if (imglength < 5) {

            //判定第几个file执行change事件
            var order = 0;
            if (beDelete.length > 0) {
                order = beDelete[0];
            }
            else {

                for (var i = 1; i < 6; i++) {
                    if ($("#fileImg" + i).attr("be") != "1") {
                        order = i;
                        break;
                    }
                }
            }
            //判定结束

            $("#fileImg" + order).change(function () {
                $("#preview").show();
                if (beDelete.length > 0) {

                    if (flag == 1) {
                        if ((browser == "Microsoft Internet Explorer" && trim_Version == "MSIE8.0"))//|| (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE9.0")
                        { flag = flag + 1; }
                        else
                        {
                            flag = flag + 1;
                        }
                        previewImage(this, beDelete[0]);
                        beDelete.splice(0, 1);
                    }
                    else {
                        return;
                    }
                }
                else {
                    if (flag == 1) {
                        if ((browser == "Microsoft Internet Explorer" && trim_Version == "MSIE8.0") || (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE9.0"))
                        { }
                        else
                        {
                            flag = flag + 1;//这里判断如果是IE8的话则不加
                        }
                        previewImage(this, imglength + 1);
                    }
                    else {
                        return;

                    }
                }
                //$("#fileImg").replaceWith("<input name='fileImg' type='file' id='fileImg' size='" + (40 + imageno) + "' style='display:none' />");
            });
            if (beDelete.length > 0) {
                tempArrayLength = beDelete.length;
                tempArrayZhi = beDelete[0];
            }
            $("#fileImg" + order).click();
            ////////////////////////////////////////////////////////////////删除后添加显示图片//////
            if (tempArrayLength > 0) {
                if ((browser == "Microsoft Internet Explorer" && trim_Version == "MSIE8.0") || (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE9.0") || (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE7.0")) {

                    if (ieFlag == 1) {

                        $("#imghead" + tempArrayZhi).show();
                        $("#imghead").find(".set_main").show();
                    }
                    else {
                        return false;
                    }
                } else {
                    $("#imghead" + imglength + 1).show();
                }
            }
                ////////////////////////////////////////////////////////////////删除后添加显示图片结束///////////
                ////////////////////////////////////////////////////////////////////////正常情况显示图片/////
            else {
                if ((browser == "Microsoft Internet Explorer" && trim_Version == "MSIE8.0") || (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE9.0") || (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE7.0")) {
                    var cc = imglength + 1;

                    if (ieFlag == 1) {

                        $("#imghead" + cc).show();
                    }
                    else {
                        return false;
                    }
                } else {

                    $("#imghead" + imglength + 1).show();
                    $("#imghead" + imglength + 1).css({ "display": "block" });
                }
            }

            ////////////////////////////////////////////////////////////////////////正常情况显示图片结束/////
        }
    });

    //删除添加的图片 滤镜版
    //$(".hc_patImg img").click(function () {
    //    $(this).hide();

    //    // $(this).attr("src", "");//  (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE9.0"  原来是MSIE8.0
    //    if ((browser == "Microsoft Internet Explorer" && (trim_Version == "MSIE9.0" || trim_Version == "MSIE8.0")) || (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE7.0")) {
    //        var attrid = $(this).attr("id");
    //        var curentIDIe8 = attrid.substr(attrid.length - 1, 1)
    //        beDelete.push(curentIDIe8);
    //        //清空IE8下的 file控件
    //        emptyFileUpload("#fileImg" + curentIDIe8);
    //    }
    //    else {
    //        var curentID = $(this).attr("id").substr(-1);
    //        beDelete.push(curentID);

    //        //清空file控件
    //        var broswerFile = document.getElementById("fileImg" + curentID);
    //        if (broswerFile.outerHTML) {
    //            broswerFile.outerHTML = broswerFile.outerHTML;
    //        } else { // FF(包括3.5)
    //            broswerFile.value = "";
    //        }
    //    }

    //    var curentImglength = imgLength();

    //    //解禁添加按钮
    //    $("#btnpreview").attr("disabled", false);
    //    //if (curentImglength == 0) {
    //    //    $("#preview").css("display", "none");
    //    //    $("#btnpreview").css("float", "left");
    //    //} else {
    //    //    $("#preview").css("display", "block");
    //    //    $("#btnpreview").css("float", "right");
    //    //}
    //});
    //删除添加的图片 滤镜版end

    //删除添加的图片 多图版
    $(".hc_patImg img").click(function () {
       
        $(this).attr("src", "");
        $(this).hide();
        var thisIndex = $(this).parent().parent().index() + 1; 
        $("#fileImg" + thisIndex).val("");//删除清空对应text的val
        //if ((browser == "Microsoft Internet Explorer" && (trim_Version == "MSIE9.0" || trim_Version == "MSIE8.0")) || (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE7.0")) {
        //    var attrid = $(this).attr("id");
        //    var curentIDIe8 = attrid.substr(attrid.length - 1, 1)
           
        //}
        //else {
        //    var curentID = $(this).attr("id").substr(-1);
        //    beDelete.push(curentID);
        //}
        //var curentImglength = imgLength();
        //解禁添加按钮
        $("#btnprevieww").attr("disabled", false);
    });


});

//图片上传预览    IE是用了滤镜。
function previewImage(file, imageno) {
    if (imageno == 1) {
        var MINWIDTH = 210;
        var MINHEIGHT = 210;
    } else {
        var MINWIDTH = 100;
        var MINHEIGHT = 100;
    }
    var div = document.getElementById('preview');
    if (file.files && file.files[0]) {
        var img = document.getElementById("imghead" + imageno);
        img.onload = function () {
            var rect = clacImgZoomParam(MINWIDTH, MINHEIGHT, img.offsetWidth, img.offsetHeight);
            $(img).attr("style", "minwidth:" + MINWIDTH + "px;height:" + MINHEIGHT + "px;margin-left:" + 0 + "px;margin-top:" + 0 + "px;display: block");
        }
        var reader = new FileReader();
        reader.onload = function (evt) { img.src = evt.target.result; }
        reader.readAsDataURL(file.files[0]);
        cancelFlag = false;
    }
    else //兼容IE
    {
        $("#imghead" + imageno).css({
            "max-width": MINWIDTH + "px",
            "max-height": MINHEIGHT + "px",
            "width": MINWIDTH + "px",
            "height": MINHEIGHT + "px",
            "margin-top": "0px"
        });

        //zm OK
        ////jia 0824+ 加上这句 file.value才能拿到值
        //$(file).select();
        ////0824 end
        //$("#imghead" + imageno).attr("src", file.value);
        //var img = document.getElementById("imghead" + imageno);
        //rect = clacImgZoomParam(MINWIDTH, MINHEIGHT, img.offsetWidth, img.offsetHeight);
        //zm ok  end


        var ss = 'FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod="scale",src="' + file.value + '");'
        var img = document.getElementById("imghead" + imageno);
        img.style.filter = ss;
        //img.style.width = 150; img.style.height = 150;

       

    }


    //谷歌浏览器
    if (window.navigator.userAgent.indexOf("Chrome") !== -1) {
        if (!cancelFlag) {
            var djImglength = imgLength();
            if ((djImglength + 1) >= 5) {
                $("#btnpreview").attr("disabled", 'disabled');
            }
            ieFlag = 1;
        }
    }
    else {

        var djImglength = imgLength();
        if ((djImglength + 1) >= 5) {
            $("#btnpreview").attr("disabled", 'disabled');
        }
        ////////按钮处理结束//////

        /////ie显示处理
        ieFlag = 1;
    }
    //给file添标记
    $("#fileImg" + imageno).attr("be", "1");
    //将添加按钮右移
    //$("#btnpreview").css("float", "right");
}









function clacImgZoomParam(MINWIDTH, MINHEIGHT, width, height) {
    var param = { top: 0, left: 0, width: width, height: height };
    if (width > MINWIDTH || height > MINHEIGHT) {
        rateWidth = width / MINWIDTH;
        rateHeight = height / MINHEIGHT;

        if (rateWidth > rateHeight) {
            param.width = MINWIDTH;
            param.height = Math.round(height / rateWidth);
        } else {
            param.width = Math.round(width / rateHeight);
            param.height = MINHEIGHT;
        }
    }

    param.left = Math.round((MINWIDTH - param.width) / 2);
    param.top = Math.round((MINHEIGHT - param.height) / 2);
    return param;
}

//上传图片
function checkLiceImg(liceimg) {

    if ($(liceimg).val() == "") {
        $("#liceimg_error").attr("class", "spanmsci");
        $("#liceimg_succeed").hide();
        liceimgstate = false;
    } else {

        //获取验证图片的类型
        var msg = "";
        var strTemp = $(liceimg).val().split(".");
        var strCheck = strTemp[strTemp.length - 1];

        //验证图片类型库
        var imagetype = "jpg|bmp|jpeg|png";
        var typelst = imagetype.split("|");

        var format = false;
        for (var i = 0; i < typelst.length; i++) {
            var type = typelst[i];
            if (type == strCheck.toLowerCase()) {
                format = true;
            }
        }

        if (format) {
            var fileSize = 0;
            try {
                fileSize = $(liceimg).get(0).files[0].size;
            } catch (e) {
                fileSize = 1024;
            }
            if ((fileSize / 1024) > 4096) {
                $("#liceimg_error").attr("class", "spanmsci");

                liceimgstate = false;
            }
            else {
                $("#liceimg_succeed").show();
                $("#liceimg_error").html("&nbsp;");
                liceimgstate = true;
            }
        }
        else {
            $("#liceimg_error").attr("class", "spanmsci");

            liceimgstate = false;
        }
    }
}
/**/
$(function () {

    $(" .nite_next ").click(function () {
        $(this).addClass("mas_show_new");
    });



});

//计算已添加的数量
function imgLength() {
    var imgs = $("#preview").find("img");
    var imgBlcokLength = 0;
    imgs.each(function (i, e) {
        if ($(e).css("display") != "none") {
            imgBlcokLength = imgBlcokLength + 1;
        }
    });
    return imgBlcokLength;
}



//IE8清空函数
//不能直接用js修改input type=file的value，但可以通过form的reset()清空它的值
//解决：将input type=file放进一个临时form，清空value,再将它移到原来位置
function emptyFileUpload(selector) {
    var fi;
    var sourceParent;

    if (selector) {
        fi = $(selector);
        sourceParent = fi.parent();
        fi.remove();
    }
    else {
        return;
    }

    $("<form id='tempForm'></form>").appendTo(document.body);

    var tempForm = $("#tempForm");

    tempForm.append(fi);
    tempForm.get(0).reset();

    sourceParent.append(fi);
    tempForm.remove();
}



