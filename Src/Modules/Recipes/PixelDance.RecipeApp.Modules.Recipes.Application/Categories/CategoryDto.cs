using System;
using PixelDance.Shared.Abstractions.Mapping;
using PixelDance.Modules.Recipes.Domain.Entities;

namespace PixelDance.Modules.Recipes.Application.Categories
{
    public class CategoryDto : IMapFrom<Category>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
