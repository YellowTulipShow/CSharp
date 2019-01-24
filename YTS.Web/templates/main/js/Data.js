function jsonpsuccessbackfunction(a,b,c,d,e,f) { }
(function() {
    /**
     * 引用自: http://blog.csdn.net/xiaoqijun/article/details/7096568
     * 将Data时间对象格式化为字符串
     *
     * @parameter {String} format
     * @return {String}
     */
    Date.prototype.FormatAsString = function(format) {
        var o = {
            "M+" : this.getMonth()+1, /* month */
            "d+" : this.getDate(), /* day */
            "[hH]+" : this.getHours(), /* hour */
            "m+" : this.getMinutes(), /* minute */
            "s+" : this.getSeconds(), /* second */
            "q+" : Math.floor((this.getMonth()+3)/3), /* quarter */
            "S" : this.getMilliseconds() /* millisecond */
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
    /**
     * 获得当前时间对象正常的符合'人类'思维的数值对象
     *
     * @return {Object}
     */
    Date.prototype.GetNormalValue = function() {
        return {
            Year: this.getFullYear(),
            Month: this.getMonth() + 1, /* especially! */
            Day: this.getDate(), /* especially! */
            Hour: this.getHours(),
            Minute: this.getMinutes(),
            Second: this.getSeconds(),
            Millisecond: this.getMilliseconds(),
        };
    }
    /**
     * 追加指定时间部分值进行加减法数值, 返回值为一个新的时间对象
     *
     * @parameter {String} matchFmt
     * @parameter {Int} diffValue
     * @return {Date}
     */
    Date.prototype.AppendValueClone = function(matchFmt, diffValue) {
        matchFmt = matchFmt || 'TTT';
        var v = this.GetNormalValue();
        diffValue = (!diffValue || isNaN(diffValue)) ? 0 : parseInt(diffValue);
        switch(matchFmt) {
            case 'yyyy': v.year += diffValue; break;
            case 'MM': v.month += diffValue; break;
            case 'dd': v.day += diffValue; break;
            case 'HH':
            case 'hh': v.hour += diffValue; break;
            case 'mm': v.minute += diffValue; break;
            case 'ss': v.second += diffValue; break;
        }
        return window.CommonData.CreateDateTime(v.year, v.month, v.day, v.hour, v.minute, v.second, v.millisecond);
    }
    /**
     * 追加指定时间部分值进行加减法数值, 返回值为当前时间对象
     *
     * @parameter {String} matchFmt
     * @parameter {Int} diffValue
     * @return {Date}
     */
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
    /* 静态 转化工具 */
    window.ConvertTool = {
        /**
         * 将Array数据对象的值组合为字符串
         *
         * @parameter {Array} arrayobj
         * @parameter {String} symbol
         * @return {String}
         */
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
    };
    /* 静态 检查数据工具 */
    window.CheckData = {
        /**
         * 检查对象是否为 undefined
         *
         * @parameter {Object} obj
         * @return {Boolean}
         */
        IsUndefined: function(obj) {
            /* 获得undefined，保证它没有被重新赋值 */
            var undefined = void(0);
            return obj === undefined;
        },
        /**
         * 检查对象是否为 undefined|null
         *
         * @parameter {Object} obj
         * @return {Boolean}
         */
        IsObjectNull: function(obj) {
            return this.IsUndefined(obj) || obj == null;
        },
        /**
         * 检查字符串是否为 undefined|null|""|''
         *
         * @parameter {String} str
         * @return {Boolean}
         */
        IsStringNull: function(str) {
            return this.IsObjectNull(str) || str.toString() == "" || str.toString() == '';
        },
    };
    /* 静态 常用数据 */
    window.CommonData = {
        /**
         * 获取指定年份和月份的最大日天数
         *
         * @parameter {Int} year
         * @parameter {Int} month
         * @return {Int}
         */
        GetMaxDayCount: function(year, month) {
            if (month == 2) {
                var calc_num = year % 100 == 0 ? 400 : 4;
                return (year % calc_num == 0) ? 29 : 28;
            }
            return (month <= 7 ? month : month + 1) % 2 == 1 ? 31 : 30;
        },
        /**
         * 根据各个时间部分创建时间对象
         *
         * @parameter {Int} year
         * @parameter {Int} month
         * @parameter {Int} day
         * @parameter {Int} hour
         * @parameter {Int} minute
         * @parameter {Int} second
         * @parameter {Int} millisecond
         * @return {Date}
         */
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
    };
    /* 静态 Ajax 请求 */
    window.AjaxRequest = {
        /**
         * 异步发送Ajax请求, (可跨域)
         *
         * @parameter {Object} argument_config
         */
        AsynchronousRequest: function(argument_config) {
            var self = this;
            var def_conf = {
                url: "",
                data: {},
                async: true,
                EventSuccess: function(json) {},
                EventError: self.defaultRequestError,
            };
            var config = $.extend(def_conf, argument_config);
            if (window.CheckData.IsStringNull(config.url)) {
                return;
            }
            var jsonpCallback_name_str = "jsonpsuccessbackfunction";
            var ajax_exe_config = {
                url: config.url,
                type: "GET",
                data: config.data,
                dataType: "jsonp",
                async: config.async,
                jsonp: "callback",
                jsonpCallback: jsonpCallback_name_str,
                success: function(json) {
                    config.EventSuccess(json);
                },
                error: function(XMLHttpRequest_obj, textStatus_obj, errorThrown_obj) {
                    if (XMLHttpRequest_obj.status == 200 && XMLHttpRequest_obj.readyState == 4) {
                        var json = XMLHttpRequest_obj.responseText;
                        var re_obj = new RegExp(jsonpCallback_name_str + "\\((.*)\\)\\;?", 'g');
                        json = json.replace(re_obj, '$1');
                        json = window.Json.ToObject(json);
                        config.EventSuccess(json);
                        return;
                    }
                    console.log("request ajax config: ", ajax_exe_config);
                    config.EventError(XMLHttpRequest_obj, textStatus_obj, errorThrown_obj);
                }
            };
            $.ajax(ajax_exe_config);
        },
        /**
         * 默认的请求错误执行方法
         *
         * @parameter {Object} XMLHttpRequest_obj (请求对象)
         * @parameter {Object} textStatus_obj (文本类型状态表示对象)
         * @parameter {Object} errorThrown_obj (错误抛出对象)
         */
        defaultRequestError: function(XMLHttpRequest_obj, textStatus_obj, errorThrown_obj) {
            console.log(XMLHttpRequest_obj);
            console.log(textStatus_obj);
            console.log(errorThrown_obj);
        },
    };
    /* 静态 Json 数据格式 */
    window.Json = {
        /**
         * 将对象或者json字符串转化为JSON格式对象
         *
         * @parameter {Object|String} jsonStr
         * @return {JsonObject}
         */
        ToObject: function(jsonStr) {
            try {
                switch(typeof(jsonStr)) {
                    case 'string': return eval('(' + jsonStr + ')'); break;
                    case 'object': return eval('(' + jsonStr.toSource() + ')'); break;
                    default: return {}; break;
                }
                /* Test: var obj = window.Json.ToObject("{id:"1", name:"zhao"}"); */
            } catch(err) {
                console.log("window.Json.ToObject() Method Error:");
                console.log(err);
                console.log(jsonStr);
                return jsonStr;
            }
        },
    };
    /* 静态 随机数据 */
    window.RandomData = {
        /**
         * 随机获取可用于ID的随机字符串
         *
         * @return {String}
         */
        ID: function() {
            return Number(Math.random().toString().substr(3) + Date.now()).toString(36);
        },
        /**
         * 获得最小和最大数值范围的其中一个值, 结果取最小, 不取最大
         *
         * @parameter {Int} min
         * @parameter {Int} max
         * @return {Int}
         */
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
        /**
         * 获得最小和最大时间范围的其中一个值
         *
         * @parameter {Date} min
         * @parameter {Date} max
         * @return {Date}
         */
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
    };
    /* 信息收集 */
    window.InfoCollect = {
        /**
         * 浏览器类型
         *
         * @return {String}
         */
        BorwserType: function () {
            var u = window.navigator.userAgent;
            var app = window.navigator.appVersion;
            /* 智能机浏览器版本信息: */
            var browser = {
                versions: {
                    trident: u.indexOf('Trident') > -1, /* IE内核 */
                    presto: u.indexOf('Presto') > -1, /* opera内核 */
                    webKit: u.indexOf('AppleWebKit') > -1, /* 苹果、谷歌内核 */
                    gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, /* 火狐内核 */
                    ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), /* ios终端 */
                    webApp: u.indexOf('Safari') == -1, /* 是否web应该程序，没有头部与底部 */

                    iPad: u.indexOf('iPad') > -1, /* 是否iPad */

                    mobile: !!u.match(/AppleWebKit.*Mobile.*/) || !!u.match(/AppleWebKit/), /* 是否为移动终端 */
                    android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, /* android终端或者uc浏览器 */
                    iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1, /* 是否为iPhone或者QQHD浏览器 */
                    wechatbrowser: u.toLowerCase().match(/MicroMessenger/i) == 'micromessenger', /* 是否微信浏览器 */
                },
                language: (navigator.browserLanguage || navigator.language).toLowerCase()
            };
            if (browser.versions.mobile ||
                browser.versions.android ||
                browser.versions.iPhone) {
                return 'Mobile';
            }
            if (browser.versions.iPad) {
                return 'Pad'
            }
            return 'PC';
        },
        /**
         * 浏览器版本
         *
         * @return {String}
         */
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
            return "Unrecognized"; /* 无法识别 */
        },
    };
    /* 静态 页面信息 */
    window.PageInfo = {
        /**
         * 获得页面宽度
         *
         * @return {Int}
         */
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
        /**
         * 获得页面高度
         *
         * @return {Int}
         */
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
        /**
         * 获得页面url中的指定参数值
         *
         * @parameter {String} name
         * @parameter {String} location_search_string
         * @return {String}
         */
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
        /**
         * 判断是否为手机端
         *
         * @return {Boolean}
         */
        IsMobile: function() {
            var type = window.InfoCollect.BorwserType();
            if (type == 'Mobile') {
                return true;
            }
            var win_w = this.Get_Page_Width();
            return win_w <= 900;
        },
        /**
         * 判断是否为平板端
         *
         * @return {Boolean}
         */
        IsPad: function() {
            var type = window.InfoCollect.BorwserType();
            if (type == 'Pad') {
                return true;
            }
            var win_w = this.Get_Page_Width();
            return 900 < win_w && win_w <= 1200;
        },
        /**
         * 判断是否为电脑端
         *
         * @return {Boolean}
         */
        IsPC: function() {
            var type = window.InfoCollect.BorwserType();
            if (type == 'PC') {
                return true;
            }
            var win_w = this.Get_Page_Width();
            return 1200 < win_w;
        },
    };
    /* 静态 页面常用方法 */
    window.PageMethod = {
        /**
         * 为页面元素添加或删除指定Class样式类名
         *
         * @parameter {JQueryElement|HTMLElement} elementobj
         * @parameter {String} classname
         * @parameter {Boolean} isAdd
         */
        SetClassName: function(elementobj, classname, isAdd) {
            var self = this;
            elementobj = $(elementobj);
            var ishas = elementobj.hasClass(classname);
            if (isAdd) {
                if (ishas) {
                    return;
                }
                elementobj.addClass(classname);
            } else {
                if (!ishas) {
                    return;
                }
                elementobj.removeClass(classname);
            }
        },
        /**
         * 取反页面元素添加或删除指定Class样式类名
         *
         * @parameter {JQueryElement|HTMLElement} elementobj
         * @parameter {String} classname
         */
        ToggleClassName: function(elementobj, classname) {
            var self = this;
            elementobj = $(elementobj);
            var ishas = elementobj.hasClass(classname);
            if (!ishas) {
                elementobj.addClass(classname);
            } else {
                elementobj.removeClass(classname);
            }
        },
        /**
         * 使元素获得焦点并且处于选择状态
         *
         * @parameter {HTMLElement} elementobj
         */
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
    };
    /* 静态 原生事件管理 */
    window.Event = {
        /**
         * 为HTMLElement元素对象-绑定事件
         *
         * @parameter {HTMLElement} obj
         * @parameter {String} eventNameStr
         * @parameter {Function} funEvent
         */
        Bind: function(obj, eventNameStr, funEvent) {
            if (!obj) { return; }
            if (obj.addEventListener) { /* 所有主流浏览器，除了 IE 8 及更早 IE版本 */
                obj.addEventListener(eventNameStr.toString(), funEvent, false);
            } else if (obj.attachEvent) { /* IE 8 及更早 IE 版本 */
                obj.attachEvent("on" + eventNameStr.toString(), funEvent);
            } else {
                obj["on" + eventNameStr] = funEvent;
            }
        },
        /**
         * 为HTMLElement元素对象-移除事件
         *
         * @parameter {HTMLElement} obj
         * @parameter {String} eventNameStr
         * @parameter {Function} funEvent
         */
        Remove: function(obj, eventNameStr, oFunc) {
            if (obj.removeEventListener) { /* ff,opera,safari等 */
            obj.removeEventListener(eventNameStr, oFunc, false);
            } else if (obj.detachEvent) { /* ie */
                obj.detachEvent("on" + eventNameStr, oFunc);
            } else { /* 其他 */
                obj["on" + eventNameStr] = null;
            }
        },
    };
})();
