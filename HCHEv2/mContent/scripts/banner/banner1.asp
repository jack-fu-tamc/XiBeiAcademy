<link rel="stylesheet" type="text/css" href="/scripts/banner/css.css" />
<script type="text/javascript" src="/scripts/banner/js1.js"></script>
<script type="text/javascript" src="/scripts/banner/js2.js"></script>
<div class="block_silbo">
<div class="silbo-slide">
<div id="SlidePlayer">
    <ul class="Slides">
        <li title="标题" style="background:url(/images/ls/ls_04.jpg) no-repeat center top"><a href="1"><img src="/images/button_00.gif" alt="标题" /></a></li>
        <li title="标题" style="background:url(/images/ls/ls_04.jpg) no-repeat center top"><a href="1"><img src="/images/button_00.gif" alt="标题" /></a></li>
        <li title="标题" style="background:url(/images/ls/ls_04.jpg) no-repeat center top"><a href="1"><img src="/images/button_00.gif" alt="标题" /></a></li>
        <li title="标题" style="background:url(/images/ls/ls_04.jpg) no-repeat center top"><a href="1"><img src="/images/button_00.gif" alt="标题" /></a></li>
        <li title="标题" style="background:url(/images/ls/ls_04.jpg) no-repeat center top"><a href="1"><img src="/images/button_00.gif" alt="标题" /></a></li>
    </ul>
</div>
<script type="text/javascript">
TB.widget.SimpleSlide.decorate('SlidePlayer', {eventType:'mouse', effect:'fade', onInit:function(){
		var lis = $D.getElementsByClassName('Slides', 'ul', $D.get('SlidePlayer'))[0].getElementsByTagName('li'),
			titles = [], hrefs = [];
		for (var i=0; i<lis.length; i++) {
			var el, innerEl;
			el = document.createElement('div');
			el.className = 'SlideTitle';
			el.innerHTML = '<b></b>';
			lis[i].appendChild(el);
		}
	}});
</script>
</div>
</div>