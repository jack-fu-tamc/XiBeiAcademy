
//上传身份证图片块
//图片上传全局变量
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






$(document).ready(function () {

    //添加图片
    $("#btnpreview").click(function () {
        flag = 1;
        ieFlag = 0;
        cancelFlag = true;
        var tempArrayLength;
        var tempArrayZhi;
        imglength = imgLength();
        if (imglength < 2) {

            //判定第几个file执行change事件
            var order = 0;
            if (beDelete.length > 0) {
                order = beDelete[0];
            }
            else {

                for (var i = 1; i < 3; i++) {
                    if ($("#fileImg" + i).attr("be") != "1") {
                        order = i;
                        break;
                    }
                }
            }
            //判定结束
            //$("#fileImg" + order).trigger("click");
            $("#fileImg" + order).change(function () {
                $("#preview").show();
                if (beDelete.length > 0) {
                    if (flag == 1) {
                        //if ((browser == "Microsoft Internet Explorer" && trim_Version == "MSIE8.0"))
                        if ((browser == "Microsoft Internet Explorer" && trim_Version == "MSIE8.0") || (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE9.0"))
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
                        //alert(cc);
                        $("#imghead" + cc).show();
                    }
                    else {
                        return false;
                    }
                } else {
                    $("#imghead" + imglength + 1).show();
                }
            }
            ////////////////////////////////////////////////////////////////////////正常情况显示图片结束/////
        }
    });

    //删除添加的图片
    $("#imagelist img").click(function () {
        $(this).hide();

        // $(this).attr("src", "");
        if ((browser == "Microsoft Internet Explorer" && trim_Version == "MSIE8.0") || (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE7.0") || (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE9.0")) {
            var attrid = $(this).attr("id");
            var curentIDIe8 = attrid.substr(attrid.length - 1, 1)
            beDelete.push(curentIDIe8);
            //显示被删除图片的 选择验证提示
            $(".spanmscinone" + curentIDIe8).children().html("<span for='fileImg2' generated='true' class=''>请选择身份证照 " + curentIDIe8 + "</span>")
            //清空IE8下的 file控件
            emptyFileUpload("#fileImg" + curentIDIe8);
        }
        else {
            var curentID = $(this).attr("id").substr(-1);
            beDelete.push(curentID);


            //显示被删除图片的 选择验证提示     
            $(".spanmscinone" + curentID).children().html("  <span for='fileImg2' generated='true' class=''>请选择身份证照 " + curentID + "</span>")

            //清空file控件
            var broswerFile = document.getElementById("fileImg" + curentID);
            if (broswerFile.outerHTML) {
                broswerFile.outerHTML = broswerFile.outerHTML;
            } else { // FF(包括3.5)
                broswerFile.value = "";
            }
        }

        var curentImglength = imgLength();

        //解禁添加按钮
        $("#btnpreview").attr("disabled", false);
        if (curentImglength == 0) {
            $("#preview").css("display", "none");
        } else {
            $("#preview").css("display", "block");
        }

        //checkLicebusimg($("#fileImg1"), $("#fileImg2"));
    });

})



//图片上传预览    IE是用了滤镜。
function previewImage(file, imageno) {
    var MAXWIDTH = 100;
    var MAXHEIGHT = 100;
    var div = document.getElementById('preview');
    if (file.files && file.files[0]) {
        var img = document.getElementById("imghead" + imageno);
        img.onload = function () {
            var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
            $(img).attr("style", "width:" + MAXWIDTH + "px;height:" + MAXHEIGHT + "px;margin-Top:" + 10 + "px;");
        }
        var reader = new FileReader();
        reader.onload = function (evt) { img.src = evt.target.result; }
        reader.readAsDataURL(file.files[0]);
        cancelFlag = false;
    }
    else //兼容IE
    {
        $("#imghead" + imageno).css({
            "max-width": MAXWIDTH + "px",
            "max-height": MAXHEIGHT + "px",
            "width": MAXWIDTH + "px",
            "height": MAXHEIGHT + "px"
            //"margin-top": "10px"
        });



        /////////0730 例子获取file类型Input的路径通过createRange
        //imgFile.select();
        //path = document.selection.createRange().text;
        //document.getElementById("imgPreview").innerHTML = "";
        //document.getElementById("imgPreview").style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled='true',sizingMethod='scale',src=" + path + ")";//使用滤镜效果
        //////////0730end

        //0730 例子获取file类型Input的路径通过outerHtml
        //var str = file.outerHTML;
        //var indexS = str.indexOf("value");
        //var indexE = str.indexOf("data-val-required");
        //var indexE7 = str.indexOf("name");
        //var path = "";
        //if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE7.0") {
        //    path = str.substr(indexS, indexE7 - indexS).substr(6);
        //}
        //else {
        //     path = str.substr(indexS, indexE - indexS).substr(6);
        //}
        //var ss = 'FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod="scale",src="' + path + '");'

        //0827zs
        //var ss = 'FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod="scale",src="' + file.value + '");'
        //var eleStr = "imghead" + imageno;
        //var eleImg = document.getElementById(eleStr);
        //eleImg.src = ""
        //eleImg.style.filter = ss;
        //eleImg.style.width = 100; eleImg.style.height = 100;
        //0827zs end

        //var img = document.getElementById("imghead" + imageno);
        //rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);

        //0827+

       
        $(file).select();
        var ss = 'FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod="scale",src="' + file.value + '");'
        var img = document.getElementById("imghead" + imageno);
        img.style.filter = ss;
        $(img).css("style", "filter:" + ss);
        //0827+ end

    }


    //谷歌浏览器
    if (window.navigator.userAgent.indexOf("Chrome") !== -1) {
        if (!cancelFlag) {
            var djImglength = imgLength();
            if ((djImglength + 1) >= 2) {
                $("#btnpreview").attr("disabled", 'disabled');
            }
            ieFlag = 1;
        }
    }
    else {

        var djImglength = imgLength(); //console.log(djImglength);
        if ((djImglength + 1) >= 2) {
            $("#btnpreview").attr("disabled", 'disabled');
        }
        ////////按钮处理结束//////

        /////ie显示处理
        ieFlag = 1;
    }
    //给file添标记
    $("#fileImg" + imageno).attr("be", "1");

    ////清除身份证照选择后的错误验证信息
    //$(".spanmscinone" + imageno).children().children().text("");//.hide();
    $(".spanmscinone" + imageno).children().children().css({ "display": "none" });
}



function clacImgZoomParam(maxWidth, maxHeight, width, height) {
    var param = { top: 0, left: 0, width: width, height: height };
    if (width > maxWidth || height > maxHeight) {
        rateWidth = width / maxWidth;
        rateHeight = height / maxHeight;

        if (rateWidth > rateHeight) {
            param.width = maxWidth;
            param.height = Math.round(height / rateWidth);
        } else {
            param.width = Math.round(width / rateHeight);
            param.height = maxHeight;
        }
    }

    param.left = Math.round((maxWidth - param.width) / 2);
    param.top = Math.round((maxHeight - param.height) / 2);
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
