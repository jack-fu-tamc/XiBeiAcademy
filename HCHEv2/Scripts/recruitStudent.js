/**
 * Created by Administrator on 2016/11/10.
 */
$(function(){
    $('.collegeMenu>ul>li').each(function(){
        $(this).click(function(){
           /* $(this).find('.menuShow').slideToggle();*/
            if($(this).find('.menuShow').is(':hidden')){
                $(this).parent().find('.menuShow').slideUp();
                $(this).find('.menuShow').slideDown();
            }
        });
    });

    /*焦点图*/
    //代码初始化
    var size=$(".img li").size();

    for(var i=1; i<=size; i++){
        var li="<li></li>";
        $(".num").append(li);
    }
    //手动控制轮播图
    $(".img li").eq(0).show();
    $(".num li").eq(0).addClass("active");
    $(".num li").mouseover(function(){
        $(this).addClass("active").siblings().removeClass("active");
        var index=$(this).index();
        i=index;
        $(".img li").eq(index).stop().fadeIn(300).siblings().stop().fadeOut(300);
    })



    //自动轮播
    var i=0;
    var t=setInterval(move,1500);


    //核心向左运动函数
    function moveL(){
        i--;
        if(i==-1){
            i=size-1;
        }

        $(".num li").eq(i).addClass("active").siblings().removeClass("active");
        $(".img li").eq(i).fadeIn(300).siblings().fadeOut(300);

    }


    //核心向右运动函数
    function move(){
        i++;
        if(i==size){
            i=0;
        }

        $(".num li").eq(i).addClass("active").siblings().removeClass("active");
        $(".img li").eq(i).fadeIn(300).siblings().fadeOut(300);

    }


    //左边按钮点击事件
    $(".out .left").click(function(){
        moveL();
    })

    //右边按钮点击事件
    $(".out .right").click(function(){
        move()

    })


    //定时器的开始于结束
    $(".out").hover(function(){
        clearInterval(t)
    },function(){
        t=setInterval(move,1500);
    })



})

$(function(){
    /*通知公告*/
    //代码初始化
    var size=$(".message-list li").size();

    for(var i=1; i<=size; i++){
        var li='<li><a href=""></a></li>';
        $(".slide-list").append(li);
    }

    //自动轮播
    var i=0;
    var t=setInterval(moveL,2000);


    //核心向左运动函数
    function moveL(){
        i++;
        if(i==size){
            i=0;
        }
        var top=i*35;
        $(".slide-list li").eq(i).addClass("active").siblings().removeClass("active");
        $(".message-list").css('top',-top);

    }


    //定时器的开始于结束
    $(".out").hover(function(){
        clearInterval(t)
    },function(){
        t=setInterval(moveL,2000);
    })


});
$(function () {
    /*左侧下拉菜单划过颜色*/
    $('.collegeMenu>ul>li').each(function () {
        $(this).click(function () {
          if($(this).find('li').size()<1){
              $(this).parent().find('li').removeClass('big-change');
             // $(this).addClass('big-change');
          }
        });
    });
    $('.stud-menu>ul>li').each(function () {
        $(this).click(function () {
            if($(this).find('li').size()<1){
                $(this).parent().find('li').removeClass('active-changeRedColor');
                //$(this).addClass('active-changeRedColor');
            }
        });
    });

    $('.menuShow>li').each(function () {
        $(this).click(function () {
            $(this).parent().find('li').removeClass('small-change');
            //$(this).addClass('small-change');
        });

    });
})