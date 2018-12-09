using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace H2F.Framework.Common.Extension
{
    /// <summary>
    /// 功能：常用string类型的扩展方法，简化常的一些操作
    /// 作者：何蛟
    /// 创建时间： 2018-12-9 11:48
    /// </summary>
    public static class StringExtention
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string Format(this string str, params object[] obj)
        {
            return string.Format(str, obj);
        }

        public static string Sub(this string str, int length)
        {
            if (str.IsNullOrEmpty())
            {
                return null;
            }
            else
            {
                return (str.Length > length) ? str.Substring(0, length) : str;
            }
        }

        public static string TryReplace(this string str, string oldStr, string newStr)
        {
            return str.IsNullOrEmpty() ? str : str.Replace(oldStr, newStr);
        }

        public static string RegexReplace(this string str, string pattern, string replacement)
        {
            return str.IsNullOrEmpty() ? str : Regex.Replace(str, pattern, replacement);
        }

        public static string ValueOfConnectionString(this string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].IsNull() ? null : ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static string ValueOfAppSetting(this string appSetingKey)
        {
            return ConfigurationManager.AppSettings[appSetingKey];
        }

        /// <summary>
        /// 解密connectionsetting里加密的字符串(根据配置IsEncryptConnectionString决定是否解密）
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static string ValueOfEncryptConnectionString(this string connectionName)
        {
            return DEncrypt.DecryptByConfig(
                   System.Configuration.ConfigurationManager.AppSettings["IsEncryptConnectionString"],
                   System.Configuration.ConfigurationManager.ConnectionStrings[connectionName].ConnectionString
                  );
        }
        /// <summary>
        /// 解密connectionsetting里加密的字符串(根据配置IsEncryptConnectionString决定是否解密）
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static string ValueOfEncryptAppSettingString(this string settingKey)
        {
            return DEncrypt.DecryptByConfig(
                   System.Configuration.ConfigurationManager.AppSettings["IsEncryptConnectionString"],
                   System.Configuration.ConfigurationManager.AppSettings[settingKey]
                  );
        }

        /// <summary>
        /// 根据配置决定是否解密某个连接字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ValueOfDecryptString(this string str)
        {
            return DEncrypt.DecryptByConfig(
                   System.Configuration.ConfigurationManager.AppSettings["IsEncryptConnectionString"],
                   str
                  );
        }
    }
}
