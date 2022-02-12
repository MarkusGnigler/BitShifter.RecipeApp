#nullable enable
using PixelDance.Shared.Kernel.Exceptions;

namespace PixelDance.Shared.Infrastructure.Guards
{
    public static partial class Guard
    {
        public static T AssertNotFound<T>(T? obj)
            where T : class
                => obj ?? throw new EntityNotFoundException();

        public static T AssertNotFound<T>(T? obj, string message)
            where T : class
                => obj ?? throw new EntityNotFoundException(message);

        public static T AssertNotFound<T>(T? obj, string name, string searchkey)
            where T : class
                => obj ?? throw new EntityNotFoundException(name, searchkey);

    }
}
