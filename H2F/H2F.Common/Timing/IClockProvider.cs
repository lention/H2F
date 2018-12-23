using System;
using System.Collections.Generic;
using System.Text;

namespace H2F.Standard .Common.Timing
{
    public interface IClockProvider
    {
        DateTime Now { get; }

        DateTimeKind Kind { get; }

        bool SupportsMultipleTimezone { get; }

        DateTime Normalize(DateTime dateTime);
    }
}
