using System;
using System.Collections.Generic;
using System.Text;
//
using H2F.Standard.Common.Extensions;
namespace H2F.Standard .Common.Timing
{
    public static class Clock
    {
        private static IClockProvider _provider;
        public static IClockProvider Provider
        {
            get { return _provider; }
            set {
                if (value.IsNull())
                {
                    throw new ArgumentNullException(nameof(value),"Can not set Clock.Provider to null!");

                }
                _provider = value;

            }
        }

        static Clock()
        { 
            Provider = ClockProviders.Unspecified; 
        }

        public static DateTime Now => Provider.Now;

        public static DateTimeKind Kind => Provider.Kind;

        public static bool SupportsMultipleTimezone => Provider.SupportsMultipleTimezone;

        public static DateTime Normalize(DateTime dateTime)
        {
            return Provider.Normalize(dateTime);
        }
    }
}
