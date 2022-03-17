using System;
using BitShifter.Shared.Abstractions.Mapping;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Application.Categories
{
    public class CategoryDto : IMapFrom<Category>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
