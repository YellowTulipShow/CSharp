/*
 * Lazy Load - jQuery plugin for lazy loading images
 *
 * Copyright (c) 2007-2013 Mika Tuupola
 *
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 *
 * Project home:
 *   http://www.appelsiini.net/projects/lazyload
 *
 * Version:  1.9.3
 *
 */
(function($, window, document, undefined) {
    var $window = $(window);

    $.fn.lazyload = function(options) {
        var elements = this;
        var $container;
        var settings = {
            threshold       : 0,
            failure_limit   : 0,
            event           : "scroll",
            effect          : "show",
            container       : window,
            data_attribute  : "original",
            skip_invisible  : true,
            appear          : null,
            load            : null,
            placeholder     : "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAANSURBVBhXYzh8+PB/AAffA0nNPuCLAAAAAElFTkSuQmCC"
        };

        function update() {
            var counter = 0;

            elements.each(function() {
                var $this = $(this);
                if (settings.skip_invisible && !$this.is(":visible")) {
                    return;
                }
                if ($.abovethetop(this, settings) ||
                    $.leftofbegin(this, settings)) {
                        /* Nothing. */
                } else if (!$.belowthefold(this, settings) &&
                    !$.rightoffold(this, settings)) {
                        $this.trigger("appear");
                        /* if we found an image we'll load, reset the counter */
                        counter = 0;
                } else {
                    if (++counter > settings.failure_limit) {
                        return false;
                    }
                }
            });

        }

        if(options) {
            /* Maintain BC for a couple of versions. */
            if (undefined !== options.failurelimit) {
                options.failure_limit = options.failurelimit;
                delete options.failurelimit;
            }
            if (undefined !== options.effectspeed) {
                options.effect_speed = options.effectspeed;
                delete options.effectspeed;
            }

            $.extend(settings, options);
        }

        /* Cache container as jQuery as object. */
        $container = (settings.container === undefined ||
                      settings.container === window) ? $window : $(settings.container);

        /* Fire one scroll event per scroll. Not one scroll event per image. */
        if (0 === settings.event.indexOf("scroll")) {
            $container.bind(settings.event, function() {
                return update();
            });
        }

        this.each(function() {
            var self = this;
            var $self = $(self);

            self.loaded = false;

            /* If no src attribute given use data:uri. */
            if ($self.attr("src") === undefined || $self.attr("src") === false) {
                if ($self.is("img")) {
                    $self.attr("src", settings.placeholder);
                }
            }

            /* When appear is triggered load original image. */
            $self.one("appear", function() {
                if (!this.loaded) {
                    if (settings.appear) {
                        var elements_left = elements.length;
                        settings.appear.call(self, elements_left, settings);
                    }
                    $("<img />")
                        .bind("load", function() {

                            var original = $self.attr("data-" + settings.data_attribute);
                            $self.hide();
                            if ($self.is("img")) {
                                $self.attr("src", original);
                            } else {
                                $self.css("background-image", "url('" + original + "')");
                            }
                            $self[settings.effect](settings.effect_speed);

                            self.loaded = true;

                            /* Remove image from array so it is not looped next time. */
                            var temp = $.grep(elements, function(element) {
                                return !element.loaded;
                            });
                            elements = $(temp);

                            if (settings.load) {
                                var elements_left = elements.length;
                                settings.load.call(self, elements_left, settings);
                            }
                        })
                        .attr("src", $self.attr("data-" + settings.data_attribute));
                }
            });

            /* When wanted event is triggered load original image */
            /* by triggering appear.                              */
            if (0 !== settings.event.indexOf("scroll")) {
                $self.bind(settings.event, function() {
                    if (!self.loaded) {
                        $self.trigger("appear");
                    }
                });
            }
        });

        /* Check if something appears when window is resized. */
        $window.bind("resize", function() {
            update();
        });

        /* With IOS5 force loading images when navigating with back button. */
        /* Non optimal workaround. */
        if ((/(?:iphone|ipod|ipad).*os 5/gi).test(navigator.appVersion)) {
            $window.bind("pageshow", function(event) {
                if (event.originalEvent && event.originalEvent.persisted) {
                    elements.each(function() {
                        $(this).trigger("appear");
                    });
                }
            });
        }

        /* Force initial check if images should appear. */
        $(document).ready(function() {
            update();
        });

        return this;
    };

    /* Convenience methods in jQuery namespace.           */
    /* Use as  $.belowthefold(element, {threshold : 100, container : window}) */

    $.belowthefold = function(element, settings) {
        var fold;

        if (settings.container === undefined || settings.container === window) {
            fold = (window.innerHeight ? window.innerHeight : $window.height()) + $window.scrollTop();
        } else {
            fold = $(settings.container).offset().top + $(settings.container).height();
        }

        return fold <= $(element).offset().top - settings.threshold;
    };

    $.rightoffold = function(element, settings) {
        var fold;

        if (settings.container === undefined || settings.container === window) {
            fold = $window.width() + $window.scrollLeft();
        } else {
            fold = $(settings.container).offset().left + $(settings.container).width();
        }

        return fold <= $(element).offset().left - settings.threshold;
    };

    $.abovethetop = function(element, settings) {
        var fold;

        if (settings.container === undefined || settings.container === window) {
            fold = $window.scrollTop();
        } else {
            fold = $(settings.container).offset().top;
        }

        return fold >= $(element).offset().top + settings.threshold  + $(element).height();
    };

    $.leftofbegin = function(element, settings) {
        var fold;

        if (settings.container === undefined || settings.container === window) {
            fold = $window.scrollLeft();
        } else {
            fold = $(settings.container).offset().left;
        }

        return fold >= $(element).offset().left + settings.threshold + $(element).width();
    };

    $.inviewport = function(element, settings) {
         return !$.rightoffold(element, settings) && !$.leftofbegin(element, settings) &&
                !$.belowthefold(element, settings) && !$.abovethetop(element, settings);
    };

    /* Custom selectors for your convenience.   */
    /* Use as $("img:below-the-fold").something() or */
    /* $("img").filter(":below-the-fold").something() which is faster */

    $.extend($.expr[":"], {
        "below-the-fold" : function(a) { return $.belowthefold(a, {threshold : 0}); },
        "above-the-top"  : function(a) { return !$.belowthefold(a, {threshold : 0}); },
        "right-of-screen": function(a) { return $.rightoffold(a, {threshold : 0}); },
        "left-of-screen" : function(a) { return !$.rightoffold(a, {threshold : 0}); },
        "in-viewport"    : function(a) { return $.inviewport(a, {threshold : 0}); },
        /* Maintain BC for couple of versions. */
        "above-the-fold" : function(a) { return !$.belowthefold(a, {threshold : 0}); },
        "right-of-fold"  : function(a) { return $.rightoffold(a, {threshold : 0}); },
        "left-of-fold"   : function(a) { return !$.rightoffold(a, {threshold : 0}); }
    });
})(jQuery, window, document);

