using System;
using BitShifter.Shared.Kernel.Common;

namespace BitShifter.Modules.Recipes.Domain.Events
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
