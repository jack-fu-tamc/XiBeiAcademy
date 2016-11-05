function huadongmen(val,n,anniu,neirong)
{
	var objanniu,objneirong;
	for(var i=1;i<=n;i++)
	{   
	    objanniu=anniu+i;
		objneirong=neirong+i;
		document.getElementById(objanniu).className="";
		document.getElementById(objneirong).style.display="none";
		if(i==val)
		{
			document.getElementById(objanniu).className="on";
			document.getElementById(objneirong).style.display="block";
		}
	}
	
}