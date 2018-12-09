using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2F.Framework.Common.Extension
{
    /// <summary>
    /// 功能: 异常扩展类，以简单方式获取所有异常信息
    /// 作者：何蛟
    /// 创建时间： 2018-12-9 12:47
    /// </summary>
    public static class ExceptionExtension
    {
        public static Exception GetInnestException(this Exception ex)
        {
            Exception innerEx = ex.InnerException;
            Exception result = ex;
            while (innerEx != null)
            {
                result = innerEx;
                innerEx = innerEx.InnerException;
            }
            return result;
        }

        public static string GetFullExceptionStr(this Exception ex)
        {
            string str = ex.Message;
            if (ex.InnerException != null)
            {
                str += " ==> " + GetFullExceptionStr(ex.InnerException);
            }
            return str;
        }
    }
}
