using System;
using BitShifter.Shared.Kernel.Common;

namespace BitShifter.Modules.Recipes.Domain.Events
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
