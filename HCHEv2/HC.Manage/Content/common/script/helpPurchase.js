$(function () {
    var $tab_li = $('.select_type .color_next');
    $tab_li.click(function () {
        $(this).addClass('type_show').siblings().removeClass('type_show')
        var index = $tab_li.index(this);
        if (index == 0) {
            $("#PurchaseList").show();
            $("#myPurchase").hide();
            $("#PurchaseSupply").hide();
        }else if (index == 1) {
            $("#PurchaseList").hide();
            $("#myPurchase").show();
            $("#PurchaseSupply").hide();
        }
        else {
            $("#PurchaseList").hide();
            $("#myPurchase").hide();
            $("#PurchaseSupply").show();
        }
    });
});