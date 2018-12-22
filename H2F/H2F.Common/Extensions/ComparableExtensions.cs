﻿using System;
using System.Collections.Generic;
using System.Text;

namespace H2F.Standard .Common.Extensions
{
    public static class ComparableExtensions
    {
        public static bool IsBetween<T>(this T value, T minInclusiveValue, T maxInclusiveValue) where T : IComparable<T>
        {
            return value.CompareTo(minInclusiveValue) >= 0 && value.CompareTo(maxInclusiveValue) <= 0;
        }
    }
}
