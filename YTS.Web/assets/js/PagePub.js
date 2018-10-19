/*
	页面公共部分的js文件“类”
	主要用于给公共部分进行设置或赋值
	如：页面顶部导航栏，页面右侧的悬浮框
*/
function PagePub() {
	// 使用的如名的js文件
	var FileZ=new ZRQWindow(); // 文件路径：/js/ZRQWindow.js

	// var FileMP=new ModulePage();

	// 设置模块页面的高度和宽度
	this.SetModulePageSize=SetModulePageSize;
	function SetModulePageSize() {
		var b=$(".ModulePage");
		b.css("width",FileZ.get_windowWidth()+"px");
		b.css("height",FileZ.get_windowHeight()+"px");
		console.log("窗口宽度为："+FileZ.get_windowWidth());
		console.log("窗口高度为："+FileZ.get_windowHeight());
	}

	// 给公共的导航栏列表的各个内容赋值。来源自网页的各个页面
	this.SetPubNavList=SetPubNavList;
	function SetPubNavList() {
		var navbox=$(".pub_Nav_list");
		var moudelName=$(".pub_Nav_Name");

		moudelName.each(function() {
			var value=$(this).children("span").text();
			navbox.append("<li><span>"+value+"</span></li>")
		});

		var ojnbLI=navbox.children("li");
		ojnbLI.each(function() {
			// var len=
		});
	}
	// 导航栏的resize()事件
	function SetPubNavListResize() {}

	// 设置页面右边悬浮框的事件
	this.RightButtonListFunction=RightButtonListFunction;
	function RightButtonListFunction() {
		/*
			.pub_rbl_list // 装所有的li的“内容”盒子 实现功能是：往右边隐藏
			.pub_rbl_list_close // 是用于关闭.pub_rbl_list 的属性

			.pub_ShrinkButton // 右边的叉号的按钮
			.pub_ShrinkButton_off // 用于将叉号变为加号
		*/
		var k=$(".pub_rbl_list");
		var b=$(".pub_ShrinkButton");
		b.click(function() {
			// 判断是不是加号，代表“内容”盒子关没关闭
			if (!$(this).is(".pub_ShrinkButton_off")) {
				// 没关就要给关上
				$(this).addClass("pub_ShrinkButton_off");
				k.addClass("pub_rbl_list_close");
			} else {
				// 关上了就要打开
				$(this).removeClass("pub_ShrinkButton_off");
				k.removeClass("pub_rbl_list_close");
			}
		});

		// 给“内容盒子的里面的li 一个状态”
		// 这样就可以触发“隐藏”的css了 动态效果通过css的transition属性实现了		
		var l=k.children("li");
		l.mouseover(function() {
			$(this).addClass("pub_rbl_li_active");
		});
		l.mouseout(function() {
			$(this).removeClass("pub_rbl_li_active");
		});

		// 用于设置页面右边悬浮窗的位置的位置或隐藏
		SetRightButtonListPosition();
	}
	// 用于设置页面右边悬浮窗的位置的位置或隐藏
	this.SetRightButtonListPosition=SetRightButtonListPosition;
	function SetRightButtonListPosition() {
		var nav=$(".pub_WebNavTitle");
		var rb=$(".pub_rbl_Show");
		var top=(FileZ.get_windowHeight()-rb.height()-nav.height())/2;
		if (top<50) {
			rb.addClass("hide");
		} else {
			rb.removeClass("hide");
			rb.css("margin-top",top+nav.height()+"px");
		}
		// console.log("浏览器的高度："+FileZ.get_windowHeight());
		// console.log("导航栏的高度："+nav.height());
		// console.log("悬浮框的高度："+rb.height());
		// console.log("计算出来的值的高度："+(top+nav.height()));
	}

/*结束*/
/*=========================================================================================*/
}