/**
    AdaptiveHeightOfBoxBasedOnWidth.js
    2017/06/09 创建 作者: YellowTulipShow
    Adaptive Height Of Box Based On Width 基于宽度的箱体自适应高度
*/
$(document).ready(function() {
    AutoBorderApplication();
    $(window).resize(function() {
        AutoBorderApplication();
    });
});
function AutoBorderApplication() {
    AdaptiveHeightOfBoxBasedOnWidth({
        ClassName: "DisplayOption",
        Rate: 1.538
    });
}
function AdaptiveHeightOfBoxBasedOnWidth(JsonOptions) {
    var obj = $("." + JsonOptions.ClassName);
    obj.height(obj.width()/JsonOptions.Rate + "px");
}