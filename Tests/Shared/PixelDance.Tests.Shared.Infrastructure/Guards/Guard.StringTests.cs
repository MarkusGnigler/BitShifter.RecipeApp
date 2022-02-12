using System;

using Xunit;
using FluentAssertions;

using PixelDance.Shared.Infrastructure.Guards;

namespace PixelDance.Tests.Shared.Infrastructure.Guards
{
    public partial class GuardTests
    {
        private readonly string stringExceptionMessage = "String value can't be empty.";

        [Fact]
        public void Guard_AssertNotNullAndNotEmpty_Throw_StringArray_NullException()
        {
            // Act
            Action sutAction = () => Guard.AssertNotNullAndNotEmpty(new[] { null, string.Empty, " " });

            // Assert
            sutAction.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("stringValue")
                .WithMessage($"{stringExceptionMessage} (Parameter 'stringValue')");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Guard_AssertNotNullAndNotEmpty_Throw_String_NullException(string value)
        {
            // Act
            Action sutAction = () => Guard.AssertNotNullAndNotEmpty(value, stringExceptionMessage);

            // Assert
            sutAction.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("stringValue")
                .WithMessage($"{stringExceptionMessage} (Parameter 'stringValue')");
        }

        [Fact]
        public void Guard_AssertNotNullAndNotEmpty_Throw_String_CustomException()
        {
            // Act
            Action sutAction = () => Guard.AssertNotNullAndNotEmpty<InvalidOperationException>(null, stringExceptionMessage);

            // Assert
            sutAction.Should()
                .Throw<InvalidOperationException>()
                .WithMessage(stringExceptionMessage);
        }
    }
}
