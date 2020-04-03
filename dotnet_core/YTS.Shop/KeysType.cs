namespace YTS.Shop
{
    public class KeysType
    {
        /// <summary>
        /// 用户金额变动记录操作类型
        /// </summary>
        public enum UserMoneyRecordOperateType
        {
            /// <summary>
            /// 充值
            /// </summary>
            Recharge = 1,

            /// <summary>
            /// 消费
            /// </summary>
            Consumption = 2,

            /// <summary>
            /// 退款
            /// </summary>
            Refund = 3,
        }
    }
}
