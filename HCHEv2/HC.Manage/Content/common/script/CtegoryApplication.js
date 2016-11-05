$(document).ready(function () {
    var $rad_li = $('.hc_roideli .hc_radiodiv .pay_list_c1');
    
    $('.hc_roideli .hc_radiodiv .pay_list_c1').click(function () {
        $($rad_li).each(function(i,v){
            $(v).removeClass('rabuit');
        })
        $(this).addClass('rabuit');
    })

});
