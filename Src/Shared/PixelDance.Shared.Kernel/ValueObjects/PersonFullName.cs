using System;
using System.Linq;
using System.Collections.Generic;
using PixelDance.Shared.Kernel.Common;

namespace PixelDance.Shared.Kernel.ValueObjects
{
    public class PersonFullName : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }

        // For ef
        private PersonFullName() { }

        private PersonFullName(string firstName, string lastName)
            : base()
        {
            FirstName = firstName;
            LastName = lastName;
            if (this.IsEmpty()) throw new ArgumentNullException("PersonFullName");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }

        public bool IsEmpty()
            => GetEqualityComponents()
                .Select(x => x.ToString())
                .Any(string.IsNullOrEmpty);

        public static PersonFullName Create(string firstName, string lastName)
            => new(firstName, lastName);
        public static PersonFullName Empty()
            => new(null, null);

    }

    //public static class PersonFullNameExtension
    //{
    //    public static void Configure<T>(this OwnedNavigationBuilder<T, PersonFullName> builder)
    //        where T : class
    //    {
    //        var prop = builder.Property(f => f.FirstName);

    //        //prop.HasColumnName("");
    //        //builder.Property(f => f.FirstName).HasColumnName("FirstName").HasDefaultValue("");

    //    }
    //}
}
