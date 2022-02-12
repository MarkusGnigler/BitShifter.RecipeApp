using System;

using Xunit;
using FluentAssertions;

using PixelDance.Shared.Infrastructure.Guards;

namespace PixelDance.Tests.Shared.Infrastructure.Guards
{
    public partial class GuardTests
    {
        private readonly string shouldExceptionMessage = "Exception Message";

        [Fact]
        public void Guard_Throw_When_ShouldIsTrue()
        {
            // Act
            Action sutAction = () => Guard.Should(true, shouldExceptionMessage);

            // Assert
            sutAction.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage($"{shouldExceptionMessage} (Parameter 'predicate')");
        }

        [Fact]
        public void Guard_Throw_When_ShouldGenericIsTrue()
        {
            // Arrange
            int quantity = 0;

            // Act
            Action sutAction = () => Guard.Should(quantity, x => x < 1, shouldExceptionMessage);

            // Assert
            sutAction.Should()
                .Throw<ArgumentOutOfRangeException>()
                .WithMessage($"{shouldExceptionMessage} (Parameter 'obj')");
        }
    }
}
