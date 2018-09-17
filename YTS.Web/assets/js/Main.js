// 自定义的Main()“构造函数”
function Main() {
	// 使用的如名的js文件
	var FileZ=new ZRQWindow(); // 文件路径：/js/ZRQWindow.js
	// FileZ.AddHeadJsFile("../js/PagePub.js");
	var FilePub=new PagePub(); // 文件路径：/js/PagePub.js
	// 调用模块对象
	var OjModulePage=new ModulePage();// 文件路径：/js/ModulePage.js

	// 页面加载完之后执行的代码
	this.MainReady=MainReady;
	function MainReady() {
		FilePub.SetModulePageSize();
		FilePub.SetPubNavList();
		FilePub.RightButtonListFunction();

		// 页面模块事件
		OjModulePage.ModulePageReady();

		// 两个按钮的事件
		var upbtn=$(".prbll_upBtn");
		upbtn.click(function() {
			OjModulePage.MP_MoveEvent(-1);
		});
		var downbtn=$(".prbll_downBtn");
		downbtn.click(function() {
			OjModulePage.MP_MoveEvent(1);
		});

		// 匿名创建ZRQWindow文件里的ZRQWindow的对象，设置span的自适应行高
		FileZ.SetSpanLineHeight("span");
		FileZ.SetSpanLineHeight("strong");
	}
	// 当页面尺寸更改之后
	this.MainResize=MainResize;
	function MainResize() {
		FilePub.SetModulePageSize()
		FilePub.SetRightButtonListPosition();
		OjModulePage.MP_MoveEvent(0,false);
	}
}