/*!
 * jQuery Cookie Plugin v1.4.1
 * https://github.com/carhartl/jquery-cookie
 *
 * Copyright 2013 Klaus Hartl
 * Released under the MIT license
 */
(function(factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD
        define(['jquery'], factory);
    } else if (typeof exports === 'object') {
        // CommonJS
        factory(require('jquery'));
    } else {
        // Browser globals
        factory(jQuery);
    }
}(function($) {

    var pluses = /\+/g;

    function encode(s) {
        return config.raw ? s : encodeURIComponent(s);
    }

    function decode(s) {
        return config.raw ? s : decodeURIComponent(s);
    }

    function stringifyCookieValue(value) {
        return encode(config.json ? JSON.stringify(value) : String(value));
    }

    function parseCookieValue(s) {
        if (s.indexOf('"') === 0) {
            // This is a quoted cookie as according to RFC2068, unescape...
            s = s.slice(1, -1).replace(/\\"/g, '"').replace(/\\\\/g, '\\');
        }

        try {
            // Replace server-side written pluses with spaces.
            // If we can't decode the cookie, ignore it, it's unusable.
            // If we can't parse the cookie, ignore it, it's unusable.
            s = decodeURIComponent(s.replace(pluses, ' '));
            return config.json ? JSON.parse(s) : s;
        } catch(e) {}
    }

    function read(s, converter) {
        var value = config.raw ? s : parseCookieValue(s);
        return $.isFunction(converter) ? converter(value) : value;
    }

    var config = $.cookie = function (key, value, options) {

        // Write

        if (value !== undefined && !$.isFunction(value)) {
            options = $.extend({}, config.defaults, options);

            if (typeof options.expires === 'number') {
                var days = options.expires, t = options.expires = new Date();
                t.setTime(+t + days * 864e+5);
            }

            return (document.cookie = [
                encode(key), '=', stringifyCookieValue(value),
                options.expires ? '; expires=' + options.expires.toUTCString() : '', // use expires attribute, max-age is not supported by IE
                options.path    ? '; path=' + options.path : '',
                options.domain  ? '; domain=' + options.domain : '',
                options.secure  ? '; secure' : ''
            ].join(''));
        }

        // Read

        var result = key ? undefined : {};

        // To prevent the for loop in the first place assign an empty array
        // in case there are no cookies at all. Also prevents odd result when
        // calling $.cookie().
        var cookies = document.cookie ? document.cookie.split('; ') : [];

        for (var i = 0, l = cookies.length; i < l; i++) {
            var parts = cookies[i].split('=');
            var name = decode(parts.shift());
            var cookie = parts.join('=');

            if (key && key === name) {
                // If second argument (value) is a function it's a converter...
                result = read(cookie, value);
                break;
            }

            // Prevent storing a cookie that we couldn't decode.
            if (!key && (cookie = read(cookie)) !== undefined) {
                result[name] = cookie;
            }
        }

        return result;
    };

    config.defaults = {};

    $.removeCookie = function (key, options) {
        if ($.cookie(key) === undefined) {
            return false;
        }

        // Must not alter options, thus extending a fresh object...
        $.cookie(key, '', $.extend({}, options, { expires: -1 }));
        return !$.cookie(key);
    };
}));

