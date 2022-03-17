using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace BitShifter.Shared.Infrastructure.Mapping
{
    public static class MappingExtensions
    {
        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(
            this IQueryable queryable, 
            IConfigurationProvider configuration)
                => queryable.ProjectTo<TDestination>(configuration).ToListAsync();
    }
}
