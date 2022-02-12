using System;
using PixelDance.Shared.Kernel.Common;

namespace PixelDance.Modules.Recipes.Domain.Events
{
    public class RecipeCreated : DomainEvent
    {
        public Guid Id { get; }

        public RecipeCreated(Guid id)
        {
            Id = id;
        }
    }
}
