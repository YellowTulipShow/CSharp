/**
 * [AlertBox 弹框]
 * @param {[type]} type  弹框类型 doubleBtn/onceCancel/onceConfirm/mini/Loading
 * @param {[type]} alertType  弹框固定fixed /''滚动样式类型 
 * @param {[type]} alertCls  弹框class 可继承修改样式
 * @param {[type]} title 弹框标题
 * @param {[type]} msg 弹框内容
 * @param {[type]} cancelText 取消按钮文本
 * @param {[type]} confirmText 确认按钮文本
 * @param {[type]} cancel 取消按钮回调事件
 * @param {[type]} confirm 确认按钮回调事件
 * @param {[type]} callback 弹框回调事件
 * @return {[Function]}    [AlertBox({type:'doubleBtn',title:'温馨提示',...})]
 */
; (function (root, factory, d) {
    root.AlertBox = factory.call(root, root, d);
})(this, function (w, d) {

    'use strict';

    var _uuid = 0;

    function AlertBox(opts) {

        if (!(this instanceof AlertBox)) {
            return new AlertBox(opts).init();
        }

        this.opts = opts || {};
        this.uuid = _uuid;
        this.type = this.opts.type || "doubleBtn";
        this.alertType = this.opts.alertType || "";
        this.alertCls = this.opts.alertCls || "";
        this.title = this.opts.title || "";
        this.msg = this.opts.msg || "";
        this.cancelText = this.opts.cancelText || "取消";
        this.confirmText = this.opts.confirmText || "确定";
        this.cancel = this.opts.cancel || "";
        this.confirm = this.opts.confirm || "";
        this.callback = this.opts.callback || "";
        this.delay = this.opts.delay || 2000;
        this.showremovebtn = this.opts.showremovebtn || "true";
    }
    
    AlertBox.prototype = {
        constructor: AlertBox,
        getEl: function (supEl, el) {
            return supEl.querySelector(el);
        },
        init: function () {
            var self = this;
            _uuid++;
            self.setStyle();
            self.addAlertBox();
            if(self.type == "mini")
            {
                self.minEvent()
            }
            else if(self.type == "Loading")
            {
                return self;
            }
            else
            {
                self.alertEvent();
            }
        },
        addAlertBox: function () {
            var self = this,
                pos = self.getPos();
            self.alertType == "fixed" ? self.getFixedMask() : self.getMask();
            self.alertType == "fixed" ? self.getEl(d, "#alertMask_" + self.uuid).insertAdjacentHTML('beforeend', self.getHtml()) : self.getEl(d, "body").insertAdjacentHTML('beforeend', self.getHtml());
            self.alertMask = self.getEl(d, "#alertMask_" + self.uuid);
            self.alertBox = self.getEl(d, "#alertBox_" + self.uuid);
            if (self.alertType == "fixed") {
                if (self.type == "Loading") {
                    self.alertBox.style.cssText = "left:" + parseInt((pos.width - 130) / 2) + "px;top:50%;-webkit-transform:translate3d(0,-50%,0);";
                }
                else {
                    self.alertBox.style.cssText = "width:" + parseInt(pos.width - (2 * 25)) + "px;left:25px;top:50%;-webkit-transform:translate3d(0,-50%,0);";
                }
            } else {
                self.alertBox.style.cssText = "width:" + parseInt(pos.width - (2 * 25)) + "px;left:25px;top:" + parseInt(pos.sTop + w.innerHeight / 2 - self.alertBox.offsetHeight / 2) + "px;";  
            }

            self.callback && typeof self.callback == "function" && self.type != "mini" && self.type != "Loading" && self.callback();
        },
        setStyle: function () {
            var self = this,
           style = d.createElement("style"),
           cssStr = ".alert-box{position:absolute;left:0;top:0;border-radius:0.2rem;background:#FFF;-webkit-box-sizing:border-box;z-index:100;font-size:0.6rem;}" +
                    ".alert-msg{padding:0.4rem 0.6rem 0.6rem;text-align:center;line-height:1.8;word-break:break-all;}" +
                    ".alert-title{padding:0.6rem 0.6rem 0;text-align:center;}" +
                    ".alert-btn{display:-webkit-flex !important;display:-webkit-box;border-top:1px solid #DCDCDC;}" +
                    ".alert-btn a{display:block;-webkit-flex:1 !important;-webkit-box-flex:1;height:1.68rem;line-height:1.68rem;text-align:center;}" +
                    ".alert-btn a.alert-confirm{border-left:1px solid #DCDCDC;color:#EDA200;}" +
                    ".alert-btn a.alert-confirm.single{border-left:none;}" +
                    ".alert-box .ui-loading-bright{margin-top:20px;width: 37px;height: 37px;display: block;background-image: url(data:image/png;base64,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);-webkit-background-size: auto 37px;-webkit-animation: rotate2 1s steps(12) infinite;}" +
                    "@-webkit-keyframes rotate2 {from {background-position: 0 0 }to {background-position: -444px 0}}"+
                    ".alert-mini-box{border-radius:0.2rem;background:rgba(0,0,0,.7);color:#fff;}" +
                    ".alert-loading-box{width: 130px;height: 110px;display: -webkit-box;-webkit-box-orient: vertical;-webkit-box-pack: center;-webkit-box-align: center;text-align: center;background: rgba(0,0,0,.65);border-radius: 6px;color: #fff;}";
            style.type = "text/css";
            style.innerText = cssStr;
            self.getEl(d, "head").appendChild(style);
        },
        getPos: function () {
            var wn = d.documentElement.offsetWidth || d.body.offsetWidth,
                h = d.documentElement.offsetHeight || d.body.offsetHeight,
                s = d.documentElement.scrollTop || d.body.scrollTop;
            if (w.innerHeight > h) {
                h = w.innerHeight;
            }
            return {
                width: wn,
                height: h,
                sTop: s
            };
        },
        getHtml: function () {
            var self = this,
                html = '';
            if (self.type == "mini") {
                html += '<div class=\"alert-box alert-mini-box ' + self.alertCls + '\"  id="alertBox_' + self.uuid + '"><div class="alert-msg">' + self.msg + '</div></div>';
            }
            else if (self.type == "Loading") {
                html += '<div class=\"alert-box alert-loading-box ' + self.alertCls + '\"  id="alertBox_' + self.uuid + '"><i class="ui-loading-bright"></i><div class="alert-msg">' + self.msg + '</div></div>';
            }
            else {
                if (self.showremovebtn == "true") {
                    html += '<div style="position: absolute;right: 5px;top: 5px;z-index:999"><a href="javascript:;" class="alert-remove"><img src="data:image/png;base64,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"></a></div>';
                }
                html += '<div class=\"alert-box ' + self.alertCls + '\" id="alertBox_' + self.uuid + '">' +
                        '<div class="alert-title">' + self.title + '</div>' +
                        '<div class="alert-msg">' + self.msg + '</div>' +
                        '<div class="alert-btn">';
                switch (self.type) {
                    case "doubleBtn":
                        html += '<a href="javascript:;" class="alert-cancel mr10">' + self.cancelText + '</a>' +
                            '<a href="javascript:;" class="alert-confirm">' + self.confirmText + '</a>';
                        break;
                    case "onceCancel":
                        html += '<a href="javascript:;" class="alert-cancel">' + self.cancelText + '</a>';
                        break;
                    case "onceConfirm":
                        html += '<a href="javascript:;" class="alert-confirm single">' + self.confirmText + '</a>';
                        break;
                }
                html += '</div></div>';
            }
            return html;
        },
        getMask: function () {
            var self = this,
                pos = self.getPos(),
                mask = d.createElement("div");
            mask.id = "alertMask_" + self.uuid;
            self.getEl(d, "body").appendChild(mask);
            mask.style.cssText = "position:absolute;left:0;top:0;width:" + pos.width + "px;height:" + pos.height + "px;background:rgba(0,0,0,0.5);z-index:99";
            self.type == "mini" && (mask.style.backgroundColor = "rgba(255, 255, 255, 0)");
        },
        getFixedMask: function () {
            var self = this,
                mask = d.createElement("div");
            mask.id = "alertMask_" + self.uuid;
            self.getEl(d, "body").appendChild(mask);
            mask.style.cssText = "position:fixed;left:0;top:0;width:100%;height:100%;background:rgba(0,0,0,.5);z-index:99;";
        },
        minEvent: function () {
            var self = this;
            setTimeout(function () {
                if (navigator.userAgent.match(/iPhone/i)) {
                    $(self.alertBox).fadeOut(500, function () {
                        self.getEl(d, "body").removeChild(self.alertBox);
                        self.callback && typeof self.callback == "function" && self.callback();
                    });
                } else {
                    self.remove(self.alertBox);
                    self.callback && typeof self.callback == "function" && self.callback();
                }
                self.remove(self.getEl(d, "#alertMask_" + self.uuid));

            }, self.delay);
        },
        RemoveloadingEvent: function (obj) {
            var self = obj;
            self.remove(self.getEl(d, "#alertMask_" + self.uuid));
        },
        alertEvent: function () {
            var self = this;
            if (self.alertBox) {
                var cancelBtn = self.getEl(self.alertBox, ".alert-cancel"),
                    confirmBtn = self.getEl(self.alertBox, ".alert-confirm"),
                    removeBtn = self.getEl(self.alertMask, ".alert-remove");
                cancelBtn && self.reset(cancelBtn, self.cancel);
                removeBtn && self.reset(removeBtn, self.cancel);
                confirmBtn && self.reset(confirmBtn, self.confirm);
            }
        },
        reset: function (el, type) {
            var self = this;
            el.onclick = function () {
                type && typeof type == "function" && type(this);
                self.alertType != "fixed" && self.remove(self.alertBox);
                self.remove(self.getEl(d, "#alertMask_" + self.uuid));
            };
        },
        remove: function (el) {
            this.getEl(d, "body").removeChild(el);
        }
    }

    return AlertBox;

}, document);

