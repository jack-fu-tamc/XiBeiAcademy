$(document).ready(function() {
	$("#select_car").click(function() {
		$("#show_car").show();
	});
	$('.aui_close').click(function() {
		$("#show_car").hide();
	});
	$('.hc_hidecate li').click(function() {
		$("#show_car").hide();
	});
	$("#hc_cycata").click(function() {
		$("#hc_lodcar").show();
		$("#hc_lodcate").hide();
		$("#hc_wordcarcae").hide();

	});
	$("#hc_kacata").click(function() {
		$("#hc_lodcate").show();
		$("#hc_wordcarcae").hide();
		$("#hc_lodcar").hide();
		$("#hc_lodcar").removeClass("hover");
	});
	$("#hc_kecata").click(function() {
		$("#hc_wordcarcae").show();
		$("#hc_lodcar").hide();
		$("#hc_lodcate").hide();
	});
});

function ctrlDisplay(obj) {
		switch (obj) {
			case "1":
				$("#hc_cycata").addClass("hover");
				$("#hc_kacata").removeClass("hover");
				$("#hc_kecata").removeClass("hover");
				break;
			case "2":
				$("#hc_cycata").removeClass("hover");
				$("#hc_kacata").addClass("hover");
				$("#hc_kecata").removeClass("hover");
				break;
			case "3":
				$("#hc_cycata").removeClass("hover");
				$("#hc_kacata").removeClass("hover");
				$("#hc_kecata").addClass("hover");
				break;
		}
	}
	//下拉选择车型
$(function() {

	$(".hc_nav p").click(function() {

		var _this = this;
		var ul = $(_this).next();
		if (ul.css("display") == "none") {
			ul.slideDown("fast");
		} else {
			ul.slideUp("fast");
		}
	});

	$(".hc_nav li").click(function() {
		var _this = this;
		var Curentp = $(_this).parent().prev();
		var text = $(_this).text();
		Curentp.text(text);
		$(".hc_new").hide();
		$("p").removeClass("select");
		var that = $(this);
		var ele = that.parent().parent();
		var index = ele.index();

		switch (index) {
			case 0:
				{
					$("#hc_catacr1").text(text);
					$("#hc_mycate1").text(text);
					break;
				}
			case 1:
				{
					$("#hc_catacr2").text(text);
					$("#hc_mycate2").text(text);
					break;
				}
			case 2:
				{
					$("#hc_catacr3").text(text);
					$("#hc_mycate3").text(text);
					break;
				}
		}
	});
});
//楼层滑动样式
$(document).ready(function() {
	var $tab_li = $('.divtab ul li');
	$tab_li.hover(function() {
		$(this).addClass('selected').siblings().removeClass('selected')
		var index = $tab_li.index(this);
		$
		$('div.tab_box > .list_cont_one').eq(index).show().siblings().hide();
	});
	//初始化
	var firstLi = $("li[name=widli]")[0];
	sortms(firstLi);
});
//字母筛选

if (!String.prototype.trim) { //判断下浏览器是否自带有trim()方法
	String.prototype.trim = function() {
		return this.replace(/(^\s*)|(\s*$)/g, "");
	}
}

//1F字母排序
var browser = navigator.appName;
if (browser == "Microsoft Internet Explorer") {
	var b_version = navigator.appVersion;
	var version = b_version.split(";");
	var trim_Version = version[1].replace(/[ ]/g, "");
}

//IE7  兼容

function searchIndex(str, j) {
		var strArry = str.split("");
		for (var i = 0; i < strArry.length; i++) {
			if (strArry[i].toString() === j.toString()) {
				alert(i);
				return i;
			}
		}
	}
	//选择字母

function sortms(ele) {
		var val = $(ele).text();
		var newInfo = getCarCategoryDate(val.toLowerCase());
		//颜色
		var lis = $("li[name=widli]");
		for (var i = 0; i < lis.length; i++) {
			$(lis[i]).css({
				"color": "#000",
				"backgroundColor": "#fff"
			});
		}
		var obj = $(ele);
		obj.css({
			"color": "#fff",
			"backgroundColor": "#c20201"
		});
		$(".hc_listimg").html(newInfo);
	}
	//模拟后台数据

function getCarCategoryDate(e) {
		var str = "";
		var cates = ["aotuo", "benchi", "bentian", "dazhong", "changle", "wuling"];
		for (var p = 0; p < cates.length; p++) {
			//<!--IE7-->
			if (browser == "Microsoft Internet Explorer" && trim_Version == "MSIE7.0") {
				var po = cates[p].indexOf(e.trim());
				if (po == 0) {
					str = str + "<a href='#'><img src=publicimg/" + cates[p] + ".jpg /></a>";
				}
			} //IE7if end
			else {

				var po = cates[p].indexOf(e.trim());
				if (po == 0) {
					str = str + "<a href='#'><img src=publicimg/" + cates[p] + ".jpg /></a>";
				}
			} //else end

		} //for end

		return str;
	}
	//展开收起

function domelisr1() {
	var lilist1 = document.getElementById("lilist1");
	lilist1.style.display = (lilist1.style.display == "none" ? "" : "none");
	var lilist2 = document.getElementById("lilist2");
	lilist2.style.display = (lilist2.style.display == "none" ? "" : "none");
	$(".hc_listimg").slideToggle("slow");
}

function domelisr2() {
	var lilist1 = document.getElementById("lilist1");
	lilist1.style.display = (lilist1.style.display == "none" ? "" : "none");
	var lilist2 = document.getElementById("lilist2");
	lilist2.style.display = (lilist2.style.display == "none" ? "" : "none");
	$(".hc_listimg").slideToggle("slow");
}

//导航搜索下拉js
$(function() {
	$(".select").each(function() {
		var s = $(this);
		var z = parseInt(s.css("z-index"));
		var dt = $(this).children("dt");
		var dd = $(this).children("dd");
		var _show = function() {
			dd.slideDown(200);
			dt.addClass("cur");
			s.css("z-index", z + 1);
		}; //展开效果
		var _hide = function() {
			dd.slideUp(200);
			dt.removeClass("cur");
			s.css("z-index", z);
		}; //关闭效果
		dt.click(function() {
			dd.is(":hidden") ? _show() : _hide();
		});
		dd.find("a").click(function() {
			dt.html($(this).html());
			_hide();
		}); //选择效果（如需要传值，可自定义参数，在此处返回对应的“value”值 ）
		$("body").click(function(i) {
			!$(i.target).parents(".select").first().is(s) ? _hide() : "";
		});
	})
});

