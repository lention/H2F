using System;
using System.Collections.Generic;
using System.Text;

namespace H2F.Standard .Common.Timing
{
    public interface IDateTimeRange
    {
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        TimeSpan TimeSpan { get; set; }
    }
}
