using System;
using System.Collections.Generic;
using System.Text;
//
using System.Reflection;
using H2F.Standard.Common.Extensions; 
using System.Linq;

namespace H2F.Standard .Common.Reflection.Extensions
{
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// 获取一个对象上的指定attribute，返回找到的第一个，或者null
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="memberInfo"></param>
        /// <param name="inherit">是否包含继承的attribute</param>
        /// <returns></returns>
        public static TAttribute GetSingleAttributeOrNull<TAttribute>(this MemberInfo memberInfo, bool inherit = true)
            where TAttribute :Attribute
        {
            if (memberInfo.IsNull())
            {
                throw new ArgumentNullException(nameof(memberInfo));
            }

            var attrs = memberInfo.GetCustomAttributes(typeof(TAttribute), inherit).ToArray();
            if (attrs.Length>0)
            {
                return (TAttribute)attrs[0];
            }

            return default(TAttribute);
        }

        /// <summary>
        /// 获取一个对象上的指定attribute(如果当前没有，则从父类上找），返回找到的第一个，或者null
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="type"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static TAttribute GetSingleAttributeOfTypeOrBaseTypesOrNull<TAttribute>(this Type type, bool inherit = true)
            where TAttribute :Attribute
        {
            var attr = type.GetTypeInfo().GetSingleAttributeOrNull<TAttribute>();
            if (attr.IsNotNull())
            {
                return attr;
            }

            if (type.GetTypeInfo().BaseType.IsNull())
            {
                return null;
            }

            return type.GetTypeInfo().BaseType.GetSingleAttributeOfTypeOrBaseTypesOrNull<TAttribute>(inherit);
        }
    }
}