// JSON
(function() {
    /* https://developer.mozilla.org/zh-CN/docs/Web/JavaScript/Reference/Global_Objects/JSON */
    if (!window.JSON) {
        window.JSON = {
            parse: function(sJSON) {
                return eval('(' + sJSON + ')');
            },
            stringify: (function() {
                var toString = Object.prototype.toString;
                var isArray = Array.isArray || function(a) {
                    return toString.call(a) === '[object Array]';
                };
                var escMap = { '"': '\\"', '\\': '\\\\', '\b': '\\b', '\f': '\\f', '\n': '\\n', '\r': '\\r', '\t': '\\t' };
                var escFunc = function(m) {
                    return escMap[m] || '\\u' + (m.charCodeAt(0) + 0x10000).toString(16).substr(1);
                };
                var escRE = /[\\"\u0000-\u001F\u2028\u2029]/g;
                return function stringify(value) {
                    if (value == null) {
                        return 'null';
                    } else if (typeof value === 'number') {
                        return isFinite(value) ? value.toString() : 'null';
                    } else if (typeof value === 'boolean') {
                        return value.toString();
                    } else if (typeof value === 'object') {
                        if (typeof value.toJSON === 'function') {
                            return stringify(value.toJSON());
                        } else if (isArray(value)) {
                            var res = '[';
                            for (var i = 0; i < value.length; i++) res += (i ? ', ': '') + stringify(value[i]);
                            return res + ']';
                        } else if (toString.call(value) === '[object Object]') {
                            var tmp = [];
                            for (var k in value) {
                                if (value.hasOwnProperty(k)) tmp.push(stringify(k) + ': ' + stringify(value[k]));
                            }
                            return '{' + tmp.join(', ') + '}';
                        }
                    }
                    return '"' + value.toString().replace(escRE, escFunc) + '"';
                };
            })()
        };
    }
})();

// Date
(function() {
    Date.prototype.FormatAsString = function(format) {
        if (arguments.length <= 0) {
            format = 'yyyy-MM-dd HH:mm:ss';
        }

        var o = {
            "M+" : this.getMonth()+1, //month
            "d+" : this.getDate(), //day
            "[hH]+" : this.getHours(), //hour
            "m+" : this.getMinutes(), //minute
            "s+" : this.getSeconds(), //second
            "q+" : Math.floor((this.getMonth()+3)/3), //quarter
            "S" : this.getMilliseconds() //millisecond
        }
        if(/(y+)/.test(format)) {
            format=format.replace(RegExp.$1, (this.getFullYear()+"").substr(4 - RegExp.$1.length));
        }
        for(var k in o) {
            if (new RegExp("(" + k + ")").test(format)) {
                format = format.replace(RegExp.$1, RegExp.$1.length==1 ? o[k] : ("00"+ o[k]).substr((""+ o[k]).length));
            }
        }
        return format;
    }
    Date.prototype.GetNormalValue = function() {
        return {
            Year: this.getFullYear(),
            Month: this.getMonth() + 1, // especially!
            Day: this.getDate(), // // especially!
            Hour: this.getHours(),
            Minute: this.getMinutes(),
            Second: this.getSeconds(),
            Millisecond: this.getMilliseconds(),
        };
    }
    Date.prototype.AppendValueClone = function(matchFmt, diffValue) {
        matchFmt = matchFmt || 'TTT';
        var v = this.GetNormalValue();
        diffValue = (!diffValue || isNaN(diffValue)) ? 0 : parseInt(diffValue);
        switch(matchFmt) {
            case 'yyyy': v.Year += diffValue; break;
            case 'MM': v.Month += diffValue; break;
            case 'dd': v.Day += diffValue; break;
            case 'HH':
            case 'hh': v.Hour += diffValue; break;
            case 'mm': v.Minute += diffValue; break;
            case 'ss': v.Second += diffValue; break;
        }
        return window.CommonData.CreateDateTime(v.Year, v.Month, v.Day, v.Hour, v.Minute, v.Second, v.Millisecond);
    }
    Date.prototype.AppendValue = function(matchFmt, diffValue) {
        if (isNaN(diffValue))
            return this;
        switch(matchFmt) {
            case 'yyyy': this.setFullYear(this.getFullYear() + Number(diffValue)); break;
            case 'MM': this.setMonth(this.getMonth() + Number(diffValue)); break;
            case 'dd': this.setDate(this.getDate() + Number(diffValue)); break;
            case 'HH':
            case 'hh': this.setHours(this.getHours() + Number(diffValue)); break;
            case 'mm': this.setMinutes(this.getMinutes() + Number(diffValue)); break;
            case 'ss': this.setSeconds(this.getSeconds() + Number(diffValue)); break;
        }
        return this;
    }
})();

// Page Function
function jsonpsuccessbackfunction(a,b,c,d,e,f) {}
(function() {
    window.AjaxRequest = {
        Queue: [null,],
        QueueIndex: 0,
        QueueEnable: false,
        QueueAppend: function(ajax_data) {
            var self = this;
            var index = self.QueueGetFirstMeetCriteriaIndex(0, function(item_data) {
                return item_data === null;
            });
            if (index < 0) {
                self.Queue.push(ajax_data);
            } else {
                self.Queue[index] = ajax_data;
            }
        },
        QueueGetFirstMeetCriteriaIndex: function(init_index, fun_judgmentItemData) {
            var self = this;
            for (var i = init_index; i < self.Queue.length + init_index; i++) {
                var index = i;
                if (index >= self.Queue.length) {
                    index = index - self.Queue.length;
                }
                if (fun_judgmentItemData(self.Queue[index])) {
                    return index;
                }
            }
            return -1;
        },
        QueueExecute: function() {
            var self = this;
            if (self.QueueEnable) {
                return;
            }
            var ajax_data = self.Queue[self.QueueIndex];
            if (ajax_data === null) {
                self.QueueNext();
            } else {
                self.QueueEnable = true;
                $.ajax(ajax_data);
            }
        },
        QueueNext: function() {
            var self = this;
            self.Queue[self.QueueIndex] = null;
            self.QueueEnable = false;
            var index = self.QueueGetFirstMeetCriteriaIndex(self.QueueIndex + 1, function(item_data) {
                return item_data != null;
            });
            if (index < 0) {
                return;
            }
            self.QueueIndex = index;
            self.QueueExecute();
        },
        CrossDomainGet: function(argument_config) {
            var self = this;
            var config = self.MergeConfig(argument_config);
            if (window.CheckData.IsStringNull(config.url)) {
                return;
            }
            var data = {
                url: config.url,
                type: "GET",
                data: config.data,
                dataType: "jsonp",
                async: true,
                jsonp: "callback",
                jsonpCallback: "jsonpsuccessbackfunction",
                success: function(json) {
                    config.EventSuccess(json);
                },
                error: function(XMLHttpRequest_obj, textStatus_obj, errorThrown_obj) {
                    self.ErrorResponseProcessing(XMLHttpRequest_obj, textStatus_obj, errorThrown_obj, config.EventSuccess);
                },
                complete: function(XMLHttpRequest_obj, TypeStatus) {
                    config.EventComplete(XMLHttpRequest_obj, TypeStatus, this);
                    self.QueueNext();
                },
            };
            self.QueueAppend(data);
            self.QueueExecute();
        },
        LocalPost: function(argument_config) {
            var self = this;
            var config = self.MergeConfig(argument_config);
            if (window.CheckData.IsStringNull(config.url)) {
                return;
            }
            var data = {
                url: config.url,
                type: "POST",
                data: config.data,
                dataType: "json",
                async: config.async,
                success: function(json) {
                    config.EventSuccess(json);
                },
                error: function(XMLHttpRequest_obj, textStatus_obj, errorThrown_obj) {
                    self.ErrorResponseProcessing(XMLHttpRequest_obj, textStatus_obj, errorThrown_obj, config.EventSuccess);
                },
                complete: function(XMLHttpRequest_obj, TypeStatus) {
                    config.EventComplete(XMLHttpRequest_obj, TypeStatus, this);
                    if (this.async) {
                        self.QueueNext();
                    }
                },
            };
            if (config.async) {
                self.QueueAppend(data);
                self.QueueExecute();
            } else {
                $.ajax(data);
            }
        },
        MergeConfig: function(argument_config) {
            var self = this;
            var def_conf = {
                url: '',
                data: {},
                async: true,
                EventSuccess: function(json) {},
                EventComplete: function(XMLHttpRequest_obj, TypeStatus, complete_this) {
                    console.log('Ajax Request Complete: ', complete_this.url);
                },
            };
            var config = $.extend(def_conf, argument_config);
            return config;
        },
        ErrorResponseProcessing: function(XMLHttpRequest_obj, textStatus_obj, errorThrown_obj, SuccessEXEMethod) {
            var self = this;
            if (window.CheckData.IsObjectNull(SuccessEXEMethod) || !self.IsSuccessRequest(XMLHttpRequest_obj)) {
                self.PrintErrorInfoObject(XMLHttpRequest_obj, textStatus_obj, errorThrown_obj);
                return;
            }
            var sjson = XMLHttpRequest_obj.responseText;
            if (window.CheckData.IsStringNull(sjson)) {
                self.PrintErrorInfoObject(XMLHttpRequest_obj, textStatus_obj, errorThrown_obj);
            } else {
                self.ParseSJSONExeSuccessMethod(sjson, SuccessEXEMethod);
            }
        },
        ParseSJSONExeSuccessMethod: function(sjson, successMethod) {
            var self = this;
            var json = {};
            try {
                json = window.JSON.parse(sjson);
            } catch(ex) {
                console.log('parse json Error:', json, '\tex:', ex);
                json = {
                    'Status': 0,
                    'Msg': '错误内容!',
                    'Url': '',
                    'Result': {},
                };
            }
            successMethod(json);
        },
        IsSuccessRequest: function(XMLHttpRequest_obj) {
            /* XMLHttpRequest 对象属性值参照地址: http://www.w3school.com.cn/xmldom/dom_http.asp */
            return XMLHttpRequest_obj.status == 200 || XMLHttpRequest_obj.readyState == 4;
        },
        PrintErrorInfoObject: function(XMLHttpRequest_obj, textStatus_obj, errorThrown_obj) {
            console.log('class.AjaxRequest (Error Request Response Result):');
            console.log('XMLHttpRequest:', XMLHttpRequest_obj, 'textStatus:', textStatus_obj, 'errorThrown:', errorThrown_obj);
        }
    };

    window.InfoCollect = {
        BorwserType: function () {
            // 智能机浏览器版本信息:
            var browser = {
                versions: function () {
                    var u = window.navigator.userAgent;
                    var app = window.navigator.appVersion;
                    //移动终端浏览器版本信息
                    return {
                        trident: u.indexOf('Trident') > -1, //IE内核
                        presto: u.indexOf('Presto') > -1, //opera内核
                        webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
                        gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
                        mobile: !!u.match(/AppleWebKit.*Mobile.*/) || !!u.match(/AppleWebKit/), //是否为移动终端
                        ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
                        android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器
                        iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1, //是否为iPhone或者QQHD浏览器
                        iPad: u.indexOf('iPad') > -1, //是否iPad
                        webApp: u.indexOf('Safari') == -1, //是否web应该程序，没有头部与底部
                        wechatbrowser: u.toLowerCase().match(/MicroMessenger/i) == 'micromessenger', // 是否微信浏览器
                    };
                }(),
                language: (navigator.browserLanguage || navigator.language).toLowerCase()
            }
            if (browser.versions.mobile ||
                browser.versions.android ||
                browser.versions.iPhone ||
                browser.versions.iPad) {
                return "Mobile";
            } else {
                return "PC";
            }
        },
        BorwserVersion: function () {
            var u = window.navigator.userAgent;
            var uStr = u.toString();
            var uStr_lower = uStr.toLowerCase();

            if (uStr_lower.indexOf('micromessenger') >= 0) {
                return 'WeChatBrowser';
            }
            if (uStr_lower.indexOf('baidubrowser') >= 0) {
                return 'BaiduBrowser';
            }
            if (uStr_lower.indexOf('baiduboxapp') >= 0) {
                return 'BaiduBoxApp';
            }
            return "Unrecognized"; // 无法识别
        },
    };

    window.CheckData = {
        IsUndefined: function(obj) {
            //获得undefined，保证它没有被重新赋值
            var undefined = void(0);
            return obj === undefined;
        },
        IsObjectNull: function(obj) {
            return this.IsUndefined(obj) || obj == null;
        },
        IsStringNull: function(str) {
            return this.IsObjectNull(str) || str.toString() == "" || str.toString() == '';
        },
        IsChinaChar: function(value) {
            return /.*[\u4e00-\u9fa5]+.*$/.test(value);
        },
    };

    window.PageInfo = {
        Get_Page_Width: function () {
            if (window.innerWidth) {
                return window.innerWidth;
            }
            if ((document.body) && (document.body.clientWidth)) {
                return document.body.clientWidth;
            }
            if (document.documentElement && document.documentElement.clientWidth) {
                return document.documentElement.clientWidth;
            }
            return 0;
        },
        Get_Page_Height: function () {
            if (window.innerHeight) {
                return window.innerHeight;
            }
            if ((document.body) && (document.body.clientHeight)) {
                return document.body.clientHeight;
            }
            if (document.documentElement && document.documentElement.clientHeight) {
                return document.documentElement.clientHeight;
            }
            return 0;
        },
        FocusSelect: function(element) {
            try {
                if (element.hasAttribute('contenteditable')) {
                    element.focus();
                }

                var selection = window.getSelection();
                var range = document.createRange();

                range.selectNodeContents(element);
                selection.removeAllRanges();
                selection.addRange(range);

                console.log(selection);
            } catch (err) {
                console.log(err);
            }
        },
        getQueryString: function(name, location_search_string) {
            if (window.CheckData.IsStringNull(location_search_string)) {
                location_search_string = window.location.search;
            }
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = location_search_string.substr(1).match(reg);
            if (r != null) {
                return unescape(r[2]);
            } else {
                return null;
            }
        },
        SetClassName: function(jqElementobj, classname, bool) {
            var self = this;
            jqElementobj = $(jqElementobj);
            var ishas = jqElementobj.hasClass(classname);
            if (bool) {
                if (ishas) {
                    return;
                }
                jqElementobj.addClass(classname);
            } else {
                if (!ishas) {
                    return;
                }
                jqElementobj.removeClass(classname);
            }
        },
        DocumentLock: function(isLock) {
            if (arguments.length <= 0) {
                isLock = true;
            }
            var classname = 'LockWindowSize';
            if (isLock) {
                $("html").addClass(classname);
                $("body").addClass(classname);
            } else {
                $("html").removeClass(classname);
                $("body").removeClass(classname);
            }
        },
        EnabelFullScreenModel: function(isLock) {
            if (arguments.length <= 0) {
                isLock = true;
            }
            var classname = 'FullScreenModel';
            if (isLock) {
                $("html").addClass(classname);
                $("body").addClass(classname);
            } else {
                $("html").removeClass(classname);
                $("body").removeClass(classname);
            }
        },
    };

    window.SetEqualHeightStyle = function (config) {
        try {
            var source = $(config.jqSign_source);
            var aims = $(config.jqSign_aims);
            var sHei = $(source).height();
            if (sHei <= 0) {
                sHei = config.errorDefault || 0;
            }
            $(aims).height(sHei);
        } catch (err) {
            console.log("window.SetEqualHeightStyle 出错! 参数: ");
            console.log(config);
            console.log(err);
        }
    }

    window.PageBodyFixedScrollTop = {
        Locking: function() {
            var self = this;
            self.SetLockStatue(true);
        },
        Unlock: function() {
            var self = this;
            self.SetLockStatue(false);
        },
        SetLockStatue: function(bool) {
            var self = this;
            if (bool) {
                window.PageBodyFixedScrollTop_Value = $(window).scrollTop();
            }
            $(".JSFixedHeight").css({
                "height": bool ? window.PageInfo.Get_Page_Height() : "",
                "overflow": bool ? "hidden" : "",
            });
            $(".body").css({
                "margin-top": bool ? window.PageBodyFixedScrollTop_Value * -1 : "",
            });
            if (!bool) {
                $(window).scrollTop(window.PageBodyFixedScrollTop_Value);
            }
        },
    };

    window.PageAlertFixedBox = {
        Open: function(keyname) {
            var self = this;
            self.SetStatus(true, keyname);
            window.PageBodyFixedScrollTop.Locking();
            if (!window.PageAlertFixedBox_IsSetCloseEvent) {
                self.SetCloseEvent();
            }
        },
        Close: function() {
            var self = this;
            self.SetStatus(false);
            window.PageBodyFixedScrollTop.Unlock();
        },
        SetStatus: function(bool, keyname) {
            var self = this;
            var win_w = window.PageInfo.Get_Page_Width();
            var win_h = window.PageInfo.Get_Page_Height();

            var id_Fbox = self.GetObj_FixedBox();
            id_Fbox.css({
                "width": bool ? win_w : "",
                "height": bool ? win_h : "0",
            });

            var id_Fb_main = self.GetObj_FixedBox_MainBox();

            if (bool) {
                id_Fb_main.addClass(keyname);

                var padding_top = parseFloat(id_Fb_main.css("padding-top").replace('px',''));
                var padding_bottom = parseFloat(id_Fb_main.css("padding-bottom").replace('px',''));
                var main_h = id_Fb_main.height() + padding_top + padding_bottom;
                main_h = (win_h - main_h) / 2
                id_Fb_main.css({
                    "top": main_h - 10,
                });
            } else {
                var keys = ['HintCopy', 'Timing'];
                for (var i = 0; i < keys.length; i++) {
                    id_Fb_main.removeClass(keys[i]);
                }
            }
        },
        IsShow: function() {
            var self = this;
            var id_Fbox = self.GetObj_FixedBox();
            if (id_Fbox.height() > 0) {
                return true;
            }
            return false;
        },
        SetCloseEvent: function() {
            var self = this;
            if (window.PageAlertFixedBox_IsSetCloseEvent) {
                return;
            }
            // 启用关闭事件
            self.GetObj_FixedBox_Close().click(function() {
                self.Close();
            });
            window.PageAlertFixedBox_IsSetCloseEvent = true;
        },
        GetObj_FixedBox: function() {
            return $("#ID_FixedBox");
        },
        GetObj_FixedBox_MainBox: function() {
            return $("#ID_FixedBox_MainBox");
        },
        GetObj_FixedBox_Close: function() {
            return $("#ID_FixedBox_Close");
        },
    };

    /* 静态 常用数据 */
    window.CommonData = {
        GetMaxDayCount: function(year, month) {
            if (month == 2) {
                var calc_num = year % 100 == 0 ? 400 : 4;
                return (year % calc_num == 0) ? 29 : 28;
            }
            return (month <= 7 ? month : month + 1) % 2 == 1 ? 31 : 30;
        },
        CreateDateTime: function(year, month, day, hour, minute, second, millisecond) {
            var year = year || 1;
            var month = month || 1;
            var day = day || 1;
            var hour = hour || 0;
            var minute = minute || 0;
            var second = second || 0;
            var millisecond = millisecond || 0;
            return new Date(year, month - 1, day, hour, minute, second, millisecond);
        },
        ASCII_Number: function() {
            return ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];
        },
        ASCII_UpperEnglish: function() {
            return ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'];
        },
        ASCII_LowerEnglish: function() {
            return ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'];
        },
        ASCII_Special: function() {
            return ['!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~'];
        },
        ASCII_WordText: function() {
            var self = this;
            var arr_number = self.ASCII_Number();
            var arr_en_upper = self.ASCII_UpperEnglish();
            var arr_en_lower = self.ASCII_LowerEnglish();
            return arr_number.concat(arr_en_upper, arr_en_lower);
        },
        ASCII_ALL: function() {
            var self = this;
            var arr_number = self.ASCII_Number();
            var arr_en_upper = self.ASCII_UpperEnglish();
            var arr_en_lower = self.ASCII_LowerEnglish();
            var arr_special = self.ASCII_Special();
            return arr_number.concat(arr_en_upper, arr_en_lower, arr_special);
        },
        ASCII_Hexadecimal: function() {
            return ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'];
        },
    };
    /* 静态 随机数据 */
    window.RandomData = {
        ID: function() {
            function S4() {
                return (((1+Math.random())*0x10000)|0).toString(16).substring(1);
            }
            var self = this;
            var str = S4();
            var ki = 4;
            for (var i = 0; i < 6; i++) {
                if (self.Int(0, 2) === 0 && ki > 0) {
                    str += '-';
                    ki -= 1;
                }
                str += S4();
            }
            return str;
        },
        Int: function(min, max) {
            if (min > max) {
                var zhong = min;
                min = max;
                max = zhong;
            }
            max = parseInt(max) - 1;
            switch(arguments.length) {
                case 1: return parseInt(Math.random() * min + 1);
                case 2: return parseInt(Math.random() * (max - min + 1) + min);
                default: return 0;
            }
        },
        DateTime: function(min, max) {
            if (min > max) {
                var zhong = min;
                min = max;
                max = zhong;
            }
            var min_val_obj = min.GetNormalValue();
            var max_val_obj = max.GetNormalValue();
            var obj = {
                result: 0,
                upstatue: 0,
            };
            function TimeRangeSelect(obj, min, max, start, end) {
                if (obj.upstatue == 4) {
                    obj.result = this.Int(min, max);
                } else {
                    var minvalue = (obj.upstatue == 3) ? min : start;
                    var maxvalue = (obj.upstatue == 2) ? max - 1 : end;
                    if (minvalue > maxvalue) {
                        var zhong = minvalue;
                        minvalue = maxvalue;
                        maxvalue = zhong;
                    }
                    obj.result = this.Int(minvalue, maxvalue + 1);

                    var selfstatus = 0;
                    if (minvalue == obj.result && obj.result == maxvalue) {
                        selfstatus = 1;
                    }
                    if (minvalue == obj.result && obj.result < maxvalue) {
                        selfstatus = (obj.upstatue == 3) ? 4 : 2;
                    }
                    if (minvalue < obj.result && obj.result == maxvalue) {
                        selfstatus = (obj.upstatue == 2) ? 4 : 3;
                    }
                    if (minvalue < obj.result && obj.result < maxvalue) {
                        selfstatus = 4;
                    }
                    obj.upstatue = (selfstatus < obj.upstatue) ? obj.upstatue : selfstatus;
                }
                return obj;
            }
            obj = TimeRangeSelect(obj, 1, 9999 + 1, min_val_obj.Year, max_val_obj.Year);
            var r_Year = obj.result;
            obj = TimeRangeSelect(obj, 1, 12 + 1, min_val_obj.Month, max_val_obj.Month);
            var r_Month = obj.result;
            obj = TimeRangeSelect(obj, 1, window.CommonData.GetMaxDayCount(r_Year, r_Month) + 1, min_val_obj.Day, max_val_obj.Day);
            var r_Day = obj.result;
            obj = TimeRangeSelect(obj, 0, 23 + 1, min_val_obj.Hour, max_val_obj.Hour);
            var r_Hour = obj.result;
            obj = TimeRangeSelect(obj, 0, 59 + 1, min_val_obj.Minute, max_val_obj.Minute);
            var r_Minute = obj.result;
            obj = TimeRangeSelect(obj, 0, 59 + 1, min_val_obj.Second, max_val_obj.Second);
            var r_Second = obj.result;
            obj = TimeRangeSelect(obj, 0, 999 + 1, min_val_obj.Millisecond, max_val_obj.Millisecond);
            var r_Millisecond = obj.result;
            return window.CommonData.CreateDateTime(r_Year, r_Month, r_Day, r_Hour, r_Minute, r_Second, r_Millisecond);
        },
        CombinedString: function(array, strlength) {
            var self = this;
            if (window.CheckData.IsSizeEmpty(array) || arguments.length <= 0) {
                array = window.CommonData.ASCII_WordText();
            }
            if (arguments.length <= 1) {
                strlength = 32;
            }
            var result = new Array();
            for (var i = 0; i < strlength; i++) {
                var index = self.Int(0, array.length);
                var item = array[index].toString();
                result.push(item);
            }
            return result.join('');
        }
    };
    /* 静态 转化工具 */
    window.ConvertTool = {
        ArrayToString: function(arrayobj, symbol) {
            if (window.CheckData.IsStringNull(symbol)) {
                return '';
            }
            var resustr = '';
            for (var i = 0; i < arrayobj.length; i++) {
                if (window.CheckData.IsStringNull(arrayobj[i])) {
                    continue;
                } else if (i != 0) {
                    resustr += symbol;
                }
                resustr += arrayobj[i];
            }
            return resustr;
        },
        ToInt: function(objvalue, defint) {
            if (arguments.length <= 1) {
                defint = 0;
            }
            var parsed = Number.parseInt(objvalue.toString(), 0);
            if (Number.isNaN(parsed)) {
                return defint;
            }
            return parsed;
        },
        ToDate: function(s_date, format) {
            if (arguments.length <= 1) {
                format = 'yyyy-MM-dd HH:mm:ss';
            }
            if (arguments.length <= 0) {
                return new Date();
            }

            s_date = s_date.replace(/-/g,"/");
            return new Date(s_date);
        },
    };
})();
