using System;
using System.Collections.Generic;
using System.Text;
//
using Newtonsoft.Json;
namespace H2F.Standard.Common.Extensions
{
    /// <summary>
    /// 功能：常用json操作扩展方法，基于Newtonsoft.Json提供常用序列化和反序列化快捷功能
    /// 作者：何蛟
    /// 创建时间： 2018-12-9 12:05
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// 把对象序列化为json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ignoreNull"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, bool ignoreNull = false)
        {
            if (obj.IsNull())
            {
                return null;
            }
            else
            {
                return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings
                {
                    DateFormatString = "yyyy-MM-dd HH:mm:ss",
                    NullValueHandling = (ignoreNull ? NullValueHandling.Ignore : NullValueHandling.Include)
                });
            }
        }

        /// <summary>
        /// 把json字符串转换为指定的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string jsonStr)
        {
            return jsonStr.IsNullOrEmpty() ? default(T) : JsonConvert.DeserializeObject<T>(jsonStr);
        }

        /// <summary>
        /// 把字符串转换为UTF8数组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] SerializeUtf8(this string str)
        {
            return str.IsNull() ? null : Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// 把UTF8数据转换为字符串
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string DeserializeUtf8(this byte[] stream)
        {
            return stream == null ? null : Encoding.UTF8.GetString(stream);
        }

        /// <summary>
        /// 把一个对象序列化为UTF8数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SerializeUtf8JsonFormat(this object obj)
        {
            var json = obj.ToJson();
            return json.IsNull() ? null : json.SerializeUtf8();
        }

        /// <summary>
        /// 把一个UTF8的数据反序列化为一个指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static T DeserializeUtf8JsonFormat<T>(this byte[] stream)
        {
            string str;
            if (stream == null)
            {
                return default(T);
            }
            else
            {
                str = stream.DeserializeUtf8();
                return str.IsNullOrEmpty() ? default(T) : str.ToObject<T>();
            }
        }

    }
}
