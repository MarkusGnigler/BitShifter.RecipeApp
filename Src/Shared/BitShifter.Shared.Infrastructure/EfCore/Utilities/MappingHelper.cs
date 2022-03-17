using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BitShifter.Shared.Infrastructure.EfCore.Utilities
{
    internal static class MappingHelper
    {

        public static readonly ValueConverter<string[], string> STRING_ARRAY_CONVERTER
            = new ValueConverter<string[], string>(
                x => string.Join(",", x), // to converter
                x => x.Split(new[] { ',' })); // from converter

        public static readonly ValueConverter<IEnumerable<string>, string> STRING_ENUMERABLE_CONVERTER
            = new ValueConverter<IEnumerable<string>, string>(
                x => string.Join(",", x), // to converter
                x => x.Split(new[] { ',' })); // from converter

        public static ValueConverter<T, string> EnumStringConverter<T>()
            => new ValueConverter<T, string>(
                x => x.ToString(), // to converter
                x => (T)Enum.Parse(typeof(T), x)); // from converter

        //https://docs.microsoft.com/en-us/ef/core/modeling/value-comparers?tabs=ef5
        public static readonly ValueComparer STANDARD_COMPARER
            = new ValueComparer<List<int>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList());
    }
}
