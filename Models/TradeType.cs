namespace WxCore.Models
{
    /// <summary>
    /// 微信交易类型
    /// </summary>
    public enum TradeType
    {
        /// <summary>
        /// JSAPI支付（或小程序支付）
        /// </summary>
        JSAPI = 0,
        /// <summary>
        /// Native支付
        /// </summary>
        NATIVE = 1,
        /// <summary>
        /// app支付
        /// </summary>
        APP = 2,
        /// <summary>
        /// H5支付
        /// </summary>
        MWEB = 3

    }
}
