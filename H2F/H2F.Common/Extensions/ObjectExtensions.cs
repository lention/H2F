using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
//
using Newtonsoft.Json;
namespace H2F.Standard .Common.Extensions
{
    /// <summary>
    /// 功能：常用对象扩展
    /// 作者：何蛟
    /// 创建时间： 2018-12-23 12:05
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 把对象强制转换为另一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T As<T>(this object obj)
           where T : class
        {
            return (T)obj;
        }

        /// <summary>
        /// 把一个对象转换为一个指定类型 <see cref="Convert.ChangeType(object,System.TypeCode)"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T To<T>(this object obj)
            where T : struct
        {
            if (typeof(T) == typeof(Guid))
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(obj.ToString());
            }

            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 检查一个项是否存在于列表中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }

        /// <summary>
        /// 是否非空对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }
        /// <summary>
        /// 是否空对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        } 
    }
}
