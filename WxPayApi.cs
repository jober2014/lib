using System;
using WxCore.Command;
using WxCore.Models;

namespace WxCore
{
    /// <summary>
    /// 微信支付API
    /// </summary>
    public class WxPayApi
    {
        private string url;
        public WxPayApi(string url)
        {
            this.url = url;
        }
        /// <summary>
        /// 统一下单接口
        /// </summary>
        /// <param name="payMode">参数</param>
        /// <returns></returns>
        public ReturnModels<xml> Unifiedorder(string key, PayMode payMode)
        {
            var result = new ReturnModels<xml>();
            if (payMode != null)
            {
                try
                {
                    payMode.sign = CommTool.MD5MakeSigne(payMode.ToDictionary(), key);
                    var parmxml = payMode.ToDictionary();
                    var rdata = HttpSendResult.SendRequest(this.url, parmxml.ToXML(), "POST", "text/xml");
                    result.RetrnData = rdata.Deserialize<xml>();
                }
                catch (Exception ex)
                {
                    result.HasError = true;
                    result.Message = ex.Message;
                }

            }
            else
            {
                result.HasError = true;
                result.Message = "支付参数为空对象。";
            }
            return result;

        }


    }
}
