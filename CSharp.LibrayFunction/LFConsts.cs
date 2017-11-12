﻿using System;

namespace CSharp.LibrayFunction
{
    /// <summary>
    /// 类库方法中: 常量组: 键名
    /// </summary>
    public class LFKeys
    {
        /// <summary>
        /// 系统版本号
        /// </summary>
        public const string SystemVersionNumber = "1.0.0";

        /// <summary>
        /// 系统作者
        /// </summary>
        public const string SystemAuthor = "YellowTulipShow";

        /// <summary>
        /// 配置文件[站点]路径(从web.config appSettings节点中读取)
        /// </summary>
        public const string FileSiteConfigPath = "SiteConfigpath";

        /// <summary>
        /// 配置文件[URL重写]路径(从web.config appSettings节点中读取)
        /// </summary>
        public const string FileUrlsConfigPath = "Urlspath";

        #region ====== Table DateTime Format ======
        /// <summary>
        /// 数据表时间格式 精确至: 毫秒 (取后三位)
        /// </summary>
        public const string TABLE_DATETIME_FORMAT_MILLISECOND = "yyyy-MM-dd HH:mm:ss.fff";
        /// <summary>
        /// 数据表时间格式 精确至: 秒
        /// </summary>
        public const string TABLE_DATETIME_FORMAT_SECOND = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 数据表时间格式 精确至: 分钟
        /// </summary>
        public const string TABLE_DATETIME_FORMAT_MINUTE = "yyyy-MM-dd HH:mm";
        /// <summary>
        /// 数据表时间格式 精确至: 小时
        /// </summary>
        public const string TABLE_DATETIME_FORMAT_HOUR = "yyyy-MM-dd HH";
        /// <summary>
        /// 数据表时间格式 精确至: 天
        /// </summary>
        public const string TABLE_DATETIME_FORMAT_DAY = "yyyy-MM-dd";
        #endregion

        #region 以前的
        //系统版本
        /// <summary>
        /// 版本号全称
        /// </summary>
        public const string ASSEMBLY_VERSION = "4.0.0";
        /// <summary>
        /// 版本年号
        /// </summary>
        public const string ASSEMBLY_YEAR = "2015";
        //File======================================================
        /// <summary>
        /// 插件配制文件名
        /// </summary>
        public const string FILE_PLUGIN_XML_CONFING = "plugin.config";
        /// <summary>
        /// 站点配置文件名
        /// </summary>
        public const string FILE_SITE_XML_CONFING = "Configpath";
        /// <summary>
        /// URL配置文件名
        /// </summary>
        public const string FILE_URL_XML_CONFING = "Urlspath";
        /// <summary>
        /// 用户配置文件名
        /// </summary>
        public const string FILE_USER_XML_CONFING = "Userpath";
        /// <summary>
        /// 订单配置文件名
        /// </summary>
        public const string FILE_ORDER_XML_CONFING = "Orderpath";
        /// <summary>
        /// 升级代码
        /// </summary>
        public const string FILE_URL_UPGRADE_CODE = "";
        /// <summary>
        /// 消息代码
        /// </summary>
        public const string FILE_URL_NOTICE_CODE = "";

        //Directory==================================================
        /// <summary>
        /// ASPX目录名
        /// </summary>
        public const string DIRECTORY_REWRITE_ASPX = "aspx";
        /// <summary>
        /// HTML目录名
        /// </summary>
        public const string DIRECTORY_REWRITE_HTML = "html";
        /// <summary>
        /// 插件目录名
        /// </summary>
        public const string DIRECTORY_REWRITE_PLUGIN = "plugin";

        //Cache======================================================
        /// <summary>
        /// 站点配置
        /// </summary>
        public const string CACHE_SITE_CONFIG = "dt_cache_site_config";
        /// <summary>
        /// 用户配置
        /// </summary>
        public const string CACHE_USER_CONFIG = "dt_cache_user_config";
        /// <summary>
        /// 订单配置
        /// </summary>
        public const string CACHE_ORDER_CONFIG = "dt_cache_order_config";
        /// <summary>
        /// HttpModule映射类
        /// </summary>
        public const string CACHE_SITE_HTTP_MODULE = "dt_cache_http_module";
        /// <summary>
        /// 绑定域名
        /// </summary>
        public const string CACHE_SITE_HTTP_DOMAIN = "dt_cache_http_domain";
        /// <summary>
        /// 站点一级目录名
        /// </summary>
        public const string CACHE_SITE_DIRECTORY = "dt_cache_site_directory";
        /// <summary>
        /// 站点ASPX目录名
        /// </summary>
        public const string CACHE_SITE_ASPX_DIRECTORY = "dt_cache_site_aspx_directory";
        /// <summary>
        /// URL重写映射表
        /// </summary>
        public const string CACHE_SITE_URLS = "dt_cache_site_urls";
        /// <summary>
        /// URL重写LIST列表
        /// </summary>
        public const string CACHE_SITE_URLS_LIST = "dt_cache_site_urls_list";
        /// <summary>
        /// 升级通知
        /// </summary>
        //public const string CACHE_OFFICIAL_UPGRADE = "dt_official_upgrade";
        /// <summary>
        /// 官方消息
        /// </summary>
        //public const string CACHE_OFFICIAL_NOTICE = "dt_official_notice";

        //Session=====================================================
        /// <summary>
        /// 网页验证码
        /// </summary>
        public const string SESSION_CODE = "dt_session_code";
        /// <summary>
        /// 短信验证码
        /// </summary>
        public const string SESSION_SMS_CODE = "dt_session_sms_code";
        /// <summary>
        /// 后台管理员
        /// </summary>
        public const string SESSION_ADMIN_INFO = "dt_session_admin_info";
        /// <summary>
        /// 会员用户
        /// </summary>
        public const string SESSION_USER_INFO = "dt_session_user_info";

