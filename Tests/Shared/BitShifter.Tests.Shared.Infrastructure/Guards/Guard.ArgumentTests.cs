using System;

using Xunit;
using FluentAssertions;

using BitShifter.Shared.Infrastructure.Guards;

namespace BitShifter.Tests.Shared.Infrastructure.Guards
{
    public partial class GuardTests
    {
        private readonly string argumentExceptionMessage = "Object can't be empty.";

        [Fact]
        public void Guard_Assert_Throw_NullException()
        {
            // Act
            Action sutAction = () => Guard.Assert<object>(null, argumentExceptionMessage);

            // Assert
            sutAction.Should()
                .Throw<ArgumentNullException>()
                .WithMessage($"{argumentExceptionMessage} (Parameter 'obj')");
        }

        [Fact]
        public void Guard_Assert_Throw_NullException_With_Condition()
        {
            // Act
            Action sutAction = () => Guard.Assert<object>(null, argumentExceptionMessage, x => x is null);

            // Assert
            sutAction.Should()
                .Throw<ArgumentNullException>()
                .WithMessage($"{argumentExceptionMessage} (Parameter 'obj')");
        }

        [Fact]
        public void Guard_AssertNotNull_Throw_NullException()
        {
            // Act
            Action sutAction = () => Guard.AssertNotNull<object>(null, argumentExceptionMessage);

            // Assert
            sutAction.Should()
                .Throw<ArgumentNullException>()
                .WithMessage($"{argumentExceptionMessage} (Parameter 'obj')");
        }

    }
}
