<link rel="stylesheet" type="text/css" href="/scripts/roll_text/css.css">
<script type="text/javascript">
				<!--
				var rollTextB_k=5; //菜单总数
				var rollTextB_i=1; //菜单默认值
				//setFocus1(0);
				rollTextB_tt=setInterval("rollTextB(1)",88000);
				function rollTextB(a){
				clearInterval(rollTextB_tt);
				rollTextB_tt=setInterval("rollTextB(1)",88000);
				rollTextB_i+=a;
				if (rollTextB_i>rollTextB_k){rollTextB_i=1;}
				if (rollTextB_i==0){rollTextB_i=rollTextB_k;}
				//alert(i)
				 for (var j=1; j<=rollTextB_k; j++){
				 document.getElementById("rollTextBMenu"+j).style.display="none";
				 }
				 document.getElementById("rollTextBMenu"+rollTextB_i).style.display="block";
				 document.getElementById("pageShow").innerHTML = rollTextB_i+"/"+rollTextB_k;
				} 
				//-->
</script>


<div class="roll_text_b">
  <p class="roll_text_button"><a class="a1" title="上一条" href="javascript:rollTextB(-1);"></a><a class="a2" title="下一条" href="javascript:rollTextB(1);"></a></p>
  <p class="roll_text_number" id="pageShow">1/5</p>
  <div class="roll_text_list">
    <ul>
      <li id="rollTextBMenu1" style="display: block">西安彩印标牌网业务已经覆盖西北近百个大中城市西安彩印标牌网业务已经覆盖西北近百个大中城市<span>07/23</span></li>
	  <li id="rollTextBMenu2" style="display: none">商协会各位领导以及宁波市电子商务企1业家们<span>07/23</span></li>
	  <li id="rollTextBMenu3" style="display: none">领导以及宁波网商协会各位领导以及宁波市电子商2务企业家们<span>07/23</span></li>
	  <li id="rollTextBMenu4" style="display: none">会各位领导以及宁波网商协宁波市电子商3务企业家们<span>07/23</span></li>
	  <li id="rollTextBMenu5" style="display: none">宁波网商协会各位领导以及宁波市电4子商务企业家们<span>07/23</span></li>
    </ul>
  </div>
  <div class="clr"></div>
</div>