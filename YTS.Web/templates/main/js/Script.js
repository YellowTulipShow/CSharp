/* MapUse_Baidu */
(function () {
    window.MapUse_Baidu = function() {
        this.box_blackShady = $("#MapBox_BlackShady");
        this.class_blackShady_fullScreen = "PlayFullScreen";
        this.btn_colseFullScreen = $("#MapBox_Btn_CloseFullScreen");
        this.box_mainVideo = $("#MapBox_mainObj");
        this.content_map = $("#MapBox_content");
        this.box_coverImgs = $(".MapBox_CoverImg");

        this.body_scrollTop = 0;
        this.Init_Control();
    };
    window.MapUse_Baidu.prototype = {
        Init_Control: function() {
            var self = this;
            self.Set_Content_Map_Src();
            if (window.InfoCollect.BorwserType() != "Mobile") {
                self.Set_coverImgs_State(false);
                return;
            } else {
                self.Set_coverImgs_State(true);
            }
            self.content_map.height(window.PageInfo.Get_Page_Height());

            self.Set_mainObj_State(false);
            self.Set_Button_ColseFullScreen_State(false);

            self.box_coverImgs.click(function() {
                self.Start_FullScreen();
            });
            self.btn_colseFullScreen.click(function() {
                self.Close_FullScreen();
            });
        },
        Set_mainObj_State: function(bool) {
            var self = this;
            self.box_mainVideo.css({
                "height": bool ? "auto" : "0",
            });
        },
        Start_FullScreen: function() {
            var self = this;
            self.body_scrollTop = $(window).scrollTop();
            self.Set_blackShady_Style(true);
        },
        Close_FullScreen: function() {
            var self = this;
            self.Set_blackShady_Style(false);
            $(window).scrollTop(self.body_scrollTop);
        },
        Set_blackShady_Style: function(bool) {
            var self = this;

            if (bool) {
                self.box_blackShady.addClass(self.class_blackShady_fullScreen);
            } else {
                self.box_blackShady.removeClass(self.class_blackShady_fullScreen);
            }

            var win_h = window.PageInfo.Get_Page_Height();
            self.box_blackShady.css({
                "height": bool ? win_h : "auto",
            });

            $(".JSFixedHeight").css({
                "height": bool ? win_h : "auto",
                "overflow": bool ? "hidden" : "auto",
            });

            self.Set_Button_ColseFullScreen_State(bool);

            self.Set_mainObj_State(bool);
            self.Set_coverImgs_State(!bool);
        },
        Set_coverImgs_State: function(bool) {
            var self = this;
            if (bool) {
                self.box_coverImgs.show();
            } else {
                self.box_coverImgs.hide();
            }
        },
        Set_Button_ColseFullScreen_State: function(bool) {
            var self = this;
            if (bool) {
                self.btn_colseFullScreen.show();
            } else {
                self.btn_colseFullScreen.hide();
            }
        },
        Set_Content_Map_Src: function() {
            var self = this;
            self.content_map.attr('src', self.content_map.attr('data-src'));
            /*
            setTimeout(function() {
                self.content_map.attr('src', self.content_map.attr('data-src'));
                console.log("地图: 5秒已到, '获取'地图");
                console.log("src路径: " + self.content_map.attr('src'));
            }, 5 * 1000);
            */
        },
    };
})();

/* SliderBanner */
(function() {
    window.SliderBanner = function(argument_config) {
        var self = this;
        var def_conf = {
            ItemList: $("#ID_BannerList").children(".li"),

            Btn_Go_L: $(".Btn_Go")[0],
            Btn_Go_R: $(".Btn_Go")[1],
        };
        self.config = $.extend(def_conf, argument_config);
        self.Index = -1;
        self.Timer = null;
        self.Direction = 1; /* -1向左, 1向右 */
        self.Init();
    }
    window.SliderBanner.prototype = {
        Init: function() {
            var self = this;
            self.SetShowItem();

            $("#ID_BannerList").on('mousemove', function() {
                self.CloseTimer();
            });
            $("#ID_BannerList").on('mouseout', function() {
                self.OpenTimer();
            });

            $(self.config.Btn_Go_L).on('click', function() {
                self.Show_Prev();
            });
            $(self.config.Btn_Go_R).on('click', function() {
                self.Show_Next();
            });
        },
        Show_Next: function() {
            var self = this;
            self.Direction = 1;
            self.SetShowItem();
        },
        Show_Prev: function() {
            var self = this;
            self.Direction = -1;
            self.SetShowItem();
        },
        SetShowItem: function() {
            var self = this;
            self.Index += self.Direction;
            var max = self.config.ItemList.length - 1;
            if (self.Index < 0) {
                self.Index = max;
            }
            if (self.Index > max) {
                self.Index = 0;
            }
            var showCN = 'Show';
            $(self.config.ItemList).removeClass(showCN);
            $(self.config.ItemList[self.Index]).addClass(showCN);
            self.OpenTimer();
        },
        OpenTimer: function() {
            var self = this;
            self.CloseTimer();
            self.Timer = window.setTimeout(function() {
                self.SetShowItem();
            }, 4 * 1000);
            return self.Timer;
        },
        CloseTimer: function() {
            var self = this;
            window.clearInterval(self.Timer);
        },
    }
})();

/* Page Method */
(function() {
    window.PageClassSetLibrary = {
        ToggleDialog: function(sign_class_str) {
            window.PageMethod.ToggleClassName($(".Dialog" + sign_class_str), 'Show');
            window.PageMethod.ToggleClassName($("html"), 'Hidden');
        },
    };
    window.PaegRequest = {
        LeaveMessage: function() {
            function ErrorAlert(error_hint_string) {
                $('#ID_LeaveMessage_Error_span').text(error_hint_string);
                window.PageClassSetLibrary.ToggleDialog('#ID_LeaveMessage_Error');
            }
            function SuccessHint() {
                window.PageClassSetLibrary.ToggleDialog('#ID_LeaveMessage_Success');
            }

            var page_info = {
                name: $("#ID_Iut_Name").val(),
                tel: $("#ID_Iut_Phone").val(),
                msg: $("#ID_Iut_Demand").val(),
            };
            if (window.CheckData.IsStringNull(page_info.name)) {
                ErrorAlert('请您输入您的姓名!');
                return;
            }
            if (window.CheckData.IsStringNull(page_info.tel)) {
                ErrorAlert('留下您的电话, 我们会在第一时间回复您!');
                return;
            }
            if (window.CheckData.IsStringNull(page_info.msg)) {
                ErrorAlert('告诉我们您想说什么!');
                return;
            }

            window.AjaxRequest.AsynchronousRequest({
                url: "/tools/leavemessage_ajax.ashx?action=commit",
                data: page_info,
                async: true,
                EventSuccess: function(json) {
                    console.log(json);
                    if (json.Status == 1) {
                        SuccessHint();
                    } else {
                        ErrorAlert(json.Msg);
                    }
                },
            });

        },
    };
})();
