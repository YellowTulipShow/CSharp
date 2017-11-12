/*
	网页的侧边导航栏：
	有六个按钮功能：分别是：
		上五个在中间黑色圆角框的灰色按钮
			上的按钮：鼠标点击-页面显示上一页
			电话的按钮：鼠标移到其上面-显示电话内容
			QQ的按钮：鼠标移到其上面-显示QQ的内容
			微信的按钮：鼠标移到其上面-显示微信的内容
			下的按钮：鼠标点击-控制页面显示下一页
		下面有一个鼠标点击-控制黑色圆角框的显示和隐藏的功能
	函数使用和结构简介：
	复制以下代码创建实例：
	<link type="text/css" rel="stylesheet" href="/css/SideNavigationBar.css">
	<script type="text/javascript" src="/js/SideNavigationBar.js"></script>

	// 用于实例化代码，可以放到任何地方
	// 参数部分添加json格式的数组参数用于实例化
	var OjSiNavbar = new SideNavigationBar({
		maxBox:".pub_RightButtonList",
	});
*/
function SideNavigationBar() {}