        //Cookies=====================================================
        /// <summary>
        /// 防重复顶踩KEY
        /// </summary>
        public const string COOKIE_DIGG_KEY = "dt_cookie_digg_key";
        /// <summary>
        /// 防重复评论KEY
        /// </summary>
        public const string COOKIE_COMMENT_KEY = "dt_cookie_comment_key";
        /// <summary>
        /// 记住会员用户名
        /// </summary>
        public const string COOKIE_USER_NAME_REMEMBER = "dt_cookie_user_name_remember";
        /// <summary>
        /// 记住会员密码
        /// </summary>
        public const string COOKIE_USER_PWD_REMEMBER = "dt_cookie_user_pwd_remember";
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public const string COOKIE_USER_MOBILE = "dt_cookie_user_mobile";
        /// <summary>
        /// 用户电子邮箱
        /// </summary>
        public const string COOKIE_USER_EMAIL = "dt_cookie_user_email";
        /// <summary>
        /// 购物车
        /// </summary>
        public const string COOKIE_SHOPPING_CART = "dt_cookie_shopping_cart";
        /// <summary>
        /// 结账清单
        /// </summary>
        public const string COOKIE_SHOPPING_BUY = "dt_cookie_shopping_buy";
        /// <summary>
        /// 返回上一页
        /// </summary>
        public const string COOKIE_URL_REFERRER = "dt_cookie_url_referrer";

        /// <summary>
        /// 分享人信息
        /// </summary>
        public const string COOKIE_SHARER = "dt_cookie_sharer";
        #endregion
    }

    /// <summary>
    /// 类库方法中: 常量组: 枚举组
    /// </summary>
    public class LFEnums
    {
        /// <summary>
        /// 统一管理操作枚举
        /// </summary>
        public enum ActionEnum
        {
            /// <summary>
            /// 所有
            /// </summary>
            All,
            /// <summary>
            /// 显示
            /// </summary>
            Show,
            /// <summary>
            /// 查看
            /// </summary>
            View,
            /// <summary>
            /// 添加
            /// </summary>
            Add,
            /// <summary>
            /// 修改
            /// </summary>
            Edit,
            /// <summary>
            /// 删除
            /// </summary>
            Delete,
            /// <summary>
            /// 审核
            /// </summary>
            Audit,
            /// <summary>
            /// 回复
            /// </summary>
            Reply,
            /// <summary>
            /// 确认
            /// </summary>
            Confirm,
            /// <summary>
            /// 取消
            /// </summary>
            Cancel,
            /// <summary>
            /// 作废
            /// </summary>
            Invalid,
            /// <summary>
            /// 生成
            /// </summary>
            Build,
            /// <summary>
            /// 安装
            /// </summary>
            Instal,
            /// <summary>
            /// 卸载
            /// </summary>
            UnLoad,
            /// <summary>
            /// 登录
            /// </summary>
            Login,
            /// <summary>
            /// 备份
            /// </summary>
            Back,
            /// <summary>
            /// 还原
            /// </summary>
            Restore,
            /// <summary>
            /// 替换
            /// </summary>
            Replace,
            /// <summary>
            /// 复制
            /// </summary>
            Copy,
            /// <summary>
            /// 导入
            /// </summary>
            Import,
            /// <summary>
            /// 导出
            /// </summary>
            Export
        }

        /// <summary>
        /// 系统导航菜单类别枚举
        /// </summary>
        public enum NavigationEnum
        {
            /// <summary>
            /// 系统后台菜单
            /// </summary>
            System,
            /// <summary>
            /// 会员中心导航
            /// </summary>
            Users,
            /// <summary>
            /// 网站主导航
            /// </summary>
            WebSite
        }

        /// <summary>
        /// 用户生成码枚举
        /// </summary>
        public enum CodeEnum
        {
            /// <summary>
            /// 注册验证
            /// </summary>
            RegVerify,
            /// <summary>
            /// 邀请注册
            /// </summary>
            Register,
            /// <summary>
            /// 取回密码
            /// </summary>
            Password
        }

        /// <summary>
        /// 金额类型枚举
        /// </summary>
        public enum AmountTypeEnum
        {
            /// <summary>
            /// 系统赠送
            /// </summary>
            SysGive,
            /// <summary>
            /// 在线充值
            /// </summary>
            Recharge,
            /// <summary>
            /// 用户消费
            /// </summary>
            Consumption,
            /// <summary>
            /// 后台加款
            /// </summary>
            AdminGive,
            /// <summary>
            /// 后台扣款
            /// </summary>
            AdminDraw,
            /// <summary>
            /// 用户提现
            /// </summary>
            DrawCash,
            /// <summary>
            /// 佣金分成
            /// </summary>
            Commission,
            /// <summary>
            /// 购买商品
            /// </summary>
            BuyGoods,
            /// <summary>
            /// 必抢区商品
            /// </summary>
            TakeDelivery,
            /// <summary>
            /// 购买必抢区商品
            /// </summary>
            FlashSale,
            /// <summary>
            /// 积分兑换
            /// </summary>
            Convert,
            /// <summary>
            /// 购买虚拟课程
            /// </summary>
            BuyXNCourse,
            /// <summary>
            /// 购买优惠券
            /// </summary>
            BuyCoupons,
            /// <summary>
            /// 购买粉丝团VIP
            /// </summary>
            BuyFandvip,
            /// <summary>
            /// 购买朋友圈VIP
            /// </summary>
            BuyMomentvip,
            /// <summary>
            /// 购买股东权限
            /// </summary>
            BuyPartner,
            /// <summary>
            /// 购买代理
            /// </summary>
            BuyAgent
        }
    }
}
