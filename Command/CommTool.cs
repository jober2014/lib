using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace WxCore.Command
{
    /// <summary>
    /// 公共工具
    /// </summary>
    public static class CommTool
    {

        /// <summary>
        ///  签名算法
        /// </summary>
        /// <param name="paramData">签名数据</param>
        /// <param name="signkey">密钥key</param>
        /// <returns></returns>
        public static string MD5MakeSigne(Dictionary<string, string> paramData, string signkey)
        {
            var result = "";
            if (paramData != null)
            {
                StringBuilder sb = new StringBuilder();
                IDictionary<String, String> dic = new SortedDictionary<String, String>(paramData);
                foreach (var item in dic)
                {
                    sb.Append(item.Key + "=" + item.Value + "&");
                }
                sb.Append("key=" + signkey);
                var md5 = MD5.Create().ComputeHash(UTF8Encoding.UTF8.GetBytes(sb.ToString()));
                sb.Clear();
                for (int i = 0; i < md5.Length; i++)
                {
                    sb.Append(md5[i].ToString("x2"));
                }
                result = sb.ToString().ToUpper();
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                var sk = String.Format("key={0}", signkey);
                var md5 = MD5.Create().ComputeHash(UTF8Encoding.UTF8.GetBytes(sk.ToString()));
                for (int i = 0; i < md5.Length; i++)
                {
                    sb.Append(md5[i].ToString("x2"));
                }
                result = sb.ToString().ToUpper();
            }
            return result;
        }

        /// <summary>
        /// 微信SHAI签名算法
        /// </summary>
        /// <param name="content">加密内容</param>     
        /// <returns></returns>
        public static String Sha1Sign(Dictionary<string, string> paramData)
        {
            string result = string.Empty;
            try
            {
                if (paramData != null)
                {
                    StringBuilder sb = new StringBuilder();
                    IDictionary<String, String> dic = new SortedDictionary<String, String>(paramData);
                    foreach (var item in dic)
                    {
                        sb.Append(item.Key + "=" + item.Value + "&");
                    }
                    SHA1 sha1 = new SHA1CryptoServiceProvider();
                    byte[] bytes_in = Encoding.UTF8.GetBytes(sb.ToString().TrimEnd('&'));
                    byte[] bytes_out = sha1.ComputeHash(bytes_in);
                    sha1.Dispose();
                    result = BitConverter.ToString(bytes_out);
                    result = result.Replace("-", "").ToLower();

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 时间戳扩展
        /// </summary>
        /// <param name="value">时间类型</param>
        /// <returns>反回时间戳，秒为单位</returns>
        public static long ToTimeStamp(this DateTime value)
        {
            TimeSpan ts = value - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        /// <summary>
        /// 将对象转为字典
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <returns></returns>
        public static Dictionary<string, string> ToDictionary(this Object obj)
        {
            var dic = new Dictionary<string, string>();
            try
            {
                if (obj != null && obj.GetType().IsClass)
                {
                    var data = obj.GetType().GetProperties();
                    foreach (var item in data)
                    {
                        if (item.GetValue(obj) == null)
                        {
                            continue;
                        }

                        if (item.GetType() == typeof(DateTime))
                        {
                            dic.Add(item.Name, ((DateTime)item.GetValue(obj)).ToString("yyyy-MM-dd HH:mm:ss"));
                        }
                        else
                        {
                            if (item.GetValue(obj) != null)
                            {
                                dic.Add(item.Name, item.GetValue(obj).ToString());
                            }

                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return dic;


        }

        /// <summary>
        /// 对像转XML
        /// </summary>
        /// <param name="dic">字典对象</param>
        /// <returns></returns>
        public static string ToXML(this Dictionary<string, string> dic)
        {
            var result = new StringBuilder();
            result.Append("<xml>");
            if (dic != null)
            {
                foreach (var item in dic)
                {
                    result.Append(string.Format("<{0}>{1}</{0}>", item.Key, item.Value));
                }

            }

            result.Append("</xml>");
            return result.ToString();

        }
        /// <summary>
        /// 转XML为不可转义数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToCDATA(this string str)
        {

            return String.Format("<![CDATA[{0}]]>", str);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(this string xml) where T : class, new()
        {
            T result = default(T);
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(typeof(T));
                    result = (T)xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }
        /// <summary>
        /// 将一个对象序列化为XML字符串
        /// </summary>
        /// <param name="o"> 要序列化的对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>序列化产生的XML字符串</returns>
        public static string XmlSerialize(this object o)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializeInternal(stream, o, Encoding.UTF8);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }


        /// <summary>
        /// 从XML字符串中反序列化对象
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="s">包含对象的XML字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserialize<T>(this string s, Encoding encoding)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentNullException("s");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(encoding.GetBytes(s)))
            {
                using (StreamReader sr = new StreamReader(ms, encoding))
                {
                    return (T)mySerializer.Deserialize(sr);
                }
            }
        }


        private static void XmlSerializeInternal(Stream stream, object o, Encoding encoding)
        {
            if (o == null)
                throw new ArgumentNullException("o");
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            XmlSerializer serializer = new XmlSerializer(o.GetType());
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.NewLineChars = "\r\n";
            settings.Encoding = encoding;
            settings.IndentChars = " ";
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                serializer.Serialize(writer, o);
                writer.Close();
            }
        }


        /// <summary>
        /// 将一个对象按XML序列化的方式写入到一个文件
        /// </summary>
        /// <param name="o">要序列化的对象</param>
        /// <param name="path">保存文件路径</param>
        /// <param name="encoding">编码方式</param>
        public static void XmlSerializeToFile(object o, string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");


            using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                XmlSerializeInternal(file, o, encoding);
            }
        }


        /// <summary>
        /// 读入一个文件，并按XML的方式反序列化对象。
        /// </summary>
        /// <typeparam name="T">结果对象类型</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>反序列化得到的对象</returns>
        public static T XmlDeserializeFromFile<T>(string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (encoding == null)
                throw new ArgumentNullException("encoding");


            string xml = File.ReadAllText(path, encoding);
            return XmlDeserialize<T>(xml, encoding);
        }

    }
}
