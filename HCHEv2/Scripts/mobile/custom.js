var webmenui = "";
webmenui =webmenui+"<nav class=\"drawer-nav\" role=\"navigation\">";
webmenui = webmenui + "<div class=\"drawer-brand\">"; 
webmenui = webmenui + "<p class=\"drawer-nav-title\"><a href=\"/\">首页</a></p>";
webmenui = webmenui + "<p class=\"drawer-nav-title\"><a href=\"/Information/subNewsClassMobile/13\">关于崇信</a></p>";
webmenui = webmenui + "<p class=\"drawer-nav-title\"><a href=\"/Information/subNewsClassMobile/1\">新闻资讯</a></p>";
webmenui = webmenui + "<p class=\"drawer-nav-title\"><a href=\"/Information/productCenterMobile\">产品中心</a></p>";
webmenui = webmenui + "<p class=\"drawer-nav-title\"><a href=\"/Information/subNewsClassMobile/7\">招商加盟</a></p>";
webmenui = webmenui + "<p class=\"drawer-nav-title\"><a href=\"/Information/subNewsClassMobile/37\">战略合作</a></p>";
webmenui = webmenui + "<p class=\"drawer-nav-title\"><a href=\"/Information/subNewsClassMobile/29\">人力资源</a></p>";
webmenui = webmenui + "<p class=\"drawer-nav-title\"><a href=\"/Information/subNewsClassMobile/32\">客户服务</a></p>";
webmenui =webmenui+"</div></nav>";

var imenu = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">      <tr>        <td class=\"a1\"><a href=\"index2.html\"><b></b><span>关于崇信</span></a></td>        <td class=\"a2\"><a href=\"index3.html\"><b></b><span>新闻资讯</span></a></td>        <td class=\"a3\"><a href=\"index4.html\"><b></b><span>产品中心</span></a></td>        <td class=\"a4\"><a href=\"index5.html\"><b></b><span>招商加盟</span></a></td>    <td class=\"a5\"><a href=\"index6.html\"><b></b><span>战略合作</span></a></td>        <td class=\"a5\"><a href=\"index7.html\"><b></b><span>人力资源</span></a></td>        <td class=\"a6\"><a href=\"index8.html\"><b></b><span>客户服务</span></a></td>      </tr>    </table>";

var Copystr="<div class=\"footerWrapper\"><div class=\"copyrightWrapper\"><span class=\"copyright\"> Copyright &copy; 2016 西安崇信生物科技有限公司 版权所有</span><a class=\"backToTopButton\"></a></div></div>";

var toolbarstr="<h4 class=\"blockTitle\">快捷操作:</h4><p><a href=\"yd.html\" class=\"buttonWrapper buttonGreen buttonCheckmark\">在线</a> <a href=\"zixun.html\" class=\"buttonWrapper buttonBlue buttonArrowRight\">在线咨询</a> <a href=\"contact.html\" class=\"buttonWrapper buttonPink buttonLocation\">地理位置</a></p>";

