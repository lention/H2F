using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2F.Framework.Common.Helper
{
    /// <summary>
    /// 功能：提供简化的异常处理方式
    /// 作者：何蛟
    /// 创建时间： 2018-12-9 12:44
    /// </summary>
    public static class ExceptionHelper
    {
        public static void HandleAndNoProcess(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
               // LogHelper.Error(ex);
            }
        }

        public static void HandAndProcess(Action action, Action<Exception> errorHandle)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                errorHandle(ex);
            }
        }
    }
}
