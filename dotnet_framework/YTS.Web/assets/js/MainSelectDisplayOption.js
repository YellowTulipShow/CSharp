/**
    MainSelectDisplayOption.js
    2017/06/09 创建 作者: YellowTulipShow
    MainSelectDisplayOption 首页开始页面选择显示选项
*/
$(document).ready(function() {
    MainSelectDisplayOption({
        ShowBigControlID: "DisplayShowBigControl",
        leftControlID: "DisplayLeftControl",
        rightControlID: "DisplayRightControl",
        OptionClass: "DisplayOption",
        trigger: "click",
        // trigger: "mouseover",
        triggerAddClass: "DisplayOptionHover"
    });
});

function MainSelectDisplayOption_2(JsonOptions) {
    var ShowBigControl = $("#" + JsonOptions.ShowBigControlID);
    var leftControl = $("#" + JsonOptions.leftControlID);
    var rightControl = $("#" + JsonOptions.rightControlID);
    var OptionControl = $("." + JsonOptions.OptionClass);

    OptionControl.ready(function() {
        var urlstring = $(this).children("img").attr("src");
        console.log(urlstring);
    });
}

function MainSelectDisplayOption(JsonOptions) {
    var ShowBigControl = $("#" + JsonOptions.ShowBigControlID);
    var leftControl = $("#" + JsonOptions.leftControlID);
    var rightControl = $("#" + JsonOptions.rightControlID);
    var OptionControl = $("." + JsonOptions.OptionClass);

    leftControl.on(JsonOptions.trigger, function() {
        var elementobj = FindParentElement(OptionControl).prev();
        if (elementobj.length > 0) {
            SetShowContent(elementobj);
        }
        console.log(elementobj);
    });
    rightControl.on(JsonOptions.trigger, function() {
        var elementobj = FindParentElement(OptionControl).next();
        if (elementobj.length > 0) {
            SetShowContent(elementobj);
        }
    });

    OptionControl.each(function() {
        $(this).on(JsonOptions.trigger, function() {
            SetShowContent($(this));
        });
    });

    SetShowContent(OptionControl[0]);

    function SetShowContent(controlObj) {
        controlObj = $(controlObj);
        OptionControl.removeClass(JsonOptions.triggerAddClass);
        controlObj.addClass(JsonOptions.triggerAddClass);
        var urlstring = controlObj.find("img").attr("src");
        $(ShowBigControl).find("img").attr("src", urlstring);
    }

    function FindParentElement(controlObj) {
        return $(controlObj).parent().find("." + JsonOptions.triggerAddClass);
    }
}