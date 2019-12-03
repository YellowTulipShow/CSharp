// VideoUse
(function() {
    window.VideoUse = function(arg_config) {
        this.control = {
            box: {
                // 视频区域
                video: $("#ID_Box_VideoRegion"),
                // 主要的播放区域
                main: $("#ID_MainPlayer"),
                // 遮罩图片
                cover_img: $("#ID_Box_Video_CoverImg"),
            },
            btn: {
                // 关闭全屏按钮
                close_fullscreen: $("#ID_Btn_VideoCloseFullScreen"),
            },
        };
        this.info = $.extend(true, {
            class: {
                // 全屏
                play: 'Play',
                loading: 'Loading',
                fullscreen: 'FullScreen',
            },
            auto_play: false,
            is_blackShady_fullScreen: false,
            address: {
                uu: "qtevp3utiw",
                vu: "b22d49712c",
                pu: "9e0d901162",
            },
        }, arg_config.info);
        this.player = {
            type: {
                Letv: null,
            },
            sdk: {
                startUp: function() {}, // 启动
                pausePlay: function() {}, // 暂停视频
                resumePlay: function() {}, // 恢复视频
                rePlay: function() {}, // 重播视频
                getVersion: function() {}, // 获得版本
                getVideoTime: function() {}, // 获得视频时间
                getPlayRecord: function() {}, // 获得播放记录
                getPlayState: function() {}, // 获得播放状态
                setPoint: function() {}, // 设定点
                shutDown: function() {}, // 关掉
                closeVideo: function() {}, // 关闭视频
            },
            callback: {
                start: this.CallBack_StartPlay, // 视频开始播放
                pause: this.CallBack_PausePlay, // 视频暂停
                resume: this.CallBack_ResumePlay, // 视频恢复播放
                fullscreen: this.CallBack_FullScreenPlay, // 视频全屏播放
            },
        };
        this.event = $.extend(true, {
            start: function() {},
            parse: function() {},
        }, arg_config.event);
        this.Init();
    };
    window.VideoUse.prototype = {
        Init: function() {
            var self = this;

            self.ObjectInit_LetvPlayer();

            self.BindPageElementEvent();
        },
        ObjectInit_LetvPlayer: function() {
            var self = this;
            self.player.type.Letv = new CloudVodPlayer();
            console.log("video address: ", self.info.address);
            window.Letv_CallbackJs_ID_PlayBoxStyleObj = function(type, data) {
                switch(type) {
                    case "videoStart":
                        console.log("视频开始播放");
                        self.player.callback.start(self);
                        break;
                    case "videoPause":
                        console.log("视频暂停");
                        self.player.callback.pause(self);
                        break;
                    case "videoResume":
                        console.log("视频恢复播放");
                        self.player.callback.resume(self);
                        break;
                    case "fullscreen":
                        console.log("视频全屏播放");
                        self.player.callback.fullscreen(self);
                        break;
                }
            }
            var jq_element_main = self.control.box.main;
            var v_w = jq_element_main.width();
            var v_h = parseInt((v_w/16) * 9);
            var customize_config = $.extend(true, {}, {
                fullscreen: "0",
                pano: "0",
                playsinline: "0",
                autoSize: "0",
                autoplay: self.info.auto_play ? '1': '0',
                uu: "",
                vu: "",
                pu: "",
                type: "h5",
                width: v_w,
                height: v_h,
                lang: "zh_CN",
                callbackJs: 'Letv_CallbackJs_ID_PlayBoxStyleObj',
            }, self.info.address);
            console.info('Init Letv Player customize_config: ', customize_config);
            self.player.type.Letv.init(customize_config, jq_element_main[0].id);

            self.player.sdk.closeVideo = self.player.type.Letv.sdk.closeVideo; // 关闭视频
            self.player.sdk.getPlayRecord = self.player.type.Letv.sdk.getPlayRecord; // 获得播放记录
            self.player.sdk.getPlayState = self.player.type.Letv.sdk.getPlayState; // 获得播放状态
            self.player.sdk.getVersion = self.player.type.Letv.sdk.getVersion; // 获得版本
            self.player.sdk.getVideoTime = self.player.type.Letv.sdk.getVideoTime; // 获得视频时间
            self.player.sdk.pausePlay = self.player.type.Letv.sdk.pauseVideo; // 暂停视频
            self.player.sdk.rePlay = self.player.type.Letv.sdk.replayVideo; // 重播视频
            self.player.sdk.resumePlay = self.player.type.Letv.sdk.resumeVideo; // 恢复视频
            self.player.sdk.setPoint = self.player.type.Letv.sdk.setPoint; // 设定点
            self.player.sdk.shutDown = self.player.type.Letv.sdk.shutDown; // 关掉
            self.player.sdk.startUp = self.player.type.Letv.sdk.startUp; // 启动
        },
        BindPageElementEvent: function() {
            var self = this;
            self.control.box.cover_img.on('click', function() {
                self.control.box.video.addClass(self.info.class.loading); // 开启加载状态
                self.player.sdk.startUp(); // 启动播放
            });
            self.control.btn.close_fullscreen.on('click', function() {
                self.player.sdk.pausePlay();
            });
        },
        CallBack_StartPlay: function(self) {
            self.control.box.video.removeClass(self.info.class.loading);
            self.control.box.video.addClass(self.info.class.play);
            self.Set_FullScreen_Status(true); // 开启全屏
            self.event.start();
        },
        CallBack_PausePlay: function(self) {
            self.Set_FullScreen_Status(false); // 关闭全屏
            self.event.parse();
        },
        CallBack_ResumePlay: function(self) {
            self.Set_FullScreen_Status(true);
            self.event.start();
        },
        CallBack_FullScreenPlay: function(self) {
            self.Set_FullScreen_Status(false);
        },
        Set_FullScreen_Status: function(isOpen) {
            var self = this;
            if (self.info.is_blackShady_fullScreen) {
                window.PageInfo.DocumentLock(isOpen);
                window.PageInfo.EnabelFullScreenModel(isOpen);
                if (isOpen) {
                    self.control.box.video.addClass(self.info.class.fullscreen);
                } else {
                    self.control.box.video.removeClass(self.info.class.fullscreen);
                }
            }
        },
    };
})();

