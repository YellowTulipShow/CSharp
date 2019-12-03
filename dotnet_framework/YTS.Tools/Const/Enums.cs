using System;

namespace YTS.Tools.Const
{
    /// <summary>
    /// 自定义枚举集合
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// 用途代码标记
        /// </summary>
        public enum UseCodeMark
        {
            /// <summary>
            /// 用户
            /// </summary>
            [Explain(@"用户")]
            User = 1001,

            /// <summary>
            /// 组别
            /// </summary>
            [Explain(@"组别")]
            Group = 1002,

            /// <summary>
            /// 角色
            /// </summary>
            [Explain(@"角色")]
            Role = 1003,

            /// <summary>
            /// 权限
            /// </summary>
            [Explain(@"权限")]
            Permissions = 1004,

            /// <summary>
            /// 文章
            /// </summary>
            [Explain(@"文章")]
            Article = 2001,

            /// <summary>
            /// 文件
            /// </summary>
            [Explain(@"文件")]
            File = 3001,
        }

        /// <summary>
        /// 管理操作
        /// </summary>
        public enum ManagementOperate
        {
            /// <summary>
            /// 所有
            /// </summary>
            [Explain(@"所有")]
            All,
            /// <summary>
            /// 显示
            /// </summary>
            [Explain(@"显示")]
            Show,
            /// <summary>
            /// 查看
            /// </summary>
            [Explain(@"查看")]
            View,
            /// <summary>
            /// 添加
            /// </summary>
            [Explain(@"添加")]
            Add,
            /// <summary>
            /// 修改
            /// </summary>
            [Explain(@"修改")]
            Edit,
            /// <summary>
            /// 删除
            /// </summary>
            [Explain(@"删除")]
            Delete,
            /// <summary>
            /// 审核
            /// </summary>
            [Explain(@"审核")]
            Audit,
            /// <summary>
            /// 回复
            /// </summary>
            [Explain(@"回复")]
            Reply,
            /// <summary>
            /// 确认
            /// </summary>
            [Explain(@"确认")]
            Confirm,
            /// <summary>
            /// 取消
            /// </summary>
            [Explain(@"取消")]
            Cancel,
            /// <summary>
            /// 作废
            /// </summary>
            [Explain(@"作废")]
            Invalid,
            /// <summary>
            /// 生成
            /// </summary>
            [Explain(@"生成")]
            Build,
            /// <summary>
            /// 安装
            /// </summary>
            [Explain(@"安装")]
            Instal,
            /// <summary>
            /// 卸载
            /// </summary>
            [Explain(@"卸载")]
            UnLoad,
            /// <summary>
            /// 登录
            /// </summary>
            [Explain(@"登录")]
            Login,
            /// <summary>
            /// 备份
            /// </summary>
            [Explain(@"备份")]
            Back,
            /// <summary>
            /// 还原
            /// </summary>
            [Explain(@"还原")]
            Restore,
            /// <summary>
            /// 替换
            /// </summary>
            [Explain(@"替换")]
            Replace,
            /// <summary>
            /// 复制
            /// </summary>
            [Explain(@"复制")]
            Copy,
            /// <summary>
            /// 导入
            /// </summary>
            [Explain(@"导入")]
            Import,
            /// <summary>
            /// 导出
            /// </summary>
            [Explain(@"导出")]
            Export,
        }

        /// <summary>
        /// 系统导航
        /// </summary>
        public enum SystemNavigation
        {
            /// <summary>
            /// 系统后台菜单
            /// </summary>
            [Explain(@"系统后台菜单")]
            System,
            /// <summary>
            /// 会员中心导航
            /// </summary>
            [Explain(@"会员中心导航")]
            Users,
            /// <summary>
            /// 网站主导航
            /// </summary>
            [Explain(@"网站主导航")]
            WebSite,
        }

        /// <summary>
        /// 用户生成码
        /// </summary>
        public enum UserGeneratedCode
        {
            /// <summary>
            /// 注册验证
            /// </summary>
            [Explain(@"注册验证")]
            RegVerify,
            /// <summary>
            /// 邀请注册
            /// </summary>
            [Explain(@"邀请注册")]
            Register,
            /// <summary>
            /// 取回密码
            /// </summary>
            [Explain(@"取回密码")]
            Password,
        }

        /// <summary>
        /// 金额类型
        /// </summary>
        public enum AmountType
        {
            /// <summary>
            /// 系统赠送
            /// </summary>
            [Explain(@"系统赠送")]
            SysGive,
            /// <summary>
            /// 在线充值
            /// </summary>
            [Explain(@"在线充值")]
            Recharge,
            /// <summary>
            /// 用户消费
            /// </summary>
            [Explain(@"用户消费")]
            Consumption,
            /// <summary>
            /// 后台加款
            /// </summary>
            [Explain(@"后台加款")]
            AdminGive,
            /// <summary>
            /// 后台扣款
            /// </summary>
            [Explain(@"后台扣款")]
            AdminDraw,
            /// <summary>
            /// 用户提现
            /// </summary>
            [Explain(@"用户提现")]
            DrawCash,
            /// <summary>
            /// 佣金分成
            /// </summary>
            [Explain(@"佣金分成")]
            Commission,
            /// <summary>
            /// 购买商品
            /// </summary>
            [Explain(@"购买商品")]
            BuyGoods,
            /// <summary>
            /// 必抢区商品
            /// </summary>
            [Explain(@"必抢区商品")]
            TakeDelivery,
            /// <summary>
            /// 购买必抢区商品
            /// </summary>
            [Explain(@"购买必抢区商品")]
            FlashSale,
            /// <summary>
            /// 积分兑换
            /// </summary>
            [Explain(@"积分兑换")]
            Convert,
            /// <summary>
            /// 购买虚拟课程
            /// </summary>
            [Explain(@"购买虚拟课程")]
            BuyXNCourse,
            /// <summary>
            /// 购买优惠券
            /// </summary>
            [Explain(@"购买优惠券")]
            BuyCoupons,
            /// <summary>
            /// 购买粉丝团VIP
            /// </summary>
            [Explain(@"购买粉丝团VIP")]
            BuyFandvip,
            /// <summary>
            /// 购买朋友圈VIP
            /// </summary>
            [Explain(@"购买朋友圈VIP")]
            BuyMomentvip,
            /// <summary>
            /// 购买股东权限
            /// </summary>
            [Explain(@"购买股东权限")]
            BuyPartner,
            /// <summary>
            /// 购买代理
            /// </summary>
            [Explain(@"购买代理")]
            BuyAgent,
        }

        /// <summary>
        /// 性别枚举
        /// </summary>
        public enum SexEnum
        {
            /// <summary>
            /// 保密
            /// </summary>
            [Explain(@"保密")]
            Secrecy = 0,
            /// <summary>
            /// 男
            /// </summary>
            [Explain(@"男")]
            Male = 1,
            /// <summary>
            /// 女
            /// </summary>
            [Explain(@"女")]
            Female = 2,
        }

        /// <summary>
        /// 是否布尔
        /// </summary>
        public enum IsBoolean
        {
            /// <summary>
            /// 是
            /// </summary>
            [Explain(@"是")]
            No = 0,
            /// <summary>
            /// 否
            /// </summary>
            [Explain(@"否")]
            Yes = 1,
        }
    }
}
