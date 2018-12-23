using System;
using System.Collections.Generic;
using System.Text;
//
using System.Reflection;
using System.IO;
using System.Linq;
namespace H2F.Standard .Common.Reflection
{
    internal static class AssemblyHelper
    {
        /// <summary>
        /// 获取目录下的所有程序集
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="searchOption"></param>
        /// <returns></returns>
        public static List<Assembly> GetAllAssembliesInFolder(string folderPath, SearchOption searchOption)
        {
            var assemblyFiles = Directory
                .EnumerateFiles(folderPath, "*.*", searchOption)
                .Where(f => f.ToLower().EndsWith(".dll") || f.ToLower().EndsWith(".exe"));

            return assemblyFiles.Select(Assembly.LoadFile).ToList();
        }
    }
}
