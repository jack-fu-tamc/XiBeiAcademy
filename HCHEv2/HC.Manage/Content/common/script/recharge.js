/*控制划过显示js*/
$(document).ready(function () {
    var $tab_li = $('.divtabs ul li');
    $tab_li.hover(function () {
        $(this).addClass('selects').siblings().removeClass('selects')
        var index = $tab_li.index(this);
        $
        $('div.tab_boxs > .list_cont_ones').eq(index).show().siblings().hide();
    });
});

