using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H2F.Standard .Common.Extensions
{
    /// <summary>
    /// 功能：常用日期扩展
    /// 作者：何蛟
    /// 创建时间： 2018-12-23 12:05
    /// </summary>
    public static class DayOfWeekExtensions
    {
        /// <summary>
        /// 判断一个日期是否是周末
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.IsIn(DayOfWeek.Saturday, DayOfWeek.Sunday);
        }

        /// <summary>
        /// 判断一个日期是否是工作日
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static bool IsWeekday(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.IsIn(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday);
        }

        /// <summary>
        /// 找到指定月内的第N个星期X
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static DateTime FindNthWeekDayOfMonth(this DayOfWeek dayOfWeek, int year, int month, int n)
        {
            if (n < 1 || n > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            var y = 0;

            var daysOfMonth = DateTimeExtensions.DaysOfMonth(year, month);

            // compensate for "last DayOfWeek in month"
            var totalInstances = dayOfWeek.TotalInstancesInMonth(year, month);
            if (n == 5 && n > totalInstances)
                n = 4;

            var foundDate = daysOfMonth
                .Where(date => dayOfWeek.Equals(date.DayOfWeek))
                .OrderBy(date => date)
                .Select(x => new { n = ++y, date = x })
                .Where(x => x.n.Equals(n)).Select(x => x.date).First(); //black magic wizardry

            return foundDate;
        }

        /// <summary>
        /// 返回指定月的工作天数
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static int TotalInstancesInMonth(this DayOfWeek dayOfWeek, int year, int month)
        {
            return DateTimeExtensions.DaysOfMonth(year, month).Count(date => dayOfWeek.Equals(date.DayOfWeek));
        }

        /// <summary>
        /// 返回指定日期所在月的工作天数
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int TotalInstancesInMonth(this DayOfWeek dayOfWeek, DateTime dateTime)
        {
            return dayOfWeek.TotalInstancesInMonth(dateTime.Year, dateTime.Month);
        }
    }
}
