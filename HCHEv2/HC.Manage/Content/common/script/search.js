

function searchService(province) {
    var type = $(".select").find("dt").html();
    if (type == "服务")
        location.href = "/Area/AreaPage?province=" + province + "&name=" + $("#txtName").val();
    else
        location.href = "/Area/AreaPage?province=" + province + "&corpcomp=" + $("#txtName").val();
}






//卖家中心头部 安卓
$(function () {
    $(".hc_topleftphone").hover(function () { $(this).css("background", "rgb(91,91,91)"); $(".hc_slidePhone").show(); }, function () { $(".hc_slidePhone").hide(); $(this).css("background", "rgb(0,0,0)"); })
})