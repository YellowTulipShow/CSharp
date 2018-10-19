function ZRQWindow() {

    // 窗口宽度
    var windowWidth=0;
    // 获得浏览器窗口的宽度
    this.get_windowWidth=get_windowWidth;
    function get_windowWidth() {
    	if (window.innerWidth) {
    		windowWidth = window.innerWidth;
    	}
    	else if ((document.body) && (document.body.clientWidth)) {
    		windowWidth = document.body.clientWidth;
    	}
    	// 通过深入 Document 内部对 body 进行检测，获取窗口大小
        if (document.documentElement && document.documentElement.clientWidth)
        {
            windowWidth = document.documentElement.clientWidth;
        }

        return windowWidth;
    }

    // 窗口宽度
    var windowHeight=0;
    // 获得浏览器窗口的高度
    this.get_windowHeight=get_windowHeight;
    function get_windowHeight() {
    	// 获取窗口高度
        if (window.innerHeight) {
            windowHeight = window.innerHeight;
        }
        else if ((document.body) && (document.body.clientHeight)) {
            windowHeight = document.body.clientHeight;
        }

        // 通过深入 Document 内部对 body 进行检测，获取窗口大小
        if (document.documentElement && document.documentElement.clientHeight)
        {
            windowHeight = document.documentElement.clientHeight;
        }

        return windowHeight;
    }

    // 用于设置span、strong之类的元素里的文字行高等于最近的父元素的高度
    this.SetSpanLineHeight=SetSpanLineHeight;
    function SetSpanLineHeight(oj) {
        // console.log(oj);
        $(oj).each(function() {
            var s;
            if ($(this).parent().parent().children("a")) {
                s=$(this).parent().parent().height();
            } else {
                s=$(this).parent().height();
            }
            // console.log(s);
            $(this).css("line-height",s+"px");
        });
    }

    // 用于引用动态引用指定的js文件
    this.AddHeadJsFile=AddHeadJsFile;
    function AddHeadJsFile(url) {
        var str="<script type=/text/javascript/ src="+url+"></script>";
        $("head").append(str);
    }

    // 用于异步执行的方法
    // function eventshijian() {FileZ.AsynchronousExecution(function() {
    // });}
    this.AsynchronousExecution=AsynchronousExecution;
    function AsynchronousExecution(event) {
        setTimeout(event(),0);
    }
}








// 匿名函数调用方法
(function(root,facotry,plug) { //首先在文件加载时加载此方法
    // 方法体(母体)
})(window,function($,value) { //在创建上面的母体方法之后调用并使用传入对应的三个参数
    // 传入方法的时候执行"参数"的"方法体内容"
},"onzhixing");






// 使用匿名函数调用方法 创建闭包对象 并创建插件
(function(root,facotry,plug) {
    return facotry(root.jQuery,plug); // 返回回调函数的闭包
})(window,function($,value) {
    $.fn[value] = function() {
        // 这里就是使用插件的具体方法内容
    }
},"onzhixing");

// 当使用的时候就可以这样写
$("#ddd").onzhixing();