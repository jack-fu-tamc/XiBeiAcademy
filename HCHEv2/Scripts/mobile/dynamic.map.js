	// 编写自定义函数,创建标注
	var dynMap;
	var points;
    var mapIndex = paramQuery('n') || 0;
	function addMarker(point,pos){
	  var myIcon = new BMap.Icon("http://bs.baidu.com/mobcard-upload-image-to-bcs/%2Fmobi512497a1b3c56?sign=MBO:wEU4w721X2hVAoGbt60VcWCbN4Y70Ls:8USARHrTeF0Sm%2B1MoqWmOP3BcAY%3D", new BMap.Size(22,29));
	  myIcon.setImageOffset(new BMap.Size(-29*pos,0));
	  var marker = new BMap.Marker(point,{icon:myIcon});
	  dynMap.addOverlay(marker);
	  var infoWindow = new BMap.InfoWindow(getInfo(point));
	  infoWindow.disableAutoPan();
	  marker.addEventListener("click", function(){this.openInfoWindow(infoWindow);});
	  if(pos == mapIndex){//默认显示数据
		 dynMap.openInfoWindow(infoWindow, point);
	  }
	}
	function getInfo(point){
		var p_str=point.lng+","+point.lat;
		for (var i= 0; i<=points.length-1; i++){
	 		if(p_str == $(points[i]).attr("value")){
	 			var value = $(points[i]).next().next().attr("value")? $(points[i]).next().next().attr("value"): $(points[i]).next().next().html();
	 			return "<div style='font-size:12px'>"+$(points[i]).next().html()+"<br/>"+value+"</div>";
	 		}
		}
		return "";
	}
	function getRoute(obj){
		var geolocation = new BMap.Geolocation();
		geolocation.getCurrentPosition(function(r){
		    if(this.getStatus() == BMAP_STATUS_SUCCESS){
		    	var poi = $(obj).parent().children().eq(0).attr("value").split(",");
		    	var driving = new BMap.DrivingRoute(dynMap, {renderOptions:{map: dynMap, autoViewport: true}});
				var point = new BMap.Point(parseFloat(poi[0]), parseFloat(poi[1]));
		    	driving.search(r.point, point);
				var gc = new BMap.Geocoder();  //根据point获取省市信息
				gc.getLocation(point, function(rs){
			        var addComp = rs.addressComponents;
			        if (window.parent.P == undefined){//预览//"+addComp.district + ", " + addComp.street + ", " + addComp.streetNumber+"
				    	var api="http://api.map.baidu.com/direction?origin=latlng:"+r.point.lat+","+r.point.lng+"|name:起点&destination=latlng:"+poi[1]+","+poi[0]+"|name:终点&mode=transit&region="+addComp.city+"&output=html";
			    		window.location.href = api;
			     	}
			    });
			}
		    else {
		        alert('获取当前位置失败');
		    }        
		},{enableHighAccuracy: true});
	}
	function limitChar(obj){//对字数限制
		var str = $(obj).next().next().html();
		$(obj).next().next().attr("value",str);
		if(str.length >19){
			$(obj).next().next().html(str.substr(0,19)+"...");
		}
	}
	function disBottomLine(obj){//取消最后一条线
		$(obj).parent().css("border-bottom","0");
	}
    function paramQuery(k){
        var s = location.search.substr(1);
        var a = s.split('&');
        var b;
        var i;
        for(i=0;i<a.length;i++){
            b = a[i].split('=');
            if( b[0] == k ){
                return b[1];
            }
        }
        return '';
    }
	function dynamicMap(){
		points =  $(".mapMarker");
		if(!points) return;
		var map = new BMap.Map("dynamicMap");
		dynMap = map;
        var pointChecker = $(points[mapIndex]);
        if(!pointChecker.get(0) ){
            pointChecker = $(points[0] );
            mapIndex = 0;
        }
		var pointxy0 = pointChecker.attr("value").split(",");
		var point = new BMap.Point(parseFloat(pointxy0[0]), parseFloat(pointxy0[1]));
		map.centerAndZoom(point, 13);
	 	for (var i= 0; i<=points.length-1; i++){
	 		var pointxy = $(points[i]).attr("value").split(",");
			var point = new BMap.Point(parseFloat(pointxy[0]), parseFloat(pointxy[1]));
			addMarker(point,i);
			limitChar(points[i]);
			if(i == points.length-1){
				disBottomLine(points[i]);
			}
		}
	 	var opts = {anchor:BMAP_ANCHOR_BOTTOM_RIGHT,type: BMAP_NAVIGATION_CONTROL_ZOOM} ;   
		map.addControl(new BMap.NavigationControl(opts));
		map.addControl(new BMap.GeolocationControl());  
	 	map.panTo(new BMap.Point(parseFloat(pointxy0[0]), parseFloat(pointxy0[1])));
	}
	function cPoint(obj){
		var pointxy = $(obj).parent().children().eq(0).attr("value").split(",");
		var point = new BMap.Point(parseFloat(pointxy[0]), parseFloat(pointxy[1]));
		dynMap.panTo(point);
		var infoWindow = new BMap.InfoWindow(getInfo(point));
		infoWindow.disableAutoPan();
		dynMap.openInfoWindow(infoWindow, point);
	}
	
