using System;

using Xunit;
using FluentAssertions;

using PixelDance.Shared.Infrastructure.Guards;
using PixelDance.Shared.Kernel.Exceptions;

namespace PixelDance.Tests.Shared.Infrastructure.Guards
{
    public partial class GuardTests
    {
        private readonly string domainExceptionMessage = "Entity was not found.";

        [Fact]
        public void Guard_AssertNotFound_Throw_EntityNotFoundException()
        {
            // Act
            Action sutAction = () => Guard.AssertNotFound<object>(null);

            // Assert
            sutAction.Should()
                .Throw<EntityNotFoundException>()
                .WithMessage(domainExceptionMessage);
        }

        [Fact]
        public void Guard_AssertNotFound_Throw_EntityNotFoundException_WithMessage()
        {
            // Act
            Action sutAction = () => Guard.AssertNotFound<object>(null, domainExceptionMessage);

            // Assert
            sutAction.Should()
                .Throw<EntityNotFoundException>()
                .WithMessage(domainExceptionMessage);
        }

        [Fact]
        public void Guard_AssertNotFound_Throw_EntityNotFoundException_With_NameSearchKey()
        {
            // Act
            Action sutAction = () => Guard.AssertNotFound<object>(null, name: "PropertyName", searchkey: "SearchKey");

            // Assert
            sutAction.Should()
                .Throw<EntityNotFoundException>()
                .WithMessage("Entity \"PropertyName\" (SearchKey) was not found.");
        }

    }
}
