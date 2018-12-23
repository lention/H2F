using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
//
using H2F.Standard.Common.Collections.Extensions;
using Newtonsoft.Json;
namespace H2F.Standard.Common.Extensions
{
    /// <summary>
    /// 功能：常用字符串扩展
    /// 作者：何蛟
    /// 创建时间： 2018-12-23 12:05
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 确保字符串以指定字符结尾
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string EnsureEndsWith(this string str, char c)
        {
            return EnsureEndsWith(str, c, StringComparison.Ordinal);
        }
        /// <summary>
        /// 确保字符串以指定字符结尾
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static string EnsureEndsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.EndsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return str + c;
        }

        /// <summary>
        /// 确保字符串以指定字符结尾
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string EnsureEndsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.EndsWith(c.ToString(culture), ignoreCase, culture))
            {
                return str;
            }

            return str + c;
        }

        /// <summary>
        /// 确保字符串以指定字符开始
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string EnsureStartsWith(this string str, char c)
        {
            return EnsureStartsWith(str, c, StringComparison.Ordinal);
        }
        /// <summary>
        /// 确保字符串以指定字符开始
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static string EnsureStartsWith(this string str, char c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.StartsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return c + str;
        }
        /// <summary>
        /// 确保字符串以指定字符开始
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <param name="ignoreCase"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string EnsureStartsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.StartsWith(c.ToString(culture), ignoreCase, culture))
            {
                return str;
            }

            return c + str;
        }

        /// <summary>
        /// 是否是空字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 是否不是空字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !str.IsNullOrEmpty();
        }

        /// <summary>
        /// 是否是空或者空白串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        /// <summary>
        /// 从左边返回指定长度的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }

        /// <summary>
        /// 是否不是空或者空白字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNullOrWhiteSpace(this string str)
        {
            return !str.IsNotNullOrWhiteSpace();
        }

        /// <summary>
        /// 格式化字符串中的换行符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NormalizeLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// 指定字符在字符串中出现第N次的位置
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int NthIndexOf(this string str, char c, int n)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            var count = 0;
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] != c)
                {
                    continue;
                }

                if ((++count) == n)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 移除字符串中，以postFixes中第一个匹配项结束的部分
        /// </summary>
        /// <param name="str"></param>
        /// <param name="postFixes"></param>
        /// <returns></returns>
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty)
            {
                return string.Empty;
            }

            if (postFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var postFix in postFixes)
            {
                if (str.EndsWith(postFix))
                {
                    return str.Left(str.Length - postFix.Length);
                }
            }

            return str;
        }
        /// <summary>
        /// 移除字符串中，以preFixes中第一个匹配项开始的部分
        /// </summary>
        /// <param name="str"></param>
        /// <param name="preFixes"></param>
        /// <returns></returns>
        public static string RemovePreFix(this string str, params string[] preFixes)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty)
            {
                return string.Empty;
            }

            if (preFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var preFix in preFixes)
            {
                if (str.StartsWith(preFix))
                {
                    return str.Right(str.Length - preFix.Length);
                }
            }

            return str;
        }

        /// <summary>
        /// 获取指定长度的右边部分
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Right(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        /// 按指定的分割符，把字符串转换为数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        ///  按指定的分割符，把字符串转换为数组（可以选择是否移除空项）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="separator"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }
        /// <summary>
        /// 把字符串按换行符分割为字符串数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }
        /// <summary>
        /// 把字符串按换行符分割为字符串数组(可选择移除空行）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }

        /// <summary>
        /// 把字符串首字母转换为小写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="invariantCulture"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string str, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return invariantCulture ? str.ToLowerInvariant() : str.ToLower();
            }

            return (invariantCulture ? char.ToLowerInvariant(str[0]) : char.ToLower(str[0])) + str.Substring(1);
        }

        /// <summary>
        /// 把字符串首字母转换为小写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToLower(culture);
            }

            return char.ToLower(str[0], culture) + str.Substring(1);
        }
        /// <summary>
        /// 转换PascalCase/camelCase 规则的字符串，为一句话，并以空格分割
        /// 例如: "ThisIsSampleSentence" 将被转换为"This is a sample sentence".
        /// </summary>
        /// <param name="str"></param>
        /// <param name="invariantCulture"></param>
        /// <returns></returns>
        public static string ToSentenceCase(this string str, bool invariantCulture = false)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return Regex.Replace(
                str,
                "[a-z][A-Z]",
                m => m.Value[0] + " " + (invariantCulture ? char.ToLowerInvariant(m.Value[1]) : char.ToLower(m.Value[1]))
            );
        }
        /// <summary>
        /// 转换PascalCase/camelCase 规则的字符串，为一句话，并以空格分割
        /// 例如: "ThisIsSampleSentence" 将被转换为"This is a sample sentence".
        /// </summary>
        /// <param name="str"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToSentenceCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1], culture));
        }

        /// <summary>
        /// 把值转换为枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>
        /// 把值转换为枚举类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static T ToEnum<T>(this string value, bool ignoreCase)
           where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        /// <summary>
        /// 把值转换为MD5值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToMd5(this string str)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(str);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 把值转换为特定的MD5值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToH2FMd5(this string str)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(str + "H2F.20181223.L7dmRmdA");
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// 将字符串转换为首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="invariantCulture"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string str, bool invariantCulture = true)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return invariantCulture ? str.ToUpperInvariant() : str.ToUpper();
            }

            return (invariantCulture ? char.ToUpperInvariant(str[0]) : char.ToUpper(str[0])) + str.Substring(1);
        }
        /// <summary>
        /// 将字符串转换为首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string str, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToUpper(culture);
            }

            return char.ToUpper(str[0], culture) + str.Substring(1);
        }
        /// <summary>
        /// 截取指定长度的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Truncate(this string str, int maxLength)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            return str.Left(maxLength);
        }

        /// <summary>
        /// 截取指定长度的字符串，并以...结尾
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string TruncateWithPostfix(this string str, int maxLength)
        {
            return TruncateWithPostfix(str, maxLength, "...");
        }

        /// <summary>
        /// 截取指定长度的字符串，并以指定字符结尾
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <param name="postfix"></param>
        /// <returns></returns>
        public static string TruncateWithPostfix(this string str, int maxLength, string postfix)
        {
            if (str == null)
            {
                return null;
            }

            if (str == string.Empty || maxLength == 0)
            {
                return string.Empty;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            if (maxLength <= postfix.Length)
            {
                return postfix.Left(maxLength);
            }

            return str.Left(maxLength - postfix.Length) + postfix;
        }

        /// <summary>
        /// 对字符串进行格式化,简化string.Format操作
        /// </summary>
        /// <param name="str"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Format(this string str, params object[] obj)
        {
            return string.Format(str, obj);
        }

        /// <summary>
        /// 使用newStr替换字符串中的oldStr
        /// </summary>
        /// <param name="str"></param>
        /// <param name="oldStr"></param>
        /// <param name="newStr"></param>
        /// <returns></returns>
        public static string TryReplace(this string str, string oldStr, string newStr)
        {
            return str.IsNullOrEmpty() ? str : str.Replace(oldStr, newStr);
        }
        /// <summary>
        /// 使用正规对字符串进行替换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string RegexReplace(this string str, string pattern, string replacement)
        {

            return str.IsNullOrEmpty() ? str : Regex.Replace(str, pattern, replacement);
        }

        /// <summary>
        /// 从web.config的ConnectionString中取出指定名称的值
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static string ValueOfConnectionString(this string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].IsNull() ? null : ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
        /// <summary>
        /// 从web.config的AppString中取出指定名称的值
        /// </summary>
        /// <param name="appSetingKey"></param>
        /// <returns></returns>
        public static string ValueOfAppSetting(this string appSetingKey)
        {
            return ConfigurationManager.AppSettings[appSetingKey];
        }  
    }

}
