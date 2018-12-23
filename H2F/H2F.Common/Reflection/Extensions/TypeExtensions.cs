using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
//
using System.Linq;
namespace H2F.Standard .Common.Reflection.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 获取指定类型的程序集信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Assembly GetAssembly(this Type type)
        {
            return type.GetTypeInfo().Assembly;
        }

        /// <summary>
        /// 获取类型上指定的方法，且参数个数也需要匹配
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="pParametersCount"></param>
        /// <param name="pGenericArgumentsCount"></param>
        /// <returns></returns>
        public static MethodInfo GetMethod(this Type type, string methodName, int pParametersCount = 0, int pGenericArgumentsCount = 0)
        {
            return type
                .GetMethods()
                .Where(m => m.Name == methodName)
                .ToList()
                .Select(m => new
                {
                    Method = m,
                    Params = m.GetParameters(),
                    Args = m.GetGenericArguments()
                })
                .Where(m => m.Params.Length == pParametersCount
                       && m.Args.Length == pGenericArgumentsCount
                 )
                .Select(m => m.Method)
                .FirstOrDefault();

        }
    }
}
