using System;
using System.Text.RegularExpressions;

namespace WxCore.Command
{
    /// <summary>
    /// 字符串正则匹配帮助类
    /// </summary>
    public static class RegexHelp
    {
        /// <summary>
        /// 返回单个正则匹配值
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regex">正则表达式字符串</param>
        /// <returns>字符串类型</returns>
        public static string ToRegexString(this string value, string regex)
        {

            if (value != null)
            {
                Regex re = new Regex(regex);
                Match m = re.Match(value);
                if (m.Success)
                {
                    return m.Value;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 返回正则匹配字符串数组
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regex">正刚表达式</param>
        /// <returns>字符串数组</returns>
        public static String[] ToRegexStringArray(this string value, string regex)
        {
            String[] array = { };
            if (value != null)
            {
                Regex rg = new Regex(regex, RegexOptions.Multiline);
                MatchCollection mc = rg.Matches(value);
                if (mc.Count > 0)
                {
                    int group = mc.Count;
                    array = new String[group];
                    for (int i = 0; i < group; i++)
                    {
                        array[i] = mc[i].Value;
                    }
                }

            }
            return array;
        }
        /// <summary>
        /// 判断是否匹配
        /// </summary>
        /// <param name="value">字符串</param>
        /// <param name="regex">正则表达式</param>
        /// <returns>bool</returns>
        public static bool IsRegex(this string value, string regex)
        {
            if (value != null)
            {
                Regex reg = new Regex(regex);
                return reg.IsMatch(value);
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 以字符串的方式分割字符数组
        /// </summary>
        /// <param name="str">要分割的字符</param>
        /// <param name="sp">字符串分割方式</param>
        /// <returns></returns>
        public static String[] RegexSplit(this string str, string sp)
        {
            if (str != null)
            {
                return Regex.Split(sp, str);
            }
            else
            {
                return null;
            }


        }
        /// <summary>
        /// 判断字符是否为空
        /// </summary>
        /// <param name="value">string</param>
        /// <returns>bool</returns>
        public static bool IsEmpty(this String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return true;
            }
            else
                return false;

        }

    }
}
