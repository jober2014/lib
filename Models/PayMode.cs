﻿namespace WxCore.Models
{
    /// <summary>
    /// 支付模型
    /// </summary>
    public class PayMode
    {
        /// <summary>
        /// *公众账号ID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// *微信支付分配的商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 自定义参数，可以为终端设备号(门店号或收银设备ID)，PC网页或公众号内支付可以传"WEB"
        /// </summary>
        public string device_info { get; set; }
        /// <summary>
        /// *随机字符串，长度要求在32位以内。推荐随机数生成算法
        /// </summary>
        public string nonce_str { get; set; }

        /// <summary>
        /// 通过签名算法计算得出的签名值，详见签名生成算法
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 签名类型，默认为MD5，支持HMAC-SHA256和MD5。
        /// </summary>
        public string sign_type { get; set; }
        /// <summary>
        /// *商品描述
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 商品详细描述
        /// </summary>
        public string detail { get; set; }
        /// <summary>
        /// 附加数据，在查询API和支付通知中原样返回，可作为自定义参数使用。
        /// </summary>
        public string attach { get; set; }
        /// <summary>
        /// *商户系统内部订单号，要求32个字符内，只能是数字、大小写字母_-|* 且在同一个商户号下唯一
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 标价币种:CNY
        /// </summary>
        public string fee_type { get; set; }
        /// <summary>
        /// *订单总金额，单位为分
        /// </summary>
        public int total_fee { get; set; }
        /// <summary>
        /// *支持IPV4和IPV6两种格式的IP地址。调用微信支付API的机器IP
        /// </summary>
        public string spbill_create_ip { get; set; }
        /// <summary>
        /// 订单生成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。
        /// </summary>
        public string time_start { get; set; }
        /// <summary>
        /// 订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。
        /// </summary>
        public string time_expire { get; set; }
        /// <summary>
        /// 订单优惠标记，使用代金券或立减优惠功能时需要的参数:WXG
        /// </summary>
        public string goods_tag { get; set; }
        /// <summary>
        /// *异步接收微信支付结果通知的回调地址，通知url必须为外网可访问的url，不能携带参数。
        /// </summary>
        public string notify_url { get; set; }
        /// <summary>
        /// *交易类型:JSAPI -JSAPI支付/ NATIVE -Native支付/APP -APP支付
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// trade_type=NATIVE时，此参数必传。此参数为二维码中包含的商品ID，商户自行定义。
        /// </summary>
        public string product_id { get; set; }
        /// <summary>
        /// 上传此参数no_credit--可限制用户不能使用信用卡支付
        /// </summary>
        public string limit_pay { get; set; }
        /// <summary>
        /// trade_type=JSAPI时（即JSAPI支付），此参数必传，此参数为微信用户在商户对应appid下的唯一标识。
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// Y，传入Y时，支付成功消息和支付详情页将出现开票入口。需要在微信支付商户平台或微信公众平台开通电子发票功能，传此字段才可生效
        /// </summary>
        public string receipt { get; set; }
        /// <summary>
        /// 该字段常用于线下活动时的场景信息上报，支持上报实际门店信息，商户也可以按需求自己上报相关信息。该字段为JSON对象数据，对象格式为{"store_info":{"id": "门店ID","name": "名称","area_code": "编码","address": "地址" }} ，字段详细说明请点击行前的+展开
        /// </summary>
        public string scene_info { get; set; }
    }
}
