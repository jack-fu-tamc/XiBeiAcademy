//页面全局搜索代码
$(function() {
	$(".bodys p").not(":first").hide();
	$(".searchbox ul li").click(function() {
		var index = $(this).index();
		if (index == 0) {
			$(this).find("a").addClass("style1");
			$("li").find("a").removeClass("style2");
		}
		if (index == 1) {
			$(this).find("a").addClass("style2");
			$("li").find("a").removeClass("style1");
		}
		var index = $(this).index();
		$(".bodys p").eq(index).show().siblings().hide();
	});
});



// 首页幻灯js

$(function() {
	var numpic = $('#slides li').size() - 1;
	var nownow = 0;
	var inout = 0;
	var TT = 0;
	var SPEED = 5000;


	$('#slides li').eq(0).siblings('li').css({
		'display': 'none'
	});


	var ulstart = '<ul id="pagination">',
		ulcontent = '',
		ulend = '</ul>';
	ADDLI();
	var pagination = $('#pagination li');
	var paginationwidth = $('#pagination').width();
	$('#pagination').css('margin-left', (470 - paginationwidth))

	pagination.eq(0).addClass('current')

	function ADDLI() {
		//var lilicount = numpic + 1;
		for (var i = 0; i <= numpic; i++) {
			ulcontent += '<li>' + '<a href="#">' + (i + 1) + '</a>' + '</li>';
		}

		$('#slides').after(ulstart + ulcontent + ulend);
	}

	pagination.on('click', DOTCHANGE)

	function DOTCHANGE() {

		var changenow = $(this).index();

		$('#slides li').eq(nownow).css('z-index', '-1000');
		$('#slides li').eq(changenow).css({
			'z-index': '-1800'
		}).show();
		pagination.eq(changenow).addClass('current').siblings('li').removeClass('current');
		$('#slides li').eq(nownow).fadeOut(400, function() {
			$('#slides li').eq(changenow).fadeIn(500);
		});
		nownow = changenow;
	}

	pagination.mouseenter(function() {
		inout = 1;
	})

	pagination.mouseleave(function() {
		inout = 0;
	})

	function GOGO() {

		var NN = nownow + 1;

		if (inout == 1) {} else {
			if (nownow < numpic) {
				$('#slides li').eq(nownow).css('z-index', '-1000');
				$('#slides li').eq(NN).css({
					'z-index': '-1800'
				}).show();
				pagination.eq(NN).addClass('current').siblings('li').removeClass('current');
				$('#slides li').eq(nownow).fadeOut(400, function() {
					$('#slides li').eq(NN).fadeIn(500);
				});
				nownow += 1;

			} else {
				NN = 0;
				$('#slides li').eq(nownow).css('z-index', '-1000');
				$('#slides li').eq(NN).stop(true, true).css({
					'z-index': '-1800'
				}).show();
				$('#slides li').eq(nownow).fadeOut(400, function() {
					$('#slides li').eq(0).fadeIn(500);
				});
				pagination.eq(NN).addClass('current').siblings('li').removeClass('current');

				nownow = 0;

			}
		}
		TT = setTimeout(GOGO, SPEED);
	}

	TT = setTimeout(GOGO, SPEED);

});

//<!--侧边导航定位脚本-->
$(window).scroll(function() {
	var scrollTop = $(document).scrollTop();
	var documentHeight = $(document).height();
	var windowHeight = $(window).height();
	var contentItems = $("#content").find(".item");
	var currentItem = "";

	if (scrollTop + windowHeight == documentHeight) {
		currentItem = "#" + contentItems.last().attr("id");
	} else {
		contentItems.each(function() {
			var contentItem = $(this);
			var offsetTop = contentItem.offset().top;
			if (scrollTop > offsetTop - 50) { //此处的200视具体情况自行设定，因为如果不减去一个数值，在刚好滚动到一个div的边缘时，菜单的选中状态会出错，比如，页面刚好滚动到第一个div的底部的时候，页面已经显示出第二个div，而菜单中还是第一个选项处于选中状态
				currentItem = "#" + contentItem.attr("id");
			}
		});
	}
	if (currentItem != $("#menu").find(".current").attr("href")) {
		$("#menu").find(".current").removeClass("current");
		$("#menu").find("[href=" + currentItem + "]").addClass("current");
	}
});

function gotop() {
		$('body,html').animate({
			scrollTop: 0
		}, 400);
	}
	//<!-- /侧边导航 -->
	//定位楼层

function gotofloor(thiz) {
	var pos = $("#" + thiz).offset().top; // 获取该点到头部的距离
	$("html,body").animate({
		scrollTop: pos - 60
	}, 400);
}