using System.Web;
using WxCore.Command;
using WxCore.Models;

namespace WxCore
{
    /// <summary>
    /// 微信辅助类
    /// </summary>
    public class WxApiHelper
    {
        /// <summary>
        /// 服务地址
        /// </summary>
        private string baseurl;

        public WxApiHelper(string baseurl)
        {
            this.baseurl = baseurl;
        }
        /// <summary>
        /// 受权URL拼接
        /// </summary>
        /// <param name="baseurl">基础受权地址如：https://open.weixin.qq.com/connect/oauth2/authorize </param>
        /// <param name="parm"></param>
        /// <returns></returns>
        public string GetCodeUrl(WxParm parm)
        {
            string result = null;
            result = baseurl + "?" + string.Format("appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}#wechat_redirect", parm.appid, HttpUtility.UrlEncode(parm.redirect_uri), parm.response_type, parm.scope, parm.state);
            return result;
        }
        /// <summary>
        /// 通过code换取网页授权access_token
        /// </summary>
        /// <param name="appid">公众号的唯一标识</param>
        /// <param name="secret">公众号的appsecret</param>
        /// <param name="code">填写第一步获取的code参数</param>
        /// <returns></returns>
        public string GetAccess_token(string appid, string secret, string code)
        {
            string result = null;
            var url = baseurl + "?" + string.Format("appid={0}&secret={1}&code={2}&grant_type=authorization_code", appid, secret, code);
            result = HttpSendResult.SendRequest(url, null, "get");
            return result;

        }
        /// <summary>
        /// 刷新access_token（如果需要）
        /// </summary>
        /// <param name="appid">公众号的唯一标识</param>
        /// <param name="refresh_token">填写通过access_token获取到的refresh_token参数</param>
        /// <returns></returns>
        public string Getrefresh_token(string appid, string refresh_token)
        {

            string result = null;
            var url = baseurl + "?" + string.Format("appid={0}&grant_type=refresh_token&refresh_token={1}", appid, refresh_token);
            result = HttpSendResult.SendRequest(url, null, "get");
            return result;
        }
        /// <summary>
        /// 获取jsapi_ticket，开发者必须在自己的服务全局缓存jsapi_ticket
        /// </summary>
        /// <param name="access_token">access_token</param>
        /// <returns></returns>
        public string GetJsapi_Ticket(string access_token,string Type)
        {
            string result = string.Empty;
            var url = baseurl + "?" + string.Format("access_token={0}&type={1}", access_token, Type);
            result = HttpSendResult.SendRequest(url, null, "get");
            return result;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="openid">普通用户标识，对该公众帐号唯一</param>
        /// <returns></returns>
        public string GetUserInfo(string access_token, string openid)
        {
            string result = null;
            var url = baseurl + "?" + string.Format("access_token={0}&openid={1}&lang=zh_CN", access_token, openid);
            result = HttpSendResult.SendRequest(url, null, "get");
            return result;

        }

    }
}
