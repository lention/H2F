using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace H2F.Framework.Common.Helper
{
    /// <summary>
    /// 功能：AES加解密帮助类
    /// 作者：何蛟
    /// 创建日期：2018-12-09  13:05
    /// </summary>
    public static class AESHepler
    {
        private const string _defaultKey = "WS2TXHZAD9J5OCOX";
        private const string _defaultIV = "V0VE41G3WXSZRQKG";
        private enum CryptoType
        {
            Encrypt,//加密
            Decrypt//解密
        }

        /// <summary>
        /// 使用AES进行加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AESEncrypt(this string data, string key = "", string iv = "")
        {
            _initKeyAndIV(ref key, ref iv);
            return _processData(data, key, CryptoType.Encrypt, iv);
        }

        /// <summary>
        /// 使用AES进行解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public static string AESDecrypt(this string data, string key = "", string iv = "")
        {
            _initKeyAndIV(ref key, ref iv);
            return _processData(data, key, CryptoType.Decrypt, iv);
        }



        #region 内部方法
        /// <summary>
        /// 验证key和iv的正确性，没有传入则使用默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        private static void _initKeyAndIV(ref string key, ref string iv)
        {
            if (string.IsNullOrWhiteSpace(key) || key.Length < 16)
            {
                key = _defaultKey;
            }

            if (string.IsNullOrWhiteSpace(iv) || key.Length < 16)
            {
                iv = _defaultIV;
            }
        }

        private static RijndaelManaged _createRijndaelManaged(byte[] keyArray, byte[] ivArray)
        {
            RijndaelManaged rjMgr = new RijndaelManaged();
            rjMgr.Key = keyArray;
            rjMgr.IV = ivArray;
            rjMgr.Mode = CipherMode.CBC;
            rjMgr.Padding = PaddingMode.PKCS7;
            return rjMgr;
        }
        private static string _processData(string data, string key, CryptoType cType, string iv = "")
        {
            if (string.IsNullOrEmpty(iv))
            {
                iv = key;
            }

            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = UTF8Encoding.UTF8.GetBytes(iv);
            byte[] toProcessArrary = cType == CryptoType.Encrypt ? UTF8Encoding.UTF8.GetBytes(data) : Convert.FromBase64String(data);

            ICryptoTransform cTransform;
            switch (cType)
            {
                case CryptoType.Encrypt:
                    cTransform = _createRijndaelManaged(keyArray, ivArray).CreateEncryptor();
                    break;
                case CryptoType.Decrypt:
                    cTransform = _createRijndaelManaged(keyArray, ivArray).CreateDecryptor();
                    break;
                default:
                    throw new Exception("没有找到匹配的CryptoType类型。");
            }

            byte[] resultArray = cTransform.TransformFinalBlock(toProcessArrary, 0, toProcessArrary.Length);
            //返回不同结果
            if (cType == CryptoType.Encrypt)
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            else
                return UTF8Encoding.UTF8.GetString(resultArray).Replace("\0", "");
        }
        #endregion
    }
}
