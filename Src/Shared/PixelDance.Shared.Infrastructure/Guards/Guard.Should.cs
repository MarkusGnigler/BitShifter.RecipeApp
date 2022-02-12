using System;

namespace PixelDance.Shared.Infrastructure.Guards
{
    public static partial class Guard
    {
        public static void Should(bool predicate, string message)
        {
            if (predicate)
                throw new ArgumentOutOfRangeException(nameof(predicate), message);
        }

        public static T Should<T>(T obj, Func<T, bool> predicate, string message)
            => predicate(obj) == false
                ? obj
                : throw new ArgumentOutOfRangeException(nameof(obj), message);
    }
}
