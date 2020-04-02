namespace WxCore.Models
{
    public class WxParm
    {
        /// <summary>
        /// 公众号的唯一标识
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 授权后重定向的回调链接地址， 请使用 urlEncode 对链接进行处理
        /// </summary>
        public string redirect_uri { get; set; }
        /// <summary>
        /// 返回类型，请填写code
        /// </summary>
        public string response_type { get; set; }
        /// <summary>
        /// 应用授权作用域，1.snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），
        /// 2.snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且， 即使在未关注的情况下，只要用户授权，也能获取其信息 ）
        /// </summary>
        public string scope { get; set; }
        /// <summary>
        /// 重定向后会带上state参数，开发者可以填写a-zA-Z0-9的参数值，最多128字节
        /// </summary>
        public string state { get; set; }
    }
}
