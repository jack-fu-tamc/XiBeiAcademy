<link rel="stylesheet" type="text/css" href="/scripts/banner/css.css" />
<script type="text/javascript" src="/scripts/banner/js1.js"></script>
<script type="text/javascript" src="/scripts/banner/js2.js"></script>
<div class="silbo-slide_02">
<div id="SlidePlayer_02">
    <ul class="Slides">
        <li title="标题"><a href="#"><img src="/images/ls/ls_04.jpg" alt="标题" /></a></li>
        <li title="标题"><a href="#"><img src="/images/ls/ls_05.jpg" alt="标题" /></a></li>
        <li title="标题"><a href="#"><img src="/images/ls/ls_04.jpg" alt="标题" /></a></li>
    </ul>
</div>
<script type="text/javascript">
TB.widget.SimpleSlide.decorate('SlidePlayer_02', {eventType:'mouse', effect:'fade', onInit:function(){
		var lis = $D.getElementsByClassName('Slides', 'ul', $D.get('SlidePlayer_02'))[0].getElementsByTagName('li'),
			titles = [], hrefs = [];
		for (var i=0; i<lis.length; i++) {
			var el, innerEl;
			el = document.createElement('div');
			el.className = 'SlideTitle';
			el.innerHTML = lis[i].getAttribute('title')+'<b></b>';
			lis[i].appendChild(el);
		}
	}});
</script>
</div>