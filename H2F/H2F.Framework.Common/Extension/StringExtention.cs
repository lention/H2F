using H2F.Framework.Common.Helper;
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
        public static string ConvertConnectionString(this string conStr, string isEncrypted = "false")
        {
            bool needDecrypt = false;
            bool.TryParse(isEncrypted, out needDecrypt);
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^data\s+source");
            if (needDecrypt && !regex.Match(conStr.ToLower().Trim()).Success)
            {
                if (conStr.ToLower().StartsWith("*noencrypt*_"))
                {
                    return conStr.Remove(0, "*noencrypt*_".Length);
                }
                return AESHepler.AESDecrypt(conStr);
            }
            //如果是没有定义为使用了加密串，或者定义使用了加密串，但实际上并没有加密则原样返回
            return conStr;
        }
    }
}
