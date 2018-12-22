using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H2F.Standard .Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 把时间类型转为Unix tiemstamp格式
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static double ToUnixTimestamp(this DateTime target)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var diff = target - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /// <summary>
        /// 把Unix timestamp格式 转换是时间对象
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime FromUnixTimestamp(this double unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return epoch.AddSeconds(unixTime);
        }

        /// <summary>
        /// 获取一天当中的最后时间（23:59）
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime ToDayEnd(this DateTime target)
        {
            return target.Date.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// 获取当前时间所在周的第一天日期
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="startOfWeek"></param>
        /// <returns></returns>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            var diff = dt.DayOfWeek - startOfWeek;

            if (diff < 0)
                diff += 7;

            return dt.AddDays(-1 * diff).Date;
        }
        /// <summary>
        /// 获取指定月的所有天数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static IEnumerable<DateTime> DaysOfMonth(int year, int month)
        {
            return Enumerable.Range(0, DateTime.DaysInMonth(year, month))
                .Select(day => new DateTime(year, month, day + 1));
        }

        /// <summary>
        /// 当前时间在一个月当中的周数
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns> 
        /// <example>11/29/2011 将返回 5, 因为它是第 5 个星期四</example>
        public static int WeekDayInstanceOfMonth(this DateTime dateTime)
        {
            var y = 0;
            return DaysOfMonth(dateTime.Year, dateTime.Month)
                .Where(date => dateTime.DayOfWeek.Equals(date.DayOfWeek))
                .Select(x => new { n = ++y, date = x })
                .Where(x => x.date.Equals(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day)))
                .Select(x => x.n).FirstOrDefault();
        }
        /// <summary>
        /// 获取当前所在月的总天数
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static int TotalDaysInMonth(this DateTime dateTime)
        {
            return DaysOfMonth(dateTime.Year, dateTime.Month).Count();
        }

        /// <summary>
        /// 转换时间为当前时区对应的时间
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeUnspecified(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
            {
                return date;
            }

            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// 去除时间的毫秒部分
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime TrimMilliseconds(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
        }
    }
}