// MapUse
(function() {
    window.MapUse = function(arg_config) {
        this.control = {
            box: {
                // 地图区域
                map: $("#ID_Box_MapRegion"),
                // iframe 控件装入 链接地图文件
                iframe: $("#ID_IframeContainer_Map"),
                // 遮罩图片
                cover_img: $("#ID_Box_Map_CoverImg"),
            },
            btn: {
                // 关闭全屏按钮
                close_fullscreen: $("#ID_Btn_MapCloseFullScreen"),
            },
        };
        this.info = $.extend(true, {
            class: {
                show: 'Show',
                pc_coverimg: 'PCCoverImg',
                fullscreen: 'FullScreen',
            },
            show_type: 'normal',
        }, arg_config.info);
        this.event = $.extend(true, {
            open: function(typedesmsg) {},
            close: function(typedesmsg) {},
        }, arg_config.event);
        this.Init();
    }
    window.MapUse.prototype = {
        Init: function() {
            var self = this;
            self.DecideType();
        },
        DecideType: function() {
            var self = this;
            switch(self.info.show_type) {
                case "notcover":
                    console.log('没有封面图片');
                    self.Type_NotCover();
                    break;
                case "normal":
                    console.log('正常的盒子里存放地图内容');
                    self.Type_Normal();
                    break;
                case "link_cover":
                    console.log('带有链接的封面图片');
                    self.Type_LinkCover();
                    break;
                case "fullscreen":
                    console.log('全屏展示');
                    self.Type_FullScreen();
                    break;
            }
        },
        Type_NotCover: function() {
            var self = this;
            self.Assignment_Src();
            self.Set_Show_State(true);
        },
        Type_Normal: function() {
            var self = this;
            self.Assignment_Src();
            self.control.box.cover_img.on('click', function() {
                // 展示地图信息
                self.Set_Show_State(true);

                var typedesmsg = '正常地图';
                self.event.open(typedesmsg);
                self.event.close(typedesmsg);
            });
        },
        Type_LinkCover: function() {
            var self = this;
            // 展示 PC 封面
            self.Set_PCCoverImg_State(true);
            self.control.box.cover_img.on('click', function() {
                var typedesmsg = '链接地图新开窗口';
                self.event.open(typedesmsg);
                self.event.close(typedesmsg);
                var link = self.control.box.iframe.attr('data-src');
                window.open(link);
            });
        },
        Type_FullScreen: function() {
            var self = this;
            self.Set_FullScreen_Status(true);
            self.Assignment_Src();
            function method(isEnable) {
                window.PageInfo.DocumentLock(isEnable);
                window.PageInfo.EnabelFullScreenModel(isEnable);
                self.Set_Show_State(isEnable);
            };
            var typedesmsg = '全屏地图模式';
            self.control.box.cover_img.on('click', function() {
                method(true);
                self.event.open(typedesmsg);
            });
            self.control.btn.close_fullscreen.on('click', function() {
                method(false);
                self.event.close(typedesmsg);
            });
        },
        Set_Show_State: function(isEnable) {
            var self = this;
            self.Set_Status(isEnable, self.info.class.show);
        },
        Set_PCCoverImg_State: function(isEnable) {
            var self = this;
            self.Set_Status(isEnable, self.info.class.pc_coverimg);
        },
        Set_FullScreen_Status: function(isEnable) {
            var self = this;
            self.Set_Status(isEnable, self.info.class.fullscreen);
        },
        Set_Status: function(isEnable, class_value_name) {
            var self = this;
            if (isEnable) {
                self.control.box.map.addClass(class_value_name);
            } else {
                self.control.box.map.removeClass(class_value_name);
            }
        },
        Assignment_Src: function() {
            var self = this;
            setTimeout(function() {
                var cbiframe = self.control.box.iframe;
                cbiframe.attr('src', cbiframe.attr('data-src'));
                console.log("src路径: ", cbiframe.attr('src'));
            }, 0.2 * 1000);
        },
    };
})();
