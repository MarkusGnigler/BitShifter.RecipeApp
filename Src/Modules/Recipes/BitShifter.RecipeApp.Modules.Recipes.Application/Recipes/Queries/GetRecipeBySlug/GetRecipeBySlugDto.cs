using System;
using System.Collections.Generic;
using AutoMapper;
using BitShifter.Shared.Kernel.Enums;
using BitShifter.Shared.Abstractions.Mapping;
using BitShifter.Modules.Recipes.Domain.Entities;

namespace BitShifter.Modules.Recipes.Application.Recipes.Queries.GetRecipeDetail
{
    public class GetRecipeBySlugDto : IMapFrom<Recipe>
    {
        public Guid Id { get; set; }

        public string Img { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; }
        public string Category { get; set; }
        public string Preparation { get; set; }
        public string Description { get; set; }
        public bool Liked { get; set; }
        public int Position { get; set; }

        public PriorityLevel Priority { get; set; }

        public ICollection<IngredientsDto> Ingredients { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Recipe, GetRecipeBySlugDto>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
            profile.CreateMap<Recipe, GetRecipeBySlugDto>()
                .ForMember(d => d.Category, opt => opt.MapFrom(s => s.Category.Name));
        }
    }
}
