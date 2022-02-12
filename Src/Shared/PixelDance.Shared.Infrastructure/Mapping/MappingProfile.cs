using System;
using AutoMapper;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using PixelDance.Shared.Abstractions.Mapping;

[assembly: InternalsVisibleTo("PixelDance.Tests.Modules.Recipes.Application")]
namespace PixelDance.Shared.Infrastructure.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(
                AppDomain.CurrentDomain.GetAssemblies());
        }

        private void ApplyMappingsFromAssembly(Assembly[] assemblies)
        {
            string companyName = GetType().FullName.Split('.').First();
            var companyAssemblies = assemblies.Where(t => t.FullName.StartsWith(companyName));

            foreach (var assembly in companyAssemblies)
            {
                var types = assembly.GetExportedTypes()
                    .Where(t => t.GetInterfaces().Any(
                        i => i.IsGenericType 
                             && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                    .ToList();

                foreach (var type in types)
                {
                    var instance = Activator.CreateInstance(type);

                    var methodInfo = type.GetMethod("Mapping")
                        ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                    methodInfo?.Invoke(instance, new object[] { this });
                }
            }
        }
    }
}