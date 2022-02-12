using AutoMapper;
using PixelDance.Modules.Recipes.Domain.Entities;
using PixelDance.Shared.Kernel.Enums;
using PixelDance.Shared.Abstractions.Mapping;

namespace PixelDance.Modules.Recipes.Application.Recipes
{
    public class IngredientsDto : IMapFrom<Ingredient>
    {
        public string Title { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public PriorityLevel Priority { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Ingredient, IngredientsDto>()
                .ForMember(d => d.Priority, opt => opt.MapFrom(s => (int)s.Priority));
        }
    }
}
