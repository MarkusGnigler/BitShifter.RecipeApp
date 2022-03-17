using System;

using BitShifter.Shared.Kernel.Common;
using BitShifter.Shared.Kernel.Interfaces;
using BitShifter.Shared.Infrastructure.Guards;

namespace BitShifter.Modules.Recipes.Domain.Entities
{
    public class Category : AuditableEntity, IAggregateRoot//, IHasDomainEvent
    {
        public string Name { get; private set; }

        //public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();

        //Only for ef
        private Category() { }

        public Category(string name)
        {
            Name = Guard.AssertNotNullAndNotEmpty<InvalidOperationException>(name, $"{nameof(Name)} is empty");
        }

        public void Update(string name)
        {
            Name = Guard.AssertNotNullAndNotEmpty<InvalidOperationException>(name, $"{nameof(Name)} is empty");

            //DomainEvents.Add(new CategoryNameChanged(Id, Name));
        }
    }
}
