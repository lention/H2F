using System;
using System.Collections.Generic;
using System.Text;

namespace H2F.Standard .Common.Timing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class DisableDateTimeNormalizationAttribute : Attribute
    {

    }
}
