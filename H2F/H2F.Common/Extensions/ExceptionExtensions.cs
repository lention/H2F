using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;

namespace H2F.Standard.Common.Extensions
{
    /// <summary>
    /// 功能：常用异常方法扩展
    /// 作者：何蛟
    /// 创建时间： 2018-12-23 12:05
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// 重新抛出异常并保留堆栈信息
        /// </summary>
        /// <param name="exception"></param>
        public static void ReThrow(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
        /// <summary>
        /// 获取完整的异常Message内容 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string FullExceptionMessage(this Exception exception)
        {
            if (exception.IsNull())
            {
                return " End.";
            }

            string str = exception.Message;
            str += " --> " + exception.InnerException.FullExceptionMessage();

            return str;
        }
    }
}
