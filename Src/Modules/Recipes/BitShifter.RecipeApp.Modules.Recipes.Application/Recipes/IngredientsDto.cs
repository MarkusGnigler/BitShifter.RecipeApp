using AutoMapper;
using BitShifter.Modules.Recipes.Domain.Entities;
using BitShifter.Shared.Kernel.Enums;
using BitShifter.Shared.Abstractions.Mapping;

namespace BitShifter.Modules.Recipes.Application.Recipes
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
