using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WxCore.Command
{
    /// <summary>
    /// http请求类
    /// </summary>
    public class HttpSendResult
    {
        static HttpSendResult()
        {
            //并发线程连接数限制
            ServicePointManager.DefaultConnectionLimit = 256;
        }
        #region 通讯函数
        /// <summary>
        /// 通讯函数
        /// </summary>
        /// <param name="url">请求Url</param>
        /// <param name="para">请求参数</param>
        /// <param name="method">请求方式GET/POST：POST传参必须为JSON格式</param>
        /// <returns></returns>
        public static string SendRequest(string url, string para, string method,string ContentType= "application/json")
        {
            string strResult = "";
            if (url == null || url == "")
                return null;
            if (method == null || method == "")
                method = "GET";
            // GET方式
            if (method.ToUpper() == "GET")
            {
                try
                {
                    if (url.ToLower().Contains("https"))
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    }
                    else
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    }

                    var ruldata = System.Web.HttpUtility.UrlEncode(para, Encoding.UTF8);
                    WebRequest wrq = WebRequest.Create(url + ruldata);
                    wrq.Method = "GET";
                    WebResponse wrp = wrq.GetResponse();
                    StreamReader sr = new StreamReader(wrp.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                    strResult = sr.ReadToEnd();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            // POST方式
            if (method.ToUpper() == "POST")
            {
                WebRequest req = WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = ContentType;
                if (url.ToLower().Contains("https"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }
                else
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                }
                byte[] SomeBytes = null;
                if (para != null)
                {
                    SomeBytes = Encoding.UTF8.GetBytes(para);
                    req.ContentLength = SomeBytes.Length;
                    Stream newStream = req.GetRequestStream();
                    newStream.Write(SomeBytes, 0, SomeBytes.Length);
                    newStream.Close();
                }
                else
                {
                    req.ContentLength = 0;
                }
                try
                {
                    WebResponse result = req.GetResponse();
                    Stream ReceiveStream = result.GetResponseStream();
                    Byte[] read = new Byte[512];
                    int bytes = ReceiveStream.Read(read, 0, 512);
                    while (bytes > 0)
                    {
                        Encoding encode = Encoding.GetEncoding("utf-8");
                        strResult += encode.GetString(read, 0, bytes);
                        bytes = ReceiveStream.Read(read, 0, 512);
                    }
                    return strResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return strResult;
        }

        /// <summary>
        /// HTTP为GET请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="parms">参数</param>
        /// <param name="ContentType">文本类型</param>
        /// <returns></returns>
        public static string Get(String url, String parms, String ContentType = "application/x-www-form-urlencoded")
        {

            String strResult = null;
            try
            {
                if (url.ToLower().Contains("https"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                }
                else
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                }
                var ruldata = url + parms;
                WebRequest wrq = WebRequest.Create(ruldata);
                wrq.Method = "GET";
                wrq.ContentType = ContentType;
                WebResponse wrp = wrq.GetResponse();
                StreamReader sr = new StreamReader(wrp.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                strResult = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return System.Web.HttpUtility.UrlDecode(strResult);
        }
        /// <summary>
        /// 指定Post请求获取全部字符串  
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="dic">参数键值对</param>
        /// <returns></returns>
        public static string Post(string url, Dictionary<string, string> dic, String ContentType = "application/x-www-form-urlencoded")
        {
            string result = "";
            if (url.ToLower().Contains("https"))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
            else
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            }
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = ContentType;
            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        /// <summary>  
        /// 指定Post地址使用Get（无参数请求）
        /// </summary>  
        /// <param name="url">请求后台地址</param>  
        /// <returns></returns>  
        public static string Post(string url, String ContentType = "application/x-www-form-urlencoded")
        {
            string result = "";
            if (url.ToLower().Contains("https"))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
            else
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            }
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = ContentType;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取内容  
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return System.Web.HttpUtility.UrlDecode(result);
        }
        #endregion


    }
}