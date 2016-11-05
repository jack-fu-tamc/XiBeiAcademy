///wap/js/路径修改
window.onload = function(){
	var win = window;
	var doc = document;
	var timeGo = function(temp){
	   return function() {
		  location.href=getHref(temp);
	   }
	}
	var adParam;
	function GetRequest() {
		var url = location.search;
		var theRequest = {};
		var str;
		var strs;
		var i;
		if (url.indexOf("?") != -1) {
			str = url.substr(1);
			strs = str.split("&");
			for(i = 0; i < strs.length; i++) {
				str = strs[i].split('=');
				if( str[0] ){
					theRequest[str[0]] = str[1] || '';
				}
			}
		 }
		 return theRequest;
	}
	var bindLogEvent = function(divid, type) {
		var elem = doc.getElementById(divid);
		if (elem) {
			var temp = elem.onclick ? elem.onclick.toString() :'';
			elem.onclick = "return false";
			addListener(elem, "click",  function() {
				setTimeout(timeGo(temp),100);
			});
		}
	};
	var getCountNum = function(type){
		if(type =='phone')return 2;
		else if(type == 'sms') return 4;
		else if(type == 'map') return 3;
		else if(type == 'online') return 1;
		else return 5;	
	}
	var log = function(url,got) {
		var img = new Image();
		var key = 'log_' + Math.floor(Math.random() * 0x80000000);
		win[key] = img;
		img.onload = img.onerror = img.onabort = function() {
			img.onload = img.onerror = img.onabort = null;
			win[key] = img = null;
		};
		img.src = url;
	};
	allHrefAppendParam();
	win.dynamicMap && head.js('/js/jquery-1.7.2.min.js',dynamicMap );
	function buildClickParam( href ){
		if( !href ) return false;
		var pos_0 = href.indexOf('location.href=');
		var pos_1 = href.lastIndexOf( '.html' );
		if( pos_0 < 0 || pos_1 < 0 ) return false;
		var pos_2 = href.lastIndexOf( '?' );
		var pos_3 = href.lastIndexOf( '#' );
		var pos_4 = href.lastIndexOf( "'" );
		var hash = '';
		if( pos_3 > 0 ){
			hash = href.substr(pos_3);
			href = href.substr(0,pos_3);
		}else{
			hash = href.substr(pos_4);
			href = href.substr(0,pos_4);
		}
		if( pos_2 > 0 ){
			href += '&' + adParam;
		}else{
			href += '?' + adParam;
		}
		return href + hash;
	}
    function buildHrefParam( href ){
        if( !href ) return false;
        var pos_0 = href.indexOf('.html');
        var pos_1 = href.indexOf('?' );
        var pos_2 = href.indexOf('#' );
        var hash = '';
        if( pos_0 == -1 ) return false;
        if( pos_2 > 0 ){
            hash = href.substr(pos_2);
            href = href.substr(0,pos_2);
        }
        if( pos_1 > 0 ){
            href = href + '&' + adParam;
        }else{
            href = href + '?' + adParam;
        }
        return href + hash;
    }
	function hrefAppendParam( dom ){
		var h = buildHrefParam( dom.getAttribute('href') );
		h && dom.setAttribute('href' , h );
	}
	function clickAppendParam( dom ){
		var e = buildClickParam( dom.getAttribute('onclick') );
		e && dom.setAttribute('onclick' , e );
	}
	function allDivAppendParam(){
		var $div = doc.getElementsByTagName('div');
		var len = $div.length;
		var i;
		for(i=0;i<len;i++){
			if( !$div[i] || !$div[i].onclick ) continue;
			clickAppendParam($div[i]);
		}
	}
	function allAnthorAppendParam(){
		var $a = doc.getElementsByTagName('a');
		var len = $a.length;
		var i;
		for(i=0;i<len;i++){
			if( !$a[i] || (!$a[i].onclick && !$a[i].href) ) continue;
			clickAppendParam( $a[i] );
			hrefAppendParam( $a[i] );
		}
	}
	function allHrefAppendParam(){
		var query   = GetRequest();
		var key     = ['srchid' , 'uid' , 'shwrk' , 'cmatch' , 'w' ];
		var keyLen  = key.length;
		var i,j,k;
		var jump = query.jump ? (parseInt(query.jump)+1) : 1;
		var p = '';
		if(query.ecom_ad != '' ) return;
		for(i=0;i<keyLen;i++){
			k = key[i];
			if( !query[k] ) return;
			p = p + k + '=' + query[k] + '&';
		}
        win.adParam = adParam = p + 'ecom_ad&jump=' + jump;
		allDivAppendParam();
		allAnthorAppendParam();
	}
}
