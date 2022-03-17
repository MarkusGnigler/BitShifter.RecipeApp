using System;
using System.Linq;
using System.Reflection;

namespace BitShifter.Shared.Infrastructure.Utilities
{
    public static class AssemblyScanner
    {
        public static void Scan<TMarker>(
            Action<TMarker> callClass, 
            params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var types = assembly.DefinedTypes.Where(
                    x => typeof(TMarker).IsAssignableFrom(x) 
                        && !x.IsInterface 
                        && !x.IsAbstract);

                types
                    .Select(Activator.CreateInstance)
                    .Cast<TMarker>()
                    .ToList()
                    .ForEach(callClass);
            }
        }
    }
}
