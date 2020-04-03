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

        /// <summary>
        /// 产品数量修改记录操作类型
        /// </summary>
        public enum ProductNumberRecordOperateType
        {
            /// <summary>
            /// 入库
            /// </summary>
            EnterWarehouse = 1,

            /// <summary>
            /// 售出
            /// </summary>
            Sold = 2,

            /// <summary>
            /// 报损
            /// </summary>
            Damaged = 3,
        }
    }
}
