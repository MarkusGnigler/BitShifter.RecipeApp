using System;
using System.Linq;

namespace PixelDance.Shared.Infrastructure.Guards
{
    public static partial class Guard
    {
        //public static void AssertNotNullAndNotEmpty<T>(T[] stringValues, Func<T, string> parser)
        public static void AssertNotNullAndNotEmpty(params string[] stringValues)
        {
            foreach (var value in stringValues)
                AssertNotNullAndNotEmpty(
                    value?.ToString() ?? string.Empty,
                    $"String value can't be empty.");
            //$"String value \"{parser(value)}\" can't be empty.");
        }

        public static void AssertNotNullAndNotEmpty<TException>(params string[] stringValues)
            where TException : Exception, new()
        {
            foreach (var value in stringValues)
                AssertNotNullAndNotEmpty<TException>(
                    value?.ToString() ?? string.Empty,
                    $"String value can't be empty.");
        }

        public static string AssertNotNullAndNotEmpty(string stringValue, string message = "")
            => string.IsNullOrWhiteSpace(stringValue) == false
                ? stringValue
                : throw new ArgumentNullException(nameof(stringValue), message);

        public static string AssertNotNullAndNotEmpty<TException>(string stringValue, string message = "")
            where TException : Exception, new()
                => string.IsNullOrWhiteSpace(stringValue) == false
                    ? stringValue
                    : throw (TException)Activator.CreateInstance(
                        typeof(TException), new[] { message });
    }
}
