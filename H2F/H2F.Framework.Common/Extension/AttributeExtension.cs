using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
//
using H2F.Standard.Common.Extensions;
namespace H2F.Framework.Common.Extension
{
    /// <summary>
    /// 功能：属性附加器扩展
    /// 作者：何蛟
    /// 创建时间：2018-12-9 12:50
    /// </summary>
    public static class AttributeExtension
    {
        public static T GetAttribute<T>(this object obj) where T : class
        {
            Type type = obj.GetType();
            return type.GetAttribute<T>();
        }

        public static T GetAttribute<T>(this Type type) where T : class
        {
            Attribute customAttr = type.GetCustomAttribute(typeof(T));
            if (customAttr.IsNotNull())
            {
                return (customAttr as T);
            }
            else
            {
                return default(T);
            }
        }
    }
} 