var $ = jQuery.noConflict();
jQuery(document).ready(function($){
				
	// initial settings start
	var mainMenuStatus = 'closed';
	var mainMenuAnimation = 'complete';
	var headerHeight = $('.headerOuterWrapper').height();
	var mainMenuHeight = $('.mainMenuWrapper').height() + 3;
	
	$('.mainMenuWrapper').css('top', -mainMenuHeight + headerHeight);
	$('.websiteWrapper').css('min-height', mainMenuHeight + headerHeight);
	
	var windowWidth = $(window).width() - 48;
		
	var lightboxInitialWidth = windowWidth;
	var lightboxInitialHeight = 220;
	// initial settings end


     
	// main menu functions start
	$('.mainMenuButton').click(function(){
		
		mainMenuHeight =  $('.mainMenuWrapper').height() + 3;
		
		if(mainMenuStatus == 'closed' && mainMenuAnimation == 'complete'){
			
			mainMenuAnimation = 'incomplete';
			$('.mainMenuWrapper').css('display', 'block');
			$('.mainMenuWrapper').stop(true, true).animate({top: headerHeight}, 600, 'easeOutQuart', function(){mainMenuStatus = 'open'; mainMenuAnimation = 'complete'});
			
		}else if(mainMenuStatus == 'open' && mainMenuAnimation == 'complete'){
			
			mainMenuAnimation = 'incomplete';
			$('.mainMenuWrapper').stop(true, true).animate({top: -mainMenuHeight + headerHeight}, 600, 'easeInQuart', function(){mainMenuStatus = 'closed'; mainMenuAnimation = 'complete'; $('.mainMenuWrapper').css('display', 'none'); });
		
		};
		
		return false;
	});	
	// main menu functions end	
	
	
	
	// adapt portfolio function starts
	function adaptPortfolio(){
		
		$('.portfolioTwoWrapper').css('width', $('.portfolioTwoPageWrapper').width() - 10);
		$('.portfolioTwoFilterableWrapper .portfolioFilterableItemsWrapper').css('width', $('.portfolioTwoFilterablePageWrapper').width() - 10);
		$('.recentProjects').css('width', $('.recentProjectsOuterWrapper').width() - 0);
		$('.recentProjects2').css('width', $('.recentProjectsOuterWrapper2').width()- 0);
		$('.recentProjects3').css('width', $('.recentProjectsOuterWrapper3').width()- 0);
		
		var portfolioTwoItemWidth = ($('.portfolioTwoPageWrapper').width() - 0)/4;
		var portfolioTwoFilterableItemWidth = ($('.portfolioTwoFilterablePageWrapper').width() - 0)/4;
		var recentProjectItemWidth = ($('.recentProjectsOuterWrapper').width() - 0)/4;
		var recentProjectItemWidth2 = ($('.recentProjectsOuterWrapper2').width() - 0)/3;
		var recentProjectItemWidth3 = ($('.recentProjectsOuterWrapper3').width() - 0)/4;
		
		$('.portfolioTwoItemWrapper').css('width', portfolioTwoItemWidth);
		$('.portfolioTwoFilterableWrapper .portfolioFilterableItemWrapper').css('width', portfolioTwoFilterableItemWidth);
		$('.recentProject').css('width', recentProjectItemWidth);
		$('.recentProject2').css('width', recentProjectItemWidth2);
		$('.recentProject3').css('width', recentProjectItemWidth3);
		
	};
	
	adaptPortfolio();
	// adapt portfolio function ends
	
	
	
	// filterable portfolio functions start
	$('#portfolioMenuWrapper > li > a').click(function(){
		
		var filterVal = $(this).attr('data-type');
		
		if(filterVal != 'all'){
			
			$('.currentPortfolioFilter').removeClass('currentPortfolioFilter');
			
			$(this).addClass('currentPortfolioFilter');
			
			$('.portfolioFilterableItemWrapper').each(function(){
	            
				var itemCategories = $(this).attr("data-type").split(",");
				  
				if($.inArray(filterVal, itemCategories) > -1){
					
					$(this).addClass('filteredPortfolioItem');
					
					$('.filteredPortfolioItem').stop(true, true).animate({opacity:1}, 300, 'easeOutCubic');
					
				}else{
						
					$(this).removeClass('filteredPortfolioItem');
					
					if(!$(this).hasClass('filteredPortfolioItem')){
						
						$(this).stop(true, true).animate({opacity:0.3}, 300, 'easeOutCubic');
					
					};
					
				};
					
			});
		
		}else{
			
			$('.currentPortfolioFilter').removeClass('currentPortfolioFilter');
			
			$(this).addClass('currentPortfolioFilter');
			
			$('.filteredPortfolioItem').removeClass('filteredPortfolioItem');
			
			$('.portfolioFilterableItemWrapper').stop(true, true).animate({opacity:1}, 300, 'easeOutCubic');
			
		}
			
		return false;
	
	});
	// filterable portfolio functions end
	
	
	
	// alert box widget function starts
	$('.alertBoxButton').click(function(){
		
		$(this).parent().fadeOut(300, function(){$(this).remove();});
		
		return false;
		
	});
	// alert box widget function ends
	
	
	
	// accordion widget function starts
	$('.accordionButton').click(function(e){
		 
		if($(this).hasClass('currentAccordion')){
			
			 $(this).parent().find('.accordionContentWrapper').stop(true, true).animate({height:'hide'}, 300, 'easeOutCubic', function(){$(this).parent().find('.accordionButton').removeClass('currentAccordion');});
			 
		}else{
			 
			$(this).parent().find('.accordionContentWrapper').stop(true, true).animate({height:'show'}, 300, 'easeOutCubic', function(){$(this).parent().find('.accordionButton').addClass('currentAccordion');});
		 
        };
		 
		return false;
		
	});
	// accordion widget function ends

	
	
	// back to top function starts
	$('.backToTopButton').click(function(){
								   
	    $('body, html').stop(true, true).animate({scrollTop:0}, 1200,'easeOutCubic'); 
		
		return false;
	
    });
	// back to top function ends 
	
	
	
	// window resize functions start
	$(window).resize(function(){
		
		windowWidth = $(window).width() - 48;
		
		lightboxInitialWidth = windowWidth;
		
		lightbox();
					
		adaptPortfolio();
				
	});
	// window resize functions end
	
	
	
	// nivo slider functions start
	$('#mainSlider').nivoSlider({
		
		controlNav: false,
		prevText: '',
        nextText: '' 
		
	});
	// nivo slider functions end
	
	
	
	// lightbox functions start
	function lightbox(){
		
		$('.portfolioOneExpandButton, .portfolioFilterableExpandButton, .singleProjectExpandButton, .portfolioOneExpandButtonimg').colorbox({
		
			maxWidth: windowWidth,
			initialWidth: lightboxInitialWidth,
			initialHeight: lightboxInitialHeight
			
		});
		
	};
	
	lightbox();
	// lightbox functions end



});