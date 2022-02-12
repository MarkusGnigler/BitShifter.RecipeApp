using System;

using PixelDance.Shared.Kernel.Common;
using PixelDance.Shared.Kernel.Interfaces;
using PixelDance.Shared.Infrastructure.Guards;

namespace PixelDance.Modules.Recipes.Domain.Entities
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
