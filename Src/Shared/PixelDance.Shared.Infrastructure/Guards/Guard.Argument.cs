#nullable enable
using System;

namespace PixelDance.Shared.Infrastructure.Guards
{
    public static partial class Guard
    {
        public static void Assert<T>(T? obj, string message, Func<T, bool>? validator = default)
            where T : class
        {
            if (validator is default(Func<T, bool>))
                AssertNotNull(obj, message);
#pragma warning disable CS8604
            if (validator?.Invoke(obj) ?? true)
                throw new ArgumentNullException(nameof(obj), message);
#pragma warning restore CS8604
        }

        public static T AssertNotNull<T>(T? obj, string message = "")
            where T : class
                => obj ?? throw new ArgumentNullException(nameof(obj), message);
    }
}
