var modulepage_index=0;
/*
	页面模块的"类"
*/
function ModulePage() {
	// 使用的如名的js文件
	var FileZ=new ZRQWindow(); // 文件路径：/js/ZRQWindow.js
	var OjMP,MLength,oj_mainbody,oj_showbox;

	this.ModulePageReady=ModulePageReady;
	function ModulePageReady() {
		OjMP=$(".ModulePage");
		MLength=OjMP.length-1;
		oj_mainbody=$(".MainBody");	
		oj_showbox=$(".MB_ShowBox");
	}

	// 用于执行翻转页面的动作
	this.MP_MoveEvent=MP_MoveEvent;
	function MP_MoveEvent(len,bool) {
		modulepage_index+=len;
		if (modulepage_index<0) {
			modulepage_index=0;
		} else if(modulepage_index>MLength) {
			modulepage_index=MLength;
		}
		if (bool==false) {
			var vatop=(modulepage_index)*FileZ.get_windowHeight();
			oj_showbox.css({
				"margin-top":"-"+vatop+"px"
			});
			return;
		}
		var vatop=(modulepage_index)*FileZ.get_windowHeight();
		oj_showbox.animate({
			marginTop:"-"+vatop+"px"
		},500);
	}
}