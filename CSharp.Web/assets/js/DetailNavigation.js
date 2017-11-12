/**
    DetailNavigation.js
    2017/06/01 创建 作者: YellowTulipShow
    Detail Navigation 详细的导航JS逻辑代码
*/
var DetailNavigation = (function(config) {
    // 用户传入的全局配置变量
    var __CONFIG__ = {
        defaultOnEvent : "click",
        eventTriggerObject : [],
        hideClass : "",
        hideObject: [],
        BodyPaddingHeightObj : ""
    };

/* ====== Init Class Object ====== */
    return Init;
    function Init(config) {
        if (!Check__CONFIG__Create(config))
            return;
        // SetBodyPaddingTopValue();
        AddEventObject();
    }
/* ====== Init Function Region ====== */
    function Check__CONFIG__Create(config) {
        $.extend(__CONFIG__, config); // 合并 配置信息
        return true;
    }
/* ====== Main Logical Area ====== */
    function SetBodyPaddingTopValue() {
        $("body").css({
            "padding-top": $(__CONFIG__.BodyPaddingHeightObj).height() + "px"
        });
        $(window).resize(function() {
            $("body").css({
                "padding-top": $(__CONFIG__.BodyPaddingHeightObj).height() + "px"
            });
        });
    }

    function AddEventObject() {
        var eventTrigObj = __CONFIG__.eventTriggerObject;
        SetHideObjHideAttr();
        for (var i = 0; i < eventTrigObj.length; i++) {
            $(eventTrigObj[i]).on(__CONFIG__.defaultOnEvent, SetHideObjHideAttr);
        }
    }

    function SetHideObjHideAttr() {
        var hideObj = __CONFIG__.hideObject;
        var hideclass = __CONFIG__.hideClass;

        for (var j = 0; j < hideObj.length; j++) {
            $(hideObj[j]).each(function() {
                if ($(this).hasClass(hideclass)) {
                    $(this).removeClass(hideclass);
                } else {
                    $(this).addClass(hideclass);
                }
            });
        }
    }
})();

