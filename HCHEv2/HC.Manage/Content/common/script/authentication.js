$(function () {
    $("#yingyebut").click(function () { $("#yingyeimg").trigger("click"); });
    $("#yingyeimg").change(function () { previewImageLice(this); });
});
//上传图片
function checkLiceImg(liceimg) {

    if ($(liceimg).val() == "") {
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

                liceimgstate = false;
            }
            else {

                liceimgstate = true;
            }
        }
        else {

            liceimgstate = false;
        }
    }
}
//图片上传预览    IE是用了滤镜。
function previewImageLice(file) {
   
    //验证营业执照
    checkLiceImg(file);
    if (!liceimgstate) {
        //下面2句 主要针对google浏览器 操作 浏览然后点取消时的bug
        $("#yingyeiew").hide();
        //显示验证错误信息
        $(".yingyeValidate").children().html("<span for='yingyeimg' generated='true' class=''>请选择营业执照</span>")
        return;
    }

    $("#yingyejpgimg").attr('src', "");
    $("#yingyejpgimg").attr('style', "");
    $("#yingyeiew").show();
    
    
    var MAXWIDTH = 150;
    var MAXHEIGHT = 150;
    var div = document.getElementById('yingyeiew');
    if (file.files && file.files[0]) {
        div.innerHTML = '<img id=yingyejpgimg>';
        var img = document.getElementById('yingyejpgimg');
        img.onload = function () {
            var rect = clacImgZoomParam(MAXWIDTH, MAXHEIGHT, img.offsetWidth, img.offsetHeight);
            $(img).attr("style", "width:" + 150 + "px;height:" + 150 + "px;margin-Top:" + 0 + "px;");
        }
        var reader = new FileReader();
        reader.onload = function (evt) { img.src = evt.target.result; }
        reader.readAsDataURL(file.files[0]); 
    }
    else //兼容IE
    {
        var ss = 'FILTER: progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod="scale",src="' + file.value + '");'
        var img = document.getElementById('yingyejpgimg');
        img.style.filter = ss;
        img.style.width = 150; img.style.height = 150;
    }
    //清除证件照选择后的错误验证信息
    $(".yingyeValidate").children().children().text("");
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






