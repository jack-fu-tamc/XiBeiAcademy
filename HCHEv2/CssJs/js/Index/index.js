//swiper banner start

var mySwiper = new Swiper('.swiper-container', {
    pagination: '.pagination',
    loop: true,//循环模式  是/否
    grabCursor: true,//光标属性   当为true时，光标移动到banner上变成手掌的样式
    paginationClickable: false,//生成分页控件
    calculateHeight: true,//响应式容器高度
    autoplay: 3000,//过度时间
    autoplayDisableOnInteraction: false//自动切换不会停止
})
$(function () {
    var h = $(".swiper-slide").find("img").height();
    $(".swiper-wrapper,.swiper-slide").css("height", h);
});
$(window).resize(function () {
    var h = $(".swiper-slide").find("img").height();
    $(".swiper-wrapper,.swiper-slide").css("height", h)
})

$('.arrow-left').on('click', function (e) {
    e.preventDefault()
    mySwiper.swipePrev()
})
$('.arrow-right').on('click', function (e) {
    e.preventDefault()
    mySwiper.swipeNext()
});

$(function () {
    var h = $('.love-item').height();
    var w = $('.love-item').width();
    $('.love-item img').each(function () {
        var hc = $(this).height();
        var wc = $(this).width();
        if (wc = w) {
            hc = w;
            $(this).css({ 'width': w, 'height': w })
        }
    })
});
$(function () {
    $('body').addClass('mhome hide-landing');
})
$(window).resize(function () {
    var h = $('.love-item').height();
    var w = $('.love-item').width();
    $('.love-item img').each(function () {
        var hc = $(this).height();
        var wc = $(this).width();
        if (wc = w) {
            hc = w;
            $(this).css({ 'width': w, 'height': w })
        }
    })
});

$(window).resize(function () {
    var h = $('.discount').height();
    var w = $('.discount').width();
    var hr = $('.up-floor').height() + $('.down-floor').height();
    $('.half-floor img').each(function () {
        var hc = $(this).height();
        var wc = $(this).width();
        if (wc != w) {
            hc = hr - 2;
            wc = w / 2;
            $(this).css({ 'width': wc, 'height': hc })
        }
    })
});
$(function () {
    var h = $('.seckill-item-img').height();
    var w = $('.seckill-new-item').width() - 8;
    $('.seckill-new-item img').each(function () {
        var hc = $(this).height();
        var wc = $(this).width();
        if (wc != hc) {
            var ig = wc / hc;
            hc = hc * ig;
            wc = w;
            $(this).css({ 'width': wc, 'height': hc })
        }
        if (wc = hc) {
            wc = w;
            hc = wc;
            $(this).css({ 'width': wc, 'height': hc })
        }
    })
});
$(window).resize(function () {
    var h = $('.seckill-item-img').height();
    var w = $('.seckill-new-item').width() - 8;
    $('.seckill-new-item img').each(function () {
        var hc = $(this).height();
        var wc = $(this).width();
        if (wc != hc) {
            var ig = wc / hc;
            hc = hc * ig;
            wc = w;
            $(this).css({ 'width': wc, 'height': hc })
        }
        if (wc = hc) {
            wc = w;
            hc = wc;
            $(this).css({ 'width': wc, 'height': hc })
        }
    })
});

//返回顶部
$(function () {
    var d = document.querySelector("#indexToTop");
    var c = "click";
    b = window.navigator.userAgent;
    if (/android|ipad|iphone/ig.test(b)) { }
    var a = {
        init: function () {
            a.scrollEvt();
            a.toTopEvt()
        },
        toTopEvt: function () {
            d.addEventListener(c, function () {
                scroll(0, 0);
                d.style.display = "none"
            }
            , false)
        },
        scrollEvt: function () {
            window.addEventListener("scroll", function () {
                var e = document.documentElement.clientHeight || document.body.clientHeight;
                var f = document.documentElement.scrollTop || document.body.scrollTop;
                if (f > e) {
                    d.style.display = "block"
                } else {
                    d.style.display = "none"
                }
            }
            , false)
        }
    };
    return a.init()
});
//TEHEUI
var startX,//触摸时的坐标
            startY,
             x, //滑动的距离
             y,
             aboveY = 0,
             aboveX = 0;

var inner = document.getElementById("seckillul");

function touchSatrt(e) {//触摸
    e.preventDefault();
    var touch = e.touches[0, 0];
    startX = touch.pageX;
    startY = touch.pageY;   //刚触摸时的坐标
}

function touchMove(e) {//滑动
    e.preventDefault();
    var touch = e.touches[0, 0];
    x = touch.pageX - startX;
    y = touch.pageY - startY;//滑动的距离
    //inner.style.webkitTransform = 'translate(' + 0+ 'px, ' + y + 'px)';  //也可以用css3的方式
    inner.style.left = aboveX + x + "px"; //这一句中的
}

function touchEnd(e) {//手指离开屏幕
    e.preventDefault();
    aboveX = parseInt(inner.style.left);//touch结束后记录内部滑块滑动的位置 在全局变量中体现 一定要用parseInt()将其转化为整数字;
}
document.getElementById("seckillul").addEventListener('touchstart', touchSatrt, false);
document.getElementById("seckillul").addEventListener('touchmove', touchMove, false);
document.getElementById("seckillul").addEventListener('touchend', touchEnd, false);

//特惠倒计时
function getRTime() {
    var EndTime = new Date('2015/12/20 10:00:00');
    var NowTime = new Date();
    var t = EndTime.getTime() - NowTime.getTime();
    /*var d=Math.floor(t/1000/60/60/24);
    t-=d*(1000*60*60*24);
    var h=Math.floor(t/1000/60/60);
    t-=h*60*60*1000;
    var m=Math.floor(t/1000/60);
    t-=m*60*1000;
    var s=Math.floor(t/1000);*/

    var d = Math.floor(t / 1000 / 60 / 60 / 24);
    var h = Math.floor(t / 1000 / 60 / 60 % 24);
    var m = Math.floor(t / 1000 / 60 % 60);
    var s = Math.floor(t / 1000 % 60);

    document.getElementById("t_d").innerHTML = d + "天";
    document.getElementById("t_h").innerHTML = h + "时";
    document.getElementById("t_m").innerHTML = m + "分";
    document.getElementById("t_s").innerHTML = s + "秒";
}
setInterval(getRTime, 1000);