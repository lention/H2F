using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2F.Framework.Common.Extension
{
    /// <summary>
    /// 功能：常用对象的扩展方法
    /// 作者：何蛟
    /// 创建时间： 2018-12-9 12:05
    /// </summary>
    public static class ObjectExtension
    {
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
    }
}
