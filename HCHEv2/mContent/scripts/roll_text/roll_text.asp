<link rel="stylesheet" type="text/css" href="/scripts/roll_text/css.css">
<script type="text/javascript">
				<!--
				var rollText_k=5; //菜单总数
				var rollText_i=1; //菜单默认值
				//setFocus1(0);
				rollText_tt=setInterval("rollText(1)",8000);
				function rollText(a){
				clearInterval(rollText_tt);
				rollText_tt=setInterval("rollText(1)",8000);
				rollText_i+=a;
				if (rollText_i>rollText_k){rollText_i=1;}
				if (rollText_i==0){rollText_i=rollText_k;}
				//alert(i)
				 for (var j=1; j<=rollText_k; j++){
				 document.getElementById("rollTextMenu"+j).style.display="none";
				 }
				 document.getElementById("rollTextMenu"+rollText_i).style.display="block";
				 document.getElementById("pageShow").innerHTML = rollText_i+"/"+rollText_k;
				} 
				//-->
</script>

<div class="notice">
	<div class="roll_text">
		<p class="roll_text_button"><a class="a1" title="上一条" href="javascript:rollText(-1);"></a><a class="a2" title="下一条" href="javascript:rollText(1);"></a></p>
		<p class="roll_text_number" id="pageShow">1/5</p>
		<div class="roll_text_list">
			<p class="title">通知公告</p>
			<ul>
				<li id="rollTextMenu1" style="display: block"><a href="#">西安彩印标牌网业务已经覆盖西北近百个大中城市西安彩印标牌网业务已经覆盖西北近百个大中城市</a><span>07/23</span></li>
				<li id="rollTextMenu2" style="display: none"><a href="#">商协会各位领导以及宁波市电子商务企1业家们</a><span>07/23</span></li>
				<li id="rollTextMenu3" style="display: none"><a href="#">领导以及宁波网商协会各位领导以及宁波市电子商2务企业家们</a><span>07/23</span></li>
				<li id="rollTextMenu4" style="display: none"><a href="#">会各位领导以及宁波网商协宁波市电子商3务企业家们</a><span>07/23</span></li>
				<li id="rollTextMenu5" style="display: none"><a href="#">宁波网商协会各位领导以及宁波市电4子商务企业家们</a><span>07/23</span></li>
			</ul>
		</div>
		<div class="clr"></div>
	</div>
</div>