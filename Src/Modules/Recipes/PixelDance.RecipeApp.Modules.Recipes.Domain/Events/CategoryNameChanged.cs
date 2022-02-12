using System;
using PixelDance.Shared.Kernel.Common;

namespace PixelDance.Modules.Recipes.Domain.Events
{
    public class CategoryNameChanged : DomainEvent
    {
        public Guid Id { get; }
        public string Name { get; }

        public CategoryNameChanged(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
