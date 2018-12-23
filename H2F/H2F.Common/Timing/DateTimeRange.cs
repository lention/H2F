using System;
using System.Collections.Generic;
using System.Text;

namespace H2F.Standard .Common.Timing
{
    public class DateTimeRange : IDateTimeRange
    {
        public DateTime StartTime { get ; set  ; }
        public DateTime EndTime { get  ; set ; }
        public TimeSpan TimeSpan { get=>EndTime - StartTime; set => EndTime = StartTime.Add(value); }

        private static DateTime Now => Clock.Now;

        public DateTimeRange(DateTime startTime, DateTime endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public DateTimeRange(DateTime startTime, TimeSpan timeSpan)
        {
            StartTime = startTime;
            TimeSpan = timeSpan;
        }

        public DateTimeRange(IDateTimeRange dateTimeRange)
        {
            StartTime = dateTimeRange.StartTime;
            EndTime = dateTimeRange.EndTime;
        }

        public static DateTimeRange Yesterday
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date.AddDays(-1), now.Date.AddMilliseconds(-1));
            }
        }

        public static DateTimeRange Today
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date, now.Date.AddDays(1).AddMilliseconds(-1));
            }
        }

        public static DateTimeRange Tomorrow
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date.AddDays(1), now.Date.AddDays(2).AddMilliseconds(-1));
            }
        }

        public static DateTimeRange LastMonth
        {
            get
            {
                var now = Now;
                var startTime = now.Date.AddDays(-now.Day + 1).AddMonths(-1);
                var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
                return new DateTimeRange(startTime, endTime);
            }
        }

        public static DateTimeRange ThisMonth
        {
            get
            {
                var now = Now;
                var startTime = now.Date.AddDays(-now.Day + 1);
                var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
                return new DateTimeRange(startTime, endTime);
            }
        }

        public static DateTimeRange NextMonth
        {
            get
            {
                var now = Now;
                var startTime = now.Date.AddDays(-now.Day + 1).AddMonths(1);
                var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
                return new DateTimeRange(startTime, endTime);
            }
        }

        public static DateTimeRange LastYear
        {
            get
            {
                var now = Now;
                return new DateTimeRange(new DateTime(now.Year - 1, 1, 1), new DateTime(now.Year, 1, 1).AddMilliseconds(-1));
            }
        }

        public static DateTimeRange ThisYear
        {
            get
            {
                var now = Now;
                return new DateTimeRange(new DateTime(now.Year, 1, 1), new DateTime(now.Year + 1, 1, 1).AddMilliseconds(-1));
            }
        }

        public static DateTimeRange NextYear
        {
            get
            {
                var now = Now;
                return new DateTimeRange(new DateTime(now.Year + 1, 1, 1), new DateTime(now.Year + 2, 1, 1).AddMilliseconds(-1));
            }
        }

        public static DateTimeRange Last30Days
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.AddDays(-30), now);
            }
        }

        public static DateTimeRange Last30DaysExceptToday
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date.AddDays(-30), now.Date.AddMilliseconds(-1));
            }
        }

        public static DateTimeRange Last7Days
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.AddDays(-7), now);
            }
        }

        public static DateTimeRange Last7DaysExceptToday
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date.AddDays(-7), now.Date.AddMilliseconds(-1));
            }
        }

        public override string ToString()
        {
            return string.Format("[{0} - {1}]", StartTime, EndTime);
        }
    }
}
