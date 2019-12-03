(function (root,facotry,plug){
    return facotry(root.jQuery,plug);
})(window,function($,plug) {
    // 配置键值
    var __KEY__ = {
        sign : "yts-",
        message : "message"
    }

    // 默认参数
    var __DEFS__ = {
        trigger : "change"
    };

    // 规则引擎
    var __RULES__ = {
        required : function(){ // 是否必须输入
            return this.val()!=="";
        },
        regex : function(){ // 是否符合此正则表达式
            return new RegExp(this.data("dv-regex")).test(this.val());
        },
        email : function(){ // 是否符合邮箱格式数据
            return /^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+((\.[a-zA-Z0-9_-]{2,3}){1,2})$/.test(this.val());
        },
        url : function(){ // 是否符合url格式数据
            return false; // ...
        }
        // ...
    };

    /*=================================以下代码基本不用变=================================*/
    // 架构方法搭建
    $.fn(plug)=function(options) {
        // jQuery继承方法,将指定的两个对象合并或者替换
        $.extend(this, __DEFS__, options);

        var $fileds = this.find("input").not("[type=button],[type=reset],[type=submit]");
        $fileds.on(this.trigger,function(){
            // console.log(this+"正在验证");

            var $field = $(this); // 被验证的目标对象
            var result = false; // 验证结果默认失败
            $field.next().remove(); // next(): DOM对象的同级下一个标签
            $.each(__RULES__,function(rule,valider){
                if ($field.data(__KEY__.sign + rule)) {
                    //我需要验证设置有值的规则
                    result=valider.call($field);
                    if (!result) {
                        $field.after("<p>"+$field.data(__KEY__.sign + rule + __KEY__.message)+"</p>");
                    }
                }
            });
        });
    }
},"inputValidator");