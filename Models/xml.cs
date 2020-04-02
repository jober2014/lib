namespace WxCore.Models
{
    /// <summary>
    /// 公共定单反回给果
    /// </summary>
    public class xml
    {
        /// <summary>
        /// 返回状态码
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 返回信息:正常为OK，否则为异常信息
        /// </summary>
        public string return_msg { get; set; }
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 业务结果:SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 预支付交易会话标识
        /// </summary>
        public string prepay_id { get; set; }
        /// <summary>
        /// 交易类型:调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP，,H5支付固定传MWEB
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// mweb_url为拉起微信支付收银台的中间页面，可通过访问该url来拉起微信客户端，完成支付,mweb_url的有效期为5分钟
        /// </summary>
        public string mweb_url { get; set; }

    }
}
