using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
//
using H2F.Standard.Common.Extensions;
namespace H2F.Standard .Common.Reflection.Extensions
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 获取给定程序集的路径或者返回为空，如果没有找到
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static string GetDirectoryPathOrNull(this Assembly assembly)
        {
            var location = assembly.Location;
            if (location.IsNull())
            {
                return null;
            }

            var directory = new FileInfo(location).Directory;
            if (directory.IsNull())
            {
                return null;
            }

            return directory.FullName;
        }
    }
}
