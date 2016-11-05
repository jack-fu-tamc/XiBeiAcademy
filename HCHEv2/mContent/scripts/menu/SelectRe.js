function $(p){return jQuery(p);}

var xdmJS={
/*判断浏览器,根据函数返回值判定属于什么浏览器,例如判断是否是IE，if(xdmJS.browser()=="ie6"){alert("this is ie6")}*/
browser:function(){
    var Sys = {};
    var ua = navigator.userAgent.toLowerCase();
    var s;
    var bro;
    (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
    (s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
    (s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
    (s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
    (s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;
    //以下判断
    if (Sys.ie){
        if(Sys.ie=="6.0"){bro="ie6";}
        else if(Sys.ie=="7.0"){bro="ie7";}
        else if(Sys.ie=="8.0"){bro="ie8";}
        else if(Sys.ie=="9.0"){bro="ie9";}}

    if (Sys.firefox){bro="firefox";}
    if (Sys.chrome){bro="chrome";}
    if (Sys.opera){bro="opera";}
    if (Sys.safari){bro="safari";}
    return bro;
},

/*根据原生下拉框的HTML模拟select下拉菜单，宽高自适应，内容多少自适应，同一页里面多个下拉框也能用*/
selectAnalog:function(selectId,className){
/*selectId:代表要使用模拟的select的ID名字或者class名字
className:代表覆盖默认设置的select-analog的样式及其子孙元素的样式*/
$("#"+selectId).each(function(index){
    /*根据select的值创建相应的HTML*/
    var firstHtml=$(this).find("option:first").html();
    var html="<div class='select-analog";
    if(className){ /*如果有覆盖样式的话就加上样式，否则不加*/
    html+=" "+className;}
    html+="'><a class='title'>";
    //html+=firstHtml;
    html+="</a>";
    var htmlUl=$("<ul></ul>");
    var optionLength=$(this).find("option").length;

    for(var i=0;i<optionLength;i++){
    var htmlLi="<li><a href='"+$(this).find("option").eq(i).val()+"'>"+$(this).find("option").eq(i).html()+"</a></li>";
    htmlUl.append(htmlLi);
}

html+="<ul>"+htmlUl.html()+"</ul></div>";
$(this).after(html);
var selectW=$(this).width();
var arrWidth=$(".arr").eq(index).width();

//$(this).hide();

/*对生成的相应的HTML加方法模拟select下拉框*/
var btnId=$(".select-analog").eq(index).find(".title");
var ul=$(".select-analog").eq(index).find("ul");
var arr="<span class='arr'></span>";

/*判断浏览器,因为各浏览器对select的宽度取值不一样*/

//if (xdmJS.browser()=="safari") {$(btnId).width(selectW+arrWidth+20);}
// else{$(btnId).width(selectW+arrWidth+2);}

var paddingW=parseInt($(btnId).css("padding-left"));
//ul.width($(btnId).width()+paddingW);
//$(".select-analog").width($(btnId).width()+paddingW);
$(".select-analog").mouseleave(function(){ul.hide();});

$(btnId).click(function(){
$(".select-analog").find("ul").hide().eq(index).show();
return false;
});

//一下代码赋值时使用
//ul.find("a").click(function(){
//$(btnId).html($(this).html()+arr);
//var aindex=ul.find("a").index($(this));
////第一种方法

////$(this).parents(".select-analog").prev("select")[0].selectedIndex=aindex;

////第二种方法
//$(this).parents(".select-analog").prev("select").find("option").eq(aindex).attr("selected",true);

//ul.hide();
//return false;
//});

});
}
